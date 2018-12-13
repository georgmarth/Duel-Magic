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

    public Animator animator;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        bool grounded = CheckGrounded();

        //Movement
        var flatPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        var flatEnemyPosition = new Vector3(enemyTarget.position.x, 0f, enemyTarget.position.z);
        Quaternion rotation = Quaternion.LookRotation(flatEnemyPosition - flatPosition, Vector3.up);
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

        float animatorSpeed = horizontalVelocity.magnitude / MovementSettings.MaxSpeed;
        animator.SetFloat("Speed", animatorSpeed);

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

    public bool AttackPress { get; set; }
    public bool AttackHold { get; set; }

    public bool DefendPress { get; set; }
    public bool DefendHold { get; set; }

    public Quaternion LookRotation { get; set; }
}