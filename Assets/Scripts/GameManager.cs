using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpawnZombie spawner;
    [SerializeField] float timeNewOleada;
    [SerializeField] int quantityspawn;

    private void OnEnable()
    {
        SystemExp.levelUp += IncrementDificult;
    }
    void Start()
    {
        quantityspawn = 1;
        StartCoroutine(GenerateZombies());

    }
    IEnumerator GenerateZombies()
    {
        spawner.InvokeSpawn(quantityspawn);
        yield return new WaitForSeconds(timeNewOleada);
        StartCoroutine(GenerateZombies());
    }

    public void IncrementDificult(int value)
    {
        quantityspawn += 1;
    }

    
}
