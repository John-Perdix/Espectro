using UnityEngine;
using UnityEngine.UI;

public class SpriteFlipbook : MonoBehaviour
{
    public Image targetImage;      // Assign the UI Image here
    public Sprite[] frames;        // Assign your 2 sliced sprites here
    public float frameRate = 0.5f; // Time between frames (in seconds)

    private int currentFrame;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frames.Length;
            targetImage.sprite = frames[currentFrame];
        }
    }
}
