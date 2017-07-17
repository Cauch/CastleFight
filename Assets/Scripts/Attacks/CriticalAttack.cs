using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalAttack : Attack {
    public float critChance;
    public float critMultiplier;

    protected override int CalculateNegativeDmg()
    {
        if (Random.Range(0, 1) < critChance) { return Mathf.FloorToInt(-dmg * critMultiplier); }
        return -dmg;
    }
}
