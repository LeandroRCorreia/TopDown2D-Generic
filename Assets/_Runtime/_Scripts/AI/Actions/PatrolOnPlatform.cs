using System.Collections;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/PatrolOnPlatform")]
public class PatrolOnPlatform : BasePrimitiveAction
{

    [InParam("AiController")]
    private EnemyIAController enemyIAController;

    [InParam("HasPlatformInFront")]
    private HasPlatformInFront hasPlatformInFront;

    private Vector3 input;

    public override void OnStart()
    {
        base.OnStart();
        input =  Random.value > 0.5f ? Vector3.right : Vector3.left;


    }

    public override TaskStatus OnUpdate()
    {
        CheckInvertInput();

        enemyIAController.input = input;

        return TaskStatus.RUNNING;
    }

    private void CheckInvertInput()
    {
        if (!hasPlatformInFront.HasPlatformFront() || hasPlatformInFront.HasWallInFront())
        {
            input *= -1;
        }
    }


}
