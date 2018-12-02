using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaOutcomeLabel : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

   
    void Update()
    {
        if (GameManager.manager.CurrentPhase == TurnPhases.MOVE)
        {
            if (GameManager.manager.CurrentDramaReport != null)
                text.text = GameManager.manager.CurrentDramaReport.ToString();
        }

       // Debug.Log(GameManager.manager.CurrentPhase + " " + GameManager.manager.CurrentDrama);
    }
}
