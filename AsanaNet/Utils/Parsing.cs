using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace AsanaNet
{
    static class Parsing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public string SafeAssignString(Dictionary<string, object> source, string name)
        {
            if (source.ContainsKey(name))
            {
                return source[name].ToString();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static private T SafeAssign<T>(Dictionary<string, object> source, string name, Asana host)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            T value = default(T);  

            if (source.ContainsKey(name) && source[name] != null)
            {   
                if (converter.CanConvertFrom(typeof(string)) && !string.IsNullOrWhiteSpace(source[name].ToString()))
                {
                    value =  (T)converter.ConvertFromString(source[name].ToString());
                }
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static private T[] SafeAssignArray<T>(Dictionary<string, object> source, string name, Asana host)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            T[] value = null;

            if (source.ContainsKey(name) && source[name] != null)
            {
                var obj = source[name] as List<object>;
                var outputList = new List<T>();

                foreach (var element in obj)
                {
                    if (converter.CanConvertFrom(typeof(string)) && !string.IsNullOrWhiteSpace(element.ToString()))
                    {
                        outputList.Add((T)converter.ConvertFromString(element.ToString()));
                    }
                }

                value = outputList.ToArray();
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static private AsanaObject SafeAssignAsanaObject<T>(Dictionary<string, object> source, string name, Asana host) where T : AsanaObject
        {
            T value = null;

            if (source.ContainsKey(name) && source[name] != null)
            {
                var obj = source[name] as Dictionary<string, object>;
                // try from cache
                var thisId = (Int64) obj["id"];
                value = (T)AsanaObject.Create(typeof(T), thisId, host);
                Parsing.Deserialize(obj, (value as AsanaObject), host);
            }

            return value;
        }
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static private T[] SafeAssignAsanaObjectArray<T>(Dictionary<string, object> source, string name, Asana host) where T : AsanaObject
        {
            T[] value = null;

            if (source.ContainsKey(name) && source[name] != null)
            {
                var list = source[name] as List<object>;

                value = new T[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    // TODO: try from cache
                    T newObj = (T)AsanaObject.Create(typeof(T));
                    Parsing.Deserialize(list[i] as Dictionary<string, object>, (newObj as AsanaObject), host);
                    value[i] = newObj;
                }
            }

            return value;
        }
         * */
        static private AsanaObjectCollection<T> SafeAssignAsanaObjectCollection<T>(Dictionary<string, object> source, string name, Asana host, object currentCollection) where T : AsanaObject
        {
            var value = (AsanaObjectCollection<T>) currentCollection;

            if (source.ContainsKey(name) && source[name] != null)
            {
                var list = source[name] as List<object>;

                if (!list.Any())
                {
                    if (!ReferenceEquals(currentCollection, null))
                        return value;
                    return new AsanaObjectCollection<T>();
                }

                if (value == null)
                    value = new AsanaObjectCollection<T>();

                //value = new T[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    var obj = list[i] as Dictionary<string, object>;

                    // try from cache
                    var thisId = (Int64)obj["id"];
                    var listElement = (T)AsanaObject.Create(typeof(T), thisId, host);
                    Parsing.Deserialize(obj, listElement, host);

                    if (!value.Contains(listElement))
                        value.Add(listElement);

//                    T newObj = (T)AsanaObject.Create(typeof(T));
//                    Parsing.Deserialize(list[i] as Dictionary<string, object>, (newObj as AsanaObject), host);
//                    value[i] = newObj;
                }
            }

            return value;
        }
        
        /// <summary>
        /// Deserializes a dictionary based on AsanaDataAttributes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        static internal void Deserialize(Dictionary<string, object> data, AsanaObject obj, Asana host)
        {
            foreach(var objectProperty in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (objectProperty.Name == "Host")
                {
                    if (obj.Host != host)
                        objectProperty.SetValue(obj, host, new object[] { });
                    continue;
                }

                try
                {
                    var thisAttributes = objectProperty.GetCustomAttributes(typeof(AsanaDataAttribute), false);
                    if (thisAttributes.Length == 0)
                        continue;

                    AsanaDataAttribute thisAttribute = thisAttributes[0] as AsanaDataAttribute;

                    if(!data.ContainsKey(thisAttribute.Name))
                        continue;

                    if (objectProperty.PropertyType == typeof(string))
                    {
                        objectProperty.SetValue(obj, SafeAssignString(data, thisAttribute.Name), null);
                    }
                    else
                    {
                        Type type = objectProperty.PropertyType.IsArray ? objectProperty.PropertyType.GetElementType() : objectProperty.PropertyType;
                        MethodInfo method = null;
                        object[] invokeParams = new object[] {data, thisAttribute.Name, host};
                        if (typeof (AsanaObject).IsAssignableFrom(type))
                        {
                            // SafeAssignAsanaObjectArray
                            method = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "SafeAssignAsanaObjectCollection" : "SafeAssignAsanaObject", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                            if (objectProperty.PropertyType.IsArray)
                                invokeParams = new object[] { data, thisAttribute.Name, host, objectProperty.GetValue(obj) };
                        }
                        else
                            method = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "SafeAssignArray" : "SafeAssign", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });

                        var methodResult = method.Invoke(null, invokeParams);

                        // this check handle base-class properties
                        if (objectProperty.DeclaringType != obj.GetType())
                        {
                            var p2 = objectProperty.DeclaringType.GetProperty(objectProperty.Name);
                            p2.SetValue(obj, methodResult, null);
                        }
                        else
                        {
                            objectProperty.SetValue(obj, methodResult, null);
                        }
                    }
                }
                catch(Exception)
                { 
                }
            }
        }

        //static internal T SetValueOnBaseType<T>

        /// <summary>
        /// Serializes a dictionary based on AsanaDataAttributes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        static internal Dictionary<string, object> Serialize(AsanaObject obj, bool asString, bool dirtyOnly, bool ignoreRequired = false)
        {
            var dict = new Dictionary<string, object>();

            foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                try
                {
                    var cas = p.GetCustomAttributes(typeof(AsanaDataAttribute), false);
                    if (cas.Length == 0)
                        continue;

                    AsanaDataAttribute ca = cas[0] as AsanaDataAttribute;

                    if (ca.Flags.HasFlag(SerializationFlags.Omit))
                        continue;

                    bool required = ca.Flags.HasFlag(SerializationFlags.Required);

                    object value = p.GetValue(obj, new object[] { });

                    if (dirtyOnly && !obj.IsDirty(ca.Name, value))
                        continue; 

                    bool present = ValidateSerializableValue(ref value, ca, p);

                    if (present == false)
                        if (!required || ignoreRequired)
                            continue;
                        else
                            throw new MissingFieldException("Couldn't save object because it was missing a required field: " + p.Name);

                    if (value.GetType().IsArray)
                    {
                        int count = 0;
                        foreach (var x in (object[])value)
                        {
                            dict.Add(ca.Name + "[" + count + "]", asString ? x.ToString() : x);
                            count++;
                        }
                    }
                    else
                    {
                        dict.Add(ca.Name, asString ? value.ToString() : value);
                    }                    
                }
                catch (Exception)
                {
                }
            }

            return dict;
        }

        static internal bool ValidateSerializableValue(ref object value, AsanaDataAttribute ca, PropertyInfo p)
        {
            bool present = true;

            // check we're valid -- edge cases first
            if (value == null)
            {
                present = false;
            }
            else if (value.GetType() == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(value as string))
                    present = false;
            }
            else if (value.GetType() == typeof(DateTime))
            {
                if((DateTime)value == new DateTime())
                    present = false;
            }
            else if (value != null && value is AsanaObject)
            {
                if (ca.Fields.Length == 1)
                {
                    var pInternal = value.GetType().GetProperty(ca.Fields[0]);
                    if (pInternal == null)
                        throw new CustomAttributeFormatException(string.Format("The AsanaDataAttribute for '{0}' specifies the Property '{1}' as a serialization value but this Property couldn't be found.", p.Name, ca.Fields[0]));

                    value = pInternal.GetValue(value, new object[] { });
                    return ValidateSerializableValue(ref value, ca, p);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return present;
        }
    }
}
