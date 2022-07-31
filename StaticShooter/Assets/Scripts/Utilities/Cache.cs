using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    static Dictionary<string, Transform> cache = new Dictionary<string, Transform>();
    public static Transform Get(string key) {
        return cache[key];
    }
    public static void Put(string key, Transform gameObject) {
        cache.Add(key, gameObject);
    }

    public static bool Contains(string key) {
        return cache.ContainsKey(key);
    }
}
