using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomButton : RoomUIScript, IPointerDownHandler, IPointerUpHandler
{
    public int increment = 1;
    readonly float incrementAfterSecs = 0.1f;
    
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

        if (room.roomType == RoomType.REST)
            gameObject.SetActive(false);


    }


    bool pressed = false;
    bool continuousIncrement = false;
    float mouseDownTime = 0;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        continuousIncrement = false;
        mouseDownTime = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        mouseDownTime = 0;
    }

    void Update()
    {
        if (pressed)
        {
            mouseDownTime += Time.deltaTime;

            if (mouseDownTime > 2 * incrementAfterSecs)
                continuousIncrement = true;

            if (continuousIncrement)
            {
                if (mouseDownTime > incrementAfterSecs)
                {
                    mouseDownTime = 0;
                    room.AddCrew(increment);
                }
            }
        }
    }

    void TaskOnClick()
    {
        room.AddCrew(increment);
    }
}
