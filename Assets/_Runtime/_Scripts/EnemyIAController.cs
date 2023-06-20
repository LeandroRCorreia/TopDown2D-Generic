using System.Collections;
using UnityEngine;

public class EnemyIAController : MonoBehaviour, ICharacter
{
    public CharacterFacing2D CharacterFacing {get; private set;}
    public CharacterMovement2D CharacterMovement {get; private set;}

    public OnTakeDamage OnTakingDamage {get; private set;}


    public Vector3 input {get; set; }

    private void Awake() 
    {
        CharacterMovement = GetComponent<CharacterMovement2D>();
        CharacterFacing = GetComponent<CharacterFacing2D>();
        OnTakingDamage = GetComponent<OnTakeDamage>();
    }

    private void Start() 
    {
        OnTakingDamage.OnTakeDamageEvent += OnTakeDamageEvent;

    }

    private void FixedUpdate()
    {
        CharacterMovement.Movement(input);
        CharacterFacing.UpdateFacing(input);

    }
    
    private void OnTakeDamageEvent()
    {
        // Destroy(gameObject);

    }

    private void OnDestroy() 
    {
        OnTakingDamage.OnTakeDamageEvent -= OnTakeDamageEvent;
 
    }

}
