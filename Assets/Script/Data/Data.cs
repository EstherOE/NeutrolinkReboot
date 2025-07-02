using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Balancing
{
    static GameData _cache;
    public static GameData Load()
    {
        if (_cache == null)
            _cache = Resources.Load<GameData>("Balancing_Default");
        return _cache;
    }
}