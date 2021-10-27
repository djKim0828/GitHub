using EmWorks.Lib.Common;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace EmWorks.Lib.Core
{
    [Serializable]
    public class Tag : ITag
    {
        #region Fields

        private bool _b = false;

        private double _d = 0;

        private TagIdentity _idendity;

        private object _is;

        private bool _isChanged = false;

        private bool _isOccurredAlarm = false;

        private bool _isReleasedAlarm = false;

        private int _n = 0;

        private int _round = 2;

        private string _s = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// object type is <see cref="TagIdentity"/>
        /// </summary>
        public Tag(TagIdentity tagIdentity)
        {
            Initialize();
            SetTagIdentity(tagIdentity);
        }

        #endregion Constructors

        #region Events

        public event EventTagChanged OnChanged
        {
            add
            {
                _onTagChanged += value; // multi cating
                //_tagChanged = value; // single cating
            }
            remove
            {
                _onTagChanged -= value;
            }
        }

        public event EventTagChanged OnCommand
        {
            add
            {
                _onTagCommand += value; // multi cating
            }
            remove
            {
                _onTagCommand -= value;
            }
        }

        public event EventTagChanged OnOccurredAlarm
        {
            add
            {
                _onOccurredAlarm += value; // multi cating
            }
            remove
            {
                _onOccurredAlarm -= value;
            }
        }

        public event EventTagChanged OnReleasedAlarm
        {
            add
            {
                _onReleasedAlarm += value; // multi cating
            }
            remove
            {
                _onReleasedAlarm -= value;
            }
        }

        public event EventTagChanged OnUpdate
        {
            add
            {
                _onTagUpdate += value; // multi cating
            }
            remove
            {
                _onTagUpdate -= value;
            }
        }

        private event EventTagChanged _onOccurredAlarm;

        private event EventTagChanged _onReleasedAlarm;

        private event EventTagChanged _onTagChanged;

        private event EventTagChanged _onTagCommand;

        private event EventTagChanged _onTagUpdate;

        #endregion Events

        #region Properties

        public bool b
        {
            get
            {
                return ConvertToAorB(_b);
            }
        }

        public double d
        {
            get
            {
                return _d;
            }
        }

        /// <summary>
        /// true =  B접도 A접 처럼 사용.
        /// false = A접, B접 Trigger 다르게 사용.
        /// </summary>
        public bool EnableConvertToAorB { get; set; }

        /// <summary>
        /// object type is <see cref="TagIdentity"/>
        /// </summary>
        public TagIdentity Identity
        {
            get
            {
                return _idendity;
            }
            set
            {
                SetTagIdentity(value);
            }
        }

        public object Is
        {
            get
            {
                return ConvertToAorB(_is);
            }
            set
            {
                Set(value);
            }
        }

        public bool IsAlarm
        {
            get
            {
                return _isOccurredAlarm;
            }
        }

        public bool IsSimulation { get; set; }

        public int n
        {
            get
            {
                return ConvertToAorB(_n);
            }
        }

        public string Name { get; set; }

        public int No { get; set; }
        /// <summary>
        /// <see cref="IsRead"/>가 true이면 <see cref="TagIdentity.IsSubscription"/> 관계 없이 읽어 줄 것.
        /// 드라이버에서 한번 읽으면 false로 변경해 줄 것을 권장.
        /// </summary>
        public bool IsRead { get; set; }

        public int Round
        {
            get
            {
                return _round;
            }
            set
            {
                _round = value;
            }
        }

        public string s
        {
            get
            {
                return _s;
            }
        }

        #endregion Properties

        #region Methods

        public virtual object Clone()
        {
            return Clone<Tag>(this);
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void RegistEventOnChanged(object callbackFunction)
        {
            OnChanged += (EventTagChanged)callbackFunction;
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void RegistEventOnCommand(object callbackFunction)
        {
            OnCommand += (EventTagChanged)callbackFunction;
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void RegistEventOnOccurredAlarm(object callbackFunction)
        {
            OnOccurredAlarm += (EventTagChanged)callbackFunction;
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void RegistEventOnReleasedAlarm(object callbackFunction)
        {
            OnReleasedAlarm += (EventTagChanged)callbackFunction;
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void RegistEventOnUpdate(object callbackFunction)
        {
            OnUpdate += (EventTagChanged)callbackFunction;
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void ReleaseEventOnChanged(object callbackFunction)
        {
            try
            {
                OnChanged -= (EventTagChanged)callbackFunction;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Trace.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void ReleaseEventOnCommand(object callbackFunction)
        {
            try
            {
                OnCommand -= (EventTagChanged)callbackFunction;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Trace.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void ReleaseEventOnOccurredAlarm(object callbackFunction)
        {
            try
            {
                OnOccurredAlarm -= (EventTagChanged)callbackFunction;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Trace.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void ReleaseEventOnReleasedAlarm(object callbackFunction)
        {
            try
            {
                OnReleasedAlarm -= (EventTagChanged)callbackFunction;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Trace.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        public void ReleaseEventOnUpdate(object callbackFunction)
        {
            try
            {
                OnUpdate -= (EventTagChanged)callbackFunction;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Trace.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// It is recommended to run after system initialization.
        /// </summary>
        public void SetDefault()
        {
            // simulation mode에서는 input, output 모두 setting.
            // real mode에서는 output만 setting..
            if (IsSimulation == false && _idendity.IoType != (int)Idx.IoType.Output)
                return;

            bool is_done = false;
            if (_idendity.DataType == (int)Idx.DataType.Int)
            {
                Set(Convert.ToInt32(_idendity.Default));
                is_done = true;
            }
            else if (_idendity.DataType == (int)Idx.DataType.Double)
            {
                Set(Convert.ToDouble(_idendity.Default));
                is_done = true;
            }
            else if (_idendity.DataType == (int)Idx.DataType.String)
            {
                Set(Convert.ToString(_idendity.Default));
                is_done = true;
            }
            if(is_done)
                Logger.Debug(Name, "SetDefault",  $"PV : {Is}", $"SV : {_idendity.Default}");
        }

        /// <summary>
        /// object type is <see cref="TagIdentity"/>
        /// </summary>
        public void SetTagIdentity(TagIdentity tagIdentity)
        {
            if (tagIdentity == null)
                return;
            try
            {
                _idendity = tagIdentity;
                Name = _idendity.Name;
            }
            catch (Exception ex)
            {
                Trace.Assert(false, ex.Message);
            }
        }

        private void CheckDefaultAlarm()
        {
            if (_idendity.AlarmCode != 0 && _idendity.IoType == (int)Idx.IoType.Input && _idendity.DataType == (int)Idx.DataType.Int)
            {
                if (n == 1 && _isOccurredAlarm == false)
                {
                    _isOccurredAlarm = true;
                    _isReleasedAlarm = false;
                }
                else if (n == 0 && _isOccurredAlarm == true)
                {
                    _isOccurredAlarm = false;
                    _isReleasedAlarm = true;
                }
                else
                {
                    _isOccurredAlarm = _isReleasedAlarm = false;
                }
            }
            else if (_idendity.AlarmCode != 0 && _idendity.IoType == (int)Idx.IoType.Input && _idendity.DataType == (int)Idx.DataType.Double)
            {
                if (((double)_idendity.Min > d || (double)_idendity.Max < d) && _isOccurredAlarm == false)
                {
                    _isOccurredAlarm = true;
                    _isReleasedAlarm = false;
                }
                else if (((double)_idendity.Min <= d && (double)_idendity.Max >= d) && _isOccurredAlarm == true)
                {
                    _isOccurredAlarm = true;
                    _isReleasedAlarm = false;
                }
                else
                {
                    _isOccurredAlarm = _isReleasedAlarm = false;
                }
            }

            if (_isOccurredAlarm) _onOccurredAlarm?.Invoke(this, new EventTagChangedArgs(this) { EventType = (int)Idx.EventType.OccurredAlarm });
            else if (_isReleasedAlarm) _onReleasedAlarm?.Invoke(this, new EventTagChangedArgs(this) { EventType = (int)Idx.EventType.ReleasedAlarm });
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

        private int ConvertToAorB(int value)
        {
            if (EnableConvertToAorB == false)
                return value;

            int ret_value = value;
            if (_idendity.IoType == (int)Idx.IoType.Input && _idendity.DataType == (int)Idx.DataType.Int && _idendity.ContactPoint == (int)Idx.ContactPoint.B)
            {
                ret_value = value == 0 ? 1 : 0;
            }

            return ret_value;
        }

        private object ConvertToAorB(object value)
        {
            if (value is System.Int32)
            {
                return ConvertToAorB((int)value);
            }

            return value;
        }

        private bool ConvertToAorB(bool value)
        {
            return ConvertToAorB(value == false ? 0 : 1) == 0 ? false : true;
        }

        private void ConvertToEachTypes(object value)
        {
            if (value == null || _isChanged == false)
                return;

            if (value is System.Boolean)
            {
                _b = Convert.ToBoolean(value);
                _s = value.ToString();
                _d = _n = ((bool)value) ? 1 : 0;
            }
            else if (value is System.Int32)
            {
                _d = _n = Convert.ToInt32(value);
                _s = value.ToString();
                _b = Convert.ToInt32(value) > 0 ? true : false;
            }
            else if (value is System.Double)
            {
                _d = Math.Round(Convert.ToDouble(value), _round, MidpointRounding.ToEven);
                _n = Convert.ToInt32(_d);
                _s = _d.ToString();
                _b = _d == 0.0F ? false : true;
            }
            else if (value is System.String)
            {
                _s = Convert.ToString(value);
            }
        }

        private void Initialize()
        {
            IsRead = true;
            EnableConvertToAorB = true;
            _isChanged = false;
            _isOccurredAlarm = false;
            _isReleasedAlarm = false;
            _round = 2;
        }

        private void Set(object newValue)
        {
            // false reset every time this function is called.
            _isChanged = false;

            //  return, if value is null.
            if (newValue == null)
            {
                _is = null;
                return;
            }
                

            if (_is == null)
            {
                // the data type of the current value is determined when the initial setting value.
                _is = newValue;
                _isChanged = true;
            }
            else
            {
                // return, if data type of new value is different.
                if (_is.GetType().Name != newValue.GetType().Name)
                {
                    Trace.Assert(false, string.Format($"Data types are not equal. {_is.GetType().Name}:{newValue.GetType().Name}"));
                    return;
                }
                else if (_is != newValue)
                {
                    // update if current value and entered value are different.
                    _is = newValue;
                    _isChanged = true;
                }
            }
            // check IsSubscription input tag only.
            if (_idendity.IoType == (int)Idx.IoType.Input && _idendity.IsSubscription == 0)
                return;
            // callback function of the linked object, if value is changed.
            if (_isChanged)
            {
                ConvertToEachTypes(_is);

                if (IsSimulation)
                    _onTagChanged?.Invoke(this, new EventTagChangedArgs(this) { EventType = (int)Idx.EventType.Changed });
                else
                {
                    if (_idendity.IoType == (int)Idx.IoType.Input || _idendity.IoType == (int)Idx.IoType.StatusInput)
                        _onTagChanged?.Invoke(this, new EventTagChangedArgs(this) { EventType = (int)Idx.EventType.Changed }); // input tag update
                    else if (_idendity.IoType == (int)Idx.IoType.Output || _idendity.IoType == (int)Idx.IoType.StatusOutput)
                        _onTagCommand?.Invoke(this, new EventTagChangedArgs(this) { EventType = (int)Idx.EventType.Command }); // output tag update
                }

                System.Threading.Thread.Sleep(1); // djkim - 안정화 위해

                CheckDefaultAlarm();
            }
        }

        private string GetTagIdentityInfo()
        {
            if (_idendity == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] pis = _idendity.GetType().GetProperties();

            foreach (var item in pis)
                sb.Append($"{item.GetValue(_idendity)},");

            if (sb.Length == 0)
                return string.Empty;

            return sb.Remove(sb.Length - 1,1).ToString(); // remove a last comma.
        }

        //private void UpdateText(object sendor, EventTagChangedArgs e)
        //{
        //    if (Ctrl == null)
        //        return;

        //    if (Ctrl is Control)
        //    {
        //        if (((Control)Ctrl).InvokeRequired)
        //        {
        //            var d = new EventTagChanged(UpdateText);
        //            ((Control)Ctrl).Invoke(d, new object[] { sendor, e });
        //        }
        //        else
        //        {
        //            var pp = Ctrl.GetType().GetProperty("Text");
        //            pp?.SetValue(Ctrl, e.Item.Is.ToString());
        //        }
        //    }
        //}

        #endregion Methods

        #region Classes

        public class Idx
        {
            #region Enums
            public const int Unknown = -1;
            public class ContactPoint
            {
                public const int A = 0;
                public const int B = 1;
            }

            public class DataType
            {
                public const int Int = 0;
                public const int Double = 1;
                public const int String = 2;
                public const int Bool = 3;
                public const int Obejct = 4;
            }

            public class EventType
            {
                public const int Changed = 0;
                public const int Command = 1;
                public const int OccurredAlarm = 2;
                public const int ReleasedAlarm = 3;
            }

            public class IoType
            {
                public const int Input = 0;
                public const int Output = 1;
                public const int StatusInput = 2;
                public const int StatusOutput = 3;
                public const int Both = 4;
            }

            #endregion Enums
        }

        #endregion Classes
    }

    /// <summary>
    /// Author : Hans, KIM
    /// Date :  2020-12-22 13:58 (create or edit date.)
    /// EventChangedTag :  EmWorks Convention Delegate. (edit detail description.)
    /// </summary>
    public delegate void EventTagChanged(object sendor, EventTagChangedArgs e);

    /// <summary>
    /// Author : Hans, KIM
    /// Date :  2020-12-22 13:58 (create or edit date.)
    /// EventChangedTagArgs :  Event Args Class of EmWorks Convention Delegate. (edit detail description.)
    /// </summary>
    public class EventTagChangedArgs : EventArgs
    {
        #region Constructors

        public EventTagChangedArgs()
        {
            Tag.Identity.DataType = Tag.Idx.DataType.Int;
            
            Initialize();
        }

        public EventTagChangedArgs(Tag tag)
        {
            Tag = tag;
            Identity = Tag.Identity;
            Initialize();
        }

        #endregion Constructors

        #region Properties

        public string EventName
        {
            get
            {
                return Enum.GetName(typeof(Tag.Idx.EventType), EventType);
            }
        }

        public int EventType { get; set; }
        public TagIdentity Identity { get; private set; }
        public string Message { get; protected set; }
        public DateTime OccurredTime { get; protected set; }
        public Tag Tag { get; private set; }

        public string Time
        {
            get
            {
                return OccurredTime.ToString("MMdd_hhmm_ss.fff");
            }
        }

        #endregion Properties

        #region Methods

        protected virtual void Initialize()
        {
            OccurredTime = DateTime.Now;
        }

        protected virtual void RecordLog()
        {
            Logger.Debug(Tag?.Name, EventName, $"PV : {Tag?.Is}", $"Alarm Code : {Identity?.AlarmCode}");
        }

        #endregion Methods
    }
}