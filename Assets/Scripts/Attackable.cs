using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attackable : Selectable {
    public float Hp;
    public float MaxHp;
    public float ArmorMods;
    public float BaseArmor;
    public Builder Creator;
    public Builder Attacker;

    private List<Effect> effects;
    public List<IDefensiveModifier> defensiveModifiers;

    public float Armor {
        get { return BaseArmor + ArmorMods; }
        private set { throw new System.Exception(); }
    }

    public void ModHp(float hp){ this.Hp = Mathf.Min(this.Hp + hp, MaxHp); }
    public void ModAllegiance(bool allegiance) { this.Allegiance = allegiance; }

    new protected virtual void Start()
    {
        base.Start();
        defensiveModifiers = new List<IDefensiveModifier>();
        effects = new List<Effect>();
    }

    protected void Update()
    {
        if(Hp <= 0)
        {
            //Delayed destroy, because someobjects still use it. Navmesh problem Possible other solution
            Destroy(this.gameObject, 0.01f);
        } else
        {
            if(Hp > MaxHp)
            {
                Hp = MaxHp;
            }
        }

        RefreshEffects();
        TriggerEffects();
    }

    void TriggerEffects()
    {
        //Tick time should be in this class, for cacheline reason ?
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].tickTime < 0)
            {
                effects[i].OnTick(this);
                effects[i].tickTime = 1;
            }
        }
    }
    
    void RefreshEffects()
    {
        List<Effect> effectToRemove = new List<Effect>();
        foreach(Effect effect in effects)
        {
            effect.tickTime -= effect.tickSpeed * Time.deltaTime;
            effect.appliedTime += Time.deltaTime;
            if(effect.duration < effect.appliedTime)
            {
                effect.OnRemove(this);
                effectToRemove.Add(effect);
            }
        }

        foreach (Effect effect in effectToRemove)
        {
            effects.Remove(effect);
        }
    }

    public void AddEffect(Effect effect)
    {
        if(AlreadyAffected(effect))
        {
           
        }
        else
        {
            effect.OnApply(this);
            effects.Add(effect);
        }
    }

    public bool AlreadyAffected(Effect effect)
    {
        return effects.Where(t => t.GetType() == effect.GetType()).Any();
    }
}
