using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    public class AsanaDummyObject : AsanaObject, IAsanaData
    {
        [AsanaData("sync_addedstory", SerializationFlags.Optional)]
        private AsanaStory _syncAddedStory { set { } }

        [AsanaData("sync_addedtask", SerializationFlags.Optional)]
        private AsanaTask _syncAddedTask { set { } }

        [AsanaData("sync_addedproject", SerializationFlags.Optional)]
        private AsanaProject _syncAddedProject { set { } }

        [AsanaData("sync_addedtag", SerializationFlags.Optional)]
        private AsanaTag _syncAddedTag { set { } }

        [AsanaData("sync_removedstory", SerializationFlags.Optional)]
        private AsanaStory _syncRemovedStory { set { value.IsRemoved = true; } }

        [AsanaData("sync_removedtask", SerializationFlags.Optional)]
        private AsanaTask _syncRemovedTask { set { value.IsRemoved = true; } }

        [AsanaData("sync_removedproject", SerializationFlags.Optional)]
        private AsanaProject _syncRemovedProject { set { value.IsRemoved = true; } }

        [AsanaData("sync_removedtag", SerializationFlags.Optional)]
        private AsanaTag _syncRemovedTag { set { value.IsRemoved = true; } }

    }
    public class AsanaEventList : AsanaObject, IAsanaData
    {
        [AsanaData("data", SerializationFlags.Optional)]
        public AsanaObjectCollection<AsanaEventData> Data { get; private set; }

        [AsanaData("sync", SerializationFlags.Required)]
        public string SyncToken
        {
            get;
            private set;
        }

//        private string _syncToken;

        //public delegate void DatasetNeedsFlush(AsanaObject response); 
//        public event AsanaResponseEventHandler DatasetFlushAction;
//        internal AsanaEventedObject FlushReturnObject { get; set; }

        [AsanaData("errors", SerializationFlags.Omit)]
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
        [AsanaData("user")]
        public AsanaUser User { get; private set; }

        [AsanaData("created_at")]
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaData("type")]
        public string Type { get; private set; }

        [AsanaData("action")]
        public string Action { get; private set; }

//        [AsanaDataAttribute("resource")]
//        public AsanaObject Resource { get; private set; }

        [AsanaData("sync_changedstory", SerializationFlags.Optional)]
        private AsanaStory _syncChangedStory { set { value.TouchChanged(); } }

        [AsanaData("sync_changedtask", SerializationFlags.Optional)]
        private AsanaTask _syncChangedTask { set { value.TouchChanged(); } }

        [AsanaData("sync_changedprojectbase", SerializationFlags.Optional)]
        private AsanaProjectBase _syncChangedProjectBase { set { value.TouchChanged(); } }
        /*
        [AsanaDataAttribute("sync_changedproject", SerializationFlags.Optional)]
        private AsanaProject _syncChangedProject { set { value.IsPossiblyOutOfSync = true; } }

        [AsanaDataAttribute("sync_changedtag", SerializationFlags.Optional)]
        private AsanaTag _syncChangedTag { set { value.IsPossiblyOutOfSync = true; } }
        */
        [AsanaData("parent")]
        public AsanaObject Parent { get; private set; }
    }
}
