using UnityEngine;
using UnityEngine.UI;

public class StarsRating : MonoBehaviour
{
	public Transform m_Container;

	public Image[] m_Stars;

	private const int STAR_WIDTH = 20;

	private const int STAR_SPACING = 5;

	public void Set(float numStars)
	{
		int num = Mathf.FloorToInt(numStars);
		float num2 = numStars - (float)num;
		for (int i = 0; i < m_Stars.Length; i++)
		{
			if (i < num)
			{
				m_Stars[i].fillAmount = 1f;
				m_Stars[i].gameObject.SetActive(value: true);
			}
			else if (i == num && !Mathf.Approximately(num2, 0f))
			{
				m_Stars[i].fillAmount = num2;
				m_Stars[i].gameObject.SetActive(value: true);
			}
			else
			{
				m_Stars[i].fillAmount = 0f;
				m_Stars[i].gameObject.SetActive(value: false);
			}
		}
	}

	public float GetLength(float numStars)
	{
		float num = numStars * 20f;
		int num2 = Mathf.FloorToInt(numStars);
		float a = numStars - (float)num2;
		if (num2 > 0)
		{
			num = ((!Mathf.Approximately(a, 0f)) ? (num + (float)(num2 * 5)) : (num + (float)((num2 - 1) * 5)));
		}
		return num;
	}
}
