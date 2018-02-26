using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable {
    // Private attributes
    const float BASE_GLOBAL_COOLDOWN = 0.2f;
    const float ANIMATOR_SPEED_MODIFIER = 0.02f;

    //Animator animator;

    public Attackable EnemyCastle;

    // Protected Attributes
    protected float _speedModifier;
    protected Skill[] _skills;

    public float DefaultSpeed;
    public float DetectionRange;
    protected float _generalColdown;

    // Abstract Methods
    protected abstract void Move(Attackable target);
    protected abstract void StopMoving();
    
    // Private and protected methods
    new protected void Start()
    {
        base.Start();
        //animator = this.GetComponent<Animator>();
        
        uiPanel.GetComponent<UIUnitManager>().unit = this;
    }

    protected virtual new void Update()
    {
        base.Update();
        if (isActive)
        {
            bool isAttacking = UseSkill();
            //animator.SetBool("isAttacking", isAttacking);

            if (isAttacking)
            {
                RefreshCooldown();
                StopMoving();
            } else
            {
                Move(TargetingFunction.GetClosestEnemy(this));
            }     
        }
    }

    void Activate()
    {
        isActive = true;
    }

    void RefreshCooldown()
    {
        foreach (ActiveSkill skill in _skills)
        {
            skill.cooldown -= (Time.deltaTime * skill.skillRefreshSpeed);
        }
    }

    //Public methods
    //Find root cause and change
    public void AdjustStart()
    {
        this.allegiance = this.Creator.allegiance;
        this.EnemyCastle = this.allegiance == false ? GameObject.FindGameObjectWithTag("Castle1").GetComponent<Attackable>() : GameObject.FindGameObjectWithTag("Castle0").GetComponent<Attackable>();
    }

    //Returns true if a skill is in use
    public abstract bool UseSkill();
}
