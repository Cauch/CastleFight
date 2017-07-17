using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour {
    public bool isSelected = false;
    public bool isActive = false;
    public GameObject uiPanel;
    public bool allegiance;
}
