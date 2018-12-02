using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaReport
{
	private readonly RoomType roomType;
	private readonly bool hasRoomBeenDestroyed;
	private readonly int resourceQtyLoss;
	private readonly int crewQtyLoss;

	public DramaReport(Room room, int resourceQtyLoss, int crewQtyLoss)
	{
		this.roomType = room.roomType;
		this.hasRoomBeenDestroyed = room.roomStatus == RoomStatus.DESTROYED;
		this.resourceQtyLoss = resourceQtyLoss;
		this.crewQtyLoss = crewQtyLoss;
	}

	public RoomType RoomType { get {return this.roomType;} }
	public bool HasRoomBeenDestroyed { get {return this.hasRoomBeenDestroyed;} }
	public int ResourceQtyLoss { get {return this.resourceQtyLoss;} }
	public int CrewQtyLoss { get {return this.crewQtyLoss;} }	

	public override string ToString()
	{
		if (this.hasRoomBeenDestroyed)
			return string.Format("{0} of {1} lost | {2} crew members lost", this.resourceQtyLoss, this.roomType, this.crewQtyLoss);

		return string.Format("{0} crew members lost", this.crewQtyLoss);
	}
}
