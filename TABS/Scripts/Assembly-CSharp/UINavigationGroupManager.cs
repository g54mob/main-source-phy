using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UINavigationGroupManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The navigation groups in the scene.")]
	protected UINavigationGroup[] groupsInScene;

	[SerializeField]
	[Tooltip("The first group(s) to enable during startup.")]
	protected UINavigationGroup[] firstGroups;

	protected List<UINavigationGroup> navigationGroups = new List<UINavigationGroup>();

	protected virtual void Awake()
	{
		AddGroupsInScene();
		EnableFirstGroups();
	}

	public virtual void EnableGroup(UINavigationGroup groupToEnable, bool disableOthers = false, bool addToList = true)
	{
		groupToEnable.EnableNavigation();
		if (addToList)
		{
			AddGroup(groupToEnable);
		}
		if (!disableOthers || navigationGroups == null || navigationGroups.Count <= 0)
		{
			return;
		}
		int i = 0;
		for (int count = navigationGroups.Count; i < count; i++)
		{
			UINavigationGroup uINavigationGroup = navigationGroups[i];
			if (!(uINavigationGroup == null) && !(uINavigationGroup == groupToEnable) && (!groupToEnable.transform.IsChildOf(uINavigationGroup.transform) || !uINavigationGroup.KeepEnabledWhenNavigatingSubMenus))
			{
				uINavigationGroup.DisableNavigation();
			}
		}
	}

	public virtual void DisableGroup(UINavigationGroup groupToDisable, bool addToList = true)
	{
		groupToDisable.DisableNavigation();
		if (addToList)
		{
			AddGroup(groupToDisable);
		}
	}

	public virtual void DisableAllGroups()
	{
		if (navigationGroups == null || navigationGroups.Count <= 0)
		{
			return;
		}
		int i = 0;
		for (int count = navigationGroups.Count; i < count; i++)
		{
			UINavigationGroup uINavigationGroup = navigationGroups[i];
			if (uINavigationGroup != null)
			{
				uINavigationGroup.enabled = false;
			}
		}
	}

	public void SetAutoSelectInAllGroups(bool autoselect)
	{
		if (navigationGroups == null || navigationGroups.Count <= 0)
		{
			return;
		}
		int i = 0;
		for (int count = navigationGroups.Count; i < count; i++)
		{
			UINavigationGroup uINavigationGroup = navigationGroups[i];
			if (uINavigationGroup != null)
			{
				uINavigationGroup.SetAutoSelect(autoselect);
			}
		}
		if (autoselect && EventSystem.current != null)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public virtual void AddGroup(UINavigationGroup groupToAdd)
	{
		if (!(groupToAdd == null) && navigationGroups != null && !navigationGroups.Contains(groupToAdd))
		{
			navigationGroups.Add(groupToAdd);
		}
	}

	public virtual void RemoveGroup(UINavigationGroup groupToRemove)
	{
		if (!(groupToRemove == null) && navigationGroups != null && navigationGroups.Contains(groupToRemove))
		{
			navigationGroups.Remove(groupToRemove);
		}
	}

	protected virtual void EnableFirstGroups()
	{
		if (firstGroups != null && firstGroups.Length != 0)
		{
			DisableAllGroups();
			int i = 0;
			for (int num = firstGroups.Length; i < num; i++)
			{
				EnableGroup(firstGroups[i]);
			}
		}
	}

	protected virtual void AddGroupsInScene()
	{
		if (groupsInScene == null || groupsInScene.Length == 0)
		{
			return;
		}
		int i = 0;
		for (int num = groupsInScene.Length; i < num; i++)
		{
			UINavigationGroup uINavigationGroup = groupsInScene[i];
			if (!(uINavigationGroup == null))
			{
				navigationGroups.Add(uINavigationGroup);
			}
		}
	}
}
