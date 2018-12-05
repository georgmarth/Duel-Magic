using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float TimeLeft;

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
