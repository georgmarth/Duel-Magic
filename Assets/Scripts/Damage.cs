using UnityEngine;

public class Damage : MonoBehaviour
{
    public int DamageAmount;

    public LayerMask enemyMask;

    private void OnTriggerEnter(Collider other)
    {
        // check if other is in enemyMask
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            other.GetComponentInParent<Health>()?.TakeDamage(DamageAmount);
        }

        Destroy(gameObject);
    }

}
