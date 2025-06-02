using UnityEngine;
//ScriptableObject that holds the tutorial images

[CreateAssetMenu(fileName = "TutorialImages", menuName = "TutorialImages")]
public class TutorialImages : ScriptableObject
{
     [SerializeField] public Sprite[] images;
}
