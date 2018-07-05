using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingCost {
    bool CanBePurchasedWith(IEnumerable<Resource> resources);
    void PurchaseWith(IEnumerable<Resource> resources);
    IEnumerable<Resource> GetResources();
    void Init();
}
