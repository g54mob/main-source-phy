using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class EngineRoomVolumeProvider : MonoBehaviour, IFloatValueProvider
{
	private Collider engineRoomTrigger;

	private GameObject player;

	private float maxHearingDistance = 20f;

	public float doorEffectMultiplier = 1f;

	private float enterSpeed = 1f;

	private float exitSpeed = 1f;

	private bool verboseLogging;

	private bool inspectorPlayerInside;

	private float inspectorDistanceToPlayer;

	private float inspectorFalloffRaw;

	private float inspectorTargetValue;

	private float inspectorProviderValue;

	public float EngineVolume => inspectorProviderValue;

	public float GetFloatValue()
	{
		return inspectorProviderValue;
	}

	private void Update()
	{
		UpdateProviderValue();
	}

	private unsafe bool IsPlayerInsideCollider()
	{
		//IL_01cb: Expected I4, but got O
		//IL_00cc: Expected O, but got Ref
		//IL_0189: Invalid comparison between F4 and I4
		if (engineRoomTrigger != null && player != null)
		{
			if ((object)player != null)
			{
				Transform transform = player.transform;
				if ((object)transform != null)
				{
					Vector3 position = transform.position;
					if ((object)engineRoomTrigger != null)
					{
						object obj = default(object);
						Vector3 vector = engineRoomTrigger.ClosestPoint((Vector3)(&obj));
						float num = vector.x - position.x;
						object obj2 = default(object);
						float num2 = vector.y - (float)obj2;
						float num3 = vector.z - position.z;
						float num4 = num * num;
						float num5 = num2 * num2;
						float num6 = num3 * num3;
						float num7 = num5 + num4;
						float num8 = num7 + num6;
						bool flag = 0.0001f < num8;
						float num9 = 0.0001f - num8;
						bool flag2 = num9 == 0f;
						bool flag3 = !flag;
						bool flag4 = !flag2;
						return flag4 & flag3;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private unsafe void UpdateProviderValue()
	{
		//IL_007b: Expected O, but got Ref
		//IL_012f: Invalid comparison between F4 and I4
		//IL_02ee: Expected F4, but got I4
		//IL_0457: Invalid comparison between I4 and F4
		//IL_032a: Expected F4, but got I4
		//IL_03c3: Expected O, but got I4
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Expected O, but got Unknown
		//IL_0502: Invalid comparison between F4 and O
		//IL_0338: Expected O, but got I4
		//IL_03df: Expected I, but got O
		//IL_0357: Invalid comparison between F4 and I4
		//IL_02e0: Expected F4, but got I4
		//IL_02bb: Expected F4, but got I4
		float num8;
		float num9;
		float num10;
		if (engineRoomTrigger != null && player != null)
		{
			Transform transform = player.transform;
			Vector3 position = transform.position;
			object obj = default(object);
			Vector3 vector = engineRoomTrigger.ClosestPoint((Vector3)(&obj));
			float num = vector.x - position.x;
			object obj3 = default(object);
			object obj2 = obj3 - obj3;
			float num2 = vector.z - position.z;
			float num3 = num * num;
			object obj4 = obj2 * obj2;
			float num4 = num2 * num2;
			float num5 = (float)obj4 + num3;
			float num6 = num5 + num4;
			bool flag = 0.0001f < num6;
			float num7 = 0.0001f - num6;
			bool flag2 = num7 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			if (inspectorPlayerInside = flag4 & flag3)
			{
				inspectorDistanceToPlayer = 0f;
				num8 = 1f;
				num9 = 1f;
				num10 = 1f;
				goto IL_0393;
			}
		}
		else
		{
			inspectorPlayerInside = false;
		}
		float num18;
		if (player != null)
		{
			Transform transform2 = base.transform;
			Vector3 position2 = transform2.position;
			Transform transform3 = player.transform;
			Vector3 position3 = transform3.position;
			nint num11 = (nint)typeof(Math);
			float num12 = position2.x - position3.x;
			object obj6 = default(object);
			object obj7 = default(object);
			object obj5 = obj6 - obj7;
			float num13 = position2.z - position3.z;
			object obj8 = obj5 * obj5;
			float num14 = num12 * num12;
			float num15 = num13 * num13;
			float num16 = (float)obj8 + num14;
			float num17 = num16 + num15;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ rcx_v17 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				num18 = 0f;
			}
			else
			{
				double num19 = Math.Sqrt(num17);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				num18 = 0f;
			}
		}
		else
		{
			num18 = 0f;
		}
		inspectorDistanceToPlayer = num18;
		float num20 = num18 / maxHearingDistance;
		num10 = 1f - num20;
		if (!(0f > num10))
		{
			if (num10 > 1f)
			{
				num10 = 1f;
			}
		}
		else
		{
			num10 = 0f;
		}
		num8 = num10 * doorEffectMultiplier;
		num9 = 1f;
		goto IL_0393;
		IL_0393:
		inspectorFalloffRaw = num10;
		inspectorTargetValue = num8;
		bool flag5 = !(num8 > inspectorProviderValue);
		object obj9 = 60;
		if (!flag5)
		{
			obj9 = 56;
		}
		float deltaTime = Time.deltaTime;
		float num21 = deltaTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v456 @ rax_v7+this @ rcx (EngineRoomVolumeProvider)]");
		float num22 = num21 * 0f;
		float num23 = num8 - inspectorProviderValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj10 = num23 & 0;
		float num26;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num22) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
		{
			float num24 = num8 - inspectorProviderValue;
			if (num24 < 0f)
			{
				num9 = -1f;
			}
			float num25 = num9 * num22;
			num26 = num25 + inspectorProviderValue;
		}
		else
		{
			num26 = num8;
		}
		inspectorProviderValue = num26;
	}
}
