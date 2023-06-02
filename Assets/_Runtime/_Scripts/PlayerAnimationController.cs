using UnityEngine;

public static class MovementStringsConstrains
{
    public static readonly string velocityX = "velocityX";


}

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;

    private Animator animator;

    //IDLE AND RUN
    private int velocityX = Animator.StringToHash(MovementStringsConstrains.velocityX);


    private void Awake() 
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void LateUpdate() 
    {
        
        animator.SetFloat(velocityX, playerController.velocityX);
    }


}
