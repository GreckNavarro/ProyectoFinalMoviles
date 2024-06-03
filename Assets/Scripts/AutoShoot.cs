using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoShoot : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab del proyectil
    public float shootInterval = 1f;     // Intervalo entre disparos en segundos
    public Transform shootPoint;         // Punto desde donde se dispara
    public float detectionRadius = 10f;  // Radio de detección de enemigos

    private float timeSinceLastShot;
    private List<GameObject> nearbyEnemies = new List<GameObject>();

    void Start()
    {
        timeSinceLastShot = 0f;

        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = detectionRadius;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootInterval)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                RotateTowards(nearestEnemy.transform);
                Shoot(nearestEnemy.transform);
            }
            timeSinceLastShot = 0f;
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
                    shortestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    void Shoot(Transform target)
    {
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
}
