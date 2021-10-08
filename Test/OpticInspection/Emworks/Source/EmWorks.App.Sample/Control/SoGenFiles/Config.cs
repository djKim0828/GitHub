using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using EmWorks.Lib.Identity;
using System;
using EmWorks.Lib.Common;

namespace EmWorks.App.Sample
{
    public class Config
    {
        #region Fields

        public static JavaScriptSerializer _jsonConvertor;
        private static System.Collections.Generic.Dictionary<string, ConfigIdentity> _data;
        private static string _filePath;
        private static Config _instance;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// 생성자, 필요시 FilePath를 변경합니다.
        /// </summary>
        /// <param name="filePath"></param>
        public Config(string filePath)
        {
            LoadConfig(filePath);
        }

        /// <summary>
        /// 생성자, Path는 실행 파일 기준
        /// </summary>
        private Config()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            _jsonConvertor = new JavaScriptSerializer();
            LoadConfig(currentPath + @"\Config");
        }

        #endregion Constructors

        #region Destructors

        ~Config()
        {
        }

        #endregion Destructors

        #region Properties
        public static string RunMode
        {
            get
            {
                return _data.FirstOrDefault(x => x.Value.Name == nameof(RunMode)).Value.Value;
            }
            set
            {
                SetValue(string.Empty, nameof(RunMode), value);
            }
        }
        public static string Language
        {
            get
            {
                return _data.FirstOrDefault(x => x.Value.Name == nameof(Language)).Value.Value;
            }
            set
            {
                SetValue(string.Empty, nameof(Language), value);
            }
        }
        public static string StartPage
        {
            get
            {
                return _data.FirstOrDefault(x => x.Value.Name == nameof(StartPage)).Value.Value;
            }
            set
            {
                SetValue(string.Empty, nameof(StartPage), value);
            }
        }
        public static string test
        {
            get
            {
                return _data.FirstOrDefault(x => x.Value.Name == nameof(test)).Value.Value;
            }
            set
            {
                SetValue(string.Empty, nameof(test), value);
            }
        }
        #endregion Properties

        #region Methods

        /// <summary>
        /// 모든 데이터를 정해진 형식으로 반환
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Generic.Dictionary<string, ConfigIdentity> GetAllData()
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            return _data;
        }

        /// <summary>
        /// 모든 Category 명칭을 List로 반환
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCategory()
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            List<string> result = new List<string>();

            var x = (from ConfigIdentity o in _data.Values
                     group o by o.Category into t
                     select t.ToList());

            foreach (List<ConfigIdentity> list in x)
            {
                result.Add(list[0].Category);
            }

            return result;
        }

        /// <summary>
        /// 입력된 이름과 동일한 Category의 Item을 List로 반환
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static List<ConfigIdentity> GetNamefromCategory(string category)
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            List<ConfigIdentity> result = new List<ConfigIdentity>();

            var x = (from ConfigIdentity o in _data.Values
                     group o by o.Category into t
                     select t.ToList());

            foreach (List<ConfigIdentity> list in x)
            {
                if (list[0].Category == category)
                {
                    result = list;
                }
            }

            return result;
        }

        /// <summary>
        /// 카테고리와 이름을 검색하여 Value를 반환
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValue(string category, string name)
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            return _data.Select(i => i.Value)
                    .Where(d => d.Category == category)
                    .FirstOrDefault(x => x.Name == name).Value;
        }

        /// <summary>
        /// 초기화 또는 File Path를 지정할 때 사용
        /// </summary>
        /// <param name="dirPath"></param>
        public static void InitInstance(string filePath = "")
        {
            if (filePath == string.Empty)
            {
                _instance = new Config();
            }
            else
            {
                _instance = new Config(filePath);
            }
        }

        /// <summary>
        /// Item 추가
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="valuse"></param>
        /// <returns></returns>
        public static bool insert(string category, string name, string valuse)
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            try
            {
                ConfigIdentity temp = new ConfigIdentity();

                temp.Id = _data.Count + 1;

                temp.Category = category;
                temp.Name = name;
                temp.Value = valuse;

                _data.Add(_data.Count.ToString(), temp);

                Save();

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// Item 제거
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Remove(string category, string name)
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            try
            {
                object a = _data.Select(i => i.Value).Where(d => d.Category == category).FirstOrDefault(x => x.Name == name);

                foreach (var item in _data.Where(kvp => kvp.Value == a).ToList())
                {
                    _data.Remove(item.Key);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// Json 파일로 저장
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            if (_instance == null)
            {
                _instance = new Config();
            }

            try
            {
                string allText = _jsonConvertor.Serialize(_data);

                // 개행 넣기
                allText = allText.Replace(@"{", "{\r\n  ");
                allText = allText.Replace(@",", ",\r\n  ");

                File.WriteAllText(_filePath, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 조건의 Item을 변경
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public static bool SetValue(string category, string name, string value, bool isSave = false)
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new Config();
                }

                _data.Select(i => i.Value)
                     .Where(d => d.Category == category)
                     .FirstOrDefault(x => x.Name == name).Value = value;

                if (isSave == true)
                {
                    Save();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Json 파일 로드
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadConfig(string filePath)
        {
            _data = new System.Collections.Generic.Dictionary<string, ConfigIdentity>();
            _jsonConvertor = new JavaScriptSerializer();

            _filePath = filePath;

            string jsonString = File.ReadAllText(_filePath);
            _data = _jsonConvertor.Deserialize<Dictionary<string, ConfigIdentity>>(jsonString);
        }

        #endregion Methods
    }
}
