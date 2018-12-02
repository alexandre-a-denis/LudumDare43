using System.Collections;
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
                float probaToLose = GameManager.manager.CurrentDramaOutcomePrediction.EvaluateProbabilityToLooseRoom();
                string probaToLoseStr = (int)(100 * probaToLose) + "%";
                text.text = "Proba to lose room: " + probaToLoseStr;
            }
        }
    }
}
