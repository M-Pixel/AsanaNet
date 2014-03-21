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
        [AsanaData("modified_at", SerializationFlags.Omit)] //
        public AsanaDateTime ModifiedAt { get; private set; }

        [AsanaData("archived", SerializationFlags.Optional)] //
        public bool Archived { get; set; }

        [AsanaData("workspace", SerializationFlags.Optional, 0, "ID")]
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
                    var collection = Workspace.Projects;
                    if (ReferenceEquals(collection, null))
                        return;
                    if (!collection.Contains(this))
                        collection.Add(this);
                }
                if (!ReferenceEquals(Team, null))
                {
                    // TODO: add after Creating too
                    if (!Team.Projects.Contains(this))
                        Team.Projects.Add(this);

                    if (!ReferenceEquals(Workspace, null))
                    {
                        var collection = Workspace.Teams;
                        if (ReferenceEquals(collection, null))
                            return;
                        if (!collection.Contains(Team))
                            collection.Add(Team);
                    }
                }
                
            }

            base.TouchUpdated();
        }

        [AsanaData("team", SerializationFlags.Optional, 1, "ID")] //
        public AsanaTeam Team
        {
            get
            {
                return _team;
            }
            internal set
            {
                if (ReferenceEquals(value, null))
                    return;

                _team = value;
            }
        }
        internal AsanaTeam _team { get; set; }

        [AsanaData("public", SerializationFlags.Omit)]
        public bool Public { get; private set; }

        [AsanaData("members", SerializationFlags.Omit)]
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

        [AsanaData("sync_addedtask", SerializationFlags.Optional)]
        private AsanaTask _syncAddedTask
        {
            set
            {
//                value.Workspace = Workspace;
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
//                value.TouchRemoved();
                //value.IsRemoved = true;
            }
        }
        

        // ------------------------------------------------------
        /*
        static public implicit operator AsanaProject(Int64 ID)
        {
            return Create(typeof(AsanaProject), ID) as AsanaProject;
        }
        */
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
