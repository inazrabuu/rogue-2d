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
    private List<Room> _roomObjects = new List<Room>();

    public static Generation Instance;

    public float enemySpawnChances;
    public float coinSpawnChances;
    public float healthSpawnChances;

    public int maxEnemiesPerRoom;
    public int maxCoinPerRoom;
    public int maxHealthPerRoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int seed = PlayerPrefs.GetInt("Seed");
        Random.InitState(seed);
        Generate();
    }

    public void Generate()
    {
        _map = new bool[mapHeight, mapWidth];
        CheckRoom(3, 3, 0, Vector2.zero, true);
        InstantiateRooms();
        FindObjectOfType<Player>().transform.position = _firstRoomPos * 12;

        UIManager.Instance.map.texture = MapTextureGenerator.Generate(_map, _firstRoomPos);
    }

    public void OnPlayerMove()
    {
        Vector2 playerPos = FindObjectOfType<Player>().transform.position;
        Vector2 roomPos = new Vector2(((int)playerPos.x + 6) / 12, ((int)playerPos.y + 6) / 12);

        UIManager.Instance.map.texture = MapTextureGenerator.Generate(_map, roomPos);
    }

    void CheckRoom(int x, int y, int remaining, Vector2 generalDirection, bool firstRoom = false)
    {
        if (_roomCount >= roomsToGeneratee)
        {
            return;
        }

        if (x < 0 || x > mapWidth - 1 || y < 0 || y > mapHeight - 1)
        {
            return;
        }

        if (firstRoom == false && remaining <= 0)
        {
            return;
        }

        if (_map[x, y] == true)
        {
            return;
        }

        if (firstRoom == true)
        {
            _firstRoomPos = new Vector2(x, y);
        }

        _roomCount++;
        _map[x, y] = true;

        bool north = Random.value > (generalDirection == Vector2.up ? 0.2f : 0.8f);
        bool south = Random.value > (generalDirection == Vector2.down ? 0.2f : 0.8f);
        bool east = Random.value > (generalDirection == Vector2.right ? 0.2f : 0.8f);
        bool west = Random.value > (generalDirection == Vector2.left ? 0.2f : 0.8f);

        int maxRemaining = roomsToGeneratee / 4;

        if (north || firstRoom)
        {
            CheckRoom(x, y + 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.up : generalDirection);
        }

        if (south || firstRoom)
        {
            CheckRoom(x, y - 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.down : generalDirection);
        }

        if (east || firstRoom)
        {
            CheckRoom(x + 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.right : generalDirection);
        }

        if (west || firstRoom)
        {
            CheckRoom(x - 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.left : generalDirection);
        }
    } 

    void InstantiateRooms()
    {
        if (_roomsInstantiated)
        {
            return;
        }

        _roomsInstantiated = true;

        for (int x = 0; x < mapWidth; ++x)
        {
            for (int y = 0; y < mapHeight; ++y)
            {
                if (_map[x, y] == false)
                {
                    continue;
                }

                GameObject roomObj = Instantiate(roomPrefab, new Vector3(x, y, 0) * 12, Quaternion.identity);
                Room room = roomObj.GetComponent<Room>();

                if (y < mapHeight - 1 && _map[x, y + 1] == true)
                {
                    room.northDoor.gameObject.SetActive(true);
                    room.northWall.gameObject.SetActive(false);
                }

                if (y > 0 && _map[x, y - 1] == true)
                {
                    room.southDoor.gameObject.SetActive(true);
                    room.southWall.gameObject.SetActive(false);
                }

                if (x < mapWidth - 1 && _map[x + 1, y] == true)
                {
                    room.eastDoor.gameObject.SetActive(true);
                    room.eastWall.gameObject.SetActive(false);
                }

                if (x > 0 && _map[x - 1, y] == true)
                {
                    room.westDoor.gameObject.SetActive(true);
                    room.westWall.gameObject.SetActive(false);
                }

                if (_firstRoomPos != new Vector2(x, y))
                {
                    room.GenerateInterior();
                }

                _roomObjects.Add(room);
            }
        }

        CalculateKeyAndExit();
    }

    void CalculateKeyAndExit()
    {
        float maxDistance = 0;
        Room a = null;
        Room b = null;

        foreach (Room aRoom in _roomObjects)
        {
            foreach (Room bRoom in _roomObjects)
            {
                float distance = Vector3.Distance(aRoom.transform.position, bRoom.transform.position);
                if (distance > maxDistance)
                {
                    a = aRoom;
                    b = bRoom;
                    maxDistance = distance;
                }
            }
        }

        a.SpawnPrefab(a.keyPrefab);
        b.SpawnPrefab(b.exitDoorPrefab);
    }
}
