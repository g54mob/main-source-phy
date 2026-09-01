using System;

public class CursorHoverHook : ClickBehaviour
{
	public Action onCursorEnter;

	public Action onCursorOver;

	public Action onCursorExit;

	public void OnMouseEnter()
	{
		if (onCursorEnter != null)
		{
			onCursorEnter();
		}
	}

	public void OnMouseExit()
	{
		if (onCursorExit != null)
		{
			onCursorExit();
		}
	}

	public override void OnCursorOver()
	{
		if (onCursorOver != null)
		{
			onCursorOver();
		}
	}
}
