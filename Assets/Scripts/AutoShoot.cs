using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoShoot : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public float shootInterval = 1.0f;    
    public Transform shootPoint;       
    public float detectionRadius = 10f; 

    private float timeSinceLastShot;
    [SerializeField] private bool shooting;
    private List<GameObject> nearbyEnemies = new List<GameObject>();

    public static Action<GameObject> QuitarZombie;

    public bool Shooting { get { return shooting; } }

    private void OnEnable()
    {
        QuitarZombie += RemoveZombie;
        SystemExp.newLvl += IncrementPower;

    }
    private void OnDisable()
    {
        QuitarZombie -= RemoveZombie;
        SystemExp.newLvl -= IncrementPower;
    }

    void Start()
    {
        timeSinceLastShot = 0f;

        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = detectionRadius;
        shooting = false;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootInterval)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && nearestEnemy.activeSelf == true)
            {
 
                RotateTowards(nearestEnemy.transform);
                Shoot(nearestEnemy.transform);
            }
            else
            {
                shooting = false;
            }
            timeSinceLastShot = 0f;
        }
    }

    private void IncrementPower()
    {
        if(shootInterval > 0.2f)
        {
            shootInterval -= 0.2f;
        }
    }
    private void RotateTowards(Transform target)
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in nearbyEnemies)
        {
            if (enemy != null)  // Verificar si el enemigo no ha sido destruido
            {
                float distance = Vector3.Distance(enemy.transform.position, position);
                if (distance < shortestDistance)
                {
                    shooting = true;
                    shortestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    void Shoot(Transform target)
    {
        Debug.Log("Disparo");
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(target);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemies.Remove(other.gameObject);
        }
    }
    void RemoveZombie(GameObject Zombie)
    {
        Debug.Log("Quitando");
        nearbyEnemies.Remove(Zombie);
    }
}
