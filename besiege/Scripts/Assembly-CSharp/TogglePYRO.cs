public class TogglePYRO : ToggleGodModeButton
{
	public override string GetModeName()
	{
		return "Fire";
	}

	public override bool IsRuleOn()
	{
		return StatMaster.GodTools.PyroMode;
	}

	public override void ToggleRule(bool toggle)
	{
		StatMaster.GodTools.PyroMode = toggle;
	}
}
