using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalAttack : Attack {
    protected float critChance;
    protected float critMultiplier;

    public CriticalAttack(float initCd, float usePerSecond, float range, float executionTime, int dmg, float critChance, float critMultiplier) : base(initCd, usePerSecond, range, executionTime, dmg)
    {
        this.critChance = critChance;
        this.critMultiplier = critMultiplier;
    }

    protected override int CalculateNegativeDmg()
    {
        if (Random.Range(0, 1) < critChance) { return Mathf.FloorToInt(-dmg * critMultiplier); }
        return -dmg;
    }
}
