using UnityEngine;
using UnityEngine.Events;

public class CamaraShutterClick : MonoBehaviour
{
    [Header("Event to invoke on touch")]
    public UnityEvent onTouched;
    [Header("Audio Data")]
    [SerializeField] AudioData audioData;
    [Header("Animacao de fotografia")]
    [SerializeField] CanvasToggle animacaoFotografia;

    [Header("Raio")]
    [SerializeField] GameObject raio;

    // Called when the object is clicked/tapped (requires a collider)
    private void OnMouseDown()
    {
        // Check if the GameObject's layer is "Interactions Colliders Touch"
        if (gameObject.layer == LayerMask.NameToLayer("Interactions Colliders Touch"))
        {
            // Check if raio is assigned and active
            if (raio != null && audioData != null && audioData.CamaraSound != null && raio.activeInHierarchy)
            {
                if (onTouched != null)
                    onTouched.Invoke();
                SoundFXManager.instance.PlaySoundFXClip(audioData.CamaraSound, transform, 1f);
            }
        }
    }
}
