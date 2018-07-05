using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HalfOnesBuildingCost : MonoBehaviour, IBuildingCost {

    public uint BiomassCost;

    Resource _biomass;

    List<Resource> _resources;

    public void Init()
    {
        _biomass = new ResourceBiomass(BiomassCost);
        _resources = new List<Resource> { _biomass };
    }

    public bool CanBePurchasedWith(IEnumerable<Resource> resources)
    {
        return resources.OfType<ResourceBiomass>().First().CanPurchase(_biomass);
    }

    public void PurchaseWith(IEnumerable<Resource> resources)
    {
        resources.OfType<ResourceBiomass>().First().Purchase(_biomass);
    }

    public IEnumerable<Resource> GetResources()
    {
        return _resources;
    }
}
