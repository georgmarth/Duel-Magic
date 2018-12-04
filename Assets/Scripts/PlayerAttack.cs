using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Launch;
    public GameObject MissilePrefab;
    public PlayerCamera PlayerCamera;

    public Transform Target;

    public void Attack()
    {
        Camera camera = PlayerCamera.GetComponent<Camera>();
        Ray centerRay = camera.ScreenPointToRay(new Vector3((camera.pixelWidth - 1) / 2f, (camera.pixelHeight - 1) / 2f, 0f));
        float distanceToPlayer = Vector3.Distance(PlayerCamera.transform.position, Target.position);
        Vector3 stationaryTarget = centerRay.GetPoint(distanceToPlayer);

        var instance = Instantiate(MissilePrefab, Launch.position, Launch.rotation);
        MissileMovement instanceMovement = instance.GetComponent<MissileMovement>();
        if (instanceMovement != null)
        {
            instanceMovement.Stationary = true;
            instanceMovement.StationaryTarget = stationaryTarget;
        }
    }
}
