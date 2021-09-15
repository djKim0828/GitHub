using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.Lib.Core
{
    public class SimCommand
    {
        public Tag _tag { get; protected set; }
        public Dictionary<int, SimProperty> Property { get; set; }
        Dictionary<int, string> _itemList;

        public SimCommand(Tag tag)
        {
            _tag = tag;
            Initialize();
        }
        private void Initialize()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            _itemList = Emu.SplitSemiColon(_tag.Identity.SimCmd);
            if (_itemList == null)
                return;
            Property = new Dictionary<int, SimProperty>();
            for (int i = 0; i < _itemList.Count; i++)
            {
                Property.Add(i, new SimProperty(_itemList[i]));
            }
        }
    }
    public class SimProperty
    {
        public class Idx
        {
            public const int Name = 0;
            public const int Type = 1;
            public const int DefaultValue = 2;
            public const int ReActValue = 3;
            public const int DelayTime = 4;
        }

        string _simCommand;
        Dictionary<int, string> _items;

        public string Name { get; protected set; }
        public int Type { get; protected set; }
        public object DefaultValue { get; protected set; }
        public object ReactValue { get; protected set; }
        public int DelayTime { get; protected set; }

        public SimProperty(string simCommand)
        {
            _simCommand = simCommand;
            Initialize();
        }

        private void Initialize()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            _items = Emu.SplitSlash(_simCommand);
            if (_items == null)
                return;

            Name = _items[Idx.Name];
            Type = Convert.ToInt32(_items[Idx.Type]);
            DefaultValue = Convert.ToDouble(_items[Idx.Type]);
            ReactValue = Convert.ToDouble(_items[Idx.Type]);
            DelayTime = Convert.ToInt32(_items[Idx.Type]);
        }
    }
}
