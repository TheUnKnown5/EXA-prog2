using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    Unarmed,
    HitScan,
    Melee,
    Total
}

public class WeaponHandler : MonoBehaviour
{
    [Header("Unarmed = Element 0 \n" +
        "HitScan = Element 1 \n" +
        "Melee = Element 3")]
    public WeaponScript[] availableWeapons = new WeaponScript[(int)WeaponState.Total];
    public WeaponScript currentWeapon = null;

    public float ScrollWheelBreakpoint = 1.0f;
    private float ScrollWheelDelta = 0.0f;


    public void Start()
    {
        int currentWeaponIndex = (int)currentWeapon.weaponType;
        WeaponSwapAnimation(currentWeaponIndex);
    }
    public void Update()
    {
        HandleWeaponSwap();

        if (Input.GetMouseButtonUp(0) && currentWeapon != null)
        {
            currentWeapon.Fire();
        }
    }

    private void HandleWeaponSwap()
    {

        ScrollWheelDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(ScrollWheelDelta) > ScrollWheelBreakpoint)
        {

            int swapDirection = (int)Mathf.Sign(ScrollWheelDelta);
            ScrollWheelDelta = 0.0f;

            int currentWeaponIndex = (int)currentWeapon.weaponType;
            currentWeaponIndex += swapDirection;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + -1;
            }
            if (currentWeaponIndex >= (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
            WeaponSwapAnimation(currentWeaponIndex);
        }
    }
    private void WeaponSwapAnimation(int currentWeaponIndex)
    {
        Debug.Log(currentWeaponIndex);
        foreach (var weapon in availableWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
        currentWeapon = availableWeapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.holdingWeaponHandler = this;
    }
}
