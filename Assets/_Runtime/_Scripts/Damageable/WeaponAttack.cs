using System.Collections;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    private TriggerDamage triggerDamage;
    [SerializeField] private float attackSeconds = 0.5f;
    
    private bool isAttacking;

    private void Awake() 
    {
        triggerDamage = GetComponentInChildren<TriggerDamage>(true);
    }
    
    public void OnAttackWeapon()
    {
        if(isAttacking) return;
        gameObject.SetActive(true);
        StartCoroutine(PerformWeaponAttack());
    }

    private void StartWeapon()
    {
        isAttacking = true;
        triggerDamage.gameObject.SetActive(true);
    }

    private IEnumerator PerformWeaponAttack()
    {
        StartWeapon();

        float finalAttackSeconds = Time.time + attackSeconds;
        while(Time.time < finalAttackSeconds)
        {
            yield return null;
        }

        EndWeapon();
    }


    private void EndWeapon()
    {
        gameObject.SetActive(false);
        isAttacking = false;
        triggerDamage.gameObject.SetActive(false);

    }

}
