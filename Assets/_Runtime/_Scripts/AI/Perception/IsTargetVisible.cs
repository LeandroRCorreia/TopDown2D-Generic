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

    private float maxDurationToForgetTarget = 3f;


    private Vector3 TargetPosition => target.position;
    private Vector3 MuzzlePosition => muzzle.position;


    private bool wasInField = false;
    private float timeAtlastFrameInField = 0;

    public override bool Check()
    {
        var isInField = field.IsInsideFieldOfView2D(target);
        
        if(!isInField && wasInField)
        {   
            return Time.time < timeAtlastFrameInField + maxDurationToForgetTarget;
        }
        else if(isInField)
        {
            timeAtlastFrameInField = Time.time;
            wasInField = isInField;
        }

        return isInField;
    }


    public override void OnAbort()
    {
        base.OnAbort();

        wasInField = false;
        timeAtlastFrameInField = 0;
    }

}
