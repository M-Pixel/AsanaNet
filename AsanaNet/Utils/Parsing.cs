using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Debug.Assert(!source.ContainsKey(name) || (source.ContainsKey(name) && !ReferenceEquals(source[name], null)));
            if (source.ContainsKey(name) && !ReferenceEquals(source[name], null))
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
                Int64 thisId = 0;
                if (obj.ContainsKey("id"))
                    thisId = (Int64) obj["id"];
                value = (T)AsanaObject.Create(typeof(T), thisId, host);
                Deserialize(obj, (value as AsanaObject), host);
            }

            return value;
        }

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

//                var newObjects = new List<T>(list.Count);

                //value = new T[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    var obj = list[i] as Dictionary<string, object>;

                    // try from cache
                    T listElement;
                    if(obj.ContainsKey("id"))
                    {
                        var thisId = (Int64)obj["id"];
                        listElement = (T)AsanaObject.Create(typeof(T), thisId, host);
                    }
                    else
                    {
                        listElement = (T)AsanaObject.Create(typeof(T), 0, host);
                    }
                    Deserialize(obj, listElement, host);

                    if (!value.Contains(listElement))
//                        newObjects.Add(listElement);
                        value.Add(listElement);

//                    T newObj = (T)AsanaObject.Create(typeof(T));
//                    Parsing.Deserialize(list[i] as Dictionary<string, object>, (newObj as AsanaObject), host);
//                    value[i] = newObj;
                }
            }

            return value;
        }
        /*
        /// <summary>
        /// http://stackoverflow.com/questions/5461295/using-isassignablefrom-with-generics
        /// </summary>
        /// <param name="givenType"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
        */


        static private bool IsDifferent<T>(Dictionary<string, object> source, string name, object oldValue)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            if (source.ContainsKey(name) && source[name] != null)
            {
                if (converter.CanConvertFrom(typeof(string)) && !string.IsNullOrWhiteSpace(source[name].ToString()))
                {
                    var valueToString = source[name].ToString();
                    var oldValueCast = (T)oldValue;
                    var oldValueToString = String.Empty;
                    if (!ReferenceEquals(oldValueCast, null))
                        oldValueToString = ((T) oldValue).ToString();

                    return valueToString == oldValueToString;
                }
            }
            return true;
        }
        static private bool IsDifferentArray<T>(Dictionary<string, object> source, string name, object oldValue)
        {
//            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
//
//            if (source.ContainsKey(name) && source[name] != null)
//            {
//                var obj = source[name] as List<object>;
//                var outputList = new List<T>();
//
//                foreach (var element in obj)
//                {
//                    if (converter.CanConvertFrom(typeof(string)) && !string.IsNullOrWhiteSpace(element.ToString()))
//                    {
//                        outputList.Add((T)converter.ConvertFromString(element.ToString()));
//                    }
//                }
//
//                value = outputList.ToArray();
//            }
            // TODO!
            return true;
        }

        /// <summary>
        /// Deserializes a dictionary based on AsanaDataAttributes
        /// </summary>
        /// <param name="data"></param>
        /// <param name="obj"></param>
        static internal void Deserialize(Dictionary<string, object> data, AsanaObject obj, Asana host)
        {
            var hasChanged = false;
            var firstTimeObject = false;

            // we need to set the Host first to use caching
            PropertyInfo prop = obj.GetType().GetProperty("Host", BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                if (obj.Host != host)
                {
                    prop.SetValue(obj, host);
                    firstTimeObject = true;
                }
            }

//            foreach (
//                var objectProperty in
//                    obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
//            {
//                var test = objectProperty.GetCustomAttributesData();
//            }
            var query =
                (from objectProperty in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                where objectProperty.GetCustomAttributesData().Any(attr => attr.AttributeType == typeof (AsanaDataAttribute))
                    // && objectProperty.Name != "Host"
                group objectProperty by objectProperty.GetCustomAttributes(typeof (AsanaDataAttribute), true)) // was false
                .ToDictionary(group => group.FirstOrDefault(), group =>
                {
                    if (group.Key.Length == 0) return null;
                    return group.Key.First() as AsanaDataAttribute;
                });

            var filtered = from property in query
                            where !ReferenceEquals(property.Value, default(AsanaDataAttribute))
                            orderby property.Value.Priority
                            select property;

            foreach (var propAttrib in filtered)
            {
                var objectProperty = propAttrib.Key;
                var thisAttribute = propAttrib.Value;

                if (!data.ContainsKey(thisAttribute.Name))
                    continue;

                if (objectProperty.PropertyType == typeof(string))
                {
                    var oldValue = objectProperty.GetValue(obj) as string;
                    var newValue = SafeAssignString(data, thisAttribute.Name);
                    if (oldValue != newValue)
                        objectProperty.SetValue(obj, newValue, null);
                    hasChanged = hasChanged || oldValue != newValue;
                    continue;
                }

                Type type = objectProperty.PropertyType.IsArray ? objectProperty.PropertyType.GetElementType() : objectProperty.PropertyType;
                //                        type = type.IsGenericType ? 
                MethodInfo method;
                object methodResult;
                object[] invokeParams = { data, thisAttribute.Name, host };

                if (typeof(AsanaObject).IsAssignableFrom(type))
                {
                    method = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "SafeAssignAsanaObjectCollection" : "SafeAssignAsanaObject", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                    if (objectProperty.PropertyType.IsArray)
                        invokeParams = new[] { data, thisAttribute.Name, host, objectProperty.GetValue(obj) };

                    methodResult = method.Invoke(null, invokeParams);
                }
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AsanaObjectCollection<>))
                {
                    method =
                        typeof(Parsing).GetMethod("SafeAssignAsanaObjectCollection",
                            BindingFlags.NonPublic | BindingFlags.Static)
                            .MakeGenericMethod(new[] { type.GetGenericArguments()[0] });

                    invokeParams = new[] { data, thisAttribute.Name, host, objectProperty.GetValue(obj) };

                    methodResult = method.Invoke(null, invokeParams);
                }
                else
                {
                    method = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "SafeAssignArray" : "SafeAssign", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                    methodResult = method.Invoke(null, invokeParams);

                    var isDiffMethod = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "IsDifferentArray" : "IsDifferent", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                    var oldValue = objectProperty.GetValue(obj);
                    var isDifferent = (isDiffMethod.Invoke(null, new[] { data, thisAttribute.Name, oldValue }) as bool?).Value;
                    hasChanged = hasChanged || isDifferent;
                }

                // this check handle base-class properties
                if (objectProperty.DeclaringType != obj.GetType())
                {
                    var baseProperty = objectProperty.DeclaringType.GetProperty(objectProperty.Name);
                    baseProperty.SetValue(obj, methodResult, null);
                }
                else
                {
                    objectProperty.SetValue(obj, methodResult, null);
                }
