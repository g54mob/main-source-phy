using System;

public class ToggleUIBlur : ToggleSetting
{
	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.UIBlur;
		}
		set
		{
			OptionsMaster.BesiegeConfig.UIBlur = value;
		}
	}

	protected override void Awake()
	{
		ReferenceMaster.onUIBlurToggled = (Action)Delegate.Combine(ReferenceMaster.onUIBlurToggled, new Action(Set));
		base.Awake();
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onUIBlurToggled != null)
		{
			ReferenceMaster.onUIBlurToggled();
		}
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onUIBlurToggled = (Action)Delegate.Remove(ReferenceMaster.onUIBlurToggled, new Action(Set));
	}

	public override void Set()
	{
		base.Set();
	}
}
