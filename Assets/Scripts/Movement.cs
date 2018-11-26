using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public MovementSettings MovementSettings;
    public MovementInput MovementInput = new MovementInput();
    public Transform enemyTarget;
    public CapsuleCollider capsuleCollider;
    public float groundCheckOffset = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        bool grounded = CheckGrounded();

        //Movement
        Quaternion rotation = Quaternion.LookRotation(enemyTarget.position - transform.position, Vector3.up);
        Vector3 forward = rotation * Vector3.forward;
        rb.MoveRotation(rotation);
        Vector3 flatForward = (new Vector3(forward.x, 0f, forward.z)).normalized;
        Vector3 flatRight = Vector3.Cross(Vector3.up, flatForward);
        Vector3 movementVector = flatForward * MovementInput.Vertical + flatRight * MovementInput.Horizontal;

        // different control in air
        if (!grounded)
        {
            movementVector *= MovementSettings.AirControl;
        }
        rb.AddForce(movementVector * MovementSettings.Acceleration, ForceMode.Acceleration);

        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, MovementSettings.MaxSpeed);

        // damp movement on ground
        if (grounded)
        {
            horizontalVelocity *= MovementSettings.InverseDamping;
        }

        float verticalVelocity = rb.velocity.y;
        rb.velocity = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.z);

        // Jump
        if (MovementInput.Jump)
        {
            if (grounded)
            {
                rb.AddForce(Vector3.up * MovementSettings.JumpSpeed, ForceMode.VelocityChange);
            }
            MovementInput.Jump = false;
        }
    }

    private bool CheckGrounded()
    {
        Vector3 center = new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y, capsuleCollider.bounds.center.z);
        center.y += capsuleCollider.radius - groundCheckOffset;
        return Physics.CheckSphere(center, capsuleCollider.radius, groundLayer);
    }
}

public class MovementInput
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public bool Jump { get; set; }

    public Quaternion LookRotation { get; set; }
}