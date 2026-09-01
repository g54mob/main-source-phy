using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class MapPiece3D : MonoBehaviour, ICursorDraggable
{
	public enum SurfaceAxis
	{
		Up,
		Forward,
		Right
	}

	public Camera cam;

	public Collider interactionCollider;

	public BoxCollider boundsBox;

	private VirtualCursor virtualCursor;

	private bool autoFindVirtualCursor;

	private string virtualCursorTag;

	public SurfaceAxis planeAxis;

	public float dragLift;

	public float dragFollowSpeed;

	public float pullThresholdPixels;

	public bool updateWhileDraggingOnCameraMotion;

	public float pickRayDistance;

	public bool centerOriginOnCursorWhileDragging;

	public bool debug;

	public bool drawGizmos;

	public Color gizmoPlaneColor;

	private bool dragging;

	private bool _externallyControlled;

	private Vector3 dragOffsetWorld;

	private Vector2 lastPointerPos;

	private Plane dragPlane;

	private Vector3 lastCamPos;

	private Quaternion lastCamRot;

	private Action m_DragStarted;

	private Action m_DragEnded;

	public bool IsDragging => dragging;

	public event Action DragStarted
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 192;
			Delegate obj2 = this.m_DragStarted;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 192;
			Delegate obj2 = this.m_DragStarted;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action DragEnded
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 200;
			Delegate obj2 = this.m_DragEnded;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 200;
			Delegate obj2 = this.m_DragEnded;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (!cam)
		{
			Camera main = Camera.main;
			cam = main;
		}
		if (!interactionCollider)
		{
			string text = base.name;
			string message = text + ": interactionCollider not assigned.";
			Debug.LogError(message, this);
		}
		if (!boundsBox)
		{
			string text2 = base.name;
			string message2 = text2 + ": boundsBox (BoxCollider) not assigned.";
			Debug.LogError(message2, this);
		}
		if (autoFindVirtualCursor && !virtualCursor)
		{
			bool flag = TryResolveVirtualCursor();
		}
	}

	private void OnEnable()
	{
		if (autoFindVirtualCursor && !virtualCursor)
		{
			bool flag = TryResolveVirtualCursor();
		}
	}

	private void OnDisable()
	{
		if (dragging)
		{
			dragging = false;
			Action dragEnded = this.m_DragEnded;
			if (this.m_DragEnded != null)
			{
				IntPtr invoke_impl = ((Delegate)dragEnded).invoke_impl;
				IntPtr method = ((Delegate)dragEnded).method;
				IntPtr method_code = ((Delegate)dragEnded).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v33 @ rax_v1 (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private void Update()
	{
		//IL_0140: Expected F4, but got O
		//IL_0150: Expected F4, but got I
		//IL_022b: Invalid comparison between F4 and I4
		//IL_0254: Expected O, but got I4
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_050a: Invalid comparison between O and F4
		//IL_037d: Expected O, but got F4
		//IL_0350: Expected O, but got I
		//IL_03b7: Expected O, but got F4
		//IL_03f6: Expected O, but got F4
		if (!interactionCollider || !boundsBox || !cam)
		{
			return;
		}
		if (dragging && _externallyControlled)
		{
			float num;
			float num2;
			if (this.virtualCursor == null)
			{
				int width = Screen.width;
				num = (float)width * 0.5f;
				int height = Screen.height;
				num2 = (float)height * 0.5f;
			}
			else
			{
				VirtualCursor virtualCursor = this.virtualCursor;
				num = (float)virtualCursor._position;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rax_v40 (VirtualCursor)+70]");
				num2 = 0f;
			}
			float num3 = num - (float)lastPointerPos;
			float num4 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapPiece3D)+90]");
			float num5 = num4 - 0f;
			float num6 = pullThresholdPixels * pullThresholdPixels;
			float num7 = num3 * num3;
			float num8 = num5 * num5;
			float num9 = num8 + num7;
			Vector2 vector = default(Vector2);
			if (updateWhileDraggingOnCameraMotion)
			{
				Transform transform = cam.transform;
				Vector3 position = transform.position;
				float num10 = position.x - (float)lastCamPos;
				object obj = vector - vector;
				float num11 = position.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapPiece3D)+AC]");
				float num12 = num11 - 0f;
				object obj2 = obj * obj;
				float num13 = num10 * num10;
				float num14 = num12 * num12;
				float num15 = (float)obj2 + num13;
				float num16 = num15 + num14;
				bool flag = 9.9999994E-11f < num16;
				float num17 = 9.9999994E-11f - num16;
				bool flag2 = num17 == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				object obj3 = flag4 & flag3;
				if (obj3 != null)
				{
					Transform transform2 = cam.transform;
					Quaternion rotation = transform2.rotation;
					object obj4 = vector * vector;
					float num18 = (float)lastCamRot * rotation.x;
					float num19 = (float)obj4 + num18;
					object obj5 = vector * vector;
					object obj6 = vector * vector;
					float num20 = num19 + (float)obj5;
					float num21 = num20 + (float)obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj7 = num21 & 0;
					object obj8 = obj7;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BF8]");
					if ((nint)obj8 > 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BF8]");
						obj7 = 0;
					}
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.999999f))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EF80");
					}
				}
			}
			if (num9 != num6)
			{
				UpdateDrag(vector);
				lastPointerPos = (Vector2)num;
				Transform transform3 = cam.transform;
				Vector3 position2 = transform3.position;
				lastCamPos = (Vector3)position2.x;
				_ = position2.z;
				Transform transform4 = cam.transform;
				lastCamRot = (Quaternion)transform4.rotation.x;
			}
		}
		if (autoFindVirtualCursor && !this.virtualCursor)
		{
			bool flag5 = TryResolveVirtualCursor();
		}
	}

	public unsafe void BeginDragFromManager(Camera raycastCamera, Vector2 screenPos)
	{
		//IL_0165: Expected O, but got Ref
		//IL_0187: Expected O, but got Ref
		//IL_01d4: Expected O, but got Ref
		if (!base.isActiveAndEnabled || !interactionCollider || !boundsBox)
		{
			return;
		}
		if (raycastCamera != null)
		{
			cam = raycastCamera;
		}
		if (!cam)
		{
			Camera main = Camera.main;
			cam = main;
		}
		if ((bool)cam)
		{
			if (autoFindVirtualCursor && !virtualCursor)
			{
				bool flag = TryResolveVirtualCursor();
			}
			object obj = default(object);
			Ray ray = cam.ScreenPointToRay((Vector3)(&obj));
			object obj2 = default(object);
			if (interactionCollider.Raycast((Ray)(&obj2), out var hitInfo, pickRayDistance))
			{
				_externallyControlled = true;
				lastPointerPos = screenPos;
				Vector3 point = hitInfo.point;
				StartDragInternal((Vector3)(&obj), screenPos);
			}
			else if (debug)
			{
				string text = base.name;
				string message = "[" + text + "] BeginDragFromManager: ray did not hit interactionCollider; aborting.";
				Debug.LogWarning(message, this);
			}
		}
	}

	public void EndDragFromManager()
	{
		bool flag = !dragging;
		_externallyControlled = false;
		if (!flag)
		{
			EndDragInternal();
		}
	}

	public void SetVirtualCursor(VirtualCursor vc)
	{
		virtualCursor = vc;
	}

	private unsafe void StartDragInternal(Vector3 hitPoint, Vector2 pressScreenPos)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00ea: Expected O, but got Ref
		//IL_00fc: Expected O, but got Ref
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Expected F4, but got Unknown
		//IL_05e9: Expected O, but got F4
		//IL_0160: Expected F4, but got I4
		//IL_0169: Expected F4, but got I4
		//IL_0172: Expected F4, but got I4
		//IL_01fd: Expected O, but got F4
		//IL_023c: Expected O, but got F4
		//IL_05fc: Expected I, but got O
		//IL_060c: Expected F4, but got I
		//IL_02f5: Expected O, but got Ref
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_0320: Expected O, but got Ref
		//IL_0330: Expected F4, but got I
		//IL_02ac: Expected O, but got F4
		//IL_02cf: Expected O, but got I4
		//IL_038a: Expected O, but got I4
		//IL_03a8: Expected F4, but got I
		//IL_045b: Expected O, but got Ref
		//IL_047b: Expected O, but got Ref
		//IL_0491: Expected O, but got I4
		//IL_0661: Expected O, but got F4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		dragging = true;
		Vector3 vector;
		if (planeAxis == SurfaceAxis.Forward)
		{
			Transform transform = boundsBox.transform;
			vector = transform.forward;
		}
		else if (planeAxis == SurfaceAxis.Right)
		{
			Transform transform2 = boundsBox.transform;
			vector = transform2.right;
		}
		else
		{
			Transform transform3 = boundsBox.transform;
			vector = transform3.up;
		}
		_ = vector.x;
		Transform transform4 = boundsBox.transform;
		Vector3 center = boundsBox.center;
		float num = default(float);
		Vector3 vector2 = transform4.TransformPoint((Vector3)(&num));
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
		_ = 0;
		_ = vector.z;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num3;
		float num4;
		float num2;
		if (!(vector2.x > 1E-05f))
		{
			num2 = 0f;
			num3 = 0f;
			num4 = 0f;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
			num3 = 0f / vector2.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
			num4 = 0f / vector2.x;
			num2 = vector.z / vector2.x;
		}
		object obj4 = default(object);
		float num5 = (float)obj4 * num4;
		float num6 = vector2.x * num3;
		float num7 = vector2.z * num2;
		float num8 = num5 + num6;
		float num9 = num8 + num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float num10 = num9 ^ 0;
		float num11 = default(float);
		dragPlane = (Plane)num11;
		Transform transform5 = cam.transform;
		Vector3 position = transform5.position;
		lastCamPos = (Vector3)position.x;
		_ = position.z;
		Transform transform6 = cam.transform;
		lastCamRot = (Quaternion)transform6.rotation.x;
		if (!centerOriginOnCursorWhileDragging)
		{
			Transform transform7 = base.transform;
			num10 = transform7.position.z - hitPoint.z;
			float num12 = num11 - num11;
			dragOffsetWorld = (Vector3)num11;
			num2 = num11;
			num = hitPoint.x;
			object obj5 = 0;
			float num13 = num11;
			ref float reference = ref *(float*)null;
		}
		else
		{
			nint num14 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-55]");
			float num12 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v546 @ rax_v27 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num15 = 0;
			dragOffsetWorld = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rcx_v25 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			_ = 0;
			Ray ray = cam.ScreenPointToRay((Vector3)(&num));
			Plane plane = (Plane)(this + 148);
			ref float reference = ref System.Runtime.CompilerServices.Unsafe.As<object, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Ray ray2 = (Ray)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ rax_v29 (UnityEngine.Ray)+10]");
			float num13 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ rax_v29 (UnityEngine.Ray)+10]");
			_ = 0;
			_ = ray.m_Origin;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ rax_v29 (UnityEngine.Ray)+10]");
			_ = 0;
			bool flag = ((Plane*)plane)->Raycast(ray2, out reference);
			bool flag2 = !flag;
			num = num11;
			object obj5 = 0;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+67]");
				num10 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-41]");
				float num16 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+67]");
				float num17 = num16 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-3D]");
				float num18 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+67]");
				float num19 = num18 * 0f;
				float num20 = num17 + num11;
				float num21 = num19 + num11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
				float num22 = 0f * dragLift;
				float num23 = vector.z * dragLift;
				num2 = num20 + num22;
				num12 = num21 + num23;
				Vector3 vector3 = ClampToBoundsPlane((Vector3)(&num));
				Transform transform8 = base.transform;
				transform8.position = (Vector3)(&num);
				num = vector3.x;
				obj5 = 0;
				num13 = num11;
				reference = ref *(float*)null;
			}
		}
		Action dragStarted = this.m_DragStarted;
		if (this.m_DragStarted != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v639.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (debug)
		{
			string arg = base.name;
			bool flag3 = centerOriginOnCursorWhileDragging;
			object arg2 = "CenterOnCursor";
			if (!flag3)
			{
				arg2 = "PreserveGrabOffset";
			}
			object arg3 = (Vector3)num;
			string message = $"[{arg}] Drag START. Mode={arg2} Normal={arg3}";
			Debug.Log(message, this);
		}
	}

	private unsafe void UpdateDrag(Vector2 screenPos)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0020: Expected O, but got Ref
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0065: Expected O, but got Ref
		//IL_00ca: Expected O, but got Ref
		//IL_010e: Expected O, but got I
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_01be: Invalid comparison between I4 and F4
		//IL_0209: Expected F4, but got I4
		//IL_021c: Expected O, but got Ref
		//IL_026a: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		Vector3 pos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
		_ = 0;
		Ray ray = cam.ScreenPointToRay(pos);
		Plane plane = (Plane)(this + 148);
		ref float enter = ref System.Runtime.CompilerServices.Unsafe.As<object, float>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
		Ray ray2 = (Ray)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v4 (UnityEngine.Ray)+10]");
		_ = 0;
		_ = ray.m_Origin;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rax_v4 (UnityEngine.Ray)+10]");
		_ = 0;
		if (!((Plane*)plane)->Raycast(ray2, out enter))
		{
			return;
		}
		Vector3 worldPos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
		_ = dragOffsetWorld;
		_ = dragPlane;
		float num = dragLift;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapPiece3D)+9C]");
		float num2 = num * 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-55]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
		object obj3 = num3 * 0;
		object obj5 = default(object);
		object obj4 = obj3 + obj5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapPiece3D)+88]");
		object obj6 = obj4 + 0;
		float num4 = (float)obj6 + num2;
		Vector3 vector = ClampToBoundsPlane(worldPos);
		_ = vector.x;
		Transform transform = base.transform;
		Transform transform2 = base.transform;
		Vector3 position = transform2.position;
		_ = position.x;
		float deltaTime = Time.deltaTime;
		float num5 = deltaTime * dragFollowSpeed;
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		float num6 = vector.z - position.z;
		float num7 = num6 * num5;
		float num8 = num7 + position.z;
		Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		transform.position = position2;
		if (debug)
		{
			string arg = base.name;
			object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
			_ = vector.x;
			_ = vector.z;
			object arg2 = (Vector3)obj7;
			string message = $"[{arg}] Dragging target={arg2}";
			Debug.Log(message, this);
		}
	}

	private unsafe void EndDragInternal()
	{
		//IL_0008: Expected O, but got Ref
		//IL_018b: Expected O, but got Ref
		//IL_01cb: Expected O, but got Ref
		//IL_03ba: Expected O, but got Ref
		//IL_0226: Expected F4, but got I4
		//IL_022f: Expected F4, but got I4
		//IL_0238: Expected F4, but got I4
		//IL_02ab: Expected O, but got Ref
		//IL_032e: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		dragging = false;
		Vector3 vector;
		if (planeAxis == SurfaceAxis.Forward)
		{
			Transform transform = boundsBox.transform;
			vector = transform.forward;
		}
		else if (planeAxis == SurfaceAxis.Right)
		{
			Transform transform2 = boundsBox.transform;
			vector = transform2.right;
		}
		else
		{
			Transform transform3 = boundsBox.transform;
			vector = transform3.up;
		}
		_ = vector.x;
		Transform transform4 = base.transform;
		Vector3 position = transform4.position;
		float num = dragLift;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
		float num2 = num * 0f;
		float num3 = dragLift;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
		float num4 = num3 * 0f;
		float num5 = dragLift * vector.z;
		float num6 = position.x - num2;
		_ = position.x;
		object obj3 = default(object);
		float num7 = (float)obj3 - num4;
		float num8 = position.z - num5;
		Transform transform5 = boundsBox.transform;
		Vector3 center = boundsBox.center;
		Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		_ = center.x;
		_ = center.z;
		Vector3 vector2 = transform5.TransformPoint(position2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
		_ = 0;
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
		_ = 0;
		_ = vector.z;
		_ = vector2.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num9;
		float num10;
		float num11;
		if (!(vector2.x > 1E-05f))
		{
			num9 = 0f;
			num10 = 0f;
			num11 = 0f;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
			num11 = 0f / vector2.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-35]");
			num10 = 0f / vector2.x;
			num9 = vector.z / vector2.x;
		}
		Vector3 worldPos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		float num12 = num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
		float num13 = num12 - 0f;
		float num14 = num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-25]");
		float num15 = num14 - 0f;
		float num16 = num13 * num11;
		float num17 = num15 * num10;
		float num18 = num17 + num16;
		float num19 = num8 - vector2.z;
		float num20 = num19 * num9;
		float num21 = num18 + num20;
		float num22 = num9 * num21;
		float num23 = num8 - num22;
		Vector3 vector3 = ClampToBoundsPlane(worldPos);
		Transform transform6 = base.transform;
		_ = vector3.x;
		Vector3 position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_ = vector3.z;
		transform6.position = position3;
		Action dragEnded = this.m_DragEnded;
		if (this.m_DragEnded != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v459.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (debug)
		{
			string arg = base.name;
			Transform transform7 = base.transform;
			Vector3 position4 = transform7.position;
			object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = position4.x;
			_ = position4.z;
			object arg2 = (Vector3)obj5;
			string message = $"[{arg}] Drag END. Final={arg2}";
			Debug.Log(message, this);
		}
	}

	private Vector2 GetPointerScreenPosition()
	{
		Vector2 result = default(Vector2);
		if (!(virtualCursor != null))
		{
			int width = Screen.width;
			int height = Screen.height;
			return result;
		}
		if ((object)virtualCursor != null)
		{
			return result;
		}
		return (Vector2)new NullReferenceException();
	}

	private bool TryResolveVirtualCursor(bool logWarnings = false)
	{
		//IL_0180: Expected I4, but got O
		if (!string.IsNullOrEmpty(virtualCursorTag))
		{
			GameObject gameObject = GameObject.FindWithTag(virtualCursorTag);
			if (gameObject != null)
			{
				if ((object)gameObject == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				VirtualCursor virtualCursor = default(VirtualCursor);
				this.virtualCursor = virtualCursor;
				if (!(this.virtualCursor == null))
				{
					goto IL_0166;
				}
				if (logWarnings)
				{
					string text = base.name;
					string message = text + ": GameObject with tag '" + virtualCursorTag + "' found but has no VirtualCursor component.";
					Debug.LogWarning(message, this);
				}
			}
		}
		if (this.virtualCursor == null)
		{
			VirtualCursor virtualCursor2 = UnityEngine.Object.FindObjectOfType<VirtualCursor>();
			this.virtualCursor = virtualCursor2;
			if (this.virtualCursor != null)
			{
				goto IL_0166;
			}
		}
		return false;
		IL_0166:
		return true;
	}

	private unsafe Vector3 GetPlaneNormal()
	{
		//IL_008c: Expected native int or pointer, but got O
		//IL_009e: Expected native int or pointer, but got O
		Vector3 vector;
		if (planeAxis == SurfaceAxis.Forward)
		{
			if ((object)boundsBox != null)
			{
				Transform transform = boundsBox.transform;
				if ((object)transform != null)
				{
					vector = transform.forward;
					goto IL_007f;
				}
			}
		}
		else if ((object)boundsBox != null)
		{
			if (planeAxis == SurfaceAxis.Right)
			{
				Transform transform2 = boundsBox.transform;
				if ((object)transform2 != null)
				{
					vector = transform2.right;
					goto IL_007f;
				}
			}
			else
			{
				Transform transform3 = boundsBox.transform;
				if ((object)transform3 != null)
				{
					vector = transform3.up;
					goto IL_007f;
				}
			}
		}
		return (Vector3)new NullReferenceException();
		IL_007f:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
	}

	private unsafe Vector3 GetPlaneOriginPoint()
	{
		//IL_0086: Expected O, but got Ref
		//IL_0097: Expected native int or pointer, but got O
		//IL_00a9: Expected native int or pointer, but got O
		if ((object)boundsBox != null)
		{
			Transform transform = boundsBox.transform;
			if ((object)boundsBox != null)
			{
				Vector3 center = boundsBox.center;
				if ((object)transform != null)
				{
					object obj = default(object);
					Vector3 vector = transform.TransformPoint((Vector3)(&obj));
					Vector3 vector2 = default(Vector3);
					((Vector3*)(nint)vector2)->x = vector.x;
					((Vector3*)(nint)vector2)->z = vector.z;
					return vector2;
				}
			}
		}
		return (Vector3)new NullReferenceException();
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

	private unsafe Vector3 ClampToBoundsPlane(Vector3 worldPos)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_04d3: Expected F4, but got I
		//IL_04e3: Invalid comparison between F4 and I
		//IL_0302: Expected O, but got I4
		//IL_040c: Expected F4, but got I
		//IL_041c: Invalid comparison between F4 and I
		//IL_0666: Expected F4, but got I
		//IL_0676: Invalid comparison between F4 and I
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Expected O, but got Unknown
		//IL_05db: Expected native int or pointer, but got O
		//IL_05ed: Expected native int or pointer, but got O
		//IL_0340: Expected F4, but got I
		//IL_0350: Invalid comparison between F4 and I
		//IL_064c: Expected F4, but got I
		object obj2 = default(object);
		object obj = obj2 - 95;
		Transform transform;
		float z;
		if ((object)boundsBox != null)
		{
			transform = boundsBox.transform;
			if ((object)transform != null)
			{
				Vector3 position = (Vector3)(obj - 57);
				_ = worldPos.z;
				_ = worldPos.x;
				Vector3 vector = transform.InverseTransformPoint(position);
				z = vector.z;
				_ = vector.x;
				if ((object)boundsBox != null)
				{
					Vector3 size = boundsBox.size;
					object obj3 = default(object);
					float num = (float)obj3 * 0.5f;
					float num2 = size.x * 0.5f;
					_ = size.x;
					float num3 = size.z * 0.5f;
					if ((object)boundsBox != null)
					{
						float num4 = boundsBox.center.x - num2;
						if ((object)boundsBox != null)
						{
							float num5 = num2 + boundsBox.center.x;
							if ((object)boundsBox != null)
							{
								float num6 = boundsBox.center.y - num;
								if ((object)boundsBox != null)
								{
									float num7 = num + boundsBox.center.y;
									if ((object)boundsBox != null)
									{
										float num8 = boundsBox.center.z - num3;
										if ((object)boundsBox != null)
										{
											float num9 = num3 + boundsBox.center.z;
											bool flag = planeAxis == SurfaceAxis.Up;
											if (!flag)
											{
												object obj4 = planeAxis - 1;
												if (!flag)
												{
													if ((nint)obj4 == 1)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
														float num10 = 0f;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
														if (!(num6 > 0f))
														{
															if (num10 > num7)
															{
																num10 = num7;
															}
														}
														else
														{
															num10 = num6;
														}
														if (!(num8 > z))
														{
															bool flag2 = !(z > num9);
															float num11 = z;
															if (!flag2)
															{
																num11 = num9;
															}
														}
														else
														{
															float num11 = num8;
														}
														if ((object)boundsBox == null)
														{
															goto IL_058f;
														}
														_ = boundsBox.center.x;
														goto IL_063c;
													}
												}
												else
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
													float num12 = 0f;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
													if (!(num4 > 0f))
													{
														if (num12 > num5)
														{
															num12 = num5;
														}
													}
													else
													{
														num12 = num4;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
													float num13 = 0f;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-35]");
													if (!(num6 > 0f))
													{
														if (num13 > num7)
														{
															num13 = num7;
														}
													}
													else
													{
														num13 = num6;
													}
													if ((object)boundsBox == null)
													{
														goto IL_058f;
													}
													z = boundsBox.center.z;
												}
												goto IL_059d;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
											float num14 = 0f;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
											if (!(num4 > 0f))
											{
												if (num14 > num5)
												{
													num14 = num5;
												}
											}
											else
											{
												num14 = num4;
											}
											if (!(num8 > z))
											{
												bool flag3 = !(z > num9);
												float num15 = z;
												if (!flag3)
												{
													num15 = num9;
												}
											}
											else
											{
												float num15 = num8;
											}
											if ((object)boundsBox != null)
											{
												_ = boundsBox.center.y;
												goto IL_063c;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_058f;
		IL_063c:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-31]");
		z = 0f;
		goto IL_059d;
		IL_059d:
		Vector3 position2 = (Vector3)(obj - 57);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
		_ = 0;
		Vector3 vector2 = transform.TransformPoint(position2);
		Vector3 vector3 = default(Vector3);
		((Vector3*)(nint)vector3)->x = vector2.x;
		((Vector3*)(nint)vector3)->z = vector2.z;
		return vector3;
		IL_058f:
		return (Vector3)new NullReferenceException();
	}

	public MapPiece3D()
	{
		//IL_0066: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A3F3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoFindVirtualCursor = true;
		virtualCursorTag = "VirtualCursor";
		dragLift = 0.02f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206FC0]");
		gizmoPlaneColor = (Color)0;
		dragFollowSpeed = 18f;
		pullThresholdPixels = 4f;
		updateWhileDraggingOnCameraMotion = true;
		pickRayDistance = 500f;
		drawGizmos = true;
		base._002Ector();
	}
}
