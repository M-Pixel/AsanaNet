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
        [AsanaData("name")]
        public string Name  { get; private set; }

        [AsanaData("is_organization")]
        public bool? IsOrganization { get; private set; }

        [AsanaData("email_domains")]
        public string[] EmailDomains { get; private set; }

        public readonly AsanaObjectCollection<AsanaTask> FetchedTasks = new AsanaObjectCollection<AsanaTask>();
        public readonly AsanaObjectCollection<AsanaTask> MyTasks = new AsanaObjectCollection<AsanaTask>();
//        public readonly AsanaObjectCollection<AsanaTeam> FetchedTeams = new AsanaObjectCollection<AsanaTeam>();

        [AsanaData("sync_addedproject")]
        private AsanaProject _syncAddedProject 
        {
            set
            {
                value._workspace = this;
                var collection = Projects;
                if (ReferenceEquals(collection, null))
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
        [AsanaData("sync_addedtag")]
        private AsanaTag _syncAddedTag
        {
            set
            {
                value._workspace = this;
                var collection = Tags;
                if (ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(value))
                    collection.Add(value);
            }
        }

        [AsanaData("sync_addedtask")]
        private AsanaTask _syncAddedTask {
            set
            {
                value._workspace = this;
                var collection = FetchedTasks;
                if (ReferenceEquals(collection, null))
                    return;
//                if (!value.IsRemoved)
//                {
                    if (!collection.Contains(value))
                        collection.Add(value);
//                }
            }
        }

        [AsanaData("sync_removedtask")]
        private AsanaTask _syncRemovedTask
        {
            set
            {
//                Asana.RemoveFromAllCacheListsOfType<AsanaTask>(value, Host);
                value.IsRemoved = true;
//                value.TouchRemoved();
            }
        }
        
        [AsanaData("sync_removedprojectbase", SerializationFlags.Optional)]
        private AsanaProjectBase _syncRemovedProjectBase
        {
            set
            {
                value.IsRemoved = true;
//                value.TouchRemoved();
//                if (value as AsanaProject != null)
//                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
//                else if (value as AsanaTag != null)
//                    Asana.RemoveFromAllCacheListsOfType<AsanaTag>(value, Host);
            }
        }

        [AsanaData("sync_removedproject")]
        private AsanaProject _syncRemovedProject
        {
            set
            {
                value.IsRemoved = true;
//                value.TouchRemoved();
//                Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            }
        }

        [AsanaData("sync_removedtag")]
        private AsanaTag _syncRemovedTag
        {
            set
            {
                value.IsRemoved = true;
//                value.TouchRemoved();
//                Asana.RemoveFromAllCacheListsOfType<AsanaTag>(value, Host);
            }
        }

        // ------------------------------------------------------
        /*
        static public implicit operator AsanaWorkspace(Int64 id)
        {
            return Create(typeof(AsanaWorkspace), id) as AsanaWorkspace;
        }
         * */
    }
}
