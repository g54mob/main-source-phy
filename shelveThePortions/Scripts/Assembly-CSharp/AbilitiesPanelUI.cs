using UnityEngine;

public class AbilitiesPanelUI : SceneSingleton<AbilitiesPanelUI>
{
	[SerializeField]
	private AbilityUI highlightAbilityUI;

	[SerializeField]
	private AbilityUI assembleAbilityUI;

	[SerializeField]
	private AbilityUI shelvehighlightAbilityUI;

	public void LoadAbilitiesPanelUI()
	{
		Invoke("InvokeActivation", 0.1f);
	}

	private void InvokeActivation()
	{
		highlightAbilityUI.gameObject.SetActive(value: true);
		assembleAbilityUI.gameObject.SetActive(value: true);
		shelvehighlightAbilityUI.gameObject.SetActive(value: true);
	}

	public void ActivateShelveHighlightPanel()
	{
		shelvehighlightAbilityUI.ActivatePanel();
	}

	public void SetShelveHighlightTimer(int seconds, float percent)
	{
		shelvehighlightAbilityUI.SetTimer(seconds, percent);
	}

	public void SetShelveHighlightLocked(int neededCabinets)
	{
		shelvehighlightAbilityUI.SetLocked(neededCabinets);
	}

	public void ActivateHighlightPanel()
	{
		highlightAbilityUI.ActivatePanel();
	}

	public void SetHighlightTimer(int seconds, float percent)
	{
		highlightAbilityUI.SetTimer(seconds, percent);
	}

	public void SetHighlightLocked(int neededCabinets)
	{
		highlightAbilityUI.SetLocked(neededCabinets);
	}

	public void ActivateAssemblePanel()
	{
		assembleAbilityUI.ActivatePanel();
	}

	public void SetAssembleTimer(int seconds, float percent)
	{
		assembleAbilityUI.SetTimer(seconds, percent);
	}

	public void SetAssembleLocked(int neededCabinets)
	{
		assembleAbilityUI.SetLocked(neededCabinets);
	}
}
