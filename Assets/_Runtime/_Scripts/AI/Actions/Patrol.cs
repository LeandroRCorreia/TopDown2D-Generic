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
            input = input * -1;
        }
        enemyIAController.input =  input;

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
    
    private IEnumerator PerformPatrol()
    {
        var durationPatrol = 2f;
        var durationIdle = 1f;

        while(true)
        {
            yield return MovementBy(Vector3.right, durationPatrol);
            yield return MovementBy(Vector3.zero, durationIdle);
            yield return MovementBy(Vector3.left, durationPatrol);
            yield return MovementBy(Vector3.zero, durationIdle);
        }

    }

    private IEnumerator MovementBy(Vector3 input, float durationMovement)
    {
        var finalTime = Time.time + durationMovement;
        while(Time.time < finalTime)
        {   
            enemyIAController.input = input;
            yield return null;
        }

    }

}
