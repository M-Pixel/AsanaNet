using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaProject : AsanaEventedObject, IAsanaData
    {
        [AsanaDataAttribute("name", SerializationFlags.Required)] //
        public string Name { get; set; }

        [AsanaDataAttribute("created_at", SerializationFlags.Omit)] //
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("modified_at", SerializationFlags.Omit)] //
        public AsanaDateTime ModifiedAt { get; private set; }

        [AsanaDataAttribute("notes", SerializationFlags.Optional)] //
        public string Notes { get; set; }

        [AsanaDataAttribute("archived", SerializationFlags.Omit)] //
        public bool Archived { get; private set; }

        [AsanaDataAttribute("workspace", SerializationFlags.Optional, "ID")]
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

                var collection = value.Projects;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(this))
                    collection.Add(this);
            }
        }

        private AsanaWorkspace _workspace { get; set; }

        [AsanaDataAttribute("followers", SerializationFlags.Optional)] //
        public AsanaObjectCollection<AsanaUser> Followers { get; private set; }

        [AsanaDataAttribute("team", SerializationFlags.Optional, "ID")] //
        public AsanaTeam Team { get; private set; }

        [AsanaDataAttribute("color", SerializationFlags.Omit)] //
        public string Color { get; private set; }

        [AsanaDataAttribute("public", SerializationFlags.Omit)]
        public bool Public { get; private set; }

        [AsanaDataAttribute("members", SerializationFlags.Omit)]
        public AsanaObjectCollection<AsanaUser> Members { get; private set; }

        [AsanaDataAttribute("sync_removed", SerializationFlags.Optional)]
        public override bool IsRemoved
        {
            get
            {
                return base.IsRemoved;
            }
            internal set
            {
                if (value)
                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(this, Host);
                base.IsRemoved = value;
            }
        }

        // ------------------------------------------------------

        static public implicit operator AsanaProject(Int64 ID)
        {
            return Create(typeof(AsanaProject), ID) as AsanaProject;
        }
        /*
        public AsanaProject(Int64 id = 0)
        {
            ID = id;
            // cache current state
            SetAsReferenceObject();
            //SavingCallback(Parsing.Serialize(this, false, true));
        }

        public AsanaProject(AsanaWorkspace workspace, Int64 id = 0) 
        {
            ID = id;
            Workspace = workspace;
            // cache current state
            SetAsReferenceObject();
            //SavingCallback(Parsing.Serialize(this, false, true));
        }

        public AsanaProject(AsanaWorkspace workspace, AsanaTeam team, Int64 id = 0)
        {
            ID = id;
            Workspace = workspace;
            Team = team;
            // cache current state
            SetAsReferenceObject();
        }

//        public override Task Refresh()
//        {
//            return Host.GetProjectById(ID, project =>
//            {
//                Name = (project as AsanaProject).Name;
//                CreatedAt = (project as AsanaProject).CreatedAt;
//                ModifiedAt = (project as AsanaProject).ModifiedAt;
//                Notes = (project as AsanaProject).Notes;
//                Archived = (project as AsanaProject).Archived;
//                Workspace = (project as AsanaProject).Workspace;
//                Followers = (project as AsanaProject).Followers;
//                Team = (project as AsanaProject).Team;
//                Color = (project as AsanaProject).Color;
//            });
//        }
         * */
    }
}
