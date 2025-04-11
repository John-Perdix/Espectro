using UnityEngine;

public class MoveCupula : MonoBehaviour
{
    [SerializeField] public float moveForce = 10f;
    [SerializeField] Vector3 axis = new Vector3(1,0,0);
    private Rigidbody rb;
    private int direction = 0; // 1 = frente, -1 = trás, 0 = parado

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (direction != 0)
        {
            rb.AddForce(axis * direction * moveForce, ForceMode.Force);
        }
    }

    // BOTÕES
    public void MoveForward()
    {
        direction = 1;
    }

    public void MoveBackward()
    {
        direction = -1;
    }

    public void Stop()
    {
        direction = 0;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
