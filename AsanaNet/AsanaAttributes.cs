using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaNet
{
    [Flags]
    public enum SerializationFlags
    {
        Optional,
        Required,
        Omit
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class AsanaDataAttribute : Attribute
    {
        public string Name { get; private set; }
        public SerializationFlags Flags { get; private set; }
        public string[] Fields { get; private set; }

        public int Priority { get; private set; }

        public AsanaDataAttribute(string name, SerializationFlags flags, int priority, params string[] fieldsToSerialize)
        {
            Name = name;
            Flags = flags;
            Priority = priority;
            Fields = fieldsToSerialize;
        }

        public AsanaDataAttribute(string name)
            : this(name, SerializationFlags.Optional, int.MaxValue)
        {

        }
        public AsanaDataAttribute(string name, SerializationFlags flags)
            : this(name, flags, int.MaxValue)
        {

        }
    }
}
