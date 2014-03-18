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
        [AsanaData     ("type",        SerializationFlags.Omit)]
        public StoryType        Type            { get; private set; }

        [AsanaData     ("text",        SerializationFlags.Required)]
        public string           Text            { get; set; }

        [AsanaData     ("created_by",  SerializationFlags.Omit)]
        public AsanaUser        CreatedBy       { get; private set; }

        [AsanaData     ("created_at",  SerializationFlags.Omit)]
        public AsanaDateTime    CreatedAt       { get; private set; }

        [AsanaData     ("source",      SerializationFlags.Omit)]
        public StorySource      Source          { get; private set; }

//        [AsanaDataAttribute     ("target",      SerializationFlags.Omit)]
//        public AsanaTask        Target          { get; internal set; }

        [AsanaData("target", SerializationFlags.Omit)]
        public AsanaTask Target
        {
            get
            {
                return _target;
            }
            internal set
            {
                if (ReferenceEquals(value, null))
                    return;
                
                _target = value;
            }
        }

        internal override void TouchUpdated()
        {
            if (!IsObjectLocal && !ReferenceEquals(Target, null))
            {
                var collection = Target.Stories;
                if (ReferenceEquals(collection, null))
                    return;
                if (!collection.Contains(this))
                    collection.Add(this);
            }
            base.TouchUpdated();
        }

        internal AsanaTask _target { get; set; }

        // ------------------------------------------------------
        [AsanaData("sync_removed", SerializationFlags.Optional)]
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
