using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaUser : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string           Name            { get; private set; }

        [AsanaDataAttribute("email")]
        public string           Email           { get; private set; }

        [AsanaDataAttribute("workspaces")]
        public AsanaObjectCollection<AsanaWorkspace> Workspaces { get; private set; }

        // ------------------------------------------------------
    }
}
