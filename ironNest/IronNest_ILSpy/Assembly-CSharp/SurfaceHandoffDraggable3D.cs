using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class SurfaceHandoffDraggable3D : MonoBehaviour, ICursorDraggable
{
	public enum HomeSurface
	{
		None,
		Clipboard,
		Map
	}

	public enum DragAnchorMode
	{
		PivotUnderCursor,
		PreserveGrabOffset
	}

	private sealed class _003CLerpLocalScaleRoutine_003Ed__74 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public SurfaceHandoffDraggable3D _003C_003E4__this;

		public Vector3 from;

		public Vector3 to;

		private float _003Ct_003E5__2;

		private float _003Cdur_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLerpLocalScaleRoutine_003Ed__74(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0038: Expected F4, but got I4
			//IL_009d: Expected O, but got I4
			//IL_02a1: Expected I4, but got O
			//IL_0107: Invalid comparison between I4 and F4
			//IL_0152: Expected F4, but got I4
			//IL_0270: Expected O, but got Ref
			//IL_01ad: Invalid comparison between I4 and F4
			//IL_01f3: Expected O, but got Ref
			SurfaceHandoffDraggable3D surfaceHandoffDraggable3D = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003Ct_003E5__2 = _003C_003E1__state;
				bool flag = !(0.0001f < duration);
				float num = 0.0001f;
				if (!flag)
				{
					num = duration;
				}
				_003Cdur_003E5__3 = num;
			}
			else if (_003C_003E1__state != 1)
			{
				return false;
			}
			object obj = 16;
			_ = 4294967295L;
			Vector3 vector = default(Vector3);
			if (1f > _003Ct_003E5__2)
			{
				float deltaTime = Time.deltaTime;
				float num2 = deltaTime / _003Cdur_003E5__3;
				float num3 = (_003Ct_003E5__2 = num2 + _003Ct_003E5__2);
				if (!(0f > num3))
				{
					if (num3 > 1f)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
				if ((object)_003C_003E4__this != null)
				{
					Transform transform = _003C_003E4__this.transform;
					float num4 = num3 + num3;
					float num5 = num3 * num3;
					float num6 = 3f - num4;
					float num7 = num6 * num5;
					if (0f > num7 || num7 > 1f)
					{
					}
					if ((object)transform != null)
					{
						transform.localScale = (Vector3)(&vector);
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			else if ((object)_003C_003E4__this != null)
			{
				Transform transform2 = _003C_003E4__this.transform;
				if ((object)transform2 != null)
				{
					transform2.localScale = (Vector3)(&vector);
					surfaceHandoffDraggable3D._surfaceScaleRoutine = null;
					return false;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private Collider interactionCollider;

	private Camera cam;

	private bool autoResolveByTag;

	private string raycastCameraTag;

	private string virtualCursorTag;

	private string clipboardSurfaceTag;

	private string mapSurfaceTag;

	private VirtualCursor virtualCursor;

	private BoundedDragSurface3D clipboardSurface;

	private BoundedDragSurface3D mapSurface;

	private DragAnchorMode dragAnchorMode;

	private float dragLift;

	private bool useSurfaceDefaultLift;

	private float dragFollowSpeed;

	private bool clampToSurfaceBounds;

	private bool useRandomLiftOffset;

	private float randomLiftMin;

	private float randomLiftMax;

	private bool resampleRandomLiftEachDrag;

	private bool matchSurfaceRotation;

	private bool smoothSurfaceRotation;

	private float surfaceRotationLerpSpeed;

	private bool enableSurfaceHandoff;

	private float exitClipboardMarginPixels;

	private float raycastMaxDistance;

	private float handoffCooldownSeconds;

	private bool preferCloserSurfaceOnOverlap;

	private bool useSurfaceScaleMultiplier;

	private bool smoothSurfaceScale;

	private float surfaceScaleTransitionDuration;

	private UnityEvent onDragStartedUnityEvent;

	private UnityEvent onDragEndedUnityEvent;

	private bool debug;

	private Action m_DragStarted;

	private Action m_DragEnded;

	private bool _dragging;

	private bool _externallyControlled;

	private HomeSurface _currentSurface;

	private Plane _activePlane;

	private float _handoffCooldownRemaining;

	private Vector3 _baseLocalScale;

	private Coroutine _surfaceScaleRoutine;

	private Vector3 _grabOffsetWorld;

	private float _randomLiftOffset;

	public bool IsDragging => _dragging;

	public event Action DragStarted
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 200;
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
			object obj = this + 200;
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
			object obj = this + 208;
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
			object obj = this + 208;
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
		//IL_0021: Expected O, but got F4
		//IL_011d: Expected F4, but got I4
		//IL_016c: Expected O, but got I4
		Transform transform = base.transform;
		Vector3 localScale = transform.localScale;
		_baseLocalScale = (Vector3)localScale.x;
		_ = localScale.z;
		if (!cam)
		{
			Camera main = Camera.main;
			cam = main;
		}
		if (!interactionCollider)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Collider collider = default(Collider);
			interactionCollider = collider;
		}
		if (useRandomLiftOffset)
		{
			float maxInclusive = randomLiftMin;
			float num = randomLiftMin;
			if (randomLiftMin < randomLiftMax)
			{
				maxInclusive = randomLiftMax;
			}
			if (num > randomLiftMax)
			{
				num = randomLiftMax;
			}
			float num2 = UnityEngine.Random.Range(num, maxInclusive);
		}
		else
		{
			float num2 = 0f;
		}
		object obj = 276;
		if (autoResolveByTag)
		{
			bool flag = ResolveReferencesByTag(logWarnings: false);
		}
	}

	private void OnEnable()
	{
		if (autoResolveByTag)
		{
			bool flag = ResolveReferencesByTag(logWarnings: false);
		}
	}

	private void OnDisable()
	{
		if (_dragging)
		{
			_dragging = false;
			Action dragEnded = this.m_DragEnded;
			if (this.m_DragEnded != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v14.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
			if (onDragEndedUnityEvent != null)
			{
				onDragEndedUnityEvent.Invoke();
			}
		}
		if (_surfaceScaleRoutine != null)
		{
			StopCoroutine(_surfaceScaleRoutine);
			_surfaceScaleRoutine = null;
		}
	}

	private void Update()
	{
		//IL_0049: Invalid comparison between F4 and I4
		if (!_dragging || !_externallyControlled)
		{
			return;
		}
		if (_handoffCooldownRemaining > 0f)
		{
			float deltaTime = Time.deltaTime;
			float handoffCooldownRemaining = _handoffCooldownRemaining - deltaTime;
			_handoffCooldownRemaining = handoffCooldownRemaining;
		}
		if (virtualCursor == null)
		{
			int width = Screen.width;
			int height = Screen.height;
		}
		UnityEngine.Object obj = ((_currentSurface == HomeSurface.Clipboard) ? clipboardSurface : ((_currentSurface != HomeSurface.Map) ? null : mapSurface));
		Vector2 screenPos = default(Vector2);
		if ((bool)obj && (bool)cam)
		{
			_activePlane = (Plane)((BoundedDragSurface3D)obj).GetPlane().m_Normal;
			if (dragAnchorMode != DragAnchorMode.PreserveGrabOffset)
			{
				ForcePlacePivotUnderCursor((BoundedDragSurface3D)obj, screenPos, snap: false);
			}
			else
			{
				PlaceWithPreservedGrabOffset((BoundedDragSurface3D)obj, screenPos, snap: false);
			}
			if (matchSurfaceRotation)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rdi_v3 (UnityEngine.Object)+40]");
				if ((nint)0 != 0 && smoothSurfaceRotation)
				{
					ApplySurfaceRotation((BoundedDragSurface3D)obj, smooth: true);
				}
			}
		}
		if (enableSurfaceHandoff)
		{
			TrySurfaceHandoff(screenPos);
		}
	}

	public unsafe void BeginDragFromManager(Camera raycastCamera, Vector2 screenPos)
	{
		//IL_014e: Expected O, but got Ref
		//IL_0170: Expected O, but got Ref
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		if (_surfaceScaleRoutine != null)
		{
			StopCoroutine(_surfaceScaleRoutine);
			_surfaceScaleRoutine = null;
		}
		if (raycastCamera != null)
		{
			cam = raycastCamera;
		}
		if (autoResolveByTag)
		{
			bool flag = ResolveReferencesByTag(logWarnings: false);
		}
		if (!cam)
		{
			Camera main = Camera.main;
			cam = main;
		}
		if (!cam)
		{
			return;
		}
		if (interactionCollider != null)
		{
			object obj = default(object);
			Ray ray = cam.ScreenPointToRay((Vector3)(&obj));
			if (!interactionCollider.Raycast((Ray)(&obj), out var _, raycastMaxDistance))
			{
				return;
			}
		}
		_externallyControlled = true;
		StartDragInternal(screenPos);
	}

	public void EndDragFromManager()
	{
		bool flag = !_dragging;
		_externallyControlled = false;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 13 Invalid \"Jump target not found in method: 0x18050CD90\"");
		}
	}

	private unsafe void StartDragInternal(Vector2 pressScreenPos)
	{
		//IL_0152: Expected O, but got I4
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected I4, but got Unknown
		//IL_05cc: Expected I, but got O
		//IL_061a: Expected F4, but got I4
		//IL_02d9: Expected O, but got Ref
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Expected O, but got Unknown
		//IL_02f8: Expected O, but got I
		//IL_0309: Expected O, but got Ref
		//IL_04a3: Expected I4, but got F4
		//IL_04b5: Expected I4, but got O
		if (resampleRandomLiftEachDrag)
		{
			if (useRandomLiftOffset)
			{
				float maxInclusive = randomLiftMin;
				float num = randomLiftMin;
				if (randomLiftMin < randomLiftMax)
				{
					maxInclusive = randomLiftMax;
				}
				if (num > randomLiftMax)
				{
					num = randomLiftMax;
				}
				float randomLiftOffset = UnityEngine.Random.Range(num, maxInclusive);
				_randomLiftOffset = randomLiftOffset;
			}
			else
			{
				_randomLiftOffset = 0f;
			}
		}
		_dragging = true;
		_handoffCooldownRemaining = 0f;
		UnityEngine.Object obj = (_currentSurface = ChooseSurfaceFromPointer(pressScreenPos)) switch
		{
			HomeSurface.Clipboard => clipboardSurface, 
			HomeSurface.Map => mapSurface, 
			_ => null, 
		};
		if (obj == null)
		{
			HomeSurface homeSurface;
			if (clipboardSurface != null)
			{
				homeSurface = HomeSurface.Clipboard;
			}
			else
			{
				bool flag = mapSurface != null;
				object obj2 = 0 - (flag ? 1 : 0);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
				homeSurface = (HomeSurface)(obj2 & 2);
			}
			_currentSurface = homeSurface;
			obj = homeSurface switch
			{
				HomeSurface.Clipboard => clipboardSurface, 
				HomeSurface.Map => mapSurface, 
				_ => null, 
			};
		}
		float enter;
		if ((bool)obj && (bool)cam)
		{
			Transform transform = base.transform;
			Transform parent = ((Component)obj).transform;
			transform.SetParent(parent, worldPositionStays: true);
			ApplySurfaceScale((BoundedDragSurface3D)obj, smooth: false);
			if (matchSurfaceRotation)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rbx_v2 (UnityEngine.Object)+40]");
				if ((nint)0 != 0)
				{
					ApplySurfaceRotation((BoundedDragSurface3D)obj, smooth: false);
				}
			}
			_activePlane = (Plane)((BoundedDragSurface3D)obj).GetPlane().m_Normal;
			nint num2 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v700 @ rax_v21 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num3 = 0;
			Vector3 zeroVector = Vector3.zeroVector;
			_grabOffsetWorld = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v590 @ rcx_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			_ = 0;
			bool flag2 = dragAnchorMode != DragAnchorMode.PreserveGrabOffset;
			enter = 0f;
			bool flag5;
			Vector2 vector2;
			if (!flag2)
			{
				object obj3 = default(object);
				Ray ray = cam.ScreenPointToRay((Vector3)(&obj3));
				Plane plane = (Plane)(this + 224);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v782 @ rax_v39 (UnityEngine.Ray)+10]");
				zeroVector = (Vector3)0;
				object obj4 = default(object);
				bool flag3 = ((Plane*)plane)->Raycast((Ray)(&obj4), out enter);
				bool flag4 = !flag3;
				float num4 = default(float);
				float maxInclusive = num4;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v782 @ rax_v39 (UnityEngine.Ray)+10]");
					float num5 = 0f * enter;
					object obj5 = default(object);
					float num6 = (float)obj5 * enter;
					Vector3 vector = default(Vector3);
					float num7 = num5 + (float)vector;
					float num8 = num6 + (float)vector;
					Transform transform2 = base.transform;
					Vector3 position = transform2.position;
					maxInclusive = position.y - num7;
					float num9 = position.z - num8;
					_grabOffsetWorld = vector;
					zeroVector = vector;
				}
				if (dragAnchorMode == DragAnchorMode.PreserveGrabOffset)
				{
					PlaceWithPreservedGrabOffset((BoundedDragSurface3D)obj, pressScreenPos, snap: true);
					flag5 = true;
					vector2 = pressScreenPos;
					goto IL_0429;
				}
			}
			ForcePlacePivotUnderCursor((BoundedDragSurface3D)obj, pressScreenPos, snap: true);
			flag5 = true;
			vector2 = pressScreenPos;
			goto IL_0429;
		}
		_dragging = false;
		return;
		IL_0429:
		Action dragStarted = this.m_DragStarted;
		if (this.m_DragStarted != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v816.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (onDragStartedUnityEvent != null)
		{
			onDragStartedUnityEvent.Invoke();
		}
		if (debug)
		{
			string arg = base.name;
			object arg2 = (HomeSurface)enter;
			Vector2 vector3 = default(Vector2);
			object arg3 = (DragAnchorMode)vector3;
			string message = $"[{arg}] Drag start on surface={arg2} anchorMode={arg3}";
			Debug.Log(message, this);
		}
	}

	private void UpdateDrag(Vector2 screenPos)
	{
		UnityEngine.Object obj = ((_currentSurface == HomeSurface.Clipboard) ? clipboardSurface : ((_currentSurface != HomeSurface.Map) ? null : mapSurface));
		if (!obj || !cam)
		{
			return;
		}
		_activePlane = (Plane)((BoundedDragSurface3D)obj).GetPlane().m_Normal;
		if (dragAnchorMode != DragAnchorMode.PreserveGrabOffset)
		{
			ForcePlacePivotUnderCursor((BoundedDragSurface3D)obj, screenPos, snap: false);
		}
		else
		{
			PlaceWithPreservedGrabOffset((BoundedDragSurface3D)obj, screenPos, snap: false);
		}
		if (matchSurfaceRotation)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdi_v1 (UnityEngine.Object)+40]");
			if ((nint)0 != 0 && smoothSurfaceRotation)
			{
				ApplySurfaceRotation((BoundedDragSurface3D)obj, smooth: true);
			}
		}
	}

	private unsafe void EndDragInternal()
	{
		//IL_01fb: Expected I4, but got O
		//IL_00ea: Expected O, but got Ref
		//IL_0173: Expected O, but got I4
		//IL_0100: Expected O, but got Ref
		//IL_0109: Expected O, but got I4
		_dragging = false;
		UnityEngine.Object obj = ((_currentSurface == HomeSurface.Clipboard) ? clipboardSurface : ((_currentSurface != HomeSurface.Map) ? null : mapSurface));
		bool flag = obj != null;
		bool flag2 = !flag;
		bool flag3 = false;
		if (!flag2)
		{
			ApplyRestingLiftOnly((BoundedDragSurface3D)obj);
			bool flag4 = !clampToSurfaceBounds;
			flag3 = false;
			if (!flag4)
			{
				Transform transform = base.transform;
				Transform transform2 = base.transform;
				Vector3 position = transform2.position;
				float num = default(float);
				Vector3 vector = ((BoundedDragSurface3D)obj).ClampToSurfaceBoundsPreserveNormalOffset((Vector3)(&num));
				transform.position = (Vector3)(&num);
				object obj2 = 0;
				flag3 = false;
			}
			if (matchSurfaceRotation)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rbx_v1 (UnityEngine.Object)+40]");
				if ((nint)0 != 0)
				{
					ApplySurfaceRotation((BoundedDragSurface3D)obj, smooth: false);
					object obj2 = 0;
					flag3 = false;
				}
			}
		}
		Action dragEnded = this.m_DragEnded;
		if (this.m_DragEnded != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v161.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (onDragEndedUnityEvent != null)
		{
			onDragEndedUnityEvent.Invoke();
		}
		if (debug)
		{
			string arg = base.name;
			object obj3 = default(object);
			object arg2 = (HomeSurface)obj3;
			string message = $"[{arg}] Drag end on surface={arg2}";
			Debug.Log(message, this);
		}
	}

	private unsafe void ApplyRestingLiftOnly(BoundedDragSurface3D surf)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0095: Expected I, but got O
		//IL_00b5: Expected F4, but got I
		//IL_00ed: Expected O, but got Ref
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0127: Expected F4, but got I4
		//IL_0130: Expected F4, but got I4
		//IL_0139: Expected F4, but got I4
		//IL_018d: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if ((bool)surf)
		{
			Vector3 planeNormal = surf.GetPlaneNormal();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			object obj3 = default(object);
			Vector3 vector;
			float num3;
			if (planeNormal.x > 1E-05f)
			{
				float num = (float)obj3 / planeNormal.x;
				float num2 = planeNormal.z / planeNormal.x;
				Vector3 vector2 = default(Vector3);
				vector = vector2;
				num3 = num2;
			}
			else
			{
				nint num4 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rax_v17 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ rcx_v16 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				num3 = 0f;
				vector = Vector3.zeroVector;
			}
			Vector3 planeOriginPoint = surf.GetPlaneOriginPoint();
			_ = planeOriginPoint.x;
			_ = _randomLiftOffset;
			Transform transform = base.transform;
			Vector3 position = transform.position;
			object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			float num6;
			float num7;
			float num8;
			if (!(position.x > 1E-05f))
			{
				num6 = 0f;
				num7 = 0f;
				num8 = 0f;
			}
			else
			{
				num8 = (float)vector / position.x;
				num7 = (float)obj3 / position.x;
				num6 = num3 / position.x;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-65]");
			object obj5 = obj3 - 0;
			float num9 = position.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
			float num10 = num9 - 0f;
			float num11 = (float)obj5 * num7;
			float num12 = num10 * num8;
			float num13 = num11 + num12;
			float num14 = position.z - planeOriginPoint.z;
			float num15 = num14 * num6;
			float num16 = num13 + num15;
			Transform transform2 = base.transform;
			float num17 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+6F]");
			float num18 = num17 * 0f;
			float num19 = num16 * num6;
			float num20 = position.z - num19;
			float num21 = num20 + num18;
			Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
			transform2.position = position2;
		}
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

	private void SampleRandomLiftOffsetIfNeeded(bool force)
	{
		if (useRandomLiftOffset)
		{
			if (!force)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj = default(object);
				if (obj == null)
				{
					return;
				}
			}
			float maxInclusive = randomLiftMin;
			float num = randomLiftMin;
			if (randomLiftMin < randomLiftMax)
			{
				maxInclusive = randomLiftMax;
			}
			if (num > randomLiftMax)
			{
				num = randomLiftMax;
			}
			float randomLiftOffset = UnityEngine.Random.Range(num, maxInclusive);
			_randomLiftOffset = randomLiftOffset;
		}
		else
		{
			_randomLiftOffset = 0f;
		}
	}

	private float ResolveLift(BoundedDragSurface3D surf)
	{
		bool flag = !useSurfaceDefaultLift;
		float defaultDragLift = dragLift;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				defaultDragLift = surf.defaultDragLift;
			}
		}
		return defaultDragLift + _randomLiftOffset;
	}

	private unsafe void CaptureGrabOffsetIfNeeded(BoundedDragSurface3D surf, Vector2 screenPos)
	{
		//IL_00d1: Expected I, but got O
		//IL_001f: Expected O, but got Ref
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003f: Expected O, but got Ref
		_ = 0;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		_grabOffsetWorld = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		if (dragAnchorMode == DragAnchorMode.PreserveGrabOffset)
		{
			object obj = default(object);
			Ray ray = cam.ScreenPointToRay((Vector3)(&obj));
			Plane plane = (Plane)(this + 224);
			object obj2 = default(object);
			if (((Plane*)plane)->Raycast((Ray)(&obj2), out var enter))
			{
				object obj3 = default(object);
				float num3 = (float)obj3 * enter;
				Vector3 vector = default(Vector3);
				float num4 = num3 + (float)vector;
				Transform transform = base.transform;
				float num5 = transform.position.z - num4;
				_grabOffsetWorld = vector;
			}
		}
	}

	private void PlaceAccordingToAnchorMode(BoundedDragSurface3D surf, Vector2 screenPos, bool snap)
	{
		if (dragAnchorMode != DragAnchorMode.PreserveGrabOffset)
		{
			ForcePlacePivotUnderCursor(surf, screenPos, snap);
		}
		else
		{
			PlaceWithPreservedGrabOffset(surf, screenPos, snap);
		}
	}

	private unsafe void PlaceWithPreservedGrabOffset(BoundedDragSurface3D surf, Vector2 screenPos, bool snap)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0051: Expected O, but got Ref
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected Ref, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_00fd: Expected O, but got I
		//IL_032c: Invalid comparison between O and F4
		//IL_014b: Expected I, but got O
		//IL_016b: Expected O, but got I
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_01fc: Invalid comparison between I4 and F4
		//IL_026b: Invalid comparison between I4 and F4
		//IL_02b6: Expected F4, but got I4
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 87;
		_ = 0;
		_ = 0;
		Vector2 vector = default(Vector2);
		Ray ray = cam.ScreenPointToRay((Vector3)(&vector));
		Plane plane = (Plane)(this + 224);
		ref float enter = ref *(float*)(obj + 95);
		Ray ray2 = (Ray)(obj - 81);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v4 (UnityEngine.Ray)+10]");
		_ = 0;
		_ = ray.m_Origin;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v4 (UnityEngine.Ray)+10]");
		_ = 0;
		if (!((Plane*)plane)->Raycast(ray2, out enter))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SurfaceHandoffDraggable3D)+E8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-5D]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+5F]");
		object obj3 = num * 0;
		float num3 = default(float);
		float num2 = (float)obj3 + num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj5;
		if (System.Runtime.CompilerServices.Unsafe.As<Plane, UIntPtr>(ref _activePlane) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SurfaceHandoffDraggable3D)+E8]");
			object obj4 = 0 / _activePlane;
			obj5 = obj4;
			nint num4 = (nint)(&vector);
		}
		else
		{
			nint num5 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v411 @ rax_v20 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ rcx_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			obj5 = 0;
		}
		bool flag = !useSurfaceDefaultLift;
		float defaultDragLift = dragLift;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj6 = default(object);
			if (obj6 != null)
			{
				defaultDragLift = surf.defaultDragLift;
			}
		}
		bool flag2 = !clampToSurfaceBounds;
		float num6 = defaultDragLift + _randomLiftOffset;
		float num7 = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SurfaceHandoffDraggable3D)+110]");
		float num8 = num7 + 0f;
		_ = _grabOffsetWorld;
		float num9 = (float)obj5 * num6;
		float num10 = num9 + num8;
		float num11 = num10;
		float num12 = num3;
		if (!flag2)
		{
			Vector3 worldPos = (Vector3)(obj - 113);
			Vector3 vector2 = surf.ClampToSurfaceBoundsPreserveNormalOffset(worldPos);
			num12 = vector2.x;
			num11 = vector2.z;
		}
		Vector3 position2;
		Transform transform3;
		if (!snap && 0f < dragFollowSpeed)
		{
			Transform transform = base.transform;
			Transform transform2 = base.transform;
			Vector3 position = transform2.position;
			_ = position.x;
			float deltaTime = Time.deltaTime;
			float num13 = deltaTime * dragFollowSpeed;
			if (!(0f > num13))
			{
				if (num13 > 1f)
				{
					num13 = 1f;
				}
			}
			else
			{
				num13 = 0f;
			}
			float num14 = num11 - position.z;
			float num15 = num14 * num13;
			float num16 = num15 + position.z;
			position2 = (Vector3)(obj - 113);
			transform3 = transform;
		}
		else
		{
			Transform transform4 = base.transform;
			position2 = (Vector3)(obj - 113);
			transform3 = transform4;
		}
		transform3.position = position2;
	}

	private unsafe void ForcePlacePivotUnderCursor(BoundedDragSurface3D surf, Vector2 screenPos, bool snap)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0051: Expected O, but got Ref
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected Ref, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_00fd: Expected O, but got I
		//IL_032c: Invalid comparison between O and F4
		//IL_014b: Expected I, but got O
		//IL_016b: Expected O, but got I
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_01fc: Invalid comparison between I4 and F4
		//IL_026b: Invalid comparison between I4 and F4
		//IL_02b6: Expected F4, but got I4
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 87;
		_ = 0;
		_ = 0;
		Vector2 vector = default(Vector2);
		Ray ray = cam.ScreenPointToRay((Vector3)(&vector));
		Plane plane = (Plane)(this + 224);
		ref float enter = ref *(float*)(obj + 95);
		Ray ray2 = (Ray)(obj - 81);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v4 (UnityEngine.Ray)+10]");
		_ = 0;
		_ = ray.m_Origin;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v4 (UnityEngine.Ray)+10]");
		_ = 0;
		if (!((Plane*)plane)->Raycast(ray2, out enter))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SurfaceHandoffDraggable3D)+E8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-5D]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+5F]");
		object obj3 = num * 0;
		float num3 = default(float);
		float num2 = (float)obj3 + num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj5;
		if (System.Runtime.CompilerServices.Unsafe.As<Plane, UIntPtr>(ref _activePlane) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SurfaceHandoffDraggable3D)+E8]");
			object obj4 = 0 / _activePlane;
			obj5 = obj4;
			nint num4 = (nint)(&vector);
		}
		else
		{
			nint num5 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ rax_v20 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rcx_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			obj5 = 0;
		}
		bool flag = !useSurfaceDefaultLift;
		float defaultDragLift = dragLift;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj6 = default(object);
			if (obj6 != null)
			{
				defaultDragLift = surf.defaultDragLift;
			}
		}
		bool flag2 = !clampToSurfaceBounds;
		float num6 = defaultDragLift + _randomLiftOffset;
		float num7 = num6 * (float)obj5;
		float num8 = num7 + num2;
		float num9 = num8;
		float num10 = num3;
		if (!flag2)
		{
			Vector3 worldPos = (Vector3)(obj - 113);
			Vector3 vector2 = surf.ClampToSurfaceBoundsPreserveNormalOffset(worldPos);
			num10 = vector2.x;
			num9 = vector2.z;
		}
		Vector3 position2;
		Transform transform3;
		if (!snap && 0f < dragFollowSpeed)
		{
			Transform transform = base.transform;
			Transform transform2 = base.transform;
			Vector3 position = transform2.position;
			_ = position.x;
			float deltaTime = Time.deltaTime;
			float num11 = deltaTime * dragFollowSpeed;
			if (!(0f > num11))
			{
				if (num11 > 1f)
				{
					num11 = 1f;
				}
			}
			else
			{
				num11 = 0f;
			}
			float num12 = num9 - position.z;
			float num13 = num12 * num11;
			float num14 = num13 + position.z;
			position2 = (Vector3)(obj - 113);
			transform3 = transform;
		}
		else
		{
			Transform transform4 = base.transform;
			position2 = (Vector3)(obj - 113);
			transform3 = transform4;
		}
		transform3.position = position2;
	}

	private void TrySurfaceHandoff(Vector2 screenPos)
	{
		//IL_0038: Invalid comparison between F4 and I4
		//IL_030b: Expected O, but got I4
		if (!cam || _handoffCooldownRemaining > 0f)
		{
			return;
		}
		bool flag;
		ref RaycastHit hit = default(ref RaycastHit);
		if (!(clipboardSurface != null))
		{
			flag = false;
		}
		else
		{
			bool flag2 = clipboardSurface.IsPointerOverSurface(cam, screenPos, raycastMaxDistance, out hit);
			flag = flag2;
		}
		bool flag3;
		if (!(mapSurface != null))
		{
			flag3 = false;
		}
		else
		{
			bool flag4 = mapSurface.IsPointerOverSurface(cam, screenPos, raycastMaxDistance, out hit);
			flag3 = flag4;
		}
		object obj = flag & flag3;
		HomeSurface homeSurface;
		string reason;
		Vector2 screenPos2;
		if (obj != null && preferCloserSurfaceOnOverlap)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
			object obj2 = default(object);
			object obj3 = default(object);
			bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
			homeSurface = (HomeSurface)((flag5 ? 1 : 0) + 1);
			if (homeSurface == _currentSurface)
			{
				return;
			}
			reason = "OverlapPreferCloser";
			screenPos2 = screenPos;
		}
		else if (_currentSurface == HomeSurface.Clipboard && mapSurface != null)
		{
			if (!flag3)
			{
				return;
			}
			bool flag6 = clipboardSurface != null;
			bool flag7 = !flag6;
			if (flag6)
			{
				bool flag8 = IsScreenPosInsideSurfaceScreenRect(clipboardSurface, screenPos, exitClipboardMarginPixels);
			}
			if (flag7)
			{
				return;
			}
			reason = "ClipboardExitToMap";
			screenPos2 = screenPos;
			homeSurface = HomeSurface.Map;
		}
		else
		{
			if (_currentSurface != HomeSurface.Map || !(clipboardSurface != null) || !flag)
			{
				return;
			}
			reason = "EnterClipboard";
			screenPos2 = screenPos;
			homeSurface = HomeSurface.Clipboard;
		}
		HandoffTo(homeSurface, screenPos2, reason);
	}

	private void HandoffTo(HomeSurface newSurface, Vector2 screenPos, string reason)
	{
		//IL_00d5: Invalid comparison between I4 and F4
		//IL_00e7: Expected F4, but got I4
		//IL_0529: Expected O, but got I4
		//IL_0129: Expected O, but got I4
		//IL_0303: Expected I4, but got O
		//IL_0298: Expected I, but got O
		//IL_02a8: Expected O, but got I
		//IL_02ca: Expected O, but got I4
		//IL_02d2: Expected F4, but got O
		//IL_0413: Expected I4, but got O
		//IL_0331: Expected I, but got O
		//IL_0341: Expected O, but got I
		//IL_0363: Expected O, but got I4
		//IL_03b0: Expected I, but got O
		//IL_03c0: Expected O, but got I
		//IL_03e2: Expected O, but got I4
		//IL_0441: Expected I, but got O
		//IL_0451: Expected O, but got I
		//IL_0473: Expected O, but got I4
		if (newSurface == _currentSurface)
		{
			return;
		}
		UnityEngine.Object obj = newSurface switch
		{
			HomeSurface.Clipboard => clipboardSurface, 
			HomeSurface.Map => mapSurface, 
			_ => null, 
		};
		if (!obj || !cam)
		{
			return;
		}
		_currentSurface = newSurface;
		bool flag = !(0f < handoffCooldownSeconds);
		float handoffCooldownRemaining = 0f;
		if (!flag)
		{
			handoffCooldownRemaining = handoffCooldownSeconds;
		}
		_handoffCooldownRemaining = handoffCooldownRemaining;
		Transform transform = base.transform;
		bool flag2 = (object)obj == null;
		object obj2 = 0;
		Component component = this;
		if (!flag2)
		{
			Transform parent = ((Component)obj).transform;
			bool flag3 = (object)transform == null;
			obj2 = 0;
			component = (Component)obj;
			if (!flag3)
			{
				transform.SetParent(parent, worldPositionStays: true);
				ApplySurfaceScale((BoundedDragSurface3D)obj, smoothSurfaceScale);
				if (matchSurfaceRotation)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rbx_v2 (UnityEngine.Object)+40]");
					if ((nint)0 != 0)
					{
						ApplySurfaceRotation((BoundedDragSurface3D)obj, smoothSurfaceRotation);
					}
				}
				Plane plane = ((BoundedDragSurface3D)obj).GetPlane();
				Vector3 normal = plane.m_Normal;
				_activePlane = (Plane)plane.m_Normal;
				Vector2 screenPos2 = default(Vector2);
				if (dragAnchorMode != DragAnchorMode.PreserveGrabOffset)
				{
					ForcePlacePivotUnderCursor((BoundedDragSurface3D)obj, screenPos2, snap: true);
				}
				else
				{
					PlaceWithPreservedGrabOffset((BoundedDragSurface3D)obj, screenPos2, snap: true);
				}
				if (!debug)
				{
					return;
				}
				object[] array = new object[4];
				string text = base.name;
				if (text != null)
				{
					nint num = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v623 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
					obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj3 = default(object);
					bool flag4 = obj3 == null;
					string text2 = (string)1;
					handoffCooldownRemaining = (float)normal;
					component = (Component)(object)text;
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj4 = default(object);
						throw obj4;
					}
				}
				array[0] = text;
				object obj6 = default(object);
				object obj5 = (HomeSurface)obj6;
				if (obj5 != null)
				{
					nint num2 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v705 @ rdx_v41 (Il2CppClass<System.Object[]>)+40]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj8 = default(object);
					bool flag5 = obj8 == null;
					string text2 = (string)1;
					object obj9 = obj5;
					if (flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text3 = default(string);
						throw text3;
					}
				}
				array[1] = obj5;
				if (reason != null)
				{
					nint num3 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v730 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj11 = default(object);
					bool flag6 = obj11 == null;
					string text2 = (string)1;
					if (flag6)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj12 = default(object);
						throw obj12;
					}
				}
				array[2] = reason;
				object obj14 = default(object);
				object obj13 = (DragAnchorMode)obj14;
				if (obj13 != null)
				{
					nint num4 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v760 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
					object obj15 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj16 = default(object);
					bool flag7 = obj16 == null;
					string text2 = (string)1;
					object obj17 = obj13;
					if (flag7)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj18 = default(object);
						throw obj18;
					}
				}
				array[3] = obj13;
				string message = string.Format("[{0}] Handoff -> {1} ({2}) anchorMode={3}", array);
				Debug.Log(message, this);
				return;
			}
		}
		throw new NullReferenceException();
	}

	private HomeSurface ChooseSurfaceFromPointer(Vector2 screenPos)
	{
		//IL_0236: Expected I4, but got O
		//IL_0124: Expected O, but got I4
		//IL_013e: Expected O, but got I4
		if ((bool)cam)
		{
			bool flag;
			ref RaycastHit hit = default(ref RaycastHit);
			if (!(clipboardSurface != null))
			{
				flag = false;
			}
			else
			{
				if ((object)clipboardSurface == null)
				{
					goto IL_0228;
				}
				bool flag2 = clipboardSurface.IsPointerOverSurface(cam, screenPos, raycastMaxDistance, out hit);
				flag = flag2;
			}
			bool flag3 = mapSurface != null;
			if (flag3)
			{
				if ((object)mapSurface == null)
				{
					goto IL_0228;
				}
				flag3 = mapSurface.IsPointerOverSurface(cam, screenPos, raycastMaxDistance, out hit);
			}
			object obj = flag & flag3;
			bool flag4 = obj == null;
			object obj2 = !flag4;
			if (obj2 == null)
			{
				if (!flag)
				{
					if (flag3)
					{
						return HomeSurface.Map;
					}
					goto IL_01c2;
				}
			}
			else
			{
				if (preferCloserSurfaceOnOverlap)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
					object obj3 = default(object);
					object obj4 = default(object);
					bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4);
					return (HomeSurface)((flag5 ? 1 : 0) + 1);
				}
				if (_currentSurface != HomeSurface.None)
				{
					goto IL_01c2;
				}
			}
			return HomeSurface.Clipboard;
		}
		return _currentSurface;
		IL_0228:
		NullReferenceException ex = new NullReferenceException();
		return (HomeSurface)ex;
		IL_01c2:
		return _currentSurface;
	}

	private unsafe void ApplySurfaceRotation(BoundedDragSurface3D surface, bool smooth)
	{
		//IL_0111: Expected O, but got Ref
		//IL_00ec: Expected O, but got Ref
		if ((bool)surface)
		{
			Transform transform = surface.transform;
			Quaternion rotation = transform.rotation;
			Quaternion rotation3;
			float num = default(float);
			Transform transform4;
			if (smooth && smoothSurfaceRotation)
			{
				Transform transform2 = base.transform;
				Transform transform3 = base.transform;
				Quaternion rotation2 = transform3.rotation;
				float deltaTime = Time.deltaTime;
				float t = deltaTime * surfaceRotationLerpSpeed;
				Quaternion a = default(Quaternion);
				Quaternion b = default(Quaternion);
				Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, t);
				rotation3 = (Quaternion)(&num);
				transform4 = transform2;
			}
			else
			{
				Transform transform5 = base.transform;
				rotation3 = (Quaternion)(&num);
				transform4 = transform5;
			}
			transform4.rotation = rotation3;
		}
	}

	private unsafe void ApplySurfaceScale(BoundedDragSurface3D surface, bool smooth)
	{
		//IL_016e: Expected O, but got Ref
		//IL_0090: Invalid comparison between I4 and F4
		//IL_010a: Expected O, but got F4
		if (!useSurfaceScaleMultiplier || !surface)
		{
			return;
		}
		float num = surface.surfaceScaleMultiplier;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SurfaceHandoffDraggable3D)+FC]");
		float num2 = num * 0f;
		if (smooth && smoothSurfaceScale && 0f < surfaceScaleTransitionDuration)
		{
			if (_surfaceScaleRoutine != null)
			{
				StopCoroutine(_surfaceScaleRoutine);
			}
			Transform transform = base.transform;
			Vector3 localScale = transform.localScale;
			_003CLerpLocalScaleRoutine_003Ed__74 obj = new _003CLerpLocalScaleRoutine_003Ed__74(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.from = (Vector3)localScale.x;
			Vector3 to = default(Vector3);
			obj.to = to;
			obj.duration = surfaceScaleTransitionDuration;
			_ = localScale.z;
			Coroutine surfaceScaleRoutine = StartCoroutine(obj);
			_surfaceScaleRoutine = surfaceScaleRoutine;
		}
		else
		{
			Transform transform2 = base.transform;
			Vector3 vector = default(Vector3);
			transform2.localScale = (Vector3)(&vector);
		}
	}

	private IEnumerator LerpLocalScaleRoutine(Vector3 from, Vector3 to, float duration)
	{
		//IL_0017: Expected O, but got F4
		//IL_0029: Expected O, but got F4
		_003CLerpLocalScaleRoutine_003Ed__74 obj = new _003CLerpLocalScaleRoutine_003Ed__74(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.from = (Vector3)from.x;
		obj.to = (Vector3)to.x;
		_ = to.z;
		obj.duration = duration;
		_ = from.z;
		return obj;
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

	private bool ResolveReferencesByTag(bool logWarnings)
	{
		//IL_06ac: Expected I4, but got O
		bool flag = cam;
		bool result = false;
		if (!flag)
		{
			bool flag2 = string.IsNullOrEmpty(raycastCameraTag);
			result = false;
			if (!flag2)
			{
				GameObject gameObject = GameObject.FindWithTag(raycastCameraTag);
				if (!(gameObject != null))
				{
					bool flag3 = !logWarnings;
					result = false;
					if (!flag3)
					{
						string text = base.name;
						string message = text + ": No camera found with tag '" + raycastCameraTag + "'.";
						Debug.LogWarning(message, this);
						result = false;
					}
				}
				else
				{
					if ((object)gameObject == null)
					{
						goto IL_069e;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (obj == null)
					{
						bool flag4 = !logWarnings;
						result = false;
						if (!flag4)
						{
							string text2 = base.name;
							string message2 = text2 + ": Tagged camera object has no Camera component.";
							Debug.LogWarning(message2, gameObject);
							result = false;
						}
					}
					else
					{
						cam = (Camera)obj;
						result = true;
					}
				}
			}
		}
		if (!cam)
		{
			Camera main = Camera.main;
			cam = main;
			if (cam != null)
			{
				result = true;
			}
		}
		if (!this.virtualCursor && !string.IsNullOrEmpty(virtualCursorTag))
		{
			GameObject gameObject2 = GameObject.FindWithTag(virtualCursorTag);
			if (!(gameObject2 != null))
			{
				if (logWarnings)
				{
					string text3 = base.name;
					string message3 = text3 + ": No VirtualCursor found with tag '" + virtualCursorTag + "'.";
					Debug.LogWarning(message3, this);
				}
			}
			else
			{
				if ((object)gameObject2 == null)
				{
					goto IL_069e;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				VirtualCursor virtualCursor = default(VirtualCursor);
				this.virtualCursor = virtualCursor;
				if (this.virtualCursor == null)
				{
					if (logWarnings)
					{
						string text4 = base.name;
						string message4 = text4 + ": VirtualCursor tag object has no VirtualCursor component.";
						Debug.LogWarning(message4, gameObject2);
					}
				}
				else
				{
					result = true;
				}
			}
		}
		if (!clipboardSurface && !string.IsNullOrEmpty(clipboardSurfaceTag))
		{
			GameObject gameObject3 = GameObject.FindWithTag(clipboardSurfaceTag);
			if (!(gameObject3 != null))
			{
				if (logWarnings)
				{
					string text5 = base.name;
					string message5 = text5 + ": No clipboard surface found with tag '" + clipboardSurfaceTag + "'.";
					Debug.LogWarning(message5, this);
				}
			}
			else
			{
				if ((object)gameObject3 == null)
				{
					goto IL_069e;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				BoundedDragSurface3D boundedDragSurface3D = default(BoundedDragSurface3D);
				clipboardSurface = boundedDragSurface3D;
				if (clipboardSurface == null)
				{
					if (logWarnings)
					{
						string text6 = base.name;
						string message6 = text6 + ": ClipboardSurface tag object has no BoundedDragSurface3D.";
						Debug.LogWarning(message6, gameObject3);
					}
				}
				else
				{
					result = true;
				}
			}
		}
		UnityEngine.Object context;
		object message7;
		if (!mapSurface && !string.IsNullOrEmpty(mapSurfaceTag))
		{
			GameObject gameObject4 = GameObject.FindWithTag(mapSurfaceTag);
			if (!(gameObject4 != null))
			{
				if (logWarnings)
				{
					string text7 = base.name;
					string text8 = text7 + ": No map surface found with tag '" + mapSurfaceTag + "'.";
					context = this;
					message7 = text8;
					goto IL_06c5;
				}
			}
			else
			{
				if ((object)gameObject4 == null)
				{
					goto IL_069e;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				BoundedDragSurface3D boundedDragSurface3D2 = default(BoundedDragSurface3D);
				mapSurface = boundedDragSurface3D2;
				if (mapSurface == null)
				{
					if (logWarnings)
					{
						string text9 = base.name;
						string text10 = text9 + ": MapSurface tag object has no BoundedDragSurface3D.";
						context = gameObject4;
						message7 = text10;
						goto IL_06c5;
					}
				}
				else
				{
					result = true;
				}
			}
		}
		goto IL_05dd;
		IL_06c5:
		Debug.LogWarning(message7, context);
		goto IL_05dd;
		IL_05dd:
		return result;
		IL_069e:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private BoundedDragSurface3D GetSurface(HomeSurface s)
	{
		return s switch
		{
			HomeSurface.Clipboard => clipboardSurface, 
			HomeSurface.Map => mapSurface, 
			_ => null, 
		};
	}

	private unsafe bool IsScreenPosInsideSurfaceScreenRect(BoundedDragSurface3D surface, Vector2 screenPos, float marginPixels)
	{
		//IL_0104: Expected O, but got I4
		//IL_0111: Expected O, but got I8
		//IL_011a: Expected O, but got I4
		//IL_04a9: Expected O, but got I8
		//IL_0474: Expected O, but got I8
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Expected O, but got Unknown
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Expected O, but got Unknown
		//IL_012c: Expected O, but got Ref
		//IL_0428: Expected I4, but got O
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_0224: Expected O, but got I8
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Expected O, but got Unknown
		//IL_029f: Expected O, but got I4
		//IL_0370: Invalid comparison between O and F4
		//IL_039b: Invalid comparison between F4 and O
		//IL_02d4: Expected O, but got Ref
		//IL_02e6: Invalid comparison between I4 and F4
		//IL_03c6: Invalid comparison between O and F4
		//IL_03f1: Invalid comparison between F4 and O
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Expected O, but got Unknown
		//IL_0551: Expected F4, but got O
		if ((bool)surface && (bool)surface.boundsBox && (bool)cam)
		{
			Transform transform = surface.boundsBox.transform;
			Vector3 center = surface.boundsBox.center;
			Vector3 size = surface.boundsBox.size;
			object obj = default(object);
			float num = (float)obj * 0.5f;
			float num2 = size.x * 0.5f;
			Vector3[] array = new Vector3[8];
			object obj2 = 0;
			object obj3 = 4294967295L;
			object obj4 = 0;
			object obj6 = default(object);
			float num7 = default(float);
			object obj14 = default(object);
			while (true)
			{
				float num3 = (float)obj3 * num2;
				float num4 = num3 + center.x;
				object obj5 = 4294967295L;
				float num6;
				object obj8;
				object obj9;
				while (true)
				{
					float num5 = (float)obj5 * num;
					num6 = num5 + (float)obj6;
					object obj7 = 4294967295L;
					while (true)
					{
						obj8 = obj4 + 1;
						obj9 = obj2 + 1;
						Vector3 vector = transform.TransformPoint((Vector3)(&num7));
						if ((nint)obj4 >= array.Length)
						{
							break;
						}
						object obj10 = obj2 * 2;
						object obj11 = obj2 + obj10;
						_ = vector.x;
						obj7 += 2;
						_ = vector.z;
						bool flag = (nint)obj7 <= 1;
						num7 = num4;
						obj2 = obj9;
						obj4 = obj8;
						if (flag)
						{
							continue;
						}
						goto IL_01cb;
					}
					break;
					IL_01cb:
					obj5 += 2;
					bool flag2 = (nint)obj5 <= 1;
					num7 = num4;
					obj2 = obj9;
					obj4 = obj8;
					if (flag2)
					{
						continue;
					}
					goto IL_0211;
				}
				goto IL_041a;
				IL_041a:
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
				IL_0211:
				obj3 = 4294967295L + 2;
				bool flag3 = (nint)obj3 <= 1;
				num7 = num4;
				obj2 = obj9;
				obj4 = obj8;
				if (flag3)
				{
					continue;
				}
				object obj12 = array + 32;
				float num8 = 1f / 0f;
				num7 = num4;
				float num9 = -1f / 0f;
				float num10 = 1f / 0f;
				float num11 = -1f / 0f;
				object obj13 = 0;
				while (true)
				{
					if ((nint)obj13 < array.Length)
					{
						if ((nint)obj13 >= array.Length)
						{
							break;
						}
						Vector3 vector2 = cam.WorldToScreenPoint((Vector3)(&num7));
						if (0f > vector2.z)
						{
							goto end_IL_0479;
						}
						if (!(vector2.x > num8))
						{
							num8 = vector2.x;
						}
						if (!(num6 > num10))
						{
							num10 = num6;
						}
						if (!(num11 > vector2.x))
						{
							num11 = vector2.x;
						}
						if (!(num9 > num6))
						{
							num9 = num6;
						}
						obj13++;
						obj12 += 12;
						num7 = (float)obj12;
						continue;
					}
					float num12 = num8 - marginPixels;
					if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref screenPos) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num12))
					{
						goto end_IL_0479;
					}
					float num13 = num11 + marginPixels;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num13) < System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref screenPos))
					{
						goto end_IL_0479;
					}
					float num14 = num10 - marginPixels;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num14))
					{
						goto end_IL_0479;
					}
					float num15 = num9 + marginPixels;
					bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num15) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14);
					return !flag4;
				}
				goto IL_041a;
				continue;
				end_IL_0479:
				break;
			}
		}
		return false;
	}

	private void StopAllLocalCoroutines()
	{
		if (_surfaceScaleRoutine != null)
		{
			StopCoroutine(_surfaceScaleRoutine);
			_surfaceScaleRoutine = null;
		}
	}

	private static float SmoothStep01(float t)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_005c: Expected F4, but got I4
		float num;
		if (!(0f > t))
		{
			bool flag = !(t > 1f);
			num = t;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num2 = num + num;
		float num3 = num * num;
		float num4 = 3f - num2;
		return num4 * num3;
	}

	public SurfaceHandoffDraggable3D()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A872]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoResolveByTag = true;
		raycastCameraTag = "MainCamera";
		virtualCursorTag = "VirtualCursor";
		clipboardSurfaceTag = "ClipboardSurface";
		mapSurfaceTag = "MapSurface";
		dragLift = 0.02f;
		useSurfaceDefaultLift = true;
		dragFollowSpeed = 22f;
		clampToSurfaceBounds = true;
		randomLiftMin = -0.003f;
		randomLiftMax = -0.001f;
		matchSurfaceRotation = true;
		surfaceRotationLerpSpeed = 18f;
		enableSurfaceHandoff = true;
		exitClipboardMarginPixels = 24f;
		raycastMaxDistance = 1000f;
		handoffCooldownSeconds = 0.1f;
		preferCloserSurfaceOnOverlap = true;
		smoothSurfaceScale = true;
		surfaceScaleTransitionDuration = 0.18f;
		base._002Ector();
	}
}
