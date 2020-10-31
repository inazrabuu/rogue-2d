using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int mapWidth = 7;
    public int mapHeight = 7;
    public int roomsToGeneratee = 12;
    public GameObject roomPrefab;

    private int _roomCount;
    private bool _roomsInstantiated;
    private Vector2 _firstRoomPos;
    private bool[,] _map;
    private List<Room> _roomObject = new List<Room>();

    public static Generation Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Generate()
    {

    }

    void CheckRoom(int x, int y, int remaining, Vector2 generalDirection, bool firstRoom = false)
    {

    }

    void InstantiateRooms()
    {

    }

    void CalculateKeyAndExit()
    {

    }
}
