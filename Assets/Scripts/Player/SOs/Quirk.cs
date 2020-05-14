using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Quirk", menuName ="Quirk/Empty")]
public class Quirk : ScriptableObject
{
    public string quirkName;
    public int playerMaxHealth;
    public bool playerRegeneratesHealth = false;
    public int playerRegenerationRate;
    public float playerHealthPackModifier;
    public int playerMaxJetPackFuel;
    public int playerJetPackPower;
    public int playerMovementSpeed;
    public int weaponMaxAmmo;
    public float weaponReloadModifier;

}
