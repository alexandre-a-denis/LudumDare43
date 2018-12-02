using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// next turn button only interactable in move phase
public class NextTurnButton : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        button.interactable = GameManager.manager.CurrentPhase == TurnPhases.MOVE;
    }
}
