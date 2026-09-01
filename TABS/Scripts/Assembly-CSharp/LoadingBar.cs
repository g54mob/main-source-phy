using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
	[SerializeField]
	private Image loadingBarImage;

	[SerializeField]
	private TABSBooter tabsBooter;

	[SerializeField]
	private Canvas canvas;

	private void Awake()
	{
		if (tabsBooter != null)
		{
			TABSBooter tABSBooter = tabsBooter;
			tABSBooter.LoadUpdate = (Action<float>)Delegate.Combine(tABSBooter.LoadUpdate, new Action<float>(OnLoadUpdate));
		}
		loadingBarImage.fillAmount = 0f;
	}

	private void OnLoadUpdate(float inc)
	{
		loadingBarImage.fillAmount += inc;
	}

	private void OnDestroy()
	{
		if (tabsBooter != null)
		{
			TABSBooter tABSBooter = tabsBooter;
			tABSBooter.LoadUpdate = (Action<float>)Delegate.Remove(tABSBooter.LoadUpdate, new Action<float>(OnLoadUpdate));
		}
	}
}
