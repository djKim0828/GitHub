using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace EmWorks.Lib.Common
{
    /// <summary>
    /// 다국어 모듈로 기본적으로 실행폴더 아래의 language 폴더의 언어별 파일을 읽어서 출력한다.
    /// 번역이 되지 않은 문구는 별도 파일 쓰여 저장되고 입력된 값이 출력되어 에러를 방지한다.
    /// multilingual language module.
    /// </summary>
    public class i18n
    {
        #region Fields

        private static i18n _instance;
        private static string _languageFilePath;
        private static string _newTextFileName;
        private ArrayList _defLang;
        private LanguageType _languageTypeValue = LanguageType.EN;
        private ArrayList _multiLang;
        private List<string> _noMatchList = new List<string>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private i18n(string languageFilePath = "")
        {

            _languageFilePath = languageFilePath;

            Initialize();


        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Destructor
        /// </summary>
        ~i18n()
        {
        }

        #endregion Destructors

        #region Enums

        public enum LanguageType
        {
            EN = 0,
            KO,
            CN,
            VN,
            IN
        }

        #endregion Enums

        #region Methods

        /// <summary>
        /// Enter the default language(English) to get the multilingual language.
        /// Returns the input value if it is not in the file.
        /// </summary>
        /// <param name="defaultLanguage">default language(English)</param>
        /// <returns>output language</returns>
        public static string GetLanguage(string defaultLanguage)
        {
            if (_instance == null)
            {
                _instance = new i18n();
            }

            return _instance.GetMultiLanguage(defaultLanguage);
        }

        /// <summary>
        /// intial instance or change the language file path
        /// </summary>
        /// <param name="languageFilePath">changed value</param>
        public static void InitInstance()
        {
            if (_languageFilePath == string.Empty)
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
                _languageFilePath = baseDir.TrimEnd(new char[] { '\\' }) + "\\Language";
            } // else
            
            _newTextFileName = _languageFilePath + "\\newLanguage.txt";

            try
            {
                System.IO.File.Delete(_newTextFileName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// intial instance or change the language file path
        /// </summary>
        /// <param name="languageFilePath">changed value</param>
        public static void InitInstance(string languageFilePath)
        {
            if (_instance == null)
            {
                _instance = new i18n(languageFilePath);
                return;
            } // else

            if (languageFilePath == string.Empty)
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
                _languageFilePath = baseDir.TrimEnd(new char[] { '\\' }) + "\\Language";
            }
            else
            {
                _languageFilePath = languageFilePath;
            }

            _newTextFileName = _languageFilePath + "\\newLanguage.txt";

            try
            {
                System.IO.File.Delete(_newTextFileName);
            }
            catch
            {
            }

            _instance.LoadFile();
        }

        /// <summary>
        /// Get Language Type
        /// </summary>
        /// <returns></returns>
        public static LanguageType GetLanguageType()
        {
            if (_instance == null)
            {
                _instance = new i18n();
            }

            return _instance._languageTypeValue;

        }

        /// <summary>
        /// Setting the language type
        /// </summary>
        /// <param name="languageType">set value</param>
        public static void SetLanguageType(LanguageType languageType)
        {
            if (_instance == null)
            {
                _instance = new i18n();
            }

            _instance._languageTypeValue = languageType;
            _instance.LoadFile();
        }

        private string GetMultiLanguage(string defaultLanguage)
        {
            if (defaultLanguage.Equals(string.Empty)) return string.Empty;

            int index = 0;
            string ret = string.Empty, defLang = string.Empty;

            try
            {
                defLang = defaultLanguage.Replace("\n", "\\n"); // 문자열 내부에 "\n" 은 잘못된 문자로 인식함. "\\n" 으로 변환해야함.

                if (_defLang == null)
                {
                    writeText(defLang);
                    return defaultLanguage;
                }

                index = _defLang.IndexOf(defLang);

                if (index >= 0 && !((string)_multiLang[index]).Equals(string.Empty))
                {
                    ret = ((string)_multiLang[index]);
                }
                else
                {
                    // 없는 문자이면,
                    writeText(defLang);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                ret = string.Empty;
            }
            finally
            {
                if (ret.Equals(string.Empty))
                    ret = defaultLanguage;
            }

            return ret;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            InitInstance();

            LoadFile();
        }

        private bool LoadFile()
        {
            try
            {
                string filename = "Language";

                switch (_languageTypeValue)
                {
                    case LanguageType.KO:
                        filename = "Language_KR.txt";
                        break;

                    case LanguageType.CN:
                        filename = "Language_CN.txt";
                        break;

                    case LanguageType.VN:
                        filename = "Language_VI.txt";
                        break;

                    default:
                        filename = "Language.txt";
                        break;
                }

                string languageFileName = _languageFilePath + "\\" + filename;
                string[] lines = System.IO.File.ReadAllLines(languageFileName);

                _defLang = new ArrayList();
                _multiLang = new ArrayList();
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(',');

                    if (temp.Length == 2)
                    {
                        _defLang.Add(temp[0]);
                        _multiLang.Add(temp[1]);
                    }
                    else
                    {
                        // , 가 2개 이상이라는 것으로 가운데 , 을 찾아서 넣어주어야 한다.
                        string def = string.Empty;
                        string mul = string.Empty;

                        for (int j = 0; j < temp.Length; j++)
                        {
                            if (j < Convert.ToInt32(temp.Length / 2))
                            {
                                def = def + temp[j] + ",";
                            }
                            else
                            {
                                mul = mul + temp[j] + ",";
                            }
                        }

                        _defLang.Add(def.Substring(0, def.Length - 1));
                        _multiLang.Add(mul.Substring(0, def.Length - 1));
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void writeText(string text)
        {
            try
            {
                if (_noMatchList.Exists(item => item == text))
                {
                    return;
                } //else

                _noMatchList.Add(text);

                FileStream fs = new FileStream(_newTextFileName, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

                sw.WriteLine(text);

                sw.Flush();

                sw.Close();

                //Close the file
                //sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Logger.Exception(e);
            }
            finally
            {
                // N/A
            }
        }

        #endregion Methods
    }
}