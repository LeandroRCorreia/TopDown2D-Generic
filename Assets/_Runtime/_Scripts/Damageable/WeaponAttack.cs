using System;
using System.Collections;
using UnityEngine;

public interface IWeapon
{
    bool IsAttacking {get;}
    float AttackDuration {get;}
    float AttackCooldown {get;}

    void OnAttackWeapon();

}

public class WeaponAttack : MonoBehaviour, IWeapon
{
    private TriggerDamage triggerDamage;
    private float lastAttackTime = 0;
    [field: SerializeField] public float AttackDuration {get; private set;} = 1f;
    
    public bool IsAttacking {get; private set;} = false;

    [field: SerializeField] public float AttackCooldown {get; private set; } = 1.5f;


    public bool IsAttackFresh => Time.time >= lastAttackTime + AttackCooldown;

    private void Awake() 
    {
        triggerDamage = GetComponentInChildren<TriggerDamage>(true);
    }
    
    public void OnAttackWeapon()
    {
        if(IsAttacking || !IsAttackFresh) return;
        gameObject.SetActive(true);
        StartCoroutine(PerformWeaponAttack());
    }

    private void StartWeapon()
    {
        IsAttacking = true;
        triggerDamage.gameObject.SetActive(true);
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


    private void EndWeapon()
    {
        gameObject.SetActive(false);
        IsAttacking = false;
        triggerDamage.gameObject.SetActive(false);
        lastAttackTime = Time.time;


    }

}
