using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(IDamageable))]
public class HealthSystem : MonoBehaviour
{
    private IDamageable damageable;
    private CurrentStatus status;

    private float _currenthealth;
    public float MaxHealth {get; private set;}
    public float CurrentHealthValue 
    {

        get {return _currenthealth;} 

        set 
        {
            if(value == 0 || _currenthealth == 0) return;

            _currenthealth += value;
            _currenthealth = Mathf.Clamp(_currenthealth, 0, MaxHealth);

            if(_currenthealth <= 0)
            {
                DeathEvent?.Invoke();
            }
            if(Mathf.Sign(value) < 0)
            {
                HealthLessEvent?.Invoke();

            }
            else
            {
                HealthHealEvent?.Invoke();
            }

        }

    }

    public float HealthInPercent => CurrentHealthValue / MaxHealth;

    public event Action HealthLessEvent;

    public event Action HealthHealEvent;

    public event Action DeathEvent;

    private void Awake() 
    {
        damageable = GetComponent<IDamageable>();
        status = GetComponent<CurrentStatus>();
        Assert.IsNotNull(status, "Status can not be null");

        MaxHealth = status.BaseStatus.MaxHealth;
        _currenthealth = status.BaseStatus.MaxHealth;

    }

    private void Start() 
    {
        Assert.IsNotNull(damageable, "Damageable can not be null");

        damageable.OnTakeDamageEvent += OnTakingDamage;
        MaxHealth = status.BaseStatus.MaxHealth;

    }

    private void OnTakingDamage(DamageInfo damageInfo)
    {
        CurrentHealthValue = -damageInfo.Damage;        
    }

}
