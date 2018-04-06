using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : Targetable {
    public float MaxDecompositionTime = 20f;
    public float DecompositionTime = 10f;
    public float DecompositionRate = 1f;

    public GameObject Model;
    public Unit Unit;

	// Use this for initialization
	void Awake () {
        Unit = GetComponent<Unit>();
        PanelType = PanelType.CORPSE;
	}
	
	// Update is called once per frame
	new protected void Update () {
        base.Update();
        DecompositionTime -= Time.deltaTime * DecompositionRate;

        if(DecompositionTime < 0)
        {
            Decompose();
        }
	}

    private void OnEnable()
    {
        //ChangeModel();
        DecompositionTime = MaxDecompositionTime;
    }

    private void ChangeModel()
    {
        if (Unit)
        {
            Model.SetActive(true);
            Unit.Model.SetActive(false);
        }
    }

    protected virtual void Decompose()
    {
        Destroy(this.gameObject, 0.01f);
    }

    public void Resurrect()
    {
        Unit.enabled = true;
        //Temp color
        Unit.AdjustStart();
        this.enabled = false;
    }
}
