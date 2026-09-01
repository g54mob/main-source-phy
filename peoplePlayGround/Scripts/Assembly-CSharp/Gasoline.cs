using UnityEngine;

public class Gasoline : Liquid
{
	public const string ID = "GASOLINE";

	public override string GetDisplayName()
	{
		return "Gasoline";
	}

	public Gasoline()
	{
		Color = new Color(0.3019608f, 0.043137256f, 0f);
	}

	public override void OnEnterContainer(BloodContainer container)
	{
	}

	public override void OnExitContainer(BloodContainer container)
	{
	}

	public override void OnEnterLimb(LimbBehaviour limb)
	{
		limb.Color = new Color(79f / 85f, 13f / 15f, 0.85490197f);
		limb.CirculationBehaviour.IsPump = false;
	}

	public override void OnUpdate(BloodContainer container)
	{
		if (container is CirculationBehaviour circulationBehaviour && circulationBehaviour.Limb.SpeciesIdentity != "Android" && container.GetAmount(this) > 0.28f)
		{
			circulationBehaviour.Limb.Damage(10f * Random.value);
		}
	}
}
