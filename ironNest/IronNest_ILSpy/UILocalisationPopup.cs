using Localisation;
using UnityEngine;

public class UILocalisationPopup : MonoBehaviour
{
	public void OnAcceptCurrent()
	{
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: false);
	}

	public void OnCancel()
	{
		LocalisationManager.Instance.SwitchLanguage("English", save: true);
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: false);
	}
}
