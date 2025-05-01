using UnityEngine;

public class ActivatorRaios : MonoBehaviour
{
    [SerializeField] public GameObject Celostato; // Check position of this
    [SerializeField] public GameObject Espelho; // Check rotation of this
    [SerializeField] public GameObject Raio; // This will be activated
    [SerializeField] public GameObject RaioAnterior;
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
        //Forma Optimizada
        //Calcular posição
        /*  float sqrThreshold = 0.5f * 0.5f;
         bool positionMatches = (Celostato.transform.localPosition - targetPosition).sqrMagnitude < sqrThreshold; */

        //Calcular angulos
        /*  float dot = Quaternion.Dot(Espelho.transform.localRotation, Quaternion.Euler(targetRotation));
         bool rotationMatches = dot > 0.9999619f; // ~cos(0.5°) */

        if (RaioAnterior == null || RaioAnterior.activeSelf)
        {
            ManageRaio();
        }


    }

    private void ManageRaio()
    {
        bool positionMatches = Vector3.Distance(Celostato.transform.localPosition, targetPosition) < 0.002f;
        bool rotationMatches = Quaternion.Angle(Espelho.transform.localRotation, Quaternion.Euler(targetRotation)) < 7f;

        Debug.Log("Posição: " + Celostato.name + Celostato.transform.localPosition);
        Debug.Log("Rotação: " + Espelho.name + Espelho.transform.localEulerAngles);

        if (rotationMatches && !soundPlayedRotation)
        {
            SoundFXManager.instance.PlaySoundFXClip(audioData.CorrectSound, transform, 1f);
            Debug.Log("Rotação Correta");
            soundPlayedRotation = true;
            wrongSoundEnabled = true;
        }
        else if (!rotationMatches && wrongSoundEnabled)
        {
            soundPlayedRotation = false; // Reset flag if rotation no longer matches
            SoundFXManager.instance.PlaySoundFXClip(audioData.WrongSound, transform, 0.3f);
            wrongSoundEnabled = false;
        }

        if (positionMatches && !soundPlayedPosition)
        {
            SoundFXManager.instance.PlaySoundFXClip(audioData.CorrectSound, transform, 1f);
            Debug.Log("Posição Correta");
            soundPlayedPosition = true;
            wrongSoundEnabled = true;
        }
        else if (!positionMatches && wrongSoundEnabled)
        {
            soundPlayedPosition = false; // Reset flag if position no longer matches
            SoundFXManager.instance.PlaySoundFXClip(audioData.WrongSound, transform, 0.3f);
            wrongSoundEnabled = false;
        }

        Raio.SetActive(positionMatches && rotationMatches);

    }

}
