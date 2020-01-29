using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    private static System.Random rng = new System.Random();
    private const float roomSize = 20f;

    public GameObject roomsParent;
    private GameObject[] rooms;
    private HashSet<Vector3> usedSpaces;

    private void Awake()
    {
        usedSpaces = new HashSet<Vector3>();
        int numRooms = roomsParent.transform.childCount;
        rooms = new GameObject[numRooms];
        List<int> roomList = new List<int>();
        List<Vector3> spaces = new List<Vector3>();
        spaces.Add(new Vector3(0f, 30f, 0f));

        for (int i = 0; i<numRooms; i++)
        {
            rooms[i] = roomsParent.transform.GetChild(i).gameObject;
            roomList.Add(i);
        }
        SpawnRooms(roomList, spaces);

    }

    void SpawnRooms(List<int> roomList, List<Vector3> spaces)
    {
        if (roomList.Count < 1) return;
        //Pick a random room. Remove that room from the pool
        int roomNumber = rng.Next(roomList.Count);
        GameObject toSpawn = rooms[roomList[roomNumber]];
        roomList.RemoveAt(roomNumber);
        //Pick a random open space. Remove it from the pool, add it to the used spaces set and update new spaces
        int spaceNumber = rng.Next(spaces.Count);
        Vector3 toSpawnLocation = spaces[spaceNumber];
        spaces.RemoveAt(spaceNumber);
        usedSpaces.Add(toSpawnLocation);
        AddNewSpaces(spaces, toSpawnLocation);

        //Spawn the room, then move onto the next random room
        Instantiate(toSpawn, toSpawnLocation, Quaternion.identity);
        SpawnRooms(roomList, spaces);
    }

    void AddNewSpaces(List<Vector3> spaces, Vector3 recentSpawn)
    {
        Vector3 north = new Vector3(recentSpawn.x, recentSpawn.y, recentSpawn.z + roomSize);
        Vector3 south = new Vector3(recentSpawn.x, recentSpawn.y, recentSpawn.z - roomSize);
        Vector3 east = new Vector3(recentSpawn.x + roomSize, recentSpawn.y, recentSpawn.z );
        Vector3 west = new Vector3(recentSpawn.x - roomSize, recentSpawn.y, recentSpawn.z);
        if (!usedSpaces.Contains(north)) spaces.Add(north);
        if (!usedSpaces.Contains(south)) spaces.Add(south);
        if (!usedSpaces.Contains(east)) spaces.Add(east);
        if (!usedSpaces.Contains(west)) spaces.Add(west);
    }

}
