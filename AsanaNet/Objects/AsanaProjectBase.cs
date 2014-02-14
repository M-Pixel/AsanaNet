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
        [AsanaDataAttribute("name", SerializationFlags.Required)]
        public string Name { get; set; }

        [AsanaDataAttribute("notes", SerializationFlags.Optional)]
        public string Notes { get; set; }

        [AsanaDataAttribute("created_at", SerializationFlags.Omit)]
        public AsanaDateTime CreatedAt { get; private set; }

        [AsanaDataAttribute("followers", SerializationFlags.Omit)]
        public AsanaObjectCollection<AsanaUser> Followers { get; private set; }

        [AsanaDataAttribute("color", SerializationFlags.Omit)]
        public string Color { get; private set; }

        [AsanaDataAttribute("workspace", SerializationFlags.Required, 0, "ID")]
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
