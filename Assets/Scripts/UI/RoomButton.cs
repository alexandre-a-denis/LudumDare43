using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomButton : RoomUIScript, IPointerDownHandler, IPointerUpHandler
{
    public int increment = 1;
    readonly float incrementAfterSecs = 0.1f;

    bool pressed = false;
    float mouseDownTime = 0;
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
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
            if (mouseDownTime > incrementAfterSecs)
            {
                mouseDownTime = 0;
                room.AddCrew(increment);
            }
        }
    }
}
