using TMPro;
using UnityEngine;

public class DMLocalizer : MonoBehaviour
{
	private LocalizeText m_localizeText;

	private CodeAnimation m_codeAnimation;

	private void GetCodeAnimationInParentRecursive(Transform t)
	{
		m_codeAnimation = t.GetComponent<CodeAnimation>();
		if (m_codeAnimation == null && t.parent != null)
		{
			GetCodeAnimationInParentRecursive(t.parent);
		}
	}

	private void Start()
	{
		GetCodeAnimationInParentRecursive(base.transform);
		if (m_codeAnimation != null)
		{
			m_codeAnimation.InPlayed += UpdateLocalizer;
		}
		UpdateLocalizer();
	}

	private void OnDestroy()
	{
		if (m_codeAnimation != null)
		{
			m_codeAnimation.InPlayed -= UpdateLocalizer;
		}
	}

	private void OnEnable()
	{
		UpdateLocalizer();
	}

	public void UpdateLocalizer()
	{
		if (m_localizeText == null)
		{
			m_localizeText = GetComponent<LocalizeText>();
		}
		if (GetComponentInChildren<TMP_Text>() == null)
		{
			Debug.LogError("DMLocalizer had no child TMP_Text component");
			return;
		}
		switch (GetComponentInChildren<TMP_Text>().text.ToLower())
		{
		case "challenging":
			m_localizeText.LocaleID = "LABEL_CHALLENGING";
			break;
		case "historical":
			m_localizeText.LocaleID = "LABEL_HISTORICAL";
			break;
		case "humor":
			m_localizeText.LocaleID = "LABEL_HUMOR";
			break;
		case "pop culture":
			m_localizeText.LocaleID = "LABEL_POPCULTURE";
			break;
		case "other":
			m_localizeText.LocaleID = "LABEL_OTHER";
			break;
		case "powerful":
			m_localizeText.LocaleID = "LABEL_POWERFUL";
			break;
		case "all tags":
			m_localizeText.LocaleID = "LABEL_ALLTAGS";
			break;
		case "battle":
			m_localizeText.LocaleID = "LABEL_BATTLE";
			break;
		case "campaign":
			m_localizeText.LocaleID = "LABEL_CAMPAIGN";
			break;
		case "unit":
			m_localizeText.LocaleID = "LABEL_UNIT";
			break;
		case "faction":
			m_localizeText.LocaleID = "LABEL_FACTION";
			break;
		case "browse":
			m_localizeText.LocaleID = "BUTTON_BROWSE";
			break;
		case "subcriptions":
			m_localizeText.LocaleID = "BUTTON_SUBSCRIPTIONS";
			break;
		case "my creations":
			m_localizeText.LocaleID = "BUTTON_MYCREATIONS";
			break;
		case "newest":
			m_localizeText.LocaleID = "LABEL_NEWEST";
			break;
		case "popularity":
			m_localizeText.LocaleID = "LABEL_POPULARITY";
			break;
		case "subscribers":
			m_localizeText.LocaleID = "LABEL_SUBSCRIBERS";
			break;
		case "rating":
			m_localizeText.LocaleID = "LABEL_RATING";
			break;
		case "all time":
			m_localizeText.LocaleID = "LABEL_ALLTIME";
			break;
		case "this month":
			m_localizeText.LocaleID = "LABEL_THISMONTH";
			break;
		case "this week":
			m_localizeText.LocaleID = "LABEL_THISWEEK";
			break;
		case "today":
			m_localizeText.LocaleID = "LABEL_TODAY";
			break;
		case "- select report type -":
			m_localizeText.LocaleID = "BUTTON_REPORTTYPE";
			break;
		case "dmca":
			m_localizeText.LocaleID = "BUTTON_DMCA";
			break;
		case "not working":
			m_localizeText.LocaleID = "BUTTON_NOTWORKING";
			break;
		case "rude content":
			m_localizeText.LocaleID = "BUTTON_RUDECONTENT";
			break;
		case "illegal content":
			m_localizeText.LocaleID = "BUTTON_ILLEGALCONTENT";
			break;
		case "stolen content":
			m_localizeText.LocaleID = "BUTTON_STOLENCONTENT";
			break;
		case "false information":
			m_localizeText.LocaleID = "BUTTON_FALSEINFORMATION";
			break;
		case "all":
			m_localizeText.LocaleID = "LABEL_ALL";
			break;
		case "subs":
			m_localizeText.LocaleID = "LABEL_SUBS";
			break;
		case "vips":
			m_localizeText.LocaleID = "LABEL_VIPS";
			break;
		case "mods":
			m_localizeText.LocaleID = "LABEL_MODS";
			break;
		case "off":
			m_localizeText.LocaleID = "LABEL_OFF";
			break;
		case "random":
			m_localizeText.LocaleID = "LABEL_RANDOM";
			break;
		case "select":
			m_localizeText.LocaleID = "LABEL_SELECT";
			break;
		}
	}
}
