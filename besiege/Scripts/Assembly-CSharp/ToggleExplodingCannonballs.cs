public class ToggleExplodingCannonballs : ToggleGodModeButton
{
	public override string GetModeName()
	{
		return "ExplosiveCannonballs";
	}

	public override bool IsRuleOn()
	{
		return StatMaster.GodTools.ExplodingCannonballs;
	}

	public override void ToggleRule(bool toggle)
	{
		StatMaster.GodTools.ExplodingCannonballs = toggle;
		Machine machine = Machine.Active();
		if (machine != null)
		{
			machine.ExplodingCannonballs = toggle;
		}
	}
}
