using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {
    const int BATTLE_FIELD_LAYER_MASK = 8;

    public float cooldown;
    public float skillRefreshSpeed;
    public float range;
    public float executionTime;

    public Skill(float initCd, float usePerSecond, float range, float executionTime)
    {
        this.cooldown = initCd;
        this.skillRefreshSpeed = usePerSecond;
        this.range = range;
        this.executionTime = executionTime;
    }
    public abstract void ApplyOnTarget(Attackable target);

    public bool IsUsable(Attackable attacker, Attackable target)
    {
        return IsMelee(attacker, target) || IsInRange(attacker, target);
    }

    bool IsMelee(Attackable attacker, Attackable target)
    {
        Bounds aBounds = attacker.GetComponent<Collider>().bounds;
        Bounds tBounds = target.GetComponent<Collider>().bounds;

        return tBounds.Intersects(aBounds);
    }

    bool IsInRange(Attackable attacker, Attackable target)
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(attacker.transform.position, target.transform.position - attacker.transform.position, out hitInfo, this.range, BATTLE_FIELD_LAYER_MASK))
        {
            return hitInfo.transform.GetInstanceID() == target.transform.GetInstanceID();
        }

        return false;

    }
}
