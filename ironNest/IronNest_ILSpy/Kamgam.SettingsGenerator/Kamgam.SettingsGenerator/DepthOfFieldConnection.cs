using System;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class DepthOfFieldConnection : Connection<bool>
{
	protected DepthOfField _dof;

	public DepthOfFieldConnection()
	{
		if (SettingsVolume.Instance != null)
		{
			_dof = SettingsVolume.Instance.GetOrAddComponent<DepthOfField>();
			_dof.Override(_dof, 1f);
			_dof.active = false;
			_dof.mode.overrideState = true;
			_dof.mode.value = DepthOfFieldMode.Gaussian;
			_dof.gaussianStart.overrideState = true;
			_dof.gaussianStart.value = 3.4028235E+38f;
			_dof.gaussianEnd.overrideState = true;
			_dof.gaussianEnd.value = 3.4028235E+38f;
		}
	}

	public override bool Get()
	{
		//IL_0079: Expected I4, but got O
		if (_dof != null)
		{
			DepthOfField dof = _dof;
			if ((object)_dof != null)
			{
				return !dof.active;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	public override void Set(bool enable)
	{
		if (_dof != null)
		{
			DepthOfField dof = _dof;
			bool active = (byte)((enable ? 1u : 0u) ^ 1u) != 0;
			dof.active = active;
			base.NotifyListenersIfChanged(enable);
		}
	}
}
