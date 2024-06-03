using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : WeaponScript
{
    [SerializeField] GameObject player;
    [SerializeField] WeaponValues weaponValues;
    [SerializeField] LayerMask IgnoreHitMask = 0;

    public override bool AttackEnemy()
    {
        if (base.AttackEnemy() == false)
        {
            return false;
        }
        Debug.Log("I am shooting");
        HitScanFire();

        return true;
    }

    void HitScanFire()
    {
        Ray weaponRay = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit = new();

        if (Physics.Raycast(weaponRay, out hit, weaponValues.weaponRange, ~IgnoreHitMask))
        {

        }
    }
}
