using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FPMS.E2105_FS111_121
{
    public partial class RecipeItem
    {
        #region Fields

        private static RecipeItem _thisRecipeItem = new RecipeItem();

        #endregion Fields

        #region Methods

        public static dynamic Convert(dynamic source, Type dest)
        {
            dynamic returnValue;

            try
            {
                if (dest == typeof(bool))
                {
                    int temp = System.Convert.ToInt32(source);
                    returnValue = System.Convert.ToBoolean(temp);
                    return returnValue;
                }//else

                returnValue = System.Convert.ChangeType(source, dest);
            }
            catch
            {
                if (dest.IsEnum == true && Enum.IsDefined(dest, source))
                {
                    returnValue = Enum.Parse(dest, source, true);
                }
                else
                {
                    returnValue = null;
                }
            }
            return returnValue;
        }

        public static bool SetMethord(RecipeMethod method)
        {
            object returnValue;

            if (method._Parameter.Count > 0)
            {
                object[] parameter = new object[method._Parameter.Count];

                for (int i = 0; i < method._Parameter.Count; i++)
                {
                    parameter[i] = Convert(method._Parameter[i]._Value, method._Parameter[i]._ValueType);
                }

                returnValue = method._MethodInfo.Invoke(_thisRecipeItem, parameter);
            }
            else
            {
                returnValue = method._MethodInfo.Invoke(_thisRecipeItem, null);
            }

            if (returnValue.GetType() == typeof(bool))
            {
                return (bool)returnValue;
            }//else

            return true;
        }

        public List<RecipeMethod> GetItemList()
        {
            List<RecipeMethod> itemList = new List<RecipeMethod>();
            foreach (var item in this.GetType().GetMethods())
            {
                bool isItem = false;

                foreach (var itemAttribute in item.GetCustomAttributes(false))
                {
                    var attribute = itemAttribute as IsItem;
                    if (attribute?._AttributeUse == true)
                    {
                        isItem = true;
                        break;
                    }//else
                }

                if (isItem == false)
                {
                    continue;
                }//else

                var RecipeItem = new RecipeMethod();
                bool isExistEnum = false;
                //Todo : Config  파일에서 불러오게 수정.
                /*
                foreach (var itemName in Enum.GetNames(typeof(ERecipeItem)))
                {
                    if (itemName == item.Name)
                    {
                        isExistEnum = true;
                        break;
                    }
                }
                */
                if (isExistEnum == true)
                {
                    RecipeItem._Name = item.Name;
                    RecipeItem._Parameter = new List<RecipeMethodParameter>();
                    RecipeItem._MethodInfo = item;

                    foreach (var itemParameter in item.GetParameters())
                    {
                        RecipeItem._Parameter.Add(new RecipeMethodParameter(itemParameter.Name, string.Empty, itemParameter.ParameterType));
                    }

                    itemList.Add(RecipeItem);
                }//else
            }

            return itemList;
        }

        #endregion Methods

        #region Classes

        public class IsComboBox : System.Attribute
        {
            #region Constructors

            public IsComboBox(string parameterName, string comboBoxValue)
            {
                this._ParameterName = parameterName;
                this._ComboBoxList = comboBoxValue.Split(',').ToList();
            }

            public IsComboBox(string parameterName, Type comboBoxValue)
            {
                this._ParameterName = parameterName;
                this._ComboBoxList = Enum.GetNames(comboBoxValue).ToList();
            }

            //todo : 삭제 예정
            public IsComboBox(string parameterName)
            {
                this._ParameterName = parameterName;
            }

            #endregion Constructors

            #region Properties

            public List<string> _ComboBoxList { get; set; }
            public string _ParameterName { get; set; }

            #endregion Properties
        }

        public class IsItem : System.Attribute
        {
            #region Constructors

            public IsItem(bool use)
            {
                this._AttributeUse = use;
            }

            #endregion Constructors

            #region Properties

            public bool _AttributeUse { get; set; }

            #endregion Properties
        }

        public class RecipeMethod : ICloneable
        {
            #region Fields

            public MethodInfo _MethodInfo;
            public string _Name;
            public List<RecipeMethodParameter> _Parameter;

            #endregion Fields

            #region Methods

            public object Clone()
            {
                var returnValue = new RecipeMethod();
                returnValue._Name = this._Name;
                returnValue._MethodInfo = this._MethodInfo;
                returnValue._Parameter = new List<RecipeMethodParameter>();

                foreach (var item in this._Parameter)
                {
                    returnValue._Parameter.Add(new RecipeMethodParameter(item._Name, item._Value,item._ValueType));
                }

                return returnValue;
            }

            public bool SetMethord()
            {
                return RecipeItem.SetMethord(this);
            }

            #endregion Methods
        }

        public class RecipeMethodParameter
        {
            #region Fields

            public string _Name;
            public object _Value;
            public Type _ValueType;

            #endregion Fields
            public RecipeMethodParameter()
            {
                //
            }
            public RecipeMethodParameter(string name, object value, Type type)
            {
                _Name = name;
                _Value = value;
                _ValueType = type;
            }
        }

        #endregion Classes
    }
}