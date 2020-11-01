using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerTransform;

    private List<GameObject> activeTiles;

    private float spawnZ = 0.0f;
    private float tileLength = 24.0f;
    private float isSafe = 12.0f;

    private int amnTilesOnScreen = 3;
    private int lastPrefabIndex = 0;

    private void Start()
    {
        activeTiles = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            if (i < 2)
                SpawnTile(0); //Empty Bridge
            else
                SpawnTile();
        }
    }
    private void Update()
    {
        if (playerTransform.position.z - isSafe > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject gameobject;

        if (prefabIndex == -1)
            gameobject = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            gameobject = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        gameobject.transform.SetParent(transform);
        gameobject.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(gameobject);
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
