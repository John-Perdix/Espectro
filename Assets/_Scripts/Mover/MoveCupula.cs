using UnityEngine;

public class MoveCupula : MonoBehaviour
{
    [SerializeField] public float moveForce = 10f;
    [SerializeField] Vector3 axis = new Vector3(1,0,0);
    [SerializeField] ActivatorRaios ativadorRaios;
    [SerializeField] float tolerancia = 0.5f;
    Vector3 snapPosition;
    bool snapEnabled;
    [SerializeField] private float stepSize = 0.01f;
    
    

    private Rigidbody rb;
    private int direction = 0; // 1 = frente, -1 = trás, 0 = parado

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (ativadorRaios != null){
            snapPosition = ativadorRaios.targetPosition;
            snapEnabled = true;
        }else{
            snapEnabled=false;
        }
    }

    void FixedUpdate()
    {

        Vector3 currentPosition = transform.position;
        float diff = (currentPosition - snapPosition).sqrMagnitude;

        if (direction != 0)
        {
            rb.AddForce(axis * direction * moveForce, ForceMode.Force);
            //transform.position += axis.normalized * direction * stepSize;


        }
        if (diff < tolerancia && snapEnabled)
        {
            SnapToPosition();
        }

        //Debug.Log("Posição de "+ name +": " + transform.localPosition);
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

    public void SnapToPosition()
    {
        rb.position = snapPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
