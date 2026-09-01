using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DraggableItem : MonoBehaviour, ICursorDraggable
{
	public enum ItemLocation
	{
		Deck,
		Surface,
		Slot
	}

	public enum DragAnchorMode
	{
		PivotUnderCursor,
		PreserveGrabOffset
	}

	public enum EjectAxis
	{
		PositiveX,
		NegativeX,
		PositiveY,
		NegativeY
	}

	private sealed class _003CLerpScaleRoutine_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public DraggableItem _003C_003E4__this;

		public Vector3 from;

		public Vector3 to;

		private float _003Ct_003E5__2;

		private float _003Cdur_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLerpScaleRoutine_003Ed__94(int _003C_003E1__state)
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
			//IL_0229: Expected I4, but got O
			//IL_0200: Expected O, but got Ref
			//IL_0162: Invalid comparison between O and F4
			//IL_018b: Expected O, but got Ref
			DraggableItem draggableItem = _003C_003E4__this;
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
				float num3 = num2 + _003Ct_003E5__2;
				_003Ct_003E5__2 = num3;
				if ((object)_003C_003E4__this != null)
				{
					Transform transform = _003C_003E4__this.transform;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180459520");
					object obj2 = default(object);
					if (0 > (nint)obj2 || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
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
					draggableItem._scaleRoutine = null;
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

	private sealed class _003CSlideCoroutine_003Ed__105 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItem _003C_003E4__this;

		public DragSurface surf;

		public Vector3 target;

		public float duration;

		private Transform _003CexpectedParent_003E5__2;

		private Vector3 _003CstartLocal_003E5__3;

		private Vector3 _003CtargetLocal_003E5__4;

		private Vector3 _003ClocalNormal_003E5__5;

		private float _003Celapsed_003E5__6;

		private float _003Cdur_003E5__7;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CSlideCoroutine_003Ed__105(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0369: Expected I4, but got I8
			//IL_0397: Expected O, but got I4
			//IL_0087: Expected O, but got F4
			//IL_061a: Unknown result type (might be due to invalid IL or missing references)
			//IL_061f: Expected O, but got Unknown
			//IL_0433: Invalid comparison between I4 and F4
			//IL_047e: Expected F4, but got I4
			//IL_0126: Expected F4, but got O
			//IL_0136: Expected F4, but got I
			//IL_095c: Expected O, but got F4
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			//IL_0117: Expected O, but got I4
			//IL_04d8: Invalid comparison between I4 and F4
			//IL_03e7: Expected O, but got I4
			//IL_0523: Expected F4, but got I4
			//IL_06c8: Expected O, but got I4
			//IL_0a79: Expected O, but got I
			//IL_0abc: Expected O, but got I
			//IL_09f6: Expected I, but got O
			//IL_0a1f: Expected F4, but got I
			//IL_0531: Unknown result type (might be due to invalid IL or missing references)
			//IL_0536: Expected O, but got Unknown
			//IL_0ae3: Expected I4, but got O
			//IL_0714: Unknown result type (might be due to invalid IL or missing references)
			//IL_0719: Expected O, but got Unknown
			//IL_0245: Expected O, but got I4
			//IL_01c3: Expected O, but got I4
			//IL_074f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0754: Expected O, but got Unknown
			//IL_077e: Expected O, but got I4
			//IL_0787: Expected O, but got I4
			//IL_0218: Expected O, but got I4
			//IL_096f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0974: Expected O, but got Unknown
			//IL_02a6: Expected I, but got O
			//IL_02c6: Expected F4, but got I
			//IL_02da: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Expected O, but got Unknown
			//IL_0312: Expected O, but got I4
			//IL_031f: Expected O, but got F4
			//IL_0893: Unknown result type (might be due to invalid IL or missing references)
			//IL_0898: Expected O, but got Unknown
			Component component = _003C_003E4__this;
			object obj = default(object);
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_ = 1;
				Transform transform = ((!(surf != null)) ? null : surf.transform);
				_003CexpectedParent_003E5__2 = transform;
				Transform transform2 = _003C_003E4__this.transform;
				Vector3 localPosition = transform2.localPosition;
				_003CstartLocal_003E5__3 = (Vector3)localPosition.x;
				_ = localPosition.z;
				float num;
				if (_003CexpectedParent_003E5__2 != null)
				{
					Vector3 position = (Vector3)(obj - 80);
					_ = target;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+38]");
					_ = 0;
					Vector3 vector = _003CexpectedParent_003E5__2.InverseTransformPoint(position);
					num = vector.x;
					float z = vector.z;
					object obj2 = 0;
				}
				else
				{
					num = (float)target;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+38]");
					float z = 0f;
				}
				_003CtargetLocal_003E5__4 = (Vector3)num;
				Vector3 vector4;
				if (_003CexpectedParent_003E5__2 != null)
				{
					Component component2 = surf;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v70 (UnityEngine.Component)+20]");
					Vector3 vector2;
					if ((nint)0 == 1)
					{
						Transform transform3 = component2.transform;
						vector2 = transform3.forward;
						object obj3 = 0;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v70 (UnityEngine.Component)+20]");
						if ((nint)0 == 2)
						{
							Transform transform4 = component2.transform;
							vector2 = transform4.right;
							object obj3 = 0;
						}
						else
						{
							Transform transform5 = component2.transform;
							vector2 = transform5.up;
							object obj3 = 0;
						}
					}
					_ = vector2.x;
					_ = vector2.x;
					_ = vector2.z;
					object obj4 = obj - 64;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					if (vector2.x > 1E-05f)
					{
						float num2 = vector2.z / vector2.x;
						float num3 = num2;
					}
					else
					{
						nint num4 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1422 @ rax_v89 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1423 @ rcx_v77 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						float num3 = 0f;
						_ = Vector3.zeroVector;
					}
					Vector3 direction = (Vector3)(obj - 64);
					Vector3 vector3 = _003CexpectedParent_003E5__2.InverseTransformDirection(direction);
					float z2 = vector3.z;
					object obj2 = 0;
					vector4 = (Vector3)vector3.x;
				}
				else
				{
					nint num6 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1287 @ rax_v81 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num7 = 0;
					vector4 = Vector3.forwardVector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1288 @ rcx_v67 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
					float z2 = 0f;
				}
				_003ClocalNormal_003E5__5 = vector4;
				_003Celapsed_003E5__6 = 0f;
				bool flag = !(0.0001f < duration);
				float num8 = 0.0001f;
				if (!flag)
				{
					num8 = duration;
				}
				_003Cdur_003E5__7 = num8;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0a47;
				}
				_003C_003E1__state = -1;
			}
			if (_003Cdur_003E5__7 > _003Celapsed_003E5__6)
			{
				bool flag2 = _003CexpectedParent_003E5__2 != null;
				bool flag3 = !flag2;
				object obj5 = 0;
				if (!flag3)
				{
					Transform transform6 = _003C_003E4__this.transform;
					Transform parent = transform6.parent;
					bool flag4 = parent == _003CexpectedParent_003E5__2;
					obj5 = 0;
					if (!flag4)
					{
						goto IL_05f2;
					}
				}
				float deltaTime = Time.deltaTime;
				float num9 = (_003Celapsed_003E5__6 = deltaTime + _003Celapsed_003E5__6) / _003Cdur_003E5__7;
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
				Transform transform7 = _003C_003E4__this.transform;
				_ = _003CtargetLocal_003E5__4;
				_ = _003CstartLocal_003E5__3;
				float num10 = 1f - num9;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num11 = 1f - num10;
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
				_ = _003ClocalNormal_003E5__5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+5C]");
				nint num12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+50]");
				object obj6 = num12 - 0;
				float num13 = (float)obj6 * num11;
				float num14 = num13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+50]");
				float num15 = num14 + 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+68]");
				nint num16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rsi_v1 (UnityEngine.Component)+C4]");
				object obj7 = num16 * 0;
				float num17 = num15 + (float)obj7;
				Vector3 localPosition2 = (Vector3)(obj - 64);
				transform7.localPosition = localPosition2;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003CexpectedParent_003E5__2 != null)
			{
				Transform transform8 = _003C_003E4__this.transform;
				Transform parent2 = transform8.parent;
				if (parent2 != _003CexpectedParent_003E5__2)
				{
					goto IL_05f2;
				}
			}
			Transform transform9 = _003C_003E4__this.transform;
			Vector3 localPosition3 = (Vector3)(obj - 64);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem+<SlideCoroutine>d__105)+5C]");
			_ = 0;
			_ = _003CtargetLocal_003E5__4;
			transform9.localPosition = localPosition3;
			_ = 0;
			if (surf != null)
			{
				_003C_003E4__this.ComputeStackingOffset(surf);
				_003C_003E4__this.ApplyFinalRestingPosition(surf);
				DragSurface dragSurface = surf;
				bool flag5 = !dragSurface.clampToBounds;
				object obj8 = 0;
				if (!flag5)
				{
					Transform transform10 = _003C_003E4__this.transform;
					Transform transform11 = _003C_003E4__this.transform;
					Vector3 position2 = transform11.position;
					Vector3 worldPos = (Vector3)(obj - 64);
					_ = position2.x;
					_ = position2.z;
					Vector3 vector5 = surf.ClampOnSurfacePreserveNormalOffset(worldPos);
					Vector3 position3 = (Vector3)(obj - 64);
					_ = vector5.x;
					_ = vector5.z;
					transform10.position = position3;
					object obj2 = 0;
					obj8 = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rsi_v1 (UnityEngine.Component)+C8]");
				if ((nint)0 != 0)
				{
					string[] array = new string[6];
					if (array != null)
					{
						array[0] = "[";
						string name = _003C_003E4__this.name;
						array[1] = name;
						array[2] = "] Slide settled on '";
						if ((object)surf != null)
						{
							string name2 = surf.name;
							array[3] = name2;
							array[4] = "'. ";
							object obj9 = obj + 32;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rsi_v1 (UnityEngine.Component)+D8]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg = default(object);
							string text = $"StackingNormalOffset={arg:F4}";
							array[5] = text;
							string message = string.Concat(array);
							Debug.Log(message, _003C_003E4__this);
							goto IL_0a47;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
			}
			goto IL_0a47;
			IL_05f2:
			_ = 0;
			goto IL_0a47;
			IL_0a47:
			return false;
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

	private Action m_DragStarted;

	private Action m_DragEnded;

	public ItemLocation CurrentLocation;

	public bool IsBeingDragged;

	public bool showPickupTooltip;

	public DraggableItemDeckArea deckRef;

	public DragSurface surfaceRef;

	public List<ItemSlot> slotRefs;

	public string dynamicSlotTag;

	public Collider Col;

	public UnityEvent OnPickedUpByPlayer;

	public UnityEvent OnReleasedByPlayer;

	public UnityEvent OnPickedUpToClipboard;

	public UnityEvent<GameObject> OnSlottedIntoSlot;

	public UnityEvent<GameObject> OnRemovedFromSlot;

	public DragAnchorMode dragAnchorMode;

	public float dragLift;

	public bool useSurfaceDefaultLift;

	public float dragFollowSpeed;

	public float pullThresholdPixels;

	public bool enableStackingOffset;

	public float stackingOffsetDonated;

	public float stackingDetectionRadius;

	public bool matchSurfaceRotation;

	public bool smoothSurfaceRotation;

	public float surfaceRotationLerpSpeed;

	public bool useSurfaceScaleMultiplier;

	public bool smoothSurfaceScale;

	public float surfaceScaleTransitionDuration;

	public bool enableSurfaceHandoff;

	public float handoffCooldownSeconds;

	public float handoffRaycastMaxDistance;

	public float ejectSlideLift;

	public bool debugDrag;

	private Vector3 _baseLocalScale;

	[NonSerialized]
	public float StackingNormalOffset;

	[NonSerialized]
	public bool IsSliding;

	private float _dragStackingOffset;

	private bool _externallyControlled;

	private Camera _dragCamera;

	private Vector3 _grabOffsetWorld;

	private Plane _activePlane;

	private float _handoffCooldownRemaining;

	private DragSurface _activeSurface;

	private bool _leftDeckThisDrag;

	private Vector2 _lastScreenPos;

	private Coroutine _scaleRoutine;

	private VirtualCursor _cachedVirtualCursor;

	private const EjectAxis k_DefaultEjectAxis = EjectAxis.NegativeX;

	private const float k_DefaultEjectDistance = 0.8f;

	private const float k_DefaultEjectDistanceRandomness = 0.4f;

	private const float k_DefaultSpreadAmount = 0.15f;

	private const float k_DefaultEjectSlideDuration = 0.35f;

	public bool IsDragging => IsBeingDragged;

	public ItemSlot SlotRef
	{
		get
		{
			//IL_0080: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ItemSlot>.Enumerator enumerator = default(List<ItemSlot>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj != null)
					{
						if ((object)obj == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ stack_8_v4 (UnityEngine.Object)+20]");
						if ((UnityEngine.Object)0 == this)
						{
							enumerator.Dispose();
							return (ItemSlot)obj;
						}
					}
					continue;
				}
				enumerator.Dispose();
				return null;
			}
			throw new NullReferenceException();
		}
	}

	public unsafe Vector3 BaseLocalScale
	{
		get
		{
			//IL_0093: Expected I, but got O
			//IL_00dd: Expected O, but got I
			//IL_0127: Invalid comparison between F4 and O
			//IL_0062: Expected F4, but got O
			//IL_0072: Expected F4, but got I
			//IL_0146: Expected native int or pointer, but got O
			//IL_0153: Expected native int or pointer, but got O
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			object obj = _baseLocalScale - Vector3.zeroVector;
			object obj3 = default(object);
			object obj4 = default(object);
			object obj2 = obj3 - obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (DraggableItem)+D4]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			object obj5 = num3 - 0;
			object obj6 = obj * obj;
			object obj7 = obj2 * obj2;
			object obj8 = obj5 * obj5;
			object obj9 = obj6 + obj7;
			object obj10 = obj9 + obj8;
			float x;
			float z;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
			{
				Transform transform = base.transform;
				if ((object)transform == null)
				{
					return (Vector3)new NullReferenceException();
				}
				Vector3 localScale = transform.localScale;
				x = localScale.x;
				z = localScale.z;
			}
			else
			{
				x = (float)_baseLocalScale;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (DraggableItem)+D4]");
				z = 0f;
			}
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
	}

	public event Action DragStarted
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 32;
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
			object obj = this + 32;
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
			object obj = this + 40;
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
			object obj = this + 40;
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
		//IL_00da: Expected O, but got F4
		if (Col == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Collider col = default(Collider);
			Col = col;
		}
		if (Col == null)
		{
			string text = base.name;
			string message = "[DraggableItem] '" + text + "' has no Collider. Pointer hit-testing and overlap checks will not work. Add a Collider component to this GameObject.";
			Debug.LogWarning(message, this);
		}
		Transform transform = base.transform;
		Vector3 localScale = transform.localScale;
		_baseLocalScale = (Vector3)localScale.x;
		_ = localScale.z;
	}

	private void OnDisable()
	{
		if (IsBeingDragged)
		{
			_externallyControlled = false;
			EndDragInternal();
		}
	}

	public void SetReferences(DragSurface surface, DraggableItemDeckArea deck, ItemSlot slot)
	{
		surfaceRef = surface;
		deckRef = deck;
		AddSlotRef(slot);
	}

	public void SetReferences(DragSurface surface, DraggableItemDeckArea deck, List<ItemSlot> slots)
	{
		surfaceRef = surface;
		deckRef = deck;
		if (slots != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ItemSlot>.Enumerator enumerator = default(List<ItemSlot>.Enumerator);
			ItemSlot slot = default(ItemSlot);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				AddSlotRef(slot);
			}
			enumerator.Dispose();
		}
	}

	public void AddSlotRef(ItemSlot slot)
	{
		if (slot != null && !slotRefs.Contains(slot))
		{
			slotRefs.Add(slot);
		}
	}

	public void RemoveSlotRef(ItemSlot slot)
	{
		bool flag = slotRefs.Remove(slot);
	}

	public void SetState(ItemLocation newLoc, DraggableItemDeckArea deck, DragSurface surface, ItemSlot slot)
	{
		CurrentLocation = newLoc;
		if (deck != null)
		{
			deckRef = deck;
		}
		if (surface != null)
		{
			surfaceRef = surface;
		}
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (obj != null)
		{
			AddSlotRef((ItemSlot)obj);
		}
	}

	public void ResnapshotBaseScale()
	{
		//IL_0032: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localScale = transform.localScale;
		bool flag = !debugDrag;
		_baseLocalScale = (Vector3)localScale.x;
		_ = localScale.z;
		if (!flag)
		{
			string arg = base.name;
			object obj = default(object);
			object arg2 = (Vector3)obj;
			string message = $"[{arg}] ResnapshotBaseScale: _baseLocalScale set to {arg2}";
			Debug.Log(message, this);
		}
	}

	public void ApplySurfaceScaleForSurface(DragSurface surf, bool smooth = false)
	{
		ApplySurfaceScale(surf, smooth);
	}

	public void BeginDragFromManager(Camera raycastCamera, Vector2 screenPos)
	{
		if (base.isActiveAndEnabled)
		{
			if (_scaleRoutine != null)
			{
				StopCoroutine(_scaleRoutine);
				_scaleRoutine = null;
			}
			bool flag = raycastCamera == null;
			Camera dragCamera = raycastCamera;
			if (flag)
			{
				Camera main = Camera.main;
				dragCamera = main;
			}
			_dragCamera = dragCamera;
			if ((bool)_dragCamera)
			{
				_externallyControlled = true;
				StartDragInternal(screenPos);
			}
		}
	}

	public void EndDragFromManager()
	{
		bool flag = !IsBeingDragged;
		_externallyControlled = false;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 13 Invalid \"Jump target not found in method: 0x180518BB0\"");
		}
	}

	public void HandlePickedUpToClipboard()
	{
		if (OnPickedUpToClipboard != null)
		{
			OnPickedUpToClipboard.Invoke();
		}
	}

	private unsafe void Update()
	{
		//IL_0067: Invalid comparison between F4 and I4
		//IL_015e: Expected F4, but got I
		//IL_016e: Expected F4, but got I
		//IL_05dd: Invalid comparison between F4 and I4
		//IL_0591: Expected O, but got F4
		//IL_0498: Expected O, but got Ref
		//IL_04ae: Expected O, but got Ref
		if (!IsBeingDragged || !_externallyControlled)
		{
			return;
		}
		if (_handoffCooldownRemaining > 0f)
		{
			float deltaTime = Time.deltaTime;
			float handoffCooldownRemaining = _handoffCooldownRemaining - deltaTime;
			_handoffCooldownRemaining = handoffCooldownRemaining;
		}
		if (!_cachedVirtualCursor)
		{
			GameObject gameObject = GameObject.FindWithTag("VirtualCursor");
			if ((bool)gameObject)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				VirtualCursor cachedVirtualCursor = default(VirtualCursor);
				_cachedVirtualCursor = cachedVirtualCursor;
			}
		}
		UnityEngine.Object cachedVirtualCursor2 = _cachedVirtualCursor;
		float num;
		float num2;
		if (_cachedVirtualCursor != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ rdi_v6 (UnityEngine.Object)+6C]");
			num = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ rdi_v6 (UnityEngine.Object)+70]");
			num2 = 0f;
		}
		else
		{
			int width = Screen.width;
			num = (float)width * 0.5f;
			int height = Screen.height;
			num2 = (float)height * 0.5f;
		}
		Vector2 screenPos = default(Vector2);
		if ((bool)_activeSurface && (bool)_dragCamera)
		{
			_activePlane = (Plane)_activeSurface.GetSurfacePlane().m_Normal;
			PlaceOnSurface(_activeSurface, screenPos, snap: false);
			if (matchSurfaceRotation)
			{
				DragSurface activeSurface = _activeSurface;
				if (activeSurface.preferAlignRotationOnEnter && smoothSurfaceRotation)
				{
					ApplySurfaceRotation(activeSurface, smooth: true);
				}
			}
		}
		if (CurrentLocation == ItemLocation.Deck && !_leftDeckThisDrag)
		{
			float num3 = num - (float)_lastScreenPos;
			float num4 = pullThresholdPixels * pullThresholdPixels;
			float num5 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem)+120]");
			float num6 = num5 - 0f;
			float num7 = num3 * num3;
			float num8 = num6 * num6;
			float num9 = num8 + num7;
			if (!(num9 < num4))
			{
				if (CurrentLocation == ItemLocation.Deck)
				{
					if (deckRef != null)
					{
						deckRef.RemoveItem(this);
					}
					bool flag = (object)_activeSurface != null;
					UnityEngine.Object activeSurface2 = _activeSurface;
					if (!flag)
					{
						activeSurface2 = surfaceRef;
					}
					if (activeSurface2 != null)
					{
						Transform transform = base.transform;
						Transform parent = ((Component)activeSurface2).transform;
						transform.SetParent(parent, worldPositionStays: true);
						((DragSurface)activeSurface2).AddItem(this);
						Transform transform2 = base.transform;
						Transform transform3 = base.transform;
						Vector3 position = transform3.position;
						float num10 = default(float);
						Vector3 vector = ((DragSurface)activeSurface2).ProjectOntoSurface((Vector3)(&num10));
						transform2.position = (Vector3)(&num10);
						CurrentLocation = ItemLocation.Surface;
					}
				}
				_leftDeckThisDrag = true;
			}
			_lastScreenPos = (Vector2)num;
		}
		if (enableSurfaceHandoff && !(_handoffCooldownRemaining > 0f) && (bool)_dragCamera)
		{
			DragSurface dragSurface = FindBestSurfaceUnderPointer(screenPos);
			if (dragSurface != null && dragSurface != _activeSurface)
			{
				HandoffTo(dragSurface, screenPos);
			}
		}
	}

	private unsafe void StartDragInternal(Vector2 pressScreenPos)
	{
		//IL_06d3: Expected I, but got O
		//IL_00f4: Expected O, but got Ref
		//IL_010a: Expected O, but got Ref
		//IL_0313: Expected O, but got Ref
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Expected O, but got Unknown
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Expected O, but got Unknown
		//IL_055e: Expected I4, but got O
		//IL_063e: Expected O, but got I
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Expected O, but got Unknown
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Expected O, but got Unknown
		//IL_0733: Expected F4, but got O
		//IL_0697: Expected O, but got F4
		//IL_03e0: Expected O, but got I4
		//IL_0440: Expected O, but got I4
		//IL_0457: Expected O, but got F4
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		ItemSlot slotRef = SlotRef;
		float x = default(float);
		if (CurrentLocation == ItemLocation.Slot && slotRef != null)
		{
			slotRef.RemoveItem(this);
			if (surfaceRef != null)
			{
				Transform transform = base.transform;
				Transform parent = surfaceRef.transform;
				transform.SetParent(parent, worldPositionStays: true);
				surfaceRef.AddItem(this);
				Transform transform2 = base.transform;
				Transform transform3 = base.transform;
				Vector3 position = transform3.position;
				Vector3 vector = surfaceRef.ProjectOntoSurface((Vector3)(&x));
				transform2.position = (Vector3)(&x);
				x = vector.x;
			}
			CurrentLocation = ItemLocation.Surface;
			if (OnRemovedFromSlot != null)
			{
				GameObject arg = slotRef.gameObject;
				OnRemovedFromSlot.Invoke(arg);
			}
		}
		IsBeingDragged = true;
		_handoffCooldownRemaining = 0f;
		_leftDeckThisDrag = false;
		_lastScreenPos = pressScreenPos;
		DragSurface dragSurface = FindBestSurfaceUnderPointer(pressScreenPos);
		if ((object)dragSurface == null)
		{
			dragSurface = surfaceRef;
		}
		_activeSurface = dragSurface;
		if (_activeSurface != null)
		{
			Transform transform4 = base.transform;
			Transform parent2 = _activeSurface.transform;
			transform4.SetParent(parent2, worldPositionStays: true);
			Transform transform5 = base.transform;
			transform5.SetAsLastSibling();
			ApplySurfaceScale(_activeSurface, smooth: false);
			if (matchSurfaceRotation)
			{
				DragSurface activeSurface = _activeSurface;
				if (activeSurface.preferAlignRotationOnEnter)
				{
					ApplySurfaceRotation(activeSurface, smooth: false);
				}
			}
			Plane surfacePlane = _activeSurface.GetSurfacePlane();
			_activePlane = (Plane)surfacePlane.m_Normal;
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v778 @ rax_v22 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			Vector3 zeroVector = Vector3.zeroVector;
			_grabOffsetWorld = Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v779 @ rcx_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			_ = 0;
			if (dragAnchorMode == DragAnchorMode.PreserveGrabOffset && (bool)_dragCamera)
			{
				object obj = (object)_dragCamera.ScreenPointToRay((Vector3)(&x)).m_Origin * (object)surfacePlane.m_Normal;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v908 @ rax_v41 (UnityEngine.Ray)+10]");
				Vector3 vector2 = default(Vector3);
				object obj2 = 0 * vector2;
				object obj3 = (object)vector2 * (object)surfacePlane.m_Normal;
				object obj4 = (object)vector2 * (object)vector2;
				object obj5 = obj3 + obj2;
				object obj6 = (object)vector2 * (object)vector2;
				object obj7 = obj5 + obj4;
				object obj8 = (object)vector2 * (object)vector2;
				object obj9 = obj + obj6;
				object obj10 = obj9 + obj8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				object obj11 = obj10 ^ 0;
				object obj12 = obj11 - (object)vector2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj14 = obj7 & 0;
				object obj15 = 0 - obj7;
				if ((nint)obj14 < 0)
				{
					obj14 = 0;
				}
				float num3 = obj15 & obj13;
				float num4 = Mathf.Epsilon * 8f;
				float num5 = (float)obj14 * 1E-06f;
				if (num5 < num4)
				{
					num5 = num4;
				}
				bool flag = num5 > num3;
				zeroVector = (Vector3)num4;
				if (!flag)
				{
					object obj16 = obj12 / obj7;
					bool flag2 = (nint)obj16 < 0;
					bool flag3 = obj16 == null;
					bool flag4 = !flag2;
					bool flag5 = !flag3;
					object obj17 = flag5 & flag4;
					bool flag6 = obj17 == null;
					zeroVector = (Vector3)num4;
					if (!flag6)
					{
						Transform transform6 = base.transform;
						Vector3 position2 = transform6.position;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v908 @ rax_v41 (UnityEngine.Ray)+10]");
						object obj18 = 0 * obj16;
						obj13 = obj18 + (object)vector2;
						object obj20 = default(object);
						object obj19 = obj20 * obj16;
						num3 = position2.y - (float)obj13;
						zeroVector = (Vector3)(obj19 + (object)vector2);
						num5 = position2.z - (float)zeroVector;
						_grabOffsetWorld = vector2;
					}
				}
			}
			PlaceOnSurface(_activeSurface, pressScreenPos, snap: true);
			Action dragStarted = this.m_DragStarted;
			if (this.m_DragStarted != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v864.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
			if (OnPickedUpByPlayer != null)
			{
				OnPickedUpByPlayer.Invoke();
			}
			if (debugDrag)
			{
				string arg2 = base.name;
				string arg3 = _activeSurface.name;
				object obj21 = default(object);
				object arg4 = (DragAnchorMode)obj21;
				string message = $"[{arg2}] Drag started on '{arg3}' anchor={arg4}";
				Debug.Log(message, this);
			}
		}
		else
		{
			IsBeingDragged = false;
			_externallyControlled = false;
		}
	}

	private void UpdateDragPosition(Vector2 screenPos)
	{
		if (!_activeSurface || !_dragCamera)
		{
			return;
		}
		_activePlane = (Plane)_activeSurface.GetSurfacePlane().m_Normal;
		PlaceOnSurface(_activeSurface, screenPos, snap: false);
		if (matchSurfaceRotation)
		{
			DragSurface activeSurface = _activeSurface;
			if (activeSurface.preferAlignRotationOnEnter && smoothSurfaceRotation)
			{
				ApplySurfaceRotation(activeSurface, smooth: true);
			}
		}
	}

	private unsafe void EndDragInternal()
	{
		//IL_0079: Expected F4, but got I4
		//IL_00ea: Expected F4, but got I4
		//IL_016b: Expected O, but got Ref
		//IL_01a7: Expected O, but got Ref
		//IL_03c5: Expected F4, but got I4
		if (!IsBeingDragged)
		{
			return;
		}
		IsBeingDragged = false;
		_dragStackingOffset = 0f;
		string text = base.name;
		string message = "[" + text + "] Drag ending";
		Debug.Log(message, this);
		bool flag = _activeSurface != null;
		bool flag2 = !flag;
		float num = 0f;
		if (flag2)
		{
			goto IL_022c;
		}
		ComputeStackingOffset(_activeSurface);
		ApplyFinalRestingPosition(_activeSurface);
		DragSurface activeSurface = _activeSurface;
		if ((object)_activeSurface != null)
		{
			bool flag3 = !activeSurface.clampToBounds;
			num = 0f;
			if (flag3)
			{
				goto IL_01ac;
			}
			Transform transform = base.transform;
			Transform transform2 = base.transform;
			if ((object)transform2 != null)
			{
				Vector3 position = transform2.position;
				if ((object)_activeSurface != null)
				{
					float num2 = default(float);
					Vector3 vector = _activeSurface.ClampOnSurfacePreserveNormalOffset((Vector3)(&num2));
					if ((object)transform != null)
					{
						num = vector.x;
						transform.position = (Vector3)(&num2);
						goto IL_01ac;
					}
				}
			}
		}
		goto IL_060e;
		IL_022c:
		string text2 = base.name;
		string message2 = "[" + text2 + "] Before ";
		Debug.Log(message2, this);
		Action dragEnded = this.m_DragEnded;
		if (this.m_DragEnded != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v634.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (OnReleasedByPlayer != null)
		{
			OnReleasedByPlayer.Invoke();
		}
		string[] array = new string[5];
		if (array != null)
		{
			array[0] = "[";
			string text3 = base.name;
			array[1] = text3;
			array[2] = "] Drag ended on '";
			string text4 = (((object)_activeSurface == null) ? null : _activeSurface.name);
			array[3] = text4;
			array[4] = "'.";
			string message3 = string.Concat(array);
			Debug.Log(message3, this);
			ItemSlot itemSlot = FindFirstOverlappingSlot();
			if (!(itemSlot == null))
			{
				MoveToSlot(itemSlot);
				return;
			}
			if (DraggableItemDeckArea.AllDecks != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				float num3 = 0f;
				UnityEngine.Object obj = null;
				List<DraggableItemDeckArea>.Enumerator enumerator = default(List<DraggableItemDeckArea>.Enumerator);
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (!obj2)
					{
						continue;
					}
					if ((object)obj2 != null)
					{
						if (((DraggableItemDeckArea)obj2).Overlaps(this))
						{
							float overlapVolume = ((DraggableItemDeckArea)obj2).GetOverlapVolume(this);
							if (overlapVolume > num3)
							{
								num3 = overlapVolume;
								obj = obj2;
							}
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				if (!(obj == null))
				{
					MoveToDeck((DraggableItemDeckArea)obj);
					return;
				}
				if (_activeSurface != null)
				{
					surfaceRef = _activeSurface;
				}
				bool flag4 = (UnityEngine.Object)null == (UnityEngine.Object)null;
				UnityEngine.Object obj3 = null;
				if (flag4)
				{
					bool flag5 = (object)_activeSurface != null;
					obj3 = _activeSurface;
					if (!flag5)
					{
						obj3 = surfaceRef;
					}
				}
				if (!(obj3 != null))
				{
					return;
				}
				CurrentLocation = ItemLocation.Surface;
				Transform transform3 = base.transform;
				if ((object)obj3 != null)
				{
					Transform parent = ((Component)obj3).transform;
					if ((object)transform3 != null)
					{
						transform3.SetParent(parent, worldPositionStays: true);
						((DragSurface)obj3).AddItem(this);
						return;
					}
				}
			}
		}
		goto IL_060e;
		IL_01ac:
		if (matchSurfaceRotation)
		{
			DragSurface activeSurface2 = _activeSurface;
			if ((object)_activeSurface == null)
			{
				goto IL_060e;
			}
			if (activeSurface2.preferAlignRotationOnEnter)
			{
				ApplySurfaceRotation(_activeSurface, smooth: false);
			}
		}
		goto IL_022c;
		IL_060e:
		throw new NullReferenceException();
	}

	private ItemSlot FindFirstOverlappingSlot()
	{
		bool flag = slotRefs == null;
		UnityEngine.Object obj = (UnityEngine.Object)(object)slotRefs;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ItemSlot>.Enumerator enumerator = default(List<ItemSlot>.Enumerator);
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj2 != null)
				{
					if ((object)obj2 == null)
					{
						throw new NullReferenceException();
					}
					if (((ItemSlot)obj2).Overlaps(this))
					{
						enumerator.Dispose();
						return (ItemSlot)obj2;
					}
				}
			}
			enumerator.Dispose();
			if (!string.IsNullOrEmpty(dynamicSlotTag))
			{
				if (ItemSlot.AllSlots == null)
				{
					goto IL_01e5;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<ItemSlot>.Enumerator enumerator2 = default(List<ItemSlot>.Enumerator);
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (!(obj3 != null))
					{
						continue;
					}
					if (slotRefs != null)
					{
						if (!slotRefs.Contains((ItemSlot)obj3))
						{
							if ((object)obj3 == null)
							{
								throw new NullReferenceException();
							}
							string text = ((Component)obj3).tag;
							if (text == dynamicSlotTag && ((ItemSlot)obj3).Overlaps(this))
							{
								AddSlotRef((ItemSlot)obj3);
								enumerator2.Dispose();
								return (ItemSlot)obj3;
							}
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator2.Dispose();
			}
			return null;
		}
		goto IL_01e5;
		IL_01e5:
		throw new NullReferenceException();
	}

	private void TrySurfaceHandoff(Vector2 screenPos)
	{
		//IL_00a5: Invalid comparison between F4 and I4
		if (!(_handoffCooldownRemaining > 0f) && (bool)_dragCamera)
		{
			DragSurface dragSurface = FindBestSurfaceUnderPointer(screenPos);
			if (dragSurface != null && dragSurface != _activeSurface)
			{
				HandoffTo(dragSurface, screenPos);
			}
		}
	}

	private void HandoffTo(DragSurface newSurface, Vector2 screenPos)
	{
		//IL_01a7: Invalid comparison between I4 and F4
		//IL_01b9: Expected F4, but got I4
		if (!(newSurface != _activeSurface))
		{
			return;
		}
		bool flag = newSurface == null;
		if (flag)
		{
			return;
		}
		if (debugDrag != flag)
		{
			string[] array = new string[7] { "[", null, null, null, null, null, null };
			string text = base.name;
			array[1] = text;
			array[2] = "] Handoff: '";
			string text2 = (((object)_activeSurface == null) ? null : _activeSurface.name);
			array[3] = text2;
			array[4] = "' -> '";
			string text3 = newSurface.name;
			array[5] = text3;
			array[6] = "'";
			string message = string.Concat(array);
			Debug.Log(message, this);
		}
		_activeSurface = newSurface;
		bool flag2 = !(0f < handoffCooldownSeconds);
		float handoffCooldownRemaining = 0f;
		if (!flag2)
		{
			handoffCooldownRemaining = handoffCooldownSeconds;
		}
		_handoffCooldownRemaining = handoffCooldownRemaining;
		Transform transform = base.transform;
		Transform parent = _activeSurface.transform;
		transform.SetParent(parent, worldPositionStays: true);
		ApplySurfaceScale(_activeSurface, smoothSurfaceScale);
		if (matchSurfaceRotation)
		{
			DragSurface activeSurface = _activeSurface;
			if (activeSurface.preferAlignRotationOnEnter)
			{
				ApplySurfaceRotation(activeSurface, smoothSurfaceRotation);
			}
		}
		_activePlane = (Plane)_activeSurface.GetSurfacePlane().m_Normal;
		PlaceOnSurface(_activeSurface, screenPos, snap: true);
	}

	private DragSurface FindBestSurfaceUnderPointer(Vector2 screenPos)
	{
		//IL_0030: Expected O, but got I8
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0131: Invalid comparison between F4 and O
		//IL_014f: Invalid comparison between F4 and I4
		//IL_0178: Expected O, but got I4
		//IL_01e3: Expected O, but got I
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj = 2147483648L;
		float num = 3.4028235E+38f;
		DragSurface result = null;
		List<DragSurface>.Enumerator enumerator = default(List<DragSurface>.Enumerator);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		ref RaycastHit hit = default(ref RaycastHit);
		object obj4 = default(object);
		float num3 = default(float);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!obj2)
				{
					continue;
				}
				if ((object)obj2 == null)
				{
					break;
				}
				if (!((Behaviour)obj2).isActiveAndEnabled || !((DragSurface)obj2).IsPointerOverSurface(_dragCamera, screenPos, handoffRaycastMaxDistance, out hit))
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ stack_-88_v3 (UnityEngine.Object)+44]");
				object obj3 = 0 - obj;
				bool flag = obj3 == null;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
				bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4);
				float num2 = num - (float)obj4;
				bool flag3 = num2 == 0f;
				bool flag4 = !flag2;
				bool flag5 = !flag3;
				object obj5 = flag5 & flag4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ stack_-88_v3 (UnityEngine.Object)+44]");
				if (0 <= (nint)obj)
				{
					object obj6 = flag & obj5;
					if (obj6 == null)
					{
						continue;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E690");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ stack_-88_v3 (UnityEngine.Object)+44]");
				obj = 0;
				num = num3;
				result = (DragSurface)obj2;
				continue;
			}
			enumerator.Dispose();
			return result;
		}
		throw new NullReferenceException();
	}

	private unsafe void PlaceOnSurface(DragSurface surf, Vector2 screenPos, bool snap)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_005e: Expected O, but got Ref
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected Ref, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_00fd: Expected O, but got I
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_0230: Expected O, but got Ref
		//IL_0340: Expected O, but got Ref
		//IL_0252: Invalid comparison between I4 and F4
		//IL_018a: Expected F4, but got I4
		//IL_02b8: Invalid comparison between I4 and F4
		//IL_0303: Expected F4, but got I4
		//IL_0316: Expected O, but got Ref
		object obj2 = default(object);
		object obj = obj2 - 87;
		_ = 0;
		if (!_dragCamera)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		Ray ray = _dragCamera.ScreenPointToRay((Vector3)(&vector));
		Plane plane = (Plane)(this + 252);
		ref float enter = ref *(float*)(obj + 95);
		Ray ray2 = (Ray)(obj - 81);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v8 (UnityEngine.Ray)+10]");
		_ = 0;
		_ = ray.m_Origin;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v8 (UnityEngine.Ray)+10]");
		_ = 0;
		if (!((Plane*)plane)->Raycast(ray2, out enter))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-6D]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+5F]");
		object obj3 = num * 0;
		Vector3 vector2 = default(Vector3);
		object obj4 = obj3 + (object)vector2;
		Vector3 normalized = vector.normalized;
		bool flag = !useSurfaceDefaultLift;
		float num2 = dragLift;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj5 = default(object);
			if (obj5 != null)
			{
				num2 = ((!(surf != null)) ? 0f : surf.defaultDragLift);
			}
		}
		float num3 = ComputeDragStackingOffset(surf);
		float num4 = num2 + num3;
		_dragStackingOffset = num3;
		float num6;
		if (dragAnchorMode == DragAnchorMode.PreserveGrabOffset)
		{
			float num5 = normalized.z * num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem)+F8]");
			object obj6 = 0 + obj4;
			num6 = num5 + (float)obj6;
		}
		else
		{
			float num7 = normalized.z * num4;
			num6 = num7 + (float)obj4;
		}
		bool flag2 = !surf.clampToBounds;
		float num8 = num6;
		if (!flag2)
		{
			Vector3 vector3 = default(Vector3);
			num8 = surf.ClampOnSurfacePreserveNormalOffset((Vector3)(&vector3)).z;
		}
		Vector3 position2;
		Transform transform3;
		if (!snap && 0f < dragFollowSpeed)
		{
			Transform transform = base.transform;
			Transform transform2 = base.transform;
			Vector3 position = transform2.position;
			float deltaTime = Time.deltaTime;
			float num9 = deltaTime * dragFollowSpeed;
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
			float num10 = num8 - position.z;
			float num11 = num10 * num9;
			float num12 = num11 + position.z;
			float num13 = default(float);
			position2 = (Vector3)(&num13);
			transform3 = transform;
		}
		else
		{
			Transform transform4 = base.transform;
			float num14 = default(float);
			position2 = (Vector3)(&num14);
			transform3 = transform4;
		}
		transform3.position = position2;
	}

	private unsafe void CaptureGrabOffsetIfNeeded(Plane plane, Vector2 screenPos)
	{
		//IL_00ee: Expected I, but got O
		//IL_0041: Expected O, but got Ref
		//IL_0056: Expected O, but got Ref
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		_grabOffsetWorld = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		if (dragAnchorMode == DragAnchorMode.PreserveGrabOffset && (bool)_dragCamera)
		{
			object obj = default(object);
			Ray ray = _dragCamera.ScreenPointToRay((Vector3)(&obj));
			object obj2 = default(object);
			if (((Plane*)plane)->Raycast((Ray)(&obj2), out var enter))
			{
				Transform transform = base.transform;
				Vector3 position = transform.position;
				object obj3 = default(object);
				float num3 = (float)obj3 * enter;
				Vector3 vector = default(Vector3);
				float num4 = num3 + (float)vector;
				float num5 = position.z - num4;
				_grabOffsetWorld = vector;
			}
		}
	}

	private float ComputeDragStackingOffset(DragSurface surf)
	{
		//IL_0561: Expected F4, but got I4
		//IL_0096: Expected F4, but got I4
		//IL_0144: Invalid comparison between F4 and I4
		//IL_062d: Expected O, but got I4
		//IL_0636: Expected F4, but got I4
		//IL_065c: Expected F4, but got I4
		//IL_01d7: Expected F4, but got O
		//IL_029d: Expected O, but got F4
		//IL_02bf: Expected O, but got F4
		//IL_02db: Invalid comparison between F4 and I4
		//IL_0396: Expected O, but got F4
		//IL_03f4: Expected O, but got I
		//IL_043d: Invalid comparison between O and F4
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Expected O, but got Unknown
		//IL_0521: Expected O, but got I4
		//IL_0529: Expected F4, but got O
		//IL_04c7: Invalid comparison between F4 and O
		float result;
		if ((bool)surf && enableStackingOffset)
		{
			bool flag = (object)surf == null;
			float num = 0f;
			if (!flag)
			{
				num = surf.GetPlaneNormal().x;
				Vector3 vector = default(Vector3);
				Vector3 normalized = vector.normalized;
				Vector3 planeOriginPoint = surf.GetPlaneOriginPoint();
				Transform transform = base.transform;
				if ((object)transform != null)
				{
					Vector3 position = transform.position;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
					num = surf.surfaceScaleMultiplier;
					float num2 = ((!(surf.surfaceScaleMultiplier > 0f)) ? 1f : surf.surfaceScaleMultiplier);
					float num3 = num2 * stackingDetectionRadius;
					float num4 = num3 * num3;
					if (DragSurface.AllSurfaces != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						object obj = 0;
						float num5 = 0f;
						List<DragSurface>.Enumerator enumerator2 = default(List<DragSurface>.Enumerator);
						List<DragSurface>.Enumerator enumerator = enumerator2;
						List<DragSurface>.Enumerator enumerator3 = default(List<DragSurface>.Enumerator);
						UnityEngine.Object obj2 = default(UnityEngine.Object);
						List<DraggableItem>.Enumerator enumerator4 = default(List<DraggableItem>.Enumerator);
						List<DraggableItem>.Enumerator enumerator5 = default(List<DraggableItem>.Enumerator);
						float num6 = default(float);
						object obj4 = default(object);
						object obj5 = default(object);
						object obj7 = default(object);
						List<DragSurface>.Enumerator enumerator6 = default(List<DragSurface>.Enumerator);
						while (enumerator3.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (!obj2)
							{
								continue;
							}
							bool flag2 = (object)obj2 == null;
							num = (float)enumerator;
							if (!flag2)
							{
								if (!((Behaviour)obj2).isActiveAndEnabled)
								{
									continue;
								}
								num = ((DragSurface)obj2).GetPlaneNormal().x;
								Vector3 normalized2 = vector.normalized;
								Vector3 planeOriginPoint2 = ((DragSurface)obj2).GetPlaneOriginPoint();
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ stack_-1F8_v7 (UnityEngine.Object)+50]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
									enumerator = (List<DragSurface>.Enumerator)enumerator4;
									while (enumerator5.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										if (!((UnityEngine.Object)num6 != null))
										{
											continue;
										}
										bool flag3 = (UnityEngine.Object)num6 == this;
										if (flag3)
										{
											continue;
										}
										if (num6 != 0f)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ stack_-1E8_v5 (System.Single)+34]");
											if ((nint)0 != (flag3 ? 1 : 0))
											{
												continue;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ stack_-1E8_v5 (System.Single)+DC]");
											if ((nint)0 != (flag3 ? 1 : 0))
											{
												continue;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ stack_-1E8_v5 (System.Single)+9C]");
											if ((nint)0 == (flag3 ? 1 : 0))
											{
												continue;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ stack_-1E8_v5 (System.Single)+30]");
											if ((nint)0 != 1)
											{
												continue;
											}
											Transform transform2 = ((Component)num6).transform;
											Vector3 position2 = transform2.position;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
											object obj3 = obj4 - obj5;
											object obj6 = obj7 - (object)enumerator6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v678 @ rax_v17+8]");
											nint num7 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1101 @ rax_v58+8]");
											object obj8 = num7 - 0;
											object obj9 = obj8 * obj8;
											object obj10 = obj6 * obj6;
											object obj11 = obj3 * obj3;
											object obj12 = obj11 + obj10;
											object obj13 = obj12 + obj9;
											bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4);
											float x = normalized2.x;
											float x2 = planeOriginPoint2.x;
											enumerator = enumerator6;
											if (flag4)
											{
												continue;
											}
											float num8 = num2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ stack_-1E8_v5 (System.Single)+A0]");
											float num9 = num8 * 0f;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ stack_-1E8_v5 (System.Single)+D8]");
											enumerator = (List<DragSurface>.Enumerator)(num9 + 0);
											if (obj != null)
											{
												bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) <= System.Runtime.CompilerServices.Unsafe.As<List<DragSurface>.Enumerator, UIntPtr>(ref enumerator);
												x = normalized2.x;
												x2 = planeOriginPoint2.x;
												if (flag5)
												{
													continue;
												}
											}
											x = normalized2.x;
											x2 = planeOriginPoint2.x;
											obj = 1;
											num5 = (float)enumerator;
											continue;
										}
										throw new NullReferenceException();
									}
									enumerator5.Dispose();
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						enumerator3.Dispose();
						bool flag6 = obj == null;
						result = 0f;
						if (!flag6)
						{
							return num5;
						}
						goto IL_068d;
					}
				}
			}
			throw new NullReferenceException();
		}
		result = 0f;
		goto IL_068d;
		IL_068d:
		return result;
	}

	private unsafe void ComputeStackingOffset(DragSurface surf)
	{
		//IL_00f4: Invalid comparison between F4 and I4
		//IL_07d7: Expected O, but got I4
		//IL_07e0: Expected O, but got I4
		//IL_07f0: Expected O, but got Ref
		//IL_07fa: Expected O, but got I4
		//IL_0803: Expected O, but got I4
		//IL_082e: Expected F4, but got I4
		//IL_0579: Expected F4, but got O
		//IL_0250: Expected O, but got F4
		//IL_0259: Expected I, but got O
		//IL_0276: Expected O, but got F4
		//IL_027f: Expected I, but got O
		//IL_0296: Invalid comparison between F4 and I4
		//IL_02c8: Expected I, but got O
		//IL_02f4: Expected I, but got O
		//IL_031d: Expected I, but got O
		//IL_034a: Expected I, but got O
		//IL_0361: Expected O, but got F4
		//IL_03cb: Expected O, but got I
		//IL_0414: Invalid comparison between O and F4
		//IL_043c: Expected O, but got Ref
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Expected O, but got Unknown
		//IL_0504: Expected O, but got I4
		//IL_0536: Expected O, but got Ref
		//IL_0540: Expected O, but got I4
		//IL_04e4: Expected O, but got Ref
		if ((bool)surf && enableStackingOffset)
		{
			Vector3 planeNormal = surf.GetPlaneNormal();
			Vector3 vector = default(Vector3);
			Vector3 normalized = vector.normalized;
			Vector3 planeOriginPoint = surf.GetPlaneOriginPoint();
			Transform transform = base.transform;
			float x = transform.position.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
			float num = ((!(surf.surfaceScaleMultiplier > 0f)) ? 1f : surf.surfaceScaleMultiplier);
			float num2 = num * stackingDetectionRadius;
			float num3 = num2 * num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			object obj = 0;
			List<DragSurface>.Enumerator enumerator = (List<DragSurface>.Enumerator)0;
			object obj3 = default(object);
			object obj2 = obj3;
			float num4 = default(float);
			object obj4 = (object)(&num4);
			object obj5 = 0;
			List<DragSurface>.Enumerator enumerator2 = (List<DragSurface>.Enumerator)0;
			List<DragSurface>.Enumerator enumerator4 = default(List<DragSurface>.Enumerator);
			List<DragSurface>.Enumerator enumerator3 = enumerator4;
			nint num5 = 0;
			List<DragSurface>.Enumerator enumerator5 = default(List<DragSurface>.Enumerator);
			UnityEngine.Object obj6 = default(UnityEngine.Object);
			object obj7 = default(object);
			List<DraggableItem>.Enumerator enumerator6 = default(List<DraggableItem>.Enumerator);
			List<DraggableItem>.Enumerator enumerator7 = default(List<DraggableItem>.Enumerator);
			float num6 = default(float);
			object obj9 = default(object);
			object obj10 = default(object);
			object obj12 = default(object);
			List<DragSurface>.Enumerator enumerator8 = default(List<DragSurface>.Enumerator);
			object arg2 = default(object);
			object arg3 = default(object);
			object arg4 = default(object);
			object arg5 = default(object);
			object arg6 = default(object);
			object arg7 = default(object);
			while (true)
			{
				if (enumerator5.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj6;
					bool flag2 = !flag;
					num5 = 0;
					if (flag2)
					{
						continue;
					}
					if ((object)obj6 == null)
					{
						break;
					}
					bool flag3 = ((Behaviour)obj6).isActiveAndEnabled;
					bool flag4 = !flag3;
					num5 = 0;
					if (flag4)
					{
						continue;
					}
					Vector3 planeNormal2 = ((DragSurface)obj6).GetPlaneNormal();
					Vector3 normalized2 = vector.normalized;
					Vector3 planeOriginPoint2 = ((DragSurface)obj6).GetPlaneOriginPoint();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v728 @ stack_-208_v9 (UnityEngine.Object)+50]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						obj2 = obj7;
						enumerator3 = (List<DragSurface>.Enumerator)enumerator6;
						num5 = 0;
						while (enumerator7.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							bool flag5 = (UnityEngine.Object)num6 != null;
							num5 = unchecked((nint)null);
							if (!flag5)
							{
								continue;
							}
							bool flag6 = (UnityEngine.Object)num6 == this;
							num5 = unchecked((nint)null);
							if (flag6)
							{
								continue;
							}
							if (num6 != 0f)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ stack_-1E8_v4 (System.Single)+34]");
								bool flag7 = (nint)0 != (flag6 ? 1 : 0);
								num5 = unchecked((nint)null);
								if (flag7)
								{
									continue;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ stack_-1E8_v4 (System.Single)+DC]");
								bool flag8 = (nint)0 != (flag6 ? 1 : 0);
								num5 = unchecked((nint)null);
								if (flag8)
								{
									continue;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ stack_-1E8_v4 (System.Single)+9C]");
								bool flag9 = (nint)0 == (flag6 ? 1 : 0);
								num5 = unchecked((nint)null);
								if (flag9)
								{
									continue;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ stack_-1E8_v4 (System.Single)+30]");
								bool flag10 = (nint)0 != 1;
								num5 = unchecked((nint)null);
								if (flag10)
								{
									continue;
								}
								Transform transform2 = ((Component)num6).transform;
								Vector3 position = transform2.position;
								x = position.x;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
								object obj8 = obj9 - obj10;
								object obj11 = obj12 - (object)enumerator8;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v651 @ rax_v16+8]");
								nint num7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1306 @ rax_v83+8]");
								object obj13 = num7 - 0;
								obj2 = obj13 * obj13;
								object obj14 = obj11 * obj11;
								object obj15 = obj8 * obj8;
								object obj16 = obj15 + obj14;
								object obj17 = obj16 + obj2;
								bool flag11 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3);
								float x2 = normalized2.x;
								float x3 = planeOriginPoint2.x;
								obj4 = (object)(&x3);
								enumerator3 = enumerator8;
								num5 = (nint)(&x2);
								if (flag11)
								{
									continue;
								}
								float num8 = num;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ stack_-1E8_v4 (System.Single)+A0]");
								float num9 = num8 * 0f;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ stack_-1E8_v4 (System.Single)+D8]");
								enumerator3 = (List<DragSurface>.Enumerator)(num9 + 0);
								float x4;
								if (obj5 != null)
								{
									bool flag12 = System.Runtime.CompilerServices.Unsafe.As<List<DragSurface>.Enumerator, UIntPtr>(ref enumerator2) <= System.Runtime.CompilerServices.Unsafe.As<List<DragSurface>.Enumerator, UIntPtr>(ref enumerator3);
									x4 = position.x;
									x2 = normalized2.x;
									x3 = planeOriginPoint2.x;
									obj4 = (object)(&x3);
									num5 = (nint)(&x2);
									if (flag12)
									{
										continue;
									}
								}
								obj = 1;
								enumerator = enumerator3;
								x4 = x;
								x2 = normalized2.x;
								x3 = planeOriginPoint2.x;
								obj4 = (object)(&x3);
								obj5 = 1;
								enumerator2 = enumerator3;
								num5 = (nint)(&x2);
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator7.Dispose();
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator5.Dispose();
				bool flag13 = obj5 == null;
				float stackingNormalOffset = 0f;
				if (!flag13)
				{
					stackingNormalOffset = (float)enumerator2;
				}
				StackingNormalOffset = stackingNormalOffset;
				if (debugDrag)
				{
					string arg = base.name;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text = $"[{arg}] StackingOffset: foundNeighbour={arg2}, ";
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text2 = $"bestCandidateOffset={arg3:F4}, ";
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text3 = $"applied={arg4:F4}, ";
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text4 = $"scaleMult={arg5:F3}, ";
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text5 = $"radius(base)={arg6:F3} radius(scaled)={arg7:F3}";
					string message = text + text2 + text3 + text4 + text5;
					Debug.Log(message, this);
				}
				return;
			}
			throw new NullReferenceException();
		}
		StackingNormalOffset = 0f;
	}

	private unsafe void ApplyFinalRestingPosition(DragSurface surf)
	{
		//IL_0008: Expected O, but got Ref
		//IL_004e: Expected O, but got Ref
		//IL_00ba: Expected O, but got Ref
		//IL_00d2: Expected O, but got Ref
		//IL_00ea: Expected O, but got Ref
		//IL_016b: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if ((bool)surf)
		{
			Vector3 planeNormal = surf.GetPlaneNormal();
			Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = planeNormal.x;
			_ = planeNormal.z;
			Vector3 normalized = ((Vector3*)vector)->normalized;
			_ = normalized.x;
			Vector3 planeOriginPoint = surf.GetPlaneOriginPoint();
			Transform transform = base.transform;
			Vector3 position = transform.position;
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = planeOriginPoint.x;
			object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			_ = planeOriginPoint.z;
			object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = normalized.x;
			_ = normalized.z;
			_ = position.x;
			_ = position.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
			Transform transform2 = base.transform;
			float num = StackingNormalOffset * normalized.z;
			float num2 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rax_v14+8]");
			float num3 = num2 + 0f;
			Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			transform2.position = position2;
		}
	}

	private float ResolveLift(DragSurface surf)
	{
		//IL_006b: Expected F4, but got I4
		bool flag = !useSurfaceDefaultLift;
		float result = dragLift;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				if (surf != null)
				{
					return surf.defaultDragLift;
				}
				result = 0f;
			}
		}
		return result;
	}

	private unsafe void ApplySurfaceRotation(DragSurface surf, bool smooth)
	{
		//IL_0111: Expected O, but got Ref
		//IL_00ec: Expected O, but got Ref
		if ((bool)surf)
		{
			Transform transform = surf.transform;
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

	private unsafe void ApplySurfaceScale(DragSurface surf, bool smooth)
	{
		//IL_0153: Expected O, but got Ref
		//IL_0090: Invalid comparison between I4 and F4
		//IL_00ea: Expected O, but got F4
		if (useSurfaceScaleMultiplier && (bool)surf)
		{
			float num = surf.surfaceScaleMultiplier;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItem)+D4]");
			float num2 = num * 0f;
			if (smooth && smoothSurfaceScale && 0f < surfaceScaleTransitionDuration)
			{
				StopAllScaleCoroutines();
				Transform transform = base.transform;
				Vector3 localScale = transform.localScale;
				_003CLerpScaleRoutine_003Ed__94 obj = new _003CLerpScaleRoutine_003Ed__94(0);
				obj._003C_003E4__this = this;
				obj.from = (Vector3)localScale.x;
				Vector3 to = default(Vector3);
				obj.to = to;
				obj.duration = surfaceScaleTransitionDuration;
				_ = localScale.z;
				Coroutine scaleRoutine = StartCoroutine(obj);
				_scaleRoutine = scaleRoutine;
			}
			else
			{
				Transform transform2 = base.transform;
				Vector3 vector = default(Vector3);
				transform2.localScale = (Vector3)(&vector);
			}
		}
	}

	private IEnumerator LerpScaleRoutine(Vector3 from, Vector3 to, float duration)
	{
		//IL_0021: Expected O, but got F4
		//IL_003d: Expected O, but got F4
		_003CLerpScaleRoutine_003Ed__94 obj = new _003CLerpScaleRoutine_003Ed__94(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.from = (Vector3)from.x;
			_ = from.z;
			obj.to = (Vector3)to.x;
			_ = to.z;
			obj.duration = duration;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private void StopAllScaleCoroutines()
	{
		if (_scaleRoutine != null)
		{
			StopCoroutine(_scaleRoutine);
			_scaleRoutine = null;
		}
	}

	private unsafe void LeaveDeckToSurface()
	{
		//IL_0111: Expected O, but got Ref
		//IL_0127: Expected O, but got Ref
		if (CurrentLocation == ItemLocation.Deck)
		{
			if (deckRef != null)
			{
				deckRef.RemoveItem(this);
			}
			bool flag = (object)_activeSurface != null;
			UnityEngine.Object activeSurface = _activeSurface;
			if (!flag)
			{
				activeSurface = surfaceRef;
			}
			if (activeSurface != null)
			{
				Transform transform = base.transform;
				Transform parent = ((Component)activeSurface).transform;
				transform.SetParent(parent, worldPositionStays: true);
				((DragSurface)activeSurface).AddItem(this);
				Transform transform2 = base.transform;
				Transform transform3 = base.transform;
				Vector3 position = transform3.position;
				float num = default(float);
				Vector3 vector = ((DragSurface)activeSurface).ProjectOntoSurface((Vector3)(&num));
				transform2.position = (Vector3)(&num);
				CurrentLocation = ItemLocation.Surface;
			}
		}
	}

	public void MoveToSurface(bool slideLeft = false, bool positionAlreadySet = false)
	{
		//IL_0024: Expected F4, but got I4
		EjectAxis ejectAxis = default(EjectAxis);
		float ejectDistance = default(float);
		float ejectDistanceRandomness = default(float);
		float spreadAmount = default(float);
		MoveToSurface(null, slideLeft, positionAlreadySet, ejectAxis, ejectDistance, ejectDistanceRandomness, spreadAmount, 1f);
	}

	public void MoveToSurface(bool slideLeft, bool positionAlreadySet, EjectAxis ejectAxis, float ejectDistance, float ejectDistanceRandomness, float spreadAmount, float slideDuration)
	{
		//IL_0023: Expected F4, but got I4
		EjectAxis ejectAxis2 = default(EjectAxis);
		float ejectDistance2 = default(float);
		float ejectDistanceRandomness2 = default(float);
		float spreadAmount2 = default(float);
		MoveToSurface(null, slideLeft, positionAlreadySet, ejectAxis2, ejectDistance2, ejectDistanceRandomness2, spreadAmount2, (float)ejectAxis);
	}

	public void MoveToSurface(DragSurface overrideSurface, bool slideLeft, bool positionAlreadySet, EjectAxis ejectAxis, float ejectDistance, float ejectDistanceRandomness, float spreadAmount, float slideDuration)
	{
		//IL_020b: Expected O, but got I
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Expected F4, but got Unknown
		//IL_0420: Expected F4, but got I
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected F4, but got Unknown
		//IL_0466: Expected F4, but got I
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_038a: Expected O, but got F4
		//IL_039f: Expected F4, but got I
		bool flag = overrideSurface == null;
		DragSurface dragSurface = overrideSurface;
		if (flag)
		{
			dragSurface = _activeSurface;
			if ((object)_activeSurface == null)
			{
				dragSurface = surfaceRef;
			}
		}
		if (!(dragSurface != null))
		{
			return;
		}
		CurrentLocation = ItemLocation.Surface;
		Transform transform = base.transform;
		Transform parent = dragSurface.transform;
		transform.SetParent(parent, worldPositionStays: true);
		dragSurface.AddItem(this);
		object obj = default(object);
		if (!slideLeft)
		{
			if (!positionAlreadySet)
			{
				Transform transform2 = base.transform;
				Transform transform3 = base.transform;
				Vector3 position = transform3.position;
				Vector3 worldPos = (Vector3)(obj - 64);
				_ = position.x;
				_ = position.z;
				Vector3 vector = dragSurface.ProjectOntoSurface(worldPos);
				Vector3 position2 = (Vector3)(obj - 64);
				_ = vector.x;
				_ = vector.z;
				transform2.position = position2;
			}
			return;
		}
		Transform transform4 = dragSurface.transform;
		Vector3 right = transform4.right;
		float z = right.z;
		_ = right.x;
		Transform transform5 = dragSurface.transform;
		Vector3 up = transform5.up;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+40]");
		bool flag2 = (nint)0 == 0;
		float num2;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+40]");
			object obj2 = -1;
			if (flag2)
			{
				goto IL_0252;
			}
			object obj3 = obj2 - 1;
			if (!flag2)
			{
				if ((nint)obj3 != 1)
				{
					goto IL_0252;
				}
				_ = up.x;
				_ = right.x;
				float num = up.z ^ -0f;
				num2 = num;
			}
			else
			{
				num2 = up.z;
				_ = up.x;
				_ = right.x;
			}
			goto IL_03f5;
		}
		_ = right.x;
		num2 = right.z;
		goto IL_03d9;
		IL_03d9:
		z = up.z;
		_ = up.x;
		goto IL_03f5;
		IL_0252:
		float num3 = z ^ -0f;
		num2 = num3;
		goto IL_03d9;
		IL_03f5:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+50]");
		float minInclusive = 0 ^ -0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+50]");
		float num4 = UnityEngine.Random.Range(minInclusive, 0f);
		float num5 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+48]");
		float num6 = num5 + 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+58]");
		float minInclusive2 = 0 ^ -0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+58]");
		float num7 = UnityEngine.Random.Range(minInclusive2, 0f);
		Transform transform6 = base.transform;
		Vector3 position3 = transform6.position;
		Vector3 worldPos2 = (Vector3)(obj - 64);
		float num8 = num2 * num6;
		float num9 = num8 + position3.z;
		float num10 = z * num7;
		float num11 = num9 + num10;
		Vector3 vector2 = dragSurface.ProjectOntoSurface(worldPos2);
		_003CSlideCoroutine_003Ed__105 obj4 = new _003CSlideCoroutine_003Ed__105(0);
		obj4._003C_003E4__this = this;
		obj4.target = (Vector3)vector2.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+60]");
		obj4.duration = 0f;
		_ = vector2.z;
		obj4.surf = dragSurface;
		Coroutine coroutine = StartCoroutine(obj4);
	}

	private unsafe static void ResolveEjectAxes(DragSurface surf, EjectAxis axis, out Vector3 ejectDir, out Vector3 spreadDir)
	{
		//IL_0154: Expected Ref, but got F4
		//IL_0171: Expected Ref, but got F4
		//IL_0069: Expected O, but got I4
		//IL_0135: Expected O, but got F4
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_00fc: Expected Ref, but got F4
		//IL_0113: Expected Ref, but got F4
		//IL_00c6: Expected O, but got F4
		//IL_00e0: Expected Ref, but got F4
		Transform transform = surf.transform;
		Vector3 right = transform.right;
		Transform transform2 = surf.transform;
		Vector3 up = transform2.up;
		bool flag = axis == EjectAxis.PositiveX;
		ref Vector3 reference2;
		if (!flag)
		{
			object obj = axis - 1;
			object obj4 = default(object);
			ref Vector3 reference;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (flag)
				{
					reference = ref *(Vector3*)up.x;
					_ = up.z;
					reference2 = ref *(Vector3*)right.x;
					_ = right.z;
					return;
				}
				if ((nint)obj2 == 1)
				{
					object obj3 = up.z ^ -0f;
					reference = ref *(Vector3*)obj4;
					reference2 = ref *(Vector3*)right.x;
					_ = right.z;
					return;
				}
			}
			object obj5 = right.z ^ -0f;
			reference = ref *(Vector3*)obj4;
		}
		else
		{
			ref Vector3 reference = ref *(Vector3*)right.x;
			_ = right.z;
		}
		reference2 = ref *(Vector3*)up.x;
		_ = up.z;
	}

	public void SettleOnSurface(DragSurface surf)
	{
		if (!(surf != null))
		{
			return;
		}
		surfaceRef = surf;
		UnityEngine.Object obj;
		if ((UnityEngine.Object)null != (UnityEngine.Object)null)
		{
			obj = null;
		}
		else
		{
			bool flag = (object)_activeSurface != null;
			obj = _activeSurface;
			if (!flag)
			{
				obj = surfaceRef;
			}
		}
		if (obj != null)
		{
			CurrentLocation = ItemLocation.Surface;
			Transform transform = base.transform;
			Transform parent = ((Component)obj).transform;
			transform.SetParent(parent, worldPositionStays: true);
			((DragSurface)obj).AddItem(this);
		}
		ComputeStackingOffset(surf);
		ApplyFinalRestingPosition(surf);
	}

	public void MoveToDeck(DraggableItemDeckArea targetDeck)
	{
		bool flag = (object)_activeSurface != null;
		UnityEngine.Object activeSurface = _activeSurface;
		if (!flag)
		{
			activeSurface = surfaceRef;
		}
		if (activeSurface != null)
		{
			((DragSurface)activeSurface).RemoveItem(this);
		}
		StackingNormalOffset = 0f;
		CurrentLocation = ItemLocation.Deck;
		targetDeck.AddBack(this);
		deckRef = targetDeck;
	}

	public void MoveToSlot()
	{
		ItemSlot itemSlot = FindFirstOverlappingSlot();
		if (itemSlot != null)
		{
			MoveToSlot(itemSlot);
		}
	}

	public void MoveToSlot(ItemSlot targetSlot)
	{
		if (targetSlot != null)
		{
			bool flag = (object)_activeSurface != null;
			UnityEngine.Object activeSurface = _activeSurface;
			if (!flag)
			{
				activeSurface = surfaceRef;
			}
			if (activeSurface != null)
			{
				((DragSurface)activeSurface).RemoveItem(this);
			}
			StackingNormalOffset = 0f;
			CurrentLocation = ItemLocation.Slot;
			targetSlot.PlaceItem(this);
			if (OnSlottedIntoSlot != null)
			{
				GameObject arg = targetSlot.gameObject;
				OnSlottedIntoSlot.Invoke(arg);
			}
		}
	}

	private IEnumerator SlideCoroutine(Vector3 target, float duration, DragSurface surf)
	{
		//IL_0021: Expected O, but got F4
		_003CSlideCoroutine_003Ed__105 obj = new _003CSlideCoroutine_003Ed__105(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.target = (Vector3)target.x;
			_ = target.z;
			obj.duration = duration;
			obj.surf = surf;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private Vector2 GetScreenPosition()
	{
		if (!_cachedVirtualCursor)
		{
			GameObject gameObject = GameObject.FindWithTag("VirtualCursor");
			if ((bool)gameObject)
			{
				if ((object)gameObject == null)
				{
					goto IL_00ff;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				VirtualCursor cachedVirtualCursor = default(VirtualCursor);
				_cachedVirtualCursor = cachedVirtualCursor;
			}
		}
		Vector2 result = default(Vector2);
		if (_cachedVirtualCursor != null)
		{
			if ((object)_cachedVirtualCursor != null)
			{
				return result;
			}
			goto IL_00ff;
		}
		int width = Screen.width;
		int height = Screen.height;
		return result;
		IL_00ff:
		return (Vector2)new NullReferenceException();
	}

	private VirtualCursor FindVirtualCursor()
	{
		if (!_cachedVirtualCursor)
		{
			GameObject gameObject = GameObject.FindWithTag("VirtualCursor");
			if ((bool)gameObject)
			{
				if ((object)gameObject == null)
				{
					return (VirtualCursor)(object)new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				VirtualCursor cachedVirtualCursor = default(VirtualCursor);
				_cachedVirtualCursor = cachedVirtualCursor;
			}
		}
		return _cachedVirtualCursor;
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

	public DraggableItem()
	{
		List<ItemSlot> list = new List<ItemSlot>();
		slotRefs = list;
		dynamicSlotTag = "";
		OnPickedUpByPlayer = new UnityEvent();
		OnReleasedByPlayer = new UnityEvent();
		OnPickedUpToClipboard = new UnityEvent();
		OnSlottedIntoSlot = new UnityEvent<GameObject>();
		OnRemovedFromSlot = new UnityEvent<GameObject>();
		dragLift = 0.02f;
		useSurfaceDefaultLift = true;
		dragFollowSpeed = 22f;
		pullThresholdPixels = 4f;
		enableStackingOffset = true;
		stackingOffsetDonated = -0.001f;
		stackingDetectionRadius = 0.12f;
		matchSurfaceRotation = true;
		surfaceRotationLerpSpeed = 18f;
		useSurfaceScaleMultiplier = true;
		surfaceScaleTransitionDuration = 0.18f;
		enableSurfaceHandoff = true;
		handoffCooldownSeconds = 0.1f;
		handoffRaycastMaxDistance = 1000f;
		ejectSlideLift = -0.01f;
		base._002Ector();
	}
}
