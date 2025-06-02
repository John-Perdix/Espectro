using UnityEngine;
using System.Collections;

public class TimerActivateObject : MonoBehaviour
{
    [Header("Timer duration in seconds")]
    public float timerDuration = 5f;

    [Header("Object to activate")]
    public GameObject[] objectToActivate;

    // Call this function to start the timer
    public void StartActivationTimer()
    {
        StartCoroutine(ActivationTimerCoroutine());
    }

    private IEnumerator ActivationTimerCoroutine()
    {
        yield return new WaitForSeconds(timerDuration);
        if (objectToActivate != null)
            foreach (GameObject obj in objectToActivate)
            {

                obj.SetActive(true);
                //Debug.Log($"Object {obj.name} activated after {timerDuration} seconds.");
            }
    }
}
