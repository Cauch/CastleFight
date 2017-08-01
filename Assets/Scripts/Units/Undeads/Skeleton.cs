using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : GroundUnit
{
    new private void Start()
    {
        base.Start();
        offensiveSkills = new[] { new Attack(0.15f, 0.5f, 100, 0.5f, 25) };
        defensiveSkills = new Skill[] { };
    }
}
