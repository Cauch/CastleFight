using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attackable : Selectable {
    public float hp;
    public float maxHp;
    public float armor;
    public bool allegiance;

    public void ModHp(float hp){ this.hp += hp; }
    public void ModArmor(float armor) { this.armor += armor; }
    public void ModAllegiance(bool allegiance) { this.allegiance = allegiance; }

    protected void Update()
    {
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        } else
        {
            if(hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }
}
