using UnityEngine;
using UnityEngine.UI;

public class SplashUI : PineUIComponent
{
	public Canvas root;

	public Image Background;

	public CanvasGroup Fader;

	public CanvasGroup Fader_Logos;

	public Image Fader_Logos_Pine;

	public Image Fader_Logos_fmod;

	public SpinnerUI Fader_SpinnerParent;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
		}
	}
}
