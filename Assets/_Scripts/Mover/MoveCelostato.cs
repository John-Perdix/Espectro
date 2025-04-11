using UnityEngine;


public class MoveCelostato : MonoBehaviour
{
    [SerializeField] private float rotationForce = 500f;
    [SerializeField] Vector3 axis = new Vector3(0,0,1);
    [SerializeField] public MoveCupula moveCupula;
    private Rigidbody rb;
    private bool isDragging = false;
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
                
            }
            // Touch ended
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
        
        // Editor testing with mouse
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchingThisObject(Input.mousePosition))
            {
                isDragging = true;
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
            Debug.Log(dragDistance);
            if(dragDistance > 0){
                moveCupula.MoveForward();
            }
            else if(dragDistance < 0)
            {
                moveCupula.MoveBackward();
            }
            else if (dragDistance == 0){
                moveCupula.Stop();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        #endif
    }
    
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
}