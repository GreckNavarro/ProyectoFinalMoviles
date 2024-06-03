using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Objects", order = 1)]
public class EnemigosSO : ScriptableObject
{
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] GameObject prefabGem;

    public float Speed { get { return speed; }}
    public int Health { get { return health; }}
    public GameObject PrefabGem { get { return prefabGem; }}



     
}
