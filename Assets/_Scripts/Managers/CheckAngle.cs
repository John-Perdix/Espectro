using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CheckAngle : MonoBehaviour
{

    [Tooltip("'Physical' modeld camera from the instrument, not the camera from the Unity scene")]
    [SerializeField] GameObject camara;//this is the camara mesh object modeld from the instrument, not the camera from the unity scene
    [SerializeField] Vector3 rotation = new Vector3(0, 0, 0); // Default rotation vector
    [SerializeField] float angleThreshold = 10f; // Angle threshold for checking
                                                 // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public GameObject[] Raios;
    [SerializeField] public GameObject RaioAnterior;
    [SerializeField] private float angloMaior = 0f; // Minimum angle for ray activation
    [SerializeField] private float angloMenor = 180f; // Maximum angle for ray activation

    [Tooltip("Audio data for sound effects")]
    public AudioData audioData;

    private bool soundPlayed = false;
    void Start()
    {
        foreach (GameObject raio in Raios)
        {
            raio.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (camara != null)
        {
            if (Quaternion.Angle(transform.localRotation, Quaternion.Euler(rotation)) < angleThreshold)
            {
                // If the rotation matches the specified vector, enable the GameObject
                camara.layer = LayerMask.NameToLayer("Interactions Colliders Touch"); // Enable the GameObject by setting it to the Default layer
                if (!soundPlayed && audioData != null && audioData.CorrectSound != null && SoundFXManager.instance != null)
                {
                    // Play the correct sound effect
                    SoundFXManager.instance.PlaySoundFXClip(audioData.CorrectSound, transform, 1f);
                    soundPlayed = true;
                }
            }
            else
            {
                // If the rotation does not match, move the GameObject to the Default layer
                camara.layer = LayerMask.NameToLayer("Default");
                soundPlayed = false; // Reset so sound can play again when re-entering the angle
            }

            if (RaioAnterior != null)
            {
                Debug.Log($"Checking RaioAnterior: {RaioAnterior.activeSelf}, IsAngleInRange: {IsAngleInRange(transform.localEulerAngles.z, angloMenor, angloMaior)}");
                // Check if the previous ray is active
                if (RaioAnterior.activeSelf && IsAngleInRange(transform.localEulerAngles.z, angloMenor, angloMaior))
                {
                    // If the previous ray is active, enable the current ray
                    foreach (GameObject raio in Raios)
                    {
                        //Debug.Log($"{raio.name} parent activeSelf: {raio.transform.parent?.gameObject.activeSelf}");
                        raio.SetActive(true);
                    }
                }
                else
                {
                    // Otherwise, disable the current rays
                    foreach (GameObject raio in Raios)
                    {
                        raio.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogWarning("RaioAnterior is not assigned or is null.");
                foreach (GameObject raio in Raios)
                {
                    raio.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Camara is not assigned in the inspector.");
            foreach (GameObject raio in Raios)
            {
                raio.SetActive(false);
            }
        }
    }

    private bool IsAngleInRange(float angle, float min, float max)
    {
        angle = (angle + 360f) % 360f;
        min = (min + 360f) % 360f;
        max = (max + 360f) % 360f;

        if (min < max)
            return angle > min && angle < max;
        else // Range wraps around 0
            return angle > min || angle < max;
    }
}
