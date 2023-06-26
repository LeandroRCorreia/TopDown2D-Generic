using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class HudGameplay : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [Header("HealthBar Elements")]
    [SerializeField] private Image sliderHealth;

    private float SpeedTargetTextHealth => speedTargetHealthPercent * 10;
    private float targetHealthText;

    [SerializeField] private float speedTargetHealthPercent = 2;
    private float PercentTakeDamage;

    private void Start() 
    {
        targetHealthText = player.healthSystem.CurrentHealthValue;
        PercentTakeDamage = player.healthSystem.HealthInPercent;
        sliderHealth.fillAmount = PercentTakeDamage;

    }


    private void LateUpdate()
    {
        targetHealthText = Mathf.MoveTowards(targetHealthText,
        player.healthSystem.CurrentHealthValue, SpeedTargetTextHealth * Time.deltaTime);

        PercentTakeDamage = Mathf.MoveTowards(PercentTakeDamage,
        player.healthSystem.HealthInPercent, speedTargetHealthPercent * Time.deltaTime);



    }



}
