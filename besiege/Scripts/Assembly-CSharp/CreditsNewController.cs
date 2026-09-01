using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("UI/CreditsNewController")]
public class CreditsNewController : MonoBehaviour
{
	public float delay = 5f;

	public float scrollSpeed = 150f;

	public FadeScreenCanvas fader;

	public RectTransform lastTransform;

	public GameObject blackBackground;

	public int levelToCheckForUnlock = 43;

	private bool loading;

	private bool IsUnlocked
	{
		get
		{
			return LEVELLORD.levelsComplete[levelToCheckForUnlock] == 1;
		}
	}

	private void Start()
	{
		blackBackground.SetActive(!IsUnlocked);
		StartCoroutine(RollCredits());
	}

	private IEnumerator RollCredits()
	{
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(delay);
		Vector3[] canvasCorners = new Vector3[4];
		(GetComponentInParent<Canvas>().transform as RectTransform).GetWorldCorners(canvasCorners);
		float canvasRectHeight = canvasCorners[1].y / (float)Screen.height;
		Vector3[] corners = new Vector3[4];
		do
		{
			base.transform.Translate(0f, scrollSpeed * Time.deltaTime * ((float)Screen.height / 1080f), 0f, Space.Self);
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				break;
			}
			yield return null;
			lastTransform.GetWorldCorners(corners);
		}
		while (corners[0].y < canvasRectHeight * (float)Screen.height);
		Time.timeScale = 1f;
		LoadLevel();
	}

	public void LoadLevel()
	{
		if (!loading)
		{
			loading = true;
			StartCoroutine(IELoadLevel());
		}
	}

	public IEnumerator IELoadLevel()
	{
		if (fader != null)
		{
			yield return fader.StartCoroutine(fader.FadeIn());
		}
		if (StatMaster.levelSimulating)
		{
			yield return new WaitForSecondsRealtime(0.5f);
		}
		SceneManager.LoadScene("TITLE SCREEN", LoadSceneMode.Single);
	}
}
