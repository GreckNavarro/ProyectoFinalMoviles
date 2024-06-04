using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] ObjectPoolingStatic poolingStatic;
    [SerializeField] int horda;
    [SerializeField] GameObject player;
    [SerializeField] float radioNoSpawn = 5f;
    [SerializeField] float radioSpawn = 15f;

    public Action<int> InvokeSpawn;

    private void OnEnable()
    {
        InvokeSpawn += GenerateZombies;
    }
    private void OnDisable()
    {
        InvokeSpawn -= GenerateZombies;
    }


    private void Start()
    {
        poolingStatic = GetComponent<ObjectPoolingStatic>();
    }

    private void GenerateZombies(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 randompos = RandomPosition();

            poolingStatic.GetObject(player.transform, randompos);
        }
    }

    //IEnumerator SpawnZombies(int quantity)
    //{
    //    for(int i = 0; i < quantity; i++)
    //    {
    //        Vector3 randompos = RandomPosition();

    //        poolingStatic.GetObject(player.transform, randompos);
    //    }
    //    yield return null;
    //}
    public Vector3 RandomPosition()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        Vector3 spawnDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
        Vector3 spawnPosition = player.transform.position + spawnDirection * radioSpawn;
        while (Vector3.Distance(spawnPosition, player.transform.position) < radioNoSpawn)
        {
            randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            spawnDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
            spawnPosition = player.transform.position + spawnDirection * radioSpawn;
        }
        return spawnPosition;
    }
}
