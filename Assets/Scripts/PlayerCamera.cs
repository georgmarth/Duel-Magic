using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    [Range(0, 1)]
    public float cameraSpeed = .8f;
    [Range(0, 1)]
    public float lookAtSpeed = .8f;

    void FixedUpdate()
    {
        Quaternion playerRotation = Quaternion.Euler(new Vector3(0f, player.rotation.eulerAngles.y, 0f));
        Vector3 targetPos = player.position + (playerRotation * offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed);

        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtSpeed);
    }
}
