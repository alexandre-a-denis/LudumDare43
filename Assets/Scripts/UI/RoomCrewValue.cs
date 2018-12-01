using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCrewValue : MonoBehaviour
{
    Room parentRoom;
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        parentRoom = GetComponentInParent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = parentRoom.numberOfCrew.ToString();
    }
}
