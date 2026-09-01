using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PrefabPageManager : MonoBehaviour
{
	public Vector2 basePosition;

	public int buttonsPerRow = 4;

	public int numberOfRows = 7;

	public GameObject referenceButton;

	public int activePage;

	public GameObject activeCategory;

	public string[] categoryNames = new string[8] { "Animals", "Buildings", "Effects", "Environment", "Humans", "Objects", "Weaponry", "Music" };

	public GameObject[] categoryGameObjects;

	public Button[] categoryButtons;

	public Text text;

	private LevelEditor levelEditor;

	private Dictionary<LevelPrefabContainer, PrefabButton> buttonList;

	public void NextPage()
	{
		if (GetPageCount(activeCategory) - 1 > activePage)
		{
			activePage++;
			SetActivePage(activePage);
		}
	}

	public PrefabButton GetButton(LevelPrefabContainer prefabContainer)
	{
		if (!buttonList.ContainsKey(prefabContainer))
		{
			return null;
		}
		return buttonList[prefabContainer];
	}

	public void PreviousPage()
	{
		if (activePage <= 0)
		{
			activePage = 0;
			return;
		}
		activePage--;
		SetActivePage(activePage);
	}

	private void UpdateText()
	{
		int pageCount = GetPageCount(activeCategory);
		text.text = activePage + 1 + " / " + pageCount;
	}

	private void SetActivePage(int pageNum)
	{
		Transform transform = activeCategory.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject gameObject = transform.GetChild(i).gameObject;
			gameObject.SetActive(gameObject.name.Equals("page" + pageNum));
		}
		UpdateText();
		ActivateButtonsOnPage();
	}

	public void ActivateButtonsOnPage()
	{
		if (levelEditor == null)
		{
			levelEditor = LevelEditor.Instance;
		}
		if (activeCategory == null)
		{
			return;
		}
		Button[] componentsInChildren = activeCategory.GetComponentsInChildren<Button>();
		Button[] array = componentsInChildren;
		foreach (Button button in array)
		{
			PrefabButton component = button.GetComponent<PrefabButton>();
			if (!(component != null))
			{
				continue;
			}
			bool flag = levelEditor.ActiveObjectBrush != null && component.container == levelEditor.ActiveObjectBrush;
			Color color = ((!flag) ? Color.clear : button.colors.pressedColor);
			if (!(button.colors.normalColor == color))
			{
				ColorBlock colors = button.colors;
				colors.normalColor = color;
				colors.highlightedColor = color;
				button.colors = colors;
				if (flag)
				{
					PrefabButton.LastPressed = component;
				}
			}
		}
	}

	public void SetActiveCategory(int category)
	{
		for (uint num = 0u; num < categoryGameObjects.Length; num++)
		{
			if (category == num)
			{
				activeCategory = categoryGameObjects[num];
			}
			categoryGameObjects[num].SetActive(category == num);
		}
		for (int i = 0; i < categoryButtons.Length; i++)
		{
			categoryButtons[i].interactable = i != category;
		}
		activePage = 0;
		SetActivePage(activePage);
	}

	public static string GetFullPathWithoutExtension(string path)
	{
		return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)).Replace("/", "\\");
	}

	protected int FindCategory(string path)
	{
		return Array.IndexOf(categoryNames, path);
	}

	public void BuildPages(List<LevelPrefabContainer> prefabContainerList)
	{
		if (buttonList == null)
		{
			buttonList = new Dictionary<LevelPrefabContainer, PrefabButton>();
		}
		else
		{
			buttonList.Clear();
		}
		if (prefabContainerList.Count == 0)
		{
			Debug.LogError("Couldn't find any prefabs in the prefab container list!");
			return;
		}
		int[] array = new int[8];
		int[] array2 = new int[8];
		char[] separator = new char[2]
		{
			"/"[0],
			"\\"[0]
		};
		int num = 0;
		for (num = 0; num < prefabContainerList.Count; num++)
		{
			LevelPrefabContainer levelPrefabContainer = prefabContainerList[num];
			if (levelPrefabContainer.path.Length < 8)
			{
				Debug.Log("Path " + num + " invalid: " + levelPrefabContainer.path);
				continue;
			}
			Texture2D texture2D = Resources.Load("LevelEditorIcons/" + levelPrefabContainer.path.Substring(0, levelPrefabContainer.path.Length - 7)) as Texture2D;
			if (texture2D == null)
			{
				continue;
			}
			string[] array3 = levelPrefabContainer.path.Split(separator);
			int num2 = FindCategory(array3[4]);
			if (num2 >= 0)
			{
				GameObject gameObject = categoryGameObjects[num2];
				if (GetPageCount(gameObject) == 0)
				{
					CreatePageEmpty(gameObject);
				}
				if (array2[num2] >= buttonsPerRow)
				{
					array[num2]++;
					array2[num2] = 0;
				}
				GameObject gameObject2;
				if (array[num2] >= numberOfRows)
				{
					gameObject2 = CreatePageEmpty(gameObject);
					array[num2] = 0;
					array2[num2] = 0;
				}
				else
				{
					gameObject2 = gameObject.transform.Find("page" + (GetPageCount(gameObject) - 1)).gameObject;
				}
				GameObject gameObject3 = UnityEngine.Object.Instantiate(referenceButton);
				RectTransform component = gameObject3.GetComponent<RectTransform>();
				gameObject3.transform.SetParent(gameObject2.transform, false);
				float num3 = array2[num2];
				float num4 = array[num2];
				component.localScale = new Vector3(1f, 1f, 1f);
				component.anchorMin = new Vector2(num3 / (float)buttonsPerRow, 1f - (num4 + 1f) / (float)numberOfRows);
				component.anchorMax = new Vector2((num3 + 1f) / (float)buttonsPerRow, 1f - num4 / (float)numberOfRows);
				component.pivot = new Vector2(0.5f, 0.5f);
				component.sizeDelta = new Vector2(0f, 0f);
				component.anchoredPosition = new Vector2(0f, 0f);
				PrefabButton value = gameObject3.AddComponent<PrefabButton>();
				buttonList.Add(levelPrefabContainer, value);
				RawImage componentInChildren = gameObject3.GetComponentInChildren<RawImage>();
				componentInChildren.texture = texture2D;
				gameObject3.name = array3[array3.Length - 1];
				array2[num2]++;
			}
		}
		SetActiveCategory(0);
	}

	protected GameObject CreatePageEmpty(GameObject mainObject)
	{
		GameObject gameObject = new GameObject();
		int pageCount = GetPageCount(mainObject);
		gameObject.transform.parent = mainObject.transform;
		gameObject.name = "page" + pageCount;
		RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
		rectTransform.localScale = new Vector3(1f, 1f, 1f);
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.pivot = new Vector2(0f, 1f);
		rectTransform.anchoredPosition = new Vector2(0f, 0f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
		return gameObject;
	}

	protected int GetPageCount(GameObject mainObject)
	{
		return mainObject.transform.childCount;
	}
}
