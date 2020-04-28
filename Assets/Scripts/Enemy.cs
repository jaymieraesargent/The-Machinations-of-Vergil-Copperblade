﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum AIState
    {
        Patrol,
        Seek,
        Fire,
        Dead
    }
    public AIState state;
    public float curHealth, maxHealth, moveSpeed, attackRange, attackSpeed, sightRange, baseDamage;
    public float fireRate = 15f;
    public int curWaypoint, difficulty;
    public bool isDead;

    private float nextTimeToFire = 0f;

    [Space(5), Header("Base References")]
    public GameObject self;
    public Transform player;
    public Transform waypointParent;
    protected Transform[] waypoints;
    public NavMeshAgent agent;
    public AudioSource shoot;
    public GameObject healthCanvas;
    public Image healthBar;
    public Animator anim;
    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        agent = self.GetComponent<NavMeshAgent>();
        curWaypoint = 1;
        agent.speed = moveSpeed;
        anim = self.GetComponent<Animator>();
        SetKinematic(true);
    }
    void Update()
    {
        anim.SetBool("Running", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Fire", false);
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Attack();
        }
        if (!isDead)
        {
            Patrol();
            Seek();
        }
        if (curHealth <= 0f)
        {
            Die();
        }
    }

    public void Damage(float amount)
    {
        curHealth -= amount;
    }

    public void Patrol()
    {
        // DO NOT CONTINUE IF NO WAYPOINTS
        if (waypoints.Length == 0 || Vector3.Distance(player.position, self.transform.position) <= sightRange)
        {
            return;
        }
        state = AIState.Patrol;
        anim.SetBool("Walking", true);
        moveSpeed = 1.8f;
        // Follow waypoints
        // Set agent to target
        agent.destination = waypoints[curWaypoint].position;
        // Are we at the waypoint?
        if (self.transform.position.x.Equals(agent.destination.x) && self.transform.position.z == agent.destination.z)
        {
            if (curWaypoint < waypoints.Length - 1)
            {
                // If so go to next waypoint
                curWaypoint++;
            }
            else
            {
                // If at the end of patrol go to start
                curWaypoint = 1;
            }
        }
        // If so go to next waypoint
    }
    public void Seek()
    {
        if(player == null)
        {
            return;
        }

        if (Vector3.Distance(player.position, self.transform.position) > sightRange || Vector3.Distance(player.position, self.transform.position) < attackRange)
        {
            // Stop seeking
            return;
        }
        state = AIState.Seek;
        moveSpeed = 2.5f;
        anim.SetBool("Running", true);
        // If player in sight range and not attack range then chase
        agent.destination = this.player.position;
    }

    public virtual void Attack()
    {
        if (Vector3.Distance(player.position, self.transform.position) > attackRange || curHealth < 0 || player.GetComponent<Player>().curHealth < 0)
        {
            // Stop Firing
            agent.enabled = true;
            return;
        }
        state = AIState.Fire;
        anim.SetBool("Fire", true);
        shoot.Play();
        agent.enabled = false;
        Debug.Log("Enemy is firing");
        // If player in attack range then attack
    }
    void Die()
    {
        state = AIState.Dead;
        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        isDead = true;
        agent.destination = this.transform.position;
    }
}
