using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="JSONHandler"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2020-12-09
    /// Description : JSON Handler
    /// </summary>
    public static class JSONHandler
    {
        #region Methods

        public static bool CreateJsonFile(string outputPath, string fileName, List<Object> objects, System.Windows.Forms.DataGridView dgvItemList)
        {
            try
            {
                if (Directory.Exists(outputPath) == false)
                {
                    Directory.CreateDirectory(outputPath);
                }

                Dictionary<string, object> temp = new Dictionary<string, object>();

                for (int i = 0; i < objects.Count; i++)
                {
                    string name = i.ToString();
                    try
                    {
                        if (dgvItemList.Columns.Contains("Category") == true && 
                            dgvItemList.Rows[i].Cells["Category"].Value.ToString() != string.Empty)
                        {
                            // 카테고리가 있으면 중복방지를 위해서
                            name = dgvItemList.Rows[i].Cells["Category"].Value.ToString() + "_";
                            name += dgvItemList.Rows[i].Cells["Name"].Value.ToString();
                        }
                        else
                        {
                            name = dgvItemList.Rows[i].Cells["Name"].Value.ToString();
                        }
                    }
                    catch
                    {
                        Logger.Error("The name does not exist.");
                    }

                    temp.Add(name, objects[i]);
                }

                JavaScriptSerializer jsonConvertor = new JavaScriptSerializer();
                string allText = jsonConvertor.Serialize(temp);

                // 개행 넣기
                allText = allText.Replace(@"{", "{\r\n  ");
                allText = allText.Replace(@",", ",\r\n  ");

                File.WriteAllText(fileName, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public static bool OpenJsonText(string jsonString, string objectsNamespace, string selectClassName, ref List<Object> objects, out string loggerData)
        {
            loggerData = string.Empty;

            objects = new List<Object>();
            JavaScriptSerializer jsonConvertor = new JavaScriptSerializer();

            // 클래스 찾기
            try
            {
                //Assembly assembly = Assembly.GetExecutingAssembly();
                Assembly assembly = Assembly.Load(objectsNamespace);
                // 클래스 이름에 해당하는 Type을 가져옮
                Type type = assembly.GetType(objectsNamespace + "." + selectClassName);

                MethodInfo mf = type.GetMethod("Open");
                object temp = Activator.CreateInstance(type);

                object[] obj = new object[3];
                obj[0] = jsonString;
                obj[1] = objects;
                obj[2] = jsonConvertor;

                objects = (List<Object>)mf.Invoke(temp, obj);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                loggerData = ex.ToString();

                return false;
            }

            return true;
        }

        #endregion Methods
    }
}
