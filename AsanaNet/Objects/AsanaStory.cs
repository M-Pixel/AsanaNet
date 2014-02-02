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
    public partial class AsanaStory : AsanaObject, IAsanaData
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

        [AsanaDataAttribute     ("target",      SerializationFlags.Omit)]
        public AsanaTask        Target          { get; internal set; }

        // ------------------------------------------------------

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
