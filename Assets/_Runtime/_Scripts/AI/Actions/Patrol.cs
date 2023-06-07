using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;


[Action("Game/Patrol")]
public class Patrol : BasePrimitiveAction
{

    [InParam("AiController")]
    EnemyIAController enemyIAController;

    public override void OnStart()
    {
        base.OnStart();
        enemyIAController.StartCoroutine(PerformPatrol());    


    }

    public override TaskStatus OnUpdate()
    {


        return TaskStatus.RUNNING;

    }


    public override void OnEnd()
    {
        base.OnEnd();
        enemyIAController.StopAllCoroutines();
    }

    public override void OnAbort()
    {
        base.OnAbort();
        enemyIAController.StopAllCoroutines();
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
