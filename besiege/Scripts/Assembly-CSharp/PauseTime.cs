using UnityEngine;

public class PauseTime : ClickBehaviour
{
	public Transform pausedObj;

	public Transform playObj;

	public override void OnClicked()
	{
		TimeSliderView.Instance.PauseTime();
	}

	public void Pause()
	{
		pausedObj.gameObject.SetActive(false);
		playObj.gameObject.SetActive(true);
	}

	public void UnPause()
	{
		pausedObj.gameObject.SetActive(true);
		playObj.gameObject.SetActive(false);
	}
}
