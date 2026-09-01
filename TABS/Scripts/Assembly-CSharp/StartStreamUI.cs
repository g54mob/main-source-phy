using UnityEngine;
using UnityEngine.UI;

public class StartStreamUI : MonoBehaviour
{
	public GameObject StreamUI;

	public Text InputText;

	private TwitchHandler TwitchHandler;

	public void Clicked()
	{
		string text = InputText.text;
		if (!(text == ""))
		{
			StreamUI.SetActive(value: true);
			TwitchHandler = Object.FindObjectOfType<TwitchHandler>();
			TwitchHandler.ConnectToStream(text);
			base.gameObject.SetActive(value: false);
		}
	}
}
