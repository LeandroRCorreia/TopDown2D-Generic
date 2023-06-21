using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventWeapon : MonoBehaviour
{

    public event Action OnTriggerDamage;
    public event Action animationFinished;


    private void AnimationFinishedTrigger()
    {
        animationFinished?.Invoke();
    }


    public void ActivedTriggerDamage()
    {
        OnTriggerDamage?.Invoke();
    }

}
