using UnityEngine;

public class Blood : Liquid
{
	public const string ID = "BLOOD";

	public override string GetDisplayName()
	{
		return "Human blood";
	}

	public Blood()
	{
		Color = new Color(0.65f, 0f, 0f);
	}

	public override void OnEnterContainer(BloodContainer container)
	{
	}

	public override void OnEnterLimb(LimbBehaviour limb)
	{
	}

	public override void OnUpdate(BloodContainer container)
	{
		if (container is CirculationBehaviour circulationBehaviour && circulationBehaviour.Limb.SpeciesIdentity == "Android" && Random.value > 0.995f)
		{
			circulationBehaviour.Limb.PhysicalBehaviour.Charge += 5f;
		}
	}

	public override void OnExitContainer(BloodContainer container)
	{
	}
}
