using System;

namespace EmWorks.Lib.Core
{
    public interface ITag : ICloneable
    {
        #region Properties

        bool b { get; }
        double d { get; }
        bool EnableConvertToAorB { get; set; }

        /// <summary>
        /// object type is <see cref="TagIdentity"/>
        /// </summary>
        TagIdentity Identity { get; set; }

        object Is { get; set; }
        bool IsAlarm { get; }
        bool IsSimulation { get; set; }
        int n { get; }
        string Name { get; set; }
        int No { get; set; }
        /// <summary>
        /// <see cref="IsRead"/>가 true이면 <see cref="TagIdentity.IsSubscription"/> 관계 없이 읽어 줄 것.
        /// 드라이버에서 한번 읽으면 false로 변경해 줄 것을 권장.
        /// </summary>
        bool IsRead { get; set; }
        int Round { get; set; }
        string s { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void RegistEventOnChanged(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void RegistEventOnCommand(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void RegistEventOnOccurredAlarm(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void RegistEventOnReleasedAlarm(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void RegistEventOnUpdate(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void ReleaseEventOnChanged(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void ReleaseEventOnCommand(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void ReleaseEventOnOccurredAlarm(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void ReleaseEventOnReleasedAlarm(object callbackFunction);

        /// <summary>
        /// object type is <see cref="EventTagChanged"/>
        /// </summary>
        void ReleaseEventOnUpdate(object callbackFunction);

        void SetDefault();

        void SetTagIdentity(TagIdentity tagIdentity);

        #endregion Methods
    }

    public interface ITagIdentity : ICloneable
    {
        #region Properties

        string Name { get; set; }
        int Index { get; set; }
        int Offset { get; set; }
        int IoType { get; set; }
        string Unit { get; set; }
        string Item { get; set; }
        string Device { get; set; }
        string Action { get; set; }
        int DataType { get; set; }
        int ContactPoint { get; set; }
        int IsSubscription { get; set; }
        object Default { get; set; }
        object Min { get; set; }
        object Max { get; set; }
        int Timeout { get; set; }
        int AlarmCode { get; set; }
        string ExeCmd { get; set; }
        int ActuatorId { get; set; }
        string Options { get; set; }
        string SimCmd { get; set; }
        string Desc { get; set; }


        #endregion Properties
    }
}