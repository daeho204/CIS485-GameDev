using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
 // spawn time
    public float timeSpawnMax = 10f;
    public float timeSpawnMin = 3f;

    public float spawnTime; //interval
    public float lastSpawnTime;

    public GameObject[] zombies;
   

    // position
    public Vector3[] locations;


    void Start()
    {

        spawnTime = Random.Range(timeSpawnMin, timeSpawnMax);
        lastSpawnTime = 0;

    }


    void Update()
    {
        if (Time.time >= lastSpawnTime + spawnTime)
        {
            //if it's time to spawn
            lastSpawnTime = Time.time;
            spawnTime = Random.Range(timeSpawnMin, timeSpawnMax);
            Spawn();
        }

    }

    void Spawn()
    {
        //Instantiate
        int zombieNum = Random.Range(0, zombies.Length);
        int locationNum = Random.Range(0, locations.Length);
        GameObject newZombie = Instantiate(zombies[zombieNum], locations[locationNum], Quaternion.identity);
        

    }

}
