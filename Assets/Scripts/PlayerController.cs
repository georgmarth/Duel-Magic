using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement Movement;
    public PlayerAttack Attack;
    public PlayerCamera PlayerCamera;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL_RIGHT = "Right Horizontal";
    private const string VERTICAL_RIGHT = "Right Vertical";
    private const string JUMP = "Jump";
    private const string FIRE = "Fire1";

    private void Update()
    {
        // Movement
        Movement.MovementInput.Horizontal = Input.GetAxis(HORIZONTAL);
        Movement.MovementInput.Vertical = Input.GetAxis(VERTICAL);

        // Jump
        if (Input.GetButtonDown(JUMP))
        {
            Movement.MovementInput.Jump = true;
        }

        // LookAt
        Vector2 cameraRotationOffset = new Vector2();
        cameraRotationOffset.x = Input.GetAxis(HORIZONTAL_RIGHT);
        cameraRotationOffset.y = Input.GetAxis(VERTICAL_RIGHT);
        PlayerCamera.rotationOffset = cameraRotationOffset;

        // Attack
        if (Input.GetButtonDown(FIRE))
        {
            Attack.Attack();
        }
    }
}
