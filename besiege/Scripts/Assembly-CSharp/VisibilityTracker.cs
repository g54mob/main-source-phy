using System;
using UnityEngine;

public class VisibilityTracker : MonoBehaviour
{
	public Action<bool> onVisibilityChanged;

	private void OnBecameVisible()
	{
		if (onVisibilityChanged != null)
		{
			onVisibilityChanged(true);
		}
	}

	private void OnBecameInvisible()
	{
		if (onVisibilityChanged != null)
		{
			onVisibilityChanged(false);
		}
	}
}
