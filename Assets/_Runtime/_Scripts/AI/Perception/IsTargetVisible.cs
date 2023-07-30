using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : ConditionBase
{

    [InParam("Target")]
    private Transform target;
    [InParam("Muzzle")]
    private Transform muzzle;

    [InParam("FieldOfView")]
    private FieldOfView field;

    private float maxDurationToForgetTarget = 5f;


    private Vector3 TargetPosition => target.position;
    private Vector3 MuzzlePosition => muzzle.position;

    private float timeAtlastFrameInField = Mathf.NegativeInfinity;

    public override bool Check()
    {
        var isInField = field.IsInsideFieldOfView2D(target);
        if(isInField)
        {
            timeAtlastFrameInField = Time.time;
        }

        return isInField || Time.time < timeAtlastFrameInField + maxDurationToForgetTarget;
    }

    public override void OnAbort()
    {
        base.OnAbort();
        timeAtlastFrameInField = 0;
    }

}
