using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    private float _currenthealth;

    public float MaxHealth {get; private set;}

    public float CurrentHealthValue 
    {

        get {return _currenthealth;} 

        set 
        {
            if(value == 0) return;


            _currenthealth += value;
            _currenthealth = Mathf.Clamp(_currenthealth, 0, MaxHealth);

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

    private void Awake() 
    {
        MaxHealth = 10;
        _currenthealth = MaxHealth;
    }

}
