using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutlineManager : MonoBehaviour
{
    [Tooltip("Reference to the outline render feature")]
    public ScriptableRendererFeature outlineDragRenderFeaturePC;
    public ScriptableRendererFeature outlineDragRenderFeatureMobile;
    public ScriptableRendererFeature outlineTouchRenderFeaturePC;
    public ScriptableRendererFeature outlineTouchRenderFeatureMobile;

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
        if (outlineDragRenderFeaturePC == null && outlineDragRenderFeatureMobile == null &&
            outlineTouchRenderFeaturePC == null && outlineTouchRenderFeatureMobile == null)
        {
            Debug.LogError("Outline Render Features are not assigned in the OutlineManager.");
            return;
        }

        // Initially disable all features
        if (outlineDragRenderFeaturePC != null) outlineDragRenderFeaturePC.SetActive(false);
        if (outlineDragRenderFeatureMobile != null) outlineDragRenderFeatureMobile.SetActive(false);
        if (outlineTouchRenderFeaturePC != null) outlineTouchRenderFeaturePC.SetActive(false);
        if (outlineTouchRenderFeatureMobile != null) outlineTouchRenderFeatureMobile.SetActive(false);
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
            ToggleRenderFeature(outlineDragRenderFeaturePC);
            ToggleRenderFeature(outlineDragRenderFeatureMobile);
            ToggleRenderFeature(outlineTouchRenderFeaturePC);
            ToggleRenderFeature(outlineTouchRenderFeatureMobile);
            yield return new WaitForSeconds(blinkInterval);
            alerta = true;
        }
    }

    private void Update()
    {
        if (alerta)
        {
            alertaComponent.SetText("Interaja com os objetos destacados");
            canvasToggle.Show();
            alerta = false;
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
