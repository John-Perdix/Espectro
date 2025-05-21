using UnityEngine;

public class DebugPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Posição de" + name + ": " + transform.localPosition);
        Debug.Log($"{name} localPosition: {transform.localPosition}, worldPosition: {transform.position}");
    }
}
