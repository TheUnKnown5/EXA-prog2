using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Enemy reference")]
    [SerializeField] GameObject enemy;

    [Header("Weapon reference")]
    [SerializeField] WeaponValues weaponValues;

    EnemyScript enemyHealth;


    public virtual bool AttackEnemy()
    {
        if (weaponValues.ammunition < 1)
        {
            return false;
        }
        weaponValues.ammunition--;
        return true;
    }
}
