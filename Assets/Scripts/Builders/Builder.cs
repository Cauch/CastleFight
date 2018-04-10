using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Builder : Selectable {
    //Temporary resource
    public List<GameObject> Buildings;
    public List<IResource> Resources;

    private List<GameObject> _ownedBuildings;

    new protected virtual void Start()
    {
        base.Start();
        this._ownedBuildings = new List<GameObject>();
        PanelType = PanelType.BUILDER;
    }

    public GameObject InstantiateBuilding(GameObject building)
    {
        GameObject newBuilding = Instantiate(building);
        _ownedBuildings.Add(newBuilding);
        return newBuilding;
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
