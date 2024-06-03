using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponValues", menuName = "WeaponValues")]

public class WeaponValues : ScriptableObject
{
    public int damage;
    public int ammunition = 1000;

    public float weaponRange = 1000f;
}
