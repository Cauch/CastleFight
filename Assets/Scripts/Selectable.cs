using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Selectable : NetworkBehaviour{
    public bool IsSelected = false;
    [SyncVar]
    public bool IsActive = true;
    [SyncVar]
    public bool Allegiance;
    public PanelType PanelType;

    protected virtual void Start()
    {
        
    }
}
