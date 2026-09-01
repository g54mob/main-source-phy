using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using TMPro;
using TwitchUnitInfo;
using UnityEngine;

public class TwitchNameBox : MonoBehaviour
{
	public enum UserFilter
	{
		All = 0,
		Subs = 1,
		VIPs = 2,
		Mods = 3
	}

	public enum GetNameMode
	{
		Looping = 0,
		Consuming = 1
	}

	public GameObject NameContainer;

	public GameObject SelectedNameContainer;

	public GameObject FloatingNamePrefab;

	[HideInInspector]
	public List<TwitchFloatingBoxName> GUINameList;

	public int SizePerChild = 75;

	public int maxNamesToDisplay = 100;

	[HideInInspector]
	public bool IsUsingAnyTwitchNameMode = true;

	private string searchFilter = "";

	private List<string> currentlyUsedNames = new List<string>();

	private TwitchUnitHandler UnitHandler;

	private GetNameMode CurrentGetNameMode = GetNameMode.Consuming;

	public List<GameObject> SelectionListObjects = new List<GameObject>();

	private bool oldPlayerActionsEnabled;

	private bool oldInputManagerEnabled;

	public TwitchMode CurrentTwitchMode { get; protected set; }

	public UserFilter CurrentUserFilter { get; protected set; }

	public void SelectText()
	{
		oldPlayerActionsEnabled = PlayerActions.Instance.Enabled;
		oldInputManagerEnabled = InputManager.Enabled;
		PlayerActions.Instance.Enabled = false;
		InputManager.Enabled = false;
	}

	public void DeselectText()
	{
		PlayerActions.Instance.Enabled = oldPlayerActionsEnabled;
		InputManager.Enabled = oldInputManagerEnabled;
	}

	private void Start()
	{
		UnitHandler = ServiceLocator.GetService<TABSTwitchHandler>().UnitHandler;
	}

	public void UpdateFloatingName(TwitchFloatingBoxName floatingName)
	{
		if (GUINameList.Contains(floatingName))
		{
			Debug.Log("GUINameList.Remove " + floatingName.TextMeshGui.text);
			GUINameList.Remove(floatingName);
			currentlyUsedNames.Add(floatingName.TextMeshGui.text);
			Debug.Log("Adding currentlyUsedNames " + floatingName.TextMeshGui.text + " (" + currentlyUsedNames.Count + ")");
		}
		else
		{
			Debug.Log("GUINameList.Add " + floatingName.TextMeshGui.text);
			GUINameList.Add(floatingName);
			currentlyUsedNames.Remove(floatingName.TextMeshGui.text);
			Debug.Log("Removing currentlyUsedNames " + floatingName.TextMeshGui.text + " (" + currentlyUsedNames.Count + ")");
		}
		FilterList();
	}

	public string GetNextSelectedName(ref ViewerTypes type, out Color color)
	{
		color = Color.white;
		if (currentlyUsedNames.Count > 0)
		{
			Transform child = SelectedNameContainer.transform.GetChild(0);
			TwitchFloatingBoxName component = child.GetComponent<TwitchFloatingBoxName>();
			string result = component.TextMeshGui.text.Replace("\u200b", "");
			color = component.Color;
			if (CurrentGetNameMode == GetNameMode.Looping)
			{
				child.SetSiblingIndex(SelectedNameContainer.transform.childCount - 1);
			}
			if (CurrentGetNameMode == GetNameMode.Consuming)
			{
				component.transform.SetParent(NameContainer.transform);
				UpdateFloatingName(component);
				FilterList();
			}
			type = component.ViewerType;
			return result;
		}
		return "";
	}

