using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class DramaOutcomePrediction
{	
	private readonly Drama drama;
	private readonly int crewQtyToSacrifice;
	private readonly int initalShipCrewQty;

	public DramaOutcomePrediction(Drama drama, int crewQtyToSacrifice, float currentHope, int initalShipCrewQty)
	{
		// Fuck Hope, we do not need THAT !
		this.drama = drama;
		this.crewQtyToSacrifice = crewQtyToSacrifice;
		this.initalShipCrewQty = initalShipCrewQty;
	}

	public Drama Drama { get {return this.drama;} }
	public int CrewQtyToSacrifice { get {return this.crewQtyToSacrifice;} }

	// Normalized between [0.0f, 1.0f]
	public float EvaluateBaseProbabilityToSaveRoom()
	{
		//sqrt(10 000) = 100
		int crewQty = this.drama.Room.numberOfCrew;
		float crewUnitFactor = 10000 / this.initalShipCrewQty;
		return (float)System.Math.Sqrt(crewQty * crewUnitFactor) / 100.0f;
	}

	// Normalized between [0.0f, 1.0f]
	public float EvaluateTotalProbabilityToSaveRoom()
	{
		int crewQty = this.drama.Room.numberOfCrew;
		float sacrificeCrewBonus = System.Math.Min(crewQty, this.crewQtyToSacrifice) * 0.1f; 
		return System.Math.Min(EvaluateBaseProbabilityToSaveRoom() + sacrificeCrewBonus, 1.0f);
	}

	public override string ToString()
	{
		return string.Format("Chance to save Room = {0}% + {1} crew members sacrified = {2}%)", EvaluateBaseProbabilityToSaveRoom() * 100, this.crewQtyToSacrifice, EvaluateTotalProbabilityToSaveRoom() * 100);
	}
}
