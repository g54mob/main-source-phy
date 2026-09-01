using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class EffectFromProfile : MonoBehaviour
{
	public const string ClassName = "EffectFromProfile";

	private EffectAbstractBase m_EffectProfile;

	private EffectAbstractBase m_EffectInstance;

	public EffectAbstractBase effectProfile
	{
		get
		{
			return m_EffectProfile;
		}
		set
		{
			m_EffectProfile = value;
			InitInstanceFromProfile();
		}
	}

	public void InitInstanceFromProfile()
	{
		//IL_0075: Expected I, but got O
		//IL_008f: Expected O, but got I
		//IL_009f: Expected O, but got I
		while ((bool)m_EffectInstance)
		{
			bool flag = m_EffectProfile;
			Behaviour effectInstance = m_EffectInstance;
			if (!flag)
			{
				effectInstance.enabled = false;
				break;
			}
			nint num = (nint)effectInstance;
			EffectAbstractBase effectAbstractBase = m_EffectProfile;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ r8_v2 (Il2CppClass<UnityEngine.Behaviour>)+178]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ r8_v2 (Il2CppClass<UnityEngine.Behaviour>)+180]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v115 @ rax_v8 (should have been resolved before IL gen)");
		}
	}

	private void OnEnable()
	{
		//IL_00b2: Expected I, but got O
		//IL_00c0: Expected I, but got O
		//IL_00d0: Expected O, but got I
		//IL_010c: Expected O, but got I
		//IL_0131: Expected O, but got I4
		//IL_0217: Expected I, but got O
		//IL_021f: Expected I, but got O
		//IL_022f: Expected O, but got I
		//IL_0166: Expected O, but got I
		//IL_018b: Expected O, but got I4
		Component component;
		EffectAbstractBase effectAbstractBase;
		if (!m_EffectInstance)
		{
			if (!m_EffectProfile)
			{
				return;
			}
			GameObject gameObject = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type componentType = default(Type);
			component = gameObject.Internal_AddComponentWithType(componentType);
			if ((object)component == null)
			{
				m_EffectInstance = null;
				goto IL_01ba;
			}
			nint num = (nint)component;
			nint num2 = (nint)typeof(EffectAbstractBase);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v3 (Il2CppClass<VLB.EffectAbstractBase>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r11_v3 (Il2CppClass<UnityEngine.Component>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v3 (Il2CppClass<VLB.EffectAbstractBase>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r11_v3 (Il2CppClass<UnityEngine.Component>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rax_v26+FFFFFFF8+v227 @ rax_v16*8]");
				bool flag = 0 == (nint)typeof(EffectAbstractBase);
				effectAbstractBase = (EffectAbstractBase)1;
				if (flag)
				{
					goto IL_01c5;
				}
			}
			effectAbstractBase = null;
			goto IL_01c5;
		}
		m_EffectInstance.enabled = true;
		return;
		IL_01c5:
		bool flag2 = (object)effectAbstractBase == null;
		Component effectInstance = null;
		if (!flag2)
		{
			effectInstance = component;
		}
		EffectAbstractBase effectAbstractBase2;
		do
		{
			m_EffectInstance = (EffectAbstractBase)effectInstance;
			nint num4 = (nint)typeof(EffectAbstractBase);
			nint num5 = (nint)component;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r10_v4 (Il2CppClass<VLB.EffectAbstractBase>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ r11_v4 (Il2CppClass<UnityEngine.Component>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r10_v4 (Il2CppClass<VLB.EffectAbstractBase>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ r11_v4 (Il2CppClass<UnityEngine.Component>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ rax_v23+FFFFFFF8+v312 @ rax_v20*8]");
				bool flag3 = 0 == (nint)typeof(EffectAbstractBase);
				effectAbstractBase2 = (EffectAbstractBase)1;
				if (flag3)
				{
					continue;
				}
			}
			effectAbstractBase2 = null;
		}
		while ((object)effectAbstractBase2 != null);
		goto IL_01ba;
		IL_01ba:
		InitInstanceFromProfile();
	}

	private void OnDisable()
	{
		if ((bool)m_EffectInstance)
		{
			m_EffectInstance.enabled = false;
		}
	}
}
