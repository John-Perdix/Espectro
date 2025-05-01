using UnityEngine;

[CreateAssetMenu(fileName = "NewAudio", menuName = "AudioClips")]
public class AudioData : ScriptableObject
{
    public AudioClip CorrectSound;
    public AudioClip WrongSound;

}