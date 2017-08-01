using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : GroundUnit {
    new private void Start()
    {
        base.Start();
        offensiveSkills =  new []{ new Attack(0.5f, 1.0f, 100, 1.0f, 10) };
        defensiveSkills = new Skill[] { };
    }
}
