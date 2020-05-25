using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool item;
    public GameObject prefab;
    public GameObject Spawnloaction;
    public float cooldown;
    float timer;

    private void Start()
    {
        spawn();
    }

    private void Update()
    {
        if (!item && timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0 && !item)
        {
            spawn();
        }
    }

    public void spawn()
    {
        item = true;
        GameObject temp = Instantiate(prefab, Spawnloaction.transform.position, Spawnloaction.transform.rotation);
        temp.transform.parent = Spawnloaction.transform;
        timer = cooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && item)
        {
            item = false;
            timer = cooldown;
        }
    }
}
