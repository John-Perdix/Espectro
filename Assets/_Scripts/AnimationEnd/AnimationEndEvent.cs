using UnityEngine;
using UnityEngine.Events;

public class AnimationEndEvent : MonoBehaviour
{
    [Header("Event to invoke at the end of the animation")]
    public UnityEvent onAnimationEnd;

    // Call this from an Animation Event at the end of your animation
    public void OnAnimationEnd()
    {
        if (onAnimationEnd != null)
            onAnimationEnd.Invoke();
    }
}