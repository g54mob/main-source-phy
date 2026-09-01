using UnityEngine;

public class SwitchObjectOnAspectRatio : MonoBehaviour
{
	public GameObject ObjectToSwitchTo;

	public float aspectThreshold = 1.7f;

	private void Start()
	{
		if ((float)Screen.width / (float)Screen.height < aspectThreshold)
		{
			ObjectToSwitchTo.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
	}
}
