using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IronGripBuildingCost : MonoBehaviour, IBuildingCost
{
    public uint FaithCost;
    public uint IronCost;

    IResource _iron;
    IResource _faith;

    List<IResource> _resources;

    public void Init()
    {
        _iron = new ResourceIron(IronCost);
        _faith = new ResourceFaith(FaithCost);
        _resources = new List<IResource> { _iron, _faith };
    }

    public bool CanBePurchasedWith(IEnumerable<IResource> resources)
    {
        return resources.OfType<ResourceIron>().First().CanPurchase(_iron) &&
            resources.OfType<ResourceFaith>().First().CanPurchase(_faith);
    }

    public void PurchaseWith(IEnumerable<IResource> resources)
    {
        resources.OfType<ResourceIron>().First().Purchase(_iron);
        resources.OfType<ResourceFaith>().First().Purchase(_faith);
    }

    public IEnumerable<IResource> GetResources()
    {
        return _resources;
    }
}
