using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClipboardPickUpHandler : MonoBehaviour
{
	private sealed class _003CSlideToClipboard_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItem item;

		public ClipboardPickUpHandler _003C_003E4__this;

		public DragSurface clipboard;

		public Vector3 target;

		private Vector3 _003Cstart_003E5__2;

		private float _003Celapsed_003E5__3;

		private float _003Cdur_003E5__4;

		private Vector3 _003CsurfNormal_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CSlideToClipboard_003Ed__29(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0068: Expected I4, but got I8
			//IL_034f: Expected I4, but got I8
			//IL_0883: Expected I4, but got O
			//IL_0392: Invalid comparison between I4 and F4
			//IL_03dd: Expected F4, but got I4
			//IL_00fc: Expected O, but got F4
			//IL_0673: Unknown result type (might be due to invalid IL or missing references)
			//IL_0678: Expected O, but got Unknown
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Expected O, but got Unknown
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_044a: Expected O, but got Unknown
			//IL_01cc: Expected O, but got F4
			//IL_0495: Unknown result type (might be due to invalid IL or missing references)
			//IL_049a: Expected O, but got Unknown
			//IL_0536: Invalid comparison between I4 and F4
			//IL_0581: Expected F4, but got I4
			//IL_07da: Unknown result type (might be due to invalid IL or missing references)
			//IL_07df: Expected O, but got Unknown
			//IL_082c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0831: Expected O, but got Unknown
			//IL_05de: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Expected O, but got Unknown
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c8: Expected O, but got Unknown
			ClipboardPickUpHandler clipboardPickUpHandler = _003C_003E4__this;
			object obj = default(object);
			if (_003C_003E1__state == 0)
			{
				DraggableItem draggableItem = item;
				_003C_003E1__state = -1;
				if ((object)item != null)
				{
					draggableItem.IsSliding = true;
					if ((object)item != null)
					{
						Transform transform = item.transform;
						if ((object)transform != null)
						{
							Vector3 position = transform.position;
							_003Cstart_003E5__2 = (Vector3)position.x;
							_ = position.z;
							_003Celapsed_003E5__3 = 0f;
							if ((object)_003C_003E4__this != null)
							{
								bool flag = !(0.0001f < clipboardPickUpHandler.slideDuration);
								float num = 0.0001f;
								if (!flag)
								{
									num = clipboardPickUpHandler.slideDuration;
								}
								_003Cdur_003E5__4 = num;
								if ((object)clipboard != null)
								{
									Vector3 planeNormal = clipboard.GetPlaneNormal();
									Vector3 vector = (Vector3)(obj - 80);
									_ = planeNormal.x;
									_ = planeNormal.z;
									Vector3 normalized = ((Vector3*)vector)->normalized;
									DragSurface dragSurface = clipboard;
									_003CsurfNormal_003E5__5 = (Vector3)normalized.x;
									_ = normalized.z;
									if ((object)clipboard != null)
									{
										if (!dragSurface.preferAlignRotationOnEnter)
										{
											goto IL_02e4;
										}
										if ((object)item != null)
										{
											Transform transform2 = item.transform;
											if ((object)clipboard != null)
											{
												Transform transform3 = clipboard.transform;
												if ((object)transform3 != null)
												{
													Quaternion rotation = transform3.rotation;
													if ((object)transform2 != null)
													{
														Quaternion rotation2 = (Quaternion)(obj - 64);
														_ = rotation.x;
														transform2.rotation = rotation2;
														goto IL_02e4;
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
				goto IL_0875;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_0867;
			}
			_003C_003E1__state = -1;
			goto IL_08ac;
			IL_08ac:
			if (_003Cdur_003E5__4 > _003Celapsed_003E5__3)
			{
				float deltaTime = Time.deltaTime;
				float num2 = (_003Celapsed_003E5__3 = deltaTime + _003Celapsed_003E5__3) / _003Cdur_003E5__4;
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
				if ((object)clipboard != null)
				{
					Transform transform4 = clipboard.transform;
					if ((object)clipboard != null)
					{
						Transform transform5 = clipboard.transform;
						if ((object)transform5 != null)
						{
							Vector3 position2 = (Vector3)(obj - 80);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardPickUpHandler+<SlideToClipboard>d__29)+40]");
							_ = 0;
							_ = target;
							Vector3 vector2 = transform5.InverseTransformPoint(position2);
							if ((object)transform4 != null)
							{
								Vector3 position3 = (Vector3)(obj - 80);
								_ = vector2.x;
								_ = vector2.z;
								Vector3 vector3 = transform4.TransformPoint(position3);
								_ = vector3.x;
								if ((object)item != null)
								{
									Transform transform6 = item.transform;
									_ = _003Cstart_003E5__2;
									float num3 = 1f - num2;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
									float num4 = 1f - num3;
									if (!(0f > num4))
									{
										if (num4 > 1f)
										{
											num4 = 1f;
										}
									}
									else
									{
										num4 = 0f;
									}
									float num5 = vector3.z;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardPickUpHandler+<SlideToClipboard>d__29)+4C]");
									float num6 = num5 - 0f;
									float num7 = num6 * num4;
									float num8 = num7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardPickUpHandler+<SlideToClipboard>d__29)+4C]");
									float num9 = num8 + 0f;
									if ((object)_003C_003E4__this != null)
									{
										_ = _003CsurfNormal_003E5__5;
										float num10 = clipboardPickUpHandler.slideLift;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardPickUpHandler+<SlideToClipboard>d__29)+60]");
										float num11 = num10 * 0f;
										float num12 = num11 + num9;
										if ((object)transform6 != null)
										{
											Vector3 position4 = (Vector3)(obj - 64);
											transform6.position = position4;
											_003C_003E2__current = null;
											_003C_003E1__state = 1;
											return true;
										}
									}
								}
							}
						}
					}
				}
			}
			else if ((object)item != null)
			{
				Transform transform7 = item.transform;
				if ((object)transform7 != null)
				{
					Vector3 position5 = (Vector3)(obj - 64);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardPickUpHandler+<SlideToClipboard>d__29)+40]");
					_ = 0;
					_ = target;
					transform7.position = position5;
					DraggableItem draggableItem2 = item;
					if ((object)item != null)
					{
						draggableItem2.IsSliding = false;
						ApplyFinalRestingPosition(item, clipboard);
						DragSurface dragSurface2 = clipboard;
						if ((object)clipboard != null)
						{
							if (!dragSurface2.clampToBounds)
							{
								goto IL_0857;
							}
							if ((object)item != null)
							{
								Transform transform8 = item.transform;
								if ((object)item != null)
								{
									Transform transform9 = item.transform;
									if ((object)transform9 != null)
									{
										Vector3 position6 = transform9.position;
										if ((object)clipboard != null)
										{
											Vector3 worldPos = (Vector3)(obj - 64);
											_ = position6.x;
											_ = position6.z;
											Vector3 vector4 = clipboard.ClampOnSurfacePreserveNormalOffset(worldPos);
											if ((object)transform8 != null)
											{
												Vector3 position7 = (Vector3)(obj - 64);
												_ = vector4.x;
												_ = vector4.z;
												transform8.position = position7;
												goto IL_0857;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_0875;
			IL_0867:
			return false;
			IL_0857:
			TrySettleIntoSlotOrDeck(item);
			goto IL_0867;
			IL_02e4:
			if ((object)item == null)
			{
				goto IL_0875;
			}
			item.ApplySurfaceScale(clipboard, true);
			goto IL_08ac;
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

	private InputActionReference pickUpAction;

	private bool enableActionOnEnable;

	private DynamicCursorManager cursorManager;

	private HoverTooltip tooltip;

	private string clipboardSurfaceTag;

	private bool animate;

	private float slideDuration;

	private float slideLift;

	private bool useSlotCyclerIfPresent;

	private bool blockPickUpFromSlot;

	private UnityEvent<GameObject> onItemPickedUp;

	private UnityEvent<GameObject> onPickUpBlocked;

	private bool debugLogs;

	private DragSurface _clipboardSurface;

	private DragSurfaceSlotCycler _cycler;

	private DraggableItem _tooltipTarget;

	private void Awake()
	{
		if (!cursorManager)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DynamicCursorManager dynamicCursorManager = default(DynamicCursorManager);
			cursorManager = dynamicCursorManager;
		}
		if (!cursorManager)
		{
			string text = base.name;
			string message = "[ClipboardPickUpHandler:" + text + "] DynamicCursorManager not found on this GameObject and none assigned in the Inspector. Pick-up will not function.";
			Debug.LogError(message, this);
		}
		ResolveClipboardSurface();
	}

	private void OnEnable()
	{
		ResolveClipboardSurface();
		if (pickUpAction != null)
		{
			InputAction action = pickUpAction.action;
			if (action != null)
			{
				if (enableActionOnEnable)
				{
					InputAction action2 = pickUpAction.action;
					if (!action2.enabled)
					{
						InputAction action3 = pickUpAction.action;
						action3.Enable();
					}
				}
				InputAction action4 = pickUpAction.action;
				Action<InputAction.CallbackContext> value = OnPickUpPerformed;
				action4.performed += value;
				goto IL_013a;
			}
		}
		string text = base.name;
		string message = "[ClipboardPickUpHandler:" + text + "] 'Pick Up Action' is not assigned. Pick-up input will never fire.";
		Debug.LogWarning(message, this);
		goto IL_013a;
		IL_013a:
		if (cursorManager != null)
		{
			Action<Interactable> value2 = OnCursorTargetChanged;
			cursorManager.OnCursorTargetChanged += value2;
		}
	}

	private void OnDisable()
	{
		if (pickUpAction != null)
		{
			InputAction action = pickUpAction.action;
			if (action != null)
			{
				InputAction action2 = pickUpAction.action;
				Action<InputAction.CallbackContext> value = OnPickUpPerformed;
				action2.performed -= value;
			}
		}
		if (cursorManager != null)
		{
			Action<Interactable> value2 = OnCursorTargetChanged;
			cursorManager.OnCursorTargetChanged -= value2;
		}
		_tooltipTarget = null;
		if ((object)tooltip != null)
		{
			tooltip.Hide();
		}
	}

	private void OnCursorTargetChanged(Interactable hovered)
	{
		if (hovered != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DraggableItem draggableItem = default(DraggableItem);
			bool flag = (object)draggableItem != null;
			DraggableItem draggableItem2 = draggableItem;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				DraggableItem draggableItem3 = default(DraggableItem);
				draggableItem2 = draggableItem3;
			}
			if (IsValidPickUpCandidate(draggableItem2) && draggableItem2.showPickupTooltip)
			{
				_tooltipTarget = draggableItem2;
				if ((object)tooltip != null)
				{
					Transform worldAnchor = draggableItem2.transform;
					tooltip.Show(worldAnchor);
				}
			}
			else
			{
				_tooltipTarget = null;
				if ((object)tooltip != null)
				{
					tooltip.Hide();
				}
			}
		}
		else
		{
			_tooltipTarget = null;
			if ((object)tooltip != null)
			{
				tooltip.Hide();
			}
		}
	}

	private void Update()
	{
		if (_tooltipTarget != null && !IsValidPickUpCandidate(_tooltipTarget))
		{
			_tooltipTarget = null;
			if ((object)tooltip != null)
			{
				tooltip.Hide();
			}
		}
	}

	private void ShowTooltip(DraggableItem item)
	{
		_tooltipTarget = item;
		if ((object)tooltip != null)
		{
			Transform worldAnchor = item.transform;
			tooltip.Show(worldAnchor);
		}
	}

	private void HideTooltip()
	{
		_tooltipTarget = null;
		if ((object)tooltip != null)
		{
			tooltip.Hide();
		}
	}

	private bool IsValidPickUpCandidate(DraggableItem item)
	{
		//IL_01e0: Expected I4, but got O
		if ((bool)item && cursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if ((object)cursorManager == null)
			{
				goto IL_01d2;
			}
			if (!dynamicCursorManager._suppressedByLockBroker)
			{
				if (!_clipboardSurface)
				{
					ResolveClipboardSurface();
				}
				if ((bool)_clipboardSurface)
				{
					if ((object)item == null)
					{
						goto IL_01d2;
					}
					bool flag = item.surfaceRef == _clipboardSurface;
					if (!flag && item.IsBeingDragged == flag && item.IsSliding == flag && (blockPickUpFromSlot == flag || item.CurrentLocation != DraggableItem.ItemLocation.Slot))
					{
						return true;
					}
				}
			}
		}
		return false;
		IL_01d2:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void OnPickUpPerformed(InputAction.CallbackContext ctx)
	{
		TryPickUp();
	}

	public void TryPickUp()
	{
		if ((bool)cursorManager)
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			GameObject arg;
			UnityEvent<GameObject> unityEvent;
			if (!dynamicCursorManager._suppressedByLockBroker)
			{
				if (!dynamicCursorManager._currentHover)
				{
					Log("Blocked — no Interactable is currently hovered.");
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				UnityEngine.Object obj = default(UnityEngine.Object);
				bool flag = (object)obj != null;
				UnityEngine.Object obj2 = obj;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
					UnityEngine.Object obj3 = default(UnityEngine.Object);
					obj2 = obj3;
				}
				if (!obj2)
				{
					string text = dynamicCursorManager._currentHover.name;
					string message = "Blocked — hovered Interactable '" + text + "' has no DraggableItem.";
					Log(message);
					return;
				}
				if (!_clipboardSurface)
				{
					ResolveClipboardSurface();
				}
				if ((bool)_clipboardSurface)
				{
					if (IsValidPickUpCandidate((DraggableItem)obj2))
					{
						HideTooltip();
						ExecutePickUp((DraggableItem)obj2);
						return;
					}
					string text2 = obj2.name;
					string message2 = "Blocked — '" + text2 + "' failed validity check.";
					Log(message2);
				}
				else
				{
					string text3 = base.name;
					string text4 = obj2.name;
					string message3 = "[ClipboardPickUpHandler:" + text3 + "] No DragSurface found with tag '" + clipboardSurfaceTag + "'. Cannot pick up '" + text4 + "'.";
					Debug.LogWarning(message3, this);
				}
				if (onPickUpBlocked == null)
				{
					return;
				}
				GameObject gameObject = ((Component)obj2).gameObject;
				arg = gameObject;
				unityEvent = onPickUpBlocked;
			}
			else
			{
				Log("Blocked — DynamicCursorManager is suppressed.");
				if (onPickUpBlocked == null)
				{
					return;
				}
				arg = null;
				unityEvent = onPickUpBlocked;
			}
			unityEvent.Invoke(arg);
		}
		else
		{
			Log("Blocked — cursorManager is null.");
		}
	}

	private unsafe void ExecutePickUp(DraggableItem item)
	{
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Expected O, but got Unknown
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected Ref, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected Ref, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_0248: Invalid comparison between F4 and I4
		//IL_01a6: Expected F4, but got I
		//IL_01b6: Expected F4, but got I
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Expected O, but got Unknown
		//IL_0283: Expected O, but got F4
		DragSurface clipboardSurface = _clipboardSurface;
		if (item.CurrentLocation == DraggableItem.ItemLocation.Slot)
		{
			item.SlotRef?.RemoveItem(item);
		}
		if (item.surfaceRef != null)
		{
			item.surfaceRef.RemoveItem(item);
		}
		_clipboardSurface.AddItem(item);
		item.surfaceRef = _clipboardSurface;
		item.CurrentLocation = DraggableItem.ItemLocation.Surface;
		Transform transform = item.transform;
		Transform parent = _clipboardSurface.transform;
		transform.SetParent(parent, worldPositionStays: true);
		_ = 0;
		_ = 0;
		_ = 0;
		object obj = default(object);
		if (useSlotCyclerIfPresent && _cycler != null && _cycler.TryGetNextSlotWorldPosition(out *(Vector3*)(obj - 80), out *(int*)(obj + 32)))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-50]");
			float num = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-48]");
			float num2 = 0f;
		}
		else
		{
			Transform transform2 = item.transform;
			Vector3 position = transform2.position;
			float num = position.x;
			float num2 = position.z;
		}
		Vector3 worldPos = (Vector3)(obj - 64);
		Vector3 vector = _clipboardSurface.ClampOnSurface(worldPos);
		string arg = item.name;
		object obj2 = obj - 48;
		_ = vector.x;
		_ = vector.z;
		object arg2 = (Vector3)obj2;
		string message = $"Success — picking up '{arg}' → {arg2}.";
		Log(message);
		if (animate && slideDuration > 0f)
		{
			_003CSlideToClipboard_003Ed__29 obj3 = new _003CSlideToClipboard_003Ed__29(0);
			obj3._003C_003E1__state = 0;
			obj3._003C_003E4__this = this;
			obj3.item = item;
			obj3.target = (Vector3)vector.x;
			_ = vector.z;
			obj3.clipboard = _clipboardSurface;
			Coroutine coroutine = item.StartCoroutine(obj3);
		}
		else
		{
			Transform transform3 = item.transform;
			_ = vector.x;
			Vector3 position2 = (Vector3)(obj - 48);
			_ = vector.z;
			transform3.position = position2;
			item.StackingNormalOffset = 0f;
			if (clipboardSurface.preferAlignRotationOnEnter)
			{
				Transform transform4 = item.transform;
				Transform transform5 = _clipboardSurface.transform;
				Quaternion rotation = transform5.rotation;
				Quaternion rotation2 = (Quaternion)(obj - 32);
				_ = rotation.x;
				transform4.rotation = rotation2;
			}
			item.ApplySurfaceScale(_clipboardSurface, false);
			TrySettleIntoSlotOrDeck(item);
		}
		if (onItemPickedUp != null)
		{
			GameObject arg3 = item.gameObject;
			onItemPickedUp.Invoke(arg3);
		}
		if (item.OnPickedUpToClipboard != null)
		{
			item.OnPickedUpToClipboard.Invoke();
		}
	}

	private unsafe Vector3 ResolveDestination(DragSurface clipboard, DraggableItem item)
	{
		//IL_0150: Expected O, but got Ref
		//IL_0161: Expected native int or pointer, but got O
		//IL_0173: Expected native int or pointer, but got O
		if (!useSlotCyclerIfPresent || !(_cycler != null))
		{
			goto IL_009f;
		}
		if ((object)_cycler != null)
		{
			if (!_cycler.TryGetNextSlotWorldPosition(out var _, out var _))
			{
				goto IL_009f;
			}
			if ((object)clipboard != null)
			{
				goto IL_0143;
			}
		}
		goto IL_0116;
		IL_009f:
		if ((object)item != null)
		{
			Transform transform = item.transform;
			if ((object)transform != null)
			{
				Vector3 position = transform.position;
				if ((object)clipboard != null)
				{
					goto IL_0143;
				}
			}
		}
		goto IL_0116;
		IL_0116:
		return (Vector3)new NullReferenceException();
		IL_0143:
		float num = default(float);
		Vector3 vector = clipboard.ClampOnSurface((Vector3)(&num));
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
	}

	private unsafe void SnapToDestination(DraggableItem item, Vector3 destination, DragSurface clipboard)
	{
		//IL_0020: Expected O, but got Ref
		//IL_0093: Expected O, but got Ref
		Transform transform = item.transform;
		float num = default(float);
		transform.position = (Vector3)(&num);
		item.StackingNormalOffset = 0f;
		if (clipboard.preferAlignRotationOnEnter)
		{
			Transform transform2 = item.transform;
			Transform transform3 = clipboard.transform;
			Quaternion rotation = transform3.rotation;
			transform2.rotation = (Quaternion)(&num);
		}
		item.ApplySurfaceScale(clipboard, false);
		TrySettleIntoSlotOrDeck(item);
	}

	private IEnumerator SlideToClipboard(DraggableItem item, Vector3 target, DragSurface clipboard)
	{
		//IL_0024: Expected O, but got F4
		_003CSlideToClipboard_003Ed__29 obj = new _003CSlideToClipboard_003Ed__29(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.item = item;
		obj.target = (Vector3)target.x;
		_ = target.z;
		obj.clipboard = clipboard;
		return obj;
	}

	private static void TrySettleIntoSlotOrDeck(DraggableItem item)
	{
		//IL_00f8: Expected F4, but got I4
		DraggableItem draggableItem = default(DraggableItem);
		if ((object)draggableItem != null && draggableItem.slotRefs != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ItemSlot>.Enumerator enumerator = default(List<ItemSlot>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						throw new NullReferenceException();
					}
					if (((ItemSlot)obj).Overlaps(draggableItem))
					{
						draggableItem.MoveToSlot((ItemSlot)obj);
						enumerator.Dispose();
						return;
					}
				}
			}
			enumerator.Dispose();
			if (DraggableItemDeckArea.AllDecks != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				float num = 0f;
				UnityEngine.Object obj2 = null;
				List<DraggableItemDeckArea>.Enumerator enumerator2 = default(List<DraggableItemDeckArea>.Enumerator);
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				while (true)
				{
					if (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (!obj3)
						{
							continue;
						}
						if ((object)obj3 == null)
						{
							break;
						}
						if (((DraggableItemDeckArea)obj3).Overlaps(draggableItem))
						{
							float overlapVolume = ((DraggableItemDeckArea)obj3).GetOverlapVolume(draggableItem);
							if (overlapVolume > num)
							{
								num = overlapVolume;
								obj2 = obj3;
							}
						}
						continue;
					}
					enumerator2.Dispose();
					if (obj2 != null)
					{
						draggableItem.MoveToDeck((DraggableItemDeckArea)obj2);
					}
					return;
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	private unsafe static void ApplyFinalRestingPosition(DraggableItem item, DragSurface surf)
	{
		//IL_06d1: Expected O, but got Ref
		//IL_0665: Expected O, but got Ref
		//IL_0149: Invalid comparison between F4 and I4
		//IL_0799: Expected O, but got I4
		//IL_07a3: Expected F4, but got I4
		//IL_0554: Expected F4, but got I4
		//IL_05ae: Expected O, but got Ref
		//IL_0286: Expected O, but got F4
		//IL_02ab: Expected O, but got F4
		//IL_02c7: Invalid comparison between F4 and I4
		//IL_0382: Expected O, but got F4
		//IL_03f7: Expected O, but got I
		//IL_0440: Invalid comparison between O and F4
		//IL_0504: Expected O, but got I4
		bool flag = surf;
		Vector3 position4;
		float num10 = default(float);
		Transform transform5;
		Transform transform6;
		if ((object)item != null)
		{
			if (flag && item.enableStackingOffset)
			{
				if ((object)surf != null)
				{
					Vector3 planeNormal = surf.GetPlaneNormal();
					Vector3 vector = default(Vector3);
					Vector3 normalized = vector.normalized;
					Vector3 planeOriginPoint = surf.GetPlaneOriginPoint();
					Transform transform = item.transform;
					if ((object)transform != null)
					{
						Vector3 position = transform.position;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
						float num = ((!(surf.surfaceScaleMultiplier > 0f)) ? 1f : surf.surfaceScaleMultiplier);
						float num2 = item.stackingDetectionRadius * item.stackingDetectionRadius;
						if (DragSurface.AllSurfaces != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							object obj = 0;
							float num3 = 0f;
							List<DragSurface>.Enumerator enumerator = default(List<DragSurface>.Enumerator);
							UnityEngine.Object obj2 = default(UnityEngine.Object);
							List<DraggableItem>.Enumerator enumerator2 = default(List<DraggableItem>.Enumerator);
							float num4 = default(float);
							object obj4 = default(object);
							object obj5 = default(object);
							object obj7 = default(object);
							object obj8 = default(object);
							while (enumerator.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								if (!obj2)
								{
									continue;
								}
								if ((object)obj2 != null)
								{
									if (!((Behaviour)obj2).isActiveAndEnabled)
									{
										continue;
									}
									Vector3 planeNormal2 = ((DragSurface)obj2).GetPlaneNormal();
									Vector3 normalized2 = vector.normalized;
									Vector3 planeOriginPoint2 = ((DragSurface)obj2).GetPlaneOriginPoint();
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v533 @ stack_-1D8_v10 (UnityEngine.Object)+50]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
										while (enumerator2.MoveNext())
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
											if (!((UnityEngine.Object)num4 != null))
											{
												continue;
											}
											bool flag2 = (UnityEngine.Object)num4 == item;
											if (flag2)
											{
												continue;
											}
											if (num4 != 0f)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ stack_-1C8_v8 (System.Single)+34]");
												if ((nint)0 != (flag2 ? 1 : 0))
												{
													continue;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ stack_-1C8_v8 (System.Single)+DC]");
												if ((nint)0 != (flag2 ? 1 : 0))
												{
													continue;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ stack_-1C8_v8 (System.Single)+9C]");
												if ((nint)0 == (flag2 ? 1 : 0))
												{
													continue;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ stack_-1C8_v8 (System.Single)+30]");
												if ((nint)0 != 1)
												{
													continue;
												}
												Transform transform2 = ((Component)num4).transform;
												if ((object)transform2 != null)
												{
													Vector3 position2 = transform2.position;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
													object obj3 = obj4 - obj5;
													object obj6 = obj7 - obj8;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1133 @ rax_v40+8]");
													nint num5 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1508 @ rax_v79+8]");
													object obj9 = num5 - 0;
													object obj10 = obj9 * obj9;
													object obj11 = obj6 * obj6;
													object obj12 = obj3 * obj3;
													object obj13 = obj12 + obj11;
													object obj14 = obj13 + obj10;
													bool flag3 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2);
													float x = normalized2.x;
													float x2 = planeOriginPoint2.x;
													if (flag3)
													{
														continue;
													}
													float num6 = num;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ stack_-1C8_v8 (System.Single)+A0]");
													float num7 = num6 * 0f;
													float num8 = num7;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v241 @ stack_-1C8_v8 (System.Single)+D8]");
													float num9 = num8 + 0f;
													if (obj != null)
													{
														bool flag4 = !(num3 > num9);
														x = normalized2.x;
														x2 = planeOriginPoint2.x;
														if (flag4)
														{
															continue;
														}
													}
													obj = 1;
													num3 = num9;
													x = normalized2.x;
													x2 = planeOriginPoint2.x;
													continue;
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										enumerator2.Dispose();
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							enumerator.Dispose();
							if (obj == null)
							{
								num3 = 0f;
							}
							item.StackingNormalOffset = num3;
							Transform transform3 = item.transform;
							if ((object)transform3 != null)
							{
								Vector3 position3 = transform3.position;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803F5980");
								Transform transform4 = item.transform;
								if ((object)transform4 != null)
								{
									position4 = (Vector3)(&num10);
									transform5 = transform4;
									goto IL_07cb;
								}
							}
						}
					}
				}
			}
			else
			{
				item.StackingNormalOffset = 0f;
				transform6 = item.transform;
				if (surf != null)
				{
					Transform transform7 = item.transform;
					if ((object)transform7 != null)
					{
						Vector3 position5 = transform7.position;
						if ((object)surf != null)
						{
							Vector3 vector2 = surf.ProjectOntoSurface((Vector3)(&num10));
							goto IL_06aa;
						}
					}
				}
				else
				{
					Transform transform8 = item.transform;
					if ((object)transform8 != null)
					{
						Vector3 vector2 = transform8.position;
						goto IL_06aa;
					}
				}
			}
		}
		goto IL_06de;
		IL_07cb:
		transform5.position = position4;
		return;
		IL_06aa:
		if ((object)transform6 == null)
		{
			goto IL_06de;
		}
		position4 = (Vector3)(&num10);
		transform5 = transform6;
		goto IL_07cb;
		IL_06de:
		throw new NullReferenceException();
	}

	private void ResolveClipboardSurface()
	{
		if ((bool)_clipboardSurface || string.IsNullOrEmpty(clipboardSurfaceTag))
		{
			return;
		}
		GameObject gameObject = GameObject.FindWithTag(clipboardSurfaceTag);
		if (!gameObject)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		DragSurface clipboardSurface = default(DragSurface);
		_clipboardSurface = clipboardSurface;
		if ((bool)_clipboardSurface)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DragSurfaceSlotCycler cycler = default(DragSurfaceSlotCycler);
			_cycler = cycler;
			string[] array = new string[5] { "Clipboard surface resolved: '", null, null, null, null };
			string text = _clipboardSurface.name;
			array[1] = text;
			array[2] = "'";
			bool flag = _cycler != null;
			bool flag2 = !flag;
			object obj = " (no slot cycler)";
			if (!flag2)
			{
				obj = " (slot cycler found)";
			}
			array[3] = (string)obj;
			array[4] = ".";
			string message = string.Concat(array);
			Log(message);
		}
		else
		{
			string text2 = base.name;
			string message2 = "[ClipboardPickUpHandler:" + text2 + "] GameObject tagged '" + clipboardSurfaceTag + "' exists but has no DragSurface component.";
			Debug.LogWarning(message2, gameObject);
		}
	}

	private void Log(string message)
	{
		if (debugLogs)
		{
			string text = base.name;
			string message2 = "[ClipboardPickUpHandler:" + text + "] " + message;
			Debug.Log(message2, this);
		}
	}

	public ClipboardPickUpHandler()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A9B3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		enableActionOnEnable = true;
		clipboardSurfaceTag = "ClipboardSurface";
		animate = true;
		slideDuration = 0.28f;
		slideLift = -0.015f;
		useSlotCyclerIfPresent = true;
		base._002Ector();
	}
}
