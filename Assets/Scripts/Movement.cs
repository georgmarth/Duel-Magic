using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public MovementSettings MovementSettings;
    public MovementInput MovementInput = new MovementInput();

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = transform.forward * MovementInput.Vertical + transform.right * MovementInput.Horizontal;
        rb.AddForce(movementVector * MovementSettings.Acceleration, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MovementSettings.MaxSpeed);
    }
}

public class MovementInput
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public Quaternion LookRotation { get; set; }
}