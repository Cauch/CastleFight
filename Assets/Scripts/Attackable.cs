using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attackable : Selectable {
    public float hp;
    public float maxHp;
    public float armor;
    public float goldWorth;
    public Builder creator;
    public Builder attacker;

    public List<Effect> effects;

    public void ModHp(float hp){ this.hp += hp; }
    public void ModArmor(float armor) { this.armor += armor; }
    public void ModAllegiance(bool allegiance) { this.allegiance = allegiance; }

    new protected virtual void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        if(hp <= 0)
        {
            //Delayed destroy, because someobjects still use it. Navmesh problem Possible other solution
            Destroy(this.gameObject, 0.01f);
        } else
        {
            if(hp > maxHp)
            {
                hp = maxHp;
            }
        }

        RefreshEffects();
        TriggerEffects();
    }

    void TriggerEffects()
    {
        //Tick time should be in this class, for cacheline reason ?
        foreach (Effect effect in effects)
        {
            if (effect.tickTime < 0)
            {
                effect.OnTick(this);
                effect.tickTime = 1;
                effect.OnRemove(this);
                effects.Remove(effect);
            }
        }
    }
    
    void RefreshEffects()
    {
        foreach(Effect effect in effects)
        {
            effect.tickTime -= effect.tickSpeed * Time.deltaTime;
            effect.appliedTime += Time.deltaTime;
            if(effect.duration < effect.appliedTime)
            {
                effect.OnRemove(this);
                effects.Remove(effect);
            }
        }
    }
}
