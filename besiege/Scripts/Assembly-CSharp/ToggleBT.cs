using System;

public class ToggleBT : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.AdvancedBuilding;
		}
		set
		{
			OptionsMaster.BesiegeConfig.AdvancedBuilding = value;
		}
	}

	protected override void Awake()
	{
		ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(Set));
		base.Awake();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Remove(ReferenceMaster.onAdvancedBuildingToggled, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onAdvancedBuildingToggled != null)
		{
			ReferenceMaster.onAdvancedBuildingToggled();
		}
	}
}
