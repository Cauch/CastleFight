using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingCost {
    bool CanBePurchasedWith(IEnumerable<IResource> resources);
    void PurchaseWith(IEnumerable<IResource> resources);
    IEnumerable<IResource> GetResources();
    void Init();
}
