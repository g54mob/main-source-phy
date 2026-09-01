using UnityEngine;

public class LECollapseToggle : MonoBehaviour
{
	public enum CE
	{
		Collapse = 0,
		Expand = 1
	}

	public CE behaviour;

	protected bool clickedOnMe;

	protected bool isMouseOver;

	public void Update()
	{
		if (InputManager.LeftMouseButton() && isMouseOver)
		{
			clickedOnMe = true;
		}
		if (InputManager.LeftMouseButtonReleased() && clickedOnMe)
		{
			if (isMouseOver)
			{
				_InvokeOnClick();
			}
			clickedOnMe = false;
		}
	}

	public void OnMouseEnter()
	{
		isMouseOver = true;
	}

	public void OnMouseExit()
	{
		isMouseOver = false;
	}

	public void Disable()
	{
		isMouseOver = false;
		clickedOnMe = false;
	}

	protected void _InvokeOnClick()
	{
		switch (behaviour)
		{
		case CE.Collapse:
			SingleInstanceFindOnly<LevelEditorUI>.Instance.InduceCollapse();
			break;
		case CE.Expand:
			SingleInstanceFindOnly<LevelEditorUI>.Instance.InduceExpansion();
			break;
		}
	}
}
