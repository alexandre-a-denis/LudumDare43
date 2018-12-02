using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaReport
{
	private readonly RoomType roomType;
	private readonly bool hasRoomBeenDestroyed;
	private readonly int resourceQtyLoss;
	private readonly int crewQtyLoss;
	private readonly int crewQtySacrified;

	public DramaReport(Room room, int resourceQtyLoss, int crewQtyLoss, int crewQtySacrified)
	{
		this.roomType = room.roomType;
		this.hasRoomBeenDestroyed = room.roomStatus == RoomStatus.DESTROYED;
		this.resourceQtyLoss = resourceQtyLoss;
		this.crewQtyLoss = crewQtyLoss;
		this.crewQtySacrified = crewQtySacrified;
	}

	public RoomType RoomType { get {return this.roomType;} }
	public bool HasRoomBeenDestroyed { get {return this.hasRoomBeenDestroyed;} }
	public int ResourceQtyLoss { get {return this.resourceQtyLoss;} }
	public int CrewQtyLoss { get {return this.crewQtyLoss;} }	
	public int CrewQtySacrified { get {return this.crewQtySacrified;} }

	public override string ToString()
	{
		if (this.hasRoomBeenDestroyed)
			return string.Format("{0} of {1} lost | {2} crew members lost ({3} of em has been sacrified)", this.resourceQtyLoss, this.roomType, this.crewQtyLoss, this.crewQtySacrified);

		return string.Format("Room has been saved; but {0} has been sacrified for it", this.crewQtySacrified);
	}
}
