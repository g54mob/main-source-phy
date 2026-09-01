using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class DragSurfaceSlotCycler : MonoBehaviour
{
	private DragSurface dragSurface;

	private List<Vector3> slotLocalOffsets;

	private int startingIndex;

	private bool resetIndexOnEnable;

	private bool drawGizmos;

	private bool drawGizmosWhenNotSelected;

	private Color slotGizmoColor;

	private float slotGizmoRadius;

	private bool drawSlotIndexLabels;

	private float gizmoNormalLift;

	private bool debug;

	private int _nextIndex;

	public DragSurface Surface => dragSurface;

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
		if (!this.dragSurface)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DragSurface dragSurface = default(DragSurface);
			this.dragSurface = dragSurface;
		}
	}

	public unsafe bool TryGetNextSlotWorldPosition(out Vector3 worldPosition, out int allocatedIndex)
	{
		//IL_02eb: Expected I4, but got O
		//IL_018a: Expected O, but got Ref
		//IL_02f3: Expected Ref, but got F4
		//IL_01e4: Expected O, but got F4
		//IL_014a: Expected O, but got Ref
		//IL_0170: Expected O, but got Ref
		//IL_0175: Expected I, but got O
		ref Vector3 reference = ref *(Vector3*)null;
		_ = 0;
		ref int reference2 = ref *(int*)4294967295L;
		float num3;
		float num4 = default(float);
		if (slotLocalOffsets != null)
		{
			List<Vector3> list = slotLocalOffsets;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
				int num;
				if ((nint)0 > (nint)0)
				{
					int nextIndex = _nextIndex;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
					num = (int)((nint)nextIndex % (nint)0);
					if (num < 0)
					{
						int num2 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
						num = (int)((nint)num2 + (nint)0);
					}
				}
				else
				{
					num = 0;
				}
				reference2 = ref *(int*)num;
				int nextIndex2 = num + 1;
				_nextIndex = nextIndex2;
				if (slotLocalOffsets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (!dragSurface)
					{
						num3 = num4;
						Vector3 vector = (Vector3)(&num4);
						nint num5 = 0;
						float num7 = default(float);
						float num6 = num7;
						goto IL_02eb;
					}
					if ((object)dragSurface != null)
					{
						Transform transform = dragSurface.transform;
						if ((object)transform != null)
						{
							object obj = default(object);
							Vector3 vector2 = transform.TransformPoint((Vector3)(&obj));
							num3 = vector2.x;
							float num6 = vector2.z;
							Vector3 vector = (Vector3)(&obj);
							nint num5 = unchecked((nint)null);
							goto IL_02eb;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
		IL_02eb:
		reference = ref *(Vector3*)num3;
		if (debug)
		{
			string arg = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string text = $"[{arg}] Slot allocated: index={arg2}, ";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object arg3 = (Vector3)num4;
			string text2 = $"localOffset={arg3}, ";
			object obj2 = default(object);
			object arg4 = (Vector3)obj2;
			string text3 = $"worldPosition={arg4}";
			string message = text + text2 + text3;
			Debug.Log(message, this);
		}
		return true;
	}

	public unsafe Vector3 LocalOffsetToWorld(Vector3 localOffset)
	{
		//IL_00e2: Expected native int or pointer, but got O
		//IL_00ef: Expected native int or pointer, but got O
		//IL_0085: Expected O, but got Ref
		float x;
		float z;
		if ((bool)dragSurface)
		{
			if ((object)dragSurface != null)
			{
				Transform transform = dragSurface.transform;
				if ((object)transform != null)
				{
					object obj = default(object);
					Vector3 vector = transform.TransformPoint((Vector3)(&obj));
					x = vector.x;
					z = vector.z;
					goto IL_00da;
				}
			}
			return (Vector3)new NullReferenceException();
		}
		x = localOffset.x;
		z = localOffset.z;
		goto IL_00da;
		IL_00da:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = x;
		((Vector3*)(nint)vector2)->z = z;
		return vector2;
	}

	private void OnDrawGizmos()
	{
		if (drawGizmos && drawGizmosWhenNotSelected)
		{
			DrawGizmosInternal();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (drawGizmos)
		{
			DrawGizmosInternal();
		}
	}

	private unsafe void DrawGizmosInternal()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0233: Expected O, but got Ref
		//IL_00e4: Expected I, but got O
		//IL_0104: Expected F4, but got I
		//IL_0270: Expected O, but got Ref
		//IL_02a4: Expected O, but got I4
		//IL_02ad: Expected O, but got I4
		//IL_0144: Expected O, but got Ref
		//IL_01f7: Expected F4, but got I
		//IL_0207: Expected F4, but got I
		//IL_02d9: Expected O, but got Ref
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_019d: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		EnsureSurfaceReference();
		if (!dragSurface || slotLocalOffsets == null)
		{
			return;
		}
		List<Vector3> list = slotLocalOffsets;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
		if ((nint)0 == 0)
		{
			return;
		}
		Vector3 planeNormal = dragSurface.GetPlaneNormal();
		_ = planeNormal.x;
		_ = planeNormal.z;
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num2;
		if (planeNormal.x > 1E-05f)
		{
			float num = planeNormal.z / planeNormal.x;
			num2 = num;
		}
		else
		{
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ rax_v27 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ rcx_v27 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num2 = 0f;
			_ = Vector3.zeroVector;
		}
		Color color = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
		float num5 = gizmoNormalLift * num2;
		_ = slotGizmoColor;
		Gizmos.color = color;
		List<Vector3> list2 = slotLocalOffsets;
		object obj4 = 0;
		object obj5 = 0;
		while (true)
		{
			object obj6 = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v15 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
			if ((nint)obj6 < 0)
			{
				object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				float num6;
				if ((bool)dragSurface)
				{
					Transform transform = dragSurface.transform;
					Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
					_ = 0;
					Vector3 vector = transform.TransformPoint(position);
					float x = vector.x;
					num6 = vector.z;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
					float x = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
					num6 = 0f;
				}
				float num7 = num6 + num5;
				Vector3 center = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				Gizmos.DrawSphere(center, slotGizmoRadius);
				list2 = slotLocalOffsets;
				obj4++;
				obj5 = obj4;
				continue;
			}
			break;
		}
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

	public DragSurfaceSlotCycler()
	{
		//IL_0022: Expected O, but got I
		List<Vector3> list = new List<Vector3>();
		slotLocalOffsets = list;
		drawGizmos = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F60]");
		slotGizmoColor = (Color)0;
		slotGizmoRadius = 0.015f;
		drawSlotIndexLabels = true;
		gizmoNormalLift = 0.002f;
		base._002Ector();
	}
}
