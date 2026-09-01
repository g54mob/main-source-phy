using System;

public class ToggleCinematicMode : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.SmoothCamera;
		}
		set
		{
			OptionsMaster.BesiegeConfig.SmoothCamera = value;
		}
	}

	protected override void Awake()
	{
		ReferenceMaster.onSmoothCamToggled = (Action)Delegate.Combine(ReferenceMaster.onSmoothCamToggled, new Action(Set));
		base.Awake();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onSmoothCamToggled = (Action)Delegate.Remove(ReferenceMaster.onSmoothCamToggled, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onSmoothCamToggled != null)
		{
			ReferenceMaster.onSmoothCamToggled();
		}
	}
}
