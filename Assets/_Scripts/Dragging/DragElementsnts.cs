using UnityEngine;

public class DragElements : MonoBehaviour
{
    [Header("Axis locks")]
    public bool blockX, blockY, blockZ;

    [Header("Snapping")]
    public Transform snapTarget;      // drag the snap point here
    public float snapDistance = 0.3f; // how close is “close enough” (world units)

    private Vector3 offSet;
    private float zCoordinate;

    /* ---------- Drag logic ---------- */

    private void OnMouseDown()
    {
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        offSet      = transform.position - GetMouseWorldPoint();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPoint() + offSet;
    }

    private void OnMouseUp()
    {
        // If we have a snap target and we're close enough, snap!
        if (snapTarget != null &&
            Vector3.Distance(transform.position, snapTarget.position) <= snapDistance)
        {
            transform.position = snapTarget.position;
        }
    }

    /* ---------- Helpers ---------- */

    private Vector3 GetMouseWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;

        float x = 0, y = 0, z = 0;

        if (!blockX) x = mousePoint.x;
        if (!blockY) y = mousePoint.y;
        if (!blockZ) z = mousePoint.z;

        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));
    }
}
