using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class SoundSwitch : MonoBehaviour
{
	public V2DemoManager DemoManager;

	protected MMSwitch _switch;

	protected virtual void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		MMSwitch mMSwitch = default(MMSwitch);
		_switch = mMSwitch;
	}

	protected virtual void OnEnable()
	{
		//IL_005f: Expected I, but got O
		//IL_006f: Expected O, but got I
		//IL_007f: Expected O, but got I
		V2DemoManager demoManager = DemoManager;
		MMSwitch mMSwitch = _switch;
		if (!demoManager.SoundActive)
		{
			mMSwitch._003CCurrentSwitchState_003Ek__BackingField = MMSwitch.SwitchStates.Off;
		}
		else
		{
			mMSwitch._003CCurrentSwitchState_003Ek__BackingField = MMSwitch.SwitchStates.On;
		}
		while (true)
		{
			MMSwitch mMSwitch2 = _switch;
			nint num = (nint)mMSwitch2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rdx_v3 (Il2CppClass<Lofelt.NiceVibrations.MMSwitch>)+2F8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rdx_v3 (Il2CppClass<Lofelt.NiceVibrations.MMSwitch>)+300]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v5 (should have been resolved before IL gen)");
		}
	}
}
