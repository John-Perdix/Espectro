using UnityEngine;

public class ActivatorRaios : MonoBehaviour
{
    [SerializeField] public GameObject Celostato; // Check position of this
    [SerializeField] public GameObject Espelho; // Check rotation of this
    [SerializeField] public GameObject[] Raios; // Now supports multiple raios!
    [SerializeField] public GameObject RaioAnterior;
    [SerializeField] public float tolerance = 0.002f;
    public AudioData audioData;

    public Vector3 targetPosition;
    public Vector3 targetRotation; // Euler angles (e.g., 0, 90, 0)
    private bool soundPlayedPosition;
    private bool soundPlayedRotation;
    private bool wrongSoundEnabled;

    void Start()
    {
        soundPlayedPosition = false;
        soundPlayedRotation = false;
        wrongSoundEnabled = false;
    }
    void Update()
    {
        if (RaioAnterior == null || RaioAnterior.activeSelf)
        {
            ManageRaio();
        }
    }

    private void ManageRaio()
    {
        float positionDistance = (Celostato.transform.localPosition - targetPosition).sqrMagnitude;
        float positionTolerance = tolerance * tolerance;

        bool positionMatches = positionDistance < positionTolerance;
        bool rotationMatches = Quaternion.Angle(Espelho.transform.localRotation, Quaternion.Euler(targetRotation)) < 7f;

        //Debug.Log($"[{Celostato.name}] sqrDistance: {positionDistance}, Tolerance: {positionTolerance}, Matches: {positionMatches}");
        //Debug.Log($"[{Celostato.name}] Position: {Celostato.transform.localPosition}, Target: {targetPosition}, Distance: {Vector3.Distance(Celostato.transform.localPosition, targetPosition)}");
        //Debug.Log($" Position matches variable of [{Celostato.name}] is {positionMatches}");

        if (Espelho == null)
        {
            return;
        }
        else
        {
            if (rotationMatches && !soundPlayedRotation)
            {
                SoundFXManager.instance.PlaySoundFXClip(audioData.CorrectSound, transform, 1f);
                //Debug.Log("Rotação Correta");
                soundPlayedRotation = true;
                wrongSoundEnabled = true;
            }
            else if (!rotationMatches && wrongSoundEnabled)
            {
                soundPlayedRotation = false;
                SoundFXManager.instance.PlaySoundFXClip(audioData.WrongSound, transform, 0.3f);
                wrongSoundEnabled = false;
            }
        }

        if (positionMatches && !soundPlayedPosition)
        {
            SoundFXManager.instance.PlaySoundFXClip(audioData.CorrectSound, transform, 1f);
            soundPlayedPosition = true;
        }
        else if (!positionMatches)
        {
            soundPlayedPosition = false;
        }

        // Activate/deactivate all raios
        bool active = positionMatches && rotationMatches;
        if (Raios != null)
        {
            foreach (var raio in Raios)
            {
                if (raio != null)
                {
                    raio.SetActive(active);
                    //Debug.Log($"Setting {raio.name} active={active}");
                }
                else
                {
                    //Debug.LogWarning("Null entry in Raios array!");
                }
            }
        }

        //Debug.Log($" Position matches variable of [{Celostato.name}] is {positionMatches} and Rotation Matches is {rotationMatches}");
    }
}
