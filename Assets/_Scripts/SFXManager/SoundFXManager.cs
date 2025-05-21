using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
    }

    // Update is called once per frame
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn the gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioclip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get length of clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after played
        Destroy(audioSource.gameObject, clipLength);
        
    }


    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume, float minPitch = 1f, float maxPitch = 1f)
    {
        //random index
        int rand = Random.Range(0, audioClip.Length);
        //spawn the gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioclip
        audioSource.clip = audioClip[rand];

        //assign volume
        audioSource.volume = volume;

        //assign random pitch
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        //play sound
        audioSource.Play();

        //get length of clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after played
        Destroy(audioSource.gameObject, clipLength);
        
    }

    internal static void PlaySoundFXClip(AudioClip soundClip, Vector3 position, double v)
    {
        throw new System.NotImplementedException();
    }
}
