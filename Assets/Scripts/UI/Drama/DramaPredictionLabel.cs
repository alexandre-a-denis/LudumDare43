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


    public void OnSaveBothHover()
    {
        if (GameManager.manager.CurrentPhase == TurnPhases.DRAMA)
            SetText(DramaSolvingOption.TryToSaveBoth);
    }

    public void OnSaveRoomHover()
    {
        if (GameManager.manager.CurrentPhase == TurnPhases.DRAMA)
            SetText(DramaSolvingOption.SaveRoom);
    }

    public void OnSaveCrewHover()
    {
        if (GameManager.manager.CurrentPhase == TurnPhases.DRAMA)
            SetText(DramaSolvingOption.SaveCrew);
    }

    public void OnMouseExit()
    {
        text.text = "";
    }


    void SetText(DramaSolvingOption option)
    {
        float saveCrew = 1-GameManager.manager.CurrentDramaOutcomePrediction.EvaluateProbabilityToLooseCrew(option);
        float saveRoom = 1-GameManager.manager.CurrentDramaOutcomePrediction.EvaluateProbabilityToLooseResources(option);

        string saveCrewStr = (int)(100 * saveCrew)+"%";
        string saveRoomStr = (int)(100 * saveRoom) + "%";

        text.text = "Save all crew: " + saveCrewStr + " Save room: " + saveRoomStr;
    }

}
