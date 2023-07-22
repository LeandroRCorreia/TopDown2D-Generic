using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IColliderInfo
{

    public Vector3 GetColliderUp {get;}
    public Vector3 GetColliderBottom {get;}

    public Vector3 GetColliderRight {get;}

    public Vector3 GetColliderLeft {get;}

}


[ExecuteAlways]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement2D : MonoBehaviour, IColliderInfo
{
    private CharacterFacing2D characterFacing;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 7;
    [SerializeField] private float acceleration = 70f;
    private Vector3 velocity;
    private Vector3 targetVelocity;
    public float CurrentPercentToReachMaxVelocity => Mathf.Abs(VelocityX) / maxSpeed;
    public float VelocityX => velocity.x;

    //ColliderInfo
    private Collider2D currentCollider;
    public Vector3 ColliderCenter => currentCollider.bounds.center;
    public Vector3 ColliderExtents => currentCollider.bounds.extents;

    private Vector3[] colliderLocalPointsRight;
    private Vector3[] colliderLocalPointsLeft;
    private Vector3[] colLocalPointsBottom;

    public Vector3 GetColliderUp => ColliderCenter + Vector3.up * ColliderExtents.y;

    public Vector3 GetColliderBottom =>  ColliderCenter + Vector3.down * ColliderExtents.y;

    public Vector3 GetColliderRight => ColliderCenter + Vector3.right * ColliderExtents.x;

    public Vector3 GetColliderLeft => ColliderCenter + Vector3.left * ColliderExtents.x;

    //

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] [Range(0.1f, 1f)] private float groundCheckRayCastLenght = 0.25f;

    public float SpeedY => rb.velocity.y;

    public bool IsTouchingWall {get; private set;}
    public bool IsOnGround {get; private set;}


    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        currentCollider = GetComponent<BoxCollider2D>();
        characterFacing = GetComponent<CharacterFacing2D>();
    }

    private void Start()
    {
        StartBoxColliderInfo();
        

    }

    private void StartBoxColliderInfo()
    {
        Vector3 getColliderRightLocalPosition = GetColliderRight - transform.position;
        colliderLocalPointsRight = new Vector3[]
        {
            getColliderRightLocalPosition + Vector3.up * ColliderExtents.y,
            getColliderRightLocalPosition,
            getColliderRightLocalPosition + Vector3.down * ColliderExtents.y,
        };

        Vector3 getColliderLeftLocalPosition = GetColliderLeft - transform.position;
        colliderLocalPointsLeft = new Vector3[]
        {
            getColliderLeftLocalPosition + Vector3.up * ColliderExtents.y,
            getColliderLeftLocalPosition,
            getColliderLeftLocalPosition + Vector3.down * ColliderExtents.y,
        };

        Vector3 getColliderBottomLocalPosition = GetColliderBottom - transform.position;
        colLocalPointsBottom = new Vector3[]
        {
            getColliderBottomLocalPosition + (Vector3.right * ColliderExtents.x),
            getColliderBottomLocalPosition,
            getColliderBottomLocalPosition + (Vector3.left * ColliderExtents.x)
        };

    }

    private void Update()
    {
        UpdateGroundCheck();

    }  

    public void UpdateMovement(Vector3 input)
    {
        SetInput(input);
        Movement();
    }

    public void Jump()
    {
        if(IsOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }

    }

    private void SetInput(Vector3 input)
    {
        targetVelocity = input * maxSpeed;
    }
    
    private void Movement()
    {
        velocity = Vector3.MoveTowards(velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        UpdateCollisionHorizontal();


        var lastPosition = transform.position;
        var targetPosition = lastPosition + velocity * Time.fixedDeltaTime;
        transform.position = targetPosition;

    }

    private void UpdateCollisionHorizontal()
    {
        WallCheck();

    }

    private void WallCheck()
    {
        var rayLenght = maxSpeed * Time.fixedDeltaTime;
        var rayDir = characterFacing.IsFacingRight ? Vector3.right : Vector3.left;

        Vector3[] colliderDirPoint = characterFacing.IsFacingRight ?
        colliderLocalPointsRight :
        colliderLocalPointsLeft;


        int hitCount = 0;
        foreach (var localPoint in colliderDirPoint)
        {
            var rayOrigin = transform.position + localPoint;

            if (Physics2D.Raycast(rayOrigin, rayDir, rayLenght, whatIsGround))
            {
                Debug.DrawRay(rayOrigin, rayDir * rayLenght, Color.green);
                ++hitCount;
                velocity = new Vector3(0, velocity.y, velocity.z);
                continue;
            }
            Debug.DrawRay(rayOrigin, rayDir * rayLenght, Color.red);
        }
        IsTouchingWall = hitCount > 1;
    }

    private void UpdateGroundCheck()
    {

        int hitCount = 0;
        foreach (var collLocalPointBottom in colLocalPointsBottom)
        {
            var rayOrigin = transform.position + collLocalPointBottom;

            if(Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckRayCastLenght, whatIsGround))
            {
                Debug.DrawRay(rayOrigin, Vector3.down * groundCheckRayCastLenght, Color.green);
                ++hitCount;
                continue;
            }
            Debug.DrawRay(rayOrigin, Vector3.down * groundCheckRayCastLenght, Color.red);

        }

        IsOnGround = hitCount > 0;
    }

}
