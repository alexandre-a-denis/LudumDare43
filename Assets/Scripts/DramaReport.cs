using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaReport
{
	private readonly RoomType roomType;
	private readonly bool hasRoomBeenDestroyed;
	private readonly int resourceQtyLost;
	private readonly int crewQtyLost;

	public DramaReport(Room room, int resourceQtyLost, int crewQtyLost)
	{
		this.roomType = room.roomType;
		this.hasRoomBeenDestroyed = room.roomStatus == RoomStatus.DESTROYED;
		this.resourceQtyLost = resourceQtyLost;
		this.crewQtyLost = crewQtyLost;
	}

	public RoomType RoomType { get {return this.roomType;} }
	public bool HasRoomBeenDestroyed { get {return this.hasRoomBeenDestroyed;} }
	public int ResourceQtyLost { get {return this.resourceQtyLost;} }
	public int CrewQtyLost { get {return this.crewQtyLost;} }	

	public override string ToString()
	{
		if (this.hasRoomBeenDestroyed)
			return string.Format("{0} of {1} lost | {2} crew members lost", this.resourceQtyLost, this.roomType, this.crewQtyLost);

		return string.Format("{0} crew members lost", this.crewQtyLost);
	}
}
