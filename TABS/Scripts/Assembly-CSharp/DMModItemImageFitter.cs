using System;
using ModIO;
using ModIO.UI;
using UnityEngine;

public class DMModItemImageFitter : MonoBehaviour, IModViewElement
{
	[Serializable]
	public struct TypeFitSettings
	{
		public string tagName;

		public float aspectRatio;
	}

	private ModView m_view;

	private ModProfile m_profile;

	private TextureAspectRatioMatcher ratioFitter;

	public TypeFitSettings[] typeSettings;

	GameObject IModViewElement.gameObject => base.gameObject;

	protected virtual void Awake()
	{
		ratioFitter = GetComponent<TextureAspectRatioMatcher>();
	}

	protected virtual void OnEnable()
	{
		if (m_view == null)
		{
			SetModView(GetComponentInParent<ModView>());
		}
		DisplayProfile(m_profile);
	}

	public void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(DisplayProfile);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(DisplayProfile);
				DisplayProfile(m_view.profile);
			}
			else
			{
				DisplayProfile(null);
			}
		}
	}

	public void DisplayProfile(ModProfile profile)
	{
		if (profile != null)
		{
			ModTag[] tags = profile.tags;
			foreach (ModTag modTag in tags)
			{
				UpdateAspectRatio(modTag.name);
			}
		}
	}

	public void UpdateAspectRatio(string tagName)
	{
		TypeFitSettings[] array = typeSettings;
		for (int i = 0; i < array.Length; i++)
		{
			TypeFitSettings typeFitSettings = array[i];
			if (typeFitSettings.tagName == tagName)
			{
				ratioFitter.aspectRatio = typeFitSettings.aspectRatio;
				break;
			}
		}
	}
}
