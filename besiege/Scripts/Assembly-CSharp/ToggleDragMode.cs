using UnityEngine;

public class ToggleDragMode : ToggleGodModeButton
{
	public Transform godHandTool;

	public override string GetModeName()
	{
		return "DragMode";
	}

	public override bool IsRuleOn()
	{
		return StatMaster.GodTools.DragMode;
	}

	public override void ToggleRule(bool toggle)
	{
		StatMaster.GodTools.DragMode = toggle;
		godHandTool.gameObject.SetActive(toggle);
	}
}
