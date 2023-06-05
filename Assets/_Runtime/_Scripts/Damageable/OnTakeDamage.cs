using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTakeDamage : MonoBehaviour, IDamageable
{
    public event System.Action OnTakeDamageEvent;

    public void TakingDamage()
    {
        
        OnTakeDamageEvent?.Invoke();
        Debug.Log("Estou aqui INTakeDamge");
    }


}
