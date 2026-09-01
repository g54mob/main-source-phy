using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class InteractableGroupTrigger : MonoBehaviour
{
	public enum TriggerPhase
	{
		OnPress,
		OnRelease,
		OnHoverEnter
	}

	[Serializable]
	public class InteractableUnityEvent : UnityEvent<Interactable>
	{
	}

	private DynamicCursorManager cursorManager;

	private string cursorManagerTag = "CursorManager";

	private bool autoDiscoverChildren = true;

	private List<Interactable> manualChildren;

	private TriggerPhase triggerPhase;

	private bool includePassiveChildren;

	private bool triggerOncePerChild;

	public InteractableUnityEvent OnChildInteracted;

	public UnityEvent OnAnyChildInteracted;

	public UnityEvent OnAllChildrenInteracted;

	private readonly HashSet<Interactable> _children;

	private readonly HashSet<Interactable> _alreadyTriggered;

	private bool _allChildrenEventFired;

	private void Awake()
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_013e: Expected O, but got I4
		//IL_0147: Expected O, but got I4
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		if (cursorManager == null)
		{
			DynamicCursorManager dynamicCursorManager = ResolveCursorManager();
			cursorManager = dynamicCursorManager;
		}
		if (!autoDiscoverChildren)
		{
			_children.Clear();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<Interactable>.Enumerator enumerator = default(List<Interactable>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj != null)
					{
						if (_children == null)
						{
							break;
						}
						_children.Add((Interactable)obj);
					}
					continue;
				}
				enumerator.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		_children.Clear();
		Interactable[] componentsInChildren = GetComponentsInChildren<Interactable>(includeInactive: true);
		object obj2 = componentsInChildren + 32;
		object obj3 = 0;
		object obj4 = 0;
		while ((nint)obj4 < componentsInChildren.Length)
		{
			_children.Add((Interactable)obj2);
			obj3++;
			obj2 += 8;
			obj4 = obj3;
		}
	}

	private DynamicCursorManager ResolveCursorManager()
	{
		if (!string.IsNullOrEmpty(cursorManagerTag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(cursorManagerTag);
			if (gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj == null)
				{
					string[] array = new string[5];
					if (array.Length > 0)
					{
						array[0] = "[InteractableGroupTrigger:";
						string text = base.name;
						if (array.Length > 1)
						{
							array[1] = text;
							if (array.Length > 2)
							{
								array[2] = "] GameObject tagged '";
								if (array.Length > 3)
								{
									array[3] = cursorManagerTag;
									if (array.Length > 4)
									{
										array[4] = "' does not have a DynamicCursorManager component. Falling back to FindFirstObjectByType.";
										string message = string.Concat(array);
										Debug.LogWarning(message, this);
										goto IL_01c1;
									}
								}
							}
						}
					}
					return (DynamicCursorManager)(object)new IndexOutOfRangeException();
				}
				return (DynamicCursorManager)obj;
			}
		}
		goto IL_01c1;
		IL_01c1:
		return UnityEngine.Object.FindFirstObjectByType<DynamicCursorManager>();
	}

	private void OnEnable()
	{
		//IL_0052: Expected O, but got I4
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Expected O, but got Unknown
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Expected O, but got Unknown
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Expected O, but got Unknown
		if (!(cursorManager != null))
		{
			return;
		}
		bool flag = triggerPhase == TriggerPhase.OnPress;
		Action<Interactable> action2;
		Delegate obj7;
		if (!flag)
		{
			object obj = triggerPhase - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return;
				}
				DynamicCursorManager dynamicCursorManager = cursorManager;
				Action<Interactable> action = HandleInteractableEvent;
				bool flag2 = (object)cursorManager == null;
				action2 = action;
				if (!flag2)
				{
					cursorManager.OnCursorTargetChanged += action;
					if (!includePassiveChildren)
					{
						return;
					}
					dynamicCursorManager = cursorManager;
					Action<Interactable> b = HandleInteractableEvent;
					bool flag3 = (object)cursorManager == null;
					action2 = action;
					if (!flag3)
					{
						Delegate obj2 = dynamicCursorManager.OnPassiveTargetChanged;
						object obj3 = cursorManager + 48;
						Delegate obj6 = default(Delegate);
						while (true)
						{
							Delegate obj4 = Delegate.Combine(obj2, b);
							bool flag4 = (object)obj4 == null;
							Delegate obj5 = obj4;
							if (!flag4)
							{
								((InteractableGroupTrigger)(object)obj4).HandleInteractableEvent((Interactable)(object)typeof(Action<Interactable>));
								bool flag5 = (object)obj5 == null;
								action2 = (Action<Interactable>)(object)typeof(Action<Interactable>);
								dynamicCursorManager = (DynamicCursorManager)(object)obj4;
								if (flag5)
								{
									break;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
							bool flag6 = (object)obj6 != obj2;
							obj2 = obj6;
							if (!flag6)
							{
								return;
							}
						}
						((InteractableGroupTrigger)(object)dynamicCursorManager).HandleInteractableEvent((Interactable)(object)action2);
						obj7 = (Delegate)(object)dynamicCursorManager;
						goto IL_03e6;
					}
				}
			}
			else
			{
				DynamicCursorManager dynamicCursorManager = cursorManager;
				Action<Interactable> b2 = HandleInteractableEvent;
				if ((object)cursorManager != null)
				{
					Delegate obj8 = dynamicCursorManager.OnPrimaryClickUp;
					object obj9 = cursorManager + 64;
					Delegate obj12 = default(Delegate);
					while (true)
					{
						Delegate obj10 = Delegate.Combine(obj8, b2);
						bool flag7 = (object)obj10 == null;
						Delegate obj11 = obj10;
						if (!flag7)
						{
							((InteractableGroupTrigger)(object)obj10).HandleInteractableEvent((Interactable)(object)typeof(Action<Interactable>));
							bool flag8 = (object)obj11 == null;
							action2 = (Action<Interactable>)(object)typeof(Action<Interactable>);
							obj7 = obj10;
							if (flag8)
							{
								break;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
						bool flag9 = (object)obj12 != obj8;
						obj8 = obj12;
						if (!flag9)
						{
							return;
						}
					}
					goto IL_03e6;
				}
			}
		}
		else
		{
			DynamicCursorManager dynamicCursorManager = cursorManager;
			Action<Interactable> b3 = HandleInteractableEvent;
			if ((object)cursorManager != null)
			{
				Delegate obj13 = dynamicCursorManager.OnPrimaryClickDown;
				object obj14 = cursorManager + 56;
				Delegate obj17 = default(Delegate);
				while (true)
				{
					Delegate obj15 = Delegate.Combine(obj13, b3);
					bool flag10 = (object)obj15 == null;
					Delegate obj16 = obj15;
					if (!flag10)
					{
						((InteractableGroupTrigger)(object)obj15).HandleInteractableEvent((Interactable)(object)typeof(Action<Interactable>));
						bool flag11 = (object)obj16 == null;
						action2 = (Action<Interactable>)(object)typeof(Action<Interactable>);
						obj7 = obj15;
						if (flag11)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag12 = (object)obj17 != obj13;
					obj13 = obj17;
					if (!flag12)
					{
						return;
					}
				}
				goto IL_0451;
			}
		}
		throw new NullReferenceException();
		IL_03e6:
		((InteractableGroupTrigger)(object)obj7).HandleInteractableEvent((Interactable)(object)action2);
		goto IL_0451;
		IL_0451:
		((InteractableGroupTrigger)(object)obj7).HandleInteractableEvent((Interactable)(object)action2);
	}

	private void OnDisable()
	{
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Expected O, but got Unknown
		//IL_0098: Expected I, but got O
		//IL_0312: Expected O, but got I
		//IL_011f: Expected I, but got O
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Expected O, but got Unknown
		//IL_0163: Expected I, but got O
		//IL_0378: Expected O, but got I
		//IL_01e0: Expected I, but got O
		//IL_0245: Expected I, but got O
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Expected O, but got Unknown
		//IL_0289: Expected I, but got O
		//IL_03e3: Expected O, but got I
		if (!(cursorManager != null))
		{
			return;
		}
		DynamicCursorManager dynamicCursorManager = cursorManager;
		Action<Interactable> value = HandleInteractableEvent;
		if ((object)cursorManager != null)
		{
			Delegate obj = dynamicCursorManager.OnPrimaryClickUp;
			object obj2 = cursorManager + 64;
			Delegate obj6 = default(Delegate);
			Delegate obj11 = default(Delegate);
			Delegate obj16 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				Delegate obj5;
				nint num;
				if (!flag)
				{
					((InteractableGroupTrigger)(object)obj3).HandleInteractableEvent((Interactable)(object)typeof(Action<Interactable>));
					bool flag2 = (object)obj4 == null;
					num = (nint)typeof(Action<Interactable>);
					obj5 = obj3;
					if (flag2)
					{
						goto IL_0305;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj6 != obj;
				obj = obj6;
				if (flag3)
				{
					continue;
				}
				dynamicCursorManager = cursorManager;
				Action<Interactable> value2 = HandleInteractableEvent;
				bool flag4 = (object)cursorManager == null;
				num = (nint)typeof(Action<Interactable>);
				if (flag4)
				{
					break;
				}
				Delegate obj7 = dynamicCursorManager.OnPrimaryClickDown;
				object obj8 = cursorManager + 56;
				while (true)
				{
					Delegate obj9 = Delegate.Remove(obj7, value2);
					bool flag5 = (object)obj9 == null;
					Delegate obj10 = obj9;
					if (!flag5)
					{
						((InteractableGroupTrigger)(object)obj9).HandleInteractableEvent((Interactable)(object)typeof(Action<Interactable>));
						bool flag6 = (object)obj10 == null;
						num = (nint)typeof(Action<Interactable>);
						obj5 = obj9;
						if (flag6)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag7 = (object)obj11 != obj7;
					obj7 = obj11;
					if (flag7)
					{
						continue;
					}
					goto IL_01ab;
				}
				goto IL_036b;
				IL_0305:
				((InteractableGroupTrigger)(object)obj5).HandleInteractableEvent((Interactable)num);
				return;
				IL_036b:
				((InteractableGroupTrigger)(object)obj5).HandleInteractableEvent((Interactable)num);
				goto IL_0305;
				IL_01ab:
				Action<Interactable> action = HandleInteractableEvent;
				bool flag8 = (object)cursorManager == null;
				num = (nint)typeof(Action<Interactable>);
				dynamicCursorManager = (DynamicCursorManager)(object)action;
				if (flag8)
				{
					break;
				}
				cursorManager.OnCursorTargetChanged -= action;
				dynamicCursorManager = cursorManager;
				Action<Interactable> value3 = HandleInteractableEvent;
				bool flag9 = (object)cursorManager == null;
				num = (nint)typeof(Action<Interactable>);
				if (flag9)
				{
					break;
				}
				Delegate obj12 = dynamicCursorManager.OnPassiveTargetChanged;
				object obj13 = cursorManager + 48;
				while (true)
				{
					Delegate obj14 = Delegate.Remove(obj12, value3);
					bool flag10 = (object)obj14 == null;
					Delegate obj15 = obj14;
					if (!flag10)
					{
						((InteractableGroupTrigger)(object)obj14).HandleInteractableEvent((Interactable)(object)typeof(Action<Interactable>));
						bool flag11 = (object)obj15 == null;
						num = (nint)typeof(Action<Interactable>);
						dynamicCursorManager = (DynamicCursorManager)(object)obj14;
						if (flag11)
						{
							break;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
					bool flag12 = (object)obj16 != obj12;
					obj12 = obj16;
					if (!flag12)
					{
						return;
					}
				}
				((InteractableGroupTrigger)(object)dynamicCursorManager).HandleInteractableEvent((Interactable)num);
				obj5 = (Delegate)(object)dynamicCursorManager;
				goto IL_036b;
			}
		}
		throw new NullReferenceException();
	}

	public void RefreshChildren()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003b: Expected O, but got I4
		//IL_0044: Expected O, but got I4
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		_children.Clear();
		Interactable[] componentsInChildren = GetComponentsInChildren<Interactable>(includeInactive: true);
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < componentsInChildren.Length)
		{
			_children.Add((Interactable)obj);
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}

	public void ResetTriggeredState()
	{
		_alreadyTriggered.Clear();
		_allChildrenEventFired = false;
	}

	private void HandleInteractableEvent(Interactable target)
	{
		if (!(target != null) || !_children.Contains(target) || !target.isInteractable || (target.isPassive && !includePassiveChildren))
		{
			return;
		}
		if (triggerOncePerChild)
		{
			if (_alreadyTriggered.Contains(target))
			{
				return;
			}
			_alreadyTriggered.Add(target);
		}
		if (OnChildInteracted != null)
		{
			OnChildInteracted.Invoke(target);
		}
		if (OnAnyChildInteracted != null)
		{
			OnAnyChildInteracted.Invoke();
		}
		if (!triggerOncePerChild || _allChildrenEventFired)
		{
			return;
		}
		HashSet<Interactable> children = _children;
		if (children._count <= 0)
		{
			return;
		}
		HashSet<Interactable> alreadyTriggered = _alreadyTriggered;
		if (alreadyTriggered._count >= children._count)
		{
			_allChildrenEventFired = true;
			if (OnAllChildrenInteracted != null)
			{
				OnAllChildrenInteracted.Invoke();
			}
		}
	}

	public InteractableGroupTrigger()
	{
		List<Interactable> list = new List<Interactable>();
		manualChildren = list;
		triggerPhase = TriggerPhase.OnRelease;
		_children = new HashSet<Interactable>();
		_alreadyTriggered = new HashSet<Interactable>();
		base._002Ector();
	}
}
