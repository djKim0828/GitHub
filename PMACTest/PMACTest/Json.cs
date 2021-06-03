using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PMACTest
{
    public static class JSON
    {
        public static Config Load(string path)
        {
            if (File.Exists(path) == false)
            {
                // 파일이 없으면 만들자!!
                File.WriteAllText(path, "");
            }

            string jsonString = File.ReadAllText(path);

            JavaScriptSerializer jsonConvertor = new JavaScriptSerializer();

            Config openObject = new Config();
            openObject = jsonConvertor.Deserialize<Config>(jsonString);

            return openObject;
        }

        public static bool Save(string path, Config config)
        {
            try
            {
                JavaScriptSerializer jsonConvertor = new JavaScriptSerializer();

                string allText = jsonConvertor.Serialize(config);

                // 개행 넣기
                allText = allText.Replace(@"{", "{\r\n  ");
                allText = allText.Replace(@",", ",\r\n  ");

                File.WriteAllText(path, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
        }
    }
}
