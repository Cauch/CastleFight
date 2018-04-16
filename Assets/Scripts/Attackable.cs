using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Attackable : Targetable {
    public float Hp;
    public float MaxHp;
    public float ArmorMods;
    public float BaseArmor;
    public Builder Creator;
    public int CreatorId;

    public List<IDefensiveModifier> defensiveModifiers;

    public float Armor {
        get { return BaseArmor + ArmorMods; }
        private set { throw new System.Exception(); }
    }

    public void AddHp(float hp){ this.Hp = Mathf.Min(this.Hp + hp, MaxHp); }
    public void ModAllegiance(bool allegiance) { this.Allegiance = allegiance; }

    new protected virtual void Start()
    {
        base.Start();
        defensiveModifiers = new List<IDefensiveModifier>();
        CreatorId = Creator.GetInstanceID();
    }

    new protected void Update()
    {
        base.Update();
        if(Hp <= 0)
        {
            //Delayed destroy, because someobjects still use it. Navmesh problem Possible other solution
            Die();
        } else
        {
            if(Hp > MaxHp)
            {
                Hp = MaxHp;
            }
        }
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject, 0.01f);
    }
}
