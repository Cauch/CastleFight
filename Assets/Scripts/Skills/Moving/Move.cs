using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : ActiveSkill
{
	public Targetable Target = null;

	public Move(float range, float refreshSpeed, float executionTime, Targetable caster, Func<Targetable, bool> isValidTarget) : base(range, refreshSpeed, executionTime, caster, isValidTarget) { }

	public abstract void SetSpeed(float speed);
	public abstract void SetRange(float range);
	public abstract void SetDestination(Vector3 target);
	public abstract void SetEnable(bool enable);
}
