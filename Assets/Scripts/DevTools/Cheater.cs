using System;
using UnityEngine;

public class Cheater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKey(KeyCode.K))
        {
            KillAll(TargetingFunction.IsUnit);
        }

	}

    void KillAll(Func<Attackable, bool> MustKill)
    {
        foreach(Attackable a in FindObjectsOfType<Attackable>())
        {
            if(MustKill(a))
            {
                a.Hp = 0;
            }
        }
    }

}
