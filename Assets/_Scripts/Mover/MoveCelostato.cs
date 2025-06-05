using UnityEngine;


public class MoveCelostato : MonoBehaviour
{
    [SerializeField] private float rotationForce = 10f;
    [SerializeField] Vector3 axis = new Vector3(0,0,1);
    [SerializeField] public MoveCupula moveCupula;
    [SerializeField] private float multiplicadorMover = 1f; // Tolerance for snapping to position
    private Rigidbody rb;
    private bool isDragging = false;
    public static bool IsAnyDragging { get; private set; } = false;
    private Vector2 lastTouchPosition;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.angularVelocity = Vector3.zero;
    }
    
    void Update()
    {
        // Handle mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // New touch began
            if (touch.phase == TouchPhase.Began)
            {
                // Check if this specific GameObject was touched
                if (IsTouchingThisObject(touch.position))
                {
                    isDragging = true;
                    IsAnyDragging = true; // Set global flag
                    lastTouchPosition = touch.position;
                    ////Debug.Log("Started touching lever");
                    ////Debug.Log("touch.position: " + lastTouchPosition);
                }
            }
            // Touch is moving<
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                //Debug.Log("Dragging is being detected");
                // Apply rotation based on touch movement
                Vector2 currentPosition = touch.position;
                float dragDistance = currentPosition.x - lastTouchPosition.x;
                rb.AddTorque(axis * dragDistance * rotationForce * Time.deltaTime);
                lastTouchPosition = currentPosition;
                moveOnDrag(dragDistance);
                
            }
            // Touch ended
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                UpdateIsAnyDragging(); // Recalculate global flag
                moveCupula?.Stop();
            }
        }
        
        // Editor testing with mouse
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchingThisObject(Input.mousePosition))
            {
                isDragging = true;
                IsAnyDragging = true;
                lastTouchPosition = Input.mousePosition;
                //Debug.Log("Started clicking lever");
                //Debug.Log("touch.position: " + lastTouchPosition);
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            //Debug.Log("Dragging is being detected");
            Vector2 currentPosition = Input.mousePosition;
            float dragDistance = currentPosition.x - lastTouchPosition.x;
            rb.AddTorque(axis * dragDistance * rotationForce * Time.deltaTime);
            lastTouchPosition = currentPosition;
            //Debug.Log(dragDistance);
            moveOnDrag(dragDistance);

            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            UpdateIsAnyDragging();
            moveCupula?.Stop();
        }
        #endif
    }
    
     private void UpdateIsAnyDragging()
    {
        // Check all MoveCelostato instances and update static flag
        foreach (MoveCelostato m in FindObjectsByType<MoveCelostato>(FindObjectsSortMode.None))
        {
            if (m.isDragging)
            {
                Debug.Log("Dragging in progress!");
                IsAnyDragging = true;
                return;
            }
        }
        IsAnyDragging = false;
    }

    public bool IsDragging => isDragging;
    
    // Helper method to check if this specific GameObject is being touched
    private bool IsTouchingThisObject(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hit THIS GameObject specifically
            //Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            return hit.collider.gameObject == this.gameObject;
        }

        //Debug.Log("Raycast hit nothing");
        return false;
    }

    private void moveOnDrag(float dragDistance){
        if (moveCupula != null){
        moveCupula.moveForce = Mathf.Abs(dragDistance*multiplicadorMover);
        if(dragDistance > 0){
                moveCupula.MoveForward();
            }
            else if(dragDistance < 0)
            {
                moveCupula.MoveBackward();
            }
            else if (!isDragging){
                moveCupula.Stop();
            }
    }else{
        return;
    }
    }

    void OnDrawGizmosSelected()
{
    var joint = GetComponent<ConfigurableJoint>();
    if (joint == null) return;

    Vector3 origin = transform.position;

    Gizmos.color = Color.red; // Primary axis
    Gizmos.DrawLine(origin, origin + transform.rotation * joint.axis);

    Gizmos.color = Color.green; // Secondary axis
    Gizmos.DrawLine(origin, origin + transform.rotation * joint.secondaryAxis);

    Gizmos.color = Color.blue; // Implied Z-axis
    Vector3 zAxis = Vector3.Cross(joint.axis, joint.secondaryAxis);
    Gizmos.DrawLine(origin, origin + transform.rotation * zAxis);
}
}