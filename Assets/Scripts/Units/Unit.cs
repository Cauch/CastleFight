using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable {
    // Private attributes
    const float BASE_GLOBAL_COOLDOWN = 0.2f;
    const float ANIMATOR_SPEED_MODIFIER = 0.02f;

    Animator animator;

    Attackable enemyCastle;
   
    // Protected Attributes
    protected float speedModifier;
    protected Skill[] offensiveSkills;
    protected Skill[] defensiveSkills;

    public float defaultSpeed;
    public float detectionRange;


    // Abstract Methods
    protected abstract void Move(Attackable target);
    protected abstract void StopMoving();
    
    // Private and protected methods
    new protected void Start()
    {
        base.Start();
        animator = this.GetComponent<Animator>();
        uiPanel.GetComponent<UIUnitManager>().unit = this;
    }

    protected virtual new void Update()
    {
        base.Update();
        if (isActive)
        {
            Attackable target = GetClosestEnemy();
            bool isAttacking = UseOffensiveSkill(target);
            animator.SetBool("isAttacking", isAttacking);

            if (isAttacking)
            {
                RefreshCooldown();
                StopMoving();
            } else
            {
                Move(target);
            }     
        }
    }

    void Activate()
    {
        isActive = true;
    }

    List<Attackable> DetectEnemies()
    {
        Collider[] enemiesCollider = Physics.OverlapSphere(this.transform.position, detectionRange);
        List<Attackable> enemies = new List<Attackable>{ };

        foreach(Collider c in enemiesCollider)
        {
            if(ColliderIsEnemy(c))
            {
                enemies.Add(c.gameObject.GetComponent<Attackable>());
            }
        }

        return enemies;
    }

    bool ColliderIsEnemy(Collider c)
    {
        Attackable a = c.gameObject.GetComponent<Attackable>();
        if (a == null) return false;

        return a.allegiance != this.allegiance && a.isActive;
    }

    Attackable GetClosestEnemy()
    {
        Attackable target = this.enemyCastle;

        float closestDistanceSqr = detectionRange;

        foreach (Attackable potentialTarget in DetectEnemies())
        {
            Vector3 directionToTarget = potentialTarget.transform.position - this.transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject != this)
            {
                closestDistanceSqr = dSqrToTarget;
                target = potentialTarget;
            }
        }

        return target;
    }

    //Rework as to have combat engagement system?
    bool UseOffensiveSkill(Attackable target)
    {
        bool atLeastOneTargetInRange = false;
        foreach (OffensiveSkill skill in offensiveSkills)
        {
            bool targetInRange = IsUsable(skill, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange ;
            if(targetInRange)
            {
                if (skill.cooldown <= 0)
                {
                    UseSkill(skill, target);
                    return targetInRange;
                }
            }
        }

        return atLeastOneTargetInRange;
    }

    bool IsUsable(Skill skill, Attackable target)
    {
        return skill.IsUsable(this, target);
    }

    void UseSkill(Skill skill, Attackable target)
    {
        skill.ApplyOnTarget(target);

        skill.cooldown = 1.0f;
        animator.SetFloat("attackSpeed", skill.skillRefreshSpeed);
    }


    void RefreshCooldown()
    {
        foreach (Skill skill in offensiveSkills)
        {
                skill.cooldown -= (Time.deltaTime * skill.skillRefreshSpeed);
        }

        foreach (Skill skill in defensiveSkills)
        {
            skill.cooldown -= (Time.deltaTime * skill.skillRefreshSpeed);
        }
    }

    //Punlic methods
    public void AdjustStart()
    {
        this.allegiance = this.creator.allegiance;
        this.enemyCastle = this.allegiance == false ? GameObject.FindGameObjectWithTag("Castle1").GetComponent<Attackable>() : GameObject.FindGameObjectWithTag("Castle0").GetComponent<Attackable>();
    }
}
