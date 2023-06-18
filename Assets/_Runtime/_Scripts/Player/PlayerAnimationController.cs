using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : CharacterAnimationController
{
    private PlayerController playerController;
    private int IsAttacking => Animator.StringToHash(MeleeAttackStringsConstants.IsAttacking);

    protected override void Awake() 
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        animator.SetBool(IsAttacking, playerController.WeaponAttack.IsAttacking);

    }

}
