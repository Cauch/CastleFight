using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnits : MonoBehaviour {

    public List<Unit> Team0 = new List<Unit>(1);
    public List<uint> Numbers0 = new List<uint> { 1 };
    public Vector3 Pos0 = new Vector3(-100, 10, 0);
    public Vector3 OffSetPerUnitX0 = new Vector3(30, 0, 0);
    public Vector3 OffSetPerUnitZ0 = new Vector3(0, 0, 30);

    public List<Unit> Team1 = new List<Unit>(1);
    public List<uint> Numbers1 = new List<uint> { 1 };
    public Vector3 Pos1 = new Vector3(100, 10, 0);
    public Vector3 OffSetPerUnitX1 = new Vector3(30, 0, 0);
    public Vector3 OffSetPerUnitZ1 = new Vector3(0, 0, 30);

    // Use this for initialization
    void Start () {
        Transform world = GameObject.FindGameObjectWithTag("World").transform;
        for (int i =0; i < Numbers0.Count; i++)
        {
            for(int j = 0; j < Numbers0[i]; j++)
            {
                Unit u = Instantiate(Team0[i], this.transform.position, Quaternion.identity, world);
                u.Allegiance = false;
                u.transform.position = Pos0 + i * OffSetPerUnitX0 + j * OffSetPerUnitZ0;
                u.AdjustStart();
            }
        }

        for (int i = 0; i < Numbers1.Count; i++)
        {
            for (int j = 0; j < Numbers1[i]; j++)
            {
                Unit u = Instantiate(Team1[i], this.transform.position, Quaternion.identity, world);
                u.Allegiance = true;
                u.transform.position = Pos1 + i * OffSetPerUnitX1 + j * OffSetPerUnitZ1;
                u.AdjustStart();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}