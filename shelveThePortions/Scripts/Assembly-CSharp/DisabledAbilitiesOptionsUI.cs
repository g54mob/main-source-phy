using System.Collections;
using UnityEngine;

public class DisabledAbilitiesOptionsUI : SceneSingleton<DisabledAbilitiesOptionsUI>
{
	[SerializeField]
	private GameObject panel;

	private void Start()
	{
		panel.SetActive(value: false);
	}

	public void ShowUI()
	{
		panel.SetActive(value: true);
		StopAllCoroutines();
		StartCoroutine(HideUI());
	}

	private IEnumerator HideUI()
	{
		yield return new WaitForSeconds(2f);
		panel.SetActive(value: false);
	}
}
