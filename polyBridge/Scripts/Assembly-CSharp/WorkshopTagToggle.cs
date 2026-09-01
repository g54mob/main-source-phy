using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopTagToggle : MonoBehaviour
{
	public Toggle m_IncludeToggle;

	public Toggle m_ExcludeToggle;

	public TextMeshProUGUI m_Label;

	[NonSerialized]
	public WorkshopTagMode m_ToggleMode;

	[NonSerialized]
	public string m_LabelLocalizationKey;

	[NonSerialized]
	public WorkshopTagType m_TagType;

	[NonSerialized]
	public string m_TagName;

	private PointerEvents m_IncludeTogglePointerEvents;

	private PointerEvents m_ExcludeTogglePointerEvents;

	private void Start()
	{
		m_IncludeTogglePointerEvents = m_IncludeToggle.GetComponent<PointerEvents>();
		m_IncludeTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
		m_ExcludeTogglePointerEvents = m_ExcludeToggle.GetComponent<PointerEvents>();
		m_ExcludeTogglePointerEvents.RegisterOnClickedDelegate(InterfaceAudio.PlayToggleAudio);
	}
}
