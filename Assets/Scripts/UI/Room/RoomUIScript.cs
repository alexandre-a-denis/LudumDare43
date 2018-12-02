using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script retrieves a room instance in parent, it conveniently access to manager
public class RoomUIScript : MonoBehaviour
{
    private Room roomPrivate;
    private GameManager managerPrivate;

    protected Room room
    {
        get {
            if (roomPrivate == null)
                roomPrivate = GetComponentInParent<Room>();
            return roomPrivate;
        }
    }

    protected GameManager manager
    {
        get
        {
            if (managerPrivate == null)
                managerPrivate = FindObjectOfType<GameManager>();
            return managerPrivate;
        }
    }
}
