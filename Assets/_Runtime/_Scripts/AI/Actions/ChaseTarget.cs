using System.Collections;
using System.Collections.Generic;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;


[Action("Game/ChaseTarget")]
public class ChaseTarget : GOAction
{

    [InParam("Target")]
    private Transform target;
    [InParam("AiController")]
    private EnemyIAController enemyIAController;
    

    public override void OnStart()
    {
        base.OnStart();
        Debug.Log("OnStart: ChaseTarget");

    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log("OnUpdate: ChaseTarget");
        var toTargetX = Mathf.Sign((target.position.x - gameObject.transform.position.x));
        Debug.Log(toTargetX);
        enemyIAController.input = new Vector3(toTargetX, 0, 0);

        return TaskStatus.RUNNING;
    }

    public override void OnAbort()
    {
        base.OnAbort();
    }


}
