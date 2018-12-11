using UnityEngine;

[CreateAssetMenu()]
public class PlayerNumber : ScriptableObject
{
    public enum Number { P1, P2 };

    public Number Player;

    public string PlayerNumberString()
    {
        switch (Player)
        {
            case Number.P1:
                return "P1";
            case Number.P2:
                return "P2";
            default:
                return "";
        }
    }
}
