using UnityEngine;

[CreateAssetMenu()]
public class MovementSettings : ScriptableObject
{
    public float MaxSpeed;
    public float MaxTurnSpeed;
    public float Acceleration;
    public float AirControl;
    public float JumpSpeed;
}
