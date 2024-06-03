using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] ObjectPoolingStatic poolingStatic;
    [SerializeField] int horda;
    [SerializeField] GameObject player;
    [SerializeField] float radioNoSpawn = 5f;
    [SerializeField] float radioSpawn = 15f;


    private void Start()
    {
        poolingStatic = GetComponent<ObjectPoolingStatic>();
        StartCoroutine("SpawnZombies");
    }

    IEnumerator SpawnZombies()
    {
        for(int i = 0; i < horda; i++)
        {
            Vector3 randompos = RandomPosition();

            poolingStatic.GetObject(player.transform, randompos);
        }
        yield return null;
    }
    public Vector3 RandomPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
        Vector3 spawnPosition = player.transform.position + spawnDirection * radioSpawn;
        while (Vector3.Distance(spawnPosition, player.transform.position) < radioNoSpawn)
        {
            randomDirection = Random.insideUnitCircle.normalized;
            spawnDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
            spawnPosition = player.transform.position + spawnDirection * radioSpawn;
        }
        return spawnPosition;
    }
}
