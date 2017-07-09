using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilder : MonoBehaviour {
    public List<GameObject> gObjects;
    public List<Vector3> positions;
    public GameObject uiManager;

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

        //Code dégueulasse
        uiManager.GetComponent<UIManager>().defaultPanel = gObjects[2];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
