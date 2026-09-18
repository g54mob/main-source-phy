using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUIManager : SceneSingleton<ControlsUIManager>
{
	[SerializeField]
	private CanvasGroup managerCG;

	[SerializeField]
	private List<GameObject> keyboardMoveList;

	[SerializeField]
	private List<ControlUIContainer> controlUIContainersList;

	public bool isRebinding;

	public bool canRebind = true;

	private void OnEnable()
	{
		EventManager.OnControlsChange += OnControlsChange;
		RefreshUI();
	}

	private void OnDisable()
	{
		EventManager.OnControlsChange -= OnControlsChange;
	}

	public void OnControlsChange()
	{
		foreach (GameObject keyboardMove in keyboardMoveList)
		{
			keyboardMove.SetActive(!Singleton<InputManager>.Instance.IsUsingGamepad);
		}
	}

	public void ResetButton()
	{
		SaveSystem.ResetPlayerInput();
		StartCoroutine(RefreshControlsUI());
	}

	public void ApplyNewRebind()
	{
		SaveSystem.SavePlayerInput();
		StartCoroutine(RefreshControlsUI());
	}

	private IEnumerator RefreshControlsUI()
	{
		yield return new WaitForEndOfFrame();
		Singleton<InputDataStore>.Instance.OnControlApplied?.Invoke();
		RefreshUI();
	}

	public void RefreshUI()
	{
		SaveSystem.LoadPlayerInput();
		foreach (ControlUIContainer controlUIContainers in controlUIContainersList)
		{
			controlUIContainers.UpdateContainerUI();
		}
	}

	public void OnRebindStarted()
	{
		isRebinding = true;
		managerCG.interactable = false;
	}

	public void OnRebindEnded()
	{
		StopAllCoroutines();
		StartCoroutine(WaitAtLeastAFrameAndSave());
	}

	private IEnumerator WaitAtLeastAFrameAndSave()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		isRebinding = false;
		managerCG.interactable = true;
	}
}
