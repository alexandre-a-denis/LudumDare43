using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutFillTest : MonoBehaviour
{
    public Room roomPrefab;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Room room = (Room)Instantiate(roomPrefab) as Room;
            room.transform.SetParent(transform, false);
        }

    }
}

