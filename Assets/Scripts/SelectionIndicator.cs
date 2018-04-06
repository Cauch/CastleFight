using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {
    MouseManager mm;
	// Use this for initialization
	void Start () {
        mm = FindObjectOfType<MouseManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if(mm.SelectedObject != null)
        {
            Bounds bigBounds = mm.SelectedObject.GetComponentInChildren<Renderer>().bounds;
            Renderer[] rs = mm.SelectedObject.GetComponentsInChildren<Renderer>();

            foreach(Renderer r in rs) {
                bigBounds.Encapsulate(r.bounds);
            }
            this.transform.localScale = 33f/32f * (new Vector3(bigBounds.size.x, 1, bigBounds.size.z));

            Vector3 t = bigBounds.center;
            t.y = 0.01f;
            this.transform.position = t;
        }
    }
}
