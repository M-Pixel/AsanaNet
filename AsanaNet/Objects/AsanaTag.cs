using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaTag : AsanaProjectBase
    {
        [AsanaData     ("workspace",   SerializationFlags.Required, 0, "ID")]
        public override AsanaWorkspace Workspace
        {
            get
            {
                return base.Workspace;
            }
            internal set
            {
                if (ReferenceEquals(value, null))
                    return;

                base.Workspace = value;
            }
        }
        internal override void TouchUpdated()
        {

            if (!IsObjectLocal)
            {
                if (!ReferenceEquals(Workspace, null))
                {
                    var collection = Workspace.Tags;
                    if (ReferenceEquals(collection, null))
                        return;
                    if (!collection.Contains(this))
                        collection.Add(this);
                }
            }
            base.TouchUpdated();
        }
        public override bool IsRemoved
        {
            get
            {
                return base.IsRemoved;
            }
            internal set
            {
                if (value)
                    Asana.RemoveFromAllCacheListsOfType<AsanaTag>(this, Host);
                base.IsRemoved = value;
            }
        }

        [AsanaData("sync_addedtask", SerializationFlags.Optional)]
        private AsanaTask _syncAddedTask
        {
            set
            {
                var collection = Tasks;
                if (ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(value))
                    collection.Add(value);
            }
        }
        [AsanaData("sync_removedtask", SerializationFlags.Optional)]
        private AsanaTask _syncRemovedTask
        {
            set
            {
                var collection = Tasks;
                if (ReferenceEquals(collection, null))
                    return;
                collection.Remove(value);
                //value.IsRemoved = true;
            }
        }

//        private AsanaWorkspace _workspace { get; set; }

        // ------------------------------------------------------

        static public implicit operator AsanaTag(Int64 ID)
        {
            return Create(typeof(AsanaTag), ID) as AsanaTag;
        }
        /*
        public AsanaTag(AsanaWorkspace workspace, Int64 id = 0) 
        {
            ID = id;
            Workspace = workspace;
            // cache current state
            SavingCallback(Parsing.Serialize(this, false, true));
        }

        //
        internal AsanaTag()
        {
        }
         * */
    }
}
