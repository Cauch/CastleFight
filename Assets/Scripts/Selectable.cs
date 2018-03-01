using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour {
    public bool IsSelected = false;
    public bool IsActive = true;
    public bool Allegiance;
    public PanelType PanelType;

    protected virtual void Start()
    {
        
    }
}
