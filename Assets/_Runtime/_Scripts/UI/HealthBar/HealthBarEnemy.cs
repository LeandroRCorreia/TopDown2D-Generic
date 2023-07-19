using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : HealthBarUI
{

    [Range(0, 5)] private float timeToDisableHealthBar = 2f;

    protected override void Start() 
    {
        base.Start();
        healthSystem.DeathEvent += OnDeathEvent;
    }

    private void OnDeathEvent()
    {
        StartCoroutine(WaitingSomeTimeToDisableHealthBar());
        

    }

    private IEnumerator WaitingSomeTimeToDisableHealthBar()
    {
        
        yield return new WaitForEndOfFrame();

        StopCoroutine(LessHealthBarCoro());

        yield return new WaitForSeconds(timeToDisableHealthBar);
        SetHealthBarActive(false);

    }

    private void OnDestroy() 
    {
        healthSystem.DeathEvent -= OnDeathEvent;

    }

}
