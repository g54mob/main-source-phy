using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleSelectedObjectHelper : MonoBehaviour
	{
		[Header("References")]
		public StandaloneInputModule inputModule;

		private List<Selectable> selectedObjectHistory = new List<Selectable>();

		private int maxHistorySize = 1000;

		private Selectable GetLastSelectedObject()
		{
			for (int num = selectedObjectHistory.Count - 1; num >= 0; num--)
			{
				if (selectedObjectHistory[num] != null && selectedObjectHistory[num].gameObject.activeInHierarchy)
				{
					return selectedObjectHistory[num];
				}
				selectedObjectHistory.RemoveAt(num);
			}
			return null;
		}

		private void Update()
		{
			Selectable lastSelectedObject = GetLastSelectedObject();
			Selectable selectable = ((EventSystem.current.currentSelectedGameObject != null) ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null);
			if (selectable != null && selectable.gameObject.activeInHierarchy && lastSelectedObject != EventSystem.current.currentSelectedGameObject)
			{
				if (selectedObjectHistory.Contains(selectable))
				{
					selectedObjectHistory.Remove(selectable);
				}
				selectedObjectHistory.Add(selectable);
				if (selectedObjectHistory.Count > maxHistorySize)
				{
					selectedObjectHistory.RemoveAt(0);
				}
			}
			else if (lastSelectedObject != null)
			{
				if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
				{
					lastSelectedObject.Select();
				}
				else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
				{
					lastSelectedObject.Select();
				}
				else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
				{
					lastSelectedObject.Select();
				}
				else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
				{
					lastSelectedObject.Select();
				}
			}
		}
	}
}
