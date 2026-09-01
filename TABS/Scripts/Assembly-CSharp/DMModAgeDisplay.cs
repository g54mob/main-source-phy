using System;
using ModIO;
using ModIO.UI;
using UnityEngine;

public class DMModAgeDisplay : MonoBehaviour, IModViewElement
{
	private LocalizeText m_localizeText;

	private ModView m_view;

	private ModProfile m_profile;

	GameObject IModViewElement.gameObject => base.gameObject;

	protected virtual void Awake()
	{
		m_localizeText = GetComponent<LocalizeText>();
	}

	protected virtual void OnEnable()
	{
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
		m_profile = profile;
		if (m_profile != null)
		{
			DateTime dateTime = ServerTimeStamp.ToLocalDateTime(m_profile.dateAdded);
			int days = (ServerTimeStamp.ToLocalDateTime(ServerTimeStamp.Now) - dateTime).Days;
			int num = days / 365;
			int num2 = days / 31;
			int num3 = days / 7;
			if (num > 0)
			{
				m_localizeText.Args = new string[1] { num.ToString() };
				m_localizeText.LocaleID = ((num > 1) ? "LABEL_MODAGE_YEARS" : "LABEL_MODAGE_YEAR");
			}
			else if (num2 > 0)
			{
				m_localizeText.Args = new string[1] { num2.ToString() };
				m_localizeText.LocaleID = ((num2 > 1) ? "LABEL_MODAGE_MONTHS" : "LABEL_MODAGE_MONTH");
			}
			else if (num3 > 0)
			{
				m_localizeText.Args = new string[1] { num3.ToString() };
				m_localizeText.LocaleID = ((num3 > 1) ? "LABEL_MODAGE_WEEKS" : "LABEL_MODAGE_WEEK");
			}
			else if (days > 0)
			{
				m_localizeText.Args = new string[1] { days.ToString() };
				m_localizeText.LocaleID = ((days > 1 || days == 0) ? "LABEL_MODAGE_DAYS" : "LABEL_MODAGE_DAY");
			}
			else
			{
				m_localizeText.LocaleID = "LABEL_MODAGE_NEW";
			}
		}
	}
}
