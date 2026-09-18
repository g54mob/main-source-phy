using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class HighlightManager : SceneSingleton<HighlightManager>
{
	[SerializeField]
	private float potionHighlightTime = 30f;

	[SerializeField]
	private float puzzleHighlightTime = 40f;

	[Space]
	[SerializeField]
	private List<SortingPuzzleHighlightData> highlightedSortingPuzzleHighlightDatasList = new List<SortingPuzzleHighlightData>();

	private List<IHighlight> highlightedIHighlightsList = new List<IHighlight>();

	private List<Outline> highlightedOutline = new List<Outline>();

	public bool CanHighlightPuzzle(SortingPuzzleType sortingPuzzleType)
	{
		foreach (SortingPuzzleHighlightData highlightedSortingPuzzleHighlightDatas in highlightedSortingPuzzleHighlightDatasList)
		{
			if (highlightedSortingPuzzleHighlightDatas.sortingPuzzleType == sortingPuzzleType)
			{
				return true;
			}
		}
		return false;
	}

	[Button]
	public void HighlightAllPuzzle()
	{
		DisableHighlights();
		foreach (SortingPuzzleHighlightData highlightedSortingPuzzleHighlightDatas in highlightedSortingPuzzleHighlightDatasList)
		{
			foreach (GameObject highlightableGameObjects in highlightedSortingPuzzleHighlightDatas.highlightableGameObjectsList)
			{
				if (highlightableGameObjects.activeSelf)
				{
					if (highlightableGameObjects.TryGetComponent<IHighlight>(out var component))
					{
						highlightedIHighlightsList.Add(component);
					}
					else
					{
						highlightedOutline.Add(highlightableGameObjects.GetComponent<Outline>());
					}
				}
			}
		}
		EnableHighlight(puzzleHighlightTime);
	}

	[Button]
	public void HighlightPuzzle(SortingPuzzleType sortingPuzzleType)
	{
		DisableHighlights();
		SortingPuzzleHighlightData sortingPuzzleHighlightData = new SortingPuzzleHighlightData();
		foreach (SortingPuzzleHighlightData highlightedSortingPuzzleHighlightDatas in highlightedSortingPuzzleHighlightDatasList)
		{
			if (highlightedSortingPuzzleHighlightDatas.sortingPuzzleType == sortingPuzzleType)
			{
				sortingPuzzleHighlightData = highlightedSortingPuzzleHighlightDatas;
				break;
			}
		}
		foreach (GameObject highlightableGameObjects in sortingPuzzleHighlightData.highlightableGameObjectsList)
		{
			if (highlightableGameObjects.activeSelf)
			{
				if (highlightableGameObjects.TryGetComponent<IHighlight>(out var component))
				{
					highlightedIHighlightsList.Add(component);
				}
				else
				{
					highlightedOutline.Add(highlightableGameObjects.GetComponent<Outline>());
				}
			}
		}
		EnableHighlight(puzzleHighlightTime);
	}

	public void HighlightPotions(PotionController potionController)
	{
		DisableHighlights();
		foreach (PotionController item in SceneSingleton<PotionsManager>.Instance.GetAllPotionsInSet(potionController))
		{
			highlightedIHighlightsList.Add(item.GetComponent<IHighlight>());
		}
		EnableHighlight(potionHighlightTime);
	}

	public void EnableHighlight(float time)
	{
		foreach (IHighlight highlightedIHighlights in highlightedIHighlightsList)
		{
			highlightedIHighlights.SetHintHighlight(flag: true);
		}
		foreach (Outline item in highlightedOutline)
		{
			item.enabled = true;
		}
		StopAllCoroutines();
		StartCoroutine(DisableHighlightsCoroutine(time));
	}

	private IEnumerator DisableHighlightsCoroutine(float time)
	{
		yield return new WaitForSeconds(time);
		DisableHighlights();
	}

	public void DisableHighlights()
	{
		foreach (IHighlight highlightedIHighlights in highlightedIHighlightsList)
		{
			highlightedIHighlights.SetHintHighlight(flag: false);
		}
		foreach (Outline item in highlightedOutline)
		{
			item.enabled = false;
		}
		highlightedIHighlightsList.Clear();
		highlightedOutline.Clear();
	}

	private void OnValidate()
	{
		foreach (SortingPuzzleHighlightData highlightedSortingPuzzleHighlightDatas in highlightedSortingPuzzleHighlightDatasList)
		{
			highlightedSortingPuzzleHighlightDatas.OnValidate();
		}
	}
}
