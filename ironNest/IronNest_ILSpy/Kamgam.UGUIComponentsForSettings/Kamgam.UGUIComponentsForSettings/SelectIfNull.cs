using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class SelectIfNull : MonoBehaviour
{
	public enum Trigger
	{
		Awake,
		OnEnable,
		Start,
		Update,
		LateUpdate,
		OnDirectionInput
	}

	public Trigger[] Triggers;

	public Selectable[] Candidates;

	public bool SearchForSelectables = true;

	public bool TreatDisabledObjectsAsNull;

	public void Awake()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005f: Expected O, but got I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		if (Triggers == null)
		{
			return;
		}
		Trigger[] triggers = Triggers;
		if (triggers.Length == 0)
		{
			return;
		}
		object obj = triggers + 32;
		object obj2 = 0;
		while ((nint)obj2 < triggers.Length)
		{
			if (obj != null)
			{
				obj2++;
				obj += 4;
				continue;
			}
			selectIfNull();
			break;
		}
	}

	public void Start()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005f: Expected O, but got I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		if (Triggers == null)
		{
			return;
		}
		Trigger[] triggers = Triggers;
		if (triggers.Length == 0)
		{
			return;
		}
		object obj = triggers + 32;
		object obj2 = 0;
		while ((nint)obj2 < triggers.Length)
		{
			if ((nint)obj != 2)
			{
				obj2++;
				obj += 4;
				continue;
			}
			selectIfNull();
			break;
		}
	}

	public void OnEnable()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005f: Expected O, but got I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		if (Triggers == null)
		{
			return;
		}
		Trigger[] triggers = Triggers;
		if (triggers.Length == 0)
		{
			return;
		}
		object obj = triggers + 32;
		object obj2 = 0;
		while ((nint)obj2 < triggers.Length)
		{
			if ((nint)obj != 1)
			{
				obj2++;
				obj += 4;
				continue;
			}
			selectIfNull();
			break;
		}
	}

	public void Update()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_0045: Expected O, but got I4
		//IL_004e: Expected O, but got I4
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_0103: Expected O, but got I4
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		if (Triggers == null)
		{
			return;
		}
		Trigger[] triggers = Triggers;
		if (triggers.Length != 0)
		{
			object obj = triggers + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj2 < triggers.Length)
			{
				if ((nint)obj != 3)
				{
					obj3++;
					obj += 4;
					obj2 = obj3;
					continue;
				}
				selectIfNull();
				break;
			}
		}
		if (Triggers == null)
		{
			return;
		}
		Trigger[] triggers2 = Triggers;
		if (triggers2.Length == 0)
		{
			return;
		}
		object obj4 = triggers2 + 32;
		object obj5 = 0;
		while ((nint)obj5 < triggers2.Length)
		{
			if ((nint)obj4 != 5)
			{
				obj5++;
				obj4 += 4;
				continue;
			}
			if (InputUtils.AnyDirection())
			{
				selectIfNull();
			}
			break;
		}
	}

	public void LateUpdate()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005f: Expected O, but got I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		if (Triggers == null)
		{
			return;
		}
		Trigger[] triggers = Triggers;
		if (triggers.Length == 0)
		{
			return;
		}
		object obj = triggers + 32;
		object obj2 = 0;
		while ((nint)obj2 < triggers.Length)
		{
			if ((nint)obj != 4)
			{
				obj2++;
				obj += 4;
				continue;
			}
			selectIfNull();
			break;
		}
	}

	private bool containsTrigger(Trigger trigger)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_005f: Expected O, but got I4
		//IL_00dc: Expected I4, but got O
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		if (Triggers != null)
		{
			Trigger[] triggers = Triggers;
			if (triggers.Length != 0)
			{
				object obj = triggers + 32;
				object obj2 = 0;
				while ((nint)obj2 < triggers.Length)
				{
					if ((nint)obj2 < triggers.Length)
					{
						if ((nint)obj != (nint)trigger)
						{
							obj2++;
							obj += 4;
							continue;
						}
						return true;
					}
					IndexOutOfRangeException ex = new IndexOutOfRangeException();
					return (byte)(int)ex != 0;
				}
			}
		}
		return false;
	}

	private void selectIfNull()
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		EventSystem current = EventSystem.current;
		if (current != null)
		{
			EventSystem current2 = EventSystem.current;
			if (!(current2.m_CurrentSelected != null))
			{
				goto IL_00c5;
			}
		}
		if (TreatDisabledObjectsAsNull)
		{
			EventSystem current3 = EventSystem.current;
			if (!current3.m_CurrentSelected.activeInHierarchy)
			{
				goto IL_00c5;
			}
			return;
		}
		return;
		IL_00c5:
		Selectable[] candidates = Candidates;
		object obj = Candidates + 32;
		UnityEngine.Object obj2 = null;
		UnityEngine.Object obj3 = null;
		UnityEngine.Object obj4;
		while (true)
		{
			bool flag = (nint)obj3 >= candidates.Length;
			obj4 = null;
			if (flag)
			{
				break;
			}
			Behaviour behaviour = (Behaviour)obj;
			if (((Behaviour)obj).enabled)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rbx_v13 (UnityEngine.Behaviour)+D8]");
				if ((nint)0 != 0)
				{
					GameObject gameObject = ((Component)obj).gameObject;
					if (gameObject.activeInHierarchy)
					{
						obj4 = (UnityEngine.Object)obj;
						break;
					}
				}
			}
			obj2 = (UnityEngine.Object)(obj2 + 1);
			obj += 8;
			obj3 = obj2;
		}
		if (SearchForSelectables && obj4 == null)
		{
			Selectable[] allSelectablesArray = Selectable.allSelectablesArray;
			object obj5 = allSelectablesArray + 32;
			UnityEngine.Object obj6 = null;
			UnityEngine.Object obj7 = null;
			while ((nint)obj7 < allSelectablesArray.Length)
			{
				Behaviour behaviour2 = (Behaviour)obj5;
				if (((Behaviour)obj5).isActiveAndEnabled)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ rbx_v12 (UnityEngine.Behaviour)+D8]");
					if ((nint)0 != 0)
					{
						GameObject gameObject2 = ((Component)obj5).gameObject;
						if (gameObject2.activeInHierarchy)
						{
							obj4 = (UnityEngine.Object)obj5;
							break;
						}
					}
				}
				obj6 = (UnityEngine.Object)(obj6 + 1);
				obj5 += 8;
				obj7 = obj6;
			}
		}
		if (obj4 != null)
		{
			EventSystem current4 = EventSystem.current;
			GameObject selectedGameObject = ((Component)obj4).gameObject;
			current4.SetSelectedGameObject(selectedGameObject);
		}
	}
}
