using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Pickup thisPickup;
    public Player playerInteract;
    public Gun gunInteract;

    private void OnTriggerEnter(Collider player)
    {
        playerInteract = player.GetComponent<Player>();
        gunInteract = player.GetComponentInChildren<Gun>();
        playerInteract.curHealth += thisPickup.healthRestoration;
        playerInteract.currentJetPackFuel += thisPickup.jetpackRefuel;
        gunInteract.currentAmmo += thisPickup.ammoRestoration;
        PickUpThis();
    }

    void PickUpThis()
    {
        thisPickup.wasPickedUp = true;

        Destroy(this);
    }
}
