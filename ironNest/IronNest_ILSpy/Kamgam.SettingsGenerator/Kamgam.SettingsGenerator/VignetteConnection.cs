using System;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class VignetteConnection : Connection<bool>
{
	protected Vignette _vignette;

	public VignetteConnection()
	{
		if (SettingsVolume.Instance != null)
		{
			_vignette = SettingsVolume.Instance.GetOrAddComponent<Vignette>();
			_vignette.Override(_vignette, 1f);
			_vignette.active = false;
			_vignette.intensity.overrideState = true;
			_vignette.intensity.value = 0f;
		}
	}

	public override bool Get()
	{
		//IL_0079: Expected I4, but got O
		if (_vignette != null)
		{
			Vignette vignette = _vignette;
			if ((object)_vignette != null)
			{
				return !vignette.active;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	public override void Set(bool enable)
	{
		if (_vignette != null)
		{
			Vignette vignette = _vignette;
			bool active = (byte)((enable ? 1u : 0u) ^ 1u) != 0;
			vignette.active = active;
			base.NotifyListenersIfChanged(enable);
		}
	}
}
