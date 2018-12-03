using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Launch;
    public GameObject MissilePrefab;

    public Transform Target;

    public void Attack()
    {
        var instance = Instantiate(MissilePrefab, Launch.position, Launch.rotation);
        MissileMovement instanceMovement = instance.GetComponent<MissileMovement>();
        if (instanceMovement != null)
            instanceMovement.Target = Target;
    }
}
