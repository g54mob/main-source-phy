using System;
using UnityEngine;

public class ToggleBlood : ToggleSetting
{
	public TextMesh text;

	public bool toggleText = true;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.BloodEnabled;
		}
		set
		{
			OptionsMaster.BesiegeConfig.BloodEnabled = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ReferenceMaster.onBloodToggled = (Action)Delegate.Combine(ReferenceMaster.onBloodToggled, new Action(Set));
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onBloodToggled = (Action)Delegate.Remove(ReferenceMaster.onBloodToggled, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onBloodToggled != null)
		{
			ReferenceMaster.onBloodToggled();
		}
	}
}
