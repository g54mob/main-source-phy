using System;

public class TimeSliderObject : ClickBehaviour
{
	public Action onClicked;

	public Action onClickHeld;

	public Action onClickReleased;

	public Action<float> onScroll;

	private bool stoppedZoom;

	public override void OnCursorOver()
	{
		base.OnCursorOver();
		if (!stoppedZoom)
		{
			stoppedZoom = true;
			StatMaster.DisableCameraZoom(true);
		}
		float num = InputManager.ZoomValue();
		if (num > 0.02f)
		{
			if (onScroll != null)
			{
				onScroll(0.01f);
			}
		}
		else if (num < -0.02f && onScroll != null)
		{
			onScroll(-0.01f);
		}
	}

	private void OnMouseExit()
	{
		if (stoppedZoom)
		{
			stoppedZoom = false;
			StatMaster.DisableCameraZoom(false);
		}
	}

	public override void OnClicked()
	{
		if (onClicked != null)
		{
			onClicked();
		}
	}

	public override void OnClickHeld()
	{
		if (onClickHeld != null)
		{
			onClickHeld();
		}
	}

	public override void OnClickReleased()
	{
		if (onClickReleased != null)
		{
			onClickReleased();
		}
	}
}
