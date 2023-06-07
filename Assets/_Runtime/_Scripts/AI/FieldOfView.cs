using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{  
    [SerializeField] private Transform player;
    [SerializeField] private float angleDegress = 45f;
    [SerializeField] private float viewDistance = 4;
    [SerializeField] private Transform muzzle;

    private Vector3 muzzlePosition => muzzle.position;

    public bool IsInsideFieldOfView2D(Transform target)
    {
        if(target == null) return false;

        var toTarget = target.position - muzzlePosition;
        var toTargetSqrtMagnitude = toTarget.sqrMagnitude;
        var quadraticViewDistance = viewDistance * viewDistance;

        if(toTargetSqrtMagnitude > quadraticViewDistance) return false;

        var dot = Vector2.Dot(toTarget, transform.right);
        if(dot < 0) return false;



        var cosHalfAngleToTarget = dot / (toTarget.magnitude * transform.forward.magnitude);
        var halfAngleToTarget = Mathf.Acos(cosHalfAngleToTarget) * Mathf.Rad2Deg;



        return halfAngleToTarget <= (angleDegress * 0.5f);
    }


    public bool IsFacingAtTarget(Transform target, Transform myTransform)
    {
        if(target == null) return false;
        
        var toTarget = target.position - myTransform.position;

        var dot = Vector3.Dot(toTarget, myTransform.right);

        return dot > 0;
    }

    private void OnDrawGizmos() 
    {
        //TODO: 
        bool IsInside = IsInsideFieldOfView2D(player);    
        var halfAngle = angleDegress * 0.5f;

        Gizmos.DrawWireSphere(muzzlePosition, viewDistance);

        
        var upDirection = Quaternion.Euler(0, 0, halfAngle) * muzzle.right;
        var downDirection = Quaternion.Euler(0, 0, -halfAngle) * muzzle.right;

        Gizmos.color = IsInside ? Color.green : Color.red;
        Gizmos.DrawRay(muzzlePosition, upDirection * viewDistance);
        Gizmos.DrawRay(muzzlePosition, downDirection * viewDistance);

    }

}
