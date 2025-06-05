using System.Collections;
using UnityEngine;

public class MoveCupula : MonoBehaviour
{
    [SerializeField] public float moveForce = 10f;
    [SerializeField] Vector3 axis = new Vector3(1, 0, 0);
    [SerializeField] ActivatorRaios ativadorRaios;
    [SerializeField] float tolerancia = 0.5f;
    Vector3 snapPosition;
    bool snapEnabled;
    [SerializeField] private float stepSize = 1f;

    [Header("Audio Setup")]
    [SerializeField] AudioData defaultAudioData; // ScriptableObject reference
    [SerializeField] AudioClip overrideAudioClip; // Optional override

    private Rigidbody rb;
    private int direction = 0; // 1 = frente, -1 = trás, 0 = parado
    private Coroutine moveSoundCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (ativadorRaios != null)
        {
            snapPosition = ativadorRaios.targetPosition;
            snapEnabled = true;
        }
        else
        {
            snapEnabled = false;
        }
    }

    void FixedUpdate()
    {

        Vector3 currentPosition = transform.position;
        float diff = (currentPosition - snapPosition).sqrMagnitude;

        if (direction != 0)
        {
            rb.AddForce(axis * direction * moveForce * stepSize, ForceMode.Force);

            AudioClip clipToUse = (overrideAudioClip != null)
                ? overrideAudioClip
                : (defaultAudioData != null ? defaultAudioData.MoveSound : null);

            if (clipToUse != null && moveSoundCoroutine == null)
            {
                moveSoundCoroutine = StartCoroutine(PlayMoveSoundLoop(clipToUse));
            }
        }
        else
        {
            if (moveSoundCoroutine != null)
            {
                StopCoroutine(moveSoundCoroutine);
                moveSoundCoroutine = null;
            }
        }

        if (diff < tolerancia && snapEnabled)
        {
            SnapToPosition();
        }

        //Debug.Log("Posição de "+ name +": " + transform.localPosition);
    }

    private IEnumerator PlayMoveSoundLoop(AudioClip clip)
    {
        while (direction != 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1f);

            float forceFactor = Mathf.Clamp(moveForce, 0.1f, 100f); // Prevent division by zero
            float interval = (clip.length * 0.8f) * (10f / forceFactor); // 10 is a reference value
            yield return new WaitForSeconds(interval);
        }
        moveSoundCoroutine = null;
    }

    // BOTÕES
    public void MoveForward()
    {
        direction = 1;

    }

    public void MoveBackward()
    {
        direction = -1;
    }

    public void Stop()
    {
        direction = 0;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void SnapToPosition()
    {
        rb.position = snapPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    /* public void PlaySoundFX(AudioClip[] audioClip)
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(audioClip, transform, 1f, minPitch: 0.8f, maxPitch: 1.2f);
    } */

}
