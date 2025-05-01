using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutlineManager : MonoBehaviour
{
    [Tooltip("Reference to the outline render feature")]
    public ScriptableRendererFeature outlineRenderFeaturePC;
    public ScriptableRendererFeature outlineRenderFeatureMobile;

    [Tooltip("Delay in seconds before starting to blink")]
    public float delayInSeconds = 30f;

    [Tooltip("Blink interval in seconds")]
    public float blinkInterval = 1f;

    [Tooltip("Component to change text in the messages")]
    public Alertas alertaComponent;
    public CanvasToggle canvasToggle;
    private bool alerta;

    private void Start()
    {
        if (outlineRenderFeaturePC == null && outlineRenderFeatureMobile == null)
        {
            Debug.LogError("Outline Render Features are not assigned in the OutlineManager.");
            return;
        }

        // Initially disable the features
        if (outlineRenderFeaturePC != null) outlineRenderFeaturePC.SetActive(false);
        if (outlineRenderFeatureMobile != null) outlineRenderFeatureMobile.SetActive(false);
        alerta = false;

        // Start blinking coroutine
        StartCoroutine(BlinkRenderFeaturesAfterDelay());
    }

    private IEnumerator BlinkRenderFeaturesAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        Debug.Log("Starting to blink outline features...");

        while (true)
        {
            ToggleRenderFeature(outlineRenderFeaturePC);
            ToggleRenderFeature(outlineRenderFeatureMobile);
            yield return new WaitForSeconds(blinkInterval);
            alerta = true;
        }
    }

    private void Update()
    {
        if (alerta){
        alertaComponent.SetText("Interaja com os objetos destacados");
        canvasToggle.Show();
        }
    }

    private void ToggleRenderFeature(ScriptableRendererFeature feature)
    {
        if (feature != null)
        {
            feature.SetActive(!feature.isActive);
        }
    }
}
