using System;
using System.Collections.Generic;

namespace Gullis
{
    public static class EnumerableExtensions
    {
        public static void Insert<T>(List<T> list, T item, int index = -1)
        {
            if (index < 0)
            {
                list.Add(item);
            }
            else
            {
                list.Insert(index, item);
            }
        }
        
        public static R Find<R, T>(Dictionary<Type, T> map) where R : T
        {
            return (R) Find(map, typeof(R));
        }

        public static T Find<T>(Dictionary<Type, T> map, Type requiredType)
        {
            if (map.ContainsKey(requiredType))
            {
                return map[requiredType];
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return map[key];
                }
            }

            throw new Exception("Value is not found!");
        }

        public static bool TryFind<T>(Dictionary<Type, T> map, Type requiredType, out T item)
        {
            if (map.ContainsKey(requiredType))
            {
                item =  map[requiredType];
                return true;
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    item = map[key];
                    return true;
                }
            }

            item = default;
            return false;
        }
    }
}