using System.Collections.Generic;
using System.Linq;
using ModIO;
using ModIO.UI;
using UnityEngine;

public class DMEnableIfContainsTag : MonoBehaviour, IModViewElement
{
	[SerializeField]
	private TagMatch[] tagMatches;

	private ModView m_view;

	private string[] m_tags = new string[0];

	GameObject IModViewElement.gameObject => base.gameObject;

	protected virtual void OnEnable()
	{
		if (m_view == null)
		{
			SetModView(GetComponentInParent<ModView>());
		}
		UpdateObjects(m_tags);
	}

	public void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(DisplayProfileTags);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(DisplayProfileTags);
				DisplayProfileTags(m_view.profile);
			}
			else
			{
				DisplayProfileTags(null);
			}
		}
	}

	public void DisplayProfileTags(ModProfile profile)
	{
		IEnumerable<string> tags = null;
		if (profile != null)
		{
			tags = profile.tagNames;
		}
		UpdateObjects(tags);
	}

	private void UpdateObjects(IEnumerable<string> tags)
	{
		if (tags == null || tags.Count() == 0 || !base.gameObject.activeSelf)
		{
			return;
		}
		TagMatch[] array = tagMatches;
		for (int i = 0; i < array.Length; i++)
		{
			TagMatch tagMatch = array[i];
			bool active = tags.Contains(tagMatch.tag);
			if (tagMatch.objectsToEnable == null)
			{
				continue;
			}
			for (int j = 0; j < tagMatch.objectsToEnable.Length; j++)
			{
				GameObject gameObject = tagMatch.objectsToEnable[j];
				if (gameObject != null)
				{
					gameObject.SetActive(active);
				}
			}
		}
	}
}
