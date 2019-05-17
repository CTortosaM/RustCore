using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelBuilder : MonoBehaviour
{
    public Room startRoomPrefab, endRoomPrefab;
    public List<AIEnemigo> enemyPrefabs;
 
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3, 20);
    public GameObject playerprefab;
    public GameObject pickupPrefab;
    List<Doorway> avaliableDoorways = new List<Doorway>();
    public delegate void LevelGeneration();
    public static event LevelGeneration onLevelFinished;
    public static event LevelGeneration startingGeneration;
    [SerializeField] private NavMeshSurface surface;

    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();

    LayerMask roomLayerMask;
    GameObject player;
    GameObject shotgunPickup;
    void Awake()
    {
        //startingGeneration();
    }
    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        //Place start room
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
        PlaceStartRoom();

       

        //Place player
        player = Instantiate(playerprefab);
        player.transform.position = startRoom.playerSpawn.transform.position;
        player.transform.rotation = startRoom.playerSpawn.transform.rotation;

        //startingGeneration();


        // player.active = false;
        //Place gun pickup



        //ResetLevelGenerator();

        StartCoroutine("GenerateLevel");
    }
    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;
        AddDoorwaysToList(startRoom, ref avaliableDoorways);

        yield return interval;
        PlaceRoom(true);
        //Random iterations

        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);
        for (int i = 0; i < iterations; i++)
        {
            // Place random room from list
            PlaceRoom(false);
            yield return interval;
        }

        //Place end room
        PlaceEndRoom();
        yield return interval;
        //Level generation finished
        Debug.Log("Level generation finished");

       
        surface.BuildNavMesh();

        foreach (Room room in placedRooms)
        {
            foreach (Transform enemySpawn in room.enemySpawns)
            {
                int random = Random.Range(0, enemyPrefabs.Count);

                if (random == 1)
                {
                    Instantiate(enemyPrefabs[random], new Vector3(enemySpawn.transform.position.x, enemySpawn.transform.position.y+1, enemySpawn.transform.position.z), new Quaternion(0, 0, 0, 0), this.transform);
                }
                else
                {
                    Instantiate(enemyPrefabs[random], enemySpawn.transform.position , new Quaternion(0, 0, 0, 0), this.transform);
                
            }
            }
        }
        shotgunPickup = Instantiate(pickupPrefab);
        shotgunPickup.transform.position = startRoom.pickupSpawn.position;

        SceneManager.UnloadSceneAsync("LoadingScreen");
        onLevelFinished();

        yield return new WaitForSeconds(1);





        //player.active = true;
    }
    void PlaceStartRoom()
    {
        //Instantiate room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;
        //Get doorways from current room and add then randomly to the list of doorways


        //Position room
        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;

    }

    void AddDoorwaysToList(Room room, ref List<Doorway> list)
    {
        foreach (Doorway doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);

        }
    }

    void PlaceRoom(bool isMainCorridor)
    {
        int num;
        //Instantiate room
        if (isMainCorridor)
        {
            num = 0;
        }
        else
        {
            num = Random.Range(1, roomPrefabs.Count);
        }
        Room currentRoom = Instantiate(roomPrefabs[num]) as Room;
        currentRoom.transform.parent = this.transform;

        //Crete doorway lists to loop over
        List<Doorway> allAvaliableDoorways = new List<Doorway>(avaliableDoorways);
        List<Doorway> currentRoomDoorways = new List<Doorway>();
        AddDoorwaysToList(currentRoom, ref currentRoomDoorways);

        //Get doorways from current room and add then randomly to the list of avaliable doorways
        AddDoorwaysToList(currentRoom, ref avaliableDoorways);

        bool roomPlaced = false;

        //Try all avaliable doorways
        foreach (Doorway avaliableDoorway in allAvaliableDoorways)
        {
            //Try all avaliable doorways in current room
            foreach (Doorway currentDoorway in currentRoomDoorways)
            {
                //Position room
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, avaliableDoorway);

                //Check room overlaps
                if (CheckRoomOverlap(currentRoom))
                {
                    continue;
                }
                roomPlaced = true;

                //Add room to list
                placedRooms.Add(currentRoom);

                //Remove occupied doorways
                currentDoorway.gameObject.SetActive(false);
                avaliableDoorways.Remove(currentDoorway);

                avaliableDoorway.gameObject.SetActive(false);
                avaliableDoorways.Remove(avaliableDoorway);



                //Exit loop if room has been placed
                break;
            }
            //Exit loop if room has been placed
            if (roomPlaced)
            {
                break;
            }

        }
        //Room couldn't be placed. Restart generator and try again
        if (!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            ResetLevelGenerator();
        }

    }

    void PositionRoomAtDoorway(ref Room room, Doorway roomDoorway, Doorway targetDoorway)
    {
        //Reset room position and rotation
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;

        //Rotate room to match previous doorway orientation
        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);

        //Position room
        Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
        room.transform.position = targetDoorway.transform.position - roomPositionOffset;

    }

    bool CheckRoomOverlap(Room room)
    {
        Bounds bounds = room.RoomBounds;
        bounds.Expand(-0.1f);

        // roomLayerMask = 9;
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);

        if (colliders.Length > 0)
        {
            Debug.Log("Collider length: " + colliders.Length);
            Debug.Log("Collider 1: " + colliders[0]);
            Debug.Log("Collider 1: " + colliders[colliders.Length - 1]);
            //Ignore collisions with current room

            foreach (Collider c in colliders)
            {
                //c.transform.parent.gameObject.Equals(room.gameObject) ||
                if (c.transform.parent.gameObject.Equals(room.gameObject) || c.transform.gameObject.Equals(room.gameObject))
                {

                    continue;

                }
                else
                {
                    Debug.LogError("Overlap detected");
                    //ResetLevelGenerator();
                    Debug.LogError(c);
                    return true;

                }
            }
        }
        return false;
    }

    void PlaceEndRoom()
    {
        //Instantiate room
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;

        //Crete doorway lists to loop over
        List<Doorway> allAvaliableDoorways = new List<Doorway>(avaliableDoorways);
        Doorway doorway = endRoom.doorways[0];

        bool roomPlaced = false;

        //Try all avaliable doorways
        foreach (Doorway avaliableDoorway in allAvaliableDoorways)
        {
            //Position room
            Room room = (Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, avaliableDoorway);

            //Check room overlaps
            if (CheckRoomOverlap(endRoom))
            {
                continue;
            }
            roomPlaced = true;
            //Remove occupied doorways
            doorway.gameObject.SetActive(false);
            avaliableDoorways.Remove(doorway);

            avaliableDoorway.gameObject.SetActive(false);
            avaliableDoorways.Remove(avaliableDoorway);

            /* for (int i = 0; i < avaliableDoorways.Count; i++)
             {
                 for (int j = 0; j < avaliableDoorways.Count; j++)
                 {
                     if (avaliableDoorways[i] != avaliableDoorways[j])
                     {
                         if (Mathf.Abs(avaliableDoorways[j].transform.position.x - avaliableDoorways[i].transform.position.x) > 0.1 || Mathf.Abs(avaliableDoorways[j].transform.position.y - avaliableDoorways[i].transform.position.y) > 0.1 || Mathf.Abs(avaliableDoorways[j].transform.position.z - avaliableDoorways[i].transform.position.z) > 0.1)
                         {
                             avaliableDoorways[i].gameObject.SetActive(false);
                             avaliableDoorways.Remove(avaliableDoorways[i]);

                             avaliableDoorways[j].gameObject.SetActive(false);
                             avaliableDoorways.Remove(avaliableDoorways[j]);
                             break;
                         }
                     }
                 }
             }*/
            //Exit loop if room has been placed
            break;
        }
        //Room couldn't be placed. Restart generator and try again
        if (!roomPlaced)
        {

            ResetLevelGenerator();
        }

    }




    void ResetLevelGenerator()
    {
        Debug.LogError("Reset level generator");

        StopCoroutine("GenerateLevel");
        //Delete all rooms
        if (startRoom)
        {
            //  Destroy(startRoom.gameObject);

        }
        if (endRoom)
        {
            Destroy(endRoom.gameObject);

        }
        foreach (Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }
        //Clear lists
        placedRooms.Clear();
        avaliableDoorways.Clear();

        //Reset coroutine
        StartCoroutine("GenerateLevel");
    }
}