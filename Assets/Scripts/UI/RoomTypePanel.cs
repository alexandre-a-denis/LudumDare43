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
        image = GetComponentInChildren<Image>();

        switch(room.roomType)
        {
            case RoomType.FOOD:
                image.sprite = foodSprite;
                break;
            case RoomType.RELICS:
                image.sprite = relicsSprite;
                break;
            case RoomType.REST:
                image.sprite = restSprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = room.roomType.ToString();
    }
}
