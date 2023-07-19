using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(IBoxInfo))]
public class FloatingDamageSpawner : MonoBehaviour
{
    
    [SerializeField] private FloatingDamage floatingDamageNormal;
    [SerializeField] private FloatingDamage floatingDamageCritical;
    private IDamageable damageable;

    private IBoxInfo boxInfo;

    private void Awake() 
    {
        damageable = GetComponent<IDamageable>();
        boxInfo = GetComponent<IBoxInfo>();
    }

    private void Start() 
    {
        damageable.OnTakeDamageEvent += OnTakeDamage;
    }

    private void OnTakeDamage(DamageInfo damageInfo)
    {
        var floatingDamagePrefab = damageInfo.IsCritical ? floatingDamageCritical : floatingDamageNormal;
        SpawnFloatingDamage(floatingDamagePrefab, damageInfo);

    }

    private void SpawnFloatingDamage(FloatingDamage typeOfFloatingDamage, in DamageInfo damageInfo)
    {
        //TODO: OBJ POOL To receive from pool
        var floatingDamageInstance = Instantiate(typeOfFloatingDamage, boxInfo.GetColliderUp, Quaternion.identity);
        //
        
        floatingDamageInstance.floatingDamageText.text = $"-{damageInfo.Damage}";
        
    }

    private void OnDestroy() 
    {
        damageable.OnTakeDamageEvent -= OnTakeDamage;

    }

}
