﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public Room startRoomPrefab, endRoomPrefab;
    public GameObject pickupPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3, 20);
    public GameObject playerprefab;

    List<Doorway> avaliableDoorways = new List<Doorway>();

    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();

    LayerMask roomLayerMask;

    GameObject player;
    GameObject shotgunPickup;


    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
    }
    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        //Place start room
        PlaceStartRoom();
        yield return interval;

        //Random iterations

        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);
        for(int i=0; i< iterations; i++)
        {
            // Place random room from list
            PlaceRoom();
            yield return interval;
        }
        //Place action room
        PlaceInclined();
        PlaceActionRoom();
       // PlaceInclined();
        for (int i = 0; i < iterations; i++)
        {
            // Place random room from list
            PlaceRoom();
            yield return interval;
        }
        //Place end room
        PlaceEndRoom();
        yield return interval;
        //Level generation finished
        Debug.Log("Level generation finished");

        yield return new WaitForSeconds(3);
        //Place player
        player = Instantiate(playerprefab);
        player.transform.position = startRoom.playerSpawn.position;
        player.transform.rotation = startRoom.playerSpawn.rotation;

        //Place gun pickup
        shotgunPickup = Instantiate(pickupPrefab);
        shotgunPickup.transform.position = startRoom.pickupSpawn.position;
        //ResetLevelGenerator();

    }
    void PlaceStartRoom()
    {
        //Instantiate room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;
        //Get doorways from current room and add then randomly to the list of doorways
        AddDoorwaysToList(startRoom, ref avaliableDoorways);

        //Position room
        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
        
    }
    
    void AddDoorwaysToList(Room room, ref List<Doorway> list)
    {
        foreach(Doorway doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);

        }
    }

    void PlaceRoom()
    {
        //Instatntiate room
        Room currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count-1)]) as Room;
        currentRoom.transform.parent = this.transform;

        //Crete doorway lists to loop over
        List<Doorway> allAvaliableDoorways = new List<Doorway>(avaliableDoorways);
        List<Doorway> currentRoomDoorways = new List<Doorway>();
        AddDoorwaysToList(currentRoom, ref currentRoomDoorways);

        //Get doorways from current room and add then randomly to the list of avaliable doorways
        AddDoorwaysToList(currentRoom, ref avaliableDoorways);

        bool roomPlaced = false;

        //Try all avaliable doorways
        foreach(Doorway avaliableDoorway in allAvaliableDoorways)
        {
            //Try all avaliable doorways in current room
            foreach(Doorway currentDoorway in currentRoomDoorways)
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

    void PositionRoomAtDoorway(ref Room room, Doorway roomDoorway, Doorway targetDoorway) {
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

    bool CheckRoomOverlap(Room room) {
        Bounds bounds = room.RoomBounds;
       // bounds.Expand(-0.1f);

       // roomLayerMask = 9;
       Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
      
        if (colliders.Length > 0)
        {
            Debug.Log("Collider length: "+colliders.Length);
            Debug.Log("Collider 1: "+colliders[0]);
            //Ignore collisions with current room
            foreach (Collider c in colliders)
            {
                if ((c.transform.parent.gameObject.Equals(room.gameObject)))
                {

                    continue;
                    
                }
                else
                {
                    Debug.LogError("Overlap detected");
                    //  ResetLevelGenerator();
                    Debug.LogError(c);
                    return true;
                    
                }
            }
        }
        return false;
    }

    void PlaceEndRoom()
    {
        //Instatntiate room
        endRoom= Instantiate(endRoomPrefab) as EndRoom;
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

            //Exit loop if room has been placed
            break;
        }
        //Room couldn't be placed. Restart generator and try again
        if (!roomPlaced)
        {
         
            ResetLevelGenerator();
        }

    }
   void PlaceActionRoom()
    {
        //Instatntiate room
        Room currentRoom = Instantiate(roomPrefabs[3]) as Room;
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

    void PlaceInclined()
    {
        //Instatntiate room
        Room currentRoom = Instantiate(roomPrefabs[1]) as Room;
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

    void ResetLevelGenerator()
    {
        Debug.LogError("Reset level generator");

        StopCoroutine("GenerateLevel");
        //Delete all rooms
        if (startRoom)
        {
            Destroy(startRoom.gameObject);

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