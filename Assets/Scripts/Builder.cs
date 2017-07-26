using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Selectable {
    public List<GameObject> buildings;
    public float incomeTime;

    List<GameObject> ownedBuildings;
    float cooldown;
    uint money;
    float income;

    public bool EnoughMoney(uint cost)
    {
        return cost < money;
    }

    public void LosteMoney(uint cost)
    {
        money -= cost;
    }

    new private void Start()
    {
        base.Start();
        uiPanel.GetComponent<UIBuilderManager>().builder = this;
        this.ownedBuildings = new List<GameObject>();
        this.money = 100;
        this.income = 10f;
        this.cooldown = incomeTime;
    }

    protected void Update()
    {
        cooldown -= Time.deltaTime;
        //UpdateIncome();
        if (cooldown < 0)
        {
            cooldown = incomeTime; // Observer incomeTime?
            UpdateMoney();
        }
    }

    public GameObject InstantiateBuilding(GameObject building)
    {
        GameObject b = Instantiate(building);
        ownedBuildings.Add(b);
        income += b.GetComponent<Building>().income;
        return b;
    }

    public void DestroyBuilding(GameObject building)
    {
        ownedBuildings.Remove(building);
        Destroy(building);
        income -= ownedBuildings[ownedBuildings.Count - 1].GetComponent<Building>().income;
    }

    //void UpdateIncome()
    //{
    //    float income = 0;
    //    foreach(GameObject b in buildings)
    //    {
    //        income += b.GetComponent<Building>().income;
    //    }
    //    foreach (Building b in ownedBuildings)
    //    {
    //        income += b.income;
    //    }
    //    income = Mathf.Clamp(income, 0, Mathf.Infinity);
    //}

    void UpdateMoney()
    {
        money += (uint)income;
    }
}
