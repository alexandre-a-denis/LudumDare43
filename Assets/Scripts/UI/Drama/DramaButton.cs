using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaButton : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        button.interactable = GameManager.manager.CurrentPhase == TurnPhases.DRAMA;
    }
}
