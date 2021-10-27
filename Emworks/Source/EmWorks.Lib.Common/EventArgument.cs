using EmWorks.Lib.Common;
using System;

namespace EmWorks.Lib.Common
{
    public class EventVelueChangeArgs : EventArgs
    {
        #region Fields

        private string _name;
        private bool? _boolenValue;
        private int? _intValue;
        private double? _doubleValue;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// 모든 값 nullable 변수로, HasValue로 null인지 확인 후 .Value로 사용할것.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="boolenValue"></param>
        /// <param name="intValue"></param>
        /// <param name="doubleValue"></param>
        public EventVelueChangeArgs(string name = null, bool? boolenValue = null, int? intValue = null, double? doubleValue = null)
        {
            this._name = name;
            this._boolenValue = boolenValue;
            this._intValue = intValue;
            this._doubleValue = doubleValue;
        }

        #endregion Constructors

        #region Properties

        public string Name
        {
            get
            {
                return _name;
            }
        }
        /// <summary>
        /// HasValue로 null인지 확인 후 .Value로 사용할것.
        /// </summary>
        public bool? BoolenValue
        {
            get
            {
                return _boolenValue;
            }
        }
        /// <summary>
        /// HasValue로 null인지 확인 후 .Value로 사용할것.
        /// </summary>
        public int? IntValue
        {
            get
            {
                return _intValue;
            }
        }
        /// <summary>
        /// HasValue로 null인지 확인 후 .Value로 사용할것.
        /// </summary>
        public double? DoubleValue
        {
            get
            {
                return _doubleValue;
            }
        }

        #endregion Properties
    }
}