using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomWarningImage : RoomUIScript
{
    bool flashing;
    Image image;
    float fadeSpeed = 6.5f;
    Color transparent = new Color(0, 0, 0, 0);

    void Start()
    {
        image = GetComponent<Image>();
        image.color = transparent;
    }

    // Update is called once per frame
    void Update()
    {
        if (room.roomStatus == RoomStatus.OPERATIONAL)
        {
            if (room.roomType == RoomType.COMMON)
                return;

            if (room.numberOfCrew == 0 && !flashing)
                StartCoroutine("Flash");
            if (flashing && room.numberOfCrew > 0)
                flashing = false;
        }
        else flashing = false;

        if (!flashing)
        {
            image.color = transparent;
            StopCoroutine("Flash");
        }
    }

    IEnumerator Flash()
    {
        flashing = true;
        while (true)
        {
            image.color = Color.Lerp(Color.white, transparent, Mathf.PingPong(fadeSpeed * Time.time, 1));
            yield return null;
        }
    }
}
