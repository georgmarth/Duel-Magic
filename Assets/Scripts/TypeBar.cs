using UnityEngine;
using UnityEngine.UI;

public class TypeBar : MonoBehaviour
{
    public Magic Magic;

    public Image Indicator;

    Color color;

    private void Start()
    {
        color = Magic.GetColor();
    }

    // Update is called once per frame
    void Update () {
        float fillAmount = 1f - Mathf.Clamp01(Magic.changeTypeCooldownTimer / Magic.ChangeTypeCooldown);
        Indicator.fillAmount = fillAmount;
        Color newColor = Magic.GetColor();
        if (newColor != Indicator.color)
        {
            Indicator.color = newColor;
        }
	}
}
