using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HalfOnesBuildingCost : MonoBehaviour, IBuildingCost {

    public uint BiomassCost;

    IResource _biomass;

    List<IResource> _resources;

    public void Init()
    {
        _biomass = new ResourceBiomass(BiomassCost);
        _resources = new List<IResource> { _biomass };
    }

    public bool CanBePurchasedWith(IEnumerable<IResource> resources)
    {
        return resources.OfType<ResourceBiomass>().First().CanPurchase(_biomass);
    }

    public void PurchaseWith(IEnumerable<IResource> resources)
    {
        resources.OfType<ResourceBiomass>().First().Purchase(_biomass);
    }

    public IEnumerable<IResource> GetResources()
    {
        return _resources;
    }
}
