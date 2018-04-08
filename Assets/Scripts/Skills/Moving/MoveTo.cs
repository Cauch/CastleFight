using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : ActiveSkill {
    Action<Targetable> _move;

    public MoveTo(Targetable caster, Action<Targetable> move, Func<Targetable, bool> isValidTarget) : base(10000, 0, 5, caster, isValidTarget)
    {
        _move = move;
    }

    public override void ApplyOnTarget(Targetable target)
    {
        base.ApplyOnTarget(target);
        _move(target);
    }

    protected override void Complete()
    {
    }
}
