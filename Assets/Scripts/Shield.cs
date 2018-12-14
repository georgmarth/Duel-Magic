using UnityEngine;

public class Shield : MonoBehaviour
{
    public float StartLifeTime = 30f;

    public Magic.Type magicType;
    
    [HideInInspector]
    public float lifeTime = 30f;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        lifeTime = StartLifeTime;
    }

    public void Damage(float amount)
    {
        lifeTime -= amount;
        if (lifeTime <= 0f)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
