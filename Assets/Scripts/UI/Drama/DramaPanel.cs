using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// activates one or the other of subpanels
public class DramaPanel : MonoBehaviour
{
    public GameObject dramaChoicePanel;
    public GameObject dramaOutcomePanel;

    // Update is called once per frame
    void Update()
    {
        dramaChoicePanel.SetActive(GameManager.manager.CurrentPhase == TurnPhases.DRAMA);
        dramaOutcomePanel.SetActive(GameManager.manager.CurrentPhase == TurnPhases.MOVE);
    }
}
