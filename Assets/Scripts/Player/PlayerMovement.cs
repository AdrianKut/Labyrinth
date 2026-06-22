using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header( "Movement" )]
    public float speed = 250f;

    [Header( "Rotation" )]
    public float rotationSpeed = 100f;
    public float minRotationVelocity = 0.2f;
    public float maxRotationVelocity = 2f;

    private Rigidbody rb;
    private bool isFlat = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;

        if( isFlat )
        {
            tilt = Quaternion.Euler( 90, 0, 0 ) * tilt;
        }

        rb.AddForce( tilt * speed, ForceMode.Force );
    }

    void Update()
    {
        Vector3 velocity = rb.linearVelocity;

        float rotationFactor = Mathf.InverseLerp(
            minRotationVelocity,
            maxRotationVelocity,
            velocity.magnitude
        );

        if( rotationFactor > 0f )
        {
            Vector3 rotationAxis =
                Vector3.Cross( Vector3.up, velocity.normalized );

            transform.Rotate(
                rotationAxis,
                rotationFactor * rotationSpeed * Time.deltaTime,
                Space.World
            );
        }
    }
}