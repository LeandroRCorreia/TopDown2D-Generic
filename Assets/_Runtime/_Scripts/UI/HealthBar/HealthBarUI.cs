using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public abstract class HealthBarUI : MonoBehaviour
{   

    [SerializeField] protected HealthSystem healthSystem;

    [Header("HealthBar Elements")]
    [SerializeField] protected Image sliderHealthBar;
    [SerializeField] private Image sliderTakeDamageBar;
    [SerializeField][Range(0.25f, 2)] private float timeToApplySmoothness = 0.5f;


    [SerializeField] protected float speedTargetHealthPercent = 4;
    private float percentHealthSmoothness = 1f;


    private bool wasInLessHealth = false;

    private void Awake() 
    {
        Assert.IsNotNull(healthSystem, "HealthSystem cannot be null");
    }

    protected virtual void Start() 
    {
        percentHealthSmoothness = healthSystem.HealthInPercent;
        sliderHealthBar.fillAmount = percentHealthSmoothness;
        healthSystem.HealthLessEvent += OnLessHealth;
        healthSystem.HealthHealEvent += OnHealHealth;

    }

    private void OnLessHealth()
    { 
        sliderHealthBar.fillAmount = healthSystem.HealthInPercent;
        
        if(!wasInLessHealth)
            StartCoroutine(LessHealthBarCoro());

    }

    private void OnHealHealth()
    {
        UpdateHealHealth();

    }

    protected virtual void UpdateHealHealth()
    {
        sliderHealthBar.fillAmount = healthSystem.HealthInPercent;
        sliderTakeDamageBar.fillAmount = healthSystem.HealthInPercent;
        percentHealthSmoothness = sliderTakeDamageBar.fillAmount;
    }

    protected IEnumerator LessHealthBarCoro()
    {
        wasInLessHealth = true;
        yield return new WaitForSeconds(timeToApplySmoothness);


        while(percentHealthSmoothness > healthSystem.HealthInPercent)
        {
            UpdateLessHealth();
            yield return null;
        }
        wasInLessHealth = false;

    }

    protected virtual void UpdateLessHealth()
    {
        percentHealthSmoothness = Mathf.MoveTowards(percentHealthSmoothness, healthSystem.HealthInPercent, speedTargetHealthPercent * Time.deltaTime);

        sliderTakeDamageBar.fillAmount = percentHealthSmoothness;
    }

    public void SetHealthBarActive(bool value)
    {
        gameObject.SetActive(value);
    }

    private void OnDestroy() 
    {
        healthSystem.HealthLessEvent -= OnLessHealth;    
        healthSystem.HealthHealEvent -= OnHealHealth;
    }

}
