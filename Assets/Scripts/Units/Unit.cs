using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable {
    const float BASE_GLOBAL_COOLDOWN = 0.2f;
    float globalCooldown = BASE_GLOBAL_COOLDOWN;

    Attackable enemyCastle;
    Attackable target;
    public float detectionRange;

    public Skill[] offensiveSkills;
    public Skill[] defensiveSkills;

    public float defaultSpeed;
    protected float speedModifier;

    List<Attackable> enemies;

    new protected void Start()
    {
        base.Start();
        uiPanel.GetComponent<UIUnitManager>().unit = this;
        target = enemyCastle;
        enemies = new List<Attackable>();
        offensiveSkills = this.GetComponents<Skill>();
    }

    public void AdjustStart()
    {
        this.allegiance = this.creator.allegiance;
        this.enemyCastle = this.allegiance == false ? GameObject.FindGameObjectWithTag("EastCastle").GetComponent<Attackable>() : GameObject.FindGameObjectWithTag("WestCastle").GetComponent<Attackable>();

    }

    protected virtual new void Update()
    {
        base.Update();
        if (isActive)
        {
            RefreshCooldown();

            // Any action should reset globalCooldown
            if(globalCooldown <= 0)
            {
                DetectEnemies();
                if (UseOffensiveSkill())
                {
                    StopMoving();
                } else
                {
                    Move(target);
                }
            }      
        }
    }

    void Activate()
    {
        isActive = true;
    }

    protected abstract void Move(Attackable target);
    protected abstract void StopMoving();


    float DetectEnemies()
    {
        Collider[] enemiesCollider = Physics.OverlapSphere(this.transform.position, detectionRange);
        this.enemies.Clear();

        foreach(Collider c in enemiesCollider)
        {
            if(IsEnemy(c))
            {
                enemies.Add(c.gameObject.GetComponent<Attackable>());
            }
        }

        return GetClosestEnemy();
    }

    bool IsEnemy(Collider c)
    {
        Attackable a = c.gameObject.GetComponent<Attackable>();
        if (a == null) return false;

        return a.allegiance != this.allegiance && a.isActive;
    }

    float GetClosestEnemy()
    {
        this.target = this.enemyCastle;
        float closestDistanceSqr = detectionRange;
        Vector3 currentPosition = this.transform.position;
        foreach (Attackable potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject != this)
            {
                closestDistanceSqr = dSqrToTarget;
                this.target = potentialTarget;
            }
        }

        return closestDistanceSqr;
    }

    bool UseOffensiveSkill()
    {
        bool atLeastOneTargetInRange = false;
        foreach (OffensiveSkill skill in offensiveSkills)
        {
            bool targetInRange = (target.transform.position - this.transform.position).sqrMagnitude <= skill.range ;
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange ;
            if (skill.cooldown <= 0 && targetInRange)
            {
                skill.ApplyOnTarget(target);
                skill.cooldown = 1;


                return targetInRange;
            }

            
        }
        return atLeastOneTargetInRange;
    }

    bool UseDefensiveSkill()
    {
        foreach (Skill skill in defensiveSkills)
        {
            bool targetInRange = (target.transform.position - this.transform.position).sqrMagnitude <= skill.range;
            if (skill.cooldown <= 0 && targetInRange)
            {
                skill.ApplyOnTarget(target);
                skill.cooldown = 1;

                globalCooldown = BASE_GLOBAL_COOLDOWN;

                return targetInRange;
            }


        }
        return false;
    }

    void RefreshCooldown()
    {
        foreach (Skill skill in offensiveSkills)
        {
                skill.cooldown -= Time.deltaTime * skill.skillRefreshSpeed;
        }

        foreach (Skill skill in defensiveSkills)
        {
            skill.cooldown -= Time.deltaTime * skill.skillRefreshSpeed;
        }

        globalCooldown -= Time.deltaTime;
    }
}
