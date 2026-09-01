using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.UGUIComponentsForSettings;

public class SelectionUGUI : MonoBehaviour
{
	public SelectionEventListener[] SelectionEventListeners;

	public GameObject Selected;

	public bool IgnoreSelectionsFromMouse = true;

	protected float m_lastMouseUseTime;

	public void Start()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0065: Expected O, but got I4
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_020f: Expected O, but got I4
		//IL_00a6: Expected O, but got I
		//IL_0249: Expected O, but got I4
		//IL_02ac: Expected O, but got I4
		//IL_02c3: Expected O, but got I4
		//IL_015b: Expected O, but got I4
		//IL_02e9: Expected O, but got I4
		//IL_02f2: Expected O, but got I4
		SelectionUGUI selectionUGUI = this;
		SelectionEventListener[] selectionEventListeners = SelectionEventListeners;
		NullReferenceException ex;
		SelectionUGUI typeFromHandle;
		if (SelectionEventListeners != null)
		{
			object obj = SelectionEventListeners + 32;
			Delegate obj2 = null;
			Delegate obj3 = null;
			while (true)
			{
				if ((nint)obj3 < selectionEventListeners.Length)
				{
					UnityEngine.Object obj4 = (UnityEngine.Object)obj;
					if ((UnityEngine.Object)obj != null)
					{
						bool flag = obj == null;
						object obj5 = 0;
						selectionUGUI = (SelectionUGUI)obj;
						if (flag)
						{
							break;
						}
						SelectionEventListener.OnSelectionChangedDelegate b = onSelectionChanged;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v92 @ rsi_v7 (UnityEngine.Object)+28]");
						Delegate obj6 = Delegate.Combine((Delegate)0, b);
						object obj8;
						if ((object)obj6 == null)
						{
							_ = 0;
						}
						else
						{
							bool flag2 = (object)obj6.GetType() != typeof(SelectionEventListener.OnSelectionChangedDelegate);
							Delegate obj7 = null;
							if (!flag2)
							{
								obj7 = obj6;
							}
							bool flag3 = (object)obj7 == null;
							obj8 = 0;
							typeFromHandle = (SelectionUGUI)(object)typeof(SelectionEventListener.OnSelectionChangedDelegate);
							obj5 = 0;
							if (flag3)
							{
								goto IL_0360;
							}
							bool flag4 = (object)obj6.GetType() != typeof(SelectionEventListener.OnSelectionChangedDelegate);
							Delegate obj9 = null;
							if (!flag4)
							{
								obj9 = obj6;
							}
							bool flag5 = (object)obj9 == null;
							obj8 = 0;
							obj5 = 0;
							ex = (NullReferenceException)(object)obj6;
							selectionUGUI = (SelectionUGUI)(object)typeof(SelectionEventListener.OnSelectionChangedDelegate);
							if (flag5)
							{
								goto IL_0370;
							}
						}
						obj8 = 0;
					}
					obj2 = (Delegate)(obj2 + 1);
					obj += 8;
					obj3 = obj2;
					continue;
				}
				if (!(Selected != null))
				{
					return;
				}
				SelectionEventListener selectionEventListener = FindFirstActiveListener();
				if (selectionEventListener != null)
				{
					SelectionEventListener selectionEventListener2 = FindFirstActiveListener();
					bool flag6 = (object)selectionEventListener2 == null;
					object obj5 = 0;
					selectionUGUI = this;
					if (flag6)
					{
						break;
					}
					bool isSelected = selectionEventListener2.IsSelected;
					bool flag7 = (object)Selected == null;
					obj5 = 0;
					selectionUGUI = (SelectionUGUI)(object)selectionEventListener2;
					if (flag7)
					{
						break;
					}
					Selected.SetActive(isSelected);
				}
				return;
			}
		}
		ex = new NullReferenceException();
		goto IL_0370;
		IL_0370:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = selectionUGUI;
		goto IL_0360;
		IL_0360:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		throw new IndexOutOfRangeException();
	}

	protected SelectionEventListener FindFirstActiveListener()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001e: Expected O, but got I4
		//IL_0027: Expected O, but got I4
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		SelectionEventListener[] selectionEventListeners = SelectionEventListeners;
		object obj = SelectionEventListeners + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj3 < selectionEventListeners.Length)
			{
				if ((nint)obj2 >= selectionEventListeners.Length)
				{
					break;
				}
				if ((UnityEngine.Object)obj != null && ((Behaviour)obj).enabled)
				{
					GameObject gameObject = ((Component)obj).gameObject;
					if (gameObject.activeInHierarchy)
					{
						return (SelectionEventListener)obj;
					}
				}
				obj2++;
				obj += 8;
				obj3 = obj2;
				continue;
			}
			return null;
		}
		return (SelectionEventListener)(object)new IndexOutOfRangeException();
	}

	public void ConnectToListeners()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0078: Expected O, but got I
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_01b1: Expected I, but got O
		//IL_016d: Expected I, but got O
		SelectionEventListener[] selectionEventListeners = SelectionEventListeners;
		object obj = SelectionEventListeners + 32;
		Delegate obj2 = null;
		Delegate obj3 = null;
		while (true)
		{
			if ((nint)obj3 >= selectionEventListeners.Length)
			{
				return;
			}
			UnityEngine.Object obj4 = (UnityEngine.Object)obj;
			if ((UnityEngine.Object)obj != null)
			{
				SelectionEventListener.OnSelectionChangedDelegate b = onSelectionChanged;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rbx_v5 (UnityEngine.Object)+28]");
				Delegate obj5 = Delegate.Combine((Delegate)0, b);
				if ((object)obj5 == null)
				{
					_ = 0;
				}
				else
				{
					bool flag = (object)obj5.GetType() != typeof(SelectionEventListener.OnSelectionChangedDelegate);
					Delegate obj6 = null;
					if (!flag)
					{
						obj6 = obj5;
					}
					bool flag2 = (object)obj6 == null;
					nint num = (nint)typeof(SelectionEventListener.OnSelectionChangedDelegate);
					if (flag2)
					{
						break;
					}
					bool flag3 = (object)obj5.GetType() != typeof(SelectionEventListener.OnSelectionChangedDelegate);
					Delegate obj7 = null;
					if (!flag3)
					{
						obj7 = obj5;
					}
					if ((object)obj7 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						num = (nint)typeof(SelectionEventListener.OnSelectionChangedDelegate);
						break;
					}
				}
			}
			obj2 = (Delegate)(obj2 + 1);
			obj += 8;
			obj3 = obj2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		throw new IndexOutOfRangeException();
	}

	public void DisconnectFromListeners()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0078: Expected O, but got I
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_01b1: Expected I, but got O
		//IL_016d: Expected I, but got O
		SelectionEventListener[] selectionEventListeners = SelectionEventListeners;
		object obj = SelectionEventListeners + 32;
		Delegate obj2 = null;
		Delegate obj3 = null;
		while (true)
		{
			if ((nint)obj3 >= selectionEventListeners.Length)
			{
				return;
			}
			UnityEngine.Object obj4 = (UnityEngine.Object)obj;
			if ((UnityEngine.Object)obj != null)
			{
				SelectionEventListener.OnSelectionChangedDelegate value = onSelectionChanged;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rbx_v5 (UnityEngine.Object)+28]");
				Delegate obj5 = Delegate.Remove((Delegate)0, value);
				if ((object)obj5 == null)
				{
					_ = 0;
				}
				else
				{
					bool flag = (object)obj5.GetType() != typeof(SelectionEventListener.OnSelectionChangedDelegate);
					Delegate obj6 = null;
					if (!flag)
					{
						obj6 = obj5;
					}
					bool flag2 = (object)obj6 == null;
					nint num = (nint)typeof(SelectionEventListener.OnSelectionChangedDelegate);
					if (flag2)
					{
						break;
					}
					bool flag3 = (object)obj5.GetType() != typeof(SelectionEventListener.OnSelectionChangedDelegate);
					Delegate obj7 = null;
					if (!flag3)
					{
						obj7 = obj5;
					}
					if ((object)obj7 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						num = (nint)typeof(SelectionEventListener.OnSelectionChangedDelegate);
						break;
					}
				}
			}
			obj2 = (Delegate)(obj2 + 1);
			obj += 8;
			obj3 = obj2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		throw new IndexOutOfRangeException();
	}

	protected void onSelectionChanged(bool isSelected)
	{
		//IL_0135: Expected O, but got I4
		//IL_00fe: Invalid comparison between F4 and I4
		//IL_0127: Expected O, but got I4
		if (InputUtils.LeftMouse())
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			m_lastMouseUseTime = realtimeSinceStartup;
		}
		if (!(Selected != null))
		{
			return;
		}
		if (isSelected && IgnoreSelectionsFromMouse)
		{
			object obj;
			if (!InputUtils.LeftMouse())
			{
				float realtimeSinceStartup2 = Time.realtimeSinceStartup;
				float num = realtimeSinceStartup2 - m_lastMouseUseTime;
				bool flag = 0.3f < num;
				float num2 = 0.3f - num;
				bool flag2 = num2 == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				obj = flag4 & flag3;
			}
			else
			{
				obj = 1;
			}
			if (obj != null)
			{
				return;
			}
		}
		Selected.SetActive(isSelected);
	}

	protected bool mouseUsed(bool ignore)
	{
		//IL_0077: Invalid comparison between F4 and I4
		if (ignore)
		{
			if (!InputUtils.LeftMouse())
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				float num = realtimeSinceStartup - m_lastMouseUseTime;
				bool flag = 0.3f < num;
				float num2 = 0.3f - num;
				bool flag2 = num2 == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				return flag4 & flag3;
			}
			return true;
		}
		return false;
	}

	protected void updateLastMouseUseTime()
	{
		if (InputUtils.LeftMouse())
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			m_lastMouseUseTime = realtimeSinceStartup;
		}
	}

	protected bool mouseWasRecentlyUsed(float maxDelay = 0.3f)
	{
		//IL_005c: Invalid comparison between F4 and I4
		if (!InputUtils.LeftMouse())
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - m_lastMouseUseTime;
			bool flag = maxDelay < num;
			float num2 = maxDelay - num;
			bool flag2 = num2 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
		return true;
	}
}
