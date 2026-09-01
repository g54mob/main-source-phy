using System;
using UnityEngine;

[Serializable]
public struct CampaignInfo
{
	public int ModID;

	public string m_description;

	public string m_thankYouTitle;

	[TextArea]
	public string m_thankYouText;

	public string Description
	{
		get
		{
			return m_description;
		}
		set
		{
			m_description = value;
		}
	}

	public string ThankYouTitle
	{
		get
		{
			return m_thankYouTitle;
		}
		set
		{
			m_thankYouTitle = value;
		}
	}

	public string ThankYouText
	{
		get
		{
			return m_thankYouText;
		}
		set
		{
			m_thankYouText = value;
		}
	}
}
