using UnityEngine;
using UnityEngine.Events;

public class HealthCheck : MonoBehaviour
{
    public Health health;

    public FloatEvent HealthChanged;

    private void Start()
    {
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentAmount, int damageAmount)
    {
        HealthChanged.Invoke((float)currentAmount / health.StartHealth);
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= OnHealthChanged;
    }
}

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }