using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class DramaOutcomeSample
{
	private readonly int resourceQty;
	private readonly int crewQty;

	private readonly int resourceQtyLoss;
	private readonly int crewQtyLoss;

	public DramaOutcomeSample(int resourceQty, int crewQty, int resourceQtyLoss, int crewQtyLoss)
	{
		this.resourceQty = resourceQty;
		this.crewQty = crewQty;

		this.resourceQtyLoss = resourceQtyLoss;
		this.crewQtyLoss = crewQtyLoss;
	}

	public int ResourceQty { get {return this.resourceQty;} }
	public int CrewQty { get {return this.crewQty;} }

	public int ResourceQtyLoss { get {return this.resourceQtyLoss;} }
	public int CrewQtyLoss { get {return this.crewQtyLoss;} }	
}

public class DramaOutcomePrediction
{
	private const int MAX_SAMPLES = 100;

	private readonly Drama drama;

	private readonly Dictionary<DramaSolvingOption, List<DramaOutcomeSample>> sampleMap = new Dictionary<DramaSolvingOption, List<DramaOutcomeSample>> {
		{ DramaSolvingOption.SaveRoom, new List<DramaOutcomeSample>() },
		{ DramaSolvingOption.SaveCrew, new List<DramaOutcomeSample>() },
		{ DramaSolvingOption.TryToSaveBoth, new List<DramaOutcomeSample>() },		
	};

	public DramaOutcomePrediction(Drama drama)
	{
		this.drama = drama;
	}

	private void RegisterSample(DramaSolvingOption option, DramaOutcomeSample sample)
	{
		this.sampleMap[option].Add(sample);
		if (this.sampleMap[option].Count > MAX_SAMPLES)
			this.sampleMap[option].RemoveAt(0);
	}

	private void GenerateSaveRoomSample()
	{
		DramaOutcomeSample newSample = new DramaOutcomeSample(this.drama.Room.resourcesNb, this.drama.Room.numberOfCrew, 0, this.drama.Room.numberOfCrew);
		RegisterSample(DramaSolvingOption.SaveRoom, newSample);
	}

	private void GenerateSaveCrewSample()
	{
		DramaOutcomeSample newSample = new DramaOutcomeSample(this.drama.Room.resourcesNb, this.drama.Room.numberOfCrew, this.drama.Room.resourcesNb, 0);
		RegisterSample(DramaSolvingOption.SaveCrew, newSample);
	}

	private void GenerateTryToSaveBothSample(float currentHope)
	{
		int resourceQty = this.drama.Room.resourcesNb;
		int crewQty = this.drama.Room.numberOfCrew;

		int maybeSurvivorQty = Enumerable.Range(1, crewQty).Select(index => Random.Range(0.0f, 1.0f)).Count(value => value > 0.1f);
		bool willRoomBeDestroyed = Random.Range((float)(System.Math.Sqrt(maybeSurvivorQty) * 0.1f), 1.0f) > (1.0f - currentHope);

		DramaOutcomeSample newSample = willRoomBeDestroyed
			? new DramaOutcomeSample(resourceQty, crewQty, resourceQty, crewQty)
			: new DramaOutcomeSample(resourceQty, crewQty, 0, crewQty - maybeSurvivorQty);

		RegisterSample(DramaSolvingOption.TryToSaveBoth, newSample);
	}

	public void GenerateSample(float currentHope)
	{
		GenerateSaveRoomSample();
		GenerateSaveCrewSample();
		GenerateTryToSaveBothSample(currentHope);
	}

	public DramaOutcomeSample PickOneSample(DramaSolvingOption option)
	{
		return this.sampleMap[option][Random.Range(0, this.sampleMap[option].Count)];
	}

	public float EvaluateProbabilityToLooseResources(DramaSolvingOption option)
	{
		int sumResourceQty = this.sampleMap[option].Sum(sample => sample.ResourceQty);
		int sumResourceQtyLoss = this.sampleMap[option].Sum(sample => sample.ResourceQtyLoss);
		return (sumResourceQty - sumResourceQtyLoss) / (float)sumResourceQty;
	}

	public float EvaluateProbabilityToLooseCrew(DramaSolvingOption option)
	{
		int sumCrewQty = this.sampleMap[option].Sum(sample => sample.CrewQty);
		int sumCrewQtyLoss = this.sampleMap[option].Sum(sample => sample.CrewQtyLoss);
		return (sumCrewQty - sumCrewQtyLoss) / (float)sumCrewQty;
	}

	public override string ToString()
	{
		StringBuilder report = new StringBuilder();
		this.sampleMap.Keys.ToList().ForEach(option => {
			report.AppendLine(string.Format("{0}: {1}% to loose resources | {2}% to loose crew", option, EvaluateProbabilityToLooseResources(option) * 100, EvaluateProbabilityToLooseCrew(option) * 100));
		});
		return report.ToString();
	}
}
