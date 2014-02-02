using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaWorkspace : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        [AsanaDataAttribute("is_organization")]
        public bool? IsOrganization { get; private set; }

        [AsanaDataAttribute("email_domains")]
        public string[] EmailDomains { get; private set; }

        /*
        public Task<AsanaObjectCollection<AsanaProject>> GetProjects(string optFields = null)
        {
            return Host.GetProjectsInWorkspace(this, optFields);
        }
        public Task<AsanaObjectCollection<AsanaTag>> GetTags(string optFields = null)
        {
            return Host.GetTagsInWorkspace(this, optFields);
        }
        public Task<AsanaObjectCollection<AsanaUser>> GetUsers(string optFields = null)
        {
            return Host.GetUsersInWorkspace(this, optFields);
        }
        public Task<AsanaObjectCollection<AsanaTask>> GetTasks(AsanaUser user, string optFields = null)
        {
            return Host.GetTasksInWorkspace(this, user, optFields);
        }
        */
        // ------------------------------------------------------

        static public implicit operator AsanaWorkspace(Int64 id)
        {
            return Create(typeof(AsanaWorkspace), id) as AsanaWorkspace;
        }
        /*
        public AsanaProject NewProject(AsanaTeam team = null)
        {
            return new AsanaProject(this, team) {Host = Host};
        }

        public async override Task Refresh()
        {
            var refresh = await Host.GetWorkspaceById(ID);

            Name = refresh.Name;
            IsOrganization = refresh.IsOrganization;
            EmailDomains = refresh.EmailDomains;
//            return Host.GetWorkspaceById(ID, workspace =>
//            {
//                Name = (workspace as AsanaWorkspace).Name;
//                IsOrganization = (workspace as AsanaWorkspace).IsOrganization;
//                EmailDomains = (workspace as AsanaWorkspace).EmailDomains;
//            });
        }
        */
    }
}
