using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTakeDamage : MonoBehaviour, IDamageable
{

    private HealthSystem healthSystem;

    [Header("Invincible Params")]
    [Range(0.05f, 2f)] [SerializeField] private float timeInvincible = 0.1f;
    [Range(5, 20f)] [SerializeField] private float speedPingPongSprite = 10;

    private float currentTime = Mathf.NegativeInfinity;

    private SpriteRenderer sprite;

    public bool IsInvincible {get; private set;}
    public event System.Action OnTakeDamageEvent;

    private void Awake() 
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        healthSystem = GetComponent<HealthSystem>();
    }

    public void TakingDamage(float damage)
    {
        if(IsInvincible) return;

        healthSystem.CurrentHealthValue = -damage;

        StartCoroutine(OnTakingDamage());
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
        OnTakeDamageEvent?.Invoke();
        currentTime = Time.time + timeInvincible;
    }

}
