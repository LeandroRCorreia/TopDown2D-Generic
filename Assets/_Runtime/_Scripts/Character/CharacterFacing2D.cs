using UnityEngine;

public class CharacterFacing2D : MonoBehaviour
{
    public bool IsFacingRight {get; private set;} = true;


    public void UpdateFacing(Vector3 input)
    {
        if(input.x > 0 && IsFacingRight || input.x < 0 && !IsFacingRight)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        transform.rotation = IsFacingRight ? Quaternion.identity : new Quaternion(0, 180, 0, 0); 
        IsFacingRight = !IsFacingRight; 
    }

}
