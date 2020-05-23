using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOAllocation : MonoBehaviour
{
    public Quirk myQuirk;
    public SoWeapon mySOWeapon;
    public Player player;
    public Gun myGun;

    void Start()
    {
        player = this.gameObject.GetComponent<Player>();
        myGun = player.gameObject.GetComponentInChildren<Gun>();

        player.myQuirkName = myQuirk.quirkName;
        player.maxHealth = myQuirk.playerMaxHealth;
        player.regenerateHealth = myQuirk.playerRegeneratesHealth;
        player.healthRegenerationRate = myQuirk.playerRegenerationRate;
        player.healthPackModifier = myQuirk.playerHealthPackModifier;
        player.maximumJetPackFuel = myQuirk.playerMaxJetPackFuel;
        player.jetPackPower = myQuirk.playerJetPackPower;
        player.moveSpeed = myQuirk.playerMovementSpeed;

        myGun.maxAmmo = mySOWeapon.weaponMaxUnchangedAmmo + myQuirk.weaponMaxAmmo;
        myGun.reloadTime = mySOWeapon.weaponReloadTime + myQuirk.weaponReloadModifier;
        myGun.fireRate = mySOWeapon.weaponFireRate;
        myGun.range = mySOWeapon.weaponRange;
        myGun.damage = mySOWeapon.weaponDamage;
        myGun.weaponName = mySOWeapon.weaponName;
    }

}
