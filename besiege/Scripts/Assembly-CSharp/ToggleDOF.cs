using System;
using UnityEngine;

public class ToggleDOF : ToggleSetting
{
	public DepthOfFieldScatter dofCode;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.DepthOfField;
		}
		set
		{
			OptionsMaster.BesiegeConfig.DepthOfField = value;
		}
	}

	protected override void Awake()
	{
		if (dofCode == null)
		{
			dofCode = Camera.main.gameObject.GetComponent<DepthOfFieldScatter>();
		}
		ReferenceMaster.onDOFChanged = (Action)Delegate.Combine(ReferenceMaster.onDOFChanged, new Action(Set));
		base.Awake();
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onDOFChanged != null)
		{
			ReferenceMaster.onDOFChanged();
		}
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onDOFChanged = (Action)Delegate.Remove(ReferenceMaster.onDOFChanged, new Action(Set));
	}

	public override void Set()
	{
		base.Set();
		if (dofCode != null)
		{
			dofCode.enabled = IsActive;
		}
	}
}
