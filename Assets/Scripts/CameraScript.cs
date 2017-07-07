using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=cfjLQrMGEb4

public class CameraScript : MonoBehaviour {

    public float camSpeed = 80f;
    public float camBorderHorizontal = 10f;
    public float camBorderVertical = 10f;
    public float scrollSpeed = 20f;

    public Vector3 clampMin = new Vector3(-500f, 100f, -200f );      
    public Vector3 clampMax = new Vector3( 500f, 300f, 100f);

    void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - camBorderVertical)
        {
            pos.z += camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= camBorderHorizontal)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= camBorderVertical)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - camBorderHorizontal)
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        // Clamping
        //pos.x = Mathf.Clamp(pos.x, -mapWidth, mapWidth);
        //pos.y = Mathf.Clamp(pos.y, maxZoom, maxUnzoom);d
        //pos.z = Mathf.Clamp(pos.z, -mapHeight, mapHeight);

        clamp(ref pos, clampMin, clampMax);

        transform.position = pos;
    }

    void clamp(ref Vector3 v, Vector3 min, Vector3 max)
    {
        v.x = Mathf.Clamp(v.x, min.x, max.x);
        v.y = Mathf.Clamp(v.y, min.y, max.y);
        v.z = Mathf.Clamp(v.z, min.z, max.z);
    }
}
