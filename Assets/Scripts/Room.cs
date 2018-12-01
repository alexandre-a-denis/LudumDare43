using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { FOOD, RELICS, COMMON }

public enum RoomStatus { OPERATIONAL, DESTROYED }

// A Room
public class Room : MonoBehaviour
{
    public WorldState worldState;

    public int id; // internal unique id, not for display I guess
    public string roomName; // display name for the user to know which room we are talking about
    public int numberOfCrew = 0;
    public int resourcesNb = 0;
    public RoomType roomType = RoomType.FOOD;
    public RoomStatus roomStatus = RoomStatus.OPERATIONAL;

    public void Destroy()
    {
        Debug.Log("Destroying room " + id);
        roomStatus = RoomStatus.DESTROYED;
        numberOfCrew = 0;
        resourcesNb = 0;
    }

    public void KillSome(int nb)
    {
        Debug.Log("Killing " + nb + " crew in room " + id);
        numberOfCrew -= nb;
    }

    public void AddCrew(int number)
    {
        worldState.AddCrewToRoom(this, number);
    }

    public override string ToString()
    {
        return string.Format("Room [id={0}, name={1}, crew={2}, resource={3}, type={4}, status={5}]", this.id, this.roomName, this.numberOfCrew, this.resourcesNb, this.roomType, this.roomStatus);
    }

}
