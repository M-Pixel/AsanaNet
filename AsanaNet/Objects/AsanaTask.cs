using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaNet
{
    public enum AssigneeStatus
    {
        inbox,      //	In the inbox.
        later,      //	Scheduled for later.
        today,      //	Scheduled for today.
        upcoming        //	Marked as upcoming.
    }

    [Serializable]
    public partial class AsanaTask : AsanaEventedObject, IAsanaData
    {
        [AsanaDataAttribute     ("name",            SerializationFlags.Required)]
        public string           Name                { get; set; }

        [AsanaDataAttribute     ("assignee",        SerializationFlags.Optional, "ID")]
        public AsanaUser        Assignee            { get; set; }

        [AsanaDataAttribute     ("assignee_status", SerializationFlags.Omit)]
        public AssigneeStatus   AssigneeStatus      { get; set; }

        [AsanaDataAttribute     ("created_at",      SerializationFlags.Omit)]
        public AsanaDateTime    CreatedAt           { get; private set; }

        [AsanaDataAttribute     ("completed",       SerializationFlags.Omit)]
        public bool             Completed           { get; set; }

        [AsanaDataAttribute     ("completed_at",    SerializationFlags.Omit)]
        public AsanaDateTime    CompletedAt         { get; private set; }

        [AsanaDataAttribute     ("due_on",          SerializationFlags.Optional)]
        public AsanaDateTime    DueOn               { get; set; }

        [AsanaDataAttribute     ("followers",       SerializationFlags.Optional)]
        public AsanaObjectCollection<AsanaUser>      Followers           { get; private set; }

        [AsanaDataAttribute     ("modified_at",     SerializationFlags.Omit)]
        public AsanaDateTime    ModifiedAt          { get; private set; }

        [AsanaDataAttribute     ("notes",           SerializationFlags.Optional)]
        public string           Notes               { get; set; }

//        [AsanaDataAttribute     ("parent",          SerializationFlags.Optional)]
//        public AsanaTask        Parent              { get; internal set; }

        [AsanaDataAttribute("parent", SerializationFlags.Optional)]
        public AsanaTask Parent
        {
            get
            {
                return _parent;
            }
            internal set
            {
                if (object.ReferenceEquals(value, null))
                    return;

                _parent = value;

                if (!IsObjectLocal)
                {
                    var collection = value.Tasks;
                    if (object.ReferenceEquals(collection, null))
                        return;
                    if (!collection.Contains(this))
                        collection.Add(this);
                }
            }
        }
        internal AsanaTask _parent { get; set; }

        /*
        [AsanaDataAttribute     ("projects",        SerializationFlags.Optional, "ID")]
        public AsanaProject[]   Projects            { get; private set; }

        [AsanaDataAttribute     ("tags",            SerializationFlags.Optional, "ID")]
        public AsanaTag[]       Tags                { get; private set; }
        [AsanaDataAttribute     ("workspace",       SerializationFlags.Required, "ID")]
        public AsanaWorkspace   Workspace           { get; internal set; }
        */

        [AsanaDataAttribute("workspace", SerializationFlags.Required, "ID")]
        public AsanaWorkspace Workspace
        {
            get
            {
                return _workspace;
            }
            internal set
            {
                if (object.ReferenceEquals(value, null))
                    return;
                _workspace = value;
                if (!IsObjectLocal)
                {
                    var collection = value.FetchedTasks;
                    if (object.ReferenceEquals(collection, null))
                        return;
                    if (!collection.Contains(this))
                        collection.Add(this);
                }
            }
        }

        internal AsanaWorkspace _workspace { get; set; }

        public override bool IsRemoved
        {
            get { return base.IsRemoved; }
            internal set
            {
                if (value)
                {
                    Asana.RemoveFromAllCacheListsOfType<AsanaTask>(this, Host);
                    if (!object.ReferenceEquals(Workspace, null))
                        Workspace.FetchedTasks.Remove(this);
                }
                base.IsRemoved = value;
            }
        }

        // ------------------------------------------------------
        /*
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
                    Asana.RemoveFromAllCacheListsOfType<AsanaTask>(this, Host);
                base.IsRemoved = value;
            }
        }
        */
        [AsanaDataAttribute("sync_addedtask", SerializationFlags.Optional)]
        internal AsanaTask _syncAddedSubTask
        {
            set
            {
                var collection = Tasks;
                if (object.ReferenceEquals(collection, null))
                    return;
//                if (!value.IsRemoved)
//                {
                    collection.Add(value);
//                }
                //                    collection.Remove(value);
                //                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            }
        }
        [AsanaDataAttribute("sync_removedtask", SerializationFlags.Optional)]
        internal AsanaTask _syncRemovedSubTask
        {
            set
            {
                value.IsRemoved = true;
                var collection = Tasks;
                if (object.ReferenceEquals(collection, null))
                    return;
                collection.Remove(value);
                //                    collection.Remove(value);
                //                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            }
        }

        [AsanaDataAttribute("sync_addedstory", SerializationFlags.Optional)]
        internal AsanaStory _syncAddedStory
        {
            set
            {
                var collection = Stories;
                if (object.ReferenceEquals(collection, null))
                    return;
//                if (!value.IsRemoved)
//                {
                    if (!collection.Contains(value))
                        collection.Add(value);
//                }
                //                    collection.Remove(value);
                //                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            }
        }
        [AsanaDataAttribute("sync_removedstory", SerializationFlags.Optional)]
        internal AsanaStory _syncRemovedStory
        {
            set
            {
                value.IsRemoved = true;
                var collection = Stories;
                if (object.ReferenceEquals(collection, null))
                    return;
                collection.Remove(value);
//                Asana.RemoveFromAllCacheListsOfType<AsanaStory>(value, Host);
                // 
            }
        }
        static public implicit operator AsanaTask(Int64 ID)
        {
            return Create(typeof(AsanaTask), ID) as AsanaTask;
        }
        /*
        public AsanaTask(AsanaWorkspace workspace, AsanaTask parentTask = null) 
        {
            Workspace = workspace;
            if (parentTask != null)
                Parent = parentTask;
        }

        public AsanaTask(AsanaWorkspace workspace, Int64 id, AsanaTask parentTask = null) 
        {
            ID = id;
            Workspace = workspace;
            if (parentTask != null)
                Parent = parentTask;

            // cache current state
            SetAsReferenceObject();
        }
         * */
        /*
        public Task AddProject(AsanaProject proj, Asana host)
        {
            Dictionary<string, object> project = new Dictionary<string, object>();
            project.Add("project", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                if (Projects == null)
                    Projects = new AsanaProject[1];
                else
                {
                    AsanaProject[] lProjects = Projects;
                    Array.Resize(ref lProjects, Projects.Length + 1);
                    Projects = lProjects;
                }

                Projects[Projects.Length - 1] = proj;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            return host.Save(this, AsanaFunction.GetFunction(Function.AddProjectToTask), project);
        }
        
        public Task AddProject(AsanaProject proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return AddProject(proj, Host);
        }

        public Task RemoveProject(AsanaProject proj, Asana host)
        {
            Dictionary<string, object> project = new Dictionary<string, object>();
            project.Add("project", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                int index = Array.IndexOf(Projects, proj);
                AsanaProject[] lProjects = new AsanaProject[Projects.Length - 1];
                if(index != 0)
                    Array.Copy(Projects, lProjects, index);
                Array.Copy(Projects, index+1, lProjects, index, lProjects.Length - index);

                Projects = lProjects;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            return host.Save(this, AsanaFunction.GetFunction(Function.RemoveProjectFromTask), project);
        }

        public Task RemoveProject(AsanaProject proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return RemoveProject(proj, Host);
        }

        public Task AddTag(AsanaTag proj, Asana host)
        {
            Dictionary<string, object> Tag = new Dictionary<string, object>();
            Tag.Add("tag", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                if (Tags == null)
                    Tags = new AsanaTag[1];
                else
                {
                    AsanaTag[] lTags = Tags;
                    Array.Resize(ref lTags, Tags.Length + 1);
                    Tags = lTags;
                }

                Tags[Tags.Length - 1] = proj;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            return host.Save(this, AsanaFunction.GetFunction(Function.AddTagToTask), Tag);
        }

        public Task AddTag(AsanaTag proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return AddTag(proj, Host);
        }

        public Task RemoveTag(AsanaTag proj, Asana host)
        {
            Dictionary<string, object> Tag = new Dictionary<string, object>();
            Tag.Add("tag", proj.ID);
            AsanaResponseEventHandler savedCallback = null;
            savedCallback = (s) =>
            {
                // add it manually
                int index = Array.IndexOf(Tags, proj);
                AsanaTag[] lTags = new AsanaTag[Tags.Length - 1];
                if (index != 0)
                    Array.Copy(Tags, lTags, index);
                Array.Copy(Tags, index + 1, lTags, index, lTags.Length - index);

                Tags = lTags;
                Saving -= savedCallback;
            };
            Saving += savedCallback;
            return host.Save(this, AsanaFunction.GetFunction(Function.RemoveTagFromTask), Tag);
        }

        public Task RemoveTag(AsanaTag proj)
        {
            if (Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return RemoveTag(proj, Host);
        }
        public Task SetParent(AsanaTask task, Asana host)
        {
            Dictionary<string, object> parent = new Dictionary<string, object>();
            parent.Add("parent", task.ID);
            return host.Save(this, AsanaFunction.GetFunction(Function.SetParentTask), parent);
        }
        */
    }
}
