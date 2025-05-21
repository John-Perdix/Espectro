using UnityEngine;
using Unity.Cinemachine;

[CreateAssetMenu(fileName = "NewCameraData", menuName = "Camera/Camera Data")]
public class CameraData : ScriptableObject
{
    public CinemachineVirtualCameraBase camera;
    public Sprite image;
    public string label;
}
