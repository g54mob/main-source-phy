using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapMarkerDragAdapter : MonoBehaviour, ICursorDraggable
{
	private DynamicCursorManager cursorManager;

	private List<InputActionReference> primaryClickActions;

	private bool enableActionsOnEnable;

	private bool _003CIsDragging_003Ek__BackingField;

	private Action m_DragStarted;

	private Action m_DragEnded;

	private Interactable _interactable;

	private bool _wasPressed;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _started;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _canceled;

	public bool IsDragging
	{
		get
		{
			return _003CIsDragging_003Ek__BackingField;
		}
		private set
		{
			_003CIsDragging_003Ek__BackingField = value;
		}
	}

	public event Action DragStarted
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 56;
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
			object obj = this + 56;
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
			object obj = this + 64;
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
			object obj = this + 64;
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
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Interactable interactable = default(Interactable);
		_interactable = interactable;
	}

	private void OnEnable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					if (action == null)
					{
						continue;
					}
					if (enableActionsOnEnable)
					{
						InputAction action2 = ((InputActionReference)obj).action;
						if (action2 == null)
						{
							throw new NullReferenceException();
						}
						if (!action2.enabled)
						{
							InputAction action3 = ((InputActionReference)obj).action;
							if (action3 == null)
							{
								break;
							}
							action3.Enable();
						}
					}
					InputAction action4 = ((InputActionReference)obj).action;
					if (_started != null)
					{
						if (_started.ContainsKey(action4))
						{
							continue;
						}
						Action<InputAction.CallbackContext> value = delegate
						{
							if (cursorManager != null && base.isActiveAndEnabled)
							{
								ResolveEdge();
							}
						};
						Action<InputAction.CallbackContext> value2 = delegate
						{
							if (base.isActiveAndEnabled)
							{
								ResolveEdge();
							}
						};
						InputAction action5 = ((InputActionReference)obj).action;
						if (_started != null)
						{
							_started.set_Item(action5, value);
							InputAction action6 = ((InputActionReference)obj).action;
							if (_canceled != null)
							{
								_canceled.set_Item(action6, value2);
								InputAction action7 = ((InputActionReference)obj).action;
								if (action7 != null)
								{
									action7.started += value;
									InputAction action8 = ((InputActionReference)obj).action;
									if (action8 != null)
									{
										action8.canceled += value2;
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			_wasPressed = false;
			return;
		}
		throw new NullReferenceException();
	}

	private unsafe void OnDisable()
	{
		//IL_003c: Expected I, but got O
		//IL_0088: Expected I, but got O
		//IL_01da: Expected I, but got O
		if (primaryClickActions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num = 0;
			List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj != null;
				num = unchecked((nint)null);
				if (!flag)
				{
					continue;
				}
				if ((object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					bool flag2 = action == null;
					num = unchecked((nint)null);
					if (flag2)
					{
						continue;
					}
					InputAction action2 = ((InputActionReference)obj).action;
					if (_started != null)
					{
						if (_started.TryGetValue(action2, out var value))
						{
							InputAction action3 = ((InputActionReference)obj).action;
							if (action3 == null)
							{
								throw new NullReferenceException();
							}
							action3.started -= value;
						}
						InputAction action4 = ((InputActionReference)obj).action;
						if (_canceled != null)
						{
							bool flag3 = _canceled.TryGetValue(action4, out var value2);
							bool flag4 = !flag3;
							nint num2 = 0;
							num = (nint)(&value2);
							if (!flag4)
							{
								InputAction action5 = ((InputActionReference)obj).action;
								if (action5 == null)
								{
									throw new NullReferenceException();
								}
								action5.canceled -= value2;
								num2 = 0;
								num = unchecked((nint)null);
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (_started != null)
			{
				_started.Clear();
				if (_canceled != null)
				{
					_canceled.Clear();
					if (_003CIsDragging_003Ek__BackingField)
					{
						_003CIsDragging_003Ek__BackingField = false;
						Action dragEnded = this.m_DragEnded;
						if (this.m_DragEnded != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v513.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
						}
					}
					_wasPressed = false;
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	private bool IsAnyActionPressed()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
		InputActionReference inputActionReference = default(InputActionReference);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if ((object)inputActionReference != null)
			{
				InputAction action = inputActionReference.action;
				if (action != null && action.enabled && action.IsPressed())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
					return true;
				}
			}
		}
		enumerator.Dispose();
		return false;
	}

	private void OnAnyStarted()
	{
		if (cursorManager != null && base.isActiveAndEnabled)
		{
			ResolveEdge();
		}
	}

	private void OnAnyCanceled()
	{
		if (base.isActiveAndEnabled)
		{
			ResolveEdge();
		}
	}

	private void ResolveEdge()
	{
		//IL_00a4: Expected O, but got I4
		bool flag = IsAnyActionPressed();
		Action action;
		if (flag)
		{
			_wasPressed = flag;
			if (_wasPressed)
			{
				return;
			}
			DynamicCursorManager dynamicCursorManager = cursorManager;
			if (!(dynamicCursorManager._currentHover == _interactable) || _003CIsDragging_003Ek__BackingField)
			{
				return;
			}
			action = this.m_DragStarted;
			_003CIsDragging_003Ek__BackingField = true;
			object obj = 0;
		}
		else
		{
			_wasPressed = flag;
			if (~(_wasPressed ? 1u : 0u) != 0 || !_003CIsDragging_003Ek__BackingField)
			{
				return;
			}
			action = this.m_DragEnded;
			_003CIsDragging_003Ek__BackingField = false;
		}
		if (action != null)
		{
			IntPtr invoke_impl = ((Delegate)action).invoke_impl;
			IntPtr method = ((Delegate)action).method;
			IntPtr method_code = ((Delegate)action).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v180 @ rax_v4 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	private void BeginDragInternal()
	{
		if (!_003CIsDragging_003Ek__BackingField)
		{
			_003CIsDragging_003Ek__BackingField = true;
			Action dragStarted = this.m_DragStarted;
			if (this.m_DragStarted != null)
			{
				IntPtr invoke_impl = ((Delegate)dragStarted).invoke_impl;
				IntPtr method = ((Delegate)dragStarted).method;
				IntPtr method_code = ((Delegate)dragStarted).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v34 @ rax_v1 (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private void EndDragInternal()
	{
		if (_003CIsDragging_003Ek__BackingField)
		{
			_003CIsDragging_003Ek__BackingField = false;
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

	public MapMarkerDragAdapter()
	{
		List<InputActionReference> list = new List<InputActionReference>();
		primaryClickActions = list;
		enableActionsOnEnable = true;
		_started = new Dictionary<InputAction, Action<InputAction.CallbackContext>>();
		_canceled = new Dictionary<InputAction, Action<InputAction.CallbackContext>>();
		base._002Ector();
	}

	private void _003COnEnable_003Eb__18_0(InputAction.CallbackContext ctx)
	{
		if (cursorManager != null && base.isActiveAndEnabled)
		{
			ResolveEdge();
		}
	}

	private void _003COnEnable_003Eb__18_1(InputAction.CallbackContext ctx)
	{
		if (base.isActiveAndEnabled)
		{
			ResolveEdge();
		}
	}
}
