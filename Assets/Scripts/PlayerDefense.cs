using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public GameObject ShieldPrefab;
    public Magic Magic;
    public float DefenseCost = 10f;

    public Transform ShieldSpawner;

    Shield shieldInstance;

    public void Defend()
    {
        if (Magic.UseMagic(DefenseCost))
        {
            if (shieldInstance == null)
            {
                var instance = Instantiate(ShieldPrefab, ShieldSpawner.position, ShieldSpawner.rotation).GetComponent<Shield>();
                shieldInstance = instance.GetComponent<Shield>();
                shieldInstance.gameObject.layer = gameObject.layer;
            }
            else
            {
                shieldInstance.transform.position = ShieldSpawner.position;
                shieldInstance.transform.rotation = ShieldSpawner.rotation;
            }
            shieldInstance.Activate();
        }
    }
}
