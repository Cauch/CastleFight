using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=cfjLQrMGEb4

public class CameraScript : MonoBehaviour
{

    public float CamSpeed = 80f;
    public float CamBorderHorizontal = 10f;
    public float CamBorderVertical = 10f;
    public float ScrollSpeed = 20f;

    public Vector3 InitCam = new Vector3(-400, 120, 0);

    public Vector3 ClampMin = new Vector3(-500f, 50, -200f );      
    public Vector3 ClampMax = new Vector3( 500f, 200, 200f);

    public void Start()
    {
        Clamp(ref InitCam, ClampMin, ClampMax);
        transform.position = InitCam;
    }

    void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - CamBorderVertical)
        {
            pos.z += CamSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= CamBorderHorizontal)
        {
            pos.x -= CamSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= CamBorderVertical)
        {
            pos.z -= CamSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - CamBorderHorizontal)
        {
            pos.x += CamSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos.y -= scroll * ScrollSpeed;

        // Clamping
        //pos.x = Mathf.Clamp(pos.x, -mapWidth, mapWidth);
        //pos.y = Mathf.Clamp(pos.y, maxZoom, maxUnzoom);d
        //pos.z = Mathf.Clamp(pos.z, -mapHeight, mapHeight);

        Clamp(ref pos, ClampMin, ClampMax);

        transform.position = pos;
    }

    void Clamp(ref Vector3 v, Vector3 min, Vector3 max)
    {
        v.x = Mathf.Clamp(v.x, min.x, max.x);
        v.y = Mathf.Clamp(v.y, min.y, max.y);
        v.z = Mathf.Clamp(v.z, min.z, max.z);
    }
}
