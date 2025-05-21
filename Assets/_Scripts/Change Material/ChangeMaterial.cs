using UnityEngine;
using UnityEngine.VFX;

public class MaterialChangerUI : MonoBehaviour
{
    [Header("Materials")]
    public Material[] materials;

    [Header("Target Renderer")]
    public Renderer targetRenderer;

    //[SerializeField] GameObject Raio;
    [SerializeField] VisualEffect[] raios;
    public Color[] color;


    void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        // Optionally apply default material at index 0
        if (materials.Length > 0 && targetRenderer != null)
        {
            targetRenderer.material = materials[0];
            
            for (int i = 0; i < raios.Length; i++)
            {
                raios[i].SetVector4("Cor", color[0]);
            }
        }
    }

    // This method can be called by a UI Button, passing the index in the Inspector
    public void SetMaterialByIndex(int index)
    {
        if (materials.Length == 0 || targetRenderer == null)
            return;

        if (index >= 0 && index < materials.Length)
        {
            targetRenderer.material = materials[index];

            for (int i = 0; i < raios.Length; i++)
            {
                raios[i].SetVector4("Cor", color[index]);
            }
        }
        else
        {
            Debug.LogWarning("Invalid material index: " + index);
        }
    }
}
