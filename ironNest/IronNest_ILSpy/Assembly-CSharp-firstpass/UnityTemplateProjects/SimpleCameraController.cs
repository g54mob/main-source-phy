using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace UnityTemplateProjects;

public class SimpleCameraController : MonoBehaviour
{
	private class CameraState
	{
		public float yaw;

		public float pitch;

		public float roll;

		public float x;

		public float y;

		public float z;

		public void SetFromTransform(Transform t)
		{
			pitch = t.eulerAngles.x;
			yaw = t.eulerAngles.y;
			roll = t.eulerAngles.z;
			x = t.position.x;
			y = t.position.y;
			z = t.position.z;
		}

		public unsafe void Translate(Vector3 translation)
		{
			//IL_001a: Expected O, but got Ref
			//IL_001a: Expected O, but got Ref
			Vector3 euler = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			object obj = default(object);
			object obj2 = default(object);
			Vector3 vector = (Quaternion)(&obj) * (Vector3)(&obj2);
			float num = vector.x + x;
			object obj3 = default(object);
			float num2 = (float)obj3 + y;
			x = num;
			float num3 = vector.z + z;
			y = num2;
			z = num3;
		}

		public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
		{
			//IL_0009: Invalid comparison between I4 and F4
			//IL_005e: Expected F4, but got I4
			//IL_01f6: Invalid comparison between I4 and F4
			//IL_00a4: Expected F4, but got I4
			//IL_0253: Invalid comparison between I4 and F4
			//IL_00e0: Expected F4, but got I4
			//IL_02b0: Invalid comparison between I4 and F4
			//IL_0126: Expected F4, but got I4
			//IL_030d: Invalid comparison between I4 and F4
			//IL_016c: Expected F4, but got I4
			//IL_036a: Invalid comparison between I4 and F4
			//IL_01a8: Expected F4, but got I4
			float num2 = default(float);
			float num = ((0f > num2) ? 0f : ((num2 > 1f) ? 1f : num2));
			float num3 = target.yaw - yaw;
			float num4 = num3 * num;
			float num5 = num4 + yaw;
			yaw = num5;
			float num6 = ((0f > num2) ? 0f : ((num2 > 1f) ? 1f : num2));
			float num7 = target.pitch - pitch;
			float num8 = num7 * num6;
			float num9 = num8 + pitch;
			pitch = num9;
			if (!(0f > num2))
			{
				if (num2 > 1f)
				{
					num2 = 1f;
				}
			}
			else
			{
				num2 = 0f;
			}
			float num10 = target.roll - roll;
			float num11 = num10 * num2;
			float num12 = num11 + roll;
			roll = num12;
			float num14 = default(float);
			float num13 = ((0f > num14) ? 0f : ((num14 > 1f) ? 1f : num14));
			float num15 = target.x - x;
			float num16 = num15 * num13;
			float num17 = num16 + x;
			x = num17;
			float num18 = ((0f > num14) ? 0f : ((num14 > 1f) ? 1f : num14));
			float num19 = target.y - y;
			float num20 = num19 * num18;
			float num21 = num20 + y;
			y = num21;
			if (!(0f > num14))
			{
				if (num14 > 1f)
				{
					num14 = 1f;
				}
			}
			else
			{
				num14 = 0f;
			}
			float num22 = target.z - z;
			float num23 = num22 * num14;
			float num24 = num23 + z;
			z = num24;
		}

		public unsafe void UpdateTransform(Transform t)
		{
			//IL_000d: Expected O, but got Ref
			//IL_001a: Expected O, but got Ref
			object obj = default(object);
			t.eulerAngles = (Vector3)(&obj);
			t.position = (Vector3)(&obj);
		}
	}

	private CameraState m_TargetCameraState;

	private CameraState m_InterpolatingCameraState;

	public float boost;

	public float positionLerpTime;

	public AnimationCurve mouseSensitivityCurve;

	public float rotationLerpTime;

	public bool invertY;

	private void OnEnable()
	{
		Transform fromTransform = base.transform;
		m_TargetCameraState.SetFromTransform(fromTransform);
		Transform fromTransform2 = base.transform;
		m_InterpolatingCameraState.SetFromTransform(fromTransform2);
	}

