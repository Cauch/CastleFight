using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System.Linq;

public class MouseManager : MonoBehaviour {
    const int LEFT_CLICK = 0;

    public GameObject DefaultSelection;
    public GameObject HoveredObject;
    public GameObject SelectedObject;

    public UIManager UiManager;

    public Color PreviousHoveredColor;
    public Color PreviousSelectedColor;

    private GameObject _previousSelectedObject;

    const int UI_POINTER = -1;

    // Use this for initialization
    void Start () {
        SelectedObject = _previousSelectedObject = HoveredObject = DefaultSelection;
        UpdatePanel();
    }

    // Update is called once per frame
    void Update () {
        _previousSelectedObject = SelectedObject;
        //Look if selectedObject was destroyed
        if(SelectedObject == null) { SelectedObject = DefaultSelection; }
        if (EventSystem.current.IsPointerOverGameObject(UI_POINTER)) {
            
        } else {
            LookForHover();
            LookForSelect();
        }
        if(SelectedObject != _previousSelectedObject)
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
            Clear(ref HoveredObject);
        }
    }
    
    void LookForSelect()
    {
        if (Input.GetMouseButtonDown(LEFT_CLICK))
        {
            if (SelectedObject == HoveredObject)
            {
                return;
            }

            Clear(ref SelectedObject);

            Selectable obj = HoveredObject.GetComponent<Selectable>();
            if ( obj != null)
            {
                obj.IsSelected = true;
                SelectedObject = HoveredObject;
            }
        }
    }

    void HoverObject(GameObject selection)
    {
        HoveredObject = selection;
        Selectable obj = selection.GetComponent<Selectable>();
    }

    void Clear(ref GameObject obj)// Rename?
    {
        if (obj == null) return;
        
        Selectable s = obj.GetComponent<Selectable>();
        if (s != null) s.IsSelected = false;
        obj = DefaultSelection;
    }

    void UpdatePanel()
    {
        IEnumerable<Selectable> selectables = SelectedObject.GetComponents<Selectable>().Where(s=> s.enabled == true);

        if (selectables.Any())
        {
            UiManager.ReplacePanel(selectables.First());
        }
    }
}
