using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaSlider : MonoBehaviour
{
    Text text;
    Slider slider;
    int oldSavedValue = -1;

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
            int saved = (int)(slider.value * room.numberOfCrew);
            text.text = saved + "/" + room.numberOfCrew + " Crew saved";

            if (saved != oldSavedValue)
            {
                GameManager.manager.UpdatePrediction(saved);
                oldSavedValue = saved;
            }
        }

    }
}
