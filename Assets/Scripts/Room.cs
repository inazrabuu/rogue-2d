using System.Collections;
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

    }

    public void SpawnPrefab(GameObject prefab, int min = 0, int max = 0)
    {

    }
}
