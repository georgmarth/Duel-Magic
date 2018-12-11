using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement Movement;
    public PlayerAttack Attack;
    public PlayerCamera PlayerCamera;

    public PlayerNumber player;

    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string HORIZONTAL_RIGHT = "Right Horizontal";
    public const string VERTICAL_RIGHT = "Right Vertical";
    public const string JUMP = "Jump";
    public const string FIRE = "Fire1";

    private void Update()
    {
        // Movement
        Movement.MovementInput.Horizontal = Input.GetAxis(PlayerInputString(HORIZONTAL));
        Movement.MovementInput.Vertical = Input.GetAxis(PlayerInputString(VERTICAL));

        // Jump
        if (Input.GetButtonDown(PlayerInputString(JUMP)))
        {
            Movement.MovementInput.Jump = true;
        }

        // LookAt
        Vector2 cameraRotationOffset = new Vector2();
        cameraRotationOffset.x = Input.GetAxis(PlayerInputString(HORIZONTAL_RIGHT));
        cameraRotationOffset.y = Input.GetAxis(PlayerInputString(VERTICAL_RIGHT));
        PlayerCamera.rotationOffset = cameraRotationOffset;

        // Attack
        if (Input.GetButtonDown(PlayerInputString(FIRE)))
        {
            Attack.Attack();
        }
    }

    private string PlayerInputString(string inputWithoutPlayer)
    {
        return inputWithoutPlayer + " " + player.PlayerNumberString();
    }
}
