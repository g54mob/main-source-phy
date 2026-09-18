using System.Collections;
using UnityEngine;

public class AbilitiesUnlockPanelUI : SceneSingleton<AbilitiesUnlockPanelUI>
{
	[SerializeField]
	private GameObject hightlightAbilityPanel;

	[SerializeField]
	private GameObject assembleAbilityPanel;

	[SerializeField]
	private GameObject shelveHighlightAbilityPanel;

	[SerializeField]
	private CanvasGroup highlightCanvasGroup;

	[SerializeField]
	private CanvasGroup shelveHighlightCanvasGroup;

	[SerializeField]
	private CanvasGroup assembleCanvasGroup;

	private void Start()
	{
		hightlightAbilityPanel.SetActive(value: false);
		assembleAbilityPanel.SetActive(value: false);
		shelveHighlightAbilityPanel.SetActive(value: false);
		highlightCanvasGroup.alpha = 0f;
		assembleCanvasGroup.alpha = 0f;
		shelveHighlightCanvasGroup.alpha = 0f;
	}

	public void ActivateShelveHightlightAbility()
	{
		shelveHighlightAbilityPanel.SetActive(value: false);
		shelveHighlightAbilityPanel.SetActive(value: true);
		StopAllCoroutines();
		StartCoroutine(DisableHighlightsCoroutine());
	}

	public void ActivateHightlightAbility()
	{
		hightlightAbilityPanel.SetActive(value: false);
		hightlightAbilityPanel.SetActive(value: true);
		StopAllCoroutines();
		StartCoroutine(DisableHighlightsCoroutine());
	}

	public void ActivateAssembleAbility()
	{
		assembleAbilityPanel.SetActive(value: false);
		assembleAbilityPanel.SetActive(value: true);
		StopAllCoroutines();
		StartCoroutine(DisableHighlightsCoroutine());
	}

	private IEnumerator DisableHighlightsCoroutine()
	{
		yield return new WaitForSeconds(4f);
		hightlightAbilityPanel.SetActive(value: false);
		shelveHighlightAbilityPanel.SetActive(value: false);
		assembleAbilityPanel.SetActive(value: false);
	}
}
