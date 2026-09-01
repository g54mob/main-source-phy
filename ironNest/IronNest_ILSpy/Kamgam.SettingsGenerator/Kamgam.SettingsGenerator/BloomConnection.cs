using System;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class BloomConnection : Connection<bool>
{
	protected Bloom _bloom;

	public BloomConnection()
	{
		if (SettingsVolume.Instance != null)
		{
			_bloom = SettingsVolume.Instance.GetOrAddComponent<Bloom>();
			_bloom.Override(_bloom, 1f);
			_bloom.active = false;
			_bloom.intensity.overrideState = true;
			_bloom.intensity.value = 0f;
		}
	}

	public override bool GetDefault()
	{
		//IL_00a0: Expected I4, but got O
		//IL_0042: Expected I4, but got I8
		SettingsVolume instance = SettingsVolume.Instance;
		if ((object)instance != null)
		{
			Bloom bloom = instance.FindDefaultVolumeComponent<Bloom>(useStackAsFallback: true, -1);
			bool flag = bloom;
			if (!flag)
			{
				return flag;
			}
			if ((object)bloom != null)
			{
				return bloom.active;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public override bool Get()
	{
		//IL_0079: Expected I4, but got O
		if (_bloom != null)
		{
			Bloom bloom = _bloom;
			if ((object)_bloom != null)
			{
				return !bloom.active;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	public override void Set(bool enable)
	{
		if (_bloom != null)
		{
			Bloom bloom = _bloom;
			bool active = (byte)((enable ? 1u : 0u) ^ 1u) != 0;
			bloom.active = active;
			base.NotifyListenersIfChanged(enable);
		}
	}
}
