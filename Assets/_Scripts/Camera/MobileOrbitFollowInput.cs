using UnityEngine;
using Unity.Cinemachine;

public class MobileOrbitFollowInput : MonoBehaviour
{
    public CinemachineCamera cineCam;
    private CinemachineOrbitalFollow orbitalFollow;

    public float horizontalSensitivity = 0.2f;
    public float verticalSensitivity = 0.2f;

    void Start()
    {
        orbitalFollow = cineCam.GetComponent<CinemachineOrbitalFollow>();
        if (orbitalFollow == null)
        {
            Debug.LogError("CinemachineOrbitalFollow not found on the assigned camera.");
        }
    }

    void Update()
    {
        if (orbitalFollow == null) return;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;

                // Adjust horizontal rotation (Y-axis)
                orbitalFollow.HorizontalAxis.Value += delta.x * horizontalSensitivity;

                // Adjust vertical rotation (X-axis)
                orbitalFollow.VerticalAxis.Value -= delta.y * verticalSensitivity;
            }
        }
    }
}
