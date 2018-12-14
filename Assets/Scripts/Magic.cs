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

    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    public Color greenColor = Color.green;

    [HideInInspector]
    public float changeTypeCooldownTimer;

    public ParticleSystem particles;

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

    public bool ChangeType()
    {
        Type newType = Type.Red;

        switch (MagicType)
        {
            case Type.Red:
                newType = Type.Green;
                break;
            case Type.Green:
                newType = Type.Blue;
                break;
            case Type.Blue:
                newType = Type.Red;
                break;
        }

        return ChangeType(newType);
    }

    private bool ChangeType(Type newType)
    {
        if (MagicType == newType)
            return false;

        if (changeTypeCooldownTimer > 0f)
            return false;

        if (UseMagic(ChangeTypeCost))
        {
            changeTypeCooldownTimer = ChangeTypeCooldown;
            MagicType = newType;
            OnTypeChanged?.Invoke(newType);
            var module = particles.main;
            module.startColor = GetColor();
            return true;
        }

        return false;
    }

    public Color GetColor()
    {
        switch (MagicType)
        {
            case Type.Red:
                return redColor;
            case Type.Green:
                return greenColor;
            case Type.Blue:
                return blueColor;
            default:
                return redColor;
        }
    }

    public static int TypeBoost(Type type1, Type type2)
    {
        int boost = 0;
        if (type1 == Type.Red)
        {
            if (type2 == Type.Green)
                boost = -1;
            else if (type2 == Type.Blue)
                boost = 1;
        }
        else if (type1 == Type.Green)
        {
            if (type2 == Type.Blue)
                boost = -1;
            else if (type2 == Type.Red)
                boost = 1;
        }
        else if (type1 == Type.Blue)
        {
            if (type2 == Type.Red)
                boost = -1;
            else if (type2 == Type.Green)
                boost = 1;
        }
        return boost;
    }
}
