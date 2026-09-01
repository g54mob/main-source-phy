using UnityEngine;

namespace ModIO.UI
{
	[AddComponentMenu("ModIO/Inspector/Open Mod Profile In Web")]
	public class InspectorOpenModProfileInWeb : MonoBehaviour, IModViewElement
	{
		public string AppendModUrl = "/edit";

		public bool AutoLogin = true;

		private ModView m_view;

		private ModProfile m_profile;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		public void OpenModProfileInWeb()
		{
			if (m_profile != null)
			{
				string profileURL = m_profile.profileURL;
				string modUrlPrefix = GetModUrlPrefix();
				string url = profileURL + AppendModUrl + modUrlPrefix;
				Application.OpenURL(url);
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(UpdateCurrentProfile);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(UpdateCurrentProfile);
					UpdateCurrentProfile(m_view.profile);
				}
				else
				{
					UpdateCurrentProfile(null);
				}
			}
		}

		public void UpdateCurrentProfile(ModProfile modProfile)
		{
			if (modProfile != m_profile)
			{
				m_profile = modProfile;
			}
		}

		private string GetModUrlPrefix()
		{
			string text = string.Empty;
			switch (PluginSettings.USER_PORTAL)
			{
			case UserPortal.Steam:
				text = "?ref=steam";
				break;
			case UserPortal.GOG:
				text = "?ref=gog";
				break;
			case UserPortal.XboxLive:
				text = "?ref=xbox";
				break;
			}
			if (!string.IsNullOrEmpty(text) && AutoLogin)
			{
				text += "&login=auto";
			}
			return text;
		}
	}
}
