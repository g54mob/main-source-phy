using System;
using UnityEngine;

[Obsolete]
public class RadioUserPreferenceControllerBehaviour : MonoBehaviour
{
	public string Key;

	private RadioButtonBehaviour[] Buttons;

	private void Start()
	{
		Buttons = GetComponentsInChildren<RadioButtonBehaviour>();
		for (int i = 0; i < Buttons.Length; i++)
		{
			Buttons[i].OnClick += ButtonClicked;
		}
		SetChecked(UserPreferenceManager.Current.GetByName(Key));
	}

	private void ButtonClicked(object sender, EventArgs e)
	{
		object value = (sender as RadioButtonBehaviour).GetValue();
		UserPreferenceManager.Current.SetByName(Key, value);
		SetChecked(value);
		GameSettingsInitBehaviour gameSettingsInitBehaviour = UnityEngine.Object.FindObjectOfType<GameSettingsInitBehaviour>();
		VideoSettingsInitBehaviour videoSettingsInitBehaviour = UnityEngine.Object.FindObjectOfType<VideoSettingsInitBehaviour>();
		if ((bool)gameSettingsInitBehaviour)
		{
			gameSettingsInitBehaviour.Sync();
		}
		if ((bool)videoSettingsInitBehaviour)
		{
			videoSettingsInitBehaviour.Sync();
		}
		UserPreferenceManager.Save();
	}

	public void SetChecked(object value)
	{
		if (Buttons == null)
		{
			return;
		}
		for (int i = 0; i < Buttons.Length; i++)
		{
			RadioButtonBehaviour radioButtonBehaviour = Buttons[i];
			if ((bool)radioButtonBehaviour)
			{
				object value2 = radioButtonBehaviour.GetValue();
				if (value2.Equals(value) || (value2 is float a && value is float b && Mathf.Approximately(a, b)))
				{
					radioButtonBehaviour.Check();
				}
				else
				{
					radioButtonBehaviour.Uncheck();
				}
			}
		}
	}
}
