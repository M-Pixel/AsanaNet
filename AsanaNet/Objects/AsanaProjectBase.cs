using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public class AsanaProjectBase : AsanaEventedObject, IAsanaData
    {
        [AsanaData("name", SerializationFlags.Required)]
        public string Name { get; set; }

        [AsanaData("notes", SerializationFlags.Optional)]
        public string Notes { get; set; }

        [AsanaData("created_at", SerializationFlags.Omit)]
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaData("followers", SerializationFlags.Omit)]
        public AsanaObjectCollection<AsanaUser> Followers { get; private set; }

        [AsanaData("color", SerializationFlags.Omit)]
        public string Color { get; private set; }

        [AsanaData("workspace", SerializationFlags.Required, 0, "ID")]
        public virtual AsanaWorkspace Workspace
        {
            get
            {
                return _workspace;
            }
            internal set
            {
                _workspace = value;
            }
        }

        internal AsanaWorkspace _workspace { get; set; }
    }
}
