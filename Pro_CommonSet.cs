using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pro_CommonSet 
{

    private static Pro_CommonSet instance;

    public static Pro_CommonSet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Pro_CommonSet();
            }
            return instance;
        }
    }

    private Dictionary<string, HashSet<Vector2Int>> set;    // Dictionary for all the Sets of the rules so it can be easily set a communication between them

    private Pro_CommonSet()
    {
        set = new Dictionary<string, HashSet<Vector2Int>>();
    }

    public void AddSet(string key)
    {
        set.Add(key, new HashSet<Vector2Int>());
    }

    public void AddToSet(string key, Vector2Int pos)
    {
        if (set.ContainsKey(key))
        {
            set[key].Add(pos);
        }
    }
    public bool IsContains(string key)
    {
        return set.ContainsKey(key);
    }

    public HashSet<Vector2Int> GetSet(string key)
    {
        if (set.ContainsKey(key))
        {
            return set[key];
        }

        throw new System.Exception("No such key");
        
    }
}
