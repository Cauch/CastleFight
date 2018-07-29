using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveGrounded : Move
{
	private NavMeshAgent _navMesh;
	private float _range;

	public MoveGrounded(Targetable caster, NavMeshAgent navMesh, Func<Targetable, bool> isValidTarget) : base(100000, 0.001f, 1000, caster, isValidTarget)
    {
		_navMesh = navMesh;
		_navMesh.stoppingDistance = 0.0f;
    }

    public override void ApplyOnTarget(Targetable target)
    {
        base.ApplyOnTarget(target);
		_navMesh.isStopped = false;
		Target = target;
    }

	public override void Break()
	{
		base.Break();
		_navMesh.isStopped = true;
	}

	protected override void Complete()
    {
		Debug.Log("Somehow completed move by time limit");
    }

	public override ActiveSkill Update()
	{
		if (!Target || TargetingFunction.IsInRangeorMelee(Caster, Target, _range))
		{
			Break();
		}
		else
		{
			_navMesh.SetDestination(Target.transform.position);
		}

		return base.Update();
	}

	public override void SetSpeed(float speed)
	{
		_navMesh.speed = speed;
	}

	public override void SetRange(float range)
	{
		_range = range;
	}

	public override void SetDestination(Vector3 target)
	{
		_navMesh.SetDestination(target);
	}

	public override void SetEnable(bool enable)
	{
		_navMesh.enabled = enable;
	}
}
