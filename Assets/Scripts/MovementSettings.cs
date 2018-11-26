using UnityEngine;

[CreateAssetMenu()]
public class MovementSettings : ScriptableObject
{
    public float MaxSpeed;
    public float MaxTurnSpeed;
    public float Acceleration;
    [Range(0f, 1f)]
    public float AirControl;
    public float JumpSpeed;
    [Range(0.1f, 1f)]
    public float InverseDamping;
}
