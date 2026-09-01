using System;
using Localisation;
using UnityEngine;

public class ToggleAA : ToggleSetting
{
	public AntialiasingAsPostEffect aaCode;

	public TextMesh text;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.BesiegeConfig.AntiAliasingMode != AAMode.FXAA2;
		}
		set
		{
			OptionsMaster.BesiegeConfig.AntiAliasingMode = (value ? OptionsMaster.FormerAntiAliasingMode : AAMode.FXAA2);
		}
	}

	protected override void Awake()
	{
		if (aaCode == null && !StatMaster.inMenu)
		{
			aaCode = Camera.main.gameObject.GetComponent<AntialiasingAsPostEffect>();
		}
		if (text == null)
		{
			text = base.transform.parent.GetComponentInChildren<TextMesh>();
		}
		ReferenceMaster.onAAChanged = (Action)Delegate.Combine(ReferenceMaster.onAAChanged, new Action(Set));
		base.Awake();
	}

	protected override void OnDestroy()
	{
		ReferenceMaster.onAAChanged = (Action)Delegate.Remove(ReferenceMaster.onAAChanged, new Action(Set));
	}

	public override void OnClicked()
	{
		base.OnClicked();
		if (ReferenceMaster.onAAChanged != null)
		{
			ReferenceMaster.onAAChanged();
		}
	}

	public override void Set()
	{
		base.Set();
		int[] array = new int[5] { 2142, 43, 3443, 3444, 3445 };
		if (IsActive)
		{
			text.text = LocalisationManager.GetTranslation(array[(int)OptionsMaster.BesiegeConfig.AntiAliasingMode]);
		}
		else
		{
			text.text = LocalisationManager.GetTranslation(array[(int)OptionsMaster.FormerAntiAliasingMode]);
		}
	}
}
