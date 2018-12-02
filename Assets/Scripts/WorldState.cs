using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 
public class WorldState : MonoBehaviour
{

    public Room roomPrefab;
    public GameObject canvas;
    private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
    private Room commonRoom;

    // ====================== ROOMS ====================== 
    // Crew are humans used by the IA/player to save rooms. Each crew member adds to the save potential but will die.
    #region ROOMS

    // initializes room
    public void Init()
    {
        // Create all rooms
        int roomId = 0;

        // Unique "common" room...
        commonRoom = CreateRoom(roomId++, "Common room", 50, 0, RoomType.COMMON);
        this.rooms.Add(commonRoom.id, commonRoom);

        // ... then some "food" rooms...
        for (int i = 0; i < 3; i++)
        {
            Room r = CreateRoom(roomId++, string.Format("Cantina #{0}", i + 1), 0, 400, RoomType.FOOD);
            this.rooms.Add(r.id, r);
        }

        // ... add some "relic" rooms. 
        for (int i = 0; i < 3; i++)
        {
            Room r = CreateRoom(roomId++, string.Format("Relic room #{0}", i + 1), 0, 10, RoomType.RELICS);
            this.rooms.Add(r.id, r);
        }

        int nbCrew = commonRoom.numberOfCrew / 6;
        foreach (var room in this.rooms)
        {
            if (room.Value.roomType != RoomType.COMMON) { AddCrewToRoom(room.Value, nbCrew); }
            Debug.Log(room);
        }

        InitialCrew = CurrentCrew();
    }

    private Room CreateRoom(int id, string name, int numberOfCrew, int resourcesNb, RoomType type)
    {
        Room newRoom = (Room)Instantiate(roomPrefab) as Room;
        newRoom.transform.SetParent(canvas.transform, false);

        newRoom.id = id;
        newRoom.roomName = name;
        newRoom.roomStatus = RoomStatus.OPERATIONAL;
        newRoom.numberOfCrew = numberOfCrew;
        newRoom.resourcesNb = resourcesNb;
        newRoom.roomType = type;
        newRoom.worldState = this;

        return newRoom;
    }

    // return rooms as list
    public List<Room> GetRooms()
    {
        return rooms.Values.ToList();
    }
    #endregion ROOMS

    // ====================== CREW ====================== 
    // Crew are humans used by the IA/player to save rooms. Each crew member adds to the save potential but will die.
    #region CREW
    private int InitialCrew;

    // Get current total number of crew members
    public int CurrentCrew()
    {
        return this.rooms.Values.Where(r => r.roomStatus == RoomStatus.OPERATIONAL).Sum(r => r.numberOfCrew);
    }

    // Moves crew between the common room and the other rooms
    public void AddCrewToRoom(Room r, int crewAmount)
    {
        if (crewAmount == 0)
        {
            Debug.Log("Trying to add 0 crew to a room, not allowed");
        }
        else if (r.roomType == RoomType.COMMON)
        {
            Debug.Log("Trying to add crew to the common room, not allowed");
        }
        else
        {
            if (crewAmount > 0)
            {
                // Attempting to move from common room to target room
                if (crewAmount > commonRoom.numberOfCrew)
                {
                    Debug.Log("Cannot add " + crewAmount + " when common room only contains " + commonRoom.numberOfCrew);
                }
                else
                {
                    commonRoom.numberOfCrew -= crewAmount;
                    r.numberOfCrew += crewAmount;
                    Debug.Log("Added " + crewAmount + " to " + r.id + ". It now has " + r.numberOfCrew + " crew members and common room has " + commonRoom.numberOfCrew);
                }
            }
            else
            {
                // Attempting to move from target room to common room
                if (-crewAmount > r.numberOfCrew)
                {
                    Debug.Log("Cannot remove " + -crewAmount + " when target room only contains " + r.numberOfCrew);
                }
                else
                {
                    commonRoom.numberOfCrew -= crewAmount;
                    r.numberOfCrew += crewAmount;
                    Debug.Log("Removed " + -crewAmount + " to " + r.id + ". It now has " + r.numberOfCrew + " crew members and common room has " + commonRoom.numberOfCrew);
                }
            }
        }
    }
    #endregion CREW

    // ====================== FOOD ====================== 
    // Food is a stock present in the ship at the start of a game. Food is consumed automatically each turn (1 unit per crew).
    // Food is stored into rooms. Loosing a food room removes all food it contained. Food is consumed from food rooms randomly.
    #region FOOD
    // Get current total amount of food
    public int CurrentFood()
    {
        return this.rooms.Values.Where(r => r.roomType == RoomType.FOOD && r.roomStatus == RoomStatus.OPERATIONAL).Sum(r => r.resourcesNb);
    }

    // Consume food from rooms
    public void ConsumeFood(int amount)
    {
        while (amount > 0)
        {
            // We do fetch the list of room each time to ensure we correctly filter empty rooms. This is not efficient but should be correct.
            List<Room> rooms = this.rooms.Values.Where(r => r.roomType == RoomType.FOOD && r.roomStatus == RoomStatus.OPERATIONAL && r.resourcesNb > 0).ToList();
            if (rooms.Count == 0)
            {
                break;
            }
            rooms[Random.Range(0, rooms.Count)].resourcesNb -= 1;
            amount--;
        }
    }
    #endregion FOOD

    // ====================== HOPE ====================== 
    // For every crew sacrified, the hope level of the remaining humans decrease. We only count the number of death, not if they were able to save
    // a room or not.
    #region HOPE

    public float CurrentHope()
    {
        return 1f;
        //return CurrentCrew() / ((float)InitialCrew);
    }

    public string CurrentHopeFormatted()
    {
        return string.Format("{0}%", Mathf.Round(this.CurrentHope() * 100));
    }
    #endregion HOPE

    // ====================== RELICS ====================== 
    // Relics are the cargo we transport and hopefully deliver.

    public int CurrentRelics()
    {
        return this.rooms.Values.Where(r => r.roomType == RoomType.RELICS && r.roomStatus == RoomStatus.OPERATIONAL).Sum(r => r.resourcesNb);
    }






}
