using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TurnPhases { DRAMA, MOVE }

public class WorldState : MonoBehaviour
{

    public Room roomPrefab;
    public GameObject canvas;
    private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
    private Room commonRoom;

    void Start()
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
        InitialRelics = CurrentRelics();

        foreach (var room in this.rooms)
        {
            if (room.Value.roomType != RoomType.COMMON)
            {
                int nbCrew = Random.Range(0, commonRoom.numberOfCrew / 2);
                AddCrewToRoom(room.Value, nbCrew);
            }
            Debug.Log(room);
        }
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

    // ====================== CREW ====================== 

    // Get current total number of crew members
    public int CurrentCrew()
    {
        return this.rooms.Values.Sum(r => r.numberOfCrew);
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

    // ====================== FOOD ====================== 
    // Food is a stock present in the ship at the start of a game. Food is consumed automatically each turn (1 unit per crew).
    // Food is stored into rooms. Loosing a food room removes all food it contained. Food is consumed from food rooms randomly.

    // Get current total amount of food
    public int CurrentFood()
    {
        return this.rooms.Values.Where(r => r.roomType == RoomType.FOOD).Sum(r => r.resourcesNb);
    }

    // Consume food from rooms
    private void ConsumeFood(int amount)
    {
        while (amount > 0)
        {
            // We do fetch the list of room each time to ensure we correctly filter empty rooms. This is not efficient but should be correct.
            List<Room> rooms = this.rooms.Values.Where(r => r.roomType == RoomType.FOOD && r.resourcesNb > 0).ToList();
            rooms[Random.Range(0, rooms.Count)].resourcesNb -= 1;
        }
    }

    private void LogFoodRoom()
    {
        this.rooms.Values.Where(r => r.roomType == RoomType.FOOD).ToList().ForEach(r => Debug.Log(r.roomName + " " + r.resourcesNb));
    }

    // ====================== HOPE ====================== 
    // Unlike Food, Hope is not a stock. Hope is calculated based on the number of Relics on the ship relative to the initial number of relics

    public int CurrentRelics()
    {
        return this.rooms.Values.Where(r => r.roomType == RoomType.RELICS).Sum(r => r.resourcesNb);
    }

    private int InitialRelics;

    public float CurrentHope()
    {
        return CurrentRelics() / ((float)InitialRelics);
    }

    // ====================== TURNS ====================== 

    // Number of turns before game ends.
    private int TurnCountLimit = 20;

    // Current turn count.
    public int CurrentTurn = 1;

    public TurnPhases CurrentPhase = TurnPhases.DRAMA;


    // True if game is ended (won or lost). Can happend if turnCount = turnCountLimit or if player looses.
    public bool End = false;

    public void AfterDrama()
    {
        CurrentPhase = TurnPhases.MOVE;
    }

    public void NextTurn()
    {
        CurrentPhase = TurnPhases.DRAMA;

        Drama newDrama = Drama.CreateRandomOne(this.rooms.Values.ToList());
        Debug.Log(newDrama);
        DramaReport newReport = DramaSolver.Process(newDrama, DramaSolvingOption.TryToSaveBoth);
        Debug.Log(newReport);

        // Consume food (1 unit per crew)
        ConsumeFood(CurrentCrew());
        if (CurrentFood() < 0)
        {
            End = true;
            Debug.Log("!! Negative food, you loose");
        }
        LogFoodRoom();

        // Check if some crew remains
        if (CurrentCrew() <= 0)
        {
            End = true;
            Debug.Log("!! Negative crew (all people died), you loose");
        }

        if (!End)
        {
            // Increase turn count & check for max number of turns
            CurrentTurn += 1;
            if (CurrentTurn < TurnCountLimit)
                Debug.Log("TurnCount increased to " + CurrentTurn);
            else
            {
                End = true;
                Debug.Log("!! Reached max turn, you win");
            }
        }
    }

}
