using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public bool Stationary;
    public Transform Target;
    public Vector3 StationaryTarget;

    public float MaxSpeed;
    public float Acceleration;
    public float TurnSpeed;
    public float StartVelocity;

    Rigidbody rb;
    Vector3 velocity;

    Vector3 lastVelocityDelta;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = transform.forward * StartVelocity;
    }

    private void FixedUpdate()
    {
        if (!Stationary)
        {
            StationaryTarget = Target.position;
        }

        // calculate Rotations
        Vector3 targetDirection = (StationaryTarget - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion actualRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
        
        // calculate velocity delta
        Vector3 deltaVelocity = actualRotation * Vector3.forward;
        deltaVelocity *= Acceleration * Time.deltaTime;

        // apply velocity change
        velocity += deltaVelocity;
        velocity = Vector3.ClampMagnitude(velocity, MaxSpeed);

        // set new rotation and position
        rb.rotation = actualRotation;
        rb.position += velocity * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(StationaryTarget, .1f);
    }
}
