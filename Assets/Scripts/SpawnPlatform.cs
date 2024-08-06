using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject spawnee;
    public GameObject boss;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        boss = GameObject.Find("Enemy");
    }

    public void SpawnObject()
    {
        Instantiate(spawnee);
        
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }

    public void StopSpawn()
    {
        if(GameObject.Find("Enemy") != null)
        {
            stopSpawning = true;
        }
    }
}
