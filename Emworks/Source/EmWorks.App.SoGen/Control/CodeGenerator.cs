using System;
using System.IO;
using System.Windows.Forms;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="CodeGenerator"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2020-12-09
    /// Description : Code Generator
    /// </summary>
    public static class CodeGenerator
    {
        #region Fields

        private static string _ConfigTempFile = @"\TemplateCode\ConfigTemp.cs";
        private static string _TagTempFile = @"\TemplateCode\TagTemp.cs";

        #endregion Fields

        #region Methods

        public static bool CreateConfigGeneralCode(string objectFilePath,
                                                   string outputPath, Type type,
                                                   System.Windows.Forms.DataGridView dgvItemList,
                                                   ref System.Windows.Controls.TextBox txb)
        {
            try
            {
                // Template 파일을 복사
                string source = HomeDir() + _ConfigTempFile;
                string fileName = Path.GetFileNameWithoutExtension(objectFilePath);

                string desc = outputPath + @"\" + fileName + ".cs";

                if (File.Exists(desc) == true)
                {
                    // 이미 있으면 삭제 후 진행
                    File.Delete(desc);

                    OutputMessage("Deleted the file because '" + desc + "' file exists.", ref txb);
                } //else

                File.Copy(source, desc);

                OutputMessage("Copied '" + source + "' to '" + desc + "'", ref txb);

                // 해당 파일을 열어서 Class Name 변경
                string tempData = File.ReadAllText(desc);
                tempData = tempData.Replace("ConfigTemp", fileName);

                // Properties 동적 생성 *****************************************************************************
                // Text 입력을 위해 상하로 분리
                int data1EndIndex = tempData.IndexOf("#region Properties");
                int data2StartIndex = tempData.IndexOf("#endregion Properties");

                string data1 = tempData.Substring(0, data1EndIndex);

                OutputMessage("Started creating Properties.", ref txb);
                string Properties = CreateConfigPropertyData(type, dgvItemList);
                OutputMessage("Finished creating Properties.", ref txb);

                string data3 = tempData.Substring(data2StartIndex, tempData.Length - data2StartIndex);
                //***************************************************************************************************

                // 저장
                File.WriteAllText(desc, data1 + Properties + data3);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                OutputMessage(ex.ToString(), ref txb);
                return false;
            }
        }

        public static bool CreateTagGeneralCode(string objectFilePath,
                                          string outputPath, Type type,
                                          System.Windows.Forms.DataGridView dgvItemList,
                                          ref System.Windows.Controls.TextBox txb)
        {
            try
            {
                // Template 파일을 복사
                string source = HomeDir() + _TagTempFile;
                string fileName = Path.GetFileNameWithoutExtension(objectFilePath);

                string desc = outputPath + @"\" + fileName + ".cs";

                if (File.Exists(desc) == true)
                {
                    // 이미 있으면 삭제 후 진행
                    File.Delete(desc);

                    OutputMessage("Deleted the file because '" + desc + "' file exists.", ref txb);
                } //else

                File.Copy(source, desc);

                OutputMessage("Copied '" + source + "' to '" + desc + "'", ref txb);

                // 해당 파일을 열어서 Class Name 변경
                string tempData = File.ReadAllText(desc);
                tempData = tempData.Replace("TagTemp", fileName);

                // Properties 동적 생성 *****************************************************************************
                // Text 입력을 분리
                int data1EndIndex = tempData.IndexOf("#region Properties");
                int data2StartIndex = tempData.IndexOf("#endregion Properties");
                int data2EndIndex = tempData.IndexOf("#endregion Methods");

                string data1 = tempData.Substring(0, data1EndIndex);

                OutputMessage("Started creating  Tag Properties.", ref txb);
                string Properties = CreateTagPropertyData(type, dgvItemList);
                OutputMessage("Finished creating Tag Properties.", ref txb);

                string data2 = tempData.Substring(data2StartIndex,
                                                (tempData.Length - data2StartIndex) -
                                                (tempData.Length - data2EndIndex)) + "\r\n";

                OutputMessage("Started creating  Tag Functions.", ref txb);
                string AddFunctions = CreateTagFunctionAddData(type, dgvItemList);
                AddFunctions += "#endregion Methods\r\n" + "    }" + "\r\n" + "}" + "\r\n";
                OutputMessage("Finished creating Tag Functions.", ref txb);

                //***************************************************************************************************

                // 저장
                File.WriteAllText(desc, data1 + Properties + data2 + AddFunctions);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                OutputMessage(ex.ToString(), ref txb);
                return false;
            }
        }

        private static string CreateConfigPropertyData(Type type, System.Windows.Forms.DataGridView dgvItemList)
        {
            // DataGrid에서 Name 필드를 찾아서 Property를 만든다.
            string Properties = "#region Properties\r\n";

            for (int i = 0; i < dgvItemList.RowCount; i++)
            {
                // Category가 있으면 제외
                if (dgvItemList.Rows[i].Cells["Category"].Value == null)
                {
                    dgvItemList.Rows[i].Cells["Category"].Value = string.Empty;
                }

                if (dgvItemList.Rows[i].Cells["Category"].Value.ToString().Trim() != string.Empty)
                {
                    continue;
                }

                string memberName = dgvItemList.Rows[i].Cells["Name"].Value.ToString();

                //이름이 숫자로만 되어 있으면 제외
                int temp = 0;
                if (int.TryParse(memberName, out temp) == true)
                {
                    continue;
                }

                Properties += "         public static string " + memberName + "\r\n" +
                              "         {\r\n" +
                              "             get\r\n" +
                              "             {\r\n" +
                              "                 return _data[nameof(" + memberName + ")].Value;\r\n" +
                              "             }\r\n" +
                              "             set\r\n" +
                              "             {\r\n" +
                              "                    SetValue(string.Empty, nameof(" + memberName + "), value);\r\n" +
                "             }\r\n" +
                              "         }\r\n";
            }
            Properties += "       ";

            return Properties;
        }

        private static string CreateTagFunctionAddData(Type type, DataGridView dgvItemList)
        {
            // DataGrid에서 Name 필드를 찾아서 Property를 만든다.
            string addFunctions = string.Empty;

            for (int i = 0; i < dgvItemList.RowCount; i++)
            {
                string memberName = dgvItemList.Rows[i].Cells["Name"].Value.ToString();

                //이름이 숫자로만 되어 있으면 제외
                int temp = 0;
                if (int.TryParse(memberName, out temp) == true)
                {
                    Logger.Infomation("Name is Number only. Skip the make function. [ Row = " + i.ToString() + "]");
                    continue;
                }

                addFunctions += MakeFunction(i, dgvItemList, memberName);
            }
            addFunctions += "       ";

            return addFunctions;
        }

        private static string CreateTagPropertyData(Type type, System.Windows.Forms.DataGridView dgvItemList)
        {
            // DataGrid에서 Name 필드를 찾아서 Property를 만든다.
            string Properties = "#region Properties\r\n";

            for (int i = 0; i < dgvItemList.RowCount; i++)
            {
                string memberName = dgvItemList.Rows[i].Cells["Name"].Value.ToString();

                //이름이 숫자로만 되어 있으면 제외
                int temp = 0;
                if (int.TryParse(memberName, out temp) == true)
                {
                    Logger.Infomation("Name is Number only. Skip the make property. [ Row = " + i.ToString() + "]");
                    continue;
                }

                Properties += MakeProperty(i, dgvItemList, memberName);
            }
            Properties += "       ";

            return Properties;
        }

        private static string HomeDir()
        {
            string currentPath = Properties.Settings.Default.RootPath;
            currentPath = currentPath.TrimEnd(new char[] { '\\' });
            return currentPath;
        }

        private static string MakeFunction(int index, DataGridView dgvItemList, string memberName)
        {
            string result = string.Empty;
            try
            {
                string ActType = dgvItemList.Rows[index].Cells["ActuatorId"].Value.ToString();
                string RelatedTag = dgvItemList.Rows[index].Cells["SimCmd"].Value.ToString();

                switch (ActType)
                {
                    case ("1"):
                        result = MakeFunctionType1(memberName, RelatedTag);
                        break;
                    case ("2"):
                        result = MakeFunctionType2(memberName, RelatedTag);
                        break;
                    case ("3"):
                        result = MakeFunctionType3(memberName, RelatedTag);
                        break;
                    case ("4"):
                        result = MakeFunctionType4(memberName, RelatedTag);
                        break;
                    default:
                        return string.Empty;
                }

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return result;
            }
        }

        private static string MakeFunctionType1(string memberName, string relatedTag)
        {
            string result = string.Empty;
            try
            {
                if (relatedTag != string.Empty)
                {
                    result = "        public bool SetMemberName(bool isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            #warning Interlock check 할 것.\r\n" +
                          "\r\n" +
                          "            const int error = -1; \r\n" +
                          "\r\n" +
                          "            int Common = (isExecute == true) ? 1 : 0;" + "\r\n" +
                          "\r\n" +
                          "            MemberName = Common;" + " \r\n" +
                          "\r\n" +
                          "            bool isActionBreak = false;\r\n" +
                          "            bool isError = false;\r\n" +
                          "            Action action = () =>" + "\r\n" +
                          "            {" + "\r\n" +
                          "                while (true)" + "\r\n" +
                          "                {" + "\r\n" +
                          "                    Thread.Sleep(5);" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is == null)" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        continue;" + "\r\n" +
                          "                    }" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is.ToString() == Common.ToString())" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        break;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is.ToString() == error.ToString())" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        isError = true;" + "\r\n" +
                          "                        break;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (isActionBreak == true)\r\n" +
                          "                    {\r\n" +
                          "                        break;\r\n" +
                          "                    }\r\n" +
                          "\r\n" +
                          "                }" + "\r\n" +
                          "            };" + "\r\n" +
                          "" + "\r\n" +
                          "            if (WaitResult(timeOut, action) == false)" + "\r\n" +
                          "            {\r\n" +
                          "                isActionBreak = true;\r\n" +
                          "                return false;\r\n" +
                          "            }\r\n" +
                          "            else\r\n" +
                          "            {\r\n" +
                          "                if (isError == true)\r\n" +
                          "                {\r\n" +
                          "                    return false;\r\n" +
                          "                }\r\n" +
                          "\r\n" +
                          "                return true;\r\n" +
                          "            }\r\n" +
                          "        }" + "\r\n";
                }
                else
                {
                    result = "        public bool SetMemberName(bool isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            #warning Interlock check 할 것.\r\n" +
                          "\r\n" +
                          "            int Common = (isExecute == true) ? 1 : 0;" + "\r\n" +
                          "\r\n" +
                          "            MemberName = Common;" + "\r\n" +
                          "\r\n" +
                          "            return true;" + "\r\n" +
                          "        }" + "\r\n";
                }

                result = result.Replace("MemberName", memberName);
                result = result.Replace("RelatedTag", relatedTag.Split('/')[0]);

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return string.Empty;
            }
        }

        private static string MakeFunctionType2(string memberName, string relatedTag)
        {
            string result = string.Empty;
            try
            {
                if (relatedTag != string.Empty)
                {
                    result = "        public bool SetMemberName(bool isExecute, out string data, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            #warning Interlock check 할 것.\r\n" +
                          "\r\n" +
                          "            int Common = (isExecute == true) ? 1 : 0;" + "\r\n" +
                          "\r\n" +
                          "            MemberName = Common;" + " \r\n" +
                          "\r\n" +
                          "            data = string.Empty;" + "\r\n" +
                          "            RelatedTag.Is = string.Empty;" + "\r\n" +
                          "\r\n" +
                          "            bool isActionBreak = false;\r\n" +
                          "            Action action = () =>" + "\r\n" +
                          "            {" + "\r\n" +
                          "                while (true)" + "\r\n" +
                          "                {" + "\r\n" +
                          "                    Thread.Sleep(5);" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is == null)" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        continue;" + "\r\n" +
                          "                    }" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is.ToString() != string.Empty)" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        break;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (isActionBreak == true)\r\n" +
                          "                    {\r\n" +
                          "                        break;\r\n" +
                          "                    }\r\n" +
                          "\r\n" +
                          "                }" + "\r\n" +
                          "            };" + "\r\n" +
                          "\r\n" +
                          "            if (WaitResult(timeOut, action) == true)" + "\r\n" +
                          "            {" + "\r\n" +
                          "                data = RelatedTag.Is.ToString();" + "\r\n" +
                          "                return true;" + "\r\n" +
                          "            }" + "\r\n" +
                          "            else" + "\r\n" +
                          "            {" + "\r\n" +
                          "                isActionBreak = true;\r\n" +
                          "                return false;" + "\r\n" +
                          "            }" + "\r\n" +
                          "" + "\r\n" +
                          "        }" + "\r\n";
                }
                else
                {
                    result = "        public bool SetMemberName(bool isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            #warning Interlock check 할 것.\r\n" +
                          "\r\n" +
                          "            int Common = (isExecute == true) ? 1 : 0;" + "\r\n" +
                          "\r\n" +
                          "            MemberName = Common;" + "\r\n" +
                          "\r\n" +
                          "            return true;" + "\r\n" +
                          "        }" + "\r\n";
                }

                result = result.Replace("MemberName", memberName);
                result = result.Replace("RelatedTag", relatedTag.Split('/')[0]);

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return string.Empty;
            }
        }

        private static string MakeFunctionType3(string memberName, string relatedTag)
        {
            string result = string.Empty;
            try
            {
                if (relatedTag != string.Empty)
                {
                    result = "        public bool SetMemberName(double isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            const int error = -1; \r\n" +
                          "\r\n" +
                          "            MemberName = isExecute;" + " \r\n" +
                          "\r\n" +
                          "            bool isActionBreak = false;\r\n" +
                          "            bool isError = false;\r\n" +
                          "            Action action = () =>" + "\r\n" +
                          "            {" + "\r\n" +
                          "                while (true)" + "\r\n" +
                          "                {" + "\r\n" +
                          "                    Thread.Sleep(5);" + "\r\n" +
                          "\r\n" +
                          "                    if (isActionBreak == true)\r\n" +
                          "                    {\r\n" +
                          "                        break;\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is == null)" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        continue;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is.ToString() == isExecute.ToString())" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        break;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is.ToString() == error.ToString())" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        isError = true;" + "\r\n" +
                          "                        break;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                }" + "\r\n" +
                          "            };" + "\r\n" +
                          "" + "\r\n" +
                          "            if (WaitResult(timeOut, action) == false)" + "\r\n" +
                          "            {\r\n" +
                          "                isActionBreak = true;\r\n" +
                          "                return false;\r\n" +
                          "            }\r\n" +
                          "            else\r\n" +
                          "            {\r\n" +
                          "                if (isError == true)\r\n" +
                          "                {\r\n" +
                          "                    return false;\r\n" +
                          "                } // else" + "\r\n" +
                          "\r\n" +
                          "                return true;\r\n" +
                          "            }\r\n" +
                          "        }" + "\r\n";
                }
                else
                {
                    result = "        public bool SetMemberName(double isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            MemberName = isExecute;" + "\r\n" +
                          "\r\n" +
                          "            return true;" + "\r\n" +
                          "        }" + "\r\n";
                }

                result = result.Replace("MemberName", memberName);
                result = result.Replace("RelatedTag", relatedTag.Split('/')[0]);

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return string.Empty;
            }
        }

        private static string MakeFunctionType4(string memberName, string relatedTag)
        {
            string result = string.Empty;
            try
            {
                if (relatedTag != string.Empty)
                {
                    result = "        public bool SetMemberName(bool isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            #warning Interlock check 할 것.\r\n" +
                          "\r\n" +
                          "            int Common = (isExecute == true) ? 1 : 0;" + "\r\n" +
                          "\r\n" +
                          "            MemberName = Common;" + " \r\n" +
                          "\r\n" +
                          "            bool isActionBreak = false;\r\n" +
                          "            Action action = () =>" + "\r\n" +
                          "            {" + "\r\n" +
                          "                while (true)" + "\r\n" +
                          "                {" + "\r\n" +
                          "                    Thread.Sleep(5);" + "\r\n" +
                          "\r\n" +
                          "                    if (RelatedTag.Is != null)" + "\r\n" +
                          "                    {" + "\r\n" +
                          "                        break;" + "\r\n" +
                          "                    } // else" + "\r\n" +
                          "\r\n" +
                          "                    if (isActionBreak == true)\r\n" +
                          "                    {\r\n" +
                          "                        break;\r\n" +
                          "                    }\r\n" +
                          "\r\n" +
                          "                }" + "\r\n" +
                          "            };" + "\r\n" +
                          "" + "\r\n" +
                          "            if (WaitResult(timeOut, action) == false)" + "\r\n" +
                          "            {\r\n" +
                          "                isActionBreak = true;\r\n" +
                          "                return false;\r\n" +
                          "            }\r\n" +
                          "            else\r\n" +
                          "            {\r\n" +
                          "                return true;\r\n" +
                          "            }\r\n" +
                          "        }" + "\r\n";
                }
                else
                {
                    result = "        public bool SetMemberName(bool isExecute, int timeOut = _defaultTime)\r\n" +
                          "        {\r\n" +
                          "            #warning Interlock check 할 것.\r\n" +
                          "\r\n" +
                          "            int Common = (isExecute == true) ? 1 : 0;" + "\r\n" +
                          "\r\n" +
                          "            MemberName = Common;" + "\r\n" +
                          "\r\n" +
                          "            return true;" + "\r\n" +
                          "        }" + "\r\n";
                }

                result = result.Replace("MemberName", memberName);
                result = result.Replace("RelatedTag", relatedTag.Split('/')[0]);

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return string.Empty;
            }
        }

        private static string MakeProperty(int index, DataGridView dgvItemList, string memberName)
        {
            string result = string.Empty;
            try
            {
                string ActType = dgvItemList.Rows[index].Cells["IOType"].Value.ToString();
                switch (ActType)
                {
                    case ("0"):
                    case ("2"):
                        // 0, 2는 Input Type
                        result = MakePropertyType0(memberName);
                        break;
                    case ("1"):
                    case ("3"):
                        // 0, 2는 OutPut Type
                        string dataType = dgvItemList.Rows[index].Cells["DataType"].Value.ToString();

                        result = MakePropertyType1(memberName, dataType);

                        break;

                    default:
                        return string.Empty;
                }

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return result;
            }
        }

        private static string MakePropertyType0(string memberName)
        {
            try
            {
                string result = "         public Tag " + memberName + "\r\n" +
                                "         {\r\n" +
                                "             get\r\n" +
                                "             {\r\n" +
                                "                 return _tags[nameof(" + memberName + ")];\r\n" +
                                "             }\r\n" +
                                "         }\r\n";

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return string.Empty;
            }
        }

        private static string MakePropertyType1(string memberName, string dataType)
        {
            string result = string.Empty;
            try
            {
                string value = string.Empty;
                switch (dataType)
                {
                    case ("0"):
                        value = "                 _tags[nameof(" + memberName + ")].Is = Convert.ToInt32(value);\r\n";
                        break;
                    case ("1"):
                        value = "                 _tags[nameof(" + memberName + ")].Is = Convert.ToDouble(value);\r\n";
                        break;
                    case ("2"):
                        value = "                 _tags[nameof(" + memberName + ")].Is = value.ToString();\r\n";
                        break;
                    default:
                        value = "                 _tags[nameof(" + memberName + ")].Is = value;\r\n";
                        break;
                }

                result = "         public object " + memberName + "\r\n" +
                          "         {\r\n" +
                          "             get\r\n" +
                          "             {\r\n" +
                          "                 return _tags[nameof(" + memberName + ")];\r\n" +
                          "             }\r\n";

                result += "             set\r\n" +
                          "             {\r\n" +
                          value +
                          "             }\r\n" +
                          "         }\r\n";

                return result;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return string.Empty;
            }
        }

        private static void OutputMessage(string message, ref System.Windows.Controls.TextBox txb)
        {
            try
            {
                if (txb == null)
                {
                    return;
                }

                txb.AppendText(message + "\n");
                txb.ScrollToEnd();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return;
            }
        }

        #endregion Methods
    }
}
