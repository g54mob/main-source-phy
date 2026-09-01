using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class MechanicalDraftingArmIKLocalXZ : MonoBehaviour
{
	public enum BendSide
	{
		CounterClockwise,
		Clockwise
	}

	public enum ElbowLevelMode
	{
		None,
		CounterRotateUpperArm,
		MatchBaseWorldZ,
		MatchReferenceWorldZ
	}

	public enum ElbowCounterSource
	{
		UpperArmA,
		UpperArmB
	}

	public enum TargetMatchAxes
	{
		LocalXOnly,
		LocalYOnly,
		LocalXAndY
	}

	private Transform baseTransform;

	private Transform upperArmA;

	private Transform upperArmB;

	private Transform elbow;

	private Transform forearmA;

	private Transform forearmB;

	private Transform target;

	private TargetMatchAxes targetMatchAxes;

	private float length1 = 1f;

	private float length2 = 1f;

	private bool autoComputeLengthsFromSetupPose = true;

	private BendSide bendSide;

	private bool clampToReach = true;

	private float epsilon = 1E-05f;

	private ElbowLevelMode elbowLevelMode = ElbowLevelMode.CounterRotateUpperArm;

	private ElbowCounterSource elbowCounterSource;

	private float elbowCounterOffsetDeg;

	private Transform elbowLevelReference;

	private bool solveInLateUpdate = true;

	private unsafe void OnValidate()
	{
		//IL_00c6: Expected O, but got Ref
		//IL_00f3: Expected O, but got Ref
		bool isPlaying = Application.isPlaying;
		if (!isPlaying && autoComputeLengthsFromSetupPose != isPlaying && baseTransform != null && elbow != null && target != null)
		{
			Vector3 position = elbow.position;
			float num = default(float);
			Vector3 vector = baseTransform.InverseTransformPoint((Vector3)(&num));
			Vector3 position2 = target.position;
			Vector3 vector2 = baseTransform.InverseTransformPoint((Vector3)(&num));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			length1 = position2.x;
			object obj2 = default(object);
			object obj = obj2 - obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			length2 = position2.x;
		}
	}

	private void Update()
	{
		if (!solveInLateUpdate)
		{
			Solve();
		}
	}

	private void LateUpdate()
	{
		if (solveInLateUpdate)
		{
			Solve();
		}
	}

	private unsafe void TryAutoComputeLengths()
	{
		//IL_008c: Expected O, but got Ref
		//IL_00b9: Expected O, but got Ref
		if (baseTransform != null && elbow != null && target != null)
		{
			Vector3 position = elbow.position;
			float num = default(float);
			Vector3 vector = baseTransform.InverseTransformPoint((Vector3)(&num));
			Vector3 position2 = target.position;
			Vector3 vector2 = baseTransform.InverseTransformPoint((Vector3)(&num));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			length1 = position2.x;
			object obj2 = default(object);
			object obj = obj2 - obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			length2 = position2.x;
		}
	}

	private unsafe void Solve()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00d9: Expected O, but got Ref
		//IL_0172: Expected O, but got I4
		//IL_03d2: Expected O, but got I
		//IL_0139: Expected O, but got I
		//IL_03ea: Expected O, but got Ref
		//IL_0427: Expected O, but got I4
		//IL_0164: Expected O, but got I4
		//IL_0507: Invalid comparison between I4 and F4
		//IL_0519: Expected F4, but got I4
		//IL_0192: Expected O, but got Ref
		//IL_01af: Expected O, but got I4
		//IL_0560: Invalid comparison between I4 and F4
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_0310: Expected O, but got Ref
		//IL_0398: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		if (!(baseTransform != null) || !(upperArmA != null) || !(elbow != null) || !(forearmA != null) || !(target != null))
		{
			return;
		}
		Vector3 position = target.position;
		Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		_ = position.x;
		_ = position.z;
		Vector3 vector = baseTransform.InverseTransformPoint(position2);
		_ = vector.x;
		object obj4;
		if (targetMatchAxes != TargetMatchAxes.LocalXOnly)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
			object obj3 = 0;
			if (targetMatchAxes == TargetMatchAxes.LocalYOnly)
			{
				obj4 = 0;
				goto IL_03d7;
			}
		}
		else
		{
			object obj3 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
		obj4 = 0;
		goto IL_03d7;
		IL_03d7:
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
		bool flag = !(epsilon > vector.x);
		float x = vector.x;
		object obj6 = 0;
		if (!flag)
		{
			_ = epsilon;
			_ = 0;
			obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			x = epsilon;
			obj6 = 0;
		}
		bool flag2 = !clampToReach;
		float num = x;
		if (!flag2)
		{
			float num2 = length1 - length2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj7 = num2 & 0;
			float num3 = (float)obj7 + epsilon;
			if (!(num3 > x))
			{
				float num4 = length2 + length1;
				float num5 = num4 - epsilon;
				bool flag3 = !(x > num5);
				num = x;
				if (!flag3)
				{
					num = num5;
				}
			}
			else
			{
				num = num3;
			}
		}
		float num6 = length1 * length1;
		float num7 = length2 * length2;
		float num8 = num6 - num7;
		float num9 = num * num;
		float num10 = num + num;
		float num11 = num8 + num9;
		float num12 = num11 / num10;
		float num13 = num12 * num12;
		float num14 = length1 * length1;
		float num15 = num14 - num13;
		bool flag4 = !(0f < num15);
		float num16 = 0f;
		if (!flag4)
		{
			num16 = num15;
		}
		if (!(0f > num16))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm8\"");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
		}
		if (bendSide == BendSide.CounterClockwise)
		{
		}
		Vector2 vBaseLocal = default(Vector2);
		float desiredZDeg = BaseLocalVectorToWorldZAngleDeg(vBaseLocal);
		float desiredZDeg2 = BaseLocalVectorToWorldZAngleDeg(vBaseLocal);
		SetWorldZAngle(upperArmA, desiredZDeg);
		if (upperArmB != null)
		{
			Quaternion localRotation = upperArmA.localRotation;
			Quaternion localRotation2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = localRotation.x;
			upperArmB.localRotation = localRotation2;
		}
		ApplyElbowLeveling();
		SetWorldZAngle(forearmA, desiredZDeg2);
		if (forearmB != null)
		{
			Quaternion localRotation3 = forearmA.localRotation;
			Quaternion localRotation4 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = localRotation3.x;
			forearmB.localRotation = localRotation4;
		}
	}

	private Vector2 FilterTargetAxes(Vector2 tBaseLocal)
	{
		Vector2 result;
		Vector2 vector = default(Vector2);
		if (targetMatchAxes != TargetMatchAxes.LocalXOnly)
		{
			bool flag = targetMatchAxes != TargetMatchAxes.LocalYOnly;
			result = tBaseLocal;
			if (!flag)
			{
				return vector;
			}
		}
		else
		{
			result = vector;
		}
		return result;
	}

	private unsafe void ApplyElbowLeveling()
	{
		//IL_0015: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected O, but got Unknown
		//IL_01e5: Invalid comparison between I4 and F4
		//IL_0206: Expected O, but got Ref
		bool flag = elbowLevelMode == ElbowLevelMode.None;
		if (flag)
		{
			return;
		}
		object obj = elbowLevelMode - 1;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				if ((nint)obj2 == 1)
				{
					if (elbowLevelReference != null)
					{
						float worldZAngleDeg = GetWorldZAngleDeg(elbowLevelReference);
						SetWorldZAngle(elbow, worldZAngleDeg);
					}
					else
					{
						SetWorldZAngle(elbow, 0f);
					}
				}
			}
			else
			{
				float worldZAngleDeg2 = GetWorldZAngleDeg(baseTransform);
				SetWorldZAngle(elbow, worldZAngleDeg2);
			}
			return;
		}
		Transform transform = upperArmA;
		if (elbowCounterSource == ElbowCounterSource.UpperArmB && upperArmB != null)
		{
			transform = upperArmB;
		}
		float num = MathF.FMod(transform.localEulerAngles.z, 360f);
		bool flag2 = !(num > 180f);
		float num2 = num;
		if (!flag2)
		{
			num2 = num + -360f;
		}
		float num3 = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj3 = num3 ^ 0;
		float x = (float)obj3 + elbowCounterOffsetDeg;
		Vector3 localEulerAngles = elbow.localEulerAngles;
		float num4 = MathF.FMod(x, 360f);
		if (0f > num4)
		{
		}
		float num5 = default(float);
		elbow.localEulerAngles = (Vector3)(&num5);
	}

	private unsafe Vector2 WorldToBaseLocalXY(Vector3 worldPos)
	{
		//IL_002e: Expected O, but got Ref
		if ((object)baseTransform != null)
		{
			object obj = default(object);
			Vector3 vector = baseTransform.InverseTransformPoint((Vector3)(&obj));
			Vector2 result = default(Vector2);
			return result;
		}
		return (Vector2)new NullReferenceException();
	}

	private unsafe float BaseLocalVectorToWorldZAngleDeg(Vector2 vBaseLocal)
	{
		//IL_0042: Invalid comparison between F4 and O
		//IL_00fb: Expected O, but got Ref
		//IL_0095: Expected O, but got Ref
		object obj2 = default(object);
		object obj = obj2 * obj2;
		object obj3 = vBaseLocal * vBaseLocal;
		float num = epsilon * epsilon;
		object obj4 = obj + obj3;
		Quaternion rotation2 = default(Quaternion);
		object obj5 = default(object);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
		{
			Quaternion rotation = baseTransform.rotation;
			float num2 = Quaternion.Internal_ToEulerRad(ref rotation2).z * 57.29578f;
			Vector3 vector = Quaternion.Internal_MakePositive((Vector3)(&obj5));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
			float num3 = (float)obj2 * 57.29578f;
			return num3 + vector.z;
		}
		Quaternion rotation3 = baseTransform.rotation;
		Vector3 vector2 = Quaternion.Internal_ToEulerRad(ref rotation2);
		return Quaternion.Internal_MakePositive((Vector3)(&obj5)).z;
	}

	private unsafe static void SetWorldZAngle(Transform t, float desiredZDeg)
	{
		//IL_0085: Expected F4, but got I4
		//IL_00c1: Invalid comparison between I4 and F4
		//IL_00e0: Expected O, but got Ref
		//IL_0066: Expected O, but got Ref
		Transform parent = t.parent;
		float num;
		object obj = default(object);
		if (parent != null)
		{
			Quaternion rotation = parent.rotation;
			Quaternion rotation2 = default(Quaternion);
			Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation2);
			num = Quaternion.Internal_MakePositive((Vector3)(&obj)).z;
		}
		else
		{
			num = 0f;
		}
		Vector3 localEulerAngles = t.localEulerAngles;
		float x = desiredZDeg - num;
		float num2 = MathF.FMod(x, 360f);
		if (0f > num2)
		{
		}
		t.localEulerAngles = (Vector3)(&obj);
	}

	private unsafe static float GetWorldZAngleDeg(Transform t)
	{
		//IL_0028: Expected O, but got Ref
		Quaternion rotation = t.rotation;
		Quaternion rotation2 = default(Quaternion);
		Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation2);
		object obj = default(object);
		return Quaternion.Internal_MakePositive((Vector3)(&obj)).z;
	}

	private static float NormalizeSignedDegrees(float degUnsigned)
	{
		float num = MathF.FMod(degUnsigned, 360f);
		if (num > 180f)
		{
			num += -360f;
		}
		return num;
	}

	private static float NormalizeUnsignedDegrees(float deg)
	{
		//IL_001b: Invalid comparison between I4 and F4
		float num = MathF.FMod(deg, 360f);
		if (0f > num)
		{
			num += 360f;
		}
		return num;
	}
}
