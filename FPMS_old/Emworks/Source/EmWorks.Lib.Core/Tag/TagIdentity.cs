using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace EmWorks.Lib.Core
{
    [Serializable]
    public class TagIdentity : ITagIdentity
    {
        #region Properties

        public string Name { get; set; }
        public int Index { get; set; }
        public int Offset { get; set; }
        public int IoType { get; set; }
        public string Unit { get; set; }
        public string Item { get; set; }
        public string Device { get; set; }
        public string Action { get; set; }
        public int DataType { get; set; }
        public int ContactPoint { get; set; }
        public int IsSubscription { get; set; }
        public object Default { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
        public int Timeout { get; set; }
        public int AlarmCode { get; set; }
        public string ExeCmd { get; set; }
        public int ActuatorId { get; set; }
        public string Options { get; set; }
        public string SimCmd { get; set; }
        public string Desc { get; set; }        
               
        #endregion Properties

        #region Methods

        public virtual object Clone()
        {
            return Clone<TagIdentity>(this);
        }

        private T Clone<T>(T sourceObject)
        {
            Type type = sourceObject.GetType();
            T clone = (T)Activator.CreateInstance(type);
            // 클래스 내부에 있는 모든 변수를 가져온다.
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // 변수가 Class 타입이면 그 재귀를 통해 다시 복제한다. 단, String은 class이지만 구조체처럼 사용되기 때문에 예외
                if (field.FieldType.IsClass && field.FieldType != typeof(String))
                {
                    // 새로운 클래스에 데이터를 넣는다.
                    field.SetValue(clone, Clone(field.GetValue(sourceObject)));
                    continue;
                }
                // 새로운 클래스에 데이터를 넣는다.
                field.SetValue(clone, field.GetValue(sourceObject));
            }
            return clone;
        }

        public List<Object> Open(string jsonString, List<Object> objects, JavaScriptSerializer jsonConvertor)
        {
            System.Collections.Generic.Dictionary<string, TagIdentity> openObject = new System.Collections.Generic.Dictionary<string, TagIdentity>();
            openObject = jsonConvertor.Deserialize<Dictionary<string, TagIdentity>>(jsonString);

            objects = new List<object>(openObject.Values);

            return objects;
        }

        #endregion Methods
    }
}