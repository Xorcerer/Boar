using System;
using System.Collections.Generic;

namespace LoganZhou.Boar
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary, 
             TKey key,
             TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary,
             TKey key,
             Func<TKey, TValue> defaultValueProvider)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value
                : defaultValueProvider(key);
        }

        public static TValue GetOrAdd<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary, 
             TKey key,
             TValue defaultValue)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
                return value;

            return (dictionary[key] = defaultValue);
        }

        public static TValue GetOrAdd<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary, 
             TKey key,
             Func<TKey, TValue> defaultValueProvider)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
                return value;

            return (dictionary[key] = defaultValueProvider(key));
        }

        public static int AddOrIncrease<TKey>(this IDictionary<TKey, int> dictionary, 
                                           TKey key,
                                           int increment) 
        {
            var count = dictionary.GetOrAdd(key, 0);
            var newCount = count + increment;
            dictionary[key] = newCount;
            return newCount;
        }

        public static int AddOrDecrease<TKey>(this IDictionary<TKey, int> dictionary, 
                                              TKey key,
                                              int decrement)
        {
            dictionary.AddOrIncrease(key, -decrement);
        }
    
    }
}

