using System.Collections;
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

    public Text hp;

    public bool isZoomedIn;
    public bool damaged;
    public int team;

    public string myQuirkName;

    void Update()
    {
        hp.text = "HP: " + curHealth;
        if(curHealth<=0)
        {
            Map.Instance.Respawn(this.gameObject);
        }
        verticalDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        horizontalDirection = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontalDirection, 0, verticalDirection);


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            var rigid = this.gameObject.GetComponent<Rigidbody>();

            if (currentJetPackFuel <= 0)
            {
                if (rigid != null)
                {
                    rigid.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
                }
            }
            else if (currentJetPackFuel > 0)
            {
                JetPack();
            }
        }

        if(regenerateHealth)
        {
            if (curHealth < maxHealth && curHealth != 0)
            {
                curHealth += healthRegenerationRate * Time.deltaTime;
            }
        }
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
