﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum TurnPhases { DRAMA, MOVE }

// overall GameManager, game init, handles turn structure 
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public static GameManager manager { get { if (singleton==null) singleton = FindObjectOfType<GameManager>(); return singleton; } }

    private WorldState worldState;

    // ====================== TURNS ====================== 

    // Number of turns before game ends.
    private int TurnCountLimit = 20;

    // Current game state
    public int CurrentTurn = 1;
    public TurnPhases CurrentPhase = TurnPhases.DRAMA;

    // Global drama
    public Drama CurrentDrama;
    public DramaReport CurrentDramaReport;
    public DramaOutcomePrediction CurrentDramaOutcomePrediction;
    
    // True if game is ended (won or lost). Can happend if turnCount = turnCountLimit or if player looses.
    public bool End = false;


    void Awake()
    {
       worldState = FindObjectOfType<WorldState>();
    }

   
    void Start()
    {
        Init();
    }

    
    void Update()
    {
        // in drama phase, disable next turn, disable
    }


    // setup initial state, should also be triggered on Reset
    public void Init()
    {
        // should reset worldState as well
        CurrentTurn = 1;
        StartDrama();
    }



    /////////////////////// TURN REGION
    #region TURN REGION

    // starts turn
    void StartTurn()
    {
        StartDrama();
    }

   
    // updates resources at the end of turn
    void EndTurn()
    {
        // Consume food (1 unit per crew)
        worldState.ConsumeFood(worldState.CurrentCrew());
        if (worldState.CurrentFood() <= 0)
        {
            End = true;
            Debug.Log("No more food, you loose !!!!!!!!!!!!!!");
        }

        // Check if some crew remains
        if (worldState.CurrentCrew() <= 0)
        {
            End = true;
            Debug.Log("No more crew, you loose !!!!!!!!!!!!");
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
                Debug.Log("Reached max turn, you win !!!!!!!!!!!!");
            }
        }
    }

    #endregion TURN REGION



    /////////////////////// DRAMA REGION
    #region DRAMA REGION

    // starts random drama, 
    void StartDrama()
    {
        CurrentPhase = TurnPhases.DRAMA;

        // drama and outcome generation (accessed through ui)
        CurrentDrama = Drama.CreateRandomOne(worldState.GetRooms());
        CurrentDramaOutcomePrediction = new DramaOutcomePrediction(CurrentDrama);
        Enumerable.Range(0, 100).ToList().ForEach(index => CurrentDramaOutcomePrediction.GenerateSample(worldState.CurrentHope()));

        Debug.Log(CurrentDrama);
        Debug.Log(CurrentDramaOutcomePrediction);
    }

    // ends the drama, starts the move phase
    void EndDrama()
    {
        StartMove();
    }

    // do action
    void DoAction(DramaSolvingOption option)
    {
        CurrentDramaReport = DramaSolver.Apply(CurrentDramaOutcomePrediction, option);
        EndDrama();
    }

    // button triggered
    public void OnTrySaveBoth()
    {
        DoAction(DramaSolvingOption.TryToSaveBoth);
    }

    // button triggered
    public void OnSaveRoom()
    {
        DoAction(DramaSolvingOption.SaveRoom);
    }

    // button triggered
    public void OnSaveCrew()
    {
        DoAction(DramaSolvingOption.SaveCrew);
    }

    #endregion DRAMA REGION



    /////////////////////// MOVE REGION
    #region MOVE REGION

    // starts
    void StartMove()
    {
        CurrentPhase = TurnPhases.MOVE;
    }

    // end move triggered when hitting NextTurn Button
    public void EndMove()
    {
        EndTurn();
        StartDrama(); // only if not gameover
    }

    #endregion MOVE REGION
}