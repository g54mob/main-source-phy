using TMPro;
using UnityEngine;

public class PageCounter : MonoBehaviour
{
	public TextMeshProUGUI Count;

	public TextMeshProUGUI PageNumber;

	public void Set(int page, int count)
	{
		page = Mathf.Clamp(page, 0, count);
		PageNumber.text = page.ToString();
		Count.text = count.ToString();
	}
}
