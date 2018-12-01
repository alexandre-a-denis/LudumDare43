using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container for "global" states
public class WorldState : MonoBehaviour
{

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

    // =============== Room management =====================

    private Dictionary<int, Room> rooms = new Dictionary<int, Room>();

    private void InitRooms()
    {
        int roomId = 0;
        int nbFoodRooms = 3;
        int nbHopeRooms = 3;

        for (int i = 0; i < nbFoodRooms; i++)
        {
            rooms.Add(roomId++, new Room("food"));
        }
    }


    public void AddCrewToRoom(string roomId, int nbCrewToAssign)
    {
        if (nbCrewToAssign > unassignedCrew)
        {

        }
        else
        {

        }
    }

    public struct Room
    {
        public int crew;
        public string type; // TODO have an enum?

        public Room(string t)
        {
            crew = 0;
            type = t;
        }


    }

}
