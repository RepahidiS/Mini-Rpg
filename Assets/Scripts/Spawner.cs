using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject goEnemyPrefab;
    public float spawnInterval;
    public int maxSpawnCount;
    public float spawnRange;

    private List<GameObject> enemyPool;

    private void Start()
    {
        enemyPool = new List<GameObject>();
        for (int i = 0; i < maxSpawnCount; i++)
        {
            GameObject currentEnemy = Instantiate(goEnemyPrefab, getRandomPosInRange(), getRandomRotation(), transform);

            Enemy enemy = currentEnemy.GetComponent<Enemy>();
            if(enemy == null)
            {
                Debug.LogError(currentEnemy.name + " need to have enemy script.");
                Debug.Break();
            }

            enemy.spawner = this;
            enemy.spawnCenter = transform.position;

            enemyPool.Add(currentEnemy);
        }

        foreach (GameObject go in enemyPool)
        {
            foreach (GameObject g in enemyPool)
            {
                if (go != g)
                    Physics.IgnoreCollision(go.GetComponent<CapsuleCollider>(), g.GetComponent<CapsuleCollider>());
            }
        }
    }

    public Vector3 getRandomPosInRange()
    {
        Vector2 pos = Random.insideUnitCircle * spawnRange;
        return new Vector3(pos.x + transform.position.x, transform.position.y, pos.y + transform.position.z);
    }

    private Quaternion getRandomRotation()
    {
        Vector3 rot = new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
        return Quaternion.Euler(rot);
    }

    public void StartSpawn()
    {

    }

    public void StopSpawn()
    {

    }
}