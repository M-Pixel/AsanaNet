using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaTeam : AsanaObject, IAsanaData
    {
        [AsanaData("name")]
        public string Name  { get; private set; }

        [AsanaData("organization", SerializationFlags.Optional, 0)]
        public AsanaWorkspace Workspace
        {
            get
            {
                return _organization;
            }
            internal set
            {
                _organization = value;
            }
        }
        internal override void TouchUpdated()
        {
            if (!IsObjectLocal && !ReferenceEquals(Workspace, null))
            {
                var collection = Workspace.Teams;
                if (ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(this))
                    collection.Add(this);
            }
            base.TouchUpdated();
        }

        internal AsanaWorkspace _organization { get; set; }
        public override bool IsRemoved
        {
            get { return base.IsRemoved; }
            internal set
            {
                if (value)
                {
                    Asana.RemoveFromAllCacheListsOfType<AsanaTeam>(this, Host);
                    if (!ReferenceEquals(Workspace, null))
                        Workspace.Teams.Remove(this);
                }
                base.IsRemoved = value;
            }
        }

        public readonly AsanaObjectCollection<AsanaProject> Projects = new AsanaObjectCollection<AsanaProject>();
        
        // ------------------------------------------------------

//        public override bool IsObjectLocal { get { return true; } }

        static public implicit operator AsanaTeam(Int64 ID)
        {
            return Create(typeof(AsanaTeam), ID) as AsanaTeam;
        }
    }
}
