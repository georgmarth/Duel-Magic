using UnityEngine;

public class Damage : MonoBehaviour
{
    public int DamageAmount;

    public int TypeBoost;

    public LayerMask enemyMask;

    public Magic.Type type;

    public GameObject OnHitExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        // check if other is in enemyMask
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            var health = other.GetComponentInParent<Health>();
            health?.TakeDamage(GetCalculatedDamage(health.magic.MagicType));
            var shield = other.GetComponentInParent<Shield>();
            shield?.Damage(GetCalculatedDamage(shield.magicType));
        }

        if (other.gameObject.layer != gameObject.layer)
        {
            if (OnHitExplosion != null)
                Instantiate(OnHitExplosion, transform.position, transform.rotation);

            var lifetime = GetComponent<Lifetime>();
            if (lifetime != null)
            {
                lifetime.Invoke();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public int GetCalculatedDamage(Magic.Type enemyType)
    {
        return DamageAmount + (TypeBoost * Magic.TypeBoost(type, enemyType));
    }
}
