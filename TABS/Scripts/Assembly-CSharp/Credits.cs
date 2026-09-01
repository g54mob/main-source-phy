using System.Collections;
using System.Collections.Generic;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using TABSCredits;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
	public float timeScale = 1f;

	public int currentID;

	public float spring = 15f;

	public float damper = 15f;

	public float scrollSpeed = 10f;

	public float heightOffset;

	private bool isAnimating;

	private float vel;

	private bool scrolling;

	public CanvasGroup backButton;

	public Image backHoldImage;

	private float backButtonTimer;

	private const float backHoldTime = 1f;

	public GameObject mover;

	public GameObject imgParent;

	public GameObject whiteBar;

	private CanvasGroup cg;

	private bool exiting;

	public GameObject source;

	public CreditInstance[] landfall;

	public CreditInstance[] bit24;

	public CreditInstance[] doubleMoose;

	public CreditInstance[] frogSong;

	public CreditInstance[] indiumPlay;

	public CreditInstance[] others1;

	public CreditInstance[] others2;

	public CreditInstance[] others3;

	private List<CreditInstance> credits;

	private IEnumerator DoSequence()
	{
		ITimeService service = ServiceLocator.GetService<ITimeService>();
		service.Unlock();
		service.SetState(timeScale, 0.1f);
		yield return new WaitForSeconds(1f);
		isAnimating = true;
		for (int i = (currentID = 0); i < landfall.Length; i++)
		{
			currentID++;
			yield return new WaitForSeconds(0.5f);
			if (i == landfall.Length - 1)
			{
				continue;
			}
			StartCoroutine(ToggleBG(turnOn: false));
			TextMeshProUGUI[] texts = mover.transform.GetChild(currentID).gameObject.GetComponentsInChildren<TextMeshProUGUI>();
			if (currentID > 1)
			{
				for (int j = 0; j < texts.Length; j++)
				{
					StartCoroutine(LerColor(turnOn: true, texts[j], Color.black, mover.transform.GetChild(currentID).gameObject));
				}
				SwitchOn(i, mover.transform.GetChild(currentID).gameObject, on: true);
			}
			yield return new WaitForSeconds(3.5f);
			StartCoroutine(ToggleBG(turnOn: true));
			yield return new WaitForSeconds(0.5f);
			if (currentID > 1)
			{
				for (int k = 0; k < texts.Length; k++)
				{
					StartCoroutine(LerColor(turnOn: true, texts[k], Color.white, mover.transform.GetChild(currentID).gameObject));
				}
				SwitchOn(i, mover.transform.GetChild(currentID).gameObject, on: false);
			}
			yield return new WaitForSeconds(0.5f);
		}
		EnableBackButton(enabled: true, backButton.alpha);
		Debug.Log("START SCROLLING");
		scrolling = true;
		while (base.transform.InverseTransformPoint(mover.transform.GetChild(mover.transform.childCount - 1).position).y < 1200f)
		{
			backButton.alpha += Time.deltaTime;
			Debug.Log("SCROLLING");
			mover.transform.position += Vector3.up * Time.deltaTime * scrollSpeed;
			yield return null;
		}
		Debug.Log("DONE SCROLLING");
		ExitCredits();
	}

	private IEnumerator ToggleBG(bool turnOn)
	{
		if (turnOn)
		{
			float c = 0f;
			while (c < 1f)
			{
				c += Time.deltaTime * 2f;
				cg.alpha = c;
				yield return null;
			}
		}
		else
		{
			float c = 1f;
			while (c > 0f)
			{
				c -= Time.deltaTime * 2f;
				cg.alpha = c;
				yield return null;
			}
		}
	}

	private IEnumerator LerColor(bool turnOn, TextMeshProUGUI text, Color targetColor, GameObject obj)
	{
		Color beforeColor = text.color;
		float c = 0f;
		while (c < 1f)
		{
			c += Time.deltaTime * 2f;
			text.color = Color.Lerp(beforeColor, targetColor, c);
			yield return null;
		}
	}

	private void SwitchOn(int id, GameObject obj, bool on)
	{
		if (on)
		{
			whiteBar.GetComponent<TABSCredits.CurveAnimation>().PlayIn();
		}
		else
		{
			whiteBar.GetComponent<TABSCredits.CurveAnimation>().PlayOut();
		}
		if (on)
		{
			for (int i = 0; i < imgParent.transform.childCount; i++)
			{
				imgParent.transform.GetChild(i).gameObject.SetActive(i == id && on);
			}
		}
	}

	private void EnableBackButton(bool enabled, float alpha)
	{
		backButton.gameObject.SetActive(enabled);
		backButton.alpha = alpha;
	}

	private void Update()
	{
		PlayerActions instance = PlayerActions.Instance;
		if (instance.InputType == InputType.Keyboard && instance.m_back.WasPressed)
		{
			ExitCredits();
		}
		if (instance.InputType == InputType.Controller)
		{
			if (instance.m_back.IsPressed)
			{
				EnableBackButton(enabled: true, 1f);
				backHoldImage.fillAmount = backButtonTimer / 1f;
				backButtonTimer += Time.deltaTime;
				if (backButtonTimer > 1f)
				{
					ExitCredits();
				}
			}
			if (instance.m_back.WasReleased)
			{
				backButtonTimer = 0f;
				backHoldImage.fillAmount = 0f;
			}
		}
		if (!scrolling && isAnimating)
		{
			Transform child = mover.transform.GetChild(currentID);
			float num = 0f;
			if (currentID != 1)
			{
				num = whiteBar.transform.position.y;
			}
			vel = FRILerp.Lerp(vel, (0f - base.transform.InverseTransformPoint(child.position + Vector3.up * num).y) * spring, damper);
			mover.transform.position += Vector3.up * Time.deltaTime * vel;
		}
	}

	public void ExitCredits()
	{
		if (!exiting)
		{
			exiting = true;
			TABSSceneManager.LoadMainMenu();
		}
	}

	private void Start()
	{
		ServiceLocator.GetService<MusicHandler>().PlayCreditsMusic();
		cg = GetComponentInChildren<CanvasGroup>();
		EnableBackButton(enabled: false, 0f);
		for (int i = 0; i < landfall.Length; i++)
		{
			if (landfall[i].creditType == CreditInstance.CreditType.Header)
			{
				landfall[i].itemSize = 1000f;
			}
			else
			{
				landfall[i].itemSize = 1000f;
			}
		}
		List<CreditInstance> list = new List<CreditInstance>();
		list.AddRange(bit24);
		list.AddRange(doubleMoose);
		list.AddRange(frogSong);
		list.AddRange(indiumPlay);
		list.AddRange(others1);
		list.AddRange(others2);
		list.AddRange(others3);
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].creditType == CreditInstance.CreditType.Header)
			{
				list[j].itemSize = 200f;
				continue;
			}
			list[j].itemSize = 100f;
			if (list[j].subText != "")
			{
				list[j].itemSize += 30f;
			}
		}
		credits = new List<CreditInstance>();
		credits.AddRange(landfall);
		credits.AddRange(list);
		for (int k = 0; k < credits.Count; k++)
		{
			GameObject gameObject = Object.Instantiate(source, source.transform.position, source.transform.rotation, source.transform.parent);
			gameObject.SetActive(value: true);
			switch (credits[k].creditType)
			{
			case CreditInstance.CreditType.Header:
			{
				GameObject gameObject2 = gameObject.transform.Find("Header").gameObject;
				gameObject2.SetActive(value: true);
				if (credits[k].localize)
				{
					gameObject2.GetComponent<LocalizeText>().LocaleID = credits[k].text;
				}
				else
				{
					gameObject2.GetComponent<TextMeshProUGUI>().text = credits[k].text;
				}
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000f, credits[k].itemSize);
				break;
			}
			case CreditInstance.CreditType.Person:
			{
				GameObject obj2 = gameObject.transform.Find("Text").gameObject;
				obj2.SetActive(value: true);
				obj2.GetComponent<TextMeshProUGUI>().text = credits[k].text;
				GameObject obj3 = gameObject.transform.Find("Subtext").gameObject;
				obj3.SetActive(value: true);
				obj3.GetComponent<LocalizeText>().LocaleID = credits[k].subText;
				gameObject.GetComponent<VerticalLayoutGroup>().spacing = 20f;
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000f, credits[k].itemSize);
				break;
			}
			case CreditInstance.CreditType.Logo:
			{
				GameObject obj = gameObject.transform.Find("Logo").gameObject;
				obj.SetActive(value: true);
				obj.GetComponent<Image>().sprite = credits[k].logo;
				gameObject.GetComponent<VerticalLayoutGroup>().padding = new RectOffset();
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000f, 200f);
				break;
			}
			case CreditInstance.CreditType.Spacing:
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000f, credits[k].spacingSize);
				break;
			}
		}
		Object.Destroy(source);
		StartCoroutine(DoSequence());
	}
}
