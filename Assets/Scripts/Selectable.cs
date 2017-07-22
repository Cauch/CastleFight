using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour {
    public bool isSelected = false;
    public bool isActive = false;
    public GameObject uiPanel;
    public bool allegiance;

    protected virtual void Start()
    {
        // This is a dumb solution... think I should just save three panel, not duplicate it and have an ugly switch case / cascade if in UIManager
        uiPanel = Instantiate(uiPanel);
    }
}
