using EmWorks.Lib.Common;
using System;

namespace FPMS.E2105_FS111_121
{
    public interface IRequestStatusInfo
    {
        #region Properties

        StatusType.Group GroupEnumId { get; }
        int GroupId { get; }
        string GroupName { get; }
        string IdMessage { get; }

        //string Message { get; }
        //string Name { get; }
        DateTime OccurredTime { get; }

        object[] Parameters { get; set; }
        int StatusId { get; }
        string StatusName { get; }
        string Time { get; }

        #endregion Properties

        #region Methods

        void SetArgs(int statusId);

        #endregion Methods
    }

    /// <summary>
    /// Author : Hans, KIM
    /// Date :  2020-09-25 12:10
    /// EventChangedStatus :  EmWorks Convention Delegate. (edit detail description.)
    /// </summary>
    public class StatusMachine
    {
        #region Fields

        public static int _requestStatus = 0; // request status.

        #endregion Fields

        #region Delegates

        /// <summary>
        /// Author : Hans, KIM
        /// Date :  2020-09-24 17:10
        /// EventChangedStatus :  EmWorks Convention Delegate. (edit detail description.)
        /// </summary>
        public delegate void EventChangedStatus(object sendor, EventChangedStatusArgs e);

        #endregion Delegates

        #region Events

        public static event EventChangedStatus ChangedStatus;

        #endregion Events

        #region Properties

        /// <summary>
        /// current requested groud id
        /// </summary>
        public static int GroupId
        {
            get
            {
                return (_requestStatus / StatusType.Times);
            }
        }

        /// <summary>
        /// current requested groud nane
        /// </summary>
        public static string GroupName
        {
            get
            {
                return Enum.GetName(typeof(StatusType.Group), GroupId);
            }
        }

        /// <summary>
        /// current requested status id
        /// </summary>
        public static int StatusId
        {
            get
            {
                return _requestStatus;
            }
        }

        /// <summary>
        /// current requested status name
        /// </summary>
        public static string StatusName
        {
            get
            {
                return StatusType.GetStatusName(_requestStatus);
            }
        }

        #endregion Properties

        #region Methods

        public static void Request(object requestStatus, params object[] parameters)
        {
            _requestStatus = (int)requestStatus;

            EventChangedStatusArgs e = new EventChangedStatusArgs(_requestStatus, parameters);
            ChangedStatus?.Invoke(null, e);
        }

        #endregion Methods

        #region Classese

        public static class ConvertToEnum<T>
        {
            #region Methods

            /// <summary>
            /// int to string.
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public static string Name(int level)
            {
                return Enum.GetName(typeof(T), level);
            }

            /// <summary>
            /// string to enum
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public static T Parse(string s)
            {
                return (T)Enum.Parse(typeof(T), s);
            }

            /// <summary>
            /// int to enum
            /// </summary>
            /// <param name="n"></param>
            /// <returns></returns>
            public static T Parse(int n)
            {
                return (T)Enum.Parse(typeof(T), Name(n));
            }

            #endregion Methods
        }

        /// <summary>
        /// Author : Hans, KIM
        /// Date :  2020-09-24 17:10
        /// EventChangedStatusArgs :  Event Args Class of EmWorks Convention Delegate. (edit detail description.)
        /// </summary>
        public class EventChangedStatusArgs : EventArgs, IRequestStatusInfo
        {
            #region Constructors

            public EventChangedStatusArgs()
            {
                Initialize();
            }

            public EventChangedStatusArgs(int statusId, string name, string message)
            {
                Initialize();
                SetArgs(statusId);
                Name = name;
                Message = message;
                RecordLog();
            }

            public EventChangedStatusArgs(int statusId, params object[] parameters)
            {
                Initialize();
                SetArgs(statusId);
                Parameters = parameters;
                RecordLog();
            }

            #endregion Constructors

            #region Properties

            public StatusType.Group GroupEnumId { get; protected set; }
            public int GroupId { get; protected set; }
            public string GroupName { get; protected set; }

