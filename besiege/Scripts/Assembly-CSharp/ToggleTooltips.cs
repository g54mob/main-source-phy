using System;

public class ToggleTooltips : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.Tooltips;
		}
		set
		{
			OptionsMaster.BesiegeConfig.Tooltips = value;
		}
	}

	protected override void Awake()
	{
		ReferenceMaster.onTooltipsToggled = (Action)Delegate.Combine(ReferenceMaster.onTooltipsToggled, new Action(Set));
		base.Awake();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onTooltipsToggled = (Action)Delegate.Remove(ReferenceMaster.onTooltipsToggled, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onTooltipsToggled != null)
		{
			ReferenceMaster.onTooltipsToggled();
		}
	}
}
