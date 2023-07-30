using UnityEngine;

[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CheckSurrounds))]
public class HasPlatformInFront : MonoBehaviour
{

    private CharacterFacing2D charFacing;
    private CheckSurrounds checkSurrounds;
    private IColliderInfo collInfo;
    [SerializeField] LayerMask whatIsPlatform;

    [Header("HasPlatform")]
    [Range(0.25f, 5f)]
    [SerializeField] private float rayDistanceFromCollider = 1.25f;

    [Range(0.25f, 2f)]
    [SerializeField] private float rayLenght = 0.35f;

    private void Awake() 
    {
        charFacing = GetComponent<CharacterFacing2D>();   
        checkSurrounds = GetComponent<CheckSurrounds>();
        collInfo = GetComponent<IColliderInfo>();
    }

    public bool HasWallInFront()
    {
        return checkSurrounds.IsTouchingHorizontal;
    }

    public bool HasPlatformFront()
    {

        var isPlatformInFront = Physics2D.Raycast
        (
            collInfo.GetColliderBottom + (charFacing.GetCharacterFacingDirection() * rayDistanceFromCollider),
            Vector2.down,
            rayLenght,
            whatIsPlatform
        );


        return isPlatformInFront;
    }

    private void OnDrawGizmos()
    {
        UpdateDraw();

    }

    private void UpdateDraw()
    {
        charFacing ??= GetComponent<CharacterFacing2D>();
        collInfo ??= GetComponent<IColliderInfo>();
        DrawPlatformRay(charFacing, collInfo);
    }

    private void DrawPlatformRay(CharacterFacing2D facingParam, IColliderInfo collInfo)
    {
        var dirCharacter = facingParam.GetCharacterFacingDirection();
        var originRayCast = collInfo.GetColliderBottom + dirCharacter * rayDistanceFromCollider;


        Gizmos.DrawRay(originRayCast, Vector3.down * rayLenght);
    }

}
