using UnityEngine;
using UnityEngine.UI;

public class TargetProximitySlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ActivatorRaios activatorRaios;
    [SerializeField] private CheckAngle checkAngle;
    [SerializeField] private Slider positionSlider;
    [SerializeField] private Slider rotationSlider;
    [SerializeField] private Slider angleSlider; // New slider for CheckAngle
    
    [Header("Slider Settings")]
    [SerializeField] private float maxPositionDistance = 0.01f;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float maxAngleRange = 180f; // Maximum angle range for CheckAngle
    
    [Header("Visual Feedback")]
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color incorrectColor = Color.red;
    [SerializeField] private Color neutralColor = Color.yellow;

    private Image positionHandleImage;
    private Image rotationHandleImage;
    private Image angleHandleImage; // New handle for angle slider

    void Start()
    {
        SetupSliders();
    }

    void Update()
    {
        if (activatorRaios != null)
        {
            UpdatePositionSlider();
            UpdateRotationSlider();
        }
        
        if (checkAngle != null)
        {
            UpdateAngleSlider();
        }
    }

    private void SetupSliders()
    {
        // Setup position slider
        if (positionSlider != null)
        {
            positionSlider.minValue = -1f;
            positionSlider.maxValue = 1f;
            positionSlider.value = 0f;
            positionHandleImage = positionSlider.handleRect.GetComponent<Image>();
        }

        // Setup rotation slider
        if (rotationSlider != null)
        {
            rotationSlider.minValue = -1f;
            rotationSlider.maxValue = 1f;
            rotationSlider.value = 0f;
            rotationHandleImage = rotationSlider.handleRect.GetComponent<Image>();
        }

        // Setup angle slider
        if (angleSlider != null)
        {
            angleSlider.minValue = -1f;
            angleSlider.maxValue = 1f;
            angleSlider.value = 0f;
            angleHandleImage = angleSlider.handleRect.GetComponent<Image>();
        }
    }

    private void UpdatePositionSlider()
    {
        if (positionSlider == null || activatorRaios.Celostato == null) return;

        Vector3 currentPosition = activatorRaios.Celostato.transform.localPosition;
        Vector3 targetPosition = activatorRaios.targetPosition;
        
        Vector3 difference = currentPosition - targetPosition;
        float distance = difference.magnitude;
        
        float normalizedValue = Mathf.Clamp(distance / maxPositionDistance, -1f, 1f);
        
        if (difference.x < 0)
            normalizedValue = -normalizedValue;

        positionSlider.value = normalizedValue;
        UpdateSliderColor(positionHandleImage, Mathf.Abs(normalizedValue), activatorRaios.tolerance);
    }

    private void UpdateRotationSlider()
    {
        if (rotationSlider == null || activatorRaios.Espelho == null) return;

        Quaternion currentRotation = activatorRaios.Espelho.transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(activatorRaios.targetRotation);
        
        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);
        float normalizedValue = Mathf.Clamp(angleDifference / maxRotationAngle, -1f, 1f);
        
        Vector3 currentEuler = currentRotation.eulerAngles;
        Vector3 targetEuler = activatorRaios.targetRotation;
        float yDifference = Mathf.DeltaAngle(currentEuler.y, targetEuler.y);
        
        if (yDifference < 0)
            normalizedValue = -normalizedValue;

        rotationSlider.value = normalizedValue;
        UpdateSliderColor(rotationHandleImage, Mathf.Abs(normalizedValue), 7f / maxRotationAngle);
    }

    private void UpdateAngleSlider()
    {
        if (angleSlider == null) return;

        // Get the current angle from CheckAngle's transform
        float currentAngle = checkAngle.transform.localEulerAngles.z;
        
        // Get the target angle from CheckAngle's rotation field
        Vector3 targetRotation = GetCheckAngleTargetRotation();
        float targetAngle = targetRotation.z;
        
        // Calculate angle difference
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        
        // Normalize to slider range (-1 to 1)
        float normalizedValue = Mathf.Clamp(angleDifference / maxAngleRange, -1f, 1f);

        angleSlider.value = normalizedValue;
        
        // Update color based on CheckAngle's angleThreshold
        float threshold = GetCheckAngleThreshold() / maxAngleRange;
        UpdateSliderColor(angleHandleImage, Mathf.Abs(normalizedValue), threshold);
    }

    private void UpdateSliderColor(Image handleImage, float distance, float tolerance)
    {
        if (handleImage == null) return;

        if (distance <= tolerance)
        {
            handleImage.color = correctColor;
        }
        else if (distance > 0.5f)
        {
            handleImage.color = incorrectColor;
        }
        else
        {
            handleImage.color = neutralColor;
        }
    }

    // Helper methods to access CheckAngle's private fields via reflection or public properties
    // You might need to make these fields public in CheckAngle or add public getters
    private Vector3 GetCheckAngleTargetRotation()
    {
        // You'll need to make the 'rotation' field public in CheckAngle or add a getter
        // For now, assuming you make it public or add: public Vector3 TargetRotation => rotation;
        return new Vector3(0, 0, 0); // Replace with: checkAngle.TargetRotation;
    }

    private float GetCheckAngleThreshold()
    {
        // You'll need to make the 'angleThreshold' field public in CheckAngle or add a getter
        // For now, assuming you make it public or add: public float AngleThreshold => angleThreshold;
        return 10f; // Replace with: checkAngle.AngleThreshold;
    }

    // Optional: Public method to set custom max distances
    public void SetMaxDistances(float maxPos, float maxRot, float maxAngle)
    {
        maxPositionDistance = maxPos;
        maxRotationAngle = maxRot;
        maxAngleRange = maxAngle;
    }
}