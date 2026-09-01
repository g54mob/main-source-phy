using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class MoveToClipboardCenterBridge3D : MonoBehaviour
{
	public enum TriggerOnceConsumeMode
	{
		OnTriggerStart,
		OnMoveCompleted
	}

	private sealed class _003CAnimateToClipboardDestinationRoutine_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToClipboardCenterBridge3D _003C_003E4__this;

		private Vector3 _003CstartPos_003E5__2;

		private Quaternion _003CstartRot_003E5__3;

		private Vector3 _003CstartScale_003E5__4;

		private Vector3 _003CtargetPos_003E5__5;

		private Quaternion _003CtargetRot_003E5__6;

		private Vector3 _003CtargetScale_003E5__7;

		private float _003Cdur_003E5__8;

		private float _003Ct_003E5__9;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateToClipboardDestinationRoutine_003Ed__37(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_001c: Expected I4, but got I8
			//IL_0317: Expected I4, but got I8
			//IL_0883: Expected I4, but got O
			//IL_0125: Expected O, but got F4
			//IL_04a2: Invalid comparison between I4 and F4
			//IL_00a5: Expected O, but got I
			//IL_04ed: Expected F4, but got I4
			//IL_093b: Invalid comparison between I4 and F4
			//IL_017c: Expected O, but got F4
			//IL_0533: Expected F4, but got I4
			//IL_096c: Expected O, but got I
			//IL_01c9: Expected O, but got F4
			//IL_0546: Expected O, but got Ref
			//IL_01f6: Expected O, but got F4
			//IL_0236: Expected O, but got I
			//IL_076d: Invalid comparison between I4 and F4
			//IL_0273: Expected O, but got F4
			//IL_07b8: Expected F4, but got I4
			//IL_0296: Expected O, but got F4
			//IL_02b1: Invalid comparison between F4 and I
			//IL_0a11: Expected O, but got I
			//IL_09c2: Expected O, but got Ref
			//IL_02e1: Expected F4, but got I
			//IL_05ff: Expected F4, but got O
			//IL_09e2: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			Component component = _003C_003E4__this;
			float num;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+58]");
					if ((nint)0 == 0)
					{
						goto IL_00dd;
					}
					Transform transform = _003C_003E4__this.transform;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
						Transform transform2 = ((Component)0).transform;
						if ((object)transform != null)
						{
							transform.SetParent(transform2, worldPositionStays: true);
							goto IL_00dd;
						}
					}
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				num = _003Ct_003E5__9;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0340;
				}
			}
			goto IL_0875;
			IL_0696:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+5B]");
			float num2;
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+68]");
				Transform transform3;
				if ((nint)0 == 0)
				{
					if (_003Ct_003E5__9 < 1f)
					{
						goto IL_07cc;
					}
					transform3 = _003C_003E4__this.transform;
					if ((object)transform3 == null)
					{
						goto IL_0875;
					}
					_ = _003CtargetScale_003E5__7;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+74]");
					_ = 0;
				}
				else
				{
					transform3 = _003C_003E4__this.transform;
					_ = _003CtargetScale_003E5__7;
					_ = _003CstartScale_003E5__4;
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
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+74]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+4C]");
					object obj3 = num3 - 0;
					float num4 = (float)obj3 * num2;
					float num5 = num4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+4C]");
					float num6 = num5 + 0f;
					if ((object)transform3 == null)
					{
						goto IL_0875;
					}
				}
				Vector3 localScale = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				transform3.localScale = localScale;
			}
			goto IL_07cc;
			IL_07cc:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
			IL_043a:
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 4294967295L;
			return false;
			IL_0340:
			if (1f > num)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+50]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+78]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						object obj4 = default(object);
						if (obj4 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+70]");
							if ((nint)0 != 0)
							{
								string name = _003C_003E4__this.name;
								string message = "[" + name + "] AnimateToClipboardDestination: aborted (dragging).";
								Debug.Log(message, _003C_003E4__this);
							}
							goto IL_043a;
						}
					}
				}
				float deltaTime = Time.deltaTime;
				float num7 = deltaTime / _003Cdur_003E5__8;
				float num8 = (_003Ct_003E5__9 = num7 + _003Ct_003E5__9);
				if (!(0f > num8))
				{
					if (num8 > 1f)
					{
						num8 = 1f;
					}
				}
				else
				{
					num8 = 0f;
				}
				float num9 = num8 + num8;
				float num10 = num8 * num8;
				float num11 = 3f - num9;
				num2 = num11 * num10;
				Transform transform4 = _003C_003E4__this.transform;
				_ = _003CtargetPos_003E5__5;
				_ = _003CstartPos_003E5__2;
				float num12 = ((0f > num2) ? 0f : ((num2 > 1f) ? 1f : num2));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+58]");
				nint num13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+30]");
				object obj5 = num13 - 0;
				float num14 = (float)obj5 * num12;
				float num15 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToClipboardCenterBridge3D+<AnimateToClipboardDestinationRoutine>d__37)+30]");
				float num16 = num15 + 0f;
				if ((object)transform4 != null)
				{
					Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					transform4.position = position;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+59]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+5A]");
						Transform transform6;
						if ((nint)0 == 0)
						{
							if (_003Ct_003E5__9 < 1f)
							{
								goto IL_0696;
							}
							Transform transform5 = _003C_003E4__this.transform;
							if ((object)transform5 == null)
							{
								goto IL_0875;
							}
							float num17 = (float)_003CtargetRot_003E5__6;
							transform6 = transform5;
						}
						else
						{
							Transform transform7 = _003C_003E4__this.transform;
							ref Quaternion b = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
							ref Quaternion a = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
							_ = _003CtargetRot_003E5__6;
							_ = _003CstartRot_003E5__3;
							Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, num2);
							if ((object)transform7 == null)
							{
								goto IL_0875;
							}
							float num17 = quaternion.x;
							transform6 = transform7;
						}
						Quaternion rotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
						transform6.rotation = rotation;
					}
					goto IL_0696;
				}
				goto IL_0875;
			}
			MoveToClipboardCenterBridge3D moveToClipboardCenterBridge3D = _003C_003E4__this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+5B]");
			nint num18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+59]");
			moveToClipboardCenterBridge3D.SnapToClipboardDestination((byte)num18 != 0, applyRotationNow: false);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+41]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+44]");
				if ((nint)0 == 1)
				{
					_ = 1;
				}
			}
			goto IL_043a;
			IL_00dd:
			Transform transform8 = _003C_003E4__this.transform;
			if ((object)transform8 != null)
			{
				Vector3 position2 = transform8.position;
				_003CstartPos_003E5__2 = (Vector3)position2.x;
				_ = position2.z;
				Transform transform9 = _003C_003E4__this.transform;
				if ((object)transform9 != null)
				{
					_003CstartRot_003E5__3 = (Quaternion)transform9.rotation.x;
					Transform transform10 = _003C_003E4__this.transform;
					if ((object)transform10 != null)
					{
						Vector3 localScale2 = transform10.localScale;
						_003CstartScale_003E5__4 = (Vector3)localScale2.x;
						_ = localScale2.z;
						Vector3 clipboardDestinationTargetWorld = _003C_003E4__this.GetClipboardDestinationTargetWorld();
						_003CtargetPos_003E5__5 = (Vector3)clipboardDestinationTargetWorld.x;
						_ = clipboardDestinationTargetWorld.z;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
							Transform transform11 = ((Component)0).transform;
							if ((object)transform11 != null)
							{
								_003CtargetRot_003E5__6 = (Quaternion)transform11.rotation.x;
								Vector3 vector = _003C_003E4__this.ComputeClipboardTargetLocalScale();
								_003CtargetScale_003E5__7 = (Vector3)vector.x;
								_ = vector.z;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+54]");
								bool flag = !(0.0001f < 0f);
								float num19 = 0.0001f;
								if (!flag)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+54]");
									num19 = 0f;
								}
								_003Ct_003E5__9 = 0f;
								num = _003Ct_003E5__9;
								_003Cdur_003E5__8 = num19;
								goto IL_0340;
							}
						}
					}
				}
			}
			goto IL_0875;
			IL_0875:
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

	private sealed class _003CAutoMoveRoutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToClipboardCenterBridge3D _003C_003E4__this;

		private float _003Cremaining_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoMoveRoutine_003Ed__36(int _003C_003E1__state)
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
			//IL_00aa: Expected I4, but got I8
			//IL_02a7: Expected I4, but got O
			//IL_0056: Expected F4, but got I4
			//IL_00dc: Invalid comparison between F4 and I4
			//IL_0074: Expected F4, but got I
			UnityEngine.Object obj = _003C_003E4__this;
			float num2;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Object)+4C]");
					bool flag = (nint)0 >= (nint)0;
					float num = 0f;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Object)+4C]");
						num = 0f;
					}
					_003Cremaining_003E5__2 = num;
					num2 = num;
					goto IL_00d3;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01d8;
				}
				_003C_003E1__state = -1;
				num2 = _003Cremaining_003E5__2;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00d3;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00d3:
			if (num2 > 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Object)+50]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Object)+78]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						object obj2 = default(object);
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Object)+70]");
							if ((nint)0 != 0)
							{
								string name = _003C_003E4__this.name;
								string message = "[" + name + "] AutoMove canceled (dragging started).";
								Debug.Log(message, _003C_003E4__this);
							}
							_ = 0;
							goto IL_01d8;
						}
					}
				}
				float deltaTime = Time.deltaTime;
				float num3 = _003Cremaining_003E5__2 - deltaTime;
				_003C_003E2__current = null;
				_003Cremaining_003E5__2 = num3;
				_003C_003E1__state = 1;
				return true;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Object)+70]");
			if ((nint)0 != 0)
			{
				string name2 = _003C_003E4__this.name;
				string message2 = "[" + name2 + "] AutoMove firing.";
				Debug.Log(message2, _003C_003E4__this);
			}
			_003C_003E4__this.MoveToClipboardCenteredNow();
			return false;
			IL_01d8:
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

	private MonoBehaviour draggableBehaviour;

	private BoundedDragSurface3D clipboardSurface;

	private bool autoResolveByTag;

	private string clipboardSurfaceTag;

	private bool useClipboardSlotCyclerIfPresent;

	private bool triggerOnlyOncePerInstantiation;

	private TriggerOnceConsumeMode triggerOnceMode;

	private bool autoMoveAfterDelay;

	private float autoMoveDelaySeconds;

	private bool autoMoveOnlyIfNotDragging;

	private bool animate;

	private float durationSeconds;

	private bool parentToClipboardBeforeMove;

	private bool matchSurfaceRotation;

	private bool smoothRotation;

	private bool applySurfaceScaleMultiplier;

	private Vector3 baseScaleOverride;

	private bool smoothScale;

	private bool clampToSurfaceBounds;

	private float destinationLift;

	private bool debug;

	private ICursorDraggable _draggable;

	private Coroutine _routine;

	private Vector3 _capturedBaseLocalScale;

	private ClipboardSlotCycler3D _cycler;

	private bool _hasAllocatedSlot;

	private Vector2 _allocatedNormalizedOffset;

	private int _allocatedSlotIndex;

	private bool _hasConsumedOneShot;

	private void Awake()
	{
		//IL_002b: Expected O, but got F4
		//IL_008f: Expected O, but got I4
		//IL_009e: Expected I4, but got I8
		Transform transform = base.transform;
		Vector3 localScale = transform.localScale;
		_capturedBaseLocalScale = (Vector3)localScale.x;
		_ = localScale.z;
		ResolveDraggableReference();
		if (autoResolveByTag)
		{
			bool flag = ResolveClipboardByTag(logWarnings: false);
		}
		ResolveCyclerReference();
		_hasAllocatedSlot = false;
		_allocatedNormalizedOffset = (Vector2)0;
		_allocatedSlotIndex = -1;
		_hasConsumedOneShot = false;
	}

	private void OnEnable()
	{
		//IL_0050: Expected O, but got I4
		//IL_005f: Expected I4, but got I8
		if (autoResolveByTag)
		{
			bool flag = ResolveClipboardByTag(logWarnings: false);
		}
		ResolveCyclerReference();
		_hasAllocatedSlot = false;
		_allocatedNormalizedOffset = (Vector2)0;
		_allocatedSlotIndex = -1;
		if (autoMoveAfterDelay)
		{
			if (_routine != null)
			{
				StopCoroutine(_routine);
			}
			_003CAutoMoveRoutine_003Ed__36 obj = new _003CAutoMoveRoutine_003Ed__36(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine routine = StartCoroutine(obj);
			_routine = routine;
		}
	}

	private void OnDisable()
	{
		//IL_004d: Expected O, but got I4
		//IL_005c: Expected I4, but got I8
		if (_routine != null)
		{
			StopCoroutine(_routine);
			_routine = null;
		}
		_hasAllocatedSlot = false;
		_allocatedNormalizedOffset = (Vector2)0;
		_allocatedSlotIndex = -1;
	}

	public void MoveToClipboardCenteredNow()
	{
		//IL_0428: Expected O, but got I4
		//IL_0437: Expected I4, but got I8
		//IL_04b7: Expected O, but got I4
		//IL_04c6: Expected I4, but got I8
		//IL_0326: Invalid comparison between I4 and F4
		if (triggerOnlyOncePerInstantiation && _hasConsumedOneShot)
		{
			if (debug)
			{
				string text = base.name;
				string message = "[" + text + "] MoveToClipboardCenteredNow: ignored (one-shot already consumed for this instance).";
				Debug.Log(message, this);
			}
			return;
		}
		if (autoResolveByTag)
		{
			bool flag = ResolveClipboardByTag(logWarnings: true);
		}
		ResolveCyclerReference();
		if ((bool)clipboardSurface)
		{
			if (_routine != null)
			{
				StopCoroutine(_routine);
				_routine = null;
			}
			if (autoMoveOnlyIfNotDragging && _draggable != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if (obj != null)
				{
					if (debug)
					{
						string text2 = base.name;
						string message2 = "[" + text2 + "] MoveToClipboardCenteredNow: skipped (currently dragging).";
						Debug.Log(message2, this);
					}
					return;
				}
			}
			_hasAllocatedSlot = false;
			_allocatedNormalizedOffset = (Vector2)0;
			_allocatedSlotIndex = -1;
			if (useClipboardSlotCyclerIfPresent && _cycler != null && _cycler.TryGetNextNormalizedOffset(out var normalizedOffset, out var allocatedIndex))
			{
				_allocatedNormalizedOffset = normalizedOffset;
				_hasAllocatedSlot = true;
				_allocatedSlotIndex = allocatedIndex;
				if (debug)
				{
					string arg = base.name;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Vector2 vector = default(Vector2);
					object arg2 = vector;
					object arg3 = default(object);
					string message3 = $"[{arg}] Allocated clipboard slot idx={arg3}, normalizedOffset={arg2}";
					Debug.Log(message3, this);
				}
			}
			if (triggerOnlyOncePerInstantiation && triggerOnceMode == TriggerOnceConsumeMode.OnTriggerStart)
			{
				_hasConsumedOneShot = true;
			}
			if (animate && 0f < durationSeconds)
			{
				_003CAnimateToClipboardDestinationRoutine_003Ed__37 obj2 = new _003CAnimateToClipboardDestinationRoutine_003Ed__37(0);
				obj2._003C_003E1__state = 0;
				obj2._003C_003E4__this = this;
				Coroutine routine = StartCoroutine(obj2);
				_routine = routine;
				return;
			}
			SnapToClipboardDestination(applyScaleNow: true, applyRotationNow: true);
			if (triggerOnlyOncePerInstantiation && triggerOnceMode == TriggerOnceConsumeMode.OnMoveCompleted)
			{
				_hasConsumedOneShot = true;
			}
			_hasAllocatedSlot = false;
			_allocatedNormalizedOffset = (Vector2)0;
			_allocatedSlotIndex = -1;
		}
		else
		{
			string text3 = base.name;
			string message4 = text3 + ": MoveToClipboardCenteredNow() called but clipboardSurface is missing.";
			Debug.LogError(message4, this);
		}
	}

	private void AllocateSlotOnceForThisMove()
	{
		//IL_0106: Expected O, but got I4
		//IL_0115: Expected I4, but got I8
		_hasAllocatedSlot = false;
		_allocatedNormalizedOffset = (Vector2)0;
		_allocatedSlotIndex = -1;
		if (useClipboardSlotCyclerIfPresent && _cycler != null && _cycler.TryGetNextNormalizedOffset(out var normalizedOffset, out var allocatedIndex))
		{
			bool flag = !debug;
			_allocatedNormalizedOffset = normalizedOffset;
			_hasAllocatedSlot = true;
			_allocatedSlotIndex = allocatedIndex;
			if (!flag)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj = default(object);
				object arg2 = (Vector2)obj;
				object arg3 = default(object);
				string message = $"[{arg}] Allocated clipboard slot idx={arg3}, normalizedOffset={arg2}";
				Debug.Log(message, this);
			}
		}
	}

	private void ClearAllocatedSlot()
	{
		//IL_0016: Expected O, but got I4
		//IL_0025: Expected I4, but got I8
		_hasAllocatedSlot = false;
		_allocatedNormalizedOffset = (Vector2)0;
		_allocatedSlotIndex = -1;
	}

	private IEnumerator AutoMoveRoutine()
	{
		_003CAutoMoveRoutine_003Ed__36 obj = new _003CAutoMoveRoutine_003Ed__36(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator AnimateToClipboardDestinationRoutine()
	{
		_003CAnimateToClipboardDestinationRoutine_003Ed__37 obj = new _003CAnimateToClipboardDestinationRoutine_003Ed__37(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void SnapToClipboardDestination(bool applyScaleNow, bool applyRotationNow)
	{
		//IL_0080: Expected O, but got Ref
		//IL_011f: Expected O, but got Ref
		//IL_00db: Expected O, but got Ref
		if (parentToClipboardBeforeMove)
		{
			Transform transform = base.transform;
			Transform parent = clipboardSurface.transform;
			transform.SetParent(parent, worldPositionStays: true);
		}
		Transform transform2 = base.transform;
		Vector3 clipboardDestinationTargetWorld = GetClipboardDestinationTargetWorld();
		float num = default(float);
		transform2.position = (Vector3)(&num);
		if (applyRotationNow)
		{
			Transform transform3 = base.transform;
			Transform transform4 = clipboardSurface.transform;
			Quaternion rotation = transform4.rotation;
			object obj = default(object);
			transform3.rotation = (Quaternion)(&obj);
		}
		if (applyScaleNow)
		{
			Transform transform5 = base.transform;
			Vector3 vector = ComputeClipboardTargetLocalScale();
			transform5.localScale = (Vector3)(&num);
		}
	}

	private void ResolveCyclerReference()
	{
		_cycler = null;
		if (useClipboardSlotCyclerIfPresent && (bool)clipboardSurface)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			ClipboardSlotCycler3D cycler = default(ClipboardSlotCycler3D);
			_cycler = cycler;
		}
	}

	private unsafe Vector3 ComputeClipboardTargetLocalScale()
	{
		//IL_00d5: Expected native int or pointer, but got O
		//IL_00e7: Expected native int or pointer, but got O
		//IL_012c: Expected I, but got O
		//IL_0178: Expected O, but got I
		//IL_01e7: Invalid comparison between F4 and I4
		//IL_0210: Expected O, but got I4
		//IL_005c: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_007d: Expected native int or pointer, but got O
		//IL_008a: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		if (!applySurfaceScaleMultiplier || !clipboardSurface)
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 localScale = transform.localScale;
				((Vector3*)(nint)vector)->x = localScale.x;
				((Vector3*)(nint)vector)->z = localScale.z;
				return vector;
			}
		}
		else
		{
			nint num = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v12 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num2 = 0;
			object obj = baseScaleOverride - Vector3.zeroVector;
			object obj2 = default(object);
			float num4 = default(float);
			float num3 = (float)obj2 - num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToClipboardCenterBridge3D)+64]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			object obj3 = num5 - 0;
			float num6 = num3 * num3;
			object obj4 = obj * obj;
			object obj5 = obj3 * obj3;
			float num7 = num6 + (float)obj4;
			float num8 = num7 + (float)obj5;
			bool flag = 9.9999994E-11f < num8;
			float num9 = 9.9999994E-11f - num8;
			bool flag2 = num9 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			object obj6 = flag4 & flag3;
			object obj7;
			if (obj6 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToClipboardCenterBridge3D)+64]");
				obj7 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToClipboardCenterBridge3D)+90]");
				obj7 = 0;
			}
			BoundedDragSurface3D boundedDragSurface3D = clipboardSurface;
			if ((object)clipboardSurface != null)
			{
				float z = boundedDragSurface3D.surfaceScaleMultiplier * (float)obj7;
				((Vector3*)(nint)vector)->x = num4;
				((Vector3*)(nint)vector)->z = z;
				return vector;
			}
		}
		return (Vector3)new NullReferenceException();
	}

	private unsafe Vector3 GetClipboardDestinationTargetWorld()
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0017: Expected native int or pointer, but got O
		//IL_0083: Expected I, but got O
		//IL_00a3: Expected F4, but got I
		//IL_01f3: Expected native int or pointer, but got O
		//IL_0200: Expected native int or pointer, but got O
		//IL_0111: Expected O, but got Ref
		//IL_0122: Expected native int or pointer, but got O
		//IL_0134: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		if ((object)clipboardSurface != null)
		{
			Vector3 planeNormal = clipboardSurface.GetPlaneNormal();
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				num2 = 0f;
			}
			Vector3 clipboardDestinationBasePointOnPlaneWorld = GetClipboardDestinationBasePointOnPlaneWorld();
			float defaultDragLift = destinationLift;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				BoundedDragSurface3D boundedDragSurface3D = clipboardSurface;
				if ((object)clipboardSurface == null)
				{
					goto IL_013e;
				}
				defaultDragLift = boundedDragSurface3D.defaultDragLift;
			}
			bool flag = !clampToSurfaceBounds;
			float num5 = num2 * defaultDragLift;
			float z = num5 + clipboardDestinationBasePointOnPlaneWorld.z;
			float x = default(float);
			((Vector3*)(nint)vector)->x = x;
			((Vector3*)(nint)vector)->z = z;
			if (!flag)
			{
				if ((object)clipboardSurface == null)
				{
					goto IL_013e;
				}
				float num6 = default(float);
				Vector3 vector2 = clipboardSurface.ClampToSurfaceBounds((Vector3)(&num6));
				((Vector3*)(nint)vector)->x = vector2.x;
				((Vector3*)(nint)vector)->z = vector2.z;
			}
			return vector;
		}
		goto IL_013e;
		IL_013e:
		return (Vector3)new NullReferenceException();
	}

	private unsafe Vector3 GetClipboardDestinationBasePointOnPlaneWorld()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0303: Expected O, but got Ref
		//IL_008a: Expected I, but got O
		//IL_00aa: Expected F4, but got I
		//IL_010a: Expected O, but got Ref
		//IL_036e: Expected O, but got I
		//IL_038b: Expected O, but got I
		//IL_016d: Expected F4, but got I4
		//IL_0176: Expected F4, but got I4
		//IL_017f: Expected F4, but got I4
		//IL_02aa: Expected F4, but got O
		//IL_02a5: Expected native int or pointer, but got O
		//IL_02b2: Expected native int or pointer, but got O
		//IL_0283: Expected F4, but got I
		//IL_027e: Expected native int or pointer, but got O
		//IL_0298: Expected F4, but got I
		//IL_0293: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		Vector3 vector2 = default(Vector3);
		if ((object)clipboardSurface != null)
		{
			Vector3 planeNormal = clipboardSurface.GetPlaneNormal();
			_ = planeNormal.x;
			_ = planeNormal.z;
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			float num3;
			if (planeNormal.x > 1E-05f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-65]");
				float num = 0f / planeNormal.x;
				float num2 = planeNormal.z / planeNormal.x;
				num3 = num2;
			}
			else
			{
				nint num4 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rax_v20 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ rcx_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				num3 = 0f;
				_ = Vector3.zeroVector;
			}
			if ((object)clipboardSurface != null)
			{
				Vector3 surfaceCenterWorldPosition = clipboardSurface.GetSurfaceCenterWorldPosition();
				_ = surfaceCenterWorldPosition.x;
				if ((object)clipboardSurface != null)
				{
					Vector3 planeOriginPoint = clipboardSurface.GetPlaneOriginPoint();
					object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					_ = planeOriginPoint.x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-65]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
					float num6;
					float num7;
					float num8;
					if (!(planeOriginPoint.x > 1E-05f))
					{
						num6 = 0f;
						num7 = 0f;
						num8 = 0f;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
						num8 = 0f / planeOriginPoint.x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-65]");
						num7 = 0f / planeOriginPoint.x;
						num6 = num3 / planeOriginPoint.x;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-35]");
					nint num9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-25]");
					object obj5 = num9 - 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
					nint num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
					object obj6 = num10 - 0;
					float num11 = (float)obj5 * num7;
					float num12 = (float)obj6 * num8;
					float num13 = num11 + num12;
					float num14 = surfaceCenterWorldPosition.z - planeOriginPoint.z;
					float num15 = num14 * num6;
					float num16 = num13 + num15;
					float num17 = num16 * num6;
					float z = surfaceCenterWorldPosition.z - num17;
					bool flag = _cycler == null;
					Vector2 vector = default(Vector2);
					if (!flag && _hasAllocatedSlot != flag)
					{
						if ((object)_cycler == null)
						{
							goto IL_02bc;
						}
						if (_cycler.TryGetWorldPointOnPlaneFromNormalizedOffset(vector, out System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89))))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
							((Vector3*)(nint)vector2)->x = 0f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
							((Vector3*)(nint)vector2)->z = 0f;
							goto IL_0417;
						}
					}
					((Vector3*)(nint)vector2)->x = (float)vector;
					((Vector3*)(nint)vector2)->z = z;
					goto IL_0417;
				}
			}
		}
		goto IL_02bc;
		IL_02bc:
		return (Vector3)new NullReferenceException();
		IL_0417:
		return vector2;
	}

	private void ResolveDraggableReference()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0049: Expected O, but got I4
		//IL_0052: Expected O, but got I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		if (draggableBehaviour == null)
		{
			MonoBehaviour[] components = GetComponents<MonoBehaviour>();
			object obj = components + 32;
			object obj2 = 0;
			object obj3 = 0;
			ICursorDraggable cursorDraggable = default(ICursorDraggable);
			while ((nint)obj3 < components.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (cursorDraggable == null)
				{
					obj2++;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				_draggable = cursorDraggable;
				draggableBehaviour = components[obj2];
				break;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			ICursorDraggable draggable = default(ICursorDraggable);
			_draggable = draggable;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			if (_draggable == null && debug)
			{
				string text = base.name;
				string message = text + ": draggableBehaviour is assigned but does not implement ICursorDraggable.";
				Debug.LogWarning(message, this);
			}
		}
	}

	private bool ResolveClipboardByTag(bool logWarnings)
	{
		//IL_019d: Expected I4, but got O
		if (clipboardSurface == null && !string.IsNullOrEmpty(clipboardSurfaceTag))
		{
			GameObject gameObject = GameObject.FindWithTag(clipboardSurfaceTag);
			UnityEngine.Object context;
			object message;
			if (gameObject != null)
			{
				if ((object)gameObject == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				BoundedDragSurface3D boundedDragSurface3D = default(BoundedDragSurface3D);
				clipboardSurface = boundedDragSurface3D;
				if ((bool)clipboardSurface)
				{
					return true;
				}
				if (!logWarnings)
				{
					goto IL_0189;
				}
				string text = base.name;
				string text2 = text + ": ClipboardSurface tag object has no BoundedDragSurface3D.";
				context = gameObject;
				message = text2;
			}
			else
			{
				if (!logWarnings)
				{
					goto IL_0189;
				}
				string text3 = base.name;
				string text4 = text3 + ": No clipboard surface found with tag '" + clipboardSurfaceTag + "'.";
				context = this;
				message = text4;
			}
			Debug.LogWarning(message, context);
		}
		goto IL_0189;
		IL_0189:
		return false;
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

	public MoveToClipboardCenterBridge3D()
	{
		//IL_0093: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A85C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoResolveByTag = true;
		clipboardSurfaceTag = "ClipboardSurface";
		useClipboardSlotCyclerIfPresent = true;
		autoMoveDelaySeconds = 0.75f;
		autoMoveOnlyIfNotDragging = true;
		durationSeconds = 0.22f;
		parentToClipboardBeforeMove = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		baseScaleOverride = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		smoothScale = true;
		base._002Ector();
	}
}
