using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilder : MonoBehaviour {
    public List<GameObject> gObjects;
    public List<Vector3> positions;
    public GameObject uiManager;
    public GameObject mouseManager;
    public GameObject indicator;
    public GameObject player;
    public Vector3 playerPos;

    GameObject world;
	// Use this for initialization
	void Start () {
        world = GameObject.FindGameObjectWithTag("World");
        for (int i = 0; i < gObjects.Count; i++)
        {
            GameObject go = Instantiate(gObjects[i]);
            go.transform.SetParent(world.transform);
            go.transform.localScale = gObjects[i].transform.localScale;
            try { go.transform.position = positions[i]; } catch (Exception e) { Debug.Log(e); }
        }

        GameObject p = Instantiate(player);
        p.transform.SetParent(world.transform);
        p.transform.localScale = player.transform.localScale;
        p.transform.position = playerPos;

        //Code dégueulasse
        uiManager.GetComponent<UIManager>().defaultPanel = p.GetComponent<Builder>().uiPanel;
        

        uiManager = Instantiate(uiManager);

        mouseManager.GetComponent<MouseManager>().defaultSelection = p;
        mouseManager.GetComponent<MouseManager>().uiManager = uiManager.GetComponent<UIManager>();

        Instantiate(mouseManager);

        Instantiate(indicator);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
