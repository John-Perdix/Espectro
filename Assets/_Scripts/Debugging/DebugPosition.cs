using UnityEngine;

public class DebugPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Posição de" + name + ": " + transform.localPosition);
        Debug.Log($"{name} localPosition: {transform.localPosition}, worldPosition: {transform.position}");
    }
}
