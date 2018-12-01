using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomTest : MonoBehaviour
{
    public Room roomPrefab;
    public GameObject canvas;

	void Start () {
        Room wreckClone = (Room) Instantiate(roomPrefab, Vector3.zero, Quaternion.identity) as Room;
        wreckClone.transform.SetParent(canvas.transform);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
