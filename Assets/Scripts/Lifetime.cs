using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float TimeLeft;

    public bool destroy = true;

    public float destroydelay;
    
    public MonoBehaviour[] disableComponents;
    public Collider[] disableColliders;
    public ParticleSystem particles;
    public GameObject[] disableObjects;

    bool dead = false;

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0f && !dead)
        {
            dead = true;
            Invoke();
        }
    }

    public void Invoke()
    {
        foreach (var component in disableComponents)
        {
            component.enabled = false;
        }
        foreach (var collider in disableColliders)
        {
            collider.enabled = false;
        }
        foreach (var go in disableObjects)
        {
            go?.SetActive(false);
        }
        
        particles?.Stop();

        if (destroy)
            Destroy(gameObject, destroydelay);
    }
}
