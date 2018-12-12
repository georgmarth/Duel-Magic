using UnityEngine;

public class MagicCheck : MonoBehaviour
{
    public Magic Magic;

    public FloatEvent HealthChanged;

    private void Start()
    {
        Magic.OnMagicChanged += OnMagicChanged;
    }

    private void OnMagicChanged(float Amount)
    {
        HealthChanged.Invoke(Amount / Magic.MaxMagic);
    }

    private void OnDestroy()
    {
        Magic.OnMagicChanged -= OnMagicChanged;
    }
}