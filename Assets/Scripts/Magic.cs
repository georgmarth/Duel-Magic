using System;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public enum Type { Red, Green, Blue }

    public Type MagicType;
    public float Amount = 100f;

    public float MaxMagic = 100f;

    public float ChangeTypeCost = 20f;

    public float RegainRate = 5f;

    public float ChangeTypeCooldown = 5f;

    private float changeTypeCooldownTimer;

    public Action<Type> OnTypeChanged;
    public Action<float> OnMagicChanged;

    private void Start()
    {
        changeTypeCooldownTimer = 0f;
    }

    private void Update()
    {
        // regain magic
        float oldAmount = Amount;
        Amount = Mathf.Min(MaxMagic, Amount + RegainRate * Time.deltaTime);
        if (oldAmount != Amount)
            OnMagicChanged?.Invoke(Amount);

        if (changeTypeCooldownTimer > 0f)
        {
            changeTypeCooldownTimer -= Time.deltaTime;
            changeTypeCooldownTimer = Mathf.Max(0f, changeTypeCooldownTimer);
        }

    }

    public bool UseMagic(float cost)
    {
        if (cost > Amount)
        {
            return false;
        }
        Amount -= cost;
        OnMagicChanged?.Invoke(Amount);
        return true;
    }

    public bool ChangeType(Type newType)
    {
        if (MagicType == newType)
            return false;

        if (changeTypeCooldownTimer > 0f)
            return false;

        if (UseMagic(ChangeTypeCost))
        {
            MagicType = newType;
            OnTypeChanged?.Invoke(newType);
            return true;
        }

        return false;
    }
}
