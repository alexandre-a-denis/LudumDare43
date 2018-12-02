using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DramaSolver
{
	public static DramaReport Apply(DramaOutcomePrediction prediction)
	{
		bool willRoomBeDestroyed = Random.Range(0.0f, 1.0f) < prediction.EvaluateTotalProbabilityToSaveRoom();

		int crewQtyLoss = prediction.CrewQtyToSacrifice;
		int resourceQtyLoss = 0;
		if (willRoomBeDestroyed)
		{
			crewQtyLoss = prediction.Drama.Room.numberOfCrew;
			resourceQtyLoss = prediction.Drama.Room.resourcesNb;
			prediction.Drama.Room.Destroy();
		}
		else 
		{
			prediction.Drama.Room.KillSome(prediction.CrewQtyToSacrifice);	
		}

		return new DramaReport(prediction.Drama.Room, resourceQtyLoss, crewQtyLoss, prediction.CrewQtyToSacrifice);
	}
}
