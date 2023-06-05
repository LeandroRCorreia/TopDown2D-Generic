using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakingDamage();
        }

    }

}
