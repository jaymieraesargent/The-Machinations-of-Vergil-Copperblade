﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float runSpeed, walkSpeed, crouchSpeed, jumpSpeed;
    public float jetPackPower;

    public float curHealth;
    public float maxHealth;
    public bool regenerateHealth = false;
    public float healthRegenerationRate;
    public float healthPackModifier;

    public float currentJetPackFuel;
    public float maximumJetPackFuel;
    public float fuelConsumptionRate;

    private float verticalDirection;
    private float horizontalDirection;

    public Image healthImage;


    public bool isZoomedIn;
    public bool damaged;
    public int team;

    public string myQuirkName;
    [SerializeField]
    public bool IsGrounded
    {
        get
        {
            float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
            RaycastHit hit; 
            if(Physics.Raycast(transform.position, Vector3.down, out hit, DisstanceToTheGround + 0.1f))
            {
                if(hit.transform.tag=="Ground")
                {
                    return true;
                }
            }


            return false;
        }
    }

    void Update()
    {
        
        if(curHealth<=0)
        {
            Map.Instance.Respawn(this.gameObject);
        }
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if(currentJetPackFuel > maximumJetPackFuel)
        {
            currentJetPackFuel = maximumJetPackFuel;
        }
        verticalDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        horizontalDirection = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontalDirection, 0, verticalDirection);


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Jet Pack Control

        if (Input.GetKey(KeyCode.Space))
        {
            var rigid = this.gameObject.GetComponent<Rigidbody>();

            if (currentJetPackFuel <= 0)
            {
                float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
                
                if (rigid != null && IsGrounded)
                {
                    rigid.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
                }
            }
            else if (currentJetPackFuel > 0)
            {
                JetPack();
            }
        }
        
        //Quirk bools

        if(regenerateHealth)
        {
            if (curHealth < maxHealth && curHealth != 0)
            {
                curHealth += healthRegenerationRate * Time.deltaTime;
            }
        }

        //UI effects
        var damageOpacityEffect = healthImage.color;
        damageOpacityEffect.a = 1 - (curHealth / maxHealth);
        healthImage.color = damageOpacityEffect;

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentJetPackFuel = maximumJetPackFuel;
    }

    public void DamagePlayer(float damage)
    {
        damaged = true;
        curHealth -= damage;
    }

    void JetPack()
    {
        currentJetPackFuel -= fuelConsumptionRate * Time.deltaTime;
        var myBody = this.gameObject.GetComponent<Rigidbody>();
        myBody.AddForce(transform.up * jetPackPower, ForceMode.Acceleration);
    }
}
