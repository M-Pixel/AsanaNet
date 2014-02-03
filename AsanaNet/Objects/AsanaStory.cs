using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    public enum StorySource
    {
        web,
        email,
        mobile,
        api,
        unknown 
    }
    public enum StoryType
    {
        comment,
        system
    }

    [Serializable]
    public partial class AsanaStory : AsanaEventedObject, IAsanaData
    {
        [AsanaDataAttribute     ("type",        SerializationFlags.Omit)]
        public StoryType        Type            { get; private set; }

        [AsanaDataAttribute     ("text",        SerializationFlags.Required)]
        public string           Text            { get; set; }

        [AsanaDataAttribute     ("created_by",  SerializationFlags.Omit)]
        public AsanaUser        CreatedBy       { get; private set; }

        [AsanaDataAttribute     ("created_at",  SerializationFlags.Omit)]
        public AsanaDateTime    CreatedAt       { get; private set; }

        [AsanaDataAttribute     ("source",      SerializationFlags.Omit)]
        public StorySource      Source          { get; private set; }

//        [AsanaDataAttribute     ("target",      SerializationFlags.Omit)]
//        public AsanaTask        Target          { get; internal set; }

        [AsanaDataAttribute("target", SerializationFlags.Omit)]
        public AsanaTask Target
        {
            get
            {
                return _target;
            }
            internal set
            {
                _target = value;
                var collection = value.Stories;
                if (object.ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(this))
                    collection.Add(this);
            }
        }
        private AsanaTask _target { get; set; }

        // ------------------------------------------------------

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
                    Asana.RemoveFromAllCacheListsOfType<AsanaStory>(this, Host);
                base.IsRemoved = value;
            }
        }

        ///
        /*
        internal AsanaStory()
        {
        }

        //
        public AsanaStory(AsanaTask task) : this("", task)
        {
        }

        //
        public AsanaStory(string text, AsanaTask task)
        {
            Text    = text;
            Target  = task;
        }
         * */
    }
}
