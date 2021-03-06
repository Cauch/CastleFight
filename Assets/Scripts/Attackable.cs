﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Attackable : Targetable {
    public Vector3 FloatingTextOffSet = new Vector3(0, 10f, 0);

    public float Hp;
    public float MaxHp;
    public float ArmorMods;
    public float BaseArmor;
    public int CreatorId;

    public List<IDefensiveModifier> defensiveModifiers;

    public float Armor {
        get { return BaseArmor + ArmorMods; }
        private set { throw new System.Exception(); }
    }

    public void AddHp(float hp)
    {
        if (GameSettings.FloatingTextOn)
        {
            HpTextHook(hp);
        }

        this.Hp = Mathf.Min(this.Hp + hp, MaxHp);
    }

    public void ModAllegiance(bool allegiance) { this.Allegiance = allegiance; }

    new protected virtual void Start()
    {
        base.Start();
        defensiveModifiers = new List<IDefensiveModifier>();
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

    private void HpTextHook(float hpDiff)
    {
        GameObject go = ObjectPooler.Instance.SpawnFromPool(PooledEnum.FLOATING_TEXT, this.gameObject.transform.position + FloatingTextOffSet, Quaternion.identity);
        FloatingText text = go.GetComponent<FloatingText>();
      
        text.SetText(hpDiff.ToString("F0"), hpDiff < 0 ? Color.red : Color.green);
    }
}
