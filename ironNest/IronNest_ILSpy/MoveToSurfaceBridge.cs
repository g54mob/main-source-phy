using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class MoveToSurfaceBridge : MonoBehaviour
{
	public enum TriggerOnceConsumeMode
	{
		OnTriggerStart,
		OnMoveCompleted
	}

	private sealed class _003CAnimateRoutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToSurfaceBridge _003C_003E4__this;

		private Vector3 _003CstartLocalPos_003E5__2;

		private Quaternion _003CstartRot_003E5__3;

		private Vector3 _003CstartScale_003E5__4;

		private Quaternion _003CtargetRot_003E5__5;

		private Vector3 _003CtargetScale_003E5__6;

		private float _003Cdur_003E5__7;

		private float _003Ct_003E5__8;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateRoutine_003Ed__36(int _003C_003E1__state)
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
			//IL_0379: Expected I4, but got I8
			//IL_0981: Expected I4, but got O
			//IL_0113: Expected O, but got I
			//IL_0500: Invalid comparison between I4 and F4
			//IL_00a5: Expected O, but got I
			//IL_054b: Expected F4, but got I4
			//IL_0561: Expected O, but got I
			//IL_0180: Expected O, but got Ref
			//IL_01b4: Expected O, but got F4
			//IL_0590: Expected O, but got Ref
			//IL_05dc: Invalid comparison between I4 and F4
			//IL_020b: Expected O, but got F4
			//IL_0631: Expected F4, but got I4
			//IL_0258: Expected O, but got F4
			//IL_0644: Expected O, but got Ref
			//IL_0298: Expected O, but got I
			//IL_02d5: Expected O, but got F4
			//IL_086b: Invalid comparison between I4 and F4
			//IL_02f8: Expected O, but got F4
			//IL_0313: Invalid comparison between F4 and I
			//IL_08b6: Expected F4, but got I4
			//IL_0b0c: Expected O, but got I
			//IL_0343: Expected F4, but got I
			//IL_0abd: Expected O, but got Ref
			//IL_06fd: Expected F4, but got O
			//IL_0add: Expected O, but got Ref
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
				num = _003Ct_003E5__8;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_03a2;
				}
			}
			goto IL_0973;
			IL_0794:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+5B]");
			float num2;
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+68]");
				Transform transform3;
				if ((nint)0 == 0)
				{
					if (_003Ct_003E5__8 < 1f)
					{
						goto IL_08ca;
					}
					transform3 = _003C_003E4__this.transform;
					if ((object)transform3 == null)
					{
						goto IL_0973;
					}
					_ = _003CtargetScale_003E5__6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToSurfaceBridge+<AnimateRoutine>d__36)+68]");
					_ = 0;
				}
				else
				{
					transform3 = _003C_003E4__this.transform;
					_ = _003CtargetScale_003E5__6;
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
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToSurfaceBridge+<AnimateRoutine>d__36)+68]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToSurfaceBridge+<AnimateRoutine>d__36)+4C]");
					object obj3 = num3 - 0;
					float num4 = (float)obj3 * num2;
					float num5 = num4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToSurfaceBridge+<AnimateRoutine>d__36)+4C]");
					float num6 = num5 + 0f;
					if ((object)transform3 == null)
					{
						goto IL_0973;
					}
				}
				Vector3 localScale = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				transform3.localScale = localScale;
			}
			goto IL_08ca;
			IL_0973:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_049c:
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			return false;
			IL_08ca:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
			IL_00dd:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
				Transform transform4 = ((Component)0).transform;
				Transform transform5 = _003C_003E4__this.transform;
				if ((object)transform5 != null)
				{
					Vector3 position = transform5.position;
					if ((object)transform4 != null)
					{
						Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
						_ = position.x;
						_ = position.z;
						Vector3 vector = transform4.InverseTransformPoint(position2);
						_003CstartLocalPos_003E5__2 = (Vector3)vector.x;
						_ = vector.z;
						Transform transform6 = _003C_003E4__this.transform;
						if ((object)transform6 != null)
						{
							_003CstartRot_003E5__3 = (Quaternion)transform6.rotation.x;
							Transform transform7 = _003C_003E4__this.transform;
							if ((object)transform7 != null)
							{
								Vector3 localScale2 = transform7.localScale;
								_003CstartScale_003E5__4 = (Vector3)localScale2.x;
								_ = localScale2.z;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
									Transform transform8 = ((Component)0).transform;
									if ((object)transform8 != null)
									{
										_003CtargetRot_003E5__5 = (Quaternion)transform8.rotation.x;
										Vector3 vector2 = _003C_003E4__this.ComputeTargetLocalScale();
										_003CtargetScale_003E5__6 = (Vector3)vector2.x;
										_ = vector2.z;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+54]");
										bool flag = !(0.0001f < 0f);
										float num7 = 0.0001f;
										if (!flag)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+54]");
											num7 = 0f;
										}
										_003Ct_003E5__8 = 0f;
										num = _003Ct_003E5__8;
										_003Cdur_003E5__7 = num7;
										goto IL_03a2;
									}
								}
							}
						}
					}
				}
			}
			goto IL_0973;
			IL_03a2:
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
								string message = "[" + name + "] AnimateRoutine: aborted (drag started mid-animation).";
								Debug.Log(message, _003C_003E4__this);
							}
							goto IL_049c;
						}
					}
				}
				float deltaTime = Time.deltaTime;
				float num8 = deltaTime / _003Cdur_003E5__7;
				float num9 = (_003Ct_003E5__8 = num8 + _003Ct_003E5__8);
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
				float num10 = num9 + num9;
				float num11 = num9 * num9;
				float num12 = 3f - num10;
				num2 = num12 * num11;
				Vector3 destinationWorldPosition = _003C_003E4__this.GetDestinationWorldPosition();
				_ = destinationWorldPosition.x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+28]");
					Transform transform9 = ((Component)0).transform;
					if ((object)transform9 != null)
					{
						Vector3 position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MoveToSurfaceBridge+<AnimateRoutine>d__36)+30]");
						_ = 0;
						_ = _003CstartLocalPos_003E5__2;
						Vector3 vector3 = transform9.TransformPoint(position3);
						_ = vector3.x;
						Transform transform10 = _003C_003E4__this.transform;
						float num13 = ((0f > num2) ? 0f : ((num2 > 1f) ? 1f : num2));
						float num14 = destinationWorldPosition.z - vector3.z;
						float num15 = num14 * num13;
						float num16 = num15 + vector3.z;
						if ((object)transform10 != null)
						{
							Vector3 position4 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
							transform10.position = position4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+59]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+5A]");
								Transform transform12;
								if ((nint)0 == 0)
								{
									if (_003Ct_003E5__8 < 1f)
									{
										goto IL_0794;
									}
									Transform transform11 = _003C_003E4__this.transform;
									if ((object)transform11 == null)
									{
										goto IL_0973;
									}
									float num17 = (float)_003CtargetRot_003E5__5;
									transform12 = transform11;
								}
								else
								{
									Transform transform13 = _003C_003E4__this.transform;
									ref Quaternion b = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
									ref Quaternion a = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
									_ = _003CtargetRot_003E5__5;
									_ = _003CstartRot_003E5__3;
									Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, num2);
									if ((object)transform13 == null)
									{
										goto IL_0973;
									}
									float num17 = quaternion.x;
									transform12 = transform13;
								}
								Quaternion rotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
								transform12.rotation = rotation;
							}
							goto IL_0794;
						}
					}
				}
				goto IL_0973;
			}
			MoveToSurfaceBridge moveToSurfaceBridge = _003C_003E4__this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+5B]");
			nint num18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+59]");
			moveToSurfaceBridge.SnapToDestination((byte)num18 != 0, applyRotation: false);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+41]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1 (UnityEngine.Component)+44]");
				if ((nint)0 == 1)
				{
					_ = 1;
				}
			}
			goto IL_049c;
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

	private sealed class _003CAutoMoveRoutine_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveToSurfaceBridge _003C_003E4__this;

		private float _003Cremaining_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoMoveRoutine_003Ed__35(int _003C_003E1__state)
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
								string message = "[" + name + "] AutoMove cancelled (dragging).";
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
			_003C_003E4__this.MoveToDestinationNow();
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

	private DragSurface destinationSurface;

	private bool autoResolveByTag;

	private string destinationSurfaceTag;

	private bool useSlotCyclerIfPresent;

	private bool triggerOnlyOncePerInstantiation;

	private TriggerOnceConsumeMode triggerOnceMode;

	private bool autoMoveAfterDelay;

	private float autoMoveDelaySeconds;

	private bool skipMoveWhileDragging;

	private bool animate;

	private float durationSeconds;

	private bool parentToDestinationBeforeMove;

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

	private DragSurfaceSlotCycler _cycler;

	private bool _hasAllocatedSlot;

	private Vector3 _allocatedLocalPosition;

	private bool _hasConsumedOneShot;

	private void Awake()
	{
		//IL_002b: Expected O, but got F4
		//IL_009a: Expected O, but got I4
		Transform transform = base.transform;
		Vector3 localScale = transform.localScale;
		_capturedBaseLocalScale = (Vector3)localScale.x;
		_ = localScale.z;
		_hasConsumedOneShot = false;
		ResolveDraggableReference();
		if (autoResolveByTag)
		{
			bool flag = ResolveDestinationByTag(logWarnings: false);
		}
		ResolveCyclerReference();
		_hasAllocatedSlot = false;
		_allocatedLocalPosition = (Vector3)0;
		_ = 0;
	}

	private void OnEnable()
	{
		//IL_0050: Expected O, but got I4
		if (autoResolveByTag)
		{
			bool flag = ResolveDestinationByTag(logWarnings: false);
		}
		ResolveCyclerReference();
		_hasAllocatedSlot = false;
		_allocatedLocalPosition = (Vector3)0;
		_ = 0;
		if (autoMoveAfterDelay)
		{
			if (_routine != null)
			{
				StopCoroutine(_routine);
			}
			_003CAutoMoveRoutine_003Ed__35 obj = new _003CAutoMoveRoutine_003Ed__35(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine routine = StartCoroutine(obj);
			_routine = routine;
		}
	}

	private void OnDisable()
	{
		//IL_004d: Expected O, but got I4
		if (_routine != null)
		{
			StopCoroutine(_routine);
			_routine = null;
		}
		_hasAllocatedSlot = false;
		_allocatedLocalPosition = (Vector3)0;
		_ = 0;
	}

	public void MoveToDestinationNow()
	{
		//IL_0375: Expected O, but got I4
		//IL_0204: Invalid comparison between I4 and F4
		string text;
		string text2;
		if (triggerOnlyOncePerInstantiation && _hasConsumedOneShot)
		{
			if (debug)
			{
				text = base.name;
				text2 = "] MoveToDestinationNow: ignored (one-shot consumed).";
				goto IL_02f9;
			}
			return;
		}
		if (autoResolveByTag)
		{
			bool flag = ResolveDestinationByTag(logWarnings: true);
		}
		ResolveCyclerReference();
		if ((bool)destinationSurface)
		{
			if (_routine != null)
			{
				StopCoroutine(_routine);
				_routine = null;
			}
			if (skipMoveWhileDragging && _draggable != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if (obj != null)
				{
					if (debug)
					{
						text = base.name;
						text2 = "] MoveToDestinationNow: skipped (object is being dragged).";
						goto IL_02f9;
					}
					return;
				}
			}
			AllocateSlotOnceForThisMove();
			if (triggerOnlyOncePerInstantiation && triggerOnceMode == TriggerOnceConsumeMode.OnTriggerStart)
			{
				_hasConsumedOneShot = true;
			}
			if (animate && 0f < durationSeconds)
			{
				_003CAnimateRoutine_003Ed__36 obj2 = new _003CAnimateRoutine_003Ed__36(0);
				obj2._003C_003E1__state = 0;
				obj2._003C_003E4__this = this;
				Coroutine routine = StartCoroutine(obj2);
				_routine = routine;
				return;
			}
			SnapToDestination(applyScale: true, applyRotation: true);
			if (triggerOnlyOncePerInstantiation && triggerOnceMode == TriggerOnceConsumeMode.OnMoveCompleted)
			{
				_hasConsumedOneShot = true;
			}
			_hasAllocatedSlot = false;
			_allocatedLocalPosition = (Vector3)0;
			_ = 0;
		}
		else
		{
			string text3 = base.name;
			string message = "[" + text3 + "] MoveToDestinationNow: destinationSurface is missing.";
			Debug.LogError(message, this);
		}
		return;
		IL_02f9:
		string message2 = "[" + text + text2;
		Debug.Log(message2, this);
	}

	private unsafe void AllocateSlotOnceForThisMove()
	{
		//IL_0181: Expected O, but got I4
		//IL_0089: Expected O, but got Ref
		//IL_00ad: Expected O, but got F4
		_hasAllocatedSlot = false;
		_allocatedLocalPosition = (Vector3)0;
		_ = 0;
		if (useSlotCyclerIfPresent && _cycler != null && _cycler.TryGetNextSlotWorldPosition(out var _, out var _))
		{
			_hasAllocatedSlot = true;
			Transform transform = destinationSurface.transform;
			Vector3 vector2 = default(Vector3);
			Vector3 vector = transform.InverseTransformPoint((Vector3)(&vector2));
			bool flag = !debug;
			_allocatedLocalPosition = (Vector3)vector.x;
			_ = vector.z;
			if (!flag)
			{
				string arg = base.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string text = $"[{arg}] Slot allocated: index={arg2}, ";
				object arg3 = vector2;
				string text2 = $"worldPosition={arg3}, ";
				object obj = default(object);
				object arg4 = (Vector3)obj;
				string text3 = $"localPosition={arg4}";
				string message = text + text2 + text3;
				Debug.Log(message, this);
			}
		}
	}

	private void ClearAllocatedSlot()
	{
		//IL_0016: Expected O, but got I4
		_hasAllocatedSlot = false;
		_allocatedLocalPosition = (Vector3)0;
		_ = 0;
	}

	private IEnumerator AutoMoveRoutine()
	{
		_003CAutoMoveRoutine_003Ed__35 obj = new _003CAutoMoveRoutine_003Ed__35(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator AnimateRoutine()
	{
		_003CAnimateRoutine_003Ed__36 obj = new _003CAnimateRoutine_003Ed__36(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void SnapToDestination(bool applyScale, bool applyRotation)
	{
		//IL_00ad: Expected O, but got Ref
		//IL_014c: Expected O, but got Ref
		//IL_0108: Expected O, but got Ref
		if ((bool)destinationSurface)
		{
			if (parentToDestinationBeforeMove)
			{
				Transform transform = base.transform;
				Transform parent = destinationSurface.transform;
				transform.SetParent(parent, worldPositionStays: true);
			}
			Transform transform2 = base.transform;
			Vector3 destinationWorldPosition = GetDestinationWorldPosition();
			float num = default(float);
			transform2.position = (Vector3)(&num);
			if (applyRotation)
			{
				Transform transform3 = base.transform;
				Transform transform4 = destinationSurface.transform;
				Quaternion rotation = transform4.rotation;
				object obj = default(object);
				transform3.rotation = (Quaternion)(&obj);
			}
			if (applyScale)
			{
				Transform transform5 = base.transform;
				Vector3 vector = ComputeTargetLocalScale();
				transform5.localScale = (Vector3)(&num);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			if (obj2 != null)
			{
				((DraggableItem)obj2).SettleOnSurface(destinationSurface);
			}
		}
	}

	private unsafe Vector3 GetDestinationWorldPosition()
	{
		//IL_0408: Expected native int or pointer, but got O
		//IL_0516: Expected native int or pointer, but got O
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_0260: Expected O, but got I4
		//IL_01aa: Expected O, but got I4
		//IL_0219: Expected O, but got I4
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Expected O, but got Unknown
		//IL_02c1: Expected I, but got O
		//IL_02e1: Expected F4, but got I
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Expected O, but got Unknown
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Expected O, but got Unknown
		//IL_0571: Invalid comparison between F4 and O
		//IL_02f5: Expected O, but got I4
		//IL_0590: Expected native int or pointer, but got O
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Expected O, but got Unknown
		object obj = default(object);
		Vector3 vector;
		float z;
		Vector3 vector2 = default(Vector3);
		if ((bool)destinationSurface)
		{
			if ((object)destinationSurface != null)
			{
				if (_hasAllocatedSlot)
				{
					Transform transform = destinationSurface.transform;
					if ((object)transform != null)
					{
						Vector3 position = (Vector3)(obj - 48);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToSurfaceBridge)+AC]");
						_ = 0;
						_ = _allocatedLocalPosition;
						vector = transform.TransformPoint(position);
						goto IL_010d;
					}
				}
				else
				{
					Transform transform2 = destinationSurface.transform;
					if ((object)transform2 != null)
					{
						vector = transform2.position;
						goto IL_010d;
					}
				}
			}
		}
		else
		{
			Transform transform3 = base.transform;
			if ((object)transform3 != null)
			{
				Vector3 position2 = transform3.position;
				z = position2.z;
				((Vector3*)(nint)vector2)->x = position2.x;
				goto IL_050e;
			}
		}
		goto IL_0412;
		IL_0412:
		return (Vector3)new NullReferenceException();
		IL_050e:
		((Vector3*)(nint)vector2)->z = z;
		return vector2;
		IL_010d:
		_ = vector.x;
		Component component = destinationSurface;
		Vector3 vector3;
		if ((object)destinationSurface != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rcx_v10 (UnityEngine.Component)+20]");
			if ((nint)0 == 1)
			{
				Transform transform4 = destinationSurface.transform;
				if ((object)transform4 != null)
				{
					vector3 = transform4.forward;
					object obj2 = 0;
					goto IL_0265;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rcx_v10 (UnityEngine.Component)+20]");
				if ((nint)0 == 2)
				{
					Transform transform5 = destinationSurface.transform;
					if ((object)transform5 != null)
					{
						vector3 = transform5.right;
						object obj2 = 0;
						goto IL_0265;
					}
				}
				else
				{
					Transform transform6 = destinationSurface.transform;
					if ((object)transform6 != null)
					{
						vector3 = transform6.up;
						object obj2 = 0;
						goto IL_0265;
					}
				}
			}
		}
		goto IL_0412;
		IL_0265:
		_ = vector3.x;
		_ = vector3.x;
		_ = vector3.z;
		object obj3 = obj - 32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num2;
		if (vector3.x > 1E-05f)
		{
			float num = vector3.z / vector3.x;
			num2 = num;
		}
		else
		{
			nint num3 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ rax_v26 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v470 @ rcx_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			num2 = 0f;
			_ = Vector3.zeroVector;
		}
		float num5 = 0f - destinationLift;
		float num6 = destinationLift;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = num6 & 0;
		if ((nint)obj4 < 0)
		{
			obj4 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj5 = num5 & 0;
		float num7 = (float)obj4 * 1E-06f;
		float num8 = Mathf.Epsilon * 8f;
		if (num7 < num8)
		{
			num7 = num8;
		}
		float defaultDragLift;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			DragSurface dragSurface = destinationSurface;
			if ((object)destinationSurface == null)
			{
				goto IL_0412;
			}
			defaultDragLift = dragSurface.defaultDragLift;
		}
		else
		{
			defaultDragLift = destinationLift;
		}
		bool flag = !clampToSurfaceBounds;
		float num9 = num2 * defaultDragLift;
		float num10 = num9 + vector.z;
		float num11 = default(float);
		float x = num11;
		z = num10;
		if (!flag)
		{
			if ((object)destinationSurface == null)
			{
				goto IL_0412;
			}
			Vector3 worldPos = (Vector3)(obj - 32);
			Vector3 vector4 = destinationSurface.ClampOnSurface(worldPos);
			x = vector4.x;
			z = vector4.z;
		}
		((Vector3*)(nint)vector2)->x = x;
		goto IL_050e;
	}

	private unsafe Vector3 ComputeTargetLocalScale()
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
		if (!applySurfaceScaleMultiplier || !destinationSurface)
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToSurfaceBridge)+64]");
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToSurfaceBridge)+64]");
				obj7 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MoveToSurfaceBridge)+90]");
				obj7 = 0;
			}
			DragSurface dragSurface = destinationSurface;
			if ((object)destinationSurface != null)
			{
				float z = dragSurface.surfaceScaleMultiplier * (float)obj7;
				((Vector3*)(nint)vector)->x = num4;
				((Vector3*)(nint)vector)->z = z;
				return vector;
			}
		}
		return (Vector3)new NullReferenceException();
	}

	private void ResolveCyclerReference()
	{
		_cycler = null;
		if (useSlotCyclerIfPresent && (bool)destinationSurface)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DragSurfaceSlotCycler cycler = default(DragSurfaceSlotCycler);
			_cycler = cycler;
		}
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
				draggableBehaviour = (MonoBehaviour)obj;
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
				string message = "[" + text + "] draggableBehaviour assigned but does not implement ICursorDraggable.";
				Debug.LogWarning(message, this);
			}
		}
	}

	private bool ResolveDestinationByTag(bool logWarnings)
	{
		//IL_03b4: Expected I4, but got O
		if (!(destinationSurface == null) || string.IsNullOrEmpty(destinationSurfaceTag))
		{
			goto IL_022d;
		}
		GameObject gameObject = GameObject.FindWithTag(destinationSurfaceTag);
		UnityEngine.Object context;
		string message;
		if (gameObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			DragSurface dragSurface = default(DragSurface);
			destinationSurface = dragSurface;
			if ((bool)destinationSurface)
			{
				return true;
			}
			if (!logWarnings)
			{
				goto IL_022d;
			}
			string[] array = new string[5];
			if (array.Length > 0)
			{
				array[0] = "[";
				string text = base.name;
				if (array.Length > 1)
				{
					array[1] = text;
					if (array.Length > 2)
					{
						array[2] = "] Tag '";
						if (array.Length > 3)
						{
							array[3] = destinationSurfaceTag;
							if (array.Length > 4)
							{
								array[4] = "' object has no DragSurface.";
								string text2 = string.Concat(array);
								context = gameObject;
								message = text2;
								goto IL_03b4;
							}
						}
					}
				}
			}
		}
		else
		{
			if (!logWarnings)
			{
				goto IL_022d;
			}
			string[] array2 = new string[5];
			if (array2.Length > 0)
			{
				array2[0] = "[";
				string text3 = base.name;
				if (array2.Length > 1)
				{
					array2[1] = text3;
					if (array2.Length > 2)
					{
						array2[2] = "] No GameObject found with tag '";
						if (array2.Length > 3)
						{
							array2[3] = destinationSurfaceTag;
							if (array2.Length > 4)
							{
								array2[4] = "'.";
								string text4 = string.Concat(array2);
								context = this;
								message = text4;
								goto IL_03b4;
							}
						}
					}
				}
			}
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
		IL_03b4:
		Debug.LogWarning(message, context);
		goto IL_022d;
		IL_022d:
		return false;
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

	public MoveToSurfaceBridge()
	{
		//IL_0093: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA3B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoResolveByTag = true;
		destinationSurfaceTag = "ClipboardSurface";
		useSlotCyclerIfPresent = true;
		autoMoveDelaySeconds = 0.75f;
		skipMoveWhileDragging = true;
		durationSeconds = 0.22f;
		parentToDestinationBeforeMove = true;
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
