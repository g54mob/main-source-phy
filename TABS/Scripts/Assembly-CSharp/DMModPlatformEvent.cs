using System.Collections.Generic;
using ModIO;
using ModIO.UI;
using UnityEngine;

public class DMModPlatformEvent : MonoBehaviour, IModViewElement
{
	[SerializeField]
	private PlatformEvent[] platformEvents;

	private ModView m_view;

	private ModProfile m_profile;

	GameObject IModViewElement.gameObject => base.gameObject;

	protected virtual void OnEnable()
	{
		if (m_view == null)
		{
			SetModView(GetComponentInParent<ModView>());
		}
		if (m_profile != null)
		{
			InvokeEvents(m_profile.tagNames);
		}
	}

	public void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(UpdateProfile);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(UpdateProfile);
				UpdateProfile(m_view.profile);
			}
			else
			{
				UpdateProfile(null);
			}
		}
	}

	public void UpdateProfile(ModProfile profile)
	{
		m_profile = profile;
		if (m_profile != null)
		{
			InvokeEvents(profile.tagNames);
		}
	}

	private void InvokeEvents(IEnumerable<string> tags)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
		PlatformEvent[] array = platformEvents;
		foreach (PlatformEvent platformEvent in array)
		{
			SettingsInstance.Platform[] platforms = platformEvent.platforms;
			for (int j = 0; j < platforms.Length; j++)
			{
				if (platforms[j] != currentPlatform)
				{
					continue;
				}
				foreach (string tag in tags)
				{
					switch (tag)
					{
					case "PC":
						platformEvent.createdOnDesktop.Invoke();
						return;
					case "XBOX":
						platformEvent.createdOnXbox.Invoke();
						return;
					case "SWITCH":
						platformEvent.createdOnSwitch.Invoke();
						return;
					case "PLAYSTATION":
					case "PLAYSTATION_4":
					case "PLAYSTATION_5":
						platformEvent.createdOnPlaystation.Invoke();
						return;
					}
				}
				platformEvent.createdOnDesktop.Invoke();
			}
		}
	}
}
