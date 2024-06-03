using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponScript
{
    [SerializeField] WeaponValues weaponValues;
    [SerializeField] LayerMask enemyLayer;

    public override bool AttackEnemy()
    {
        if (base.AttackEnemy() == false)
        {
            return false;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, weaponValues.weaponRange, enemyLayer);
        bool hit = false;

        foreach (var hitCollider in hitColliders)
        {
            EnemyScript enemy = hitCollider.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.EnemyDamageToTake(weaponValues.damage);
                hit = true;
            }
        }

        return hit;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponValues.weaponRange);
    }
}
