using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class ClipboardSlotCycler3D : MonoBehaviour
{
	private BoundedDragSurface3D clipboardSurface;

	private List<Vector2> normalizedSlotOffsets;

	private int startingIndex;

	private bool resetIndexOnEnable;

	private bool drawGizmos;

	private bool drawGizmosWhenNotSelected;

	private Color boundsGizmoColor;

	private Color slotGizmoColor;

	private float slotGizmoRadius;

	private bool drawSlotIndexLabels;

	private float gizmoNormalLift;

	private bool debug;

	private int _nextIndex;

	public BoundedDragSurface3D ClipboardSurface => clipboardSurface;

	private void Awake()
	{
		EnsureSurfaceReference();
		_nextIndex = startingIndex;
	}

	private void OnEnable()
	{
		if (resetIndexOnEnable)
		{
			_nextIndex = startingIndex;
		}
	}

	private void OnValidate()
	{
		//IL_0011: Invalid comparison between I4 and F4
		//IL_0053: Invalid comparison between I4 and F4
		EnsureSurfaceReference();
		if (0f > slotGizmoRadius)
		{
			slotGizmoRadius = 0f;
		}
		if (0f > gizmoNormalLift)
		{
			gizmoNormalLift = 0f;
		}
	}

	private void EnsureSurfaceReference()
	{
		if (!clipboardSurface)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			BoundedDragSurface3D boundedDragSurface3D = default(BoundedDragSurface3D);
			clipboardSurface = boundedDragSurface3D;
		}
	}

	public unsafe bool TryGetNextNormalizedOffset(out Vector2 normalizedOffset, out int allocatedIndex)
	{
		//IL_017d: Expected I4, but got O
		ref Vector2 reference = ref *(Vector2*)null;
		ref int reference2 = ref *(int*)4294967295L;
		if (normalizedSlotOffsets != null)
		{
			List<Vector2> list = normalizedSlotOffsets;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				int num;
				if ((nint)0 > (nint)0)
				{
					int nextIndex = _nextIndex;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
					num = (int)((nint)nextIndex % (nint)0);
					if (num < 0)
					{
						int num2 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
						num = (int)((nint)num2 + (nint)0);
					}
				}
				else
				{
					num = 0;
				}
				reference2 = ref *(int*)num;
				if (normalizedSlotOffsets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					object obj = default(object);
					reference = ref *(Vector2*)obj;
					int nextIndex2 = allocatedIndex + 1;
					bool flag = !debug;
					_nextIndex = nextIndex2;
					if (!flag)
					{
						string arg = base.name;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object obj2 = default(object);
						object arg2 = (Vector2)obj2;
						object arg3 = default(object);
						string message = $"[{arg}] Allocated slot index={arg3}, normalizedOffset={arg2}";
						Debug.Log(message, this);
					}
					return true;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
	}

	public unsafe bool TryGetWorldPointOnPlaneFromNormalizedOffset(Vector2 normalizedOffset, out Vector3 worldPointOnPlane)
	{
		//IL_0008: Expected O, but got Ref
		//IL_03ea: Expected I4, but got O
		//IL_0430: Expected F4, but got I4
		//IL_012f: Expected O, but got I4
		//IL_01d1: Expected F4, but got I4
		//IL_01da: Expected F4, but got I4
		//IL_01f3: Expected O, but got Ref
		//IL_0195: Expected F4, but got I4
		//IL_04d0: Expected O, but got Ref
		//IL_02bc: Expected I, but got O
		//IL_02dc: Expected F4, but got I
		//IL_030c: Expected O, but got Ref
		//IL_053b: Expected O, but got I
		//IL_0558: Expected O, but got I
		//IL_05e7: Expected Ref, but got F4
		//IL_036f: Expected F4, but got I4
		//IL_0378: Expected F4, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		ref Vector3 reference = ref *(Vector3*)null;
		_ = 0;
		if (!clipboardSurface)
		{
			goto IL_03ce;
		}
		BoundedDragSurface3D boundedDragSurface3D = clipboardSurface;
		BoundedDragSurface3D boundedDragSurface3D2;
		Transform transform;
		float num;
		float num2;
		Vector3 size4;
		if ((object)clipboardSurface != null)
		{
			if (!boundedDragSurface3D.boundsBox)
			{
				goto IL_03ce;
			}
			boundedDragSurface3D2 = clipboardSurface;
			if ((object)clipboardSurface != null && (object)boundedDragSurface3D2.boundsBox != null)
			{
				transform = boundedDragSurface3D2.boundsBox.transform;
				BoundedDragSurface3D.SurfaceAxis surfaceAxis = InferPlaneNormalAxis(clipboardSurface);
				bool flag = surfaceAxis == BoundedDragSurface3D.SurfaceAxis.Up;
				if (!flag)
				{
					object obj3 = surfaceAxis - 1;
					if (flag)
					{
						Vector3 size = boundedDragSurface3D2.boundsBox.size;
						Vector3 size2 = boundedDragSurface3D2.boundsBox.size;
						num = 0f;
						num2 = 0f;
						goto IL_045e;
					}
					if ((nint)obj3 == 1)
					{
						Vector3 size3 = boundedDragSurface3D2.boundsBox.size;
						size4 = boundedDragSurface3D2.boundsBox.size;
						num2 = 0f;
						goto IL_0435;
					}
				}
				Vector3 size5 = boundedDragSurface3D2.boundsBox.size;
				size4 = boundedDragSurface3D2.boundsBox.size;
				num2 = 0f;
				goto IL_0435;
			}
		}
		goto IL_03dc;
		IL_045e:
		Vector3 center = boundedDragSurface3D2.boundsBox.center;
		_ = center.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-3D]");
		float num4 = default(float);
		float num3 = 0f + num4;
		float num5 = num + center.z;
		if ((object)transform != null)
		{
			Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			Vector3 vector = transform.TransformPoint(position);
			_ = vector.x;
			if ((object)clipboardSurface != null)
			{
				Vector3 planeNormal = clipboardSurface.GetPlaneNormal();
				_ = planeNormal.x;
				_ = planeNormal.z;
				object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 65));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				float num7;
				if (planeNormal.x > 1E-05f)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-3D]");
					num5 = 0f / planeNormal.x;
					float num6 = planeNormal.z / planeNormal.x;
					num3 = num4;
					num7 = num6;
				}
				else
				{
					nint num8 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v576 @ rax_v25 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v577 @ rcx_v21 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					num7 = 0f;
					_ = Vector3.zeroVector;
					float num6 = num4;
				}
				if ((object)clipboardSurface != null)
				{
					Vector3 planeOriginPoint = clipboardSurface.GetPlaneOriginPoint();
					object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
					_ = planeOriginPoint.x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-41]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-3D]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					float num10;
					float num11;
					if (!(planeOriginPoint.x > 1E-05f))
					{
						num10 = 0f;
						num11 = 0f;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-41]");
						num2 = 0f / planeOriginPoint.x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-3D]");
						num11 = 0f / planeOriginPoint.x;
						num10 = num7 / planeOriginPoint.x;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-19]");
					nint num12 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
					object obj6 = num12 - 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-15]");
					nint num13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-5]");
					object obj7 = num13 - 0;
					float num14 = (float)obj6 * num2;
					float num15 = (float)obj7 * num11;
					float num16 = num15 + num14;
					float num17 = vector.z - planeOriginPoint.z;
					float num18 = num17 * num10;
					float num19 = num16 + num18;
					float num20 = num10 * num19;
					float num21 = vector.z - num20;
					reference = ref *(Vector3*)num4;
					return true;
				}
			}
		}
		goto IL_03dc;
		IL_0435:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-45]");
		float num22 = 0f * size4.z;
		num = num22;
		goto IL_045e;
		IL_03ce:
		return false;
		IL_03dc:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void OnDrawGizmos()
	{
		if (drawGizmos && drawGizmosWhenNotSelected)
		{
			DrawGizmosInternal(selected: false);
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (drawGizmos)
		{
			DrawGizmosInternal(selected: true);
		}
	}

	private unsafe void DrawGizmosInternal(bool selected)
	{
		//IL_0008: Expected O, but got Ref
		//IL_009f: Expected O, but got Ref
		//IL_0124: Expected O, but got Ref
		//IL_015b: Expected O, but got Ref
		//IL_015b: Expected O, but got Ref
		//IL_0187: Expected O, but got Ref
		//IL_030c: Expected O, but got Ref
		//IL_032c: Expected O, but got I4
		//IL_0335: Expected O, but got I4
		//IL_0246: Expected O, but got Ref
		//IL_0266: Expected O, but got I
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_0297: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		EnsureSurfaceReference();
		if (!clipboardSurface)
		{
			return;
		}
		BoundedDragSurface3D boundedDragSurface3D = clipboardSurface;
		if (!boundedDragSurface3D.boundsBox)
		{
			return;
		}
		BoundedDragSurface3D boundedDragSurface3D2 = clipboardSurface;
		Transform transform = boundedDragSurface3D2.boundsBox.transform;
		Vector3 pos = default(Vector3);
		Gizmos.color = (Color)(&pos);
		Matrix4x4 matrix = Gizmos.matrix;
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		Vector3 lossyScale = transform.lossyScale;
		Quaternion q = default(Quaternion);
		Vector3 s = default(Vector3);
		Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref s);
		_ = matrix4x.m01;
		_ = matrix4x.m02;
		_ = matrix4x.m03;
		float num = default(float);
		Gizmos.matrix = (Matrix4x4)(&num);
		Vector3 center = boundedDragSurface3D2.boundsBox.center;
		Vector3 size = boundedDragSurface3D2.boundsBox.size;
		float num2 = default(float);
		float num3 = default(float);
		Gizmos.DrawWireCube((Vector3)(&num2), (Vector3)(&num3));
		_ = matrix.m01;
		_ = matrix.m02;
		_ = matrix.m03;
		Gizmos.matrix = (Matrix4x4)(&num);
		if (normalizedSlotOffsets == null)
		{
			return;
		}
		List<Vector2> list = normalizedSlotOffsets;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rax_v28 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		if ((nint)0 == 0)
		{
			return;
		}
		Vector3 planeNormal = clipboardSurface.GetPlaneNormal();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		if (planeNormal.x > 1E-05f)
		{
			float num4 = planeNormal.z / planeNormal.x;
		}
		Gizmos.color = (Color)(&q);
		List<Vector2> list2 = normalizedSlotOffsets;
		float x = size.x;
		object obj3 = 0;
		object obj4 = 0;
		while (true)
		{
			object obj5 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ rax_v35 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			if ((nint)obj5 < 0)
			{
				object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 112));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+70]");
				if (TryGetWorldPointOnPlaneFromNormalizedOffset((Vector2)0, out var _))
				{
					Gizmos.DrawSphere((Vector3)(&x), slotGizmoRadius);
				}
				list2 = normalizedSlotOffsets;
				obj3++;
				obj4 = obj3;
				continue;
			}
			break;
		}
	}

	private static BoundedDragSurface3D.SurfaceAxis InferPlaneNormalAxis(BoundedDragSurface3D surface)
	{
		//IL_034b: Expected I4, but got O
		//IL_00ff: Expected I, but got O
		//IL_011f: Expected F4, but got I
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		if ((bool)surface)
		{
			if ((object)surface != null)
			{
				if (!surface.boundsBox)
				{
					goto IL_0337;
				}
				if ((object)surface.boundsBox != null)
				{
					Transform transform = surface.boundsBox.transform;
					Vector3 planeNormal = surface.GetPlaneNormal();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					Vector3 vector;
					Vector3 vector2 = default(Vector3);
					float num2;
					if (planeNormal.x > 1E-05f)
					{
						float num = planeNormal.z / planeNormal.x;
						vector = vector2;
						num2 = num;
					}
					else
					{
						nint num3 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ rax_v24 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rcx_v18 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						num2 = 0f;
						vector = Vector3.zeroVector;
					}
					if ((object)transform != null)
					{
						Vector3 up = transform.up;
						float num5 = (float)vector * up.x;
						object obj2 = default(object);
						object obj = obj2 * (object)vector2;
						float num6 = (float)obj + num5;
						float num7 = num2 * up.z;
						float num8 = num6 + num7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj3 = num8 & 0;
						Vector3 forward = transform.forward;
						float num9 = (float)vector * forward.x;
						object obj4 = obj2 * (object)vector2;
						float num10 = (float)obj4 + num9;
						float num11 = num2 * forward.z;
						float num12 = num10 + num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj5 = num12 & 0;
						Vector3 right = transform.right;
						float num13 = num2 * right.z;
						float num14 = (float)vector * right.x;
						object obj6 = obj2 * (object)vector2;
						float num15 = (float)obj6 + num14;
						float num16 = num15 + num13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj7 = num16 & 0;
						BoundedDragSurface3D.SurfaceAxis result;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
						{
							result = BoundedDragSurface3D.SurfaceAxis.Forward;
						}
						else if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
						{
							bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
							result = BoundedDragSurface3D.SurfaceAxis.Right;
							if (!flag)
							{
								result = BoundedDragSurface3D.SurfaceAxis.Up;
							}
						}
						else
						{
							result = BoundedDragSurface3D.SurfaceAxis.Up;
						}
						return result;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (BoundedDragSurface3D.SurfaceAxis)ex;
		}
		goto IL_0337;
		IL_0337:
		return BoundedDragSurface3D.SurfaceAxis.Up;
	}

	private unsafe static Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
	{
		//IL_0013: Invalid comparison between O and F4
		//IL_0090: Expected native int or pointer, but got O
		//IL_009d: Expected native int or pointer, but got O
		//IL_00aa: Expected native int or pointer, but got O
		//IL_0175: Expected native int or pointer, but got O
		//IL_0182: Expected native int or pointer, but got O
		//IL_0030: Expected F4, but got I4
		//IL_0039: Expected F4, but got I4
		//IL_0042: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj = default(object);
		float x;
		float y;
		float z;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			x = 0f;
			y = 0f;
			z = 0f;
		}
		else
		{
			z = planeNormal.z / (float)obj;
			y = planeNormal.y / (float)obj;
			x = planeNormal.x / (float)obj;
		}
		((Vector3*)(nint)planeNormal)->x = x;
		((Vector3*)(nint)planeNormal)->y = y;
		((Vector3*)(nint)planeNormal)->z = z;
		float num = point.x - planePoint.x;
		float num3 = default(float);
		float num2 = num3 - num3;
		float num4 = num * planeNormal.x;
		float num5 = point.z - planePoint.z;
		float num6 = num2 * num3;
		float num7 = num5 * planeNormal.z;
		float num8 = num6 + num4;
		float num9 = num8 + num7;
		float num10 = num9 * planeNormal.z;
		float z2 = point.z - num10;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = num3;
		((Vector3*)(nint)vector)->z = z2;
		return vector;
	}

	private static int Mod(int x, int m)
	{
		if (m > 0)
		{
			int num = x % m;
			int num2 = num + m;
			if (num < 0)
			{
				num = num2;
			}
			return num;
		}
		return 0;
	}

	public ClipboardSlotCycler3D()
	{
		//IL_0017: Expected O, but got I
		//IL_0034: Expected O, but got I
		List<Vector2> list = new List<Vector2>();
		normalizedSlotOffsets = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F50]");
		boundsGizmoColor = (Color)0;
		drawGizmos = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F60]");
		slotGizmoColor = (Color)0;
		slotGizmoRadius = 0.015f;
		drawSlotIndexLabels = true;
		gizmoNormalLift = 0.002f;
		base._002Ector();
	}
}
