using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundUnit : Unit {
    NavMeshAgent navMesh;

    // Use this for initialization
    new void Start () {
        base.Start();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = this.defaultSpeed;
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    protected override void Move(Attackable target)
    {
        navMesh.SetDestination(target.transform.position);
        navMesh.isStopped = false;

    }

    protected override void StopMoving()
    {
        navMesh.isStopped = true;
    }
}
