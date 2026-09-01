public class ToggleInvincible : ToggleGodModeButton
{
	public override string GetModeName()
	{
		return "Invincibility";
	}

	public override bool IsRuleOn()
	{
		return StatMaster.GodTools.UnbreakableMode;
	}

	public override void ToggleRule(bool toggle)
	{
		StatMaster.GodTools.UnbreakableMode = toggle;
		Machine machine = Machine.Active();
		if (!(machine != null))
		{
			return;
		}
		machine.UnbreakableMode = toggle;
		foreach (BlockBehaviour simulationBlock in machine.SimulationBlocks)
		{
			simulationBlock.SetJointsInvicible(toggle);
		}
	}
}
