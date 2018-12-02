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

		int survivorsQty = selectedSample.CrewQty - selectedSample.CrewQtyLoss;
		prediction.Drama.Room.AddCrew(-survivorsQty);
		prediction.Drama.Room.KillSome(selectedSample.CrewQty - survivorsQty);

		if (selectedSample.WillRoomBeDestroyed)
			prediction.Drama.Room.Destroy();

		return new DramaReport(prediction.Drama.Room, selectedSample.ResourceQtyLoss, selectedSample.CrewQtyLoss);
	}
}
