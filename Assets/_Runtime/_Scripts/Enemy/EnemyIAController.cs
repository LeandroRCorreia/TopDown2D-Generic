using System;
using System.Collections;
using UnityEngine;

public class EnemyIAController : MonoBehaviour, ICharacter
{
    public CharacterFacing2D CharacterFacing {get; private set;}
    public CharacterMovement2D CharacterMovement {get; private set;}
    public CheckSurrounds CheckSurrounds {get; private set;}
    public OnTakeDamage OnTakingDamage {get; private set;}

    [Range(0.01f, 3f)] [SerializeField] private float tempTimeToDisable = 3;

    public Vector3 input {get; set; }

    private BehaviorExecutor behaviorExecutor;

    public HealthSystem HealthControl {get; private set;}

    private void Awake() 
    {
        CharacterMovement = GetComponent<CharacterMovement2D>();
        CharacterFacing = GetComponent<CharacterFacing2D>();
        OnTakingDamage = GetComponent<OnTakeDamage>();
        CheckSurrounds = GetComponent<CheckSurrounds>();
        behaviorExecutor = GetComponent<BehaviorExecutor>();
        HealthControl = GetComponent<HealthSystem>();
    }

    private void Start() 
    {
        OnTakingDamage.OnTakeDamageEvent += OnTakeDamageEvent;
        HealthControl.DeathEvent += OnDeathEvent;

    }

    private void OnDeathEvent()
    {
        behaviorExecutor.paused = true;
        input = Vector3.zero;
        

        StartCoroutine(TEMPDisableInCertainTimeGm());

    }

    private IEnumerator TEMPDisableInCertainTimeGm()
    {
        float finaltime = Time.time + tempTimeToDisable;

        while(Time.time < finaltime)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        CharacterMovement.UpdateMovement(input);
        CharacterFacing.UpdateFacing(input);

    }
    
    private void OnTakeDamageEvent(DamageInfo damageParams)
    {
        

    }

    private void OnDestroy() 
    {
        OnTakingDamage.OnTakeDamageEvent -= OnTakeDamageEvent;
        HealthControl.DeathEvent -= OnDeathEvent;
 
    }

}
