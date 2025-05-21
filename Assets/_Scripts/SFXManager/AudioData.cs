using UnityEngine;

[CreateAssetMenu(fileName = "NewAudio", menuName = "AudioClips")]
public class AudioData : ScriptableObject
{
    public AudioClip CorrectSound;
    public AudioClip WrongSound;
    public AudioClip BeginSound;
    public AudioClip MoveSound;

}