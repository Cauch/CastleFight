using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPierce : IOffensiveModifier
{
    float armorPierce;

    public ArmorPierce(float armorPierce)
    {
        this.armorPierce = armorPierce;
    }

    public Attack ModifyAttack(Attack attack)
    {
        return attack;
    }

    public float ModifyDefense(float armor)
    {
        return armor* (1- armorPierce);
    }
}
