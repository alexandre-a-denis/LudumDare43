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
	public static DramaReport Apply(DramaOutcomePrediction prediction, DramaSolvingOption option)
	{
		DramaOutcomeSample selectedSample = prediction.PickOneSample(option);

		if (selectedSample.WillRoomBeDestroyed)
		{
			int survivorsQty = selectedSample.CrewQty - selectedSample.CrewQtyLoss;
			prediction.Drama.Room.AddCrew(-survivorsQty);
			prediction.Drama.Room.Destroy();
		}
		else 
		{
			prediction.Drama.Room.KillSome(selectedSample.CrewQtyLoss);	
		}

		return new DramaReport(prediction.Drama.Room, selectedSample.ResourceQtyLoss, selectedSample.CrewQtyLoss);
	}
}
