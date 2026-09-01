using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class GammaConnection : Connection<float>
{
	protected LiftGammaGain _effect;

	public Vector4 _defaultValue;

	public GammaConnection()
	{
		//IL_0123: Expected O, but got I
		//IL_00bb: Expected I4, but got I8
		//IL_010c: Expected O, but got F4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [1822E7BB0]");
		_defaultValue = (Vector4)0;
		base._002Ector();
		if (SettingsVolume.Instance != null)
		{
			_effect = SettingsVolume.Instance.GetOrAddComponent<LiftGammaGain>();
			_effect.Override(_effect, 1f);
			_effect.active = false;
			LiftGammaGain liftGammaGain = SettingsVolume.Instance.FindDefaultVolumeComponent<LiftGammaGain>(useStackAsFallback: false, -1);
			if (liftGammaGain != null)
			{
				_defaultValue = (Vector4)liftGammaGain.gamma.value.x;
			}
		}
	}

	public void UpdateDefaultValue()
	{
		//IL_002a: Expected I4, but got I8
		//IL_007b: Expected O, but got F4
		SettingsVolume instance = SettingsVolume.Instance;
		LiftGammaGain liftGammaGain = instance.FindDefaultVolumeComponent<LiftGammaGain>(useStackAsFallback: false, -1);
		if (liftGammaGain != null)
		{
			_defaultValue = (Vector4)liftGammaGain.gamma.value.x;
		}
	}

	public override float Get()
	{
		//IL_00ab: Expected F4, but got I4
		if (_effect != null)
		{
			LiftGammaGain effect = _effect;
			if (effect.active && effect.gamma.overrideState)
			{
				LiftGammaGain effect2 = _effect;
				Vector4 value = effect2.gamma.value;
				float result = default(float);
				return result;
			}
		}
		return 0f;
	}

	public unsafe override void Set(float gamma)
	{
		//IL_0061: Expected O, but got Ref
		if (_effect != null)
		{
			LiftGammaGain effect = _effect;
			effect.active = true;
			LiftGammaGain effect2 = _effect;
			object obj = default(object);
			effect2.gamma.Override((Vector4)(&obj));
			base.NotifyListenersIfChanged(gamma);
		}
	}
}
