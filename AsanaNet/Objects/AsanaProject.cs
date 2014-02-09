using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaProject : AsanaProjectBase
    {
        [AsanaDataAttribute("modified_at", SerializationFlags.Omit)] //
        public AsanaDateTime ModifiedAt { get; private set; }

        [AsanaDataAttribute("archived", SerializationFlags.Optional)] //
        public bool Archived { get; set; }

        [AsanaDataAttribute("workspace", SerializationFlags.Optional, "ID")]
        public override AsanaWorkspace Workspace
        {
            get
            {
                return base.Workspace;
            }
            internal set
            {
                if (object.ReferenceEquals(value, null))
                    return;

                base.Workspace = value;

                if (!IsObjectLocal)
                {
                    var collection = value.Projects;
                    if (object.ReferenceEquals(collection, null))
                        return;
                    if (!collection.Contains(this))
                        collection.Add(this);
                }
            }
        }

        [AsanaDataAttribute("team", SerializationFlags.Optional, "ID")] //
        public AsanaTeam Team
        {
            get
            {
                return _team;
            }
            internal set
            {
                if (object.ReferenceEquals(value, null))
                    return;

                _team = value;

                if (!IsObjectLocal)
                {
                    // TODO: add after Creating too
                    if (!value.Projects.Contains(this))
                        value.Projects.Add(this);

                    if (!object.ReferenceEquals(Workspace, null))
                    {
                        var collection = Workspace.Teams;
                        if (object.ReferenceEquals(collection, null))
                            return;
                        if (!collection.Contains(value))
                            collection.Add(value);
                    }
                }
            }
        }
        internal AsanaTeam _team { get; set; }

        [AsanaDataAttribute("public", SerializationFlags.Omit)]
        public bool Public { get; private set; }

        [AsanaDataAttribute("members", SerializationFlags.Omit)]
        public AsanaObjectCollection<AsanaUser> Members { get; private set; }

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


        [AsanaDataAttribute("sync_addedtask", SerializationFlags.Optional)]
        private AsanaTask _syncAddedTask
        {
            set
            {
//                value.Workspace = Workspace;
                var collection = Tasks;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(value))
                    collection.Add(value);
            }
        }

        [AsanaDataAttribute("sync_removedtask", SerializationFlags.Optional)]
        private AsanaTask _syncRemovedTask
        {
            set
            {
                var collection = Tasks;
                if (object.ReferenceEquals(collection, null))
                    return;
                collection.Remove(value);
//                value.IsRemoved = true;
            }
        }
        

        // ------------------------------------------------------

        static public implicit operator AsanaProject(Int64 ID)
        {
            return Create(typeof(AsanaProject), ID) as AsanaProject;
        }

        public AsanaProject(AsanaTeam team = null)
        {
            Team = team;
        }
        public AsanaProject()
        {
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
