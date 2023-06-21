using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public static class MeleeAttackStringsConstants
{
    public static readonly string AttackMultiplier = "AttackMultiplier";
    public static readonly string IsAttacking = "IsAttacking";

}

public class WeaponAttack : MonoBehaviour, IWeapon
{
    [SerializeField] private CharacterFacing2D characterFacing;
    private TriggerDamage triggerDamage;
    private float lastAttackTime = 0;
    [field: SerializeField] public float AttackDuration {get; private set;} = 1f;
    
    public bool IsAttacking {get; private set;} = false;

    [field: SerializeField] public float AttackCooldown {get; private set; } = 1.5f;

    private bool IsAttackReady => Time.time >= lastAttackTime + AttackCooldown;

    [SerializeField] private AnimationEventWeapon animationEventWeapon;

    private void Awake() 
    {
        triggerDamage = GetComponentInChildren<TriggerDamage>(true);
        animationEventWeapon.OnTriggerDamage += OntriggerDamageEvent;
        
    }

    private void Start() 
    {
        Assert.IsNotNull(animationEventWeapon, "AnimationTriggerWeapon cannot be null");
        Assert.IsNotNull(triggerDamage, "TriggerDamage cannot be null");
        Assert.IsNotNull(characterFacing, "CharacterFacing cannot be null");
        gameObject.SetActive(true);
        triggerDamage.gameObject.SetActive(false);
    }
    
    public void OnAttackWeapon()
    {
        if(IsAttacking || !IsAttackReady) return;

        gameObject.SetActive(true);
        StartCoroutine(PerformWeaponAttack());
    }

    private void StartWeapon()
    {
        Vector3 DecidingAttackDir()
        {
            Vector3 attackPosition = transform.localPosition;
            if(Mathf.Sign(transform.localPosition.x) != Mathf.Sign(characterFacing.GetCharacterFacingDirection().x))
            {
                attackPosition.x = transform.localPosition.x * -1;
            }

            return attackPosition;
        }

        IsAttacking = true;
        Vector3 attackPosition = DecidingAttackDir();
        transform.localPosition = attackPosition;
    }

    private IEnumerator PerformWeaponAttack()
    {
        StartWeapon();

        float finalAttackSeconds = Time.time + AttackDuration;

        while(Time.time < finalAttackSeconds)
        {
            yield return null;
        }

        EndWeapon();
    }

    private void OntriggerDamageEvent()
    {
        triggerDamage.gameObject.SetActive(true);
    }

    private void EndWeapon()
    {
        IsAttacking = false;
        triggerDamage.gameObject.SetActive(false);
        lastAttackTime = Time.time;

    }

    private void OnDestroy() 
    {
        animationEventWeapon.OnTriggerDamage -= OntriggerDamageEvent;

    }

}
