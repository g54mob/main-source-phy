using Cpp2ILInjected;
using UnityEngine;

public sealed class SwingReceiver : MonoBehaviour
{
	private Transform pivot;

	private float baseImpulseScale = 1f;

	private float maxTiltAngleDegrees = 20f;

	private float maxTwistAngleDegrees = 4f;

	private float stiffness = 18f;

	private float baseDamping = 6f;

	private bool useCapturedRestRotation = true;

	private float _impulseScaleMul = 1f;

	private float _dampingMul = 1f;

	private Vector2 _tiltAngleDeg;

	private Vector2 _tiltAngularVel;

	private float _twistAngleDeg;

	private float _twistAngularVel;

	private Quaternion _restWorldRotation;

	private float _previousMotionMagnitude;

	private float _motionMagnitude;

	private float _motionSpikePerSecond;

	public Vector2 TiltAngleDegrees
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
	}

	public Vector2 TiltAngularVelocity
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
	}

	public float TwistAngleDegrees => _twistAngleDeg;

	public float TwistAngularVelocity => _twistAngularVel;

	public float MotionMagnitude => _motionMagnitude;

	public float MotionSpikePerSecond => _motionSpikePerSecond;

	private void Awake()
	{
		//IL_0065: Expected O, but got F4
		if (pivot == null)
		{
			Transform transform = base.transform;
			pivot = transform;
		}
		_restWorldRotation = (Quaternion)pivot.rotation.x;
	}

	private void OnEnable()
	{
		SwingController.Register(this);
	}

	private void OnDisable()
	{
		SwingController.Unregister(this);
	}

	public void ApplyControllerOverrides(float impulseScaleMultiplier, float dampingMultiplier)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_001b: Expected F4, but got I4
		//IL_0056: Invalid comparison between I4 and F4
		//IL_0068: Expected F4, but got I4
		bool flag = !(0f < impulseScaleMultiplier);
		float impulseScaleMul = 0f;
		if (!flag)
		{
			impulseScaleMul = impulseScaleMultiplier;
		}
		_impulseScaleMul = impulseScaleMul;
		bool flag2 = !(0f < dampingMultiplier);
		float dampingMul = 0f;
		if (!flag2)
		{
			dampingMul = dampingMultiplier;
		}
		_dampingMul = dampingMul;
	}

	public void ApplyWorldImpulse(Vector2 worldXZImpulse, float worldYTwistImpulse)
	{
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0190: Expected O, but got F4
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		Vector2 vector;
		if (SwingController._003CUseWorldZToScaleWorldXImpulse_003Ek__BackingField && SwingController._003CWorldZToWorldXImpulseMultiplier_003Ek__BackingField != null)
		{
			Transform transform;
			if (pivot != null)
			{
				transform = pivot;
			}
			else
			{
				Transform transform2 = base.transform;
				transform = transform2;
			}
			Vector3 position = transform.position;
			float num = SwingController._003CWorldZToWorldXImpulseMultiplier_003Ek__BackingField.Evaluate(position.z);
			vector = num * worldXZImpulse;
		}
		else
		{
			vector = worldXZImpulse;
		}
		float num2 = _impulseScaleMul * baseImpulseScale;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj2 = default(object);
		object obj = obj2 ^ 0;
		float num3 = (float)obj * num2;
		float num4 = num2 * worldYTwistImpulse;
		float num5 = num3 + (float)_tiltAngularVel;
		float twistAngularVel = num4 + _twistAngularVel;
		float num6 = num2 * (float)vector;
		_tiltAngularVel = (Vector2)num5;
		float num7 = num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+54]");
		float num8 = num7 + 0f;
		_twistAngularVel = twistAngularVel;
	}

	private unsafe void Update()
	{
		//IL_001c: Invalid comparison between I4 and F4
		//IL_0056: Expected O, but got F4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_0120: Expected O, but got F4
		//IL_0175: Expected O, but got F4
		//IL_0313: Expected O, but got F4
		//IL_0323: Expected F4, but got I
		//IL_0343: Invalid comparison between F4 and I
		//IL_037d: Expected O, but got F4
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_0455: Expected O, but got I
		//IL_048d: Invalid comparison between I4 and F4
		//IL_02af: Invalid comparison between I4 and F4
		//IL_02be: Expected F4, but got I4
		//IL_0260: Expected F4, but got I4
		//IL_0555: Expected F4, but got O
		//IL_04c6: Expected F4, but got I
		//IL_0303: Expected O, but got Ref
		float deltaTime = Time.deltaTime;
		if (!(0f < deltaTime))
		{
			return;
		}
		float num = _dampingMul * baseDamping;
		object obj = stiffness ^ -0f;
		float num2 = num * (float)_tiltAngularVel;
		object obj2 = obj * (object)_tiltAngleDeg;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+4C]");
		object obj3 = obj * 0;
		float num3 = (float)obj2 - num2;
		float num4 = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+54]");
		float num5 = num4 * 0f;
		float num6 = (float)obj3 - num5;
		float num7 = num3 * deltaTime;
		float num8 = maxTiltAngleDegrees ^ -0f;
		float num9 = num7 + (float)_tiltAngularVel;
		float num10 = num6 * deltaTime;
		float num11 = num10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+54]");
		float num12 = num11 + 0f;
		_tiltAngularVel = (Vector2)num9;
		float num13 = num9 * deltaTime;
		float num14 = num13 + (float)_tiltAngleDeg;
		float num15 = num12 * deltaTime;
		float num16 = num15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+4C]");
		float num17 = num16 + 0f;
		_tiltAngleDeg = (Vector2)num14;
		if (!(num8 > num14))
		{
			if (num14 > maxTiltAngleDegrees)
			{
				num14 = maxTiltAngleDegrees;
			}
		}
		else
		{
			num14 = num8;
		}
		_tiltAngleDeg = (Vector2)num14;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+4C]");
		float num18 = 0f;
		float num19 = maxTiltAngleDegrees ^ -0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+4C]");
		if (!(num19 > 0f))
		{
			if (num18 > maxTiltAngleDegrees)
			{
				num18 = maxTiltAngleDegrees;
			}
		}
		else
		{
			num18 = num19;
		}
		float num20 = num * _twistAngularVel;
		object obj4 = stiffness ^ -0f;
		object obj5 = obj4 * _twistAngleDeg;
		float num21 = maxTwistAngleDegrees ^ -0f;
		float num22 = (float)obj5 - num20;
		float num23 = num22 * deltaTime;
		float num24 = (_twistAngularVel = num23 + _twistAngularVel) * deltaTime;
		float num25 = num24 + _twistAngleDeg;
		if (!(num21 > num25))
		{
			if (num25 > maxTwistAngleDegrees)
			{
				num25 = maxTwistAngleDegrees;
			}
		}
		else
		{
			num25 = num21;
		}
		_twistAngleDeg = num25;
		_previousMotionMagnitude = _motionMagnitude;
		object obj6 = _tiltAngularVel * _tiltAngularVel;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+54]");
		nint num26 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+54]");
		object obj7 = num26 * 0;
		float num27 = _twistAngularVel * _twistAngularVel;
		object obj8 = obj6 + obj7;
		float num28 = (float)obj8 + num27;
		float num29;
		if (!(0f > num28))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm2\"");
			num29 = 0f;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			num29 = num28;
		}
		_motionMagnitude = num29;
		float num30 = num29 - _motionMagnitude;
		float num31 = num30 / deltaTime;
		bool flag = 0f > num31;
		float motionSpikePerSecond = 0f;
		if (!flag)
		{
			motionSpikePerSecond = num31;
		}
		_motionSpikePerSecond = motionSpikePerSecond;
		Vector3 axis = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_AngleAxis((float)_tiltAngleDeg, ref axis);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SwingReceiver)+4C]");
		Quaternion quaternion2 = Quaternion.Internal_AngleAxis(0f, ref axis);
		Quaternion quaternion3 = Quaternion.Internal_AngleAxis(_twistAngleDeg, ref axis);
		if (!useCapturedRestRotation)
		{
			Quaternion rotation = pivot.rotation;
		}
		float num32 = default(float);
		pivot.rotation = (Quaternion)(&num32);
	}

	private void CaptureCurrentAsRestRotation()
	{
		//IL_0065: Expected O, but got F4
		if (pivot == null)
		{
			Transform transform = base.transform;
			pivot = transform;
		}
		_restWorldRotation = (Quaternion)pivot.rotation.x;
	}

	private void ResetSwingState()
	{
		//IL_0013: Expected I, but got O
		//IL_004e: Expected I, but got O
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		_tiltAngleDeg = Vector2.zeroVector;
		nint num3 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v6 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num4 = 0;
		_tiltAngularVel = Vector2.zeroVector;
		_twistAngleDeg = 0f;
		_previousMotionMagnitude = 0f;
		_motionSpikePerSecond = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
	}
}
