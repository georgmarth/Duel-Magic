using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int amount;

    public int StartHealth = 100;

    /// <summary>
    /// OnHealthChanged(int current, int delta)
    /// delta is unclamped
    /// </summary>
    public Action<int, int> OnHealthChanged;

    public Action OnDeath;

    public int Amount => amount;

    private void Start()
    {
        amount = StartHealth;
    }

    public void TakeDamage(int damage)
    {
        amount = Mathf.Max(0, amount - damage);
        OnHealthChanged?.Invoke(amount, -damage);

        if (amount == 0)
            OnDeath?.Invoke();
    }

    public void GainHealth(int gain)
    {
        amount += gain;
        OnHealthChanged (amount, gain);
    }
}