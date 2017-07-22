using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingUnit : Unit {
	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
        SetHeight();
        base.Update();
    }


    protected override void Move(Attackable target)
    {
        Vector3 direction = new Vector3(target.transform.position.x - this.transform.position.x, 0f, target.transform.position.z - this.transform.position.z).normalized;
        this.transform.position += direction * defaultSpeed;
    }


    protected override void StopMoving(){ 
        //Do nothing
    }

    void SetHeight()
    {
        this.transform.position = this.isActive ? new Vector3(this.transform.position.x, 100f, this.transform.position.z) : new Vector3(this.transform.position.x, 3f, this.transform.position.z);
    }

}
