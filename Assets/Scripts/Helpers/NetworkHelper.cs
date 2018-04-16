using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class NetworkHelper {
    public static bool IsOffline = true;
    public static Builder Builder;
    public static PlayerConnection Player;

    public static void InstantiateBuilding(GameObject prefab, Vector3 position)
    {
        NetworkHash128 buildingId = prefab.GetComponent<NetworkIdentity>().assetId;
        Player.Cmd_InstantiateBuilding(buildingId, Builder.Allegiance, position);
    }

    public static void DestroyBuilding(GameObject prefab)
    {
        Player.Cmd_DestroyBuilding(prefab.GetComponent<NetworkIdentity>().netId);
    }

}
