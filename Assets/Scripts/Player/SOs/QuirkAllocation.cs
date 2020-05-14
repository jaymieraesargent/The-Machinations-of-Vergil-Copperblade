using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuirkAllocation : MonoBehaviour
{
    public Quirk myQuirk;
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

        myGun.maxAmmo = myQuirk.weaponMaxAmmo;
        myGun.reloadTime = myQuirk.weaponReloadModifier;
    }

}
