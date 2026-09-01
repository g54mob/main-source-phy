using System;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class MotionBlurConnection : Connection<bool>
{
	protected MotionBlur _blur;

	public MotionBlurConnection()
	{
		if (SettingsVolume.Instance != null)
		{
			_blur = SettingsVolume.Instance.GetOrAddComponent<MotionBlur>();
			_blur.Override(_blur, 1f);
			_blur.active = false;
			_blur.quality.overrideState = true;
			_blur.quality.value = MotionBlurQuality.Low;
			_blur.intensity.overrideState = true;
			_blur.intensity.value = 0f;
		}
	}

	public override bool Get()
	{
		//IL_0079: Expected I4, but got O
		if (_blur != null)
		{
			MotionBlur blur = _blur;
			if ((object)_blur != null)
			{
				return !blur.active;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	public override void Set(bool enable)
	{
		if (_blur != null)
		{
			MotionBlur blur = _blur;
			bool active = (byte)((enable ? 1u : 0u) ^ 1u) != 0;
			blur.active = active;
			base.NotifyListenersIfChanged(enable);
		}
	}
}
