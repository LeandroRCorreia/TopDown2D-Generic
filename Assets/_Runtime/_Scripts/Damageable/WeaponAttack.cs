using System.Collections;
using UnityEngine;

public interface IWeapon
{
    bool IsAttacking {get;}
    float AttackSeconds {get;}

    void OnAttackWeapon();

}

public class WeaponAttack : MonoBehaviour, IWeapon
{

    private TriggerDamage triggerDamage;
    [field: SerializeField] public float AttackSeconds {get; private set;} = 0.5f;
    
    public bool IsAttacking {get; private set;} = false;

    private void Awake() 
    {
        triggerDamage = GetComponentInChildren<TriggerDamage>(true);
    }
    
    public void OnAttackWeapon()
    {
        if(IsAttacking) return;
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

        float finalAttackSeconds = Time.time + AttackSeconds;
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

    }

}
