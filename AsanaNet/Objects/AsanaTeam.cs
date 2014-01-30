using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Serializable]
    public class AsanaTeam : AsanaObject, IAsanaData
    {
        [AsanaDataAttribute("name")]
        public string Name  { get; private set; }

        // ------------------------------------------------------

        public override bool IsObjectLocal { get { return true; } }

        static public implicit operator AsanaTeam(Int64 ID)
        {
            return Create(typeof(AsanaTeam), ID) as AsanaTeam;
        }
    }
}
