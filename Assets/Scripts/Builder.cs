using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Builder : Selectable {
    const int RIGHT_CLICK = 1;

    public List<GameObject> buildings;
    public float incomeTime;

    List<GameObject> ownedBuildings;
    float cooldown;
    uint money;
    float income;

    NavMeshAgent navMesh;
    Collider planeCollider;
    Vector3 destination;


    public bool EnoughMoney(uint cost)
    {
        return cost < money;
    }

    public void LosteMoney(uint cost)
    {
        money -= cost;
    }

    private void Start()
    {
        planeCollider = GameObject.FindGameObjectWithTag("Plane").GetComponent<Collider>();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = 1000;
        uiPanel.GetComponent<UIBuilderManager>().builder = this;
        this.ownedBuildings = new List<GameObject>();
        this.money = 100;
        this.income = 10f;
        this.cooldown = incomeTime;
        destination = this.transform.position;
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

        if(Input.GetMouseButtonDown(RIGHT_CLICK)) {
            GetDestination();
            Move(destination);
        }
       
    }

    void GetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (planeCollider.Raycast(ray, out hitInfo, 1000f))
        {
            destination = hitInfo.point;
        }
    }

    void Move(Vector3 position)
    {
        navMesh.SetDestination(position);
    }

    public GameObject InstantiateBuilding(GameObject building)
    {
        GameObject b = Instantiate(building);
        ownedBuildings.Add(b);
        Building buildingComponent = b.GetComponent<Building>();
        buildingComponent.creator = this;
        buildingComponent.AdjustStart();
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
