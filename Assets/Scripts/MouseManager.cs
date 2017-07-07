using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class MouseManager : MonoBehaviour {
    enum PANELS { UNIT, BUILDING, BUILDER};

    public GameObject hoveredObject;
    public GameObject selectedObject;
    GameObject previousSelectedObject;
    public Color previousHoveredColor;
    public Color previousSelectedColor;
    public GameObject thrashCode;
    public UIManager uiManager;

    Dictionary<PANELS, GameObject> panels;

    public List<GameObject> panelList;

    const int UI_POINTER = -1;

    // Use this for initialization
    void Start () {
        panels = new Dictionary<PANELS, GameObject>();

        panels.Add(PANELS.UNIT, panelList[0]);
        panels.Add(PANELS.BUILDING, panelList[1]);
        panels.Add(PANELS.BUILDER, panelList[2]);
    }

    // Update is called once per frame
    void Update () {
        previousSelectedObject = selectedObject;
        if (EventSystem.current.IsPointerOverGameObject(UI_POINTER)) {
            
        } else {
            LookForHover();
            LookForSelect();
        }
        if(selectedObject == previousSelectedObject) { } else
        {
            UpdatePanel();
        }
    }

    void LookForHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            HoverObject(hitObject);
        }
        else
        {
            Clear(ref hoveredObject);
        }
    }
    
    void LookForSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == hoveredObject)
            {
                return;
            }

            Clear(ref selectedObject);

            Selectable obj = hoveredObject.GetComponent<Selectable>();
            if ( obj != null)
            {
                obj.isSelected = true;
                selectedObject = hoveredObject;
            }
        }
    }

    void HoverObject(GameObject selection)
    {
        hoveredObject = selection;
        // Mouse becomes green?
    }

    void Clear(ref GameObject obj)// Rename?
    {
        if (obj == null) return;
        
        Selectable s = obj.GetComponent<Selectable>();
        if (s != null) s.isSelected = false;
        obj = null;
    }

    void UpdatePanel()
    {
        // FIND BETTER SOLUTION

        GameObject activePanel = null;

        if (selectedObject == previousSelectedObject) return;
        if (selectedObject == null)
        {
            panels.TryGetValue(PANELS.BUILDER, out activePanel);
        }
        else { 
            
            if(selectedObject.GetComponent<Unit>() != null)
            {
                panels.TryGetValue(PANELS.UNIT, out activePanel);
                activePanel.GetComponent<UIUnitManager>().unit = selectedObject.GetComponent<Unit>();
            } else if (selectedObject.GetComponent<Building>() != null)
            {
                panels.TryGetValue(PANELS.BUILDING, out activePanel);
                //activePanel.GetComponent<UIBuildingManager>().unit = selectedObject.GetComponent<Building>();
            }
        }
        uiManager.ReplacePanel(activePanel);
    }

    //Color SetColor(GameObject obj, Color c)
    //{
    //    Color rc = Color.white;
    //    //Renderer[] rs = obj.GetComponentsInChildren<Renderer>(); // To put in utility
    //    //foreach (Renderer r in rs)
    //    //{
    //    //    Material m = r.material;
    //    //    rc = m.color;
    //    //    m.color = c;
    //    //}

    //    return rc;
    //}

    //void ClearHover(ref GameObject obj, ref Color c) // Rename?
    //{
    //    if (obj == null) return;
    //    else
    //    {
    //        if(hoveredObject != selectedObject)
    //        {
    //            SetColor(obj, c);
    //            c = Color.white; // Useless
    //        }
    //    }
    //    obj = null;
    //}

    //void ClearSelection(ref GameObject obj, ref Color c) // Rename?
    //{
    //    if (obj == null) return;
    //    else
    //    {
    //        SetColor(obj, c);
    //        c = Color.white; // Useless
    //    }
    //    obj = null;
    //}

    //void LookForSelect()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        if(selectedObject == hoveredObject)
    //        {
    //            return;
    //        }

    //        Clear(ref selectedObject);
    //        selectedObject = hoveredObject;

    //        //ClearSelection(ref selectedObject, ref previousSelectedColor);


    //        //selectedObject = hoveredObject;
    //        //previousSelectedColor = previousHoveredColor;
    //        //SetColor(selectedObject, Color.yellow);
    //    }
    //}

    //void HoverObject(GameObject selection)
    //{
    //    if(hoveredObject != null)
    //    {



    //        if (hoveredObject == selection)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            Clear(ref hoveredObject);
    //            //ClearHover(ref hoveredObject, ref previousHoveredColor);
    //        }
    //    }

    //    hoveredObject = selection;

    //    if (selectedObject != selection)
    //    {
    //        previousHoveredColor = SetColor(hoveredObject, Color.green);
    //    }
    //}
}
