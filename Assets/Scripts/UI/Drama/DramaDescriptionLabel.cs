using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaDescriptionLabel : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

   
    void Update()
    {
        if (GameManager.manager.CurrentPhase == TurnPhases.DRAMA && 
            GameManager.manager.CurrentDrama!=null)
        {
            text.text = GameManager.manager.CurrentDrama.ToString();
        }

        Debug.Log(GameManager.manager.CurrentPhase+" "+GameManager.manager.CurrentDrama);
    }
}
