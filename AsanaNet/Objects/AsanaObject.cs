using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace AsanaNet
{
    /// <summary>
    /// This interface allows objects to specify whether they are 'in tact' and provides the interface for fetching them.
    /// </summary>
    public interface IAsanaData
    {
        bool IsObjectLocal { get; }
    }

    [Serializable]
    public partial class AsanaEventedObject : AsanaObject, IAsanaEventedObject
    {
        private AsanaEventList EventList
        {
            get
            {
                return _eventList;
            }
            set
            {
                _eventList = value;
                if (_eventList.PreconditionFailed)
                    DatasetCacheFlushAction(this);
//                _eventList.FlushReturnObject = this;
//                _eventList.DatasetFlushAction += DatasetFlushAction;
            }
        }
        protected virtual void DatasetCacheFlushAction(AsanaObject response)
        {
            if (!ReferenceEquals(Host, null))
            {
                var allCachedObjects = Host.ObjectCache.GetAllNotStartingWith("/");
                foreach (var obj in allCachedObjects.OfType<AsanaObject>())
                {
                    (obj).IsPossiblyOutOfSync = true;
                }
            }
        }

        private AsanaEventList _eventList { get; set; }

        // TODO: opt fields are now there as asana doesn't support tags/projects differentiation
        public Task Sync(string optFields = "created_at,type,resource,resource.name,resource.tags,resource.tags.name,resource.tags.workspace,resource.followers,resource.target,resource.text,parent,parent.name")
        {
            var task = Host.GetEvents(this, ReferenceEquals(EventList, null) ? "0" : EventList.SyncToken, optFields,
                AsanaCacheLevel.Ignore);
            task.ContinueWith(
                    async (eventList) =>
                    {
                        EventList = await eventList;
                    });
            return task;
        }
    }

    interface IAsanaEventedObject
    {
        Task Sync(string optFields = null);
    }
//
//    public interface IAsanaSyncable
//    {
//        event AsanaResponseEventHandler SelfRemoved;
//        event AsanaResponseEventHandler SelfChanged;
//        event AsanaResponseEventHandler ChildAdded;
//        event AsanaResponseEventHandler ChildRemoved;
//    }

    enum AsanaExistance
    {
        Local = 0,
        Deleted = -1
    }

    // TODO: http://stackoverflow.com/questions/2365152/whats-the-difference-between-using-the-serializable-attribute-implementing-is
    // http://stackoverflow.com/questions/2121424/serializing-data-transfer-objects-in-net/2121517#2121517
    // http://www.codeproject.com/Articles/1789/Object-Serialization-using-C
    [Serializable]
    public abstract class AsanaObject
    {
        [AsanaData("id", SerializationFlags.Omit)]
        public Int64 ID { get; internal set; }

        public Asana Host { get; internal set; }

        //add event handler here or in separate interface extended

        /// <summary>
        /// Object has been remotely removed.
        /// </summary>
        public event AsanaResponseEventHandler Removed;

        /// <summary>
        /// The object was saved successfully and changes should be reflected in the state.
        /// </summary>
        public event AsanaResponseEventHandler Updated;

        /// <summary>
        /// The object was changed remotely, and the changes might or might not be reflected in the state.
        /// </summary>
        public event AsanaResponseEventHandler Changed;

        public bool IsPossiblyOutOfSync
        {
            get { return _syncState; }
            internal set
            {
                _syncState = value;
//                TouchChanged();
            }
        }
        private bool _syncState { get; set; }

        internal void TouchChanged()
        {
//            _lastSave = Parsing.Serialize(this, false, false);
            _syncState = true;
            if (Changed != null)
                Changed(this);
        }
        internal virtual void TouchUpdated()
        {
            _lastSave = Parsing.Serialize(this, false, false, true);

            if (Updated != null)
                Updated(this);
        }

        internal void TouchRemoved()
        {
            if (Removed != null)
                Removed(this);
        }

        //        [AsanaDataAttribute("sync_removed", SerializationFlags.Optional)]
        public virtual bool IsRemoved
        {
            get
            {
                return ID == (Int64)AsanaExistance.Deleted;
            }
            internal set
            {
                if (value)
                {
                    IDBeforeRemoved = ID;
                    ID = (Int64)AsanaExistance.Deleted;
                    TouchRemoved();
                }
            }
        }

        public Int64 IDBeforeRemoved { get; set; }

        // memento
        private Dictionary<string, object> _lastSave;

        internal bool IsDirty(string key, object value)
        {
            object lvalue = null;
            if(_lastSave.TryGetValue(key, out lvalue))
            {
                return !value.Equals(lvalue);
            }

            return true;
        }

        public virtual bool IsObjectLocal { get { return ID <= 0; } }

        /// <summary>
        /// Creates a new T without requiring a public constructor
        /// </summary>
        /// <param name="t"></param>
        internal static AsanaObject Create(Type t)
        {
            try
            {
                AsanaObject o = (AsanaObject)Activator.CreateInstance(t == typeof(AsanaObject) ? typeof(AsanaDummyObject) : t, true);
                return o;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a new T without requiring a public constructor.
        /// If the ID exists in the cache of host, it will return that element instead.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ID"></param>
        /// <param name="host"></param>
        /// <param name="tryFromCacheFirst">if true try to reuse the old object</param>
        /// <returns></returns>
        internal static AsanaObject Create(Type t, Int64 ID, Asana host, bool tryFromCacheFirst = true)// = null)
        {
            if (host != null && tryFromCacheFirst && host.ObjectCache.Contains(ID.ToString()))
                return (AsanaObject) host.ObjectCache.Get(ID.ToString());

            var o = Create(t);

            if (ReferenceEquals(o.Host, null)) o.Host = host;

            if (ID != 0 && t != typeof(AsanaDummyObject) && t != typeof(AsanaProjectBase))
            {
                o.ID = ID;
                if (host != null) host.ObjectCache.Set(ID.ToString(), o);
            }

            if (t == typeof (AsanaWorkspace))
            {
                ((AsanaWorkspace) o).Projects.CollectionChanged += (sender, args) =>
                {
                    if (args.Action != NotifyCollectionChangedAction.Replace &&
                        args.Action != NotifyCollectionChangedAction.Remove) return;
                    foreach (var removedObj in args.OldItems)
                    {
                        (removedObj as AsanaProject).IsRemoved = true;
                    }
                };
                ((AsanaWorkspace)o).Tags.CollectionChanged += (sender, args) =>
                {
                    if (args.Action != NotifyCollectionChangedAction.Replace &&
                        args.Action != NotifyCollectionChangedAction.Remove) return;
                    foreach (var removedObj in args.OldItems)
                    {
                        (removedObj as AsanaTag).IsRemoved = true;
                    }
                };
            }

            return o;
        }

        /// <summary>
        /// Parameterless contructor
        /// </summary>
        internal AsanaObject()
        {
            
        }

        /// <summary>
        /// Overrides the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ID.ToString();
        }

        public static bool operator ==(AsanaObject a, Int64 id)
        {
            return a.ID == id;
        }

        public static bool operator !=(AsanaObject a, Int64 id)
        {
            return a.ID != id;
        }

        public override bool Equals(object obj)
        {
            if (obj is AsanaObject)
            {
                return ID == (obj as AsanaObject).ID;
            }
            if (obj is Int64)
            {
                return ID == (Int64)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public virtual Task Refresh(string optFields = null)
        {
            throw new NotImplementedException();
        }
    }

    // new version
    public interface IAsanaObjectCollection<T> : IList<T> where T: AsanaObject
    {
    }

    // new version
    [Serializable]
    // TODO: consider http://stackoverflow.com/questions/670577/observablecollection-doesnt-support-addrange-method-so-i-get-notified-for-each
    public class AsanaObjectCollection<T> : ObservableCollection<T>, IAsanaObjectCollection<T> where T : AsanaObject
    {
        public bool Initialized = false;
        public new void Add(T item)
        {
            if (!Initialized) Initialized = true;
            base.Add(item);
        }
    }

    static public class IAsanaObjectCollectionExtensions
    {
        // new version
        static public List<Task> RefreshAll<T>(this IAsanaObjectCollection<T> objects) where T : AsanaObject
        {
            var workers = new List<Task>();
            foreach (T o in objects)
            {
                if (o.Host == null)
                    throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
                workers.Add(o.Refresh());
            }
            return workers;
        }
    }

    static public class AsanaObjectExtensions
    {
        static public Task<T> Save<T>(this T obj) where T : AsanaObject
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Save(obj, null);
        }
    }
}
