using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTypePanel : RoomUIScript
{
    public Sprite relicsSprite;
    public Sprite foodSprite;
    public Sprite restSprite;

    Text text;
    Image image;

    // Use this for initialization
    void Start()
    {
        text = GetComponentInChildren<Text>();

        // sets image only once
        image = GetComponentInChildren<Image>();
        switch(room.roomType)
        {
            case RoomType.FOOD:
                image.sprite = foodSprite;
                break;
            case RoomType.RELICS:
                image.sprite = relicsSprite;
                break;
            case RoomType.COMMON:
                image.sprite = restSprite;
                break;
        }
    }

    // sets text
    void Update()
    {
        text.text = room.roomType.ToString();
        if (room.roomType!=RoomType.COMMON)
            text.text += " " + room.resourcesNb;
    }
}
