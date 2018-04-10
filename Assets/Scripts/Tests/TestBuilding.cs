using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilding : MonoBehaviour {
    public List<GameObject> buildings;
    public Vector3 startPos;
    public float offsetX, offsetZ;
    public GameObject world;

	// Use this for initialization
	void Start () {
        startPos = startPos - new Vector3(offsetX, 0, offsetZ);

        foreach (GameObject b in buildings)
        {
            GameObject temp = Instantiate(b);
            temp.transform.position = startPos += new Vector3(offsetX, 0, offsetZ);
                temp.transform.SetParent(world.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
