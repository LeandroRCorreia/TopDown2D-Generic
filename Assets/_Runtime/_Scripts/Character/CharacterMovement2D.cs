using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CheckSurrounds))]
public class CharacterMovement2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private CheckSurrounds checkSurrounds;

    [Header("Ground Movement")]
    [SerializeField] MovementParams movementParams;
    private Vector3 velocity;
    private Vector3 targetVelocity;

    public float PercentToReachMaxVelocity => Mathf.Abs(VelocityX) / movementParams.MaxSpeed;
    public float VelocityX => velocity.x;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;


    public float SpeedY => rb.velocity.y;



    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        checkSurrounds = GetComponent<CheckSurrounds>();
    }

    public void UpdateMovement(Vector3 input)
    {
        SetInput(input);
        Movement();
    }

    public void Jump()
    {
        if(checkSurrounds.IsTouchingBottom && !checkSurrounds.IsTouchingTop)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }

    }

    private void SetInput(Vector3 input)
    {
        input = input.normalized; 
        targetVelocity = input * movementParams.MaxSpeed;
    }
    
    private void Movement()
    {
        velocity = Vector3.MoveTowards(velocity, targetVelocity, movementParams.Acceleration * Time.fixedDeltaTime);
        

        var lastPosition = transform.position;
        var targetPosition = lastPosition + velocity * Time.fixedDeltaTime;
        transform.position = targetPosition;

    }

    public void StopImediatelly()
    {
        velocity = Vector3.zero;
    }

}
