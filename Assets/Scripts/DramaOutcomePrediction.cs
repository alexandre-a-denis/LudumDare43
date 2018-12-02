using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class DramaOutcomeSample
{
	private readonly int resourceQty;
	private readonly int crewQty;

	private readonly bool willRoomBeDestroyed;
	private readonly int resourceQtyLoss;
	private readonly int crewQtyLoss;

	public DramaOutcomeSample(int resourceQty, int crewQty, bool willRoomBeDestroyed, int resourceQtyLoss, int crewQtyLoss)
	{
		this.resourceQty = resourceQty;
		this.crewQty = crewQty;

		this.willRoomBeDestroyed = willRoomBeDestroyed;
		this.resourceQtyLoss = resourceQtyLoss;
		this.crewQtyLoss = crewQtyLoss;
	}

	public int ResourceQty { get {return this.resourceQty;} }
	public int CrewQty { get {return this.crewQty;} }

	public bool WillRoomBeDestroyed { get {return this.willRoomBeDestroyed;} }
	public int ResourceQtyLoss { get {return this.resourceQtyLoss;} }
	public int CrewQtyLoss { get {return this.crewQtyLoss;} }	
}

public class DramaOutcomePrediction
{
	private const int MAX_SAMPLES = 100;

	private readonly Drama drama;
	private readonly List<DramaOutcomeSample> samples;

	public DramaOutcomePrediction(Drama drama, int escapeCrewQty, float currentHope)
	{
		this.drama = drama;
		this.samples = Enumerable.Range(0, MAX_SAMPLES).Select(sampleIndex => GenerateSample(escapeCrewQty, currentHope)).ToList();
	}

	private DramaOutcomeSample GenerateSample(int escapeCrewQty, float currentHope)
	{
		int resourceQty = this.drama.Room.resourcesNb;
		int crewQty = this.drama.Room.numberOfCrew;

		int sacrificingCrewQty = crewQty - escapeCrewQty;
		float sacrificingCrewFactor = (float)System.Math.Sqrt(sacrificingCrewQty);
		bool willRoomBeDestroyed = (Random.Range(0.0f, 1.0f) * sacrificingCrewFactor * currentHope) < 1.0f;

		return new DramaOutcomeSample(resourceQty, crewQty, willRoomBeDestroyed, willRoomBeDestroyed ? resourceQty : 0, sacrificingCrewQty);
	}

	public Drama Drama { get {return this.drama;} }

	public DramaOutcomeSample PickOneSample()
	{
		return this.samples[Random.Range(0, this.samples.Count)];
	}

	public float EvaluateProbabilityToLooseRoom()
	{
		return this.samples.Count(sample => sample.WillRoomBeDestroyed) / (float)this.samples[option].Count();
	}

	public float EvaluateProbabilityToLooseResources()
	{
		return this.samples.Count(sample => sample.ResourceQtyLoss > 0) / (float)this.samples[option].Count();
	}

	public float EvaluateProbabilityToLooseCrew()
	{
		return this.samples.Count(sample => sample.CrewQtyLoss > 0) / (float)this.samples[option].Count();
	}

	public override string ToString()
	{
		return string.Format("{0}% to loose room | {1}% to loose resources | {2}% to loose crew", EvaluateProbabilityToLooseRoom() * 100, EvaluateProbabilityToLooseResources() * 100, EvaluateProbabilityToLooseCrew() * 100);
	}
}
