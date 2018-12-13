using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    public Image image;
    public PlayerAttack attack;

    // Update is called once per frame
    void Update()
    {
        float progress = Mathf.Clamp01(attack.cooldownTimer / attack.AttackCooldown);
        image.fillAmount = progress;
    }
}
