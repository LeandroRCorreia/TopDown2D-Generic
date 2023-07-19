using System.Collections;
using UnityEngine;

public struct DamageInfo
{
    public int Damage;
    public bool IsCritical;
}

public class OnTakeDamage : MonoBehaviour, IDamageable
{
    private SpriteRenderer sprite;

    [Header("Invincible Params")]
    [Range(0.05f, 2f)] [SerializeField] private float timeInvincible = 0.1f;
    [Range(5, 20f)] [SerializeField] private float speedPingPongSprite = 10;

    private float currentTime = Mathf.NegativeInfinity;
    public CurrentStatus EntityStatus {get;private set;}
    public bool IsInvincible {get; private set;}

    public event System.Action<DamageInfo> OnTakeDamageEvent;

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        EntityStatus = GetComponent<CurrentStatus>();
    }

    public void TakingDamage(in IssuerDamageInfo issuerDamageInfo)
    {
        if (IsInvincible) return;

        DamageInfo damageInfo = ProcessTakingDamage(issuerDamageInfo);

        OnTakeDamageEvent?.Invoke(damageInfo);

        StartCoroutine(OnTakingDamage());
    }

    private DamageInfo ProcessTakingDamage(in IssuerDamageInfo issuerDamageInfo)
    {
        DamageInfo damageInfo = new()
        {
            IsCritical = issuerDamageInfo.currentStatus.BaseStatus.CritChance >= Random.value,
            Damage = issuerDamageInfo.currentStatus.BaseStatus.Strength
        };

        damageInfo.Damage = damageInfo.IsCritical ?
        Mathf.RoundToInt(damageInfo.Damage * issuerDamageInfo.currentStatus.BaseStatus.DamamageMultiplier) :
        damageInfo.Damage;

        damageInfo.Damage = Mathf.RoundToInt(damageInfo.Damage - 
        (damageInfo.Damage * EntityStatus.BaseStatus.PercentageMitigatedDefense));
        
        return damageInfo;
    }

    private IEnumerator OnTakingDamage()
    {
        StartTakeDamageBehaviour();
        Color initialColor = sprite.color;

        while (Time.time < currentTime)
        {
            UpdateTakeDamageBehaviour();
            yield return null;
        }

        sprite.color = initialColor;
        EndTakeDamageBehaviour();
    }

    private void UpdateTakeDamageBehaviour()
    {
        float pingPongEffect = Mathf.PingPong(Time.time * speedPingPongSprite, 1);

        sprite.color = new Vector4(
        pingPongEffect,
        pingPongEffect,
        pingPongEffect,
        pingPongEffect
        );

    }

    private void EndTakeDamageBehaviour()
    {
        IsInvincible = false;
    }

    private void StartTakeDamageBehaviour()
    {
        IsInvincible = true;
        currentTime = Time.time + timeInvincible;
    }

}