            public string IdMessage
            {
                get
                {
                    return string.Format($"{GroupName},{GroupId},{StatusName},{StatusId}");
                }
            }

            public string Message { get; protected set; }
            public string Name { get; protected set; }
            public DateTime OccurredTime { get; protected set; }
            public object[] Parameters { get; set; }
            public int StatusId { get; protected set; }
            public string StatusName { get; protected set; }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            public virtual void SetArgs(int statusId)
            {
                StatusId = statusId;
                StatusName = StatusType.GetStatusName(statusId);

                GroupId = (int)StatusType.GetGroup(statusId);
                GroupEnumId = ConvertToEnum<StatusType.Group>.Parse(GroupId);
                GroupName = Enum.GetName(typeof(StatusType.Group), GroupId);
            }

            protected virtual void Initialize()
            {
                StatusId = 0;
                Name = string.Empty;
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
                // add record log code here.
                // 이벤트 발생을 Debug 로고로 남긴다. [2020/09/25 hans Kim]
                Logger.Debug($"{nameof(StatusMachine)} - Changed status.", IdMessage);
            }

            #endregion Methods
        }

        #endregion Classese
    }

    /// <summary>
    /// Author : Hans, KIM
    /// Date :  2020-09-25 12:10
    /// kind of status for the StatusMachine object  :  EmWorks Convention Delegate. (edit detail description.)
    /// </summary>
    public class StatusType
    {
        #region Fields

        public const int Times = 1000;

        #endregion Fields

        #region Methods

        public static Group GetGroup(int id)
        {
            // 나누어 소수점 이하 내림 값.
            return StatusMachine.ConvertToEnum<Group>.Parse(id / Times);
        }

        public static string GetStatusName(int id)
        {
            Group group = GetGroup(id);
            string status_name = string.Empty;

            switch (group)
            {
                case Group.Default:
                    status_name = Enum.GetName(typeof(Default), id);
                    break;

                case Group.Language:
                    status_name = Enum.GetName(typeof(Language), id);
                    break;

                case Group.Operation:
                    status_name = Enum.GetName(typeof(Operation), id);
                    break;

                case Group.Maint:
                    status_name = Enum.GetName(typeof(Maint), id);
                    break;

                case Group.Recipe:
                    status_name = Enum.GetName(typeof(Recipe), id);
                    break;

                case Group.Data:
                    status_name = Enum.GetName(typeof(Data), id);
                    break;

                default:
                    break;
            }
            return status_name;
        }

        #endregion Methods

        #region Enums

        public enum Alarm
        {
            Default = (int)StatusType.Group.Alarm * StatusType.Times,
            Occurred,
            Released,
            ChangedClearLevel,
            Max
        }

        public enum Data : int
        {
            Default = (int)StatusType.Group.Data * StatusType.Times,
            ChangedView,
            Logger,
            Alarm
        }

        public enum Default : int
        {
            Default = (int)StatusType.Group.Default * StatusType.Times,
            Max
        }

        public enum Group : int
        {
            Default = 0,
            Application,
            Language,
            User,
            Alarm,
            Operation,
            Maint,
            Recipe,
            Data,
            Max
        }

        public enum Language : int
        {
            Default = (int)StatusType.Group.Language * StatusType.Times,
            Korean,
            English,
            Chinese,
            Vietnamese,
            Max
        }

        public enum Maint : int
        {
            Default = (int)StatusType.Group.Maint * StatusType.Times,
            ChangedView,
            Motion,
            Detector,
            Chamber,
            Monitoring,
            Io,
            initalize,
            Exit
        }

        public enum Operation : int
        {
            Default = (int)StatusType.Group.Operation * StatusType.Times,
            ChangedView,
            OpMain
        }

        public enum Recipe : int
        {
            Default = (int)StatusType.Group.Recipe * StatusType.Times,
            ChangedView,
            RpMain
        }

        #endregion Enums
    }
}