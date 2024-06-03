using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] HealthValues healthValues;

    public void EnemyDamageToTake(int damageToTake)
    {
        healthValues.maxHealth -= damageToTake;
        if (healthValues.maxHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
