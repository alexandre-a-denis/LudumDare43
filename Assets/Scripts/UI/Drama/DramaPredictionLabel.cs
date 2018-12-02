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
        float saveCrew = GameManager.manager.CurrentDramaOutcomePrediction.EvaluateProbabilityToLooseCrew(option);
        float saveRoom = GameManager.manager.CurrentDramaOutcomePrediction.EvaluateProbabilityToLooseResources(option);

        text.text = option+" => Save crew: " + saveCrew + " Save room: " + saveRoom;
    }

}
