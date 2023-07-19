using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public struct IssuerDamageInfo
{
    public CurrentStatus currentStatus;
    
}

[RequireComponent(typeof(Collider2D))]
public class TriggerDamage : MonoBehaviour
{

    //TODO: inappropriate pointer encapsulation 
    [field: SerializeField] public CurrentStatus currentStatus {get; private set;}
    //

    private Collider2D colliderTrigger;

    private void Awake() 
    {
        colliderTrigger = GetComponent<Collider2D>();
    }

    private void Start() 
    {
        Assert.IsNotNull(colliderTrigger, "Collider trigger cannot be null");
        colliderTrigger.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            var issuerDamageInfo = new IssuerDamageInfo
            {
                currentStatus = currentStatus
            };

            damageable.TakingDamage(in issuerDamageInfo);


        }

    }

}
