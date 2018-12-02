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


    static string IAOutcomeDescription()
    {
        string ret = "";
        DramaReport report = GameManager.manager.CurrentDramaReport;
        if (report.HasRoomBeenDestroyed)
            if (report.RoomType == RoomType.FOOD)
                ret += IAText.OnFoodLoss() + " ";
            else if (report.RoomType == RoomType.RELICS)
                ret += IAText.OnRelicLoss() + " ";
        if (report.CrewQtyLoss > 0)
            ret += IAText.OnCrewLoss();

        if (ret.Length == 0)
            ret += IAText.OnNothingLost();

        return ret;
    }
}
