using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Vector3 offset;
    public float AngleRange = 20f;
    [HideInInspector]
    public Vector2 rotationOffset;
    [Range(0, 1)]
    public float cameraSpeed = .8f;
    [Range(0, 1)]
    public float lookAtSpeed = .8f;

    void FixedUpdate()
    {

        Quaternion playerRotation = Quaternion.Euler(new Vector3(0f, player.rotation.eulerAngles.y, 0f));
        Vector3 targetPos = player.position + (playerRotation * offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed);

        Quaternion horizontalOffset = Quaternion.Euler(new Vector3(rotationOffset.y, 0f, 0f) * AngleRange);
        Quaternion verticalOffset = Quaternion.Euler(new Vector3(0f, rotationOffset.x, 0f) * AngleRange);

        Quaternion targetRotation = verticalOffset * Quaternion.LookRotation(enemy.position - transform.position, Vector3.up) * horizontalOffset;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtSpeed);
    }
}
