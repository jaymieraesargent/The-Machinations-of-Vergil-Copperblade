using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactetController : MonoBehaviour
{
    public float moveSpeed;
    public float runSpeed, walkSpeed, crouchSpeed, jumpSpeed;
    public float curHealth;
    private float verticalDirection;
    private float horizontalDirection;
    public Text hp;
    public bool isZoomedIn;
    public bool damaged;

    void Update()
    {
        hp.text = "HP: " + curHealth;

        verticalDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        horizontalDirection = Input.GetAxis("Horizontal") * moveSpeed / 3 * Time.deltaTime;
        transform.Translate(horizontalDirection, 0, verticalDirection);


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown("space"))
        {
            var rigid = this.gameObject.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                rigid.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            }
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DamagePlayer(float damage)
    {
        damaged = true;
        curHealth -= damage;
    }
}
