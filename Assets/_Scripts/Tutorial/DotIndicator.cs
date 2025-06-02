using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DotIndicator : MonoBehaviour
{
    public GameObject dotPrefab; // Assign your dot prefab in the Inspector
    public Transform dotContainer; // Assign the Horizontal Layout Group panel
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    private List<Image> dots = new List<Image>();

    public void SetupDots(int count)
    {
        // Clear old dots
        foreach (Transform child in dotContainer)
            Destroy(child.gameObject);
        dots.Clear();

        // Create new dots
        for (int i = 0; i < count; i++)
        {
            var dot = Instantiate(dotPrefab, dotContainer).GetComponent<Image>();
            dot.color = inactiveColor;
            dots.Add(dot);
        }
    }

    public void SetActiveDot(int index)
    {
        for (int i = 0; i < dots.Count; i++)
            dots[i].color = (i == index) ? activeColor : inactiveColor;
    }
}