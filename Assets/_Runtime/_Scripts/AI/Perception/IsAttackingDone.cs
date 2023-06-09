using System.Collections;
using System.Collections.Generic;
using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;



[Condition("Game/Perception/IsAttackingDone")]
public class IsAttackingDone : GOCondition
{

    [InParam("WeaponAttack")]
    private WeaponAttack weaponAttack;

    public override bool Check()
    {
        return !weaponAttack.IsAttacking;
    }
}
