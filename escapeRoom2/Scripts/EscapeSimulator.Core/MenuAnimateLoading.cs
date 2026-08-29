using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimateLoading : MonoBehaviour
{
	private Text text;

	public string originalText = "";

	private StringBuilder builder = new StringBuilder(100);

	private void Awake()
	{
		text = GetComponent<Text>();
		originalText = text.text;
	}

	private void Update()
	{
		builder.Clear();
		int num = (int)(Time.time * 10f) % originalText.Length;
		bool flag = false;
		for (int i = 0; i < originalText.Length; i++)
		{
			char c = originalText[i];
			if (c == '<')
			{
				flag = true;
			}
			if (i == num && !flag)
			{
				builder.Append("<color=#575757aa>");
				builder.Append(c);
				builder.Append("</color>");
			}
			else
			{
				builder.Append(c);
			}
			if (c == '>')
			{
				flag = false;
			}
		}
		text.text = builder.ToString();
	}
}
