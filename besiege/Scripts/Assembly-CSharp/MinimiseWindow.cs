using System;
using System.Collections;
using UnityEngine;

public class MinimiseWindow : ClickBehaviour
{
	public Transform openWindow;

	public Transform minimisedWindow;

	public float clickTimer = 0.3f;

	protected bool isClick;

	public Action OnMinimise;

	protected virtual void Awake()
	{
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		StartCoroutine(TimeClick());
	}

	public override void OnClickReleased()
	{
		if (isClick)
		{
			Minimise();
		}
	}

	public virtual void Minimise()
	{
		openWindow.gameObject.SetActive(false);
		minimisedWindow.gameObject.SetActive(true);
		if (OnMinimise != null)
		{
			OnMinimise();
		}
	}

	public virtual void Maximise()
	{
		openWindow.gameObject.SetActive(true);
		minimisedWindow.gameObject.SetActive(false);
	}

	protected virtual IEnumerator TimeClick()
	{
		isClick = true;
		yield return new WaitForSeconds(clickTimer);
		isClick = false;
	}
}
