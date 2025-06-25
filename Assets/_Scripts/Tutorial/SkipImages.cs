using UnityEngine;
using UnityEngine.UI;

public class SkipImages : MonoBehaviour
{
    [Header("Tutorial Images Data")]
    [SerializeField] private TutorialImages tutorialImages; // Reference to ScriptableObject
    [SerializeField] private DotIndicator dotIndicator; // Reference to your DotIndicator

    [Header("Object to disable at end")]
    public GameObject objectToDisable;

    private int currentIndex = 0;
    private SpriteRenderer spriteRenderer;
    private Image uiImage;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiImage = GetComponent<Image>();
        //SetImage(0);
    }

    void Start()
    {
        dotIndicator.SetupDots(tutorialImages.images.Length);
        SetImage(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                NextImage();
            }
            else
            {
                PreviousImage();
            }
        }
    }

    public void NextImage()
    {
        if (tutorialImages == null || tutorialImages.images.Length == 0) return;

        // If at last image and user tries to go next, disable the object
        if (currentIndex == tutorialImages.images.Length - 1)
        {
            if (objectToDisable != null)
                objectToDisable.SetActive(false);
            return;
        }

        currentIndex = (currentIndex + 1) % tutorialImages.images.Length;
        SetImage(currentIndex);
    }

    public void PreviousImage()
    {
        if (tutorialImages == null || tutorialImages.images.Length == 0) return;
        currentIndex = (currentIndex - 1 + tutorialImages.images.Length) % tutorialImages.images.Length;
        SetImage(currentIndex);
    }

    private void SetImage(int index)
    {
        if (tutorialImages == null || tutorialImages.images.Length == 0) return;
        Sprite sprite = tutorialImages.images[index];
        if (spriteRenderer != null)
            spriteRenderer.sprite = sprite;
        if (uiImage != null)
            uiImage.sprite = sprite;

        dotIndicator.SetActiveDot(index);
    }
}
