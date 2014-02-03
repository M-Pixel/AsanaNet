using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [AsanaDataAttribute("sync_state", SerializationFlags.Optional)]
        public bool IsPossiblyOutOfSync {
            get { return _syncState; }
            private set
            {
                _syncState = value;
                TouchChanged();
            } 
        }
        private bool _syncState { get; set; }

        [AsanaDataAttribute("sync_removed", SerializationFlags.Optional)]
        public virtual bool IsRemoved 
        {
            get
            {
                //return _isRemoved;
                return ID == (Int64)AsanaExistance.Deleted;
            }
            internal set
            {
                //_isRemoved = value;
                ID = (Int64) AsanaExistance.Deleted;
            }
        }
        //private bool _isRemoved { get; set; }

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
                    DatasetFlushAction(this);
//                _eventList.FlushReturnObject = this;
//                _eventList.DatasetFlushAction += DatasetFlushAction;
            }
        }
        protected virtual void DatasetFlushAction(AsanaObject response)
        {
            //throw new NotImplementedException();
        }

        private AsanaEventList _eventList { get; set; }

        public Task Sync(string optFields = null)
        {
//            if (object.ReferenceEquals(EventList))
            return Host.GetEvents(this, ReferenceEquals(EventList, null) ? "0" : EventList.SyncToken, optFields,
                AsanaCacheLevel.Ignore)
                .ContinueWith(
                    (eventList) =>
                    {
                        EventList = eventList.Result;
                    });
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

    [Serializable]
    public abstract class AsanaObject
    {
        [AsanaDataAttribute("id", SerializationFlags.Omit)]
        public Int64 ID { get; internal set; }

        public Asana Host { get; internal set; }

        //add event handler here or in separate interface extended

        /// <summary>
        /// A positive response has been received but any object updating has yet to be performed.
        /// </summary>
        public event AsanaResponseEventHandler Saving;

        /// <summary>
        /// The object was saved successfully and changes should be reflected.
        /// </summary>
        public event AsanaResponseEventHandler Saved;

        /// <summary>
        /// The object was changed remotely, and the changes might be reflected in the state.
        /// </summary>
        public event AsanaResponseEventHandler Changed;

        public void TouchChanged()
        {
            if (Changed != null)
                Changed(this);
        }

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

        internal void SavingCallback(Dictionary<string, object> saved)
        {
            _lastSave = saved;

            if (Saving != null)
                Saving(this);
        }

        internal void SavedCallback()
        {
            if (Saved != null)
                Saved(this);
        }
        public virtual bool IsObjectLocal { get { return ID <= 0; } }

        public void SetAsReferenceObject()
        {
            SavingCallback(Parsing.Serialize(this, false, false, true));
        }
     
        /// <summary>
        /// Creates a new T without requiring a public constructor
        /// </summary>
        /// <param name="t"></param>
        internal static AsanaObject Create(Type t)
        {
            try
            {
                AsanaObject o = (AsanaObject)Activator.CreateInstance(t == typeof(AsanaObject) ? typeof(AsanaDummyParent) : t, true);
                return o;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        internal static T Create<T>() where T: AsanaObject
        {
            try
            {
                var o = Activator.CreateInstance<T>();
//                AsanaObject o = (AsanaObject)Activator.CreateInstance(t, true);
                return o;
            }
            catch (System.Exception)
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
        /// <param name="host">if not null then try to reuse the old object</param>
        /// <returns></returns>
        internal static AsanaObject Create(Type t, Int64 ID, Asana host = null)
        {
            if (host != null && host._objectCache.Contains(ID.ToString()))
                return (AsanaObject) host._objectCache.Get(ID.ToString());

            AsanaObject o = Create(t);
            o.ID = ID;

            if (host != null) host._objectCache.Add(ID.ToString(), o);

            return o;
        }

        internal static T Create<T>(Int64 ID, Asana host = null) where T: AsanaObject
        {
            if (host != null && host._objectCache.Contains(ID.ToString()))
                return (T) host._objectCache.Get(ID.ToString());

            T o = Create<T>();
//            AsanaObject o = Create(t);
            o.ID = ID;
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
                return this.ID == (obj as AsanaObject).ID;
            }
            if (obj is Int64)
            {
                return this.ID == (Int64)obj;
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
    /*
    // depracated version
    public interface IAsanaObjectCollection : IList<AsanaObject>
    {
    }

    // depracated version
    [Serializable]
    public class AsanaObjectCollection : ObservableCollection<AsanaObject>, IAsanaObjectCollection
    {
    }
    */

    // new version
    public interface IAsanaObjectCollection<T> : IList<T> where T: AsanaObject
    {
    }

    // new version
    [Serializable]
    public class AsanaObjectCollection<T> : ObservableCollection<T>, IAsanaObjectCollection<T> where T : AsanaObject
    {
    }

    static public class IAsanaObjectCollectionExtensions
    {
        /*
        // depracated version
        static public List<Task> RefreshAll<T>(this IAsanaObjectCollection objects) where T : AsanaObject
        {
            List<Task> workers = new List<Task>();
            foreach (T o in objects)
            {
                if (o.Host == null)
                    throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
                workers.Add(o.Refresh());
            }
            return workers;
        }
        */
        // new version
        static public List<Task> RefreshAll<T>(this IAsanaObjectCollection<T> objects) where T : AsanaObject
        {
            List<Task> workers = new List<Task>();
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
        /*
        static public Task<T> Save<T>(this T obj, Asana host, AsanaFunction function) where T : AsanaObject
        {
            return host.Save(obj, function);
        }

        static public Task<T> Save<T>(this T obj, Asana host) where T: AsanaObject
        {
            return host.Save(obj, null);
        }

        static public Task<T> Save<T>(this T obj, AsanaFunction function) where T : AsanaObject
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Save(obj, function);
        }
        */
        static public Task<T> Save<T>(this T obj) where T : AsanaObject
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Save(obj, null);
        }
        /*
        static public Task Delete(this AsanaObject obj, Asana host)
        {
            return host.Delete(obj);
        }

        static public Task Delete(this AsanaObject obj)
        {
            if (obj.Host == null)
                throw new NullReferenceException("This AsanaObject does not have a host associated with it so you must specify one when saving.");
            return obj.Host.Delete(obj);
        }
         * */
    }
}
