using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace EmWorks.Lib.Common
{
    public class ClassToINI
    {
        #region Fields

        /// <summary>
        /// INI 저장가능 Type List
        /// </summary>
        public List<Type> INIReadAndWriteOkTypeList = new List<Type>() { typeof(int), typeof(float), typeof(double), typeof(string), typeof(bool) };

        /// <summary>
        /// INI File Info
        /// </summary>
        private FileInfo _file = default(FileInfo);

        private string _sectionName = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="fileInfo"></param>
        public ClassToINI(FileInfo fileInfo)
        {
            _file = fileInfo;
        }

        #endregion Constructors

        #region Properties

        public string FilePath
        {
            get
            {
                return _file.FullName;
            }
        }

        /// <summary>
        /// Section Name
        /// </summary>
        [UseINI(false)]
        public string IsSectionName
        {
            get
            {
                if (string.IsNullOrEmpty(_sectionName) == true)
                {
                    string className = this.GetType().Name;
                    return className;
                }//else

                return _sectionName;
            }
            set
            {
                _sectionName = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 동적 Type 변환 함수
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static dynamic Convert(dynamic source, Type dest)
        {
            dynamic returnValue;

            try
            {
                if (dest == typeof(bool))
                {
                    int value = System.Convert.ToInt32(source);
                    returnValue = System.Convert.ToBoolean(value);

                    return returnValue;
                }//else

                returnValue = System.Convert.ChangeType(source, dest);
            }
            catch(Exception ex)
            {
                Logger.Exception(ex);

                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// 파일을 읽어서 Class 안에 있는 Property에 값을 채워 넣는다
        /// </summary>
        /// <returns></returns>
        public virtual bool LoadINI()
        {
            var properties = TypeDescriptor.GetProperties(this.GetType());

            foreach (PropertyDescriptor item in properties)
            {
                bool isUseINI = true;
                string sectionName = IsSectionName;

                foreach (Attribute attribute in item.Attributes)
                {
                    var attributeSectionName = attribute as SectionName;
                    var attributeUseIni = attribute as UseINI;

                    if (attributeSectionName != null)
                    {
                        sectionName = attributeSectionName.IsSectionName;
                    }//else

                    if (attributeUseIni != null)
                    {
                        isUseINI = attributeUseIni.AttributeUse;
                    }//else
                }

                if (isUseINI == false)
                {
                    continue;
                }//else

                foreach (var type in INIReadAndWriteOkTypeList)
                {
                    if (type == item.PropertyType)
                    {
                        var readValue = IniReadValue(sectionName, item.Name);
                        var value = Convert(readValue, item.PropertyType);

                        item.SetValue(this, value);

                        break;
                    }//else
                }
            }
            return true;
        }

        /// <summary>
        /// Class 안에 있는 Property를 이용하여 값 저장
        /// </summary>
        /// <returns></returns>
        public bool SaveINI()
        {
            var properties = TypeDescriptor.GetProperties(this.GetType());

            foreach (PropertyDescriptor item in properties)
            {
                bool isUseINI = true;
                string sectionName = IsSectionName;

                foreach (Attribute attribute in item.Attributes)
                {
                    var attributeSectionName = attribute as SectionName;
                    var attributeUseIni = attribute as UseINI;

                    if (attributeSectionName != null)
                    {
                        sectionName = attributeSectionName.IsSectionName;
                    }//else

                    if (attributeUseIni != null)
                    {
                        isUseINI = attributeUseIni.AttributeUse;
                    }//else
                }

                if (isUseINI == false)
                {
                    continue;
                }//else

                foreach (var type in INIReadAndWriteOkTypeList)
                {
                    if (type == item.PropertyType)
                    {
                        string inputValue = string.Empty;

                        if (type == typeof(bool))
                        {
                            inputValue = System.Convert.ToInt32(item.GetValue(this)).ToString();
                        }
                        else
                        {
                            inputValue = item.GetValue(this).ToString();
                        }

                        IniWriteValue(sectionName, item.Name, inputValue);

                        break;
                    }//else
                }
            }
            return true;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// IniReadValue
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        private string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, _file.FullName);
            return temp.ToString();
        }

        /// <summary>
        /// IniWriteValue
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        private void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, _file.FullName);
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// INI Section Name Attribute
        /// </summary>
        public class SectionName : System.Attribute
        {
            #region Constructors

            public SectionName(string str)
            {
                this.IsSectionName = str;
            }

            #endregion Constructors

            #region Properties

            public string IsSectionName { get; set; }

            #endregion Properties
        }

        /// <summary>
        /// INI 사용 여부 Attribute
        /// </summary>
        public class UseINI : System.Attribute
        {
            #region Constructors

            public UseINI(bool use)
            {
                this.AttributeUse = use;
            }

            #endregion Constructors

            #region Properties

            public bool AttributeUse { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}