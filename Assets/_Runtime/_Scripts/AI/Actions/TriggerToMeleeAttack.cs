using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

[Action("Game/Attacks/TriggerToMeleeAttack")]
public class TriggerToMeleeAttack : BasePrimitiveAction
{
    [InParam("WeaponAttack")]
    private WeaponAttack weaponAttack;

    [InParam("AiController")]
    private EnemyIAController AiController;

    public override void OnStart()
    {
        Assert.IsNotNull(weaponAttack);

    }

    public override TaskStatus OnUpdate()
    {
        TaskStatus currentTaskStatus = TaskStatus.RUNNING; 

        AiController.input = Vector3.zero;
        weaponAttack.OnAttackWeapon();

        return currentTaskStatus;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override void OnAbort()
    {
        base.OnAbort();
    }

}
