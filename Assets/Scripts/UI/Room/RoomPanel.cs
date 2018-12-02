using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// overall RoomPanel, disable room if not operational
public class RoomPanel : RoomUIScript
{
    bool deactivated;
    Image roomBackground;

    // alert handling
    bool alertStarted;
    float fadeSpeed = 3.5f;
    Color color; // room background initial color

    void Start()
    {
        roomBackground = GetComponent<Image>();
        color = roomBackground.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (room.roomStatus == RoomStatus.DESTROYED)
        {
            if (!deactivated)
            {
                deactivated = true;
                roomBackground.color = Color.red;
         
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
            }
        }

        if (GameManager.manager.CurrentPhase == TurnPhases.DRAMA)
        {
            if (GameManager.manager.CurrentDrama.Room == room)
            {
                if (!alertStarted)
                {
                    alertStarted = true;
                    StartCoroutine("Fade");
                }
            }
            else
            {
                alertStarted = false;
                StopCoroutine("Fade");
            }
        }
        else
        {
            alertStarted = false;
            StopCoroutine("Fade");
            roomBackground.color = deactivated ? Color.red : color;
        }
    }


    IEnumerator Fade()
    {
        while (true)
        {
           roomBackground.color = Color.Lerp(color, Color.red, Mathf.PingPong(fadeSpeed*Time.time, 1));
            yield return null;
        }
    }

}
