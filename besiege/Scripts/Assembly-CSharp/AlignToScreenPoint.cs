using System;
using UnityEngine;

public class AlignToScreenPoint : MonoBehaviour
{
	public float percentFromEdge;

	public float xOffsetPercent;

	public float yOffsetPercent;

	public bool alignTop;

	public bool alignTopMid;

	public bool alignLeft;

	public bool alignLeftMid;

	public Camera cam;

	public bool alignOnStart;

	private float onePercentFromVerticalEdge;

	private float onePercentFromHorizontalEdge;

	private float xPos;

	private float yPos;

	public void Start()
	{
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChange));
		if (alignOnStart)
		{
			Align();
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChange));
	}

	private void OnResolutionChange()
	{
		if (alignOnStart)
		{
			Align();
		}
	}

	[ContextMenu("Align")]
	public void Align()
	{
		if (cam == null)
		{
			cam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		AlignObject();
	}

	public void AlignObject()
	{
		if (!base.enabled)
		{
			return;
		}
		onePercentFromVerticalEdge = cam.pixelHeight;
		onePercentFromVerticalEdge /= 100f;
		onePercentFromHorizontalEdge = cam.pixelWidth;
		onePercentFromHorizontalEdge /= 100f;
		if (alignLeft)
		{
			xPos = cam.ScreenToWorldPoint(new Vector3(onePercentFromVerticalEdge * percentFromEdge * xOffsetPercent, 1f, 1f)).x;
		}
		else
		{
			xPos = cam.ScreenToWorldPoint(new Vector3((float)cam.pixelWidth - onePercentFromVerticalEdge * percentFromEdge * xOffsetPercent, 1f, 1f)).x;
		}
		if (!alignTop)
		{
			if (alignLeftMid)
			{
				yPos = cam.ScreenToWorldPoint(new Vector3(1f, (float)Screen.height / 2f, 1f)).y;
			}
			else
			{
				yPos = cam.ScreenToWorldPoint(new Vector3(1f, onePercentFromVerticalEdge * percentFromEdge * yOffsetPercent, 1f)).y;
			}
			if (alignTopMid)
			{
				xPos = cam.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, 1f, 1f)).x;
			}
		}
		else
		{
			if (alignLeftMid)
			{
				yPos = cam.ScreenToWorldPoint(new Vector3(1f, (float)Screen.height / 2f, 1f)).y;
			}
			else
			{
				yPos = cam.ScreenToWorldPoint(new Vector3(1f, (float)cam.pixelHeight - onePercentFromVerticalEdge * percentFromEdge * yOffsetPercent, 1f)).y;
			}
			if (alignTopMid)
			{
				xPos = cam.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, 1f, 1f)).x;
			}
		}
		base.transform.position = new Vector3(xPos, yPos, base.transform.position.z);
	}
}
