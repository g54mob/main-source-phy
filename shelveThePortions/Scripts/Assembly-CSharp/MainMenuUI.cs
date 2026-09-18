using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : PanelUI<MainMenuUI>
{
	[SerializeField]
	private Button continueButton;

	[SerializeField]
	private GameObject wishlistButton;

	[Space]
	[SerializeField]
	private Image studioImage;

	[SerializeField]
	private List<Sprite> studioSprites;

	private int index;

	protected override void Awake()
	{
		base.Awake();
		EventManager.MainMenuLoaded += StartCycleImages;
		EventManager.GameStart += StopCycleImages;
	}

	private void OnDisable()
	{
		EventManager.MainMenuLoaded -= StartCycleImages;
		EventManager.GameStart -= StopCycleImages;
	}

	private void Start()
	{
		if (Singleton<GameBuildManager>.Instance != null)
		{
			continueButton.gameObject.SetActive(SaveSystem.IsThereASavedLevel() && !Singleton<GameBuildManager>.Instance.IsFreeVersion());
			wishlistButton.SetActive(Singleton<GameBuildManager>.Instance.IsFreeVersion());
		}
	}

	private void StartCycleImages()
	{
		StopAllCoroutines();
		StartCoroutine(CycleImages());
	}

	private void StopCycleImages()
	{
		StopAllCoroutines();
	}

	private IEnumerator CycleImages()
	{
		index = 0;
		while (true)
		{
			yield return new WaitForSeconds(3f);
			studioImage.sprite = studioSprites[index];
			index++;
			if (index >= studioSprites.Count)
			{
				index = 0;
			}
		}
	}

	public override void SetSelectedButton()
	{
		if (continueButton.gameObject.activeSelf)
		{
			EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
		}
		else
		{
			EventSystem.current.SetSelectedGameObject(selectedButton);
		}
	}

	public void ContinueGame()
	{
		SceneSingleton<GameManager>.Instance.ContinueGame();
	}

	public void StartGame()
	{
		SceneSingleton<GameManager>.Instance.StartNewGame();
	}

	public void ShowOptions()
	{
		SceneSingleton<UIManager>.Instance.ShowOptionMenu(isPauseMenu: false);
	}

	public void ExitApplication()
	{
		ScenesManager.ExitApplication();
	}

	public void OpenWishListButton()
	{
		Singleton<GoToURL>.Instance.OpenURLSteam(URLHolder.SteamPageURL);
	}

	public void OpenStudioURL()
	{
		Singleton<GoToURL>.Instance.OpenURLSteam(URLHolder.StudioSteamURL);
	}
}
