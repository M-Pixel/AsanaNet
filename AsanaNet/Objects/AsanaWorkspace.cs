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

        [AsanaDataAttribute("sync_newproject", SerializationFlags.Optional)]
        internal AsanaProject _syncNewProject 
        {
            set
            {
                var collection = Projects;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!value.IsRemoved)
                {
                    if(!collection.Contains(value))
                        collection.Add(value);
                }
//                    collection.Remove(value);
//                    Asana.RemoveFromAllCacheListsOfType<AsanaProject>(value, Host);
            } 
        }

        [AsanaDataAttribute("sync_newtask", SerializationFlags.Optional)]
        internal AsanaTask _syncNewTask {
            set
            {
                var collection = FetchedTasks;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!value.IsRemoved)
                {
                    if (!collection.Contains(value))
                        collection.Add(value);
                }
            } 
        }

        // ------------------------------------------------------

        static public implicit operator AsanaWorkspace(Int64 id)
        {
            return Create(typeof(AsanaWorkspace), id) as AsanaWorkspace;
        }
    }
}
