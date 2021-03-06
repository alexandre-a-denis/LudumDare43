﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum DramaType
{
	Fire,
	HullBreach
}

public class Drama
{
	private readonly DramaType type;
	private readonly Room room;

	public static Drama CreateRandomOne(List<Room> rooms)
	{
		List<DramaType> dramaTypes = System.Enum.GetValues(typeof(DramaType)).Cast<DramaType>().ToList();
		DramaType selectedDramaType = dramaTypes[Random.Range(0, dramaTypes.Count())];

		List<Room> validRooms = rooms.Where(room => room.roomType != RoomType.COMMON && room.roomStatus != RoomStatus.DESTROYED).ToList();
		Room selectedRoom = validRooms[Random.Range(0, validRooms.Count())];

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
		return string.Format("{0} in {1}: {2} {3} could be lost!", this.type, this.room.roomName, this.room.resourcesNb, this.room.roomType);
	}
}
