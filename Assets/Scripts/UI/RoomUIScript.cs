using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script retrieves a room instance in parent
public class RoomUIScript : MonoBehaviour
{
    private Room roomPrivate;

    protected Room room
    {
        get {
            if (roomPrivate == null)
                roomPrivate = GetComponentInParent<Room>();
            return roomPrivate;
        }
    }
}
