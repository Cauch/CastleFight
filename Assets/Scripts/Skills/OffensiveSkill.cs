using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffensiveSkill : Skill {
    public OffensiveSkill(float initCd, float usePerSecond, float range, float executionTime) : base(initCd, usePerSecond, range, executionTime) { }
}
