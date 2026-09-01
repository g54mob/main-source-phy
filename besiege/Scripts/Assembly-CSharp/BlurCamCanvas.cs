using System.Collections.Generic;
using UnityEngine;

public class BlurCamCanvas : BlurCamTest
{
	protected List<RectTransform> _targets = new List<RectTransform>();

	public override Object target
	{
		get
		{
			return _targets[_targets.Count - 1];
		}
		set
		{
			bool flag = value != null;
			if (flag)
			{
				_targets.Add(value as RectTransform);
			}
			else
			{
				_targets.Clear();
			}
			Start();
			myCamera.enabled = !StatMaster.hudHidden && flag;
		}
	}

	public virtual void AddTarget(RectTransform t)
	{
		if (!_targets.Contains(t))
		{
			_targets.Add(t);
		}
	}

	public virtual void RemoveTarget(RectTransform t)
	{
		if (_targets.Contains(t))
		{
			_targets.Remove(t);
		}
	}

	protected override void LateUpdate()
	{
		if (_targets.Count < 1 || StatMaster.hudHidden)
		{
			if (myCamera.enabled)
			{
				myCamera.enabled = false;
			}
			return;
		}
		if (!myCamera.enabled)
		{
			myCamera.enabled = true;
		}
		Vector3[] array = new Vector3[4];
		_targets[_targets.Count - 1].GetWorldCorners(array);
		for (int i = 1; i < 4; i += 2)
		{
			array[i] = new Vector3(Mathf.Clamp(array[i].x / (float)Screen.width, 0f, 1f), Mathf.Clamp(array[i].y / (float)Screen.height, 0f, 1f), 0f);
		}
		myCamera.rect = new Rect(array[1].x, array[3].y, array[3].x - array[1].x, array[1].y - array[3].y);
	}
}
