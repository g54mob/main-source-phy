using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public sealed class EnginePowerController : MonoBehaviour, IFloatValueProvider
{
	private DieselEngineController dieselEngine;

	private float riseSpeed = 0.3f;

	private float fallSpeed = 0.5f;

	private float _debugTargetPower;

	private float _debugCurrentPower;

	private float _003CPower_003Ek__BackingField;

	public float Power
	{
		get
		{
			return _003CPower_003Ek__BackingField;
		}
		private set
		{
			_003CPower_003Ek__BackingField = value;
		}
	}

	public float ClampedPower => _003CPower_003Ek__BackingField;

	public string ProviderName
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA4F]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			return "EnginePower";
		}
	}

	public float GetFloatValue()
	{
		return _003CPower_003Ek__BackingField;
	}

	private void Update()
	{
		//IL_007a: Expected F4, but got I4
		//IL_0101: Expected O, but got I4
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0198: Invalid comparison between F4 and O
		//IL_0088: Expected O, but got I4
		//IL_00a7: Invalid comparison between F4 and I4
		float deltaTime = Time.deltaTime;
		float num;
		if (dieselEngine != null)
		{
			DieselEngineController dieselEngineController = dieselEngine;
			if (dieselEngineController._003CEnginesRunning_003Ek__BackingField)
			{
				num = dieselEngineController._003CFuelMixtureSystemValue_003Ek__BackingField;
				goto IL_00e5;
			}
		}
		num = 0f;
		goto IL_00e5;
		IL_00e5:
		bool flag = !(num > _003CPower_003Ek__BackingField);
		object obj = 44;
		if (!flag)
		{
			obj = 40;
		}
		float num2 = deltaTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v6+this @ rcx (EnginePowerController)]");
		float num3 = num2 * 0f;
		float num4 = num - _003CPower_003Ek__BackingField;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num4 & 0;
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		bool flag3 = !flag2;
		float num5 = num;
		if (!flag3)
		{
			float num6 = num - _003CPower_003Ek__BackingField;
			float num7 = ((num6 < 0f) ? (-1f) : 1f);
			float num8 = num7 * num3;
			num5 = num8 + _003CPower_003Ek__BackingField;
		}
		_003CPower_003Ek__BackingField = num5;
		_debugTargetPower = num;
		_debugCurrentPower = num5;
	}
}
