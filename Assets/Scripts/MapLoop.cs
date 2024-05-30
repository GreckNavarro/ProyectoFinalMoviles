using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoop : MonoBehaviour
{
    private Vector3 pos = Vector3.zero;
    [SerializeField] private GameObject prefObject;
    [SerializeField] float spacing = 10.0f;
    int[,] map = new int[3, 3]
    {
        { 1, 1, 1 },
        { 1, 1, 1 },
        { 1, 1, 1 }
    };
    Transform player;
    Vector3 terrainPos = Vector3.zero;
    Vector3 playerPos = Vector3.zero;
    Vector3 initialDiff = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateInitialMap();
        initialDiff = player.position - transform.position;
        initialDiff.y = 0;

        initialDiff.x = Mathf.Floor(initialDiff.x);
        initialDiff.z = Mathf.Floor(initialDiff.z);

        playerPos.x = Mathf.Floor(player.position.x);
        playerPos.z = Mathf.Floor(player.position.z);
        playerPos.y = 0;

        terrainPos = playerPos - initialDiff;
        transform.position = terrainPos;
    }

    private void GenerateInitialMap()
    {
        for (int z = 0; z < map.GetLength(0); z++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                pos.z = z * spacing;
                pos.x = x * spacing;
                Instantiate(prefObject, pos, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMap();
    }

    private void UpdateMap()
    {
        playerPos.x = Mathf.Floor(player.position.x);
        playerPos.z = Mathf.Floor(player.position.z);
        playerPos.y = 0;

        terrainPos = playerPos - initialDiff;
        transform.position = terrainPos;
    }
}
