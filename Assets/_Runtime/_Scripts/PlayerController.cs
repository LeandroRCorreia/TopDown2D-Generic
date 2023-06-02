using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sprite;
    
    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    private Vector3 input;
    private Vector3 lastPosition;
    public float velocityX => Mathf.Abs((transform.position.x - lastPosition.x) * speed);

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();    
    }

    private void FixedUpdate() 
    {
        input = Vector3.zero;
        input.x = Input.GetAxis("Horizontal");


        
        lastPosition = transform.position;
        transform.position = lastPosition + input * speed * Time.fixedDeltaTime;
    }

    private void LateUpdate() 
    {
        if(input.x > 0 && sprite.flipX || input.x < 0 && !sprite.flipX)
        {
            FlipSprite();
        }

    }

    private void FlipSprite()
    {
        sprite.flipX = !sprite.flipX;
    }

}
