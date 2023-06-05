using UnityEngine;

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
    private Vector3 playerInput;
    public CharacterMovement2D CharacterMovement {get; private set; }
    public CharacterFacing2D CharacterFacing {get; private set;}

    private void Awake() 
    {
        CharacterMovement = GetComponent<CharacterMovement2D>();
        CharacterFacing = GetComponent<CharacterFacing2D>();
    }

    private void Update() 
    {
        playerInput = Vector3.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        CharacterMovement.SetInput(playerInput);
        
        CharacterFacing.UpdateFacing(playerInput);

    }

    private void FixedUpdate() 
    {
        CharacterMovement.UpdateMovement();
        
    }

}
