using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaTag : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute     ("notes",       SerializationFlags.Optional)]
        public string           Notes           { get; set; }

        [AsanaDataAttribute     ("name",        SerializationFlags.Required)]
        public string           Name            { get; set; }

        [AsanaDataAttribute     ("created_at",  SerializationFlags.Omit)]
        public AsanaDateTime    CreatedAt       { get; private set; }

        [AsanaDataAttribute     ("followers",   SerializationFlags.Omit)]
        public AsanaObjectCollection<AsanaUser> Followers { get; private set; }

        [AsanaDataAttribute     ("workspace",   SerializationFlags.Required, "ID")]
        public AsanaWorkspace Workspace
        {
            get
            {
                return _workspace;
            }
            internal set
            {
                _workspace = value;

                Debug.Assert(!IsObjectLocal);

                var collection = value.Tags;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(this))
                    collection.Add(this);
            }
        }

        private AsanaWorkspace _workspace { get; set; }

        [AsanaDataAttribute     ("color",       SerializationFlags.Omit)]
        public string           Color           { get; private set; }

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
