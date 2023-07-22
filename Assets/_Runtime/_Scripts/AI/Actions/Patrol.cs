using System.Collections;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/Patrol")]
public class Patrol : BasePrimitiveAction
{

    [InParam("AiController")]
    private EnemyIAController enemyIAController;

    [InParam("HasPlatformInFront")]
    private HasPlatformInFront hasPlatformInFront;

    private Vector3 input;

    public override void OnStart()
    {
        base.OnStart();
        input = Vector3.right;

    }

    public override TaskStatus OnUpdate()
    {
        if(!hasPlatformInFront.HasPlatformFront() || hasPlatformInFront.HasWallInFront())
        {
            input *= -1;
        }
        enemyIAController.input = input;

        return TaskStatus.RUNNING;
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
