using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuilderHelper {
    static public List<Builder> Builders = new List<Builder>(10);
    static private int _idCounter = 0;
    public static int AddBuilder(Builder b)
    {
        Builders.Add(b);
        return _idCounter++;
    }
    
    public static Builder GetBuilderById(int id)
    {
        return Builders[id];
    }
}
