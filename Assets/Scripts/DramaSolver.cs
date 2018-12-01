using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum DramaSolvingOption
{
	SaveRoom,
	SaveCrew,
	TryToSaveBoth
}

public static class DramaSolver
{
	private static DramaReport SaveRoom(Drama drama)
	{
		int oldResourceQty = drama.Room.resourcesNb;
		int oldCrewQty = drama.Room.numberOfCrew;

		drama.Room.KillSome(oldCrewQty);

		return new DramaReport(drama.Room, 0, oldCrewQty);
	}

	private static DramaReport SaveCrew(Drama drama)
	{
		int oldResourceQty = drama.Room.resourcesNb;
		int oldCrewQty = drama.Room.numberOfCrew;

		drama.Room.AddCrew(-oldCrewQty);
		drama.Room.Destroy();

		return new DramaReport(drama.Room, oldResourceQty, 0);
	}

	private static DramaReport TryToSaveBoth(Drama drama, float currentHope)
	{
		int oldResourceQty = drama.Room.resourcesNb;
		int oldCrewQty = drama.Room.numberOfCrew;

		int maybeSurvivorQty = Enumerable.Range(1, oldCrewQty).Select(index => Random.Range(0.0f, 1.0f)).Count(value => value > 0.1f);
		bool willRoomBeDestroyed = Random.Range((float)(System.Math.Sqrt(maybeSurvivorQty) * 0.1f), 1.0f) > (1.0f - currentHope);

		if (willRoomBeDestroyed)
		{
			drama.Room.Destroy();
			return new DramaReport(drama.Room, oldResourceQty, oldCrewQty);	
		}
		
		int deadCrewQty = oldCrewQty - maybeSurvivorQty;
		drama.Room.KillSome(deadCrewQty);
		return new DramaReport(drama.Room, 0, deadCrewQty);
	}

	public static DramaReport Process(Drama drama, DramaSolvingOption option, float currentHope)
	{
		switch (option)
		{
			case DramaSolvingOption.SaveRoom: return SaveRoom(drama);
			case DramaSolvingOption.SaveCrew: return SaveCrew(drama);
			case DramaSolvingOption.TryToSaveBoth: return TryToSaveBoth(drama, currentHope);
			default: throw new System.Exception("Option not implemented");
		}
	}
}
