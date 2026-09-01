using ModIO;
using UnityEngine;
using UnityEngine.UI;

public class TermsDialog : MonoBehaviour
{
	public Text acceptText;

	public Text declineText;

	public GameObject textTemplate;

	public Button agreeBtn;

	public Button declineBtn;

	public ScrollRect termsScrollView;

	public GameObject loadIndicator;

	public CanvasGroup canvasGroup
	{
		get
		{
			return base.gameObject.GetComponent<CanvasGroup>();
		}
	}

	private void OnEnable()
	{
		agreeBtn.gameObject.SetActive(false);
		declineBtn.gameObject.SetActive(true);
		declineText.text = string.Empty;
		loadIndicator.SetActive(true);
		APIClient.GetTermsOfUse(delegate(TermsOfUseInfo termInfo)
		{
			OpenTerms(termInfo);
		}, delegate(WebRequestError error)
		{
			Debug.LogError("Failed to grab terms due to error: " + error.displayMessage + ", errorMsg: " + error.errorMessage);
		});
	}

	private void ShowText(string text)
	{
		GameObject gameObject = Object.Instantiate(textTemplate, textTemplate.transform.parent) as GameObject;
		gameObject.GetComponent<Text>().text = text;
		gameObject.SetActive(true);
	}

	public void OpenTerms(TermsOfUseInfo terms)
	{
		loadIndicator.SetActive(false);
		acceptText.text = terms.buttonText_agree.ToUpper();
		declineText.text = terms.buttonText_disagree.ToUpper();
		ShowText(terms.terms);
		agreeBtn.gameObject.SetActive(true);
		declineBtn.gameObject.SetActive(true);
		for (int i = 0; i <= 6; i++)
		{
			TextAsset textAsset = Resources.Load<TextAsset>("ModIOTerms\\terms" + i);
			if (textAsset != null)
			{
				ShowText(textAsset.text);
			}
		}
	}
}
