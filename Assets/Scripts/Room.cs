using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { FOOD, RELICS, REST }

public enum RoomStatus { OPERATIONAL, DESTROYED }

// A Room
public class Room : MonoBehaviour
{
    public WorldState worldState;

    public int numberOfCrew=0;
    public int resourcesNb=0;
    public RoomType roomType = RoomType.FOOD;
    public RoomStatus roomStatus = RoomStatus.OPERATIONAL;

    

    public void AddCrew(int number)
    {
        numberOfCrew += number;
    }



}
