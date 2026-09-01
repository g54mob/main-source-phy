using System.Collections;
using UnityEngine;

public class TextAnimate : MonoBehaviour
{
	public string texty = string.Empty;

	public TextMesh textDisplay;

	public float letterPause = 0.2f;

	public float fullStopPause = 0.1f;

	private void OnEnable()
	{
		TypeText();
	}

	private IEnumerator TypeText()
	{
		textDisplay.text = string.Empty;
		char[] array = texty.ToCharArray();
		foreach (char letter in array)
		{
			textDisplay.text += letter;
			if (letter != ' ')
			{
				GetComponent<AudioSource>().Play();
			}
			else if (letter == '.' || letter == ',')
			{
				yield return new WaitForSeconds(fullStopPause);
			}
			yield return new WaitForSeconds(letterPause);
		}
	}
}
