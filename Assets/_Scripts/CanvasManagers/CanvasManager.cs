using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
     public float moveAmount = 200f;
     
    public bool isActive;

    public void Start()
    {
        gameObject.SetActive(isActive);
    }
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void MoveDown()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition -= new Vector2(0, moveAmount);
        }
    }

    public void MoveUp()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition -= new Vector2(moveAmount, 0);
        }
    }
    
}