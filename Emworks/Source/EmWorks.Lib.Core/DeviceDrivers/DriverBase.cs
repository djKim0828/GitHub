using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmWorks.Lib.Core
{
    public abstract class DriverBase
    {
        class RetCode
        {
            public const int Success = 0;
            public const int Failure =-1;
        }

        protected Dictionary<string, Tag> _tags;
        protected Dictionary<string, Tag> _xTags;
        protected Dictionary<string, Tag> _yTags;

        public virtual bool IsOpened { get; protected set; } = false;
        public virtual bool IsSimulation { get; protected set; } = true;
        public bool IsUpdateLoop { get; protected set; } = false;
        public virtual int LoopInterval { get; set; } = 5;

        public abstract int Open(Tag commonTag);
        public abstract int Close(Tag commandTag);
        protected abstract void OnCommand(object sendor, EventTagChangedArgs e);
        protected abstract void DoUpdate();
        protected abstract void DoSimulation(Tag tag);

        public DriverBase(Dictionary<string, Tag> tags, bool isSimulation)
        {
            _tags = tags;
            IsSimulation = isSimulation;
            Initialize();
        }

        ~DriverBase()
        {
            Close(null);
            // exit update thread loop
            IsUpdateLoop = false;
        }

        protected virtual void Initialize()
        {
            InitInstance();
            RegistEvent();

            if (IsSimulation)
                return;

            //if (Open() == RetCode.Success)
            //{
            //    // if dry and run mode
            //    StartUpdateProc();
            //}
        }

        private void InitInstance()
        {
            if (_tags != null)
            {
                if (_tags.Count > 0)
                {
                    _xTags = (from item in _tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Input
                              select item).ToDictionary(x => x.Key, y => y.Value);

                    _yTags = (from item in _tags
                              where item.Value.Identity.IoType == (int)Tag.Idx.IoType.Output
                              select item).ToDictionary(x => x.Key, y => y.Value);
                }
            }
        }

        private void RegistEvent()
        {
            if (_xTags != null)
            {
                foreach (var tag in _xTags)
                {
                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    }
                }
            }

            if (_yTags != null)
            {
                foreach (var tag in _yTags)
                {
                    if (IsSimulation)
                    {
                        tag.Value.OnChanged += new EventTagChanged(OnSimulation);
                    }

                    tag.Value.OnCommand += new EventTagChanged(OnCommand);
                }
            }
        }

        protected virtual void OnSimulation(object sendor, EventTagChangedArgs e)
        {
            if(IsSimulation)
            {
                DoSimulation(e.Tag);
            }
        }
        private void StartUpdateProc()
        {
            IsUpdateLoop = true;
            ThreadPool.QueueUserWorkItem(ThreadUpdateProc, this);
        }
        protected virtual void ThreadUpdateProc(object threadContext)
        {
            while (IsUpdateLoop)
            {
                if (IsOpened)
                {
                    DoUpdate();
                    Thread.Sleep(LoopInterval);
                }
                else
                    Thread.Sleep(5000);
            }
        }
    }
    public class IoDataBuffer
    {
        public Dictionary<int, int> xInt { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> yInt { get; set; } = new Dictionary<int, int>();

        public Dictionary<int, double> xDouble { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, double> yDouble { get; set; } = new Dictionary<int, double>();

        public Dictionary<int, string> xString { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> yString { get; set; } = new Dictionary<int, string>();
    }
}
