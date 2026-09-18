using UnityEngine;

public class PettingPanelUI : MonoBehaviour
{
	[SerializeField]
	private GameObject button;

	[SerializeField]
	private GameObject pettingHand;

	private void Start()
	{
		EventManager.OnControlsChange += OnControlsChange;
		OnControlsChange();
	}

	private void OnDisable()
	{
		EventManager.OnControlsChange -= OnControlsChange;
	}

	public void UpdatePanel()
	{
		pettingHand.SetActive(!SceneSingleton<TutorialManager>.Instance.isTutorialCompleted);
	}

	private void OnControlsChange()
	{
		button.SetActive(Singleton<InputManager>.Instance.IsUsingGamepad);
	}
}
