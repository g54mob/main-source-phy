using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
	public TextMesh titleTextMesh;

	public TextMesh titleDropShadows;

	public TextMesh listTextMesh;

	public TextMesh listDropShadows;

	public Image fadeImage;

	public float fadeInTime = 2f;

	public float fadeOutTime = 2f;

	public float rotateSpeed = 1f;

	public CreditsSet[] creditsSets;

	private bool dragging;

	private CreditsSet currentSet;

	public void Awake()
	{
		Color color = fadeImage.color;
		color.a = 1f;
		fadeImage.color = color;
		StartCoroutine(PlayCredits());
	}

	public void Update()
	{
		if (InputManager.RotateCameraKeyHeld())
		{
			currentSet.Diorama.Rotate(0f, (0f - InputManager.MouseX()) * rotateSpeed * Time.smoothDeltaTime, 0f, Space.World);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("TITLE SCREEN", LoadSceneMode.Single);
		}
	}

	private IEnumerator PlayCredits()
	{
		int currentSetIndex = 0;
		while (currentSetIndex < creditsSets.Length)
		{
			if (creditsSets[currentSetIndex].Title == "SPECIAL THANKS")
			{
				listTextMesh.fontSize = 15;
				listDropShadows.fontSize = 15;
			}
			else
			{
				listTextMesh.fontSize = 30;
				listDropShadows.fontSize = 30;
			}
			currentSet = creditsSets[currentSetIndex];
			titleTextMesh.text = "- " + currentSet.Title + " -";
			titleDropShadows.text = "- " + currentSet.Title + " -";
			listTextMesh.text = currentSet.List;
			listDropShadows.text = currentSet.List;
			currentSet.Diorama.localRotation = Quaternion.identity;
			currentSet.Diorama.gameObject.SetActive(true);
			fadeImage.CrossFadeAlpha(0f, fadeInTime, false);
			yield return new WaitForSeconds(fadeInTime);
			yield return new WaitForSeconds(currentSet.TimeToShow);
			fadeImage.CrossFadeAlpha(1f, fadeOutTime, false);
			yield return new WaitForSeconds(fadeOutTime);
			currentSet.Diorama.gameObject.SetActive(false);
			currentSetIndex++;
			if (currentSetIndex == creditsSets.Length)
			{
				SceneManager.LoadScene("CreditsSpecialThanks", LoadSceneMode.Single);
			}
		}
	}
}
