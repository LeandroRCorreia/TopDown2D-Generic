using System.Collections;
using UnityEngine;

public class EnemyIAController : MonoBehaviour, ICharacter
{
    public CharacterFacing2D CharacterFacing {get; private set;}
    public CharacterMovement2D CharacterMovement {get; private set;}

    private void Awake() 
    {
        CharacterMovement = GetComponent<CharacterMovement2D>();
        CharacterFacing = GetComponent<CharacterFacing2D>();
    }

    private void Start()
    {
        StartCoroutine(PerformPatrol());    
        
    }

    private IEnumerator PerformPatrol()
    {
        var durationPatrol = 2f;
        var durationIdle = 1f;

        while(true)
        {
            yield return MovementBy(Vector3.right, durationPatrol);
            yield return MovementBy(Vector3.zero, durationIdle);
            yield return MovementBy(Vector3.left, durationPatrol);
            yield return MovementBy(Vector3.zero, durationIdle);
        }


    }

    private IEnumerator MovementBy(Vector3 input, float durationMovement)
    {
        var finalTime = Time.time + durationMovement;
        while(Time.time < finalTime)
        {
            CharacterMovement.Movement(input);
            CharacterFacing.UpdateFacing(input);
            yield return null;
        }

    }

}
