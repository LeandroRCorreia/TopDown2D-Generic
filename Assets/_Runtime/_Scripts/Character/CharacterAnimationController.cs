using UnityEngine;

public static class CharacterStringsConstrains
{
    public static readonly string SpeedX = "SpeedX";
    public static readonly string SpeedY = "SpeedY";
    public static readonly string IsOnAir = "IsOnAir";

}

public class CharacterAnimationController : MonoBehaviour
{
    private ICharacter character;
    protected Animator animator;
    //IDLE AND RUN
    private int SpeedX => Animator.StringToHash(CharacterStringsConstrains.SpeedX);
    private int SpeedY => Animator.StringToHash(CharacterStringsConstrains.SpeedY);
    private int IsOnAir => Animator.StringToHash(CharacterStringsConstrains.IsOnAir);

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
        animator.SetFloat(SpeedX, character.CharacterMovement.speedX);
        animator.SetFloat(SpeedY, character.CharacterMovement.SpeedY);
        animator.SetBool(IsOnAir, character.CharacterMovement.IsOnAir);
    }

}
