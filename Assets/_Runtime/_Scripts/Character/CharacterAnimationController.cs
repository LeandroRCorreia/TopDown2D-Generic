using UnityEngine;

public static class CharacterStringsConstrains
{
    public static readonly string velocityX = "velocityX";


}

public class CharacterAnimationController : MonoBehaviour
{
    private ICharacter character;
    protected Animator animator;
    //IDLE AND RUN
    private int velocityX = Animator.StringToHash(CharacterStringsConstrains.velocityX);

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
