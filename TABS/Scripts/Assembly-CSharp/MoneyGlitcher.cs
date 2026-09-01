using TMPro;
using UnityEngine;

public class MoneyGlitcher : MonoBehaviour
{
	public TextMeshProUGUI redMon;

	public TextMeshProUGUI blueMon;

	private TextMeshProUGUI redMonGl;

	private TextMeshProUGUI blueMonGl;

	private void Start()
	{
		redMonGl = Object.Instantiate(redMon, redMon.transform.position, redMon.transform.rotation, redMon.transform.parent);
		redMonGl.gameObject.SetActive(value: false);
		blueMonGl = Object.Instantiate(blueMon, blueMon.transform.position, blueMon.transform.rotation, blueMon.transform.parent);
		blueMonGl.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (!redMon.gameObject.activeInHierarchy)
		{
			return;
		}
		if (int.Parse(blueMon.text) >= 999999)
		{
			if (!blueMonGl.gameObject.activeSelf)
			{
				blueMonGl.gameObject.SetActive(value: true);
			}
			blueMonGl.text = Random.Range(100000, 999999).ToString();
			blueMon.enabled = false;
		}
		else
		{
			if (blueMonGl.gameObject.activeSelf)
			{
				blueMonGl.gameObject.SetActive(value: false);
			}
			blueMon.enabled = true;
		}
		if (int.Parse(redMon.text) >= 999999)
		{
			if (!redMonGl.gameObject.activeSelf)
			{
				redMonGl.gameObject.SetActive(value: true);
			}
			redMonGl.text = Random.Range(100000, 999999).ToString();
			redMon.enabled = false;
		}
		else
		{
			if (redMonGl.gameObject.activeSelf)
			{
				redMonGl.gameObject.SetActive(value: false);
			}
			redMon.enabled = true;
		}
	}
}
