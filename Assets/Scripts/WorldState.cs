using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container for "global" states
public class WorldState : MonoBehaviour
{

    public Room roomPrefab;
    public GameObject canvas;
    private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
    private Room restRoom;

    void Start()
    {
        // Create all rooms
        int roomId = 0;

        // Unique "rest" room...
        restRoom = NewRoom(roomId++, "Rest Room", totalCrew, 0, RoomType.REST);
        rooms.Add(restRoom.id, restRoom);

        // ... then some "food" rooms...
        for (int i = 0; i < 3; i++)
        {
            Room r = NewRoom(roomId++, "Cantina " + i, 0, 400, RoomType.FOOD);
            rooms.Add(r.id, r);
        }

        // ... add some "relic" rooms. 
        for (int i = 0; i < 3; i++)
        {
            Room r = NewRoom(roomId++, "Bio-harzard " + i, 0, 400, RoomType.RELICS);
            rooms.Add(r.id, r);
        }

        foreach (var room in rooms)
        {
            Debug.Log("Added room " + room.Key + " of type: " + room.Value.roomType + " with " + room.Value.numberOfCrew + " members");
        }
    }

    private Room NewRoom(int id, string name, int numberOfCrew, int resourcesNb, RoomType type)
    {
        Room newRoom = (Room)Instantiate(roomPrefab) as Room;
        newRoom.transform.SetParent(canvas.transform, false);

        newRoom.id = id;
        newRoom.name = name;
        newRoom.roomStatus = RoomStatus.OPERATIONAL;
        newRoom.numberOfCrew = numberOfCrew;
        newRoom.resourcesNb = resourcesNb;
        newRoom.roomType = type;
        newRoom.worldState = this;

        return newRoom;
    }

    public void AddCrewToRoom(Room r, int crewAmount)
    {
        if (crewAmount == 0)
        {
            Debug.Log("Trying to add 0 crew to a room, not allowed");
        }
        else if (r.roomType == RoomType.REST)
        {
            Debug.Log("Trying to add crew to the rest room, not allowed");
        }
        else
        {
            if (crewAmount > 0)
            {
                // Attempting to move from rest room to target room
                if (crewAmount > restRoom.numberOfCrew)
                {
                    Debug.Log("Cannot add " + crewAmount + " when rest room only contains " + restRoom.numberOfCrew);
                }
                else
                {
                    restRoom.numberOfCrew -= crewAmount;
                    r.numberOfCrew += crewAmount;
                    Debug.Log("Added " + crewAmount + " to " + r.id  + ". It now has " + r.numberOfCrew + " crew members and rest room has " + restRoom.numberOfCrew);
                }
            }
            else
            {
                // Attempting to move from target room to rest room
                if (-crewAmount > r.numberOfCrew)
                {
                    Debug.Log("Cannot remove " + -crewAmount + " when target room only contains " + r.numberOfCrew);
                }
                else
                {
                    restRoom.numberOfCrew -= crewAmount;
                    r.numberOfCrew += crewAmount;
                    Debug.Log("Removed " + -crewAmount + " to " + r.id + ". It now has " + r.numberOfCrew + " crew members and rest room has " + restRoom.numberOfCrew);
                }
            }
        }
    }

    // Number of turns before game ends.
    public int turnCountLimit = 20;

    // Current turn count.
    public int turnCount = 1;

    // True if game is ended (won or lost). Can happend if turnCount = turnCountLimit or if player looses.
    public bool end = false;

    // Defines the number of human in the ship.
    public int totalCrew;

    // Crew that is not assigned to any room
    public int unassignedCrew;

    // Number of food units (each crew member uses 1 food per turn). If no food is left, crew will starve/die.
    int foodAmount;

    // Level of hope. Hope is not "consumed" like food is. It will affect capacity of crew during events.
    int hopeLevel;


    // Rooms & crew in them



    // Increase turn count. Perform "end of turn"
    public void NextTurn()
    {
        // Consume food (1 unit per crew)
        foodAmount -= totalCrew;
        if (foodAmount < 0)
        {
            end = true;
            Debug.Log("Negative food, you loose");
        }

        // Check if some crew remains
        if (totalCrew <= 0)
        {
            end = true;
            Debug.Log("Negative crew (all people died), you loose");
        }

        if (!end)
        {
            // Increase turn count & check for max number of turns
            turnCount += 1;
            if (turnCount < turnCountLimit)
                Debug.Log("TurnCount increased to " + turnCount);
            else
            {
                end = true;
                Debug.Log("Reached max turn, you win");
            }
        }
    }

}
