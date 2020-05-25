using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName ="Pickup/Empty")]
public class Pickup : ScriptableObject
{
    public string pickupType;
    public GameObject pickupObject;

    public float healthRestoration;
    public float jetpackRefuel;
    public int ammoRestoration;

    public bool wasPickedUp = false;
}
