using System;
using UnityEngine;

[ExecuteAlways]
public class CameraGameplay : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float cameraOffSetY = 3;

    public Vector3 playerPosition => playerController.transform.position;

    [Header("LookAt")]

    [Range(0.5f, 4)]
    [SerializeField] private float finalTargetCameraX = 2f;
    [SerializeField] private float speedSmoothView = 5f;
    private float currentTargetCameraX = 0f;

    private Vector3 arm;

    [Space]

    [Header("Player Run")]
    [SerializeField] private float finalRunPlayer = 2;
    [SerializeField] private float currentSpeedPositionX = 0;

    [Range(0.05f, 5f)][SerializeField] 
    private float speedAceleration = 3;

    [Range(0.05f, 5f)][SerializeField] 
    private float speedDeceleration = 3;

    [Range(0.05f, 1f)][SerializeField]
    private float velocityPercentToApplyEffect;


    public float FinalRunPlayer => playerController.CharacterFacing.IsFacingRight ? finalRunPlayer : -finalRunPlayer;
    public float TargetCameraX => playerController.CharacterFacing.IsFacingRight ? finalTargetCameraX: -finalTargetCameraX;


    void LateUpdate()
    {
        UpdateCameraPreferences();
    }

    private void UpdateCameraPreferences()
    {
        HorizontalCameraPreferences();
        arm.y = cameraOffSetY;
        var followPlayer = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        transform.position = followPlayer + arm;
    }

    private void HorizontalCameraPreferences()
    {
        currentTargetCameraX = CameraTargetInFrontOfPlayer();
        currentSpeedPositionX = WhenPlayerRunsSpeedPlayer();
        
        arm.x = currentTargetCameraX + currentSpeedPositionX;
    }

    private float CameraTargetInFrontOfPlayer()
    {
        return Mathf.Lerp(currentTargetCameraX, TargetCameraX, speedSmoothView * Time.fixedDeltaTime);
    }

    private float WhenPlayerRunsSpeedPlayer()
    {
        bool canApplyEffect = playerController.CharacterMovement.currentPercentToReachMaxVelocity > velocityPercentToApplyEffect;

        var value = canApplyEffect ?
        Mathf.Lerp(currentSpeedPositionX, FinalRunPlayer, speedAceleration * Time.deltaTime) :
        Mathf.Lerp(currentSpeedPositionX, 0.0f, speedDeceleration * Time.deltaTime);

        return value;
    }

}
