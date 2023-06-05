using UnityEngine;

public static class MovementStringsConstrains
{
    public static readonly string velocityX = "velocityX";


}

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private ICharacter character;
    protected Animator animator;
    //IDLE AND RUN
    private int velocityX = Animator.StringToHash(MovementStringsConstrains.velocityX);

    private void Awake() 
    {
        character = GetComponent<ICharacter>();
        animator = GetComponentInChildren<Animator>();
    }

    private void LateUpdate() 
    {
        animator.SetFloat(velocityX, character.CharacterMovement.percentVelocityX);
    }


}
