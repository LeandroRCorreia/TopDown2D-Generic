using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    
    [Header("Overlays")]

    [SerializeField] private HudGameplay hudGameplay;

    private void Awake() 
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        ShowHudOverlay();
        
    }

    public void ShowHudOverlay()
    {
        hudGameplay.gameObject.SetActive(true);


    }


}
