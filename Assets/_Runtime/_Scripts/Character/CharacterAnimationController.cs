using UnityEngine;

public static class CharacterStringsConstrains
{
    public static readonly string SpeedX = "SpeedX";


}

public class CharacterAnimationController : MonoBehaviour
{
    private ICharacter character;
    protected Animator animator;
    //IDLE AND RUN
    private int SpeedX => Animator.StringToHash(CharacterStringsConstrains.SpeedX);

    protected virtual void Awake() 
    {
        character = GetComponent<ICharacter>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void LateUpdate() 
    {
        MovementAnimator();
    }

    protected virtual void MovementAnimator()
    {
        animator.SetFloat(SpeedX, character.CharacterMovement.percentVelocityX);
    }

}
