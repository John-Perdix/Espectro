using UnityEngine;

public class Billboard : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
