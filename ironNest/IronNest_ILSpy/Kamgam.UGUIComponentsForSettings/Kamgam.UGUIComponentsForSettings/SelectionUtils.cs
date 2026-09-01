using UnityEngine;
using UnityEngine.EventSystems;

namespace Kamgam.UGUIComponentsForSettings;

public static class SelectionUtils
{
	public static void SetSelected(GameObject go, bool triggerOnReselect = true)
	{
		//IL_00f4: Expected O, but got I4
		EventSystem current = EventSystem.current;
		if (!(current != null) || !(go != null))
		{
			return;
		}
		EventSystem current2 = EventSystem.current;
		if (!current2.m_SelectionGuard)
		{
			EventSystem current3 = EventSystem.current;
			bool flag = current3.m_CurrentSelected == go;
			EventSystem current4 = EventSystem.current;
			current4.SetSelectedGameObject(go);
			object obj = triggerOnReselect & flag;
			if (obj != null)
			{
				EventSystem current5 = EventSystem.current;
				BaseEventData eventData = new BaseEventData(current5);
				GameObject gameObject = ExecuteEvents.ExecuteHierarchy(go, eventData, ExecuteEvents.s_SelectHandler);
			}
		}
	}
}
