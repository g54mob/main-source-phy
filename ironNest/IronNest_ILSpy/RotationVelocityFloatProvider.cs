using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class RotationVelocityFloatProvider : MonoBehaviour, IFloatValueProvider
{
	public enum AngularVelocitySource
	{
		Rigidbody3D,
		Rigidbody2D,
		TransformDelta
	}

	public enum Axis
	{
		X,
		Y,
		Z
	}

	public Transform target;

	public AngularVelocitySource source = AngularVelocitySource.TransformDelta;

	public Axis axis = Axis.Y;

	public bool useWorldAxis;

	public float minSpeed;

	public float maxSpeed = 90f;

	public bool enableSmoothing = true;

	public float smoothing = 30f;

	public bool enableMicroValueClamp = true;

	public int microValuePrecisionDecimals = 4;

	public float rotationVelocityRaw;

	public float rotationVelocityNormalized;

	public bool logWarnings = true;

	private Transform _effectiveTarget;

	private Rigidbody _rb3D;

	private Rigidbody2D _rb2D;

	private Quaternion _prevRotation;

	private bool _hadPrevRotation;

	private float _microClampThreshold;

	private void Awake()
	{
		//IL_0070: Expected I, but got O
		Transform effectiveTarget = ((!(target != null)) ? base.transform : target);
		_effectiveTarget = effectiveTarget;
		bool flag = _effectiveTarget != null;
		nint num = unchecked((nint)null);
		if (flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Rigidbody rb3D = default(Rigidbody);
			_rb3D = rb3D;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Rigidbody2D rb2D = default(Rigidbody2D);
			_rb2D = rb2D;
			num = 0;
		}
		ResetRotationHistory();
		int num2 = microValuePrecisionDecimals;
		if (microValuePrecisionDecimals >= 0)
		{
			if (num2 > 8)
			{
				num2 = 8;
			}
		}
		else
		{
			num2 = 0;
		}
		int num3 = -num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		_microClampThreshold = 10f;
	}

	private void OnEnable()
	{
		ResetRotationHistory();
		int num = microValuePrecisionDecimals;
		if (microValuePrecisionDecimals >= 0)
		{
			if (num > 8)
			{
				num = 8;
			}
		}
		else
		{
			num = 0;
		}
		int num2 = -num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		_microClampThreshold = 10f;
	}

	private void OnValidate()
	{
		int num = microValuePrecisionDecimals;
		if (microValuePrecisionDecimals >= 0)
		{
			if (num > 8)
			{
				num = 8;
			}
		}
		else
		{
			num = 0;
		}
		int num2 = -num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		_microClampThreshold = 10f;
	}

	private void Update()
	{
		//IL_002b: Expected O, but got I
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_006c: Invalid comparison between F4 and O
		//IL_01b7: Expected O, but got I4
		//IL_0312: Expected F4, but got I4
		//IL_008c: Expected F4, but got I4
		//IL_0144: Expected F4, but got I4
		//IL_01e4: Expected O, but got I
		//IL_01ed: Expected O, but got I4
		//IL_02c3: Invalid comparison between I4 and F4
		//IL_02dc: Expected O, but got I4
		//IL_00f9: Invalid comparison between I4 and F4
		//IL_0204: Invalid comparison between F4 and I4
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_027c: Invalid comparison between F4 and O
		//IL_028b: Expected F4, but got I4
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Expected O, but got Unknown
		//IL_0360: Invalid comparison between I4 and F4
		//IL_016e: Expected O, but got I4
		//IL_0260: Expected F4, but got I4
		//IL_0224: Expected F4, but got I4
		//IL_0395: Expected O, but got I
		//IL_0198: Expected O, but got I4
		float num = ComputeAngularSpeed();
		bool flag = !enableMicroValueClamp;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = 0;
		rotationVelocityRaw = num;
		float microClampThreshold;
		if (!flag)
		{
			microClampThreshold = _microClampThreshold;
			float num2 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = num2 & 0;
			float microClampThreshold2 = _microClampThreshold;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)microClampThreshold2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
			{
				num = 0f;
			}
			rotationVelocityRaw = num;
		}
		float num5;
		if (minSpeed < maxSpeed)
		{
			bool flag2 = minSpeed == maxSpeed;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018055304Bh\"");
			if (!flag2)
			{
				float num3 = rotationVelocityRaw - minSpeed;
				float num4 = maxSpeed - minSpeed;
				num5 = num3 / num4;
				if (!(0f > num5))
				{
					if (num5 > 1f)
					{
						num5 = 1f;
					}
					goto IL_02ba;
				}
			}
			num5 = 0f;
			goto IL_02ba;
		}
		bool flag3 = !logWarnings;
		object obj3 = 0;
		if (!flag3)
		{
			Debug.LogWarning("[RotationVelocityFloatProvider] Invalid normalization range (max <= min). Returning 0.");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			obj = 0;
			obj3 = 0;
		}
		goto IL_0309;
		IL_02ba:
		bool flag4 = 0f > num5;
		microClampThreshold = minSpeed;
		obj3 = 0;
		if (!flag4)
		{
			bool flag5 = !(num5 > 1f);
			microClampThreshold = minSpeed;
			obj3 = 0;
			if (!flag5)
			{
				num5 = 1f;
				microClampThreshold = minSpeed;
				obj3 = 0;
			}
			goto IL_02ea;
		}
		goto IL_0309;
		IL_0309:
		num5 = 0f;
		goto IL_02ea;
		IL_02ea:
		if (enableSmoothing)
		{
			float num6 = Time.deltaTime;
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			float num7 = smoothing;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj4 = num7 ^ 0;
			float num8 = (float)obj4 * num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num9 = 1f - num8;
			if (!(0f > num9))
			{
				if (num9 > 1f)
				{
					num9 = 1f;
				}
			}
			else
			{
				num9 = 0f;
			}
			float num10 = num5 - rotationVelocityNormalized;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			obj = 0;
			float num11 = num10 * num9;
			num5 = num11 + rotationVelocityNormalized;
		}
		rotationVelocityNormalized = num5;
		if (enableMicroValueClamp)
		{
			object obj5 = num5 & obj;
			float microClampThreshold3 = _microClampThreshold;
			bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)microClampThreshold3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
			float num12 = 0f;
			if (!flag6)
			{
				num12 = num5;
			}
			rotationVelocityNormalized = num12;
		}
	}

	public float GetFloatValue()
	{
		return rotationVelocityNormalized;
	}

	private void EnsureEffectiveTarget()
	{
		Transform effectiveTarget = ((!(target != null)) ? base.transform : target);
		_effectiveTarget = effectiveTarget;
	}

	private void CacheTargetRigidbodies()
	{
		if (_effectiveTarget != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Rigidbody rb3D = default(Rigidbody);
			_rb3D = rb3D;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Rigidbody2D rb2D = default(Rigidbody2D);
			_rb2D = rb2D;
		}
	}

	private void ResetRotationHistory()
	{
		//IL_0057: Expected O, but got F4
		bool flag = _effectiveTarget != null;
		if (!flag)
		{
			_hadPrevRotation = flag;
			return;
		}
		Quaternion rotation = _effectiveTarget.rotation;
		_hadPrevRotation = true;
		_prevRotation = (Quaternion)rotation.x;
	}

	private unsafe float ComputeAngularSpeed()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0744: Expected F4, but got I4
		//IL_098a: Expected F4, but got I4
		//IL_05ee: Expected F4, but got I4
		//IL_07a4: Expected O, but got I4
		//IL_0065: Expected F4, but got I4
		//IL_0825: Expected O, but got I
		//IL_0835: Expected O, but got I
		//IL_0852: Expected O, but got I
		//IL_086f: Expected O, but got I
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Expected F4, but got Unknown
		//IL_0b8d: Expected O, but got Ref
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Expected F4, but got Unknown
		//IL_0903: Expected F4, but got I4
		//IL_090c: Expected F4, but got I4
		//IL_0915: Expected F4, but got I4
		//IL_0bc3: Expected I, but got O
		//IL_0bec: Expected F4, but got I
		//IL_07df: Expected O, but got Ref
		//IL_066c: Expected O, but got Ref
		//IL_067c: Expected I4, but got O
		//IL_0a10: Expected O, but got I4
		//IL_0a19: Expected O, but got I4
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		//IL_03b3: Expected O, but got I
		//IL_03c3: Expected O, but got I
		//IL_03e0: Expected O, but got I
		//IL_03fd: Expected O, but got I
		//IL_0a35: Expected O, but got Ref
		//IL_0491: Expected F4, but got I4
		//IL_049a: Expected F4, but got I4
		//IL_04a3: Expected F4, but got I4
		//IL_0a6b: Expected I, but got O
		//IL_0a94: Expected F4, but got I
		//IL_0364: Expected O, but got Ref
		//IL_0394: Expected O, but got I4
		//IL_0c99: Expected O, but got Ref
		//IL_0515: Expected I, but got O
		//IL_0535: Expected F4, but got I
		//IL_0b29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Expected O, but got Unknown
		//IL_0b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Expected O, but got Unknown
		//IL_0b55: Invalid comparison between F4 and I4
		//IL_0598: Expected O, but got F4
		//IL_05a1: Expected F4, but got I4
		//IL_0567: Expected O, but got F4
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Expected F4, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		Quaternion rotation;
		object obj3 = default(object);
		object message2;
		if (source != AngularVelocitySource.Rigidbody3D)
		{
			if (source != AngularVelocitySource.Rigidbody2D)
			{
				if (_hadPrevRotation)
				{
					bool flag = (object)_effectiveTarget == null;
					float num = 0f;
					if (flag)
					{
						goto IL_098f;
					}
					rotation = _effectiveTarget.rotation;
					ref Quaternion rotation2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					_ = _prevRotation;
					Quaternion quaternion = Quaternion.Internal_Inverse(ref rotation2);
					ref float angle = ref System.Runtime.CompilerServices.Unsafe.As<object, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					ref Vector3 reference = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
					ref Quaternion q = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
					float num2 = (float)obj3 * quaternion.x;
					float num3 = rotation.x * (float)obj3;
					float num4 = num3 + num2;
					object obj4 = obj3 * obj3;
					object obj5 = obj3 * obj3;
					float num5 = num4 + (float)obj4;
					object obj6 = obj3 * obj3;
					object obj7 = obj3 * obj3;
					float num6 = num5 - (float)obj7;
					object obj8 = obj3 * obj3;
					object obj9 = obj5 + obj8;
					float num7 = rotation.x * (float)obj3;
					float num8 = (float)obj3 * quaternion.x;
					object obj10 = obj3 * obj3;
					float num9 = (float)obj9 + num8;
					float num10 = rotation.x * (float)obj3;
					float num11 = num9 - num7;
					object obj11 = obj3 * obj3;
					object obj12 = obj3 * obj3;
					object obj13 = obj6 + obj11;
					object obj14 = obj3 * obj3;
					float num12 = (float)obj3 * quaternion.x;
					float num13 = (float)obj13 + num10;
					float num14 = num13 - num12;
					float num15 = rotation.x * quaternion.x;
					float num16 = (float)obj12 - num15;
					float num17 = num16 - (float)obj14;
					float num18 = num17 - (float)obj10;
					Quaternion.Internal_ToAxisAngleRad(ref q, out reference, out angle);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
					num = 0f * 57.29578f;
					object obj15 = num & -2147483649L;
					if ((nint)obj15 <= 2139095040)
					{
						object obj16 = num & -2147483649L;
						if ((nint)obj16 != 2139095040)
						{
							goto IL_09e9;
						}
					}
					_ = 0;
					goto IL_09e9;
				}
				ResetRotationHistory();
			}
			else if (_rb2D != null)
			{
				bool flag2 = (object)_rb2D == null;
				float num = 0f;
				if (flag2)
				{
					goto IL_098f;
				}
				float angularVelocity = _rb2D.angularVelocity;
				bool flag3 = axis == Axis.Z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float result = angularVelocity & 0;
				if (!flag3 && logWarnings)
				{
					object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
					_ = axis;
					object arg = (Axis)obj17;
					string message = string.Format("[{0}] Rigidbody2D rotates about world Z; axis '{1}' selection is effectively Z.", "RotationVelocityFloatProvider", arg);
					Debug.LogWarning(message);
				}
				if (flag3)
				{
					return result;
				}
			}
			else if (logWarnings)
			{
				message2 = "[RotationVelocityFloatProvider] Rigidbody2D source selected but no Rigidbody2D on target.";
				goto IL_0b71;
			}
		}
		else
		{
			if (_rb3D != null)
			{
				bool flag4 = (object)_rb3D == null;
				float num = 0f;
				if (!flag4)
				{
					Vector3 angularVelocity2 = _rb3D.angularVelocity;
					num = angularVelocity2.x;
					_ = angularVelocity2.x;
					Vector3 vector = GetAxisVector();
					bool flag5 = useWorldAxis;
					Vector3 vector2 = (Vector3)0;
					if (!flag5)
					{
						if ((object)_effectiveTarget == null)
						{
							goto IL_098f;
						}
						vector2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
						_ = vector.x;
						_ = vector.z;
						vector = _effectiveTarget.TransformDirection(vector2);
					}
					_ = vector.x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-79]");
					Vector3 vector3 = (Vector3)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-75]");
					object obj18 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-75]");
					nint num19 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-75]");
					object obj19 = num19 * 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-79]");
					nint num20 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-79]");
					object obj20 = num20 * 0;
					float num21 = vector.z * vector.z;
					object obj21 = obj19 + obj20;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-79]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-75]");
					_ = 0;
					_ = vector.z;
					float num22 = (float)obj21 + num21;
					bool flag6 = !(1E-06f > num22);
					float num23 = vector.z;
					if (!flag6)
					{
						nint num24 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v824 @ rax_v19 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num25 = 0;
						vector3 = Vector3.forwardVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v826 @ rcx_v18 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
						num23 = 0f;
						_ = Vector3.forwardVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v826 @ rcx_v18 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
						_ = 0;
						obj18 = obj3;
					}
					object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					float num26;
					float num27;
					float num28;
					if (!(1E-06f > 1E-05f))
					{
						num26 = 0f;
						num27 = 0f;
						num28 = 0f;
					}
					else
					{
						num26 = (float)vector3 / 1E-06f;
						num27 = (float)obj18 / 1E-06f;
						num28 = num23 / 1E-06f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-45]");
					float num29 = 0f * num27;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
					float num30 = 0f * num26;
					float num31 = angularVelocity2.z * num28;
					float num32 = num29 + num30;
					float num33 = num32 + num31;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					return num33 & 0;
				}
				goto IL_098f;
			}
			if (logWarnings)
			{
				message2 = "[RotationVelocityFloatProvider] Rigidbody3D source selected but no Rigidbody on target.";
				goto IL_0b71;
			}
		}
		goto IL_0981;
		IL_098f:
		throw new NullReferenceException();
		IL_0b71:
		Debug.LogWarning(message2);
		goto IL_0981;
		IL_09e9:
		Vector3 vector4 = GetAxisVector();
		bool flag7 = useWorldAxis;
		object obj23 = 0;
		Vector3 vector5 = (Vector3)0;
		if (!flag7)
		{
			if ((object)_effectiveTarget == null)
			{
				goto IL_098f;
			}
			vector5 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
			_ = vector4.x;
			_ = vector4.z;
			vector4 = _effectiveTarget.TransformDirection(vector5);
			obj23 = 0;
		}
		_ = vector4.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-69]");
		Vector3 vector6 = (Vector3)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-65]");
		object obj24 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-65]");
		nint num34 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-65]");
		object obj25 = num34 * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-69]");
		nint num35 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-69]");
		object obj26 = num35 * 0;
		float num36 = vector4.z * vector4.z;
		object obj27 = obj25 + obj26;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-69]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-65]");
		_ = 0;
		_ = vector4.z;
		float num37 = (float)obj27 + num36;
		bool flag8 = !(1E-06f > num37);
		float num38 = vector4.z;
		if (!flag8)
		{
			nint num39 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v939 @ rax_v55 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num40 = 0;
			vector6 = Vector3.forwardVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v941 @ rcx_v48 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
			num38 = 0f;
			_ = Vector3.forwardVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v941 @ rcx_v48 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
			_ = 0;
			obj24 = obj3;
		}
		object obj28 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num41;
		float num42;
		float num43;
		if (!(1E-06f > 1E-05f))
		{
			num41 = 0f;
			num42 = 0f;
			num43 = 0f;
		}
		else
		{
			num41 = (float)vector6 / 1E-06f;
			num42 = (float)obj24 / 1E-06f;
			num43 = num38 / 1E-06f;
		}
		object obj29 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num45;
		if (1E-06f > 1E-05f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-51]");
			float num44 = 0f / 1E-06f;
			num45 = num44;
		}
		else
		{
			nint num46 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1036 @ rax_v50 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num47 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1037 @ rcx_v45 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num45 = 0f;
			_ = Vector3.zeroVector;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-65]");
		float num48 = 0f * num42;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-69]");
		float num49 = 0f * num41;
		float num50 = num45 * num43;
		float num51 = num48 + num49;
		float num52 = num51 + num50;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj30 = num52 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
		object obj31 = obj30 * 0;
		float deltaTime = Time.deltaTime;
		if (deltaTime > 0f)
		{
			float deltaTime2 = Time.deltaTime;
			float num53 = (float)obj31 / deltaTime2;
			_prevRotation = (Quaternion)rotation.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			return num53 & 0;
		}
		_prevRotation = (Quaternion)rotation.x;
		return 0f;
		IL_0981:
		return 0f;
	}

	private unsafe Vector3 GetAxisVector()
	{
		//IL_00d6: Expected I, but got O
		//IL_00f4: Expected F4, but got O
		//IL_00ef: Expected native int or pointer, but got O
		//IL_0109: Expected F4, but got I
		//IL_0104: Expected native int or pointer, but got O
		//IL_0090: Expected I, but got O
		//IL_00ae: Expected F4, but got O
		//IL_00a9: Expected native int or pointer, but got O
		//IL_00c3: Expected F4, but got I
		//IL_00be: Expected native int or pointer, but got O
		//IL_004a: Expected I, but got O
		//IL_0068: Expected F4, but got O
		//IL_0063: Expected native int or pointer, but got O
		//IL_007d: Expected F4, but got I
		//IL_0078: Expected native int or pointer, but got O
		bool flag = axis == Axis.X;
		Vector3 vector = default(Vector3);
		if (!flag)
		{
			if (!flag)
			{
				nint num = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num2 = 0;
				((Vector3*)(nint)vector)->x = (float)Vector3.forwardVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			((Vector3*)(nint)vector)->x = (float)Vector3.upVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		nint num5 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num6 = 0;
		((Vector3*)(nint)vector)->x = (float)Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	private float Normalize(float speed)
	{
		//IL_0146: Expected F4, but got I4
		//IL_00a2: Invalid comparison between I4 and F4
		//IL_00b1: Expected O, but got I4
		//IL_0177: Expected F4, but got I4
		//IL_003b: Expected O, but got I4
		//IL_0184: Invalid comparison between O and F4
		//IL_00da: Expected O, but got I4
		//IL_0108: Expected F4, but got I4
		//IL_00f1: Expected O, but got I4
		object obj;
		float num3;
		if (minSpeed < maxSpeed)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180552DF9h\"");
			if (minSpeed == maxSpeed)
			{
				obj = 0;
			}
			else
			{
				float num = maxSpeed - minSpeed;
				float num2 = speed - minSpeed;
				num3 = num2 / num;
				bool flag = 0f > num3;
				obj = 0;
				if (!flag)
				{
					bool flag2 = !(num3 > 1f);
					obj = 0;
					if (!flag2)
					{
						obj = 0;
						num3 = 1f;
					}
					goto IL_017c;
				}
			}
			num3 = 0f;
			goto IL_017c;
		}
		if (logWarnings)
		{
			Debug.LogWarning("[RotationVelocityFloatProvider] Invalid normalization range (max <= min). Returning 0.");
		}
		return 0f;
		IL_017c:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				return 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		return num3;
	}

	private void RecomputeMicroClampThreshold()
	{
		int num = microValuePrecisionDecimals;
		if (microValuePrecisionDecimals >= 0)
		{
			if (num > 8)
			{
				num = 8;
			}
		}
		else
		{
			num = 0;
		}
		int num2 = -num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		_microClampThreshold = 10f;
	}

	private float ClampMicro(float value)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001f: Invalid comparison between F4 and O
		//IL_003c: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = value & 0;
		float microClampThreshold = _microClampThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)microClampThreshold) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			return 0f;
		}
		return value;
	}
}
