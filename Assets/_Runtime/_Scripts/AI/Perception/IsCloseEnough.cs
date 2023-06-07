using System.Collections;
using System.Collections.Generic;
using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;


[Condition("Game/Perception/IsCloseEnough")]
public class IsCloseEnough : GOCondition
{

    [InParam("FieldOfView")]
    private FieldOfView fieldOfView;
    [InParam("Target")]
    private Transform target;


    [InParam("DistanceToConsiderCloseEnough")]
    private float distanceCloseEnough;

    public override bool Check()
    {
        var myPosition = gameObject.transform.position;
        var distanceQuadratic = distanceCloseEnough * distanceCloseEnough;
        var isCloseEnoughToTarget = (target.position - myPosition).sqrMagnitude < distanceQuadratic;
        var isFacingAtTarget = fieldOfView.IsFacingAtTarget(target, gameObject.transform);
        Debug.Log($"Check: IsCloseEnough: {isCloseEnoughToTarget && isFacingAtTarget}");

        return isCloseEnoughToTarget && isFacingAtTarget;
    }



    public override void OnAbort()
    {
        base.OnAbort();
        
    }

}
