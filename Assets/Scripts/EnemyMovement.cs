using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("BodyPlayer").transform;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BodyPlayer")
        {
           Destroy(this.gameObject);
        }
    }
}
