using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Enemy reference")]
    [SerializeField] GameObject enemy;

    public virtual void Attack()
    {
        Destroy(enemy);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Attack();
        }
    }
}
