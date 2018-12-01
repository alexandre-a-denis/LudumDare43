using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container for "global" states
public class WorldState : MonoBehaviour
{

    // Number of turns before game ends.
    private int turnCountLimit = 20;

    // Current turn count.
    public int turnCount = 1;

    // True if game is ended (won or lost). Can happend if turnCount = turnCountLimit or if player looses.
    public bool end = false;

    // Defines the number of human in the ship.
    int crewSize;

    // Number of food units (each crew member uses 1 food per turn). If no food is left, crew will starve/die.
    int foodAmount;

    // Level of hope. Hope is not "consumed" like food is. It will affect capacity of crew during events.
    int hopeLevel;

    // Increase turn count. Perform "end of turn"
    public void NextTurn()
    {
        // Consume food (1 unit per crew)
        foodAmount -= crewSize;
        if (foodAmount < 0)
        {
            // No more food, you loose
            end = true;
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
                Debug.Log("Reached max turn");
            }
        }
    }



}
