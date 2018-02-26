using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {
    const int BATTLE_FIELD_LAYER_MASK = 8;

    public float range;
    public Func<Attackable, bool> isValidTarget;

    public Skill(float range, Func<Attackable, bool> isValidTarget)
    {
        this.range = range;
        this.isValidTarget = isValidTarget;
    }
    public abstract void ApplyOnTarget(Attackable target);

    public bool IsUsable(Attackable attacker, Attackable target)
    {
        return (IsMelee(attacker, target) || IsInRange(attacker, target)) && isValidTarget(target);
    }

    bool IsMelee(Attackable attacker, Attackable target)
    {
        Bounds aBounds = attacker.GetComponent<Collider>().bounds;
        Bounds tBounds = target.GetComponent<Collider>().bounds;

        return tBounds.Intersects(aBounds);
    }

    bool IsInRange(Attackable attacker, Attackable target)
    {
        return (target.transform.position - attacker.transform.position).sqrMagnitude < range * range;
        //RaycastHit hitInfo;
        //if(Physics.Raycast(attacker.transform.position, target.transform.position - attacker.transform.position, out hitInfo, this.range, BATTLE_FIELD_LAYER_MASK))
        //{
        //    return hitInfo.transform.GetInstanceID() == target.transform.GetInstanceID();
        //}

        //return false;
    }
}
