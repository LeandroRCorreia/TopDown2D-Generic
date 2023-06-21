using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : CharacterAnimationController
{
    private PlayerController playerController;
    private int IsAttacking => Animator.StringToHash(MeleeAttackStringsConstants.IsAttacking);

    private IDamageable damageable;


    protected override void Awake() 
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
        damageable = GetComponent<IDamageable>();
        damageable.OnTakeDamageEvent += OnTakeDamage;

    }

    private void Start() 
    {
        

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        animator.SetBool(IsAttacking, playerController.WeaponAttack.IsAttacking);

    }

    private void OnTakeDamage()
    {

    }

    private void OnDestroy() 
    {
        damageable.OnTakeDamageEvent -= OnTakeDamage;
    }

}
