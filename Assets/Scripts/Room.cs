﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    public Transform northDoor;
    public Transform westDoor;
    public Transform southDoor;
    public Transform eastDoor;

    [Header("Walls")]
    public Transform northWall;
    public Transform westWall;
    public Transform southWall;
    public Transform eastWall;

    [Header("Values")]
    public int insideWidth;
    public int insideHeight;

    [Header("Prefab")]
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public GameObject keyPrefab;
    public GameObject exitDoorPrefab;

    private List<Vector3> _usedPositions = new List<Vector3>();

    public void GenerateInterior()
    {
        if (Random.value < Generation.Instance.enemySpawnChances)
        {
            SpawnPrefab(enemyPrefab, 1, Generation.Instance.maxEnemiesPerRoom + 1);
        }

        if (Random.value < Generation.Instance.coinSpawnChances)
        {
            SpawnPrefab(coinPrefab, 1, Generation.Instance.maxCoinPerRoom + 1);
        }

        if (Random.value < Generation.Instance.healthSpawnChances)
        {
            SpawnPrefab(healthPrefab, 1, Generation.Instance.maxHealthPerRoom + 1);
        }
    }

    public void SpawnPrefab(GameObject prefab, int min = 0, int max = 0)
    {
        int num = 1;
        if (min > 0 || max > 0)
        {
            num = Random.Range(min, max);
        }

        for (int x = 0; x < num; x++)
        {
            GameObject obj = Instantiate(prefab);
            Vector3 pos = transform.position + new Vector3(Random.Range(-insideWidth / 2, insideWidth / 2 + 1), Random.Range(-insideHeight / 2, insideHeight / 2 + 1), 0);

            while (_usedPositions.Contains(pos))
            {
                pos = transform.position + new Vector3(Random.Range(-insideWidth / 2, insideWidth / 2 + 1), Random.Range(-insideHeight / 2, insideHeight / 2 + 1), 0);
            }

            obj.transform.position = pos;
            _usedPositions.Add(pos);

            if (prefab == enemyPrefab)
            {
                EnemyManager.Instance.enemies.Add(obj.GetComponent<Enemy>());
            }
        }
    }
}
