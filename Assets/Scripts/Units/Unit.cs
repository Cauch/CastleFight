using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable {
    Attackable enemyCastle;
    Attackable target;
    float attackCooldown;
    public float attackSpeed;
    public float dammage;
    public float detectionRange;
    public float defaultAttackRange;

    public float defaultSpeed;
    protected float speedModifier;

    List<Attackable> enemies;

    new protected void Start()
    {
        uiPanel.GetComponent<UIUnitManager>().unit = this;
        target = enemyCastle;
        enemies = new List<Attackable>();
    }

    public void AdjustStart()
    {
        this.allegiance = this.creator.allegiance;
        this.enemyCastle = this.allegiance == false ? GameObject.FindGameObjectWithTag("Castle1").GetComponent<Attackable>() : GameObject.FindGameObjectWithTag("Castle0").GetComponent<Attackable>();

    }

    protected virtual new void Update()
    {
        base.Update();
        if (isActive)
        {
            attackCooldown -= Time.deltaTime * attackSpeed;

            DetectEnemies();
            if (!AttackTarget()) {
                Move(target);
            }
        }
    }

    void Activate()
    {
        isActive = true;
    }

    protected abstract void Move(Attackable target);

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

    bool AttackTarget()
    {
        bool targetInRange = (target.transform.position - this.transform.position).sqrMagnitude <= defaultAttackRange;
        if (this.attackCooldown <= 0 && targetInRange)
        {
            attackCooldown = 1;
            target.hp -= this.dammage;
        }
        return targetInRange;
    }
}
