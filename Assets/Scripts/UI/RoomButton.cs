using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : RoomUIScript
{
    public int increment = 1;
    Button button;

    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        room.AddCrew(increment);
        Debug.Log("Added " + increment + " to " + room.ToString());
    }
}
