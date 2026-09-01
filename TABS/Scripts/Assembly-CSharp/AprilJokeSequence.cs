using System.Collections;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AprilJokeSequence : MonoBehaviour
{
	public Image image;

	public TextMeshProUGUI text;

	[Multiline]
	public string jk;

	[Multiline]
	public string gameOut;

	[Multiline]
	public string happyFool;

	private AudioSource source;

	private float alphaMenuTime = 7f;

	public GameObject alphaAnimation;

	private SoundPlayer soundPlayer;

	private readonly AudioPathData uiUnitPlaced = new AudioPathData("UI", "Unit Placed");

	private readonly AudioPathData uiHover = new AudioPathData("UI", "Hover");

	private void Start()
	{
		soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		source = GetComponent<AudioSource>();
		StartCoroutine(Sequence());
		StartCoroutine(GotoMenu());
	}

	private void LoadMenuScene()
	{
		TABSSceneManager.LoadMainMenu();
	}

	private IEnumerator GotoMenu()
	{
		yield return new WaitForSecondsRealtime(20f);
		yield return new WaitForSecondsRealtime(alphaMenuTime);
		LoadMenuScene();
	}

	private IEnumerator Sequence()
	{
		yield return new WaitForSeconds(10f);
		while (Time.timeScale > 0f)
		{
			source.pitch -= Time.unscaledDeltaTime * 1f;
			Time.timeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime * 1f, 0f, 1f);
			yield return null;
		}
		Time.timeScale = 0f;
		source.pitch = 0f;
		yield return new WaitForSecondsRealtime(0.5f);
		while (image.color.a < 1f)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.unscaledDeltaTime * 1f);
			yield return null;
		}
		Time.timeScale = 1f;
		alphaAnimation.SetActive(value: true);
		yield return new WaitForSecondsRealtime(alphaMenuTime);
		yield return new WaitForSecondsRealtime(0.5f);
		alphaAnimation.SetActive(value: false);
		char[] charArray = jk.ToCharArray();
		for (int i = 0; i < charArray.Length; i++)
		{
			text.text += charArray[i];
			TypeFeedback();
			yield return new WaitForSecondsRealtime(0.05f);
		}
		yield return new WaitForSecondsRealtime(2f);
		RemoveFeedback();
		text.text = "";
		yield return new WaitForSecondsRealtime(1f);
		charArray = happyFool.ToCharArray();
		for (int i = 0; i < charArray.Length; i++)
		{
			text.text += charArray[i];
			TypeFeedback();
			yield return new WaitForSecondsRealtime(0.05f);
		}
		yield return new WaitForSecondsRealtime(2f);
		RemoveFeedback();
		text.text = "";
		yield return new WaitForSecondsRealtime(1f);
	}

	private void TypeFeedback()
	{
		soundPlayer.PlaySoundEffectNonAlloc(uiUnitPlaced, 0.2f, base.transform.position);
	}

	private void RemoveFeedback()
	{
		soundPlayer.PlaySoundEffectNonAlloc(uiHover, 1f, base.transform.position);
	}

	private void LightsOut()
	{
		soundPlayer.PlaySoundEffectNonAlloc(uiUnitPlaced, 0.3f, base.transform.position);
	}
}
