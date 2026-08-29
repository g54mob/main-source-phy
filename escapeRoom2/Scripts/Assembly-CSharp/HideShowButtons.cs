using UnityEngine;

public class HideShowButtons : MonoBehaviour
{
	public GameObject buttons;

	private bool areVisible;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			areVisible = !areVisible;
			if (areVisible)
			{
				buttons.SetActive(value: false);
			}
			else
			{
				buttons.SetActive(value: true);
			}
		}
	}
}
