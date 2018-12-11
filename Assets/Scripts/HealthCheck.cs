using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthCheck : MonoBehaviour
{
    public Health health;

    public IntEvent HealthChanged;

    private void Start()
    {
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentAmount, int damageAmount)
    {
        HealthChanged.Invoke((float) currentAmount / health.StartHealth);
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= OnHealthChanged;
    }
}

[System.Serializable]
public class IntEvent : UnityEvent<float>
{

}