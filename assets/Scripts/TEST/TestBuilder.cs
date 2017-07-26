using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilder : MonoBehaviour {
   
    public GameObject uiManager;
    public GameObject mouseManager;
    public GameObject indicator;
    public List<GameObject> players;
    public List<Vector3> playerPosList;
    public List<GameObject> gObjects;
    public List<Vector3> gObjectsPosList;

    GameObject world;
    // Use this for initialization
    void Start() {
        world = GameObject.FindGameObjectWithTag("World");
        for (int i = 0; i < gObjects.Count; i++)
        {
            GameObject go = Instantiate(gObjects[i]);
            go.transform.SetParent(world.transform);
            go.transform.localScale = gObjects[i].transform.localScale;
            try { go.transform.position = gObjectsPosList[i]; } catch (Exception e) { Debug.Log(e); }
        }


        GameObject playerInstance = null;

        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i];
            Vector3 playerPos = playerPosList[i];
            playerInstance = Instantiate(player, playerPos, Quaternion.identity);
            playerInstance.transform.SetParent(world.transform);
            playerInstance.transform.localScale = player.transform.localScale;
        }


        //Code dégueulasse
        uiManager.GetComponent<UIManager>().defaultPanel = playerInstance.GetComponent<Builder>().uiPanel;
        
        uiManager = Instantiate(uiManager);

        mouseManager.GetComponent<MouseManager>().defaultSelection = playerInstance;
        mouseManager.GetComponent<MouseManager>().uiManager = uiManager.GetComponent<UIManager>();

        Instantiate(mouseManager);

        Instantiate(indicator);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
