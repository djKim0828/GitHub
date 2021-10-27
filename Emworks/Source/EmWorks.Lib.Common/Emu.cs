using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.Lib.Common
{
    /// <summary>
    /// <see cref="Emu"/> is the EmWorks Utilities Class
    /// </summary>
    public partial class Emu
    {
        /// <summary>
        /// Comma Procedure
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CommaProcedure(string value)
        {
            // 3자리마다 콤마 삽입
            return CommaProcedure(Double.Parse(value), value);
        }

        /// <summary>
        /// Comma Procedure
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CommaProcedure(double v, string s)
        {
            int pos = 0;
            if (s.Contains("."))
            {
                pos = s.Length - s.IndexOf('.');    // 소수점 아래 자리수 + 1
                if (pos == 1)   // 맨 뒤에 소수점이 있으면 그대로 리턴
                    return s;
                string formatStr = "{0:N" + (pos - 1) + "}";
                s = string.Format(formatStr, v);
            }
            else
                s = string.Format("{0:N0}", v);
            return s;
        }


        public static T DeepClone<T>(T targetObject)
        {
            Type type = targetObject.GetType();
            T clone = (T)Activator.CreateInstance(type);
            // 클래스 내부에 있는 모든 변수를 가져온다.
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // 변수가 Class 타입이면 그 재귀를 통해 다시 복제한다. 단, String은 class이지만 구조체처럼 사용되기 때문에 예외
                if (field.FieldType.IsClass && field.FieldType != typeof(String))
                {
                    // 새로운 클래스에 데이터를 넣는다.
                    field.SetValue(clone, DeepClone(field.GetValue(targetObject)));
                    continue;
                }
                // 새로운 클래스에 데이터를 넣는다.
                field.SetValue(clone, field.GetValue(targetObject));
            }
            return clone;
        }

        /// <summary>
        /// Insert comma every 3 digits
        /// <param name="value"></param>
        /// <returns></returns>
        public static string InsertComma(string value)
        {
            try
            {
                return InsertComma(Double.Parse(value), value);
            }
            catch
            {
                return value;
            }
        }

        /// <summary>
        /// Insert comma every 3 digits
        /// <param name="value"></param>
        /// <returns></returns>
        public static string InsertComma(double value)
        {
            try
            {
                return InsertComma(value, value.ToString());
            }
            catch
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Insert comma every 3 digits
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string InsertComma(double v, string s)
        {
            int pos = 0;
            if (s.Contains("."))
            {
                pos = s.Length - s.IndexOf('.');    // 소수점 아래 자리수 + 1
                if (pos == 1)   // 맨 뒤에 소수점이 있으면 그대로 리턴
                    return s;
                string formatStr = "{0:N" + (pos - 1) + "}";
                s = string.Format(formatStr, v);
            }
            else
                s = string.Format("{0:N0}", v);
            return s;
        }

        /// <summary>
        /// 스레드로 함수 실행 한 후, 업테이트 할 함수를 호출해 주는 기능.
        /// </summary>
        /// <param name="exeFunction">실행 함수</param>
        /// <param name="exeFunctionArg">실행 함수 인자</param>
        /// <param name="updateFunctions">실행 후 호출될 업데이트 함수</param>
        /// <returns></returns>
        public static int ExcuteUpdate(Func<object, int> exeFunction, object exeFunctionArg, Func<object, int> updateFunctions)
        {
            int ret = 0;
            Task<int>.Factory.StartNew(() => exeFunction(exeFunctionArg)).ContinueWith(o =>
            {
                ret = o.Result;
                updateFunctions(ret);
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return ret;

            #region example for using UpdateCtrlAfterRead()
            //private void btnCallback_Click(object sender, EventArgs e)
            //{
            //    ServiceClass sc = new ServiceClass();
            //    int ret = excuteUpdate(ExeFunction, Convert.ToInt32(txtCallbackResult.Text), updateFunction);
            //}
            //public int ExeFunction(object exeFunctionArg)
            //{
            //    Thread.Sleep(5000);
            //    return (int)value * 10;
            //}
            //private int updateFunction(object returnValue)
            //{
            //    txtCallbackResult.Text = returnValue.ToString();
            //    return 0;
            //}
            #endregion
        }
        public static string CurrentTime
        {
            get
            {
                return $"{DateTime.Now:yyyy / MM / dd HH: mm: ss.fff}";
            }
        }

        public static Dictionary<int, string> SplitSlash(string value)
        {
            return Split('/', value);
        }
        public static Dictionary<int, string> SplitComma(string value)
        {
            return Split(',', value);
        }
        public static Dictionary<int, string> SplitColon(string value)
        {
            return Split(':', value);
        }
        public static Dictionary<int, string> SplitSemiColon(string value)
        {
            return Split(';', value);
        }
        public static Dictionary<int, string> Split(char spliter, string value)
        {
            string[] array = value.Split(spliter);
            if (array == null)
                return new Dictionary<int, string>();

            Dictionary<int, string> ret = new Dictionary<int, string>();
            string item = string.Empty;
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    ret.Add(i, array[i]);
                }
            }
            catch (Exception ex)
            {
                Trace.Assert(false, string.Format($"{ex.Message} value : {item}"));
            }

            return ret;
        }

        /// <summary>
        /// 실행파일 명 뒤의 인자를 읽어줌 (구별문자는 스페이스)
        /// </summary>
        /// <returns>string array</returns>
        public static string[] GetCommandLineArgs()
        {
            // "aaa.exe simulation" 실행하면 strint[0] 실행파일 full path, string[1] simulation
            // Environment.GetCommandLineArgs().Select(item => item.ToUpper());
            return Environment.GetCommandLineArgs();
        }
        /// <summary>
        /// 특정 Directory 날짜 기준으로 삭세하는 함수
        /// 사용예) DeleteOldFiles("D:/EmWorks Files/Logs",-30);
        /// </summary>
        /// <param name="dirPath">대상 디렉토리 경로</param>
        /// <param name="dateLimit"> 음수 (예) -30 </param>
        public static void DeleteOldFiles(string dirPath, int dateLimit)
        {
            DirectoryInfo dir_info = new DirectoryInfo(dirPath);
            DateTime cmp_time = DateTime.ParseExact(DateTime.Now.AddDays(dateLimit).ToString("yyyyMMdd"), "yyyyMMdd", null);

            foreach (var file in dir_info.GetFiles())
            {
                #region 설명
                //DateTime date1 = new DateTime(2020, 04, 19, 0, 0, 0);
                //DateTime date2 = new DateTime(2020, 04, 18, 0, 0, 0);
                //int result = DateTime.Compare(date2, date1); //-1

                //date1 = new DateTime(2020, 04, 19, 0, 0, 0);
                //date2 = new DateTime(2020, 05, 18, 0, 0, 0);
                //result = DateTime.Compare(date2, date1); //1

                //date1 = new DateTime(2020, 04, 19, 0, 0, 0);
                //date2 = new DateTime(2020, 04, 19, 0, 0, 0);
                //result = DateTime.Compare(date2, date1); //0
                #endregion
                if (DateTime.Compare(file.CreationTime, cmp_time) < 0) // 비교 결과 -1일 경우
                {
                    try
                    {
                        File.Delete(file.FullName); // file이 열려 있으면 Error 발생 함.
                    }
                    catch
                    {
                        // 설정기간에 파일이 삭제되지 않을 경우 확인.
                    }
                }
            }
        }

        /// <summary>
        /// 특정 Directory에서 가장 최근 파일 이름을 가져오는 함수.
        /// 사용예) string recent = Utility.GetRecentFileName("D:/EmWorks Files/Logs","*.txt");
        /// </summary>
        /// <param name="dirPath">대상 디렉토리 경로</param>
        /// <param name="extensionName"> 확장자 명</param>
        /// <returns></returns>
        public static string GetRecentFileName(string dirPath, string extensionName)
        {
            string[] file_list = Directory.GetFiles(dirPath, extensionName);
            string file_name = null;
            List<FileInfo> files = new List<FileInfo>();
            if (file_list != null || file_list.Length > 0)
            {
                for (int i = 0; i < file_list.Length; i++)
                    files.Add(new FileInfo(file_list[i]));
                var query = from a in files orderby a.CreationTime descending select a;
                file_name = files.OrderBy(o => o.CreationTime).Last().FullName;
            }
            return file_name;
        }
    }
    public class SingletonObject<T> where T : class, new()
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }

        protected SingletonObject()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
        }
    }
    public static class Spliter
    {
        #region Fields

        public static char Comma = ',';
        public static char Slash = '/';

        #endregion Fields
    }
    public static class EnumConvert<T>
    {
        public static string ToString(int level)
        {
            return Enum.GetName(typeof(T), level);
        }
        public static T ToEnum(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
        public static T ToEnum(int level)
        {
            string name = ToString(level);
            return (T)Enum.Parse(typeof(T), name);
        }
    }
    public class EventAgrsWrapper<T> where T : new()
    {
        public T Inst { get; set; }

        protected T GetObject()
        {
            if (Inst == null)
                return Inst = new T();

            return Inst;
        }

        public EventAgrsWrapper() { }

        public EventAgrsWrapper(T obj)
        {
            Inst = obj;
        }

        public EventAgrsWrapper(object value)
        {
            Inst = GetObject();
            try
            {
                Type t = Inst.GetType();
                PropertyInfo p = t.GetRuntimeProperty("Value");
                p?.SetValue(Inst, value);
            }
            catch (Exception ex)
            {
                Trace.Assert(false, ex.Message);
            }

            //object v = p.GetValue(Inst);
        }

        public EventAgrsWrapper(int id, string name, string message)
        {
            Inst = GetObject();
            try
            {
                Type t = Inst.GetType();
                PropertyInfo p = t.GetRuntimeProperty("Id");
                p?.SetValue(Inst, id);

                p = t.GetRuntimeProperty("Name");
                p?.SetValue(Inst, name);

                p = t.GetRuntimeProperty("Message");
                p?.SetValue(Inst, message);

                //object v = p.GetValue(Inst); 
                //MethodInfo m = t.GetMethod("Send");
                // out or ref로 인자를 통해 값을 받을 경우, object [] v = new object[2]; 선언해서 넘겨주면 해당 Index로 넘어온다.
                //m.Invoke(Inst, new object[] { p.GetValue(Inst) }); // return도 받을 수 있음.
            }
            catch (Exception ex)
            {
                Trace.Assert(false, ex.Message);
            }
        }

    }

    public class GetTime
    {
        public static string TimeFormat { get; set; } = "yy / MM / dd HH: mm: ss";
        public static DateTime Korea
        {
            get
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Korea Standard Time");
            }
        }
        public string IsKorea
        {
            get
            {
                return Korea.ToString(TimeFormat);
            }
        }
        public DateTime China
        {
            get
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "China Standard Time");
            }
        }
        public string IsChina
        {
            get
            {
                return China.ToString(TimeFormat);
            }
        }
        /// <summary>
        /// Bangkok, Hanoi, Jakarta
        /// </summary>
        public DateTime Vietnam
        {
            get
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time");
            }
        }
        /// <summary>
        /// Bangkok, Hanoi, Jakarta
        /// </summary>
        public string IsVietnam
        {
            get
            {
                return Vietnam.ToString(TimeFormat);
            }
        }
        private void Locale()
        {
        // <add key="Id000" value="0,Dateline Standard Time,날짜 변경선 표준시,(UTC-12:00) 날짜 변경선 서쪽,날짜 변경선 일광 절약 시간,-12:00:00,Korea Standard Time" />
        //<add key="Id001" value="1,UTC-11,UTC-11,(UTC-11:00) 협정 세계시-11,UTC-11,-11:00:00,Korea Standard Time" />
        //<add key="Id002" value="2,Aleutian Standard Time,알류샨 표준시,(UTC-10:00) 알류샨 열도,알류샨 일광 절약 시간,-10:00:00,Korea Standard Time" />
        //<add key="Id003" value="3,Hawaiian Standard Time,하와이 표준시,(UTC-10:00) 하와이,하와이 일광 절약 시간,-10:00:00,Korea Standard Time" />
        //<add key="Id004" value="4,Marquesas Standard Time,마키저스 표준시,(UTC-09:30) 마키저스 제도,마키저스 일광 절약 시간,-09:30:00,Korea Standard Time" />
        //<add key="Id005" value="5,Alaskan Standard Time,알래스카 표준시,(UTC-09:00) 알래스카,알래스카 태평양 일광 절약 시간,-09:00:00,Korea Standard Time" />
        //<add key="Id006" value="6,UTC-09,UTC-09,(UTC-09:00) 협정 세계시-09,UTC-09,-09:00:00,Korea Standard Time" />
        //<add key="Id007" value="7,Yukon Standard Time,유콘 표준시,(UTC-07:00) 유콘,유콘 일광 절약 시간,-08:00:00,Korea Standard Time" />
        //<add key="Id008" value="8,Pacific Standard Time (Mexico),태평양 표준시(멕시코),(UTC-08:00) 바하 캘리포니아,태평양 일광 절약 시간(멕시코),-08:00:00,Korea Standard Time" />
        //<add key="Id009" value="9,Pacific Standard Time,태평양 표준시,(UTC-08:00) 태평양 표준시 (미국과 캐나다),태평양 일광 절약 시간,-08:00:00,Korea Standard Time" />
        //<add key="Id010" value="10,UTC-08,UTC-08,(UTC-08:00) 협정 세계시-08,UTC-08,-08:00:00,Korea Standard Time" />
        //<add key="Id011" value="11,Mountain Standard Time,산지 표준시,(UTC-07:00) 산지 표준시 (미국과 캐나다),산지 일광 절약 시간,-07:00:00,Korea Standard Time" />
        //<add key="Id012" value="12,US Mountain Standard Time,미국 산지 표준시,(UTC-07:00) 애리조나,미국 산지 일광 절약 시간,-07:00:00,Korea Standard Time" />
        //<add key="Id013" value="13,Mountain Standard Time (Mexico),산지 표준시(멕시코),(UTC-07:00) 치와와, 라파스, 마사틀란,산지 일광 절약 시간(멕시코),-07:00:00,Korea Standard Time" />
        //<add key="Id014" value="14,Central Standard Time (Mexico),중부 표준시(멕시코),(UTC-06:00) 과달라하라, 멕시코시티, 몬테레이,중부 일광 절약 시간(멕시코),-06:00:00,Korea Standard Time" />
        //<add key="Id015" value="15,Canada Central Standard Time,캐나다 중부 표준시,(UTC-06:00) 서스캐처원,캐나다 중부 일광 절약 시간,-06:00:00,Korea Standard Time" />
        //<add key="Id016" value="16,Easter Island Standard Time,이스터 섬 표준시,(UTC-06:00) 이스터 섬,이스터 섬 일광 절약 시간,-06:00:00,Korea Standard Time" />
        //<add key="Id017" value="17,Central Standard Time,중부 표준시,(UTC-06:00) 중부 표준시 (미국과 캐나다),중부 일광 절약 시간,-06:00:00,Korea Standard Time" />
        //<add key="Id018" value="18,Central America Standard Time,중앙 아메리카 표준시,(UTC-06:00) 중앙 아메리카,중앙 아메리카 일광 절약 시간,-06:00:00,Korea Standard Time" />
        //<add key="Id019" value="19,Eastern Standard Time,동부 표준시,(UTC-05:00) 동부 표준시 (미국과 캐나다),동부 일광 절약 시간,-05:00:00,Korea Standard Time" />
        //<add key="Id020" value="20,SA Pacific Standard Time,SA 태평양 표준시,(UTC-05:00) 보고타, 리마, 키토, 리오 브랑코,SA 태평양 일광 절약 시간,-05:00:00,Korea Standard Time" />
        //<add key="Id021" value="21,Haiti Standard Time,아이티 표준시,(UTC-05:00) 아이티,아이티 일광 절약 시간,-05:00:00,Korea Standard Time" />
        //<add key="Id022" value="22,US Eastern Standard Time,미국 동부 표준시,(UTC-05:00) 인디애나 (동부),미국 동부 일광 절약 시간,-05:00:00,Korea Standard Time" />
        //<add key="Id023" value="23,Eastern Standard Time (Mexico),동부 표준시(멕시코),(UTC-05:00) 체투말,동부 일광 절약 시간(멕시코),-05:00:00,Korea Standard Time" />
        //<add key="Id024" value="24,Turks And Caicos Standard Time,터크스 케이커스 표준시,(UTC-05:00) 터크스 케이커스,터크스 케이커스 일광 절약 시간,-05:00:00,Korea Standard Time" />
        //<add key="Id025" value="25,Cuba Standard Time,쿠바 표준시,(UTC-05:00) 하바나,쿠바 일광 절약 시간,-05:00:00,Korea Standard Time" />
        //<add key="Id026" value="26,Atlantic Standard Time,대서양 표준시,(UTC-04:00) 대서양 표준시 (캐나다),대서양 일광 절약 시간,-04:00:00,Korea Standard Time" />
        //<add key="Id027" value="27,Pacific SA Standard Time,태평양 SA 표준시,(UTC-04:00) 산티아고,태평양 SA 일광 절약 시간,-04:00:00,Korea Standard Time" />
        //<add key="Id028" value="28,Paraguay Standard Time,파라과이 표준시,(UTC-04:00) 아순시온,파라과이 일광 절약 시간,-04:00:00,Korea Standard Time" />
        //<add key="Id029" value="29,SA Western Standard Time,SA 서부 표준시,(UTC-04:00) 조지타운, 라파스, 마노스, 산후안,SA 서부 일광 절약 시간,-04:00:00,Korea Standard Time" />
        //<add key="Id030" value="30,Venezuela Standard Time,베네수엘라 표준시,(UTC-04:00) 카라카스,베네수엘라 일광 절약 시간,-04:00:00,Korea Standard Time" />
        //<add key="Id031" value="31,Central Brazilian Standard Time,브라질 중부 표준시,(UTC-04:00) 쿠이아바,브라질 중부 일광 절약 시간,-04:00:00,Korea Standard Time" />
        //<add key="Id032" value="32,Newfoundland Standard Time,뉴펀들랜드 표준시,(UTC-03:30) 뉴펀들랜드,뉴펀들랜드 일광 절약 시간,-03:30:00,Korea Standard Time" />
        //<add key="Id033" value="33,Greenland Standard Time,그린란드 표준시,(UTC-03:00) 그린란드,그린란드 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id034" value="34,Montevideo Standard Time,몬테비디오 표준시,(UTC-03:00) 몬테비디오,몬테비디오 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id035" value="35,Argentina Standard Time,아르헨티나 표준 시간,(UTC-03:00) 부에노스아이레스,아르헨티나 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id036" value="36,E. South America Standard Time,동남부 아메리카 표준시,(UTC-03:00) 브라질리아,동남부 아메리카 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id037" value="37,Bahia Standard Time,바이아 표준시,(UTC-03:00) 살바도르,바이아 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id038" value="38,Saint Pierre Standard Time,생피에르 표준시,(UTC-03:00) 생피에르앤드미클롱,생피에르 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id039" value="39,Tocantins Standard Time,토칸칭스 표준시,(UTC-03:00) 아라구아이나,토칸칭스 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id040" value="40,SA Eastern Standard Time,SA 동부 표준시,(UTC-03:00) 카옌, 포르탈레자,SA 동부 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id041" value="41,Magallanes Standard Time,마가야네스 표준시,(UTC-03:00) 푼타아레나스,마가야네스 일광 절약 시간,-03:00:00,Korea Standard Time" />
        //<add key="Id042" value="42,Mid-Atlantic Standard Time,중부-대서양 표준시,(UTC-02:00) 중부-대서양 - 이전,중부-대서양 일광 절약 시간,-02:00:00,Korea Standard Time" />
        //<add key="Id043" value="43,UTC-02,UTC-02,(UTC-02:00) 협정 세계시-02,UTC-02,-02:00:00,Korea Standard Time" />
        //<add key="Id044" value="44,Azores Standard Time,아조레스 표준시,(UTC-01:00) 아조레스,아조레스 일광 절약 시간,-01:00:00,Korea Standard Time" />
        //<add key="Id045" value="45,Cape Verde Standard Time,카보베르데 표준 시간,(UTC-01:00) 카보베르데 제도,카보베르데 일광 절약 시간,-01:00:00,Korea Standard Time" />
        //<add key="Id046" value="46,UTC,협정 세계시,(UTC) 협정 세계시,협정 세계시,00:00:00,Korea Standard Time" />
        //<add key="Id047" value="47,GMT Standard Time,GMT 표준시,(UTC+00:00) 더블린, 에든버러, 리스본, 런던,GMT 일광 절약 시간,00:00:00,Korea Standard Time" />
        //<add key="Id048" value="48,Greenwich Standard Time,그리니치 표준시,(UTC+00:00) 몬로비아, 레이캬비크,그리니치 일광 절약 시간,00:00:00,Korea Standard Time" />
        //<add key="Id049" value="49,Sao Tome Standard Time,상투메 표준시,(UTC+00:00) 상투메,상투메 일광 절약 시간,00:00:00,Korea Standard Time" />
        //<add key="Id050" value="50,Morocco Standard Time,모로코 표준 시간,(UTC+01:00) 카사블랑카,모로코 일광 절약 시간,00:00:00,Korea Standard Time" />
        //<add key="Id051" value="51,Central Europe Standard Time,중앙 유럽 표준시 ,(UTC+01:00) 베오그라드,브라티슬라바,부다페스트,류블랴나,프라하,중앙 유럽 일광 절약 시간 ,01:00:00,Korea Standard Time" />
        //<add key="Id052" value="52,Romance Standard Time,로망스 표준시,(UTC+01:00) 브뤼셀, 코펜하겐, 마드리드, 파리,로망스 일광 절약 시간,01:00:00,Korea Standard Time" />
        //<add key="Id053" value="53,Central European Standard Time,중앙 유럽 표준시,(UTC+01:00) 사라예보, 스코페, 바르샤바, 자그레브,중앙 유럽 일광 절약 시간,01:00:00,Korea Standard Time" />
        //<add key="Id054" value="54,W. Central Africa Standard Time,서중앙 아프리카 표준시,(UTC+01:00) 서중앙 아프리카,서중앙 아프리카 일광 절약 시간,01:00:00,Korea Standard Time" />
        //<add key="Id055" value="55,W. Europe Standard Time,서유럽 표준시,(UTC+01:00) 암스테르담, 베를린, 베른, 로마, 스톡홀름, 빈,서유럽 일광 절약 시간,01:00:00,Korea Standard Time" />
        //<add key="Id056" value="56,West Bank Standard Time,팔레스타인 영토 표준시,(UTC+02:00) 가자, 헤브론,팔레스타인 영토 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id057" value="57,Syria Standard Time,시리아 표준시,(UTC+02:00) 다마스쿠스,시리아 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id058" value="58,Middle East Standard Time,중동 표준시,(UTC+02:00) 베이루트,중동 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id059" value="59,Namibia Standard Time,나미비아 표준시,(UTC+02:00) 빈트후크,나미비아 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id060" value="60,GTB Standard Time,GTB 표준시,(UTC+02:00) 아테네, 부카레스트,GTB 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id061" value="61,Jordan Standard Time,요르단 표준시,(UTC+02:00) 암만,요르단 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id062" value="62,Israel Standard Time,예루살렘 표준시,(UTC+02:00) 예루살렘,예루살렘 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id063" value="63,Egypt Standard Time,이집트 표준시,(UTC+02:00) 카이로,이집트 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id064" value="64,Kaliningrad Standard Time,러시아 TZ 1 표준시,(UTC+02:00) 칼리닌그라드,러시아 TZ 1 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id065" value="65,E. Europe Standard Time,동유럽 표준시,(UTC+02:00) 키시네프,동유럽 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id066" value="66,Libya Standard Time,리비아 표준시,(UTC+02:00) 트리폴리,리비아 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id067" value="67,South Africa Standard Time,남아프리카 표준시,(UTC+02:00) 하라레, 프리토리아,남아프리카 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id068" value="68,Sudan Standard Time,수단 표준시,(UTC+02:00) 하르툼,수단 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id069" value="69,FLE Standard Time,FLE 표준시,(UTC+02:00) 헬싱키, 키예프, 리가, 소피아, 탈린, 빌뉴스,FLE 일광 절약 시간,02:00:00,Korea Standard Time" />
        //<add key="Id070" value="70,E. Africa Standard Time,동아프리카 표준시,(UTC+03:00) 나이로비,동아프리카 일광 절약 시간,03:00:00,Korea Standard Time" />
        //<add key="Id071" value="71,Russian Standard Time,러시아 TZ 2 표준시,(UTC+03:00) 모스크바, 상트페테르부르크,러시아 TZ 2 일광 절약 시간,03:00:00,Korea Standard Time" />
        //<add key="Id072" value="72,Belarus Standard Time,벨로루시 표준시,(UTC+03:00) 민스크,벨로루시 일광 절약 시간,03:00:00,Korea Standard Time" />
        //<add key="Id073" value="73,Arabic Standard Time,아랍 표준시,(UTC+03:00) 바그다드,아랍 일광 절약 시간,03:00:00,Korea Standard Time" />
        //<add key="Id074" value="74,Turkey Standard Time,터키 표준 시간,(UTC+03:00) 이스탄불,터키 일광 절약 시간,03:00:00,Korea Standard Time" />
        //<add key="Id075" value="75,Arab Standard Time,아랍 표준시 ,(UTC+03:00) 쿠웨이트, 리야드,아랍 일광 절약 시간 ,03:00:00,Korea Standard Time" />
        //<add key="Id076" value="76,Iran Standard Time,이란 표준시,(UTC+03:30) 테헤란,이란 일광 절약 시간,03:30:00,Korea Standard Time" />
        //<add key="Id077" value="77,Azerbaijan Standard Time,아제르바이잔 표준시,(UTC+04:00) 바쿠,아제르바이잔 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id078" value="78,Volgograd Standard Time,볼고그라드 표준시,(UTC+04:00) 볼고그라드,볼고그라드일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id079" value="79,Saratov Standard Time,사라토브 표준시,(UTC+04:00) 사라토브,사라토브 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id080" value="80,Arabian Standard Time,아랍 표준시  ,(UTC+04:00) 아랍: 아부다비, 무스카트,아랍 일광 절약 시간  ,04:00:00,Korea Standard Time" />
        //<add key="Id081" value="81,Astrakhan Standard Time,아스트라한 표준시,(UTC+04:00) 아스트라한, 울랴노브스크,아스트라한 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id082" value="82,Caucasus Standard Time,코코서스 표준시,(UTC+04:00) 예레반,코코서스 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id083" value="83,Russia Time Zone 3,러시아 TZ 3 표준시,(UTC+04:00) 이젭스크, 사마라,러시아 TZ 3 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id084" value="84,Georgian Standard Time,그루지야 표준시,(UTC+04:00) 트빌리시,그루지야 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id085" value="85,Mauritius Standard Time,모리셔스 표준 시간,(UTC+04:00) 포트루이스,모리셔스 일광 절약 시간,04:00:00,Korea Standard Time" />
        //<add key="Id086" value="86,Afghanistan Standard Time,아프가니스탄 표준시,(UTC+04:30) 카불,아프가니스탄 일광 절약 시간,04:30:00,Korea Standard Time" />
        //<add key="Id087" value="87,West Asia Standard Time,서아시아 표준시 ,(UTC+05:00) 아슈하바트, 타슈켄트,서아시아 일광 절약 시간제 ,05:00:00,Korea Standard Time" />
        //<add key="Id088" value="88,Ekaterinburg Standard Time,러시아 TZ 4 표준시,(UTC+05:00) 예카테린부르크,러시아 TZ 4 일광 절약 시간,05:00:00,Korea Standard Time" />
        //<add key="Id089" value="89,Pakistan Standard Time,파키스탄 표준 시간,(UTC+05:00) 이슬라마바드, 카라치,파키스탄 일광 절약 시간,05:00:00,Korea Standard Time" />
        //<add key="Id090" value="90,Qyzylorda Standard Time,키질로르다 표준시,(UTC+05:00) 키질로르다,키질로르다 일광 절약 시간,05:00:00,Korea Standard Time" />
        //<add key="Id091" value="91,Sri Lanka Standard Time,스리랑카 표준시,(UTC+05:30) 스리자야와르데네푸라,스리랑카 일광 절약 시간,05:30:00,Korea Standard Time" />
        //<add key="Id092" value="92,India Standard Time,인도 표준시,(UTC+05:30) 첸나이, 콜카타, 뭄바이, 뉴델리,인도 일광 절약 시간제,05:30:00,Korea Standard Time" />
        //<add key="Id093" value="93,Nepal Standard Time,네팔 표준시,(UTC+05:45) 카트만두,네팔 일광 절약 시간,05:45:00,Korea Standard Time" />
        //<add key="Id094" value="94,Bangladesh Standard Time,방글라데시 표준시,(UTC+06:00) 다카,방글라데시 일광 절약 시간,06:00:00,Korea Standard Time" />
        //<add key="Id095" value="95,Central Asia Standard Time,중앙 아시아 표준시,(UTC+06:00) 아스타나,중앙 아시아 일광 절약 시간,06:00:00,Korea Standard Time" />
        //<add key="Id096" value="96,Omsk Standard Time,옴스크 표준시,(UTC+06:00) 옴스크,옴스크 일광 절약 시간,06:00:00,Korea Standard Time" />
        //<add key="Id097" value="97,Myanmar Standard Time,미얀마 표준시,(UTC+06:30) 양곤,미얀마 일광 절약 시간,06:30:00,Korea Standard Time" />
        //<add key="Id098" value="98,N. Central Asia Standard Time,노보시비르스크 표준시,(UTC+07:00) 노보시비르스크,노보시비르스크 일광 절약 시간,07:00:00,Korea Standard Time" />
        //<add key="Id099" value="99,Altai Standard Time,알타이 표준시,(UTC+07:00) 바르나울, 고르노알타이스크,알타이 일광 절약 시간,07:00:00,Korea Standard Time" />
        //<add key="Id100" value="100,SE Asia Standard Time,동남 아시아 표준시,(UTC+07:00) 방콕, 하노이, 자카르타,동남 아시아 일광 절약 시간,07:00:00,Korea Standard Time" />
        //<add key="Id101" value="101,North Asia Standard Time,러시아 TZ 6 표준시,(UTC+07:00) 크라스노야르스크,러시아 TZ 6 일광 절약 시간,07:00:00,Korea Standard Time" />
        //<add key="Id102" value="102,Tomsk Standard Time,톰스크 표준시,(UTC+07:00) 톰스크,톰스크 일광 절약 시간,07:00:00,Korea Standard Time" />
        //<add key="Id103" value="103,W. Mongolia Standard Time,서몽골 표준시,(UTC+07:00) 호브드,서몽골 일광 절약 시간,07:00:00,Korea Standard Time" />
        //<add key="Id104" value="104,China Standard Time,중국 표준시,(UTC+08:00) 베이징, 충칭, 홍콩 특별 행정구, 우루무치,중국 일광 절약 시간,08:00:00,Korea Standard Time" />
        //<add key="Id105" value="105,Ulaanbaatar Standard Time,울란바토르 표준시,(UTC+08:00) 울란바토르,울란바토르 일광 절약 시간,08:00:00,Korea Standard Time" />
        //<add key="Id106" value="106,North Asia East Standard Time,러시아 TZ 7 표준시,(UTC+08:00) 이르쿠츠크,러시아 TZ 7 일광 절약 시간,08:00:00,Korea Standard Time" />
        //<add key="Id107" value="107,Singapore Standard Time,말레이 반도 표준시,(UTC+08:00) 콸라룸푸르, 싱가포르,말레이 반도 일광 절약 시간,08:00:00,Korea Standard Time" />
        //<add key="Id108" value="108,Taipei Standard Time,타이베이 표준시,(UTC+08:00) 타이베이,타이베이 일광 절약 시간,08:00:00,Korea Standard Time" />
        //<add key="Id109" value="109,W. Australia Standard Time,서부 오스트레일리아 표준시,(UTC+08:00) 퍼스,서부 오스트레일리아 일광 절약 시간,08:00:00,Korea Standard Time" />
        //<add key="Id110" value="110,Aus Central W. Standard Time,오스트레일리아 중부 표준시,(UTC+08:45) 유클라,오스트레일리아 중부 일광 절약 시간,08:45:00,Korea Standard Time" />
        //<add key="Id111" value="111,Korea Standard Time,대한민국 표준시,(UTC+09:00) 서울,대한민국 일광 절약 시간,09:00:00,Korea Standard Time" />
        //<add key="Id112" value="112,Yakutsk Standard Time,러시아 TZ 8 표준시,(UTC+09:00) 야쿠츠크,러시아 TZ 8 일광 절약 시간,09:00:00,Korea Standard Time" />
        //<add key="Id113" value="113,Tokyo Standard Time,도쿄 표준시,(UTC+09:00) 오사카, 삿포로, 도쿄,도쿄 일광 절약 시간,09:00:00,Korea Standard Time" />
        //<add key="Id114" value="114,Transbaikal Standard Time,트란스바이칼 표준시,(UTC+09:00) 치타,트란스바이칼 일광 절약 시간,09:00:00,Korea Standard Time" />
        //<add key="Id115" value="115,North Korea Standard Time,북한 표준시,(UTC+09:00) 평양,북한 일광 절약 시간,09:00:00,Korea Standard Time" />
        //<add key="Id116" value="116,AUS Central Standard Time,오스트레일리아 중부 표준시,(UTC+09:30) 다윈,오스트레일리아 중부 일광 절약 시간,09:30:00,Korea Standard Time" />
        //<add key="Id117" value="117,Cen. Australia Standard Time,중부 오스트레일리아 표준시,(UTC+09:30) 애들레이드,중부 오스트레일리아 일광 절약 시간,09:30:00,Korea Standard Time" />
        //<add key="Id118" value="118,West Pacific Standard Time,서아시아 표준시,(UTC+10:00) 괌, 포트모르즈비,서아시아 일광 절약 시간제,10:00:00,Korea Standard Time" />
        //<add key="Id119" value="119,E. Australia Standard Time,동부 오스트레일리아 표준시,(UTC+10:00) 브리즈번,동부 오스트레일리아 일광 절약 시간,10:00:00,Korea Standard Time" />
        //<add key="Id120" value="120,Vladivostok Standard Time,러시아 TZ 9 표준시,(UTC+10:00) 블라디보스토크,러시아 TZ 9 일광 절약 시간,10:00:00,Korea Standard Time" />
        //<add key="Id121" value="121,AUS Eastern Standard Time,오스트레일리아 동부 표준시,(UTC+10:00) 캔버라, 멜버른, 시드니,오스트레일리아 동부 일광 절약 시간,10:00:00,Korea Standard Time" />
        //<add key="Id122" value="122,Tasmania Standard Time,태즈메이니아 표준시,(UTC+10:00) 호바트,태즈메이니아 일광 절약 시간,10:00:00,Korea Standard Time" />
        //<add key="Id123" value="123,Lord Howe Standard Time,로드하우 표준시,(UTC+10:30) 로드하우 섬,로드하우 일광 절약 시간,10:30:00,Korea Standard Time" />
        //<add key="Id124" value="124,Norfolk Standard Time,노퍽 표준시,(UTC+11:00) 노퍽 섬,노퍽 일광 절약 시간,11:00:00,Korea Standard Time" />
        //<add key="Id125" value="125,Magadan Standard Time,마가단 표준시,(UTC+11:00) 마가단,마가단 일광 절약 시간,11:00:00,Korea Standard Time" />
        //<add key="Id126" value="126,Bougainville Standard Time,부건빌 표준시,(UTC+11:00) 부건빌 섬,부건빌 일광 절약 시간,11:00:00,Korea Standard Time" />
        //<add key="Id127" value="127,Sakhalin Standard Time,사할린 표준시,(UTC+11:00) 사할린,사할린 일광 절약 시간,11:00:00,Korea Standard Time" />
        //<add key="Id128" value="128,Central Pacific Standard Time,중앙 태평양 표준시,(UTC+11:00) 솔로몬 제도, 뉴칼레도니아,중앙 태평양 일광 절약 시간,11:00:00,Korea Standard Time" />
        //<add key="Id129" value="129,Russia Time Zone 10,러시아 TZ 10 표준시,(UTC+11:00) 초쿠르다흐,러시아 TZ 10 일광 절약 시간,11:00:00,Korea Standard Time" />
        //<add key="Id130" value="130,Russia Time Zone 11,러시아 TZ 11 표준시,(UTC+12:00) 아나디리, 페트로파블로프스크-캄차스키,러시아 TZ 11 일광 절약 시간,12:00:00,Korea Standard Time" />
        //<add key="Id131" value="131,New Zealand Standard Time,뉴질랜드 표준시,(UTC+12:00) 오클랜드, 웰링턴,뉴질랜드 일광 절약 시간,12:00:00,Korea Standard Time" />
        //<add key="Id132" value="132,Kamchatka Standard Time,캄차카 반도 표준시,(UTC+12:00) 페트로파블로프스크-캄차스키 - 사용하지 않음,캄차카 반도 일광 절약 시간,12:00:00,Korea Standard Time" />
        //<add key="Id133" value="133,Fiji Standard Time,피지 표준시,(UTC+12:00) 피지,피지 일광 절약 시간,12:00:00,Korea Standard Time" />
        //<add key="Id134" value="134,UTC+12,UTC+12,(UTC+12:00) 협정 세계시+12,UTC+12,12:00:00,Korea Standard Time" />
        //<add key="Id135" value="135,Chatham Islands Standard Time,채텀 섬 표준시,(UTC+12:45) 채텀 섬,채텀 섬 일광 절약 시간,12:45:00,Korea Standard Time" />
        //<add key="Id136" value="136,Tonga Standard Time,통가 표준시,(UTC+13:00) 누쿠알로파,통가 일광 절약 시간,13:00:00,Korea Standard Time" />
        //<add key="Id137" value="137,Samoa Standard Time,사모아 표준시,(UTC+13:00) 사모아,사모아 일광 절약 시간,13:00:00,Korea Standard Time" />
        //<add key="Id138" value="138,UTC+13,UTC+13,(UTC+13:00) 협정 세계시+13,UTC+13,13:00:00,Korea Standard Time" />
        //<add key="Id139" value="139,Line Islands Standard Time,라인 제도 표준시,(UTC+14:00) 키리티마티 섬,라인 제도 일광 절약 시간,14:00:00,Korea Standard Time" />
        }
    }
}
