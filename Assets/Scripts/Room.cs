using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { FOOD, RELICS, REST }

public enum RoomStatus { OPERATIONAL, DESTROYED }

// A Room
public class Room : MonoBehaviour
{

    public int numberOfCrew;
    public int resourcesNb;
    public RoomType roomType;
    public RoomStatus roomStatus;

    

    public void AddCrew(int number)
    {
        numberOfCrew += number;
    }



}
