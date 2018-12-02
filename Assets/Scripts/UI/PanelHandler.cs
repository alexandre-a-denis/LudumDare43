using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// handles panel switching
public class PanelHandler : MonoBehaviour
{
    public GameObject statusPanel;
    public GameObject eventPanel;

    public GameObject introPanel;
    public GameObject endPanel;

    public GameObject dramaChoicePanel;
    public GameObject dramaOutcomePanel;
    public GameObject dramaMovePanel;
   
    public void ShowIntroPanel()
    {
        eventPanel.SetActive(false);
        statusPanel.SetActive(false);
        endPanel.SetActive(false);

        introPanel.SetActive(true);
    }


    public void ShowChoicePanel()
    {
        introPanel.SetActive(false);

        dramaOutcomePanel.SetActive(false);
        dramaMovePanel.SetActive(false);

        eventPanel.SetActive(true);
        statusPanel.SetActive(true);
        dramaChoicePanel.SetActive(true);
    }


    public void ShowEndPanel(bool won, string endMessage)
    {
        dramaOutcomePanel.SetActive(false);
        dramaMovePanel.SetActive(false);
        dramaChoicePanel.SetActive(false);

        endPanel.SetActive(true);

        EndPanel endPanelScript = endPanel.GetComponent<EndPanel>();
        endPanelScript.SetCauseText(endMessage);
        endPanelScript.SetStatusText(won ? "SUCCESS !!!" : "Game Over....");
    }


    public void ShowOutcomePanel()
    {
        dramaOutcomePanel.SetActive(true);
        dramaMovePanel.SetActive(false);
        dramaChoicePanel.SetActive(false);
    }


    public void ShowMovePanel()
    {
        dramaOutcomePanel.SetActive(false);
        dramaMovePanel.SetActive(true);
        dramaChoicePanel.SetActive(false);
    }

}
