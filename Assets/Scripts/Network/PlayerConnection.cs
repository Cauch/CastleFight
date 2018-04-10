using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour {
    [SyncVar]
    public string Name;
    [SyncVar]
    public int BuilderId;
    [SyncVar]
    public bool Allegiance;

    public GameObject UiManager;
    public GameObject MouseManager;
    public GameObject Indicator;
    private GameObject _world;

    // Use this for initialization
    void Start() {
        _world = GameObject.FindGameObjectWithTag("World");

        if (isLocalPlayer)
        {
            Builder player = Instantiate(PrefabIdHelper.Builders[BuilderId], _world.transform).GetComponent<Builder>();
            player.Allegiance = Allegiance;

            UiManager = Instantiate(UiManager);
            UiManager.GetComponent<UIManager>().DefaultSelectable = player;
            UiManager.GetComponent<UIManager>().IsOffline = false;
            MouseManager.GetComponent<MouseManager>().DefaultSelection = player.gameObject;
            MouseManager.GetComponent<MouseManager>().UiManager = UiManager.GetComponent<UIManager>();

            Instantiate(MouseManager);

            Instantiate(Indicator);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Commands 
    [Command]
    public void Cmd_InstantiateBuilding(NetworkHash128 prefabId, bool allegiance, Vector3 position)
    {
        GameObject go = Instantiate(PrefabIdHelper.IdGoBuilding[prefabId], position, Quaternion.identity, _world.transform);
        Attackable building = go.GetComponent<Attackable>(); 

        building.Allegiance = allegiance;
        building.IsActive = true;
        NetworkServer.Spawn(go);
        //Rpc_Instantiate(prefab, allegiance, position);
    }
        
    //Remote procedure calls
    //[ClientRpc]
    //public void Rpc_Instantiate(GameObject prefab, bool allegiance, Vector3 position)
    //{
    //    if(!prefab)
    //    {
    //        Debug.Log("RPC Prefab is null");
    //        return;
    //    }
    //}
}
