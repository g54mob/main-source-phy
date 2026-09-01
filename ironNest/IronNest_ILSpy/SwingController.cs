using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

public sealed class SwingController : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass32_0
	{
		public System.Random rng;
	}

	private static readonly List<SwingReceiver> Receivers;

	private static bool _003CUseWorldZToScaleWorldXImpulse_003Ek__BackingField;

	private static AnimationCurve _003CWorldZToWorldXImpulseMultiplier_003Ek__BackingField;

	private Vector2 testWorldDirectionXZ;

	private float testImpulseStrength;

	private bool continuousTestImpulse;

	private bool applyTestImpulse;

	private bool allowExternalContinuous;

	private bool allowExternalOneShot;

	private bool enableRandomization;

	private bool useStablePerReceiverRandom;

	private Vector2 strengthMultiplierMinMax;

	private Vector2 dampingMultiplierMinMax;

	private float directionJitterDegrees;

	private Vector2 twistImpulseMinMax;

	private bool useWorldZToScaleWorldXImpulse;

	private AnimationCurve worldZToWorldXImpulseMultiplier;

	private bool findReceiversOnStart;

	private Vector2 _externalContinuousAccumulatedXZ;

	internal static bool UseWorldZToScaleWorldXImpulse
	{
		get
		{
			return _003CUseWorldZToScaleWorldXImpulse_003Ek__BackingField;
		}
		private set
		{
			_003CUseWorldZToScaleWorldXImpulse_003Ek__BackingField = value;
		}
	}

	internal static AnimationCurve WorldZToWorldXImpulseMultiplier
	{
		get
		{
			return _003CWorldZToWorldXImpulseMultiplier_003Ek__BackingField;
		}
		private set
		{
			_003CWorldZToWorldXImpulseMultiplier_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		PublishCurveConfig();
	}

	private void OnValidate()
	{
		PublishCurveConfig();
	}

	private void PublishCurveConfig()
	{
		_003CUseWorldZToScaleWorldXImpulse_003Ek__BackingField = useWorldZToScaleWorldXImpulse;
		_003CWorldZToWorldXImpulseMultiplier_003Ek__BackingField = worldZToWorldXImpulseMultiplier;
	}

	private void Start()
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		if (!findReceiversOnStart)
		{
			return;
		}
		List<SwingReceiver> receivers = Receivers;
		int version = receivers._version + 1;
		receivers._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			receivers._size = 0;
		}
		else
		{
			receivers._size = 0;
			if (receivers._size > 0)
			{
				Array.Clear(receivers._items, 0, receivers._size);
			}
		}
		SwingReceiver[] array = UnityEngine.Object.FindObjectsByType<SwingReceiver>(FindObjectsSortMode.None);
		object obj2 = array + 32;
		int num = 0;
		int num2 = 0;
		while (num < array.Length)
		{
			if ((UnityEngine.Object)obj2 != null && !Receivers.Contains((SwingReceiver)obj2))
			{
				Receivers.Add((SwingReceiver)obj2);
			}
			num2++;
			obj2 += 8;
			num = num2;
		}
	}

	private void Update()
	{
		//IL_013f: Expected I, but got O
		//IL_00c6: Expected O, but got I
		//IL_00dc: Invalid comparison between O and F4
		if (!Application.isPlaying)
		{
			return;
		}
		Vector2 baseWorldXZImpulse = default(Vector2);
		if (continuousTestImpulse)
		{
			ApplyImpulseToAll(baseWorldXZImpulse, 0f);
		}
		if (applyTestImpulse)
		{
			ApplyImpulseToAll(baseWorldXZImpulse, 0f);
			applyTestImpulse = false;
		}
		if (allowExternalContinuous)
		{
			object obj = _externalContinuousAccumulatedXZ * _externalContinuousAccumulatedXZ;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingController)+68]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingController)+68]");
			object obj2 = num * 0;
			object obj3 = obj + obj2;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-07f))
			{
				ApplyImpulseToAll(baseWorldXZImpulse, 0f);
			}
		}
		nint num2 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rax_v9 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rax_v10 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		_externalContinuousAccumulatedXZ = Vector2.zeroVector;
	}

	public void AddExternalContinuousWorldXZ(Vector2 worldXZImpulse)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		if (Application.isPlaying)
		{
			Vector2 externalContinuousAccumulatedXZ = worldXZImpulse + _externalContinuousAccumulatedXZ;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingController)+68]");
			object obj2 = default(object);
			object obj = obj2 + 0;
			_externalContinuousAccumulatedXZ = externalContinuousAccumulatedXZ;
		}
	}

	public void TriggerExternalImpulse(Vector2 worldXZImpulse, float worldTwistImpulse = 0f)
	{
		if (Application.isPlaying && allowExternalOneShot)
		{
			ApplyImpulseToAll(worldXZImpulse, worldTwistImpulse);
		}
	}

	private void ApplyImpulseToAll(Vector2 baseWorldXZImpulse, float baseWorldTwistImpulse)
	{
		//IL_002f: Expected O, but got I4
		//IL_00ba: Expected O, but got I4
		//IL_0122: Expected F4, but got I
		//IL_0122: Expected F4, but got O
		//IL_0141: Expected F4, but got I
		//IL_0141: Expected F4, but got O
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected F4, but got Unknown
		//IL_0267: Expected F4, but got I
		//IL_0267: Expected F4, but got O
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_0201: Invalid comparison between O and F4
		List<SwingReceiver> receivers = Receivers;
		bool flag = (nint)Receivers < 0;
		int num = receivers._size - 1;
		if (flag)
		{
			return;
		}
		_003C_003Ec__DisplayClass32_0 obj = (_003C_003Ec__DisplayClass32_0)0;
		Vector2 vector = baseWorldXZImpulse;
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		float num7 = default(float);
		bool flag4;
		Vector2 worldXZImpulse = default(Vector2);
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj2 != null)
			{
				bool flag2 = !enableRandomization;
				float dampingMultiplier = 1f;
				float impulseScaleMultiplier = 1f;
				float worldYTwistImpulse = baseWorldTwistImpulse;
				if (!flag2)
				{
					bool flag3 = !useStablePerReceiverRandom;
					obj = (_003C_003Ec__DisplayClass32_0)0;
					System.Random random = null;
					if (!flag3)
					{
						int instanceID = obj2.GetInstanceID();
						System.Random random2 = new System.Random(instanceID);
						obj = (_003C_003Ec__DisplayClass32_0)random2;
						random = random2;
					}
					Vector2 vector2 = strengthMultiplierMinMax;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingController)+38]");
					float num2 = _003CApplyImpulseToAll_003Eg__Range_007C32_1((float)vector2, 0f, ref obj);
					Vector2 vector3 = dampingMultiplierMinMax;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingController)+40]");
					float num3 = _003CApplyImpulseToAll_003Eg__Range_007C32_1((float)vector3, 0f, ref obj);
					float num4 = (float)vector * num2;
					float num5 = directionJitterDegrees;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
					float min = num5 ^ 0;
					float num6 = num7 * num2;
					float num8 = _003CApplyImpulseToAll_003Eg__Range_007C32_1(min, directionJitterDegrees, ref obj);
					float num9 = num6 * num6;
					float num10 = num4 * num4;
					float num11 = num9 + num10;
					if (num11 > 1E-06f)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj3 = num8 & 0;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
						{
							float num12 = num8 * ((float)Math.PI / 180f);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
						}
					}
					Vector2 vector4 = twistImpulseMinMax;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingController)+4C]");
					float num13 = _003CApplyImpulseToAll_003Eg__Range_007C32_1((float)vector4, 0f, ref obj);
					worldYTwistImpulse = num13 + baseWorldTwistImpulse;
					dampingMultiplier = num3;
					impulseScaleMultiplier = num2;
					vector = baseWorldXZImpulse;
				}
				flag4 = (nint)obj2 < 0;
				((SwingReceiver)obj2).ApplyControllerOverrides(impulseScaleMultiplier, dampingMultiplier);
				((SwingReceiver)obj2).ApplyWorldImpulse(worldXZImpulse, worldYTwistImpulse);
			}
			else
			{
				flag4 = (nint)Receivers < 0;
				Receivers.RemoveAt(num);
			}
			num--;
		}
		while (!flag4);
	}

	public static void Register(SwingReceiver receiver)
	{
		if (receiver != null && !Receivers.Contains(receiver))
		{
			Receivers.Add(receiver);
		}
	}

	public static void Unregister(SwingReceiver receiver)
	{
		if (receiver != null)
		{
			bool flag = Receivers.Remove(receiver);
		}
	}

	public SwingController()
	{
		//IL_000b: Expected O, but got I4
		//IL_003d: Expected O, but got I4
		//IL_004e: Expected O, but got I4
		//IL_006e: Expected O, but got I8
		testWorldDirectionXZ = (Vector2)0;
		_ = 1065353216;
		testImpulseStrength = 1f;
		allowExternalContinuous = true;
		enableRandomization = true;
		strengthMultiplierMinMax = (Vector2)1062836634;
		_ = 1066611507;
		dampingMultiplierMinMax = (Vector2)1063675494;
		_ = 1066192077;
		directionJitterDegrees = 8f;
		twistImpulseMinMax = (Vector2)3189348762L;
		_ = 1041865114;
		worldZToWorldXImpulseMultiplier = AnimationCurve.Linear(-10f, 1f, 10f, 1f);
		findReceiversOnStart = true;
		base._002Ector();
	}

	static SwingController()
	{
		List<SwingReceiver> receivers = new List<SwingReceiver>(256);
		Receivers = receivers;
	}

	internal static float _003CApplyImpulseToAll_003Eg__Next01_007C32_0(ref _003C_003Ec__DisplayClass32_0 P_0)
	{
		if ((object)P_0 != null)
		{
			object obj = P_0;
			object obj2 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v12 @ rdx_v1+1B8] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
			float result = default(float);
			return result;
		}
		return UnityEngine.Random.value;
	}

	internal static float _003CApplyImpulseToAll_003Eg__Range_007C32_1(float min, float max, ref _003C_003Ec__DisplayClass32_0 P_2)
	{
		//IL_0074: Invalid comparison between I4 and F4
		//IL_00bf: Expected F4, but got I4
		float num;
		if ((object)P_2 != null)
		{
			object obj = P_2;
			object obj2 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v58 @ rdx_v2+1B8] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
			num = min;
		}
		else
		{
			num = UnityEngine.Random.value;
		}
		if (!(0f > num))
		{
			if (num > 1f)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num2 = max - min;
		float num3 = num2 * num;
		return num3 + min;
	}
}
