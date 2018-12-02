using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum TurnPhases { INTRO, DRAMA, DRAMA_OUTCOME, MOVE }

// overall GameManager, game init, handles turn structure 
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public static GameManager manager { get { if (singleton == null) singleton = FindObjectOfType<GameManager>(); return singleton; } }

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

    // drama panel to handle display
    PanelHandler panelHandler;
    
    void Awake()
    {
        worldState = FindObjectOfType<WorldState>();
        panelHandler = FindObjectOfType<PanelHandler>();
    }

    void Start()
    {
        Init();
    }

    // initializes the game
    public void Init()
    {
        CurrentPhase = TurnPhases.INTRO;
        panelHandler.ShowIntroPanel();
    }


    // starts the game, after the intro, triggered by button
    public void StartGame()
    {
        // should reset worldState as well
        End = false;
        CurrentTurn = 1;
        worldState.Init();
        StartTurn();
    }


    void EndGame(bool won, string cause)
    {
        worldState.DestroyRooms();
        panelHandler.ShowEndPanel(won, cause);
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
            EndGame(false, "No more food, you loose !!!!!!!!!!!!!!");
            return;
        }

        // Check if some crew remains
        if (worldState.CurrentCrew() <= 0)
        {
            End = true;
            Debug.Log("No more crew, you loose !!!!!!!!!!!!");
            EndGame(false, "No more crew, you loose !!!!!!!!!!!!");
            return;
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
                EndGame(true, "Reached max turn, you win !!!!!!!!!!!!");
                return;
            }
        }
    }
    #endregion TURN REGION
    
    /////////////////////// DRAMA REGION
    #region DRAMA REGION

    // starts random drama, 
    public void StartDrama()
    {
        // drama and outcome generation (accessed through ui)
        CurrentDrama = Drama.CreateRandomOne(worldState.GetRooms());
        Debug.Log(CurrentDrama);

        if (CurrentDrama.Room.numberOfCrew == 0)
        {
            AutomaticResolution();
            return;
        }

        // only sets phase after all drama stuff has been made
        panelHandler.ShowChoicePanel();
        CurrentPhase = TurnPhases.DRAMA;
    }

    // ends the drama, displays outcome
    void EndDrama()
    {
        CurrentPhase = TurnPhases.DRAMA_OUTCOME;
        panelHandler.ShowOutcomePanel();
    }

    // updates prediction upon slider change
    public void UpdatePrediction(int saved)
    {
        CurrentDramaOutcomePrediction = new DramaOutcomePrediction(CurrentDrama, saved, worldState.CurrentHope());
        Debug.Log(CurrentDramaOutcomePrediction);
    }

    // Evacuate Now ! (triggered by button)
    public void Evacuate()
    {
        CurrentDramaReport = DramaSolver.Apply(CurrentDramaOutcomePrediction);
        EndDrama();
    }


    // automatic drama resolution
    void AutomaticResolution()
    {
        Debug.Log("Drama in a room with 0 crew members !! Auto-resolution !");
        UpdatePrediction(0);
        CurrentDramaReport = DramaSolver.Apply(CurrentDramaOutcomePrediction);
        EndDrama();
    }


    #endregion DRAMA REGION

    /////////////////////// MOVE REGION
    #region MOVE REGION

    // starts move (triggered by button)
    public void StartMove()
    {
        panelHandler.ShowMovePanel();
        CurrentPhase = TurnPhases.MOVE;
    }

    // end move triggered when hitting NextTurn Button
    public void EndMove()
    {
        EndTurn();
        if (!End)
           StartDrama(); // only if not gameover
    }

    #endregion MOVE REGION
}
