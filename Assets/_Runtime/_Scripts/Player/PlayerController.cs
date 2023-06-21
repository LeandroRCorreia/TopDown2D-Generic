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
    public CharacterMovement2D CharacterMovement {get; private set; }
    public CharacterFacing2D CharacterFacing {get; private set;}
    public IDamageable OnTakeDamage {get; private set;}
    public IWeapon WeaponAttack {get; private set;}

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        CharacterMovement = GetComponent<CharacterMovement2D>();
        CharacterFacing = GetComponent<CharacterFacing2D>();
        WeaponAttack = GetComponentInChildren<IWeapon>(true);
        playerInputActions = new();

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
