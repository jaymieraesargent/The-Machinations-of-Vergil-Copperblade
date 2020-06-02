using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Quirk", menuName ="Quirk/Empty")]
public class Quirk : ScriptableObject
{
    public string quirkName;
    public string quirkDescription;
    public int playerMaxHealth = 20;
    public bool playerRegeneratesHealth = false;
    public int playerRegenerationRate = 2;
    public float playerHealthPackModifier = 1;
    public int playerMaxJetPackFuel = 20;
    public int playerJetPackPower = 10;
    public int playerMovementSpeed = 10;
    public int weaponMaxAmmo;
    public float weaponReloadModifier;

    public Sprite quirkIcon;
    public GameObject quirkParticleVisual;
    public Material quirkMaterialVisual;

}
