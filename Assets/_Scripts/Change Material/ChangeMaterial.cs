using UnityEngine;
using UnityEngine.VFX;
using TMPro;

public class MaterialChangerUI : MonoBehaviour
{
    [Header("Materials")]
    public Material[] materials;

    [Header("Target Renderer")]
    public Renderer targetRenderer;

    [SerializeField] VisualEffect[] raios;
    public Color[] color;

    [Header("UI")]
    public TextMeshProUGUI filtroAtivo;

    private int currentIndex = 1; // Start at 1 to skip index 0

    void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        // Apply default material at index 0
        if (materials.Length > 0 && targetRenderer != null)
        {
            //targetRenderer.material = materials[0];

            for (int i = 0; i < raios.Length; i++)
            {
                raios[i].SetVector4("Cor", color[0]);
            }
        }
        filtroAtivo.text = "Filtros - Sem Filtro";
        //UpdateFiltroAtivoText();
    }

    // Toggle to next material (skipping index 0)
    public void ToggleMaterial()
    {
        if (materials.Length <= 1 || targetRenderer == null)
            return;

        currentIndex++;
        if (currentIndex >= materials.Length)
            currentIndex = 1; // Loop back to 1, skipping 0

        targetRenderer.material = materials[currentIndex];

        for (int i = 0; i < raios.Length; i++)
        {
            raios[i].SetVector4("Cor", color[currentIndex]);
        }

        UpdateFiltroAtivoText();
    }

    private void UpdateFiltroAtivoText()
    {
        if (filtroAtivo == null) return;

        if (currentIndex == 0)
            filtroAtivo.text = "Filtros - Sem Filtro";
        else if (currentIndex == 1)
            filtroAtivo.text = "Filtro H Alpha";
        else if (currentIndex == 2)
            filtroAtivo.text = "Filtro k3";
        else
            filtroAtivo.text = "";
    }

    // Touch or click detection
    void OnMouseDown()
    {
        ToggleMaterial();
    }
}
