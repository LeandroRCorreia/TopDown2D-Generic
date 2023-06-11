using UnityEngine;

public class CharacterFacing2D : MonoBehaviour
{
    public bool IsFacingRight {get; private set;} = true;

    private SpriteRenderer sprite;

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();    
    }

    public void UpdateFacing(Vector3 input)
    {
        if(input.x > 0 && !IsFacingRight || input.x < 0 && IsFacingRight)
        {
            FlipSprite();
        }
    }

    public Vector3 GetCharacterFacingDirection()
    {
        return IsFacingRight ? Vector3.right : Vector3.left;
    }

    private void FlipSprite()
    {
        sprite.flipX = !sprite.flipX;
        IsFacingRight = !IsFacingRight; 
    }

}
