using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CastleHelper {
    // Lazy impl when dot net 4
    static Targetable _castle0;
    static Targetable _castle1;
    static Targetable Castle0
    {
        get { if(!_castle0) { _castle0 = GameObject.FindGameObjectWithTag("Castle0").GetComponent<Targetable>(); } return _castle0; }
    }

    static Targetable Castle1
    {
        get { if (!_castle1) { _castle1 = GameObject.FindGameObjectWithTag("Castle1").GetComponent<Targetable>(); } return _castle1; }
    }

    public static Targetable GetCastle(bool allegiance)
    {
        return allegiance ?  Castle1 : Castle0;
    }
}
