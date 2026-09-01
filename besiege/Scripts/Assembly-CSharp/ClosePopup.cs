using System;
using UnityEngine;

public class ClosePopup : ClickBehaviour
{
	public bool self;

	public Transform close;

	[NonSerialized]
	private float startTime;

	private void Start()
	{
		Refresh();
	}

	private void Update()
	{
		if (startTime + 5f < Time.time)
		{
			Bye();
		}
	}

	public override void OnClicked()
	{
		Bye();
	}

	private void Bye()
	{
		if (self)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(close.gameObject);
		}
	}

	public void Refresh()
	{
		startTime = Time.time;
	}
}
