using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<Spawner> spawners;

    private void Awake()
    {
        spawners = new List<Spawner>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Spawner currentSpawner = transform.GetChild(i).GetComponent<Spawner>();
            if (currentSpawner)
                spawners.Add(currentSpawner);
        }
    }

    public void StartSpawners()
    {
        foreach (Spawner spawner in spawners)
            spawner.StartSpawn();
    }

    public void StopSpawners()
    {
        foreach (Spawner spawner in spawners)
            spawner.StopSpawn();
    }
}