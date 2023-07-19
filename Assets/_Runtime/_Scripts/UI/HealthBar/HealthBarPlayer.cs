using UnityEngine;
using TMPro;

public class HealthBarPlayer : HealthBarUI
{
    [SerializeField] private TextMeshProUGUI healthText;
    private float SpeedTargetTextHealth => speedTargetHealthPercent * 10;
    private float targetHealthText;

    protected override void Start() 
    {
        base.Start();
        targetHealthText = healthSystem.CurrentHealthValue;
        UpdateUiText();

    }

    protected override void UpdateLessHealth()
    {
        base.UpdateLessHealth();
        targetHealthText = Mathf.MoveTowards(targetHealthText,
        healthSystem.CurrentHealthValue, SpeedTargetTextHealth * Time.deltaTime);
        UpdateUiText();
    }

    protected override void UpdateHealHealth()
    {
        base.UpdateHealHealth();
        targetHealthText = healthSystem.CurrentHealthValue;
        UpdateUiText();
    }

    private void UpdateUiText()
    {
        healthText.text = $"{Mathf.Floor(targetHealthText)}/{healthSystem.MaxHealth}";
    }


}
