using UnityEngine;

public class ResetButtonManager : MonoBehaviour
{
    public GameObject dialogoContainer; // Reference to the dialogo container
    public GameObject buttonReset;

    void Update()
    {
        if (dialogoContainer != null && buttonReset != null)
        {
            buttonReset.SetActive(!dialogoContainer.activeSelf);
        }
    }
}
