using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using TMPro;
using UnityEngine;

public class UnitEditorMenuButton : MonoBehaviour
{
	public delegate List<CharacterItem> GetButtonsCallback();

	public Action m_onClick;

	public GetButtonsCallback[] m_getButtonsCallbackSmallE;

	public Action m_clickItemCallback;

	public string[] buttonsNames;

	private UnitEditorScrollMenu scrollMenu;

	public GameObject[] spawnedButtons;

	public bool disabled;

	private bool isRemovingButtons;

	private void Start()
	{
		scrollMenu = GetComponentInParent<UnitEditorScrollMenu>();
	}

	public void Click()
	{
		if (disabled)
		{
			return;
		}
		m_onClick?.Invoke();
		StopAllCoroutines();
		if (isRemovingButtons)
		{
			for (int i = 0; i < spawnedButtons.Length; i++)
			{
				if ((bool)spawnedButtons[i])
				{
					UnitEditorMenuButton component = spawnedButtons[i].GetComponent<UnitEditorMenuButton>();
					if ((bool)component)
					{
						component.StopAllCoroutines();
						KillAllChildren(component);
					}
					UnityEngine.Object.Destroy(spawnedButtons[i]);
				}
			}
			spawnedButtons = null;
			isRemovingButtons = false;
		}
		StopAllCoroutines();
		if (spawnedButtons != null && spawnedButtons.Length != 0)
		{
			for (int j = 0; j < spawnedButtons.Length; j++)
			{
				if ((bool)spawnedButtons[j])
				{
					UnitEditorMenuButton component2 = spawnedButtons[j].GetComponent<UnitEditorMenuButton>();
					if ((bool)component2)
					{
						component2.StopAllCoroutines();
						KillAllChildren(component2);
					}
				}
			}
			StartCoroutine(RemoveButtonsOverTime());
		}
		else
		{
			string[] array = buttonsNames;
			spawnedButtons = new GameObject[array.Length];
			StartCoroutine(SpawnButtonsOverTime(array));
		}
	}

	private void KillAllChildren(UnitEditorMenuButton editorButton)
	{
		if ((bool)editorButton && editorButton.spawnedButtons != null && editorButton.spawnedButtons.Length != 0)
		{
			for (int i = 0; i < editorButton.spawnedButtons.Length; i++)
			{
				UnityEngine.Object.Destroy(editorButton.spawnedButtons[i]);
			}
		}
	}

	public IEnumerator SpawnButtonsOverTime(string[] names)
	{
		for (int i = 0; i < names.Length; i++)
		{
			if (!isRemovingButtons)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(scrollMenu.subCategory, base.transform.position, base.transform.rotation, base.transform.parent);
				if (m_getButtonsCallbackSmallE != null)
				{
					UnitEditorSubCategoryButton component = gameObject.GetComponent<UnitEditorSubCategoryButton>();
					if ((bool)component)
					{
						component.m_getButtonsCallbackE = m_getButtonsCallbackSmallE[i];
					}
				}
				gameObject.transform.localScale = Vector3.one;
				spawnedButtons[i] = gameObject;
				gameObject.GetComponentInChildren<TextMeshProUGUI>().text = names[i];
				int siblingIndex = base.transform.GetSiblingIndex() + 1 + i;
				gameObject.transform.SetSiblingIndex(siblingIndex);
			}
			yield return new WaitForSeconds(0.025f);
		}
	}

	public IEnumerator RemoveButtonsOverTime()
	{
		isRemovingButtons = true;
		float t = scrollMenu.removeButtonsCurve.keys[scrollMenu.removeButtonsCurve.keys.Length - 1].time;
		float c = 0f;
		while (c < t)
		{
			c += Time.deltaTime;
			for (int i = 0; i < spawnedButtons.Length; i++)
			{
				if (spawnedButtons[i] != null)
				{
					UnitEditorMenuButton component = spawnedButtons[i].GetComponent<UnitEditorMenuButton>();
					if ((bool)component)
					{
						component.disabled = true;
					}
					if (scrollMenu.removeButtonsCurve.Evaluate(c) > (float)i / (float)(spawnedButtons.Length - 1))
					{
						StartCoroutine(ScaleOutButton(spawnedButtons[i]));
					}
				}
			}
			yield return null;
		}
		StartCoroutine(ScaleOutButton(spawnedButtons[spawnedButtons.Length - 1]));
		spawnedButtons = null;
		isRemovingButtons = false;
	}

	private IEnumerator ScaleOutButton(GameObject go)
	{
		if (go.name == "IS BEING REMOVED")
		{
			yield break;
		}
		go.AddComponent<RemoveAfterSeconds>().seconds = 0.2f;
		go.name = "IS BEING REMOVED";
		float c = 0f;
		RectTransform rect = go.transform as RectTransform;
		float height = rect.sizeDelta.y;
		if (go != null)
		{
			while ((bool)rect && rect.sizeDelta.y > -10f)
			{
				rect.sizeDelta = new Vector2(rect.sizeDelta.x, (1f - c) * height);
				c += Time.deltaTime * 20f;
				yield return null;
			}
			UnityEngine.Object.Destroy(go);
		}
	}
}
