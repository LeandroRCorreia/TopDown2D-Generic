using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speedBg = 5;

    [SerializeField] private PlayerController playerControl;
    [SerializeField] private Camera mainCamera;
    [SerializeField] SpriteRenderer[] backGrounds;

    private float bgSize;

    private void Start() 
    {
        bgSize = backGrounds[0].bounds.size.x;
        
    }
    
    private void FixedUpdate()
    {
        ApplyVelocityOnBg();
        UpdateEndlessBg();
    }

    private void UpdateEndlessBg()
    {
        CheckRight();
        CheckLeft();
    }

    private void CheckRight()
    {
        var bgRightPosition = backGrounds[1].transform.position;
        var IsBgPassBoundRightCamera = IsBgPassedBorderRight(bgRightPosition, bgSize);

        if (IsBgPassBoundRightCamera)
        {
            Vector3 bgCenter = backGrounds[1].transform.position;
            var safeNewPosition = bgCenter + Vector3.right * bgSize;
            backGrounds[0].transform.position = safeNewPosition;
            Swap();

        }


    }

    private void CheckLeft()
    {
        var bgLeftPosition = backGrounds[0].transform.position;
        var IsBgPassBoundLeftCameara = IsBgPassedBorderLeft(bgLeftPosition, bgSize);
        if (IsBgPassBoundLeftCameara)
        {
            Vector3 bgCenter = backGrounds[0].transform.position;
            var safeNewPosition = bgCenter + Vector3.left * bgSize;
            backGrounds[1].transform.position = safeNewPosition;
            Swap();
        }
    }

    private void ApplyVelocityOnBg()
    {
        if(speedBg == 0) return;
        float backgroundDirX = Mathf.Clamp(playerControl.CharacterMovement.VelocityX, -1, 1);
        if(backgroundDirX == 0) return;
        
        Vector3 bgDir = new()
        {
            x = backgroundDirX * -1
        };
        
        foreach (SpriteRenderer bg in backGrounds)
        {
            bg.transform.position += bgDir * speedBg * Time.fixedDeltaTime;
        }

    }

    private void Swap()
    {
        var aux = backGrounds[0];
        backGrounds[0] = backGrounds[1];
        backGrounds[1] = aux;
    }

    private bool IsBgPassedBorderLeft(Vector3 center, float width)
    {
        Vector3 boundBg = center + (Vector3.left * width * 0.5f);
        var cameraNormalized = mainCamera.WorldToViewportPoint(boundBg);
        Debug.Log("Left: " + cameraNormalized);
        return cameraNormalized.x >= 0;
    }

    private bool IsBgPassedBorderRight(Vector3 center, float width)
    {
        Vector3 boundBg = center + (Vector3.right * width * 0.5f);
        var cameraNormalized = mainCamera.WorldToViewportPoint(boundBg);
        Debug.Log("Right: " + cameraNormalized);

        return cameraNormalized.x <= 1;
    }

}
