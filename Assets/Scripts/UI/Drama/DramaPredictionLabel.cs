﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaPredictionLabel : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = "";
    }

    void Update()
    {
        if (GameManager.manager.CurrentPhase == TurnPhases.DRAMA)
        {
            if (GameManager.manager.CurrentDramaOutcomePrediction != null)
            {
                float probaToSave = GameManager.manager.CurrentDramaOutcomePrediction.EvaluateTotalProbabilityToSaveRoom();
                string probaToSaveStr = (int)(100 * probaToSave) + "%";
                text.text = "Saving the room => " + probaToSaveStr;
            }
        }
    }
}
