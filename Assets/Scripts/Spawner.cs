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
        item = true;
        GameObject temp = Instantiate(prefab, Spawnloaction.transform);
        timer = cooldown;
    }

    private void Update()
    {
        if (!item && timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0 && !item)
        {
            GameObject temp = Instantiate(prefab, Spawnloaction.transform);
            item = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && !item)
        {
            item = true;
            timer = cooldown;
        }
    }
}
