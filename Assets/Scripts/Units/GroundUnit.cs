using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GroundUnit : Unit
{
    // Use this for initialization
    new protected void Start () {
        base.Start();

		NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
		navMesh.speed = this.DefaultSpeed;

		_move = new MoveGrounded(this, navMesh, (Targetable t) => true);
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    protected override void Move(Targetable target)
    {
        //if (_navMesh.enabled)
        //{
        //    _navMesh.SetDestination(target.transform.position);
        //    _navMesh.isStopped = false;
        //}
    }

    protected override void StopMoving()
    {
        //if (_navMesh.enabled)
        //{
        //    _navMesh.isStopped = true;
        //}
    }

    private void OnEnable()
    {
        //if (_navMesh)
        //{
        //    _navMesh.enabled = true;
        //}
    }

    private void OnDisable()
    {
        //if (_navMesh)
        //{
        //    _navMesh.enabled = false;
        //}
    }

    public override void SetSpeed(float speed)
    {
        //_navMesh.speed = speed;
    }
}