	public void FilterList()
	{
		if (CurrentTwitchMode != TwitchMode.Select)
		{
			return;
		}
		TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
		if (!service)
		{
			return;
		}
		List<ActiveChatter> list = new List<ActiveChatter>();
		List<string> list2 = new List<string>();
		switch (CurrentUserFilter)
		{
		case UserFilter.All:
			list.AddRange(service.ActiveChatters.viewers.Values);
			list.AddRange(service.ActiveChatters.subscribers.Values);
			list.AddRange(service.ActiveChatters.vips.Values);
			list2.AddRange(service.ViewerInfo.Viewers.chatters.viewers);
			list2.AddRange(service.ViewerInfo.Viewers.chatters.vips);
			break;
		case UserFilter.Subs:
			list.AddRange(service.ActiveChatters.subscribers.Values);
			list.AddRange(service.ActiveChatters.vips.Values);
			list2.AddRange(service.ViewerInfo.Viewers.chatters.viewers);
			list2.AddRange(service.ViewerInfo.Viewers.chatters.vips);
			break;
		case UserFilter.VIPs:
			list.AddRange(service.ActiveChatters.vips.Values);
			list2.AddRange(service.ViewerInfo.Viewers.chatters.vips);
			break;
		}
		list.AddRange(service.ActiveChatters.broadcaster.Values);
		list.AddRange(service.ActiveChatters.moderators.Values);
		list.AddRange(service.ActiveChatters.admins.Values);
		list.AddRange(service.ActiveChatters.staff.Values);
		list2.AddRange(service.ViewerInfo.Viewers.chatters.broadcaster);
		list2.AddRange(service.ViewerInfo.Viewers.chatters.global_mods);
		list2.AddRange(service.ViewerInfo.Viewers.chatters.moderators);
		list2.AddRange(service.ViewerInfo.Viewers.chatters.admins);
		list2.AddRange(service.ViewerInfo.Viewers.chatters.staff);
		if (UnitHandler.IncludeLurkers && service.ActiveChatters.hashes.Count < 20)
		{
			List<string> list3 = new List<string>(list.Count);
			foreach (ActiveChatter item in list)
			{
				list3.Add(item.name.ToLower());
			}
			foreach (string item2 in list2)
			{
				if (!list3.Contains(item2))
				{
					list.Add(new ActiveChatter
					{
						name = item2,
						color = Color.white
					});
				}
			}
		}
		FilterList_Internal(searchFilter, list);
	}

	public ViewerTypes CheckViewerType(string name)
	{
		TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
		if (service.ActiveChatters.subscribers.ContainsKey(name))
		{
			return ViewerTypes.subscriber;
		}
		if (service.ActiveChatters.moderators.ContainsKey(name) || service.ViewerInfo.Viewers.chatters.moderators.Contains(name))
		{
			return ViewerTypes.mod;
		}
		if (service.ActiveChatters.vips.ContainsKey(name) || service.ViewerInfo.Viewers.chatters.vips.Contains(name))
		{
			return ViewerTypes.vip;
		}
		if (service.ActiveChatters.broadcaster.ContainsKey(name) || service.ViewerInfo.Viewers.chatters.broadcaster.Contains(name))
		{
			return ViewerTypes.broadcaster;
		}
		return ViewerTypes.viewer;
	}

	public void AddNameGUI(ActiveChatter chatter)
	{
		ServiceLocator.GetService<TwitchHandler>();
		if (GUINameList.Count < maxNamesToDisplay)
		{
			GameObject obj = Object.Instantiate(FloatingNamePrefab);
			TextMeshProUGUI componentInChildren = obj.GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.text = chatter.name;
			componentInChildren.color = chatter.color;
			obj.transform.parent = NameContainer.transform;
			TwitchFloatingBoxName component = obj.GetComponent<TwitchFloatingBoxName>();
			component.Color = chatter.color;
			component.NameBoxRef = this;
			component.AvailableNames = NameContainer;
			component.SelectedNames = SelectedNameContainer;
			component.TextMeshGui = componentInChildren;
			component.ViewerType = CheckViewerType(chatter.name);
			Debug.Log("Create new gui name " + component.TextMeshGui.text);
			GUINameList.Add(component);
		}
	}

