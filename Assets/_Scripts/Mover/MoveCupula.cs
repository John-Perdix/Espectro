using UnityEngine;

public class MoveCupula : MonoBehaviour
{
    [SerializeField] public float moveForce = 10f;
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
            rb.AddForce(Vector3.right * direction * moveForce, ForceMode.Force);
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
        // opcional: podes também travar mesmo de vez
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
