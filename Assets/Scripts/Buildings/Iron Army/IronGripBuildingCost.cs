using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IronGripBuildingCost : MonoBehaviour, IBuildingCost
{
    public uint FaithCost;
    public uint IronCost;

    Resource _iron;
    Resource _faith;

    List<Resource> _resources;

    public void Init()
    {
        _iron = new ResourceIron(IronCost);
        _faith = new ResourceFaith(FaithCost);
        _resources = new List<Resource> { _iron, _faith };
    }

    public bool CanBePurchasedWith(IEnumerable<Resource> resources)
    {
        return resources.OfType<ResourceIron>().First().CanPurchase(_iron) &&
            resources.OfType<ResourceFaith>().First().CanPurchase(_faith);
    }

    public void PurchaseWith(IEnumerable<Resource> resources)
    {
        resources.OfType<ResourceIron>().First().Purchase(_iron);
        resources.OfType<ResourceFaith>().First().Purchase(_faith);
    }

    public IEnumerable<Resource> GetResources()
    {
        return _resources;
    }
}
