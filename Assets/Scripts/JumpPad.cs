using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float force;
    //public GameObject playerController;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject var = collision.gameObject;
        Rigidbody rb = var.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * force);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("Player"))
        {
            other.transform.TransformDirection(Vector3.up * force);
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        other.transform.TransformDirection(Vector3.up * force);
    } */

    /*private void OnTriggerEnter(Collider other)
    {
        playerController.transform.TransformDirection(Vector3.up * force);
    } */
}
