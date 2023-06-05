using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float acceleration = 70f;
    private Vector3 Velocity;
    private Vector3 targetVelocity;


    public float percentVelocityX => Mathf.Clamp01(Mathf.Abs(Velocity.x));
    

    public void SetInput(Vector3 input)
    {

        targetVelocity = input * speed * Time.deltaTime;
    }

    public void UpdateMovement()
    {
        Velocity = Vector3.MoveTowards(Velocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        var lastPosition = transform.position;
        var targetPosition = lastPosition + Velocity * Time.fixedDeltaTime;
        transform.position = targetPosition;
    }


}
