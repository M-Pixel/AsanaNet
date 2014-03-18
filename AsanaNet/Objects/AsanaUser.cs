using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public partial class AsanaUser : AsanaObject, IAsanaData
    {
        [AsanaData("name")]
        public string           Name            { get; private set; }

        [AsanaData("email")]
        public string           Email           { get; private set; }

        [AsanaData("workspaces")]
        public AsanaObjectCollection<AsanaWorkspace> Workspaces { get; private set; }

        // ------------------------------------------------------
    }
}
