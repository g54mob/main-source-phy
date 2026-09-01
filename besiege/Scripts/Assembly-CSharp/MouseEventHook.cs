using System;

public class MouseEventHook : ClickBehaviour
{
	public Action onMouseDown;

	public Action onMouseDrag;

	public Action onMouseUp;

	public int mask = -1;

	public override void OnClicked()
	{
		if (UIMask.InsideMask(mask, base.transform.position) && onMouseDown != null)
		{
			onMouseDown();
		}
	}

	public override void OnClickDrag()
	{
		if (UIMask.InsideMask(mask, base.transform.position) && onMouseDrag != null)
		{
			onMouseDrag();
		}
	}

	public override void OnClickReleased()
	{
		if (UIMask.InsideMask(mask, base.transform.position) && onMouseUp != null)
		{
			onMouseUp();
		}
	}
}
