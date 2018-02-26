using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier {
    Attack ModifyAttack(Attack attack);
    float ModifyDefense(float armor);
}
