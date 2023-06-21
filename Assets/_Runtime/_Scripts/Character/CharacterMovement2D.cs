using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement2D : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float acceleration = 70f;
    private Vector3 Velocity;
    private Vector3 targetVelocity;
    public float speedX => Mathf.Clamp01(Mathf.Abs(Velocity.x));


    [Header("Collision")]

    private BoxCollider2D boxCollider;
    private Vector3 colliderExtents => boxCollider.bounds.extents; 

    [Header("Jump")]

    [SerializeField] private float jumpForce = 7f;
    public float SpeedY => rb.velocity.y;


    //GroundCheck

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector3 rayCastPointOffSet = Vector3.up * 0.5f;

    private Vector3 rayCastPoint => transform.position + rayCastPointOffSet;
    public bool IsOnAir {get; private set;}
    [SerializeField] [Range(0.1f, 1f)] private float rayCastLenght = 0.5f;
    private RaycastHit2D[] rayCastsIsOnGround = new RaycastHit2D[5];

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        UpdateGroundCheck();

    }

    private void UpdateGroundCheck()
    {
        ContactFilter2D contactFilter2D = new()
        {
            useLayerMask = true,
            layerMask = whatIsGround
        };

        
        int hitCount = Physics2D.RaycastNonAlloc(rayCastPoint, Vector2.down,
        rayCastsIsOnGround, rayCastLenght, whatIsGround);

        IsOnAir = hitCount < 1;
    }

    public void Movement(Vector3 input)
    {
        SetInput(input);
        UpdateMovement();
    }

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

    public void Jump()
    {
        if(!IsOnAir)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }

    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = IsOnAir ? Color.green : Color.red;
        Gizmos.DrawRay(rayCastPoint, Vector3.down * rayCastLenght);   

    }

}
