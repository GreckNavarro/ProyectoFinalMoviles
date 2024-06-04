using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] ObjectPoolingStatic pooling;
    [SerializeField] GameObject targetObject;
    [SerializeField] EnemigosSO typeEnemy;
    [SerializeField] int currentLife;

    public Action<int> TakeDamage;
    public static Action OnDeath;

    private void OnEnable()
    {
        TakeDamage += Damaged;
    }
    private void OnDisable()
    {
        TakeDamage -= Damaged;
    }

    public void ChangeObjectPooling(ObjectPoolingStatic pool)
    {
        pooling = pool;
    }
    public void SetTarget(Transform player, Vector3 position)
    {
        target = player;
        transform.position = position;
        currentLife = typeEnemy.Health;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, typeEnemy.Speed * Time.deltaTime);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public void Damaged(int damage)
    {
        currentLife -= damage;
        if (currentLife < 0)
        {
            DesactiveObject();
            OnDeath?.Invoke();
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BodyPlayer")
        {
            DesactiveObject();
        }
    }

    private void DesactiveObject()
    {
        AutoShoot.QuitarZombie(this.gameObject);
        pooling.SetObject(this.gameObject);
        GameObject gems = Instantiate(typeEnemy.PrefabGem, transform.position, Quaternion.identity);
        gems.AddComponent<GemsController>();
        BoxCollider collidergems =gems.AddComponent<BoxCollider>();
        collidergems.isTrigger = true;
        gameObject.SetActive(false);
    }
}
