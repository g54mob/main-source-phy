using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class ShellBlueprint : MonoBehaviour
{
	public GameObject shellVisualPrefab;

	public int currentPowderCharge;

	public ShellDefinition shellDefinition;

	public void Init(ShellDefinition shell)
	{
		shellDefinition = shell;
	}

	public float GetAdjustedShellSpeed()
	{
		//IL_005f: Expected F4, but got I4
		ShellDefinition shellDefinition = this.shellDefinition;
		if ((object)this.shellDefinition != null && shellDefinition.chargeToSpeedMultiplier != null)
		{
			float num = shellDefinition.chargeToSpeedMultiplier.Evaluate(currentPowderCharge);
			ShellDefinition shellDefinition2 = this.shellDefinition;
			if ((object)this.shellDefinition != null)
			{
				return num * shellDefinition2.ShellSpeed;
			}
		}
		throw new NullReferenceException();
	}

	public float GetAdjustedHorizontalDispersion()
	{
		//IL_005f: Expected F4, but got I4
		ShellDefinition shellDefinition = this.shellDefinition;
		if ((object)this.shellDefinition != null && shellDefinition.chargeToHorizontalDispersionMultiplier != null)
		{
			float num = shellDefinition.chargeToHorizontalDispersionMultiplier.Evaluate(currentPowderCharge);
			ShellDefinition shellDefinition2 = this.shellDefinition;
			if ((object)this.shellDefinition != null)
			{
				return num * shellDefinition2.horizontalDispersion;
			}
		}
		throw new NullReferenceException();
	}

	public float GetAdjustedVerticalDispersion()
	{
		//IL_005f: Expected F4, but got I4
		ShellDefinition shellDefinition = this.shellDefinition;
		if ((object)this.shellDefinition != null && shellDefinition.chargeToVerticalDispersionMultiplier != null)
		{
			float num = shellDefinition.chargeToVerticalDispersionMultiplier.Evaluate(currentPowderCharge);
			ShellDefinition shellDefinition2 = this.shellDefinition;
			if ((object)this.shellDefinition != null)
			{
				return num * shellDefinition2.verticalDispersion;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void GetRangeForCharge(int chargeLevel, out float minRange, out float maxRange)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_004f: Expected O, but got I4
		//IL_0058: Expected O, but got I4
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0119: Expected O, but got I4
		//IL_0122: Expected O, but got I4
		//IL_012b: Expected O, but got I4
		//IL_0134: Expected O, but got I4
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_015b: Expected O, but got I
		//IL_0229: Expected I, but got O
		//IL_0164: Unsupported input type for neg.
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		ShellDefinition shellDefinition = this.shellDefinition;
		ref float reference = ref *(float*)null;
		ref float reference2 = ref *(float*)1148846080;
		PowderChargeRangeMapping[] chargeRangeMappings = shellDefinition.chargeRangeMappings;
		object obj = shellDefinition.chargeRangeMappings + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < chargeRangeMappings.Length)
		{
			object obj4 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v13+10]");
			if ((nint)0 != chargeLevel)
			{
				obj3++;
				obj += 8;
				obj2 = obj3;
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v13+14]");
			reference = ref *(float*)null;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v13+18]");
			reference2 = ref *(float*)null;
			return;
		}
		ShellDefinition shellDefinition2 = this.shellDefinition;
		PowderChargeRangeMapping[] chargeRangeMappings2 = shellDefinition2.chargeRangeMappings;
		object obj5 = shellDefinition2.chargeRangeMappings + 32;
		object obj6 = 0;
		object obj7 = 2147483647;
		object obj8 = 0;
		object obj9 = 0;
		while ((nint)obj8 < chargeRangeMappings2.Length)
		{
			object obj10 = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ rbx_v8+10]");
			object obj11 = -chargeLevel;
			nint num = (nint)typeof(Math);
			object obj12 = 0 - obj11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rcx_v10 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 < (nint)0)
			{
				obj12 = obj11;
			}
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7);
			object obj13 = obj7;
			if (!flag)
			{
				obj13 = obj12;
			}
			obj6++;
			obj5 += 8;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
			{
				obj10 = obj9;
			}
			obj7 = obj13;
			obj8 = obj6;
			obj9 = obj10;
		}
		if (obj9 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ rbp_v6+14]");
			reference = ref *(float*)null;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ rbp_v6+18]");
			reference2 = ref *(float*)null;
		}
	}

	public bool SetPowderCharge(int chargeLevel)
	{
		//IL_00ae: Expected I4, but got O
		ShellDefinition shellDefinition = this.shellDefinition;
		if ((object)this.shellDefinition != null)
		{
			int num = default(int);
			if (num >= 1)
			{
				if (num > shellDefinition.maxPowderCharges)
				{
					num = shellDefinition.maxPowderCharges;
				}
			}
			else
			{
				num = 1;
			}
			if (currentPowderCharge == num)
			{
				return false;
			}
			currentPowderCharge = num;
			return true;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
