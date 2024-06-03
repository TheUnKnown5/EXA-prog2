using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Enemy reference")]
    [SerializeField] GameObject enemy;


    [SerializeField] WeaponValues weaponValues;

    EnemyScript enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyScript>();
    }

    public virtual void AttackEnemy()
    {
        enemyHealth.EnemyDamageToTake(weaponValues.damage);
    }
}
