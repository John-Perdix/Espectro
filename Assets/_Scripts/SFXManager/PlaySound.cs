using UnityEngine;

public class PlaySoundClip : MonoBehaviour
{
    private AudioData audioData;

    public void PlaySound()
    {
        SoundFXManager.instance.PlaySoundFXClip(audioData.BeginSound, transform, 1f);
    }
}