using UnityEngine;

public class LocalizationUITextFitter : MonoBehaviour
{
	private void OnEnable()
	{
		Singleton<LocalizationSystem>.Instance.OnLanguageChangeUIUpdate += OnLanguageChangeUIUpdate;
		Singleton<UISystem>.Instance.RefreshContentSize(base.gameObject);
	}

	private void OnDisable()
	{
		Singleton<LocalizationSystem>.Instance.OnLanguageChangeUIUpdate -= OnLanguageChangeUIUpdate;
	}

	public void OnLanguageChangeUIUpdate()
	{
		Singleton<UISystem>.Instance.RefreshContentSize(base.gameObject);
	}
}
