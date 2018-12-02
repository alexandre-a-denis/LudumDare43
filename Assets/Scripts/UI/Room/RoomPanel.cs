using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// overall RoomPanel, disable room if not operational
public class RoomPanel : RoomUIScript
{
    bool deactivated;
    Image roomBackground;

    void Start()
    {
        roomBackground = GetComponent<Image>();
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
    }
}
