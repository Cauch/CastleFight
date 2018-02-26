using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveSkill : Skill
{
    public PassiveSkill(float range, Func<Attackable, bool> isValidTarget) : base(range, isValidTarget)
    {
    }
}
