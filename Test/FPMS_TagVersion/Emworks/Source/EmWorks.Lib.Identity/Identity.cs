using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EmWorks.Lib.Identity
{
    public class ConfigIdentity
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public ConfigIdentity()
        {
            Name = Value = Category = string.Empty;
        }

        public List<Object> Open(string jsonString, List<Object> objects, JavaScriptSerializer jsonConvertor)
        {
            System.Collections.Generic.Dictionary<string, ConfigIdentity> openObject = new System.Collections.Generic.Dictionary<string, ConfigIdentity>();
            openObject = jsonConvertor.Deserialize<Dictionary<string, ConfigIdentity>>(jsonString);

            objects = new List<object>(openObject.Values);

            return objects;
        }
    }

    public class AlarmIdentity
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public AlarmIdentity()
        {
            Id = Name = Value = Category = string.Empty;
        }

        public List<Object> Open(string jsonString, List<Object> objects, JavaScriptSerializer jsonConvertor)
        {
            System.Collections.Generic.Dictionary<string, AlarmIdentity> openObject = new System.Collections.Generic.Dictionary<string, AlarmIdentity>();
            openObject = jsonConvertor.Deserialize<Dictionary<string, AlarmIdentity>>(jsonString);

            objects = new List<object>(openObject.Values);

            return objects;
        }
    }

}
