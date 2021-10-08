using EmWorks.Lib.Common;
using System;

namespace EmWorks.App.OpticInspection
{
    public interface IRequestStatusInfo
    {
        int GroupId { get; }
        StatusType.Group GroupEnumId { get; }
        string GroupName { get; }
        string IdMessage { get; }
        //string Message { get; }
        //string Name { get; }
        DateTime OccurredTime { get; }
        object[] Parameters { get; set; }
        int StatusId { get; }
        string StatusName { get; }
        string Time { get; }

        void SetArgs(int statusId);
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
        #endregion

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
                case Group.Application:
                    status_name = Enum.GetName(typeof(Application), id);
                    break;
                case Group.Language:
                    status_name = Enum.GetName(typeof(Language), id);
                    break;
                case Group.User:
                    status_name = Enum.GetName(typeof(User), id);
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
                case Group.Alarm:
                    status_name = Enum.GetName(typeof(Alarm), id);
                    break;
                default:
                    break;
            }
            return status_name;
        }

        #endregion

        #region Enums

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

        public enum Default : int
        {
            Default = (int)StatusType.Group.Default * StatusType.Times,
            Max
        }

        public enum Application : int
        {
            Default = (int)StatusType.Group.Application * StatusType.Times,
            Exit,
            ExeLogger,
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

        public enum Operation : int
        {
            Default = (int)StatusType.Group.Operation * StatusType.Times,
            ChangedView,
            Start,
            Stop,
            Pause,
            Resume,
            Abort,
            Max
        }

        public enum Maint : int
        {
            Default = (int)StatusType.Group.Maint * StatusType.Times,
            ChangedView,
            Manual,
            Io,
            Max
        }

        public enum Recipe : int
        {
            Default = (int)StatusType.Group.Recipe * StatusType.Times,
            ChangedView,
            Recipe,
            Max
        }

        public enum Data : int
        {
            Default = (int)StatusType.Group.Data * StatusType.Times,
            ChangedView,
            Log,
            Alarm,
            Product,
            Mcc,
            Max
        }

        public enum Alarm
        {
            Default = (int)StatusType.Group.Alarm * StatusType.Times,
            Occurred,
            Released,
            ChangedClearLevel,
            Max
        }

        public enum User
        {
            Default = (int)StatusType.Group.User * StatusType.Times,
            Master,
            Engineer,
            Operator,
            Other,
            Max
        }

        #endregion Enums
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

        #endregion

        #region Delegates

        /// <summary>
        /// Author : Hans, KIM
        /// Date :  2020-09-24 17:10
        /// EventChangedStatus :  EmWorks Convention Delegate. (edit detail description.)
        /// </summary>
        public delegate void EventChangedStatus(object sendor, EventChangedStatusArgs e);

        #endregion

        #region Events

        public static event EventChangedStatus ChangedStatus;

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        public static void Request(object requestStatus, params object[] parameters)
        {
            _requestStatus = (int)requestStatus;

            EventChangedStatusArgs e = new EventChangedStatusArgs(_requestStatus, parameters);
            ChangedStatus?.Invoke(null, e);
        }

        #endregion

        #region Classese

        /// <summary>
        /// Author : Hans, KIM
        /// Date :  2020-09-24 17:10
        /// EventChangedStatusArgs :  Event Args Class of EmWorks Convention Delegate. (edit detail description.)
        /// </summary>
        public class EventChangedStatusArgs : EventArgs, IRequestStatusInfo
        {
            public DateTime OccurredTime { get; protected set; }
            public int StatusId { get; protected set; }
            public int GroupId { get; protected set; }
            public StatusType.Group GroupEnumId { get; protected set; }
            public string StatusName { get; protected set; }
            public string GroupName { get; protected set; }
            public string Name { get; protected set; }
            public string Message { get; protected set; }
            public object[] Parameters { get; set; }
            public string IdMessage
            {
                get
                {
                    return string.Format($"{GroupName},{GroupId},{StatusName},{StatusId}");
                }
            }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

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

            protected virtual void Initialize()
            {
                StatusId = 0;
                Name = string.Empty;
                OccurredTime = DateTime.Now;
            }

            public virtual void SetArgs(int statusId)
            {
                StatusId = statusId;
                StatusName = StatusType.GetStatusName(statusId);

                GroupId = (int)StatusType.GetGroup(statusId);
                GroupEnumId = ConvertToEnum<StatusType.Group>.Parse(GroupId);
                GroupName = Enum.GetName(typeof(StatusType.Group), GroupId);
            }

            protected virtual void RecordLog()
            {
                // add record log code here.
                // 이벤트 발생을 Debug 로고로 남긴다. [2020/09/25 hans Kim]
                Logger.Debug($"{nameof(StatusMachine)} - Changed status.", IdMessage);
            }
        }

        public static class ConvertToEnum<T>
        {
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

            /// <summary>
            /// int to string.
            /// </summary>
            /// <param name="level"></param>
            /// <returns></returns>
            public static string Name(int level)
            {
                return Enum.GetName(typeof(T), level);
            }
        }

        #endregion
    }
}