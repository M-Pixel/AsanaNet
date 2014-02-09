using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaTeam : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        [AsanaDataAttribute("organization")]
        public AsanaWorkspace Organization
        {
            get
            {
                return _organization;
            }
            internal set
            {
                _organization = value;
                if (!IsObjectLocal)
                {
                    var collection = value.Teams;
                    if (object.ReferenceEquals(collection, null))
                        return;
                    if (!collection.Contains(this))
                        collection.Add(this);
                }
            }
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
                    if (!object.ReferenceEquals(Organization, null))
                        Organization.Teams.Remove(this);
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
