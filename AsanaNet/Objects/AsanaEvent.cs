using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    public class AsanaDummyObject : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("sync_addedstory", SerializationFlags.Optional)]
        private AsanaStory _syncAddedStory { set { } }

        [AsanaDataAttribute("sync_addedtask", SerializationFlags.Optional)]
        private AsanaTask _syncAddedTask { set { } }

        [AsanaDataAttribute("sync_addedproject", SerializationFlags.Optional)]
        private AsanaProject _syncAddedProject { set { } }

        [AsanaDataAttribute("sync_addedtag", SerializationFlags.Optional)]
        private AsanaTag _syncAddedTag { set { } }

        [AsanaDataAttribute("sync_removedstory", SerializationFlags.Optional)]
        private AsanaStory _syncRemovedStory { set { value.IsRemoved = true; } }

        [AsanaDataAttribute("sync_removedtask", SerializationFlags.Optional)]
        private AsanaTask _syncRemovedTask { set { value.IsRemoved = true; } }

        [AsanaDataAttribute("sync_removedproject", SerializationFlags.Optional)]
        private AsanaProject _syncRemovedProject { set { value.IsRemoved = true; } }

        [AsanaDataAttribute("sync_removedtag", SerializationFlags.Optional)]
        private AsanaTag _syncRemovedTag { set { value.IsRemoved = true; } }

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

//        [AsanaDataAttribute("resource")]
//        public AsanaObject Resource { get; private set; }

        [AsanaDataAttribute("sync_changedstory", SerializationFlags.Optional)]
        private AsanaStory _syncChangedStory { set { value.TouchChanged(); } }

        [AsanaDataAttribute("sync_changedtask", SerializationFlags.Optional)]
        private AsanaTask _syncChangedTask { set { value.TouchChanged(); } }

        [AsanaDataAttribute("sync_changedprojectbase", SerializationFlags.Optional)]
        private AsanaProjectBase _syncChangedProjectBase { set { value.TouchChanged(); } }
        /*
        [AsanaDataAttribute("sync_changedproject", SerializationFlags.Optional)]
        private AsanaProject _syncChangedProject { set { value.IsPossiblyOutOfSync = true; } }

        [AsanaDataAttribute("sync_changedtag", SerializationFlags.Optional)]
        private AsanaTag _syncChangedTag { set { value.IsPossiblyOutOfSync = true; } }
        */
        [AsanaDataAttribute("parent")]
        public AsanaObject Parent { get; private set; }
    }
}