//                if (!firstTimeObject && hasChanged)
//                    obj.TouchChanged();
            }
            if (firstTimeObject || hasChanged)
                obj.TouchUpdated();
            /*
            foreach(var objectProperty in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
            {
                if (objectProperty.Name == "Host")
                {
//                    if (obj.Host != host)
//                        objectProperty.SetValue(obj, host, new object[] { });
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
                        var oldValue = objectProperty.GetValue(obj) as string;
                        var newValue = SafeAssignString(data, thisAttribute.Name);
                        objectProperty.SetValue(obj, newValue, null);
                        hasChanged = hasChanged || oldValue != newValue;
                        continue;
                    }

                    Type type = objectProperty.PropertyType.IsArray ? objectProperty.PropertyType.GetElementType() : objectProperty.PropertyType;
//                        type = type.IsGenericType ? 
                    MethodInfo method;
                    object methodResult;
                    object[] invokeParams = {data, thisAttribute.Name, host};
                        
                    if (typeof(AsanaObject).IsAssignableFrom(type))
                    {
                        method = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "SafeAssignAsanaObjectCollection" : "SafeAssignAsanaObject", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                        if (objectProperty.PropertyType.IsArray)
                            invokeParams = new[] { data, thisAttribute.Name, host, objectProperty.GetValue(obj) };

                        methodResult = method.Invoke(null, invokeParams);
                    }
                    else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (AsanaObjectCollection<>))
                    {
                        method =
                            typeof (Parsing).GetMethod("SafeAssignAsanaObjectCollection",
                                BindingFlags.NonPublic | BindingFlags.Static)
                                .MakeGenericMethod(new[] {type.GetGenericArguments()[0]});

                        invokeParams = new[] {data, thisAttribute.Name, host, objectProperty.GetValue(obj)};

                        methodResult = method.Invoke(null, invokeParams);
                    }
                    else
                    {
                        method = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "SafeAssignArray" : "SafeAssign", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                        methodResult = method.Invoke(null, invokeParams);

                        var isDiffMethod = typeof(Parsing).GetMethod(objectProperty.PropertyType.IsArray ? "IsDifferentArray" : "IsDifferent", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(new[] { type });
                        var oldValue = objectProperty.GetValue(obj);
                        var isDifferent = (isDiffMethod.Invoke(null, new[] { data, thisAttribute.Name, oldValue }) as bool?).Value;
                        hasChanged = hasChanged || isDifferent;
                    }

                    // this check handle base-class properties
                    if (objectProperty.DeclaringType != obj.GetType())
                    {
                        var baseProperty = objectProperty.DeclaringType.GetProperty(objectProperty.Name);
                        baseProperty.SetValue(obj, methodResult, null);
                    }
                    else
                    {
                        objectProperty.SetValue(obj, methodResult, null);
                    }
                }
                catch(Exception)
                { 
                }
            
                if (!firstTimeObject && hasChanged)
                    obj.TouchChanged();
            }
             * */
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
                    var cas = p.GetCustomAttributes(typeof(AsanaDataAttribute), true); // was false
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
                    else if (value.GetType().GetInterface(typeof(ICollection<>).FullName) != null) //(value is ICollection)
                    {
                        int count = 0;
                        foreach (var x in (ICollection)value)
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
                // explanation: we need to be able to delete a string too!
                if ((value as string) == null)
                //if (string.IsNullOrEmpty(value as string))
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
