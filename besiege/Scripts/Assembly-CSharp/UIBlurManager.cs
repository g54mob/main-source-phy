using System;
using UnityEngine;

public class UIBlurManager : SingleInstance<UIBlurManager>
{
	public Renderer[] blurRenderers;

	public Camera[] blurCameras;

	public override string Name
	{
		get
		{
			return "UIBlurManager";
		}
	}

	private void Awake()
	{
		ReferenceMaster.onUIBlurToggled = (Action)Delegate.Combine(ReferenceMaster.onUIBlurToggled, new Action(ToggleUIBlur));
		ToggleUIBlur();
	}

	public void ToggleUIBlur()
	{
		bool uIBlur = OptionsMaster.BesiegeConfig.UIBlur;
		for (int i = 0; i < blurRenderers.Length; i++)
		{
			if (blurRenderers[i] != null)
			{
				blurRenderers[i].enabled = uIBlur;
			}
		}
		for (int j = 0; j < blurCameras.Length; j++)
		{
			if (blurCameras[j] != null)
			{
				blurCameras[j].enabled = uIBlur;
			}
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onUIBlurToggled = (Action)Delegate.Remove(ReferenceMaster.onUIBlurToggled, new Action(ToggleUIBlur));
	}
}
