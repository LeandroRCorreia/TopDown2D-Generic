using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

interface ICharacter
{
    CharacterMovement2D CharacterMovement {get;}
    CharacterFacing2D CharacterFacing {get;}

}

interface IPlayer : ICharacter
{

}

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(CharacterFacing2D))]
public class PlayerController : MonoBehaviour, IPlayer
{

    private PlayerInputActions playerInputActions;

    private CharacterFacing2D _characterFacing;

    private CharacterMovement2D _characterMovement;

    public CharacterMovement2D CharacterMovement 
    {
        get
        {
            return _characterMovement ?? GetComponent<CharacterMovement2D>();
        }
        private set
        {
            _characterMovement = value;
        } 
    
    }

    public CharacterFacing2D CharacterFacing 
    {
        get
        {
            return _characterFacing ?? GetComponent<CharacterFacing2D>();
        }
        private set
        {
            _characterFacing = value;
        }

    }

    public IDamageable OnTakeDamage {get; private set;}
    public IWeapon WeaponAttack { get; private set; }

    public HealthSystem healthSystem { get; private set; }

    private void Awake() 
    {
        playerInputActions = new();
        CharacterMovement = GetComponent<CharacterMovement2D>();
        CharacterFacing = GetComponent<CharacterFacing2D>();

        healthSystem = GetComponent<HealthSystem>();
        WeaponAttack = GetComponentInChildren<IWeapon>(true);

    }

    private void Start() 
    {
        playerInputActions.Ground.Enable();   
        playerInputActions.Ground.AttackMelee.performed += OnAttackMelee;
        playerInputActions.Ground.Jump.performed += OnJump;

    }

    private void Update() 
    {
        Vector2 playerInput = playerInputActions.Ground.Movement.ReadValue<Vector2>();
        CharacterMovement.SetInput(playerInput);
        if(!WeaponAttack.IsAttacking) 
            CharacterFacing.UpdateFacing(playerInput);

    }

    private void FixedUpdate() 
    {
        CharacterMovement.UpdateMovement();
        
    }

    private void OnAttackMelee(InputAction.CallbackContext context)
    {
        WeaponAttack.OnAttackWeapon();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        CharacterMovement.Jump(); 

    }

    private void OnDestroy() 
    {
        playerInputActions.Ground.AttackMelee.performed -= OnAttackMelee;
        playerInputActions.Ground.Jump.performed -= OnJump;

    }

}
