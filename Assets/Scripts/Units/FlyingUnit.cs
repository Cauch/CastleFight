using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingUnit : Unit {
    float _speed;

	// Use this for initialization
	new void Start () {
        base.Start();
        _speed = DefaultSpeed;
    }

    // Update is called once per frame
    new void Update () {
        SetHeight();
        base.Update();
    }


    protected override void Move(Targetable target)
    {
        Vector3 direction = new Vector3(target.transform.position.x - this.transform.position.x, 0f, target.transform.position.z - this.transform.position.z).normalized;
        this.transform.position += direction * _speed;
    }


    protected override void StopMoving(){ 
        //Do nothing
    }

    void SetHeight()
    {
        this.transform.position = this.IsActive ? new Vector3(this.transform.position.x, 100f, this.transform.position.z) : new Vector3(this.transform.position.x, 3f, this.transform.position.z);
    }

    public override void SetSpeed(float speed)
    {
        _speed = speed;
    }

}
