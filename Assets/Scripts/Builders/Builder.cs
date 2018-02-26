using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Selectable {
    //Temporary resource
    public List<GameObject> Buildings;
    List<GameObject> _ownedBuildings;

    public List<IResource> Resources;

    new protected virtual void Start()
    {
        base.Start();
        uiPanel.GetComponent<UIBuilderManager>().Builder = this;
        this._ownedBuildings = new List<GameObject>();
    }

    protected void Update()
    {
    }

    public GameObject InstantiateBuilding(GameObject building)
    {
        GameObject b = Instantiate(building);
        _ownedBuildings.Add(b);
        return b;
    }

    public void DestroyBuilding(GameObject building)
    {
        _ownedBuildings.Remove(building);
        Destroy(building);
    }

    public bool CanPayBuilding(IBuildingCost buildingCost)
    {
        return buildingCost.CanBePurchasedWith(Resources);
    }

    public void PayBuilding(IBuildingCost buildingCost)
    {
        buildingCost.PurchaseWith(Resources);
    }

}
