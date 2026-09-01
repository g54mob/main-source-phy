using System.Collections;
using Landfall.TABS.GameMode;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionCanvas : MonoBehaviour
{
	public float startDelay;

	public GameObject CampaignUI;

	public GameObject SandboxUI;

	public GameObject UIContainer;

	public Image fade;

	private bool isOpen;

	public void ShowMap()
	{
		if (!isOpen)
		{
			Canvas component = GetComponent<Canvas>();
			component.enabled = false;
			component.enabled = true;
			StopAllCoroutines();
			StartCoroutine(DelayedShow());
			GetComponent<Canvas>().overrideSorting = true;
			isOpen = true;
		}
	}

	private IEnumerator DelayedShow()
	{
		yield return new WaitForSeconds(startDelay);
		fade.enabled = true;
		UIContainer.SetActive(value: true);
		GetGameModeUI();
		float timer = 0f;
		while (timer < 1f)
		{
			timer += Time.unscaledDeltaTime * 5f;
			float a = Mathf.Lerp(1f, 0f, timer);
			Color color = fade.color;
			color.a = a;
			fade.color = color;
			yield return null;
		}
		fade.enabled = false;
	}

	public void HideMap()
	{
		if (isOpen)
		{
			Canvas component = GetComponent<Canvas>();
			component.enabled = false;
			component.enabled = true;
			StopAllCoroutines();
			StartCoroutine(DelayedHide());
			GetComponent<Canvas>().overrideSorting = false;
			isOpen = false;
		}
	}

	private IEnumerator DelayedHide()
	{
		float timer = 0f;
		while (timer < 1f)
		{
			timer += Time.unscaledDeltaTime * 15f;
			float a = Mathf.Lerp(0f, 1f, timer);
			Color color = fade.color;
			color.a = a;
			fade.color = color;
			yield return null;
		}
		UIContainer.SetActive(value: false);
	}

	public void GetGameModeUI()
	{
		if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() == typeof(SandboxGameMode))
		{
			if (SandboxUI != null)
			{
				SandboxUI.SetActive(value: true);
				UISandboxLevelSelector component = SandboxUI.GetComponent<UISandboxLevelSelector>();
				if (component != null)
				{
					component.OpenPage();
				}
			}
		}
		else if (CampaignUI != null)
		{
			CampaignUI.SetActive(value: true);
			Paginator component2 = CampaignUI.GetComponent<Paginator>();
			if (component2 != null)
			{
				component2.OpenPage();
			}
		}
	}
}
