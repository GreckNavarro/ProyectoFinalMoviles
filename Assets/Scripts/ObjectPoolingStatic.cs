using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingStatic : MonoBehaviour
{
    public List<GameObject> objectPool;
    public GameObject objPref;
    public int maxQuantity;

    private void Awake()
    {
        InstantiateObjects();
    }

    public void InstantiateObjects()
    {
        GameObject tmp;
        for(int i = 0; i < maxQuantity; i++)
        {
            tmp = Instantiate(objPref, transform.position, transform.rotation);
            tmp.GetComponent<EnemyMovement>().ChangeObjectPooling(this);
            objectPool.Add(tmp);
            tmp.transform.SetParent(this.transform);
            tmp.SetActive(false);
        }
    }
    public void GetObject(Transform player, Vector3 posinicial)
    {
        if(objectPool.Count > 0)
        {
            GameObject tmp = objectPool[0];
            objectPool.Remove(tmp);
            tmp.SetActive(true);
            tmp.GetComponent<EnemyMovement>().SetTarget(player, posinicial);


        }
        else
        {
            Debug.Log("No hay más objetos en el pool");
        }
    }
    public void SetObject(GameObject obj)
    {
        
        objectPool.Add(obj);

    }

}
