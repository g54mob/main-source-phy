using System;
using UnityEngine;

[AddComponentMenu("UI/UI Tab Toggler")]
public class UITabToggler : MonoBehaviour
{
	[Serializable]
	public class Tab
	{
		public SimpleUIButton button;

		public MonoBehaviour activeComponent;

		public MonoBehaviour inactiveComponent;
	}

	public Action<int> OnTabChanged;

	public Tab[] tabs;

	private int currentTab;

	private void Awake()
	{
		for (int i = 0; i < tabs.Length; i++)
		{
			int j = i;
			tabs[i].button.Click += delegate
			{
				SetTab(j);
			};
		}
	}

	public void SetTab(int index)
	{
		if (currentTab != index)
		{
			currentTab = index;
			for (int i = 0; i < tabs.Length; i++)
			{
				bool flag = ((i == index) ? true : false);
				tabs[i].activeComponent.enabled = flag;
				tabs[i].inactiveComponent.enabled = !flag;
			}
			if (OnTabChanged != null)
			{
				OnTabChanged(index);
			}
		}
	}
}
