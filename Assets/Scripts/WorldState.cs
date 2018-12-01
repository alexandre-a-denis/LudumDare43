using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container for "global" states
public class WorldState : MonoBehaviour
{

    // Number of turns before game ends.
    int turnCountLimit = 20;

    // Current turn count.
    public int turnCount = 1;

    // Defines the number of human in the ship.
    int crewSize;

    // Number of food units (each crew member uses 1 food per turn). If no food is left, crew will starve/die.
    int foodAmount;

    // Level of hope. Hope is not "consumed" like food is. It will affect capacity of crew during events.
    int hopeLevel;

    // Increase turn count. Perform "end of turn"
    public void NextTurn()
    {
        turnCount += 1;
        if (turnCount < turnCountLimit)
            Debug.Log("TurnCount increased to " + turnCount);
        else
            Debug.Log("Reached max turn");
    }


}
