using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeleeAttackStringsConstants
{
    public static readonly string AttackMultiplier = "AttackMultiplier";
    public static readonly string IsAttacking = "IsAttacking";

}

public class EnemyAnimationController : CharacterAnimationController
{

    [SerializeField] private WeaponAttack weapon;


    private int IsAttacking => Animator.StringToHash(MeleeAttackStringsConstants.IsAttacking);



    protected sealed override void LateUpdate() 
    {
        base.LateUpdate();
        
        animator.SetBool(IsAttacking, weapon.IsAttacking);

    }


}
