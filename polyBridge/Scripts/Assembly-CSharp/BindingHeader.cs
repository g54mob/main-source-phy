using System;
using TMPro;
using UnityEngine;

public class BindingHeader : MonoBehaviour
{
	public TextMeshProUGUI m_Mode;

	public TextMeshProUGUI m_BindingText;

	public TextMeshProUGUI m_AltBindingText;

	[NonSerialized]
	public string m_ModeLocId;

	private void OnEnable()
	{
		m_Mode.text = Localize.Get(m_ModeLocId);
	}
}
