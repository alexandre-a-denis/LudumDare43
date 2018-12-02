using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// handles panel switching
public class EventPanel : MonoBehaviour
{
    public GameObject dramaChoicePanel;
    public GameObject dramaOutcomePanel;
    public GameObject dramaMovePanel;


    public void ShowChoicePanel()
    {
        dramaOutcomePanel.SetActive(false);
        dramaMovePanel.SetActive(false);
        dramaChoicePanel.SetActive(true);
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
