using System;
using UnityEngine;

public class PolyTwitchSuggestionsParent : MonoBehaviour
{
	[NonSerialized]
	public string m_LayoutHash;

	public void RemoveSuggestions()
	{
		foreach (Transform item in base.transform)
		{
			PolyTwitchSuggestionSlot component = item.GetComponent<PolyTwitchSuggestionSlot>();
			if ((bool)component)
			{
				PolyTwitchSuggestions.m_Suggestions.Remove(component.m_Suggestion);
			}
		}
		PolyTwitchSuggestions.SortAutoplayList();
	}
}
