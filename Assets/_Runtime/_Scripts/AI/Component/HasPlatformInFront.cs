using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterFacing2D))]
public class HasPlatformInFront : MonoBehaviour
{

    private CharacterFacing2D facing;
    [SerializeField] LayerMask whatIsPlatform;

    [Header("HasPlatform")]
    [Range(0.25f, 2f)]
    [SerializeField] private float platformRayDistance = 0.5f;

    [Range(0.25f, 2f)]
    [SerializeField] private float platformRayLenght = 0.35f;
    private RaycastHit2D[] platformColls = new RaycastHit2D[5]; 

    [Space]

    [Header("HasWallInFront")]
    [Range(0.25f, 2f)]
    [SerializeField] private float wallRayDistance = 0.5f;

    [Range(0.25f, 2f)]
    [SerializeField] private float wallRayLenght = 0.25f;
    [Range(0.05f, 2f)]
    [SerializeField] private float wallRayOffsetY = 0.25f;

    private RaycastHit2D[] wallColls = new RaycastHit2D[5]; 

    private void Awake() 
    {
        facing = GetComponent<CharacterFacing2D>();   
    }

    public bool HasWallInFront()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useLayerMask = true;
        contactFilter.layerMask = whatIsPlatform;

        var upOffSet = (Vector3.up * wallRayOffsetY);
        var dirCharacterOffset = (facing.GetCharacterFacingDirection() * wallRayDistance);
        var pointRayCast = transform.position + upOffSet + dirCharacterOffset;


        int hitCount = Physics2D.Raycast(
        pointRayCast,
        facing.GetCharacterFacingDirection(),
        contactFilter,
        wallColls,
        wallRayLenght);
        

        return hitCount > 0;
    }

    public bool HasPlatformFront()
    {
        //Valid only if obj is OnGround;
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useLayerMask = true;
        contactFilter.layerMask = whatIsPlatform;

        int hitCount = Physics2D.Raycast(
        transform.position + (facing.GetCharacterFacingDirection() * wallRayDistance),
        Vector2.down,
        contactFilter,
        platformColls,
        platformRayLenght);

        return hitCount > 0;
    }

    private void OnDrawGizmos()
    {
        UpdateDraw();

    }

    private void UpdateDraw()
    {
        if (facing == null)
        {
            facing = GetComponent<CharacterFacing2D>();
        }
        DrawPlatformRay(facing);
        DrawWallRay(facing);
    }

    private void DrawPlatformRay(CharacterFacing2D facingParam)
    {

        var position = transform.position;
        var dirCharacter = facingParam.GetCharacterFacingDirection();
        var pointLine = dirCharacter * platformRayDistance;

        Gizmos.DrawRay(position + pointLine, Vector3.down * platformRayLenght);
    }

    private void DrawWallRay(CharacterFacing2D facingParam)
    {
        var position = transform.position + (Vector3.up * wallRayOffsetY);
        var dirCharacter = facingParam.GetCharacterFacingDirection();
        var pointLine = dirCharacter * wallRayDistance;

        Gizmos.DrawRay(position + pointLine, facingParam.GetCharacterFacingDirection() * wallRayLenght);
    }

}
