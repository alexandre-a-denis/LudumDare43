using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaSlider : MonoBehaviour
{
    Text text;
    Slider slider;
    int oldSacrifiedValue = -1;

    // Use this for initialization
    void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.CurrentDrama != null)
        {
            Room room = GameManager.manager.CurrentDrama.Room;
            int sacrified = (int)(slider.value * room.numberOfCrew);
            text.text = sacrified + "/" + room.numberOfCrew + " Crew sacrified";

            if (sacrified != oldSacrifiedValue)
            {
                GameManager.manager.UpdatePrediction(room.numberOfCrew-sacrified);
                oldSacrifiedValue = sacrified;
            }
        }

    }
}
