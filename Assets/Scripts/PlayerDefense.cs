using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public GameObject ShieldPrefab;
    public Magic Magic;
    public float DefenseCost = 10f;

    [Range(0, 1)]
    public float shieldTransparency = .7f;

    public Animator animator;

    public Transform ShieldSpawner;

    Shield shieldInstance;

    public void Defend()
    {
        if (Magic.UseMagic(DefenseCost))
        {
            animator.SetTrigger("Attack");
            if (shieldInstance == null)
            {
                var instance = Instantiate(ShieldPrefab, ShieldSpawner.position, ShieldSpawner.rotation).GetComponent<Shield>();
                shieldInstance = instance.GetComponent<Shield>();
                instance.gameObject.layer = gameObject.layer;
                foreach (Transform child in instance.transform)
                {
                    child.gameObject.layer = gameObject.layer;
                }
            }
            else
            {
                shieldInstance.transform.position = ShieldSpawner.position;
                shieldInstance.transform.rotation = ShieldSpawner.rotation;
            }
            var renderer = shieldInstance.GetComponentInChildren<Renderer>();
            renderer.material.color = ShieldColor();
            shieldInstance.magicType = Magic.MagicType;
            shieldInstance.Activate();
        }
    }

    Color ShieldColor()
    {
        Color color = Magic.GetColor();
        color.a = shieldTransparency;
        return color;
    }
}
