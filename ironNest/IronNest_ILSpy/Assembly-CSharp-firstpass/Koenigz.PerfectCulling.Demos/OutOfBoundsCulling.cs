using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Koenigz.PerfectCulling.Demos;

public class OutOfBoundsCulling : MonoBehaviour
{
	public Vector3 Margin;

	public Transform CameraTransformReference;

	private PerfectCullingVolume volume;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		PerfectCullingVolume perfectCullingVolume = default(PerfectCullingVolume);
		volume = perfectCullingVolume;
	}

	private unsafe void LateUpdate()
	{
		//IL_0008: Expected O, but got Ref
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_027d: Invalid comparison between O and F4
		//IL_0337: Expected O, but got I4
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Expected O, but got Unknown
		//IL_0372: Expected O, but got I4
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Expected O, but got Unknown
		//IL_02af: Invalid comparison between O and F4
		//IL_0073: Expected O, but got Ref
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_02e1: Invalid comparison between O and F4
		//IL_0300: Invalid comparison between F4 and I4
		//IL_0329: Expected O, but got I4
		//IL_0093: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		if (CameraTransformReference == null)
		{
			List<PerfectCullingCamera> allCameras = PerfectCullingCamera.AllCameras;
			if (allCameras._size <= 0)
			{
				return;
			}
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 80));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
			Transform cameraTransformReference = ((Component)0).transform;
			CameraTransformReference = cameraTransformReference;
		}
		Transform transform = volume.transform;
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		Vector3 pos = default(Vector3);
		Quaternion q = default(Quaternion);
		Vector3 s = default(Vector3);
		Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref s);
		_ = matrix4x.m02;
		_ = matrix4x.m03;
		Matrix4x4 m = default(Matrix4x4);
		Matrix4x4 matrix4x2 = Matrix4x4.Internal_Inverse(ref m);
		Vector3 position2 = CameraTransformReference.position;
		object obj4 = default(object);
		float num = position2.x * (float)obj4;
		object obj6 = default(object);
		object obj5 = obj6 * obj4;
		float num2 = position2.z * (float)obj4;
		float num3 = num + (float)obj5;
		float num4 = position2.x * matrix4x2.m00;
		float num5 = num3 + num2;
		float num6 = (float)obj6 * matrix4x2.m01;
		object obj7 = obj6 * obj4;
		float num7 = num5 + (float)obj4;
		float num8 = position2.z * matrix4x2.m02;
		float num9 = num6 + num4;
		float num10 = position2.x * (float)obj4;
		float num11 = position2.z * (float)obj4;
		float num12 = num9 + num8;
		float num13 = num10 + (float)obj7;
		float num14 = num12 + matrix4x2.m03;
		float num15 = num13 + num11;
		float num16 = num15 + (float)obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj8 = num14 & 0;
		object obj11;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj9 = num7 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj10 = num16 & 0;
				bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f);
				float num17 = (float)obj10 - 0.5f;
				bool flag2 = num17 == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				obj11 = flag4 & flag3;
				goto IL_033c;
			}
		}
		obj11 = 1;
		goto IL_033c;
		IL_033c:
		bool flag5 = volume.enabled;
		object obj12 = flag5 & obj11;
		bool flag6 = obj12 == null;
		object obj13 = !flag6;
		if (obj13 == null)
		{
			if (!flag5 && obj11 == null)
			{
				volume.enabled = true;
			}
		}
		else
		{
			volume.enabled = false;
			volume.QueueToggleAllRenderers(state: false);
			volume.ExecuteQueue();
		}
	}
}
