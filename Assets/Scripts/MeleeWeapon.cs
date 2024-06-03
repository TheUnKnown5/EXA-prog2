using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponScript
{
    [SerializeField] LayerMask enemyLayer;

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return false;
        }

        Debug.Log("I am attacking");
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

    private void OnCollisionEnter(Collision other)
    {
        var hitEnemy = other.gameObject.GetComponent<EnemyScript>();
        if (hitEnemy != null)
        {
            Debug.Log("I am attacking!");
            Fire();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponValues.weaponRange);
    }
}