	private void FilterList_Internal(string name, List<ActiveChatter> AllNamesList)
	{
		int num = 0;
		if (name == "")
		{
			for (int i = 0; i < AllNamesList.Count; i++)
			{
				string text = AllNamesList[i].name;
				if (currentlyUsedNames.Contains(text))
				{
					continue;
				}
				string text2 = text.ToLower();
				if (currentlyUsedNames.Contains(text2))
				{
					for (int j = 0; j < currentlyUsedNames.Count; j++)
					{
						if (!(currentlyUsedNames[j] == text2))
						{
							continue;
						}
						Debug.Log("Updating currentlyUsedNames " + currentlyUsedNames[j] + " to " + text);
						currentlyUsedNames[j] = text;
						for (int k = 0; k < SelectedNameContainer.transform.childCount; k++)
						{
							TwitchFloatingBoxName component = SelectedNameContainer.transform.GetChild(i).GetComponent<TwitchFloatingBoxName>();
							if (component.TextMeshGui.text == text2)
							{
								component.TextMeshGui.text = AllNamesList[i].name;
								component.TextMeshGui.color = AllNamesList[i].color;
								component.Color = AllNamesList[i].color;
								component.ViewerType = CheckViewerType(AllNamesList[i].name);
								break;
							}
						}
						break;
					}
				}
				else if (i >= maxNamesToDisplay)
				{
					break;
				}
				if (GUINameList.Count > num)
				{
					GUINameList[num].TextMeshGui.text = AllNamesList[i].name;
					GUINameList[num].TextMeshGui.color = AllNamesList[i].color;
					GUINameList[num].Color = AllNamesList[i].color;
					GUINameList[num].ViewerType = CheckViewerType(AllNamesList[i].name);
				}
				else if (!currentlyUsedNames.Contains(AllNamesList[i].name))
				{
					AddNameGUI(AllNamesList[i]);
				}
				num++;
			}
			ClearOldGUIs(num);
			return;
		}
		int num2 = 0;
		for (int l = 0; l < AllNamesList.Count; l++)
		{
			if (!currentlyUsedNames.Contains(AllNamesList[l].name) && AllNamesList[l].name.ToLower().Contains(name.ToLower()))
			{
				if (GUINameList.Count > num)
				{
					GUINameList[num].TextMeshGui.text = AllNamesList[l].name;
					GUINameList[num].TextMeshGui.color = AllNamesList[l].color;
					GUINameList[num].Color = AllNamesList[l].color;
					GUINameList[num].ViewerType = CheckViewerType(AllNamesList[l].name);
					num2++;
				}
				else
				{
					AddNameGUI(AllNamesList[l]);
					num2++;
				}
				if (num2 >= maxNamesToDisplay)
				{
					break;
				}
				num++;
			}
		}
		ClearOldGUIs(num);
	}

	private void ClearOldGUIs(int currIndex)
	{
		if (GUINameList.Count > currIndex)
		{
			for (int num = GUINameList.Count - 1; num >= currIndex; num--)
			{
				Debug.Log("Destroy " + GUINameList[num].TextMeshGui.text);
				GameObject obj = GUINameList[num].gameObject;
				GUINameList.RemoveAt(num);
				Object.Destroy(obj);
			}
		}
	}

	public void SetMode(int mode)
	{
		CurrentTwitchMode = (TwitchMode)mode;
		if (CurrentTwitchMode == TwitchMode.Select)
		{
			foreach (GameObject selectionListObject in SelectionListObjects)
			{
				selectionListObject.SetActive(value: true);
			}
		}
		else
		{
			foreach (GameObject selectionListObject2 in SelectionListObjects)
			{
				selectionListObject2.SetActive(value: false);
			}
			ClearOldGUIs(0);
		}
		if (UnitHandler != null && UnitHandler.NameHandler != null)
		{
			UnitHandler.NameHandler.ClearRandomNameList();
		}
		FilterList();
	}

	public void SetUserFilter(int mode)
	{
		CurrentUserFilter = (UserFilter)mode;
		if (UnitHandler != null && UnitHandler.NameHandler != null && CurrentTwitchMode == TwitchMode.Random)
		{
			UnitHandler.NameHandler.ClearRandomNameList();
		}
		FilterList();
	}

	public void SetGetNameMode(bool mode)
	{
		CurrentGetNameMode = ((!mode) ? GetNameMode.Consuming : GetNameMode.Looping);
		FilterList();
	}

	public void SetSearch(string search)
	{
		searchFilter = search.Replace("\u200b", "");
		FilterList();
	}
}
