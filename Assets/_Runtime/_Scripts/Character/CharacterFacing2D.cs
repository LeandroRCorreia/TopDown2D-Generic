using UnityEngine;


public class CharacterFacing2D : MonoBehaviour
{
    public bool IsFacingRight => !sprite.flipX; 

    private SpriteRenderer sprite;

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();    
    }

    public void UpdateFacing(Vector3 input)
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
