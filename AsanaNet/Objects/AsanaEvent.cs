using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    public class AsanaDummyParent : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("sync_newtask", SerializationFlags.Optional)]
        private AsanaTask _syncNewTask { set { } }

        [AsanaDataAttribute("sync_newstory", SerializationFlags.Optional)]
        private AsanaStory _syncNewStory { set { } }

        [AsanaDataAttribute("sync_newproject", SerializationFlags.Optional)]
        private AsanaProject _syncNewProject { set { } }

    }
    public class AsanaEventList : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("data", SerializationFlags.Optional)]
        public AsanaObjectCollection<AsanaEventData> Data { get; private set; }

        [AsanaDataAttribute("sync", SerializationFlags.Required)]
        public string SyncToken
        {
            get;
            private set;
        }

//        private string _syncToken;

        //public delegate void DatasetNeedsFlush(AsanaObject response); 
//        public event AsanaResponseEventHandler DatasetFlushAction;
//        internal AsanaEventedObject FlushReturnObject { get; set; }

        [AsanaDataAttribute("errors", SerializationFlags.Omit)]
        public bool PreconditionFailed
        {
            get;
            private set;
            //            get { return _preconditionFailed; }
            //            private set
            //            {
            //                if (DatasetFlushAction != null)
            //                    DatasetFlushAction.Invoke(FlushReturnObject);
            //                _preconditionFailed = value;
            //            }
        }
//        private bool _preconditionFailed { get; set; }
    }
    public class AsanaEventData : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("user")]
        public AsanaUser User { get; private set; }

        [AsanaDataAttribute("created_at")]
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("type")]
        public string Type { get; private set; }

        [AsanaDataAttribute("action")]
        public string Action { get; private set; }

        [AsanaDataAttribute("resource")]
        public AsanaObject Resource { get; private set; }

        [AsanaDataAttribute("parent")]
        public AsanaObject Parent { get; private set; }
    }
}
