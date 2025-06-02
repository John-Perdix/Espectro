using UnityEngine;

public class CupulaTouchToggle : MonoBehaviour
{
    private MoveCupula moveCupula;
    private bool toggleState = false; // false = next is forward, true = next is backward
    private bool isTouching = false;

    void Start()
    {
        moveCupula = GetComponent<MoveCupula>();
    }

    void OnMouseDown()
    {
        if (moveCupula == null) return;

        if (!toggleState)
            moveCupula.MoveForward();
        else
            moveCupula.MoveBackward();

        toggleState = !toggleState;
        isTouching = true;
    }

    void OnMouseUp()
    {
        if (moveCupula == null) return;
        moveCupula.Stop();
        isTouching = false;
    }

    // For touch devices
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                if (touch.phase == TouchPhase.Began && !isTouching)
                {
                    OnMouseDown();
                }
                else if (touch.phase == TouchPhase.Ended && isTouching)
                {
                    OnMouseUp();
                }
            }
        }
    }
}