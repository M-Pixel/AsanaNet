using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaWorkspace : AsanaEventedObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        [AsanaDataAttribute("is_organization")]
        public bool? IsOrganization { get; private set; }

        [AsanaDataAttribute("email_domains")]
        public string[] EmailDomains { get; private set; }

        public readonly AsanaObjectCollection<AsanaTask> FetchedTasks = new AsanaObjectCollection<AsanaTask>();
//        public readonly AsanaObjectCollection<AsanaTeam> FetchedTeams = new AsanaObjectCollection<AsanaTeam>();

        [AsanaDataAttribute("sync_addedproject", SerializationFlags.Optional)]
        private AsanaProject _syncAddedProject 
        {
            set
            {
                value._workspace = this;
                var collection = Projects;
                if (object.ReferenceEquals(collection, null))
                    return;
//                if (!value.IsRemoved)
//                {
                if(!collection.Contains(value))
                    collection.Add(value);
//                }
//                    collection.Remove(value);
//                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            } 
        }
        [AsanaDataAttribute("sync_addedtag", SerializationFlags.Optional)]
        private AsanaTag _syncAddedTag
        {
            set
            {
                value._workspace = this;
                var collection = Tags;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(value))
                    collection.Add(value);
            }
        }

        [AsanaDataAttribute("sync_addedtask", SerializationFlags.Optional)]
        private AsanaTask _syncAddedTask {
            set
            {
                value._workspace = this;
                var collection = FetchedTasks;
                if (object.ReferenceEquals(collection, null))
                    return;
//                if (!value.IsRemoved)
//                {
                    if (!collection.Contains(value))
                        collection.Add(value);
//                }
            }
        }

        [AsanaDataAttribute("sync_removedtask", SerializationFlags.Optional)]
        private AsanaTask _syncRemovedTask
        {
            set
            {
//                Asana.RemoveFromAllCacheListsOfType<AsanaTask>(value, Host);
                value.IsRemoved = true;
            }
        }
        
        [AsanaDataAttribute("sync_removedprojectbase", SerializationFlags.Optional)]
        private AsanaProjectBase _syncRemovedProjectBase
        {
            set
            {
                value.IsRemoved = true;
//                if (value as AsanaProject != null)
//                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
//                else if (value as AsanaTag != null)
//                    Asana.RemoveFromAllCacheListsOfType<AsanaTag>(value, Host);
            }
        }

        [AsanaDataAttribute("sync_removedproject", SerializationFlags.Optional)]
        private AsanaProject _syncRemovedProject
        {
            set
            {
                value.IsRemoved = true;
//                Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            }
        }

        [AsanaDataAttribute("sync_removedtag", SerializationFlags.Optional)]
        private AsanaTag _syncRemovedTag
        {
            set
            {
                value.IsRemoved = true;
//                Asana.RemoveFromAllCacheListsOfType<AsanaTag>(value, Host);
            }
        }

        // ------------------------------------------------------

        static public implicit operator AsanaWorkspace(Int64 id)
        {
            return Create(typeof(AsanaWorkspace), id) as AsanaWorkspace;
        }
    }
}
