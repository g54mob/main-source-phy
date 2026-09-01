using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class HapticReceiver : MonoBehaviour, ISerializationCallbackReceiver
{
	private float _outputLevel = 1f;

	private bool _hapticsEnabled = true;

	public float outputLevel
	{
		get
		{
			return HapticController._outputLevel;
		}
		set
		{
			HapticController._outputLevel = value;
			if (HapticController.Init())
			{
			}
			HapticController.ApplyLevelsToGamepadRumbler();
		}
	}

	public bool hapticsEnabled
	{
		get
		{
			return HapticController._hapticsEnabled;
		}
		set
		{
			//IL_008b: Expected I, but got O
			//IL_001d: Expected I, but got O
			bool flag = !HapticController._hapticsEnabled;
			nint num = (nint)typeof(HapticController);
			if (!flag)
			{
				HapticController.Stop();
				num = (nint)typeof(HapticController);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v6 (Il2CppClass<Lofelt.NiceVibrations.HapticController>)+E4]");
			if ((nint)0 == 0)
			{
				HapticController._hapticsEnabled = value;
			}
			else
			{
				HapticController._hapticsEnabled = value;
			}
		}
	}

	public void OnBeforeSerialize()
	{
		_outputLevel = HapticController._outputLevel;
		_hapticsEnabled = HapticController._hapticsEnabled;
	}

	public void OnAfterDeserialize()
	{
		HapticController._outputLevel = _outputLevel;
		HapticController._hapticsEnabled = _hapticsEnabled;
	}

	private void Start()
	{
		bool flag = HapticController.Init();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (!hasFocus)
		{
			HapticController.Stop();
		}
	}

	private void OnDestroy()
	{
		GamepadRumbler.Stop();
	}
}
