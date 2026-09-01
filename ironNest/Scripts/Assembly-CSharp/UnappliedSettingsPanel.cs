using System.Collections.Generic;
using Kamgam.SettingsGenerator;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UnappliedSettingsPanel : MonoBehaviour
{
	[SerializeField]
	private TMP_Text _unappliedSettingsText;

	[SerializeField]
	private SettingsLocalizationKeys _settingsLocalizationKeys;

	[SerializeField]
	private UnityEvent _onHide;

	[SerializeField]
	private UnityEvent _onShow;

	public void Show(List<ISetting> unappliedSettings)
	{
	}

	public void Hide()
	{
	}

	private void SetUnappliedSettings(List<ISetting> unappliedSettings)
	{
	}
}
