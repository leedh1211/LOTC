using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotEffect : IWeaponApplicable
{

    public void ApplyToWeapon(WeaponHandler weaponHandler)
    {
        weaponHandler.projectileCount += 1; //°ͺ Αυ°‘ 
    }

}
