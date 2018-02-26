using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GroundUnit : Unit
{
    private NavMeshAgent _navMesh;

    // Use this for initialization
    new protected void Start () {
        base.Start();
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.speed = this.DefaultSpeed;
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    protected override void Move(Attackable target)
    {
        _navMesh.SetDestination(target.transform.position);
        _navMesh.isStopped = false;
    }

    protected override void StopMoving()
    {
        _navMesh.isStopped = true;
    }
}
