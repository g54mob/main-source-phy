using System;
using System.Collections;
using UnityEngine;

public class ClickBehaviour : MonoBehaviour
{
	public Action OnActivation;

	protected bool releaseOnlyOver;

	protected Vector2 cursorPos;

	protected bool _wasClicked;

	protected bool checkDown = true;

	protected bool checkHeld = true;

	protected bool checkDrag = true;

	protected bool checkUp = true;

	private void OnMouseOver()
	{
		OnCursorOver();
	}

	public virtual void OnCursorOver()
	{
		if (!base.enabled)
		{
			return;
		}
		if ((checkDown || checkHeld || checkDrag || checkUp) && InputManager.LeftMouseButton())
		{
			_wasClicked = true;
			cursorPos = InputManager.CursorPosition();
			if (checkDown)
			{
				OnClicked();
				if (OnActivation != null)
				{
					OnActivation();
				}
			}
		}
		if (_wasClicked && releaseOnlyOver && (checkDown || checkHeld || checkDrag || checkUp) && InputManager.LeftMouseButtonReleased())
		{
			_wasClicked = false;
			if (checkUp)
			{
				OnClickReleased();
			}
		}
	}

	protected virtual void LateUpdate()
	{
		if (!_wasClicked)
		{
			return;
		}
		if ((checkHeld || checkDrag) && InputManager.LeftMouseButtonHeld())
		{
			OnClickHeld();
			if (InputManager.CursorMoved(cursorPos))
			{
				OnClickDrag();
			}
		}
		if (!releaseOnlyOver && (checkDown || checkHeld || checkDrag || checkUp) && InputManager.LeftMouseButtonReleased())
		{
			_wasClicked = false;
			if (checkUp)
			{
				OnClickReleased();
			}
		}
	}

	public virtual void OnClicked()
	{
		checkDown = false;
	}

	public virtual void OnClickHeld()
	{
		checkHeld = false;
	}

	public virtual void OnClickDrag()
	{
		checkDrag = false;
	}

	public virtual void OnClickReleased()
	{
		checkUp = false;
	}

	public virtual void OnDisable()
	{
		if (_wasClicked && ReferenceMaster.Instance != null)
		{
			ReferenceMaster.Instance.StartCoroutine(ResetClick());
		}
	}

	protected IEnumerator ResetClick()
	{
		yield return new WaitForEndOfFrame();
		_wasClicked = false;
	}
}
