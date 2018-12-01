using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum DramaType
{
	fire,
	hullBreach
}

public class Drama
{
	private readonly DramaType type;
	private readonly Room room;

	public static Drama CreateRandomOne(List<Room> rooms)
	{
		List<DramaType> dramaTypes = System.Enum.GetValues(typeof(DramaType)).Cast<DramaType>().ToList();
		DramaType selectedDramaType = dramaTypes[Random.Range(0, dramaTypes.Count() -1)];
		Room selectedRoom = rooms[Random.Range(0, rooms.Count() -1)];

		return new Drama(selectedDramaType, selectedRoom);
	}

	public Drama(DramaType type, Room room)
	{
		this.type = type;
		this.room = room;
	}

	public DramaType Type { get {return this.type;} }
	public Room Room { get {return this.room;} }

	public override string ToString()
	{
		return string.Format("{0} in an unamed room: {1} of {2} could be lost", this.type, this.room.resourcesNb, this.room.roomType);
	}
}
