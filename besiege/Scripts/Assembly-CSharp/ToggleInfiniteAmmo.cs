public class ToggleInfiniteAmmo : ToggleGodModeButton
{
	public override string GetModeName()
	{
		return "InfiniteAmmo";
	}

	public override bool IsRuleOn()
	{
		return StatMaster.GodTools.InfiniteAmmoMode;
	}

	public override void ToggleRule(bool toggle)
	{
		StatMaster.GodTools.InfiniteAmmoMode = toggle;
		Machine machine = Machine.Active();
		if (machine != null)
		{
			machine.InfiniteAmmoMode = toggle;
		}
	}
}
