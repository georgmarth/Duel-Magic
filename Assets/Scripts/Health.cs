using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int amount;

    public int StartHealth = 100;

    public Animator animator;
    public Magic magic;

    public GameState state;

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
        animator?.SetTrigger("Hit");
        amount = Mathf.Max(0, amount - damage);
        OnHealthChanged?.Invoke(amount, -damage);

        if (amount == 0)
            Death();
            
    }

    public void GainHealth(int gain)
    {
        amount += gain;
        OnHealthChanged (amount, gain);
    }

    public void Death()
    {
        animator?.SetTrigger("Death");
        OnDeath?.Invoke();
        state.Lose(GetComponent<PlayerController>().player);
    }
}