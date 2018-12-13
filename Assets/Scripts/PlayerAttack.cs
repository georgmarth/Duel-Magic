using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Launch;
    public GameObject MissilePrefab;
    public PlayerCamera PlayerCamera;
    public Magic Magic;
    public LayerMask enemyMask;
    public float AttackCost = 5f;
    public float AttackCooldown = .5f;
    public Animator animator;

    public Transform Target;

    [HideInInspector]
    public float cooldownTimer;

    private void Start()
    {
        cooldownTimer = 0f;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        Camera camera = PlayerCamera.GetComponent<Camera>();
        Ray centerRay = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        float distanceToPlayer = Vector3.Distance(PlayerCamera.transform.position, Target.position);
        Vector3 stationaryTarget = centerRay.GetPoint(distanceToPlayer);

        if (cooldownTimer >= AttackCooldown)
        {
            if (Magic.UseMagic(AttackCost))
            {
                cooldownTimer = 0f;
                animator.SetTrigger("Attack");
                var instance = Instantiate(MissilePrefab, Launch.position, Launch.rotation);
                MissileMovement instanceMovement = instance.GetComponent<MissileMovement>();
                if (instanceMovement != null)
                {
                    instanceMovement.Stationary = true;
                    instanceMovement.StationaryTarget = stationaryTarget;
                }
                Damage missileDamage = instance.GetComponent<Damage>();
                instance.layer = gameObject.layer;
                foreach (Transform child in instance.transform)
                {
                    child.gameObject.layer = gameObject.layer;
                }
                if (missileDamage != null)
                {
                    missileDamage.enemyMask = enemyMask;
                    missileDamage.type = Magic.MagicType;
                }

                // change colors on missiles
                var particles = instance.GetComponent<ParticleSystem>();
                var mainModule = particles.main;
                mainModule.startColor = Magic.GetColor();

                var renderers = instance.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    renderer.material.color = Magic.GetColor();
                }
                instance.GetComponentInChildren<Light>().color = Magic.GetColor();
            }
        }
    }
}
