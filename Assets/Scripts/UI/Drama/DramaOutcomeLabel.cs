using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// displays the outcome of a drama
public class DramaOutcomeLabel : MonoBehaviour
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
            text.text = "";

        if (GameManager.manager.CurrentPhase == TurnPhases.MOVE)
            if (GameManager.manager.CurrentDramaReport != null && text.text.Length == 0)
                text.text = GameManager.manager.CurrentDramaReport.ToString() + "\n" + IAOutcomeDescription();
    }


    private void OnEnable()
    {
        if (text!=null)
            text.text = "";
    }


    static string IAOutcomeDescription()
    {
        return IAText.CommentOnDramaOutcome(GameManager.manager.CurrentDramaReport);
    }
}
