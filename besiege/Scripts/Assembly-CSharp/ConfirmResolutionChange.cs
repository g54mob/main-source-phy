using System;
using UnityEngine;

public class ConfirmResolutionChange : MonoBehaviour
{
	public static bool AwaitingConfirmation;

	public static bool FullscreenConfirm;

	[NonSerialized]
	public Resolution oldResolution;

	[NonSerialized]
	public Resolution targetResolution;

	[NonSerialized]
	public float timeout = -1f;

	public TextMesh countdown;

	public GameObject yesText;

	public GameObject noText;

	public bool begin;

	public CurrentResolution resText;

	private void OnEnable()
	{
		begin = false;
		timeout = -1f;
	}

	public void BeginCountdown(Resolution old, Resolution target)
	{
		AwaitingConfirmation = true;
		FullscreenConfirm = false;
		GameObject.Find("/OPTIONS LIST/Resolutions/CurrentResolutionText").GetComponent<CurrentResolution>().Set();
		timeout = Time.time + 10f;
		oldResolution = old;
		targetResolution = target;
		noText.GetComponent<ConfirmResolutionButton>().resolution = oldResolution;
		begin = true;
	}

	private void BeginCountdown(bool current)
	{
		timeout = Time.time + 10f;
		AwaitingConfirmation = true;
		FullscreenConfirm = true;
	}

	private void Update()
	{
		if (!begin)
		{
			return;
		}
		if (timeout > Time.time)
		{
			countdown.text = Mathf.CeilToInt(timeout - Time.time).ToString();
			return;
		}
		if (FullscreenConfirm)
		{
			Screen.fullScreen = !Screen.fullScreen;
			OptionsMaster.BesiegeConfig.WindowedMode = !Screen.fullScreen;
		}
		else
		{
			Screen.SetResolution(oldResolution.width, oldResolution.height, !OptionsMaster.BesiegeConfig.WindowedMode);
		}
		AwaitingConfirmation = false;
		begin = false;
		timeout = -1f;
		base.gameObject.SetActive(false);
	}
}
