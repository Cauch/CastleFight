using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class PrefabIdHelper {
    public static int AvailableId = 0;

    public static Dictionary<NetworkHash128, GameObject> IdGoBuilding = PopulateBuildingDictionaryIdGo();
    public static Dictionary<GameObject, NetworkHash128> GoIdBuilding = Reverse(PopulateBuildingDictionaryIdGo());
    
    public static List<GameObject> Builders = LoadAllBuilder();

    public static Dictionary<NetworkHash128, GameObject> PopulateBuildingDictionaryIdGo()
    {
        return Resources.LoadAll<GameObject>("")
            .Where(go => go.GetComponent<NetworkIdentity>()).
            ToDictionary(go=> go.GetComponent<NetworkIdentity>().assetId);
    }

    public static Dictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
    {
        var dictionary = new Dictionary<TValue, TKey>();
        foreach (var entry in source)
        {
            if (!dictionary.ContainsKey(entry.Value))
                dictionary.Add(entry.Value, entry.Key);
        }
        return dictionary;
    }

    public static List<GameObject> LoadAllBuilder()
    {
        return Resources.LoadAll<GameObject>("")
            .Where(go=>go.GetComponent<Builder>()).
            ToList();
    }

}