	private unsafe Vector3 GetInputTranslationDirection()
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0017: Expected native int or pointer, but got O
		//IL_00d2: Expected I, but got O
		//IL_010c: Expected native int or pointer, but got O
		//IL_0119: Expected native int or pointer, but got O
		//IL_0158: Expected I, but got O
		//IL_0192: Expected native int or pointer, but got O
		//IL_019f: Expected native int or pointer, but got O
		//IL_01de: Expected I, but got O
		//IL_0218: Expected native int or pointer, but got O
		//IL_0225: Expected native int or pointer, but got O
		//IL_0264: Expected I, but got O
		//IL_029e: Expected native int or pointer, but got O
		//IL_02ab: Expected native int or pointer, but got O
		//IL_02ea: Expected I, but got O
		//IL_0324: Expected native int or pointer, but got O
		//IL_0331: Expected native int or pointer, but got O
		//IL_034e: Expected I, but got O
		//IL_0382: Expected native int or pointer, but got O
		//IL_038f: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		float x = default(float);
		if (Input.GetKeyInt(KeyCode.W))
		{
			_ = vector.x;
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v31 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			float num3 = vector.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rcx_v29 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
			float z = num3 + 0f;
			_ = Vector3.forwardVector;
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z;
		}
		if (Input.GetKeyInt(KeyCode.S))
		{
			_ = vector.x;
			nint num4 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v28 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			float num6 = vector.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rcx_v26 (Il2CppStaticFields<UnityEngine.Vector3>)+5C]");
			float z2 = num6 + 0f;
			_ = Vector3.backVector;
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z2;
		}
		if (Input.GetKeyInt(KeyCode.A))
		{
			_ = vector.x;
			nint num7 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rax_v25 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num8 = 0;
			float num9 = vector.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rcx_v23 (Il2CppStaticFields<UnityEngine.Vector3>)+38]");
			float z3 = num9 + 0f;
			_ = Vector3.leftVector;
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z3;
		}
		if (Input.GetKeyInt(KeyCode.D))
		{
			_ = vector.x;
			nint num10 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rax_v22 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num11 = 0;
			float num12 = vector.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ rcx_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
			float z4 = num12 + 0f;
			_ = Vector3.rightVector;
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z4;
		}
		if (Input.GetKeyInt(KeyCode.Q))
		{
			_ = vector.x;
			nint num13 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v380 @ rax_v19 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num14 = 0;
			float num15 = vector.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ rcx_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+2C]");
			float z5 = num15 + 0f;
			_ = Vector3.downVector;
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z5;
		}
		if (Input.GetKeyInt(KeyCode.E))
		{
			_ = vector.x;
			nint num16 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v453 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num17 = 0;
			float num18 = vector.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v451 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			float z6 = num18 + 0f;
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z6;
		}
		return vector;
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00e0: Expected O, but got Ref
		//IL_025d: Expected O, but got Ref
		//IL_0270: Expected O, but got Ref
		//IL_011d: Expected I4, but got I8
		//IL_03fc: Expected O, but got Ref
		//IL_0426: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39D90]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_ = 0;
		if (Input.GetMouseButtonDown(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetMouseButtonUp(1))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		if (Input.GetMouseButton(1))
		{
			float axis = Input.GetAxis("Mouse X");
			float axis2 = Input.GetAxis("Mouse Y");
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
			bool flag = invertY;
			int num = 1;
			if (!flag)
			{
				num = -1;
			}
			float num2 = (float)num * axis2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			float num3 = mouseSensitivityCurve.Evaluate(axis2);
			CameraState targetCameraState = m_TargetCameraState;
			float num4 = axis * num3;
			float yaw = num4 + targetCameraState.yaw;
			targetCameraState.yaw = yaw;
			CameraState targetCameraState2 = m_TargetCameraState;
			float num5 = num2 * num3;
			float pitch = num5 + targetCameraState2.pitch;
			targetCameraState2.pitch = pitch;
		}
		Vector3 inputTranslationDirection = GetInputTranslationDirection();
		float deltaTime = Time.deltaTime;
		_ = inputTranslationDirection.x;
		float num6 = inputTranslationDirection.z * deltaTime;
		if (Input.GetKeyInt(KeyCode.LeftShift))
		{
			num6 *= 10f;
		}
		Vector2 mouseScrollDelta = Input.mouseScrollDelta;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7B]");
		float num7 = 0f * 0.2f;
		float num8 = num7 + boost;
		boost = num8;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		CameraState targetCameraState3 = m_TargetCameraState;
		float num9 = 2f * num6;
		ref Vector3 euler = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		float num10 = targetCameraState3.pitch * ((float)Math.PI / 180f);
		float num11 = targetCameraState3.roll * ((float)Math.PI / 180f);
		float num12 = targetCameraState3.yaw * ((float)Math.PI / 180f);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		Quaternion quaternion2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = quaternion.x;
		Vector3 vector2 = quaternion2 * vector;
		float x = vector2.x + targetCameraState3.x;
		_ = vector2.x;
		object obj4 = default(object);
		float y = (float)obj4 + targetCameraState3.y;
		targetCameraState3.x = x;
		float z = vector2.z + targetCameraState3.z;
		targetCameraState3.y = y;
		targetCameraState3.z = z;
		float deltaTime2 = Time.deltaTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FAE0");
		float num13 = 0.00999999f / positionLerpTime;
		float num14 = deltaTime2 * num13;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float positionLerpPct = 1f - num14;
		float deltaTime3 = Time.deltaTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FAE0");
		float num15 = 0.00999999f / rotationLerpTime;
		float num16 = deltaTime3 * num15;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float rotationLerpPct = 1f - num16;
		m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);
		CameraState interpolatingCameraState = m_InterpolatingCameraState;
		Transform transform = base.transform;
		Vector3 eulerAngles = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		_ = interpolatingCameraState.roll;
		transform.eulerAngles = eulerAngles;
		Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		_ = interpolatingCameraState.z;
		transform.position = position;
	}

	public SimpleCameraController()
	{
		CameraState targetCameraState = new CameraState();
		m_TargetCameraState = targetCameraState;
		m_InterpolatingCameraState = new CameraState();
		boost = 3.5f;
		positionLerpTime = 0.2f;
		Keyframe[] keys = new Keyframe[2];
		float outTangent = default(float);
		Keyframe keyframe = new Keyframe(0f, 0.5f, 0f, outTangent);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(1f, 2.5f, 0f, outTangent);
		_ = 0;
		_ = 0;
		_ = 0;
		mouseSensitivityCurve = new AnimationCurve(keys);
		rotationLerpTime = 0.01f;
		base._002Ector();
	}
}
