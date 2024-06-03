using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Jag har inte kunnat få vapnen att fungera.

    [Header("Enemy reference")]
    [SerializeField] GameObject enemy;

    [Header("Weapon reference")]
    public WeaponValues weaponValues;
    public WeaponHandler holdingWeaponHandler = null;
    public WeaponState weaponType = WeaponState.Total;

    EnemyScript enemyHealth;


    public virtual bool Fire()
    {
        if (weaponValues.ammunition < 1)
        {
            return false;
        }
        weaponValues.ammunition--;
        return true;
    }
}
