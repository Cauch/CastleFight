using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {
    public float cooldown;
    public float skillRefreshSpeed;
    public float range;

    public abstract void ApplyOnTarget(Attackable target);

}
