using System;
using UnityEngine;

public class AnimationEventPlayer : MonoBehaviour
{

    public event Action JumpTriggerEvent;

    private void JumpTrigger()
    {
        JumpTriggerEvent?.Invoke();
    }


}
