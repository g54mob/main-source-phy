using System.Text;
using UnityEngine;

[RequireComponent(typeof(DynamicText))]
public class TextEffect : MonoBehaviour
{
	public string charGradient = " \u00b8.:oOQ@&8*º°\"'\u00b4";

	public int width = 10;

	public int height = 10;

	private DynamicText dynamicText;

	private void Start()
	{
	}

	private void Update()
	{
		width = Mathf.Clamp(width, 1, 100);
		height = Mathf.Clamp(height, 1, 100);
		int length = charGradient.Length;
		if (length < 1)
		{
			Debug.LogWarning("No char gradient!", this);
			return;
		}
		dynamicText = GetComponent<DynamicText>();
		if (dynamicText == null)
		{
			Debug.LogError("No Dynamic Text!", this);
			return;
		}
		int num = height * (width + 3);
		StringBuilder textSB = dynamicText.textSB;
		textSB.EnsureCapacity(num);
		textSB.Length = num;
		int num2 = 0;
		for (int i = 0; i < height; i++)
		{
			textSB[num2++] = '.';
			for (int j = 0; j < width; j++)
			{
				float fixedTime = Time.fixedTime;
				float num3 = Mathf.Sin(fixedTime * 0.77f + (float)j * 0.41f) * 3f;
				float num4 = Mathf.Sin(fixedTime * 0.65f - (float)i * 0.51f) * 3f;
				float num5 = Mathf.Sin(fixedTime * 1.93f - (float)j * 0.27f) * 4f;
				float num6 = Mathf.Sin(fixedTime * 1.91f + (float)i * 0.29f) * 3f;
				int index = (int)(num3 * num4 + num5 + num6 + (float)(length * 16) + (float)(length / 2)) % length;
				textSB[num2++] = charGradient[index];
			}
			textSB[num2++] = '.';
			textSB[num2++] = '\n';
		}
		dynamicText.FinishedTextSB();
	}
}
