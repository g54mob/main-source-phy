using System;
using UnityEngine;

[AddComponentMenu("UI/Simple UI Button")]
public class SimpleUIButton : ClickBehaviour
{
	public Action HoverChanged;

	protected bool clickedOnMe;

	[HideInInspector]
	public bool IsHovered;

	protected float heldTime;

	protected Collider buttonCollider;

	public event Click Click;

	public event Down Down;

	public event Held Held;

	public event Released Released;

	public event MouseEnter MouseEnter;

	public event MouseExit MouseExit;

	protected virtual void Awake()
	{
		buttonCollider = GetComponent<Collider>();
	}

	public void ToggleButton(bool toggle)
	{
		if (!(buttonCollider == null))
		{
			buttonCollider.enabled = toggle;
			base.enabled = toggle;
		}
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (!clickedOnMe)
		{
			return;
		}
		if (InputManager.LeftMouseButtonHeld())
		{
			_InvokeOnHeld();
		}
		else if (InputManager.LeftMouseButtonReleased())
		{
			if (IsHovered && heldTime < 0.25f)
			{
				_InvokeOnClick();
			}
			_InvokeOnReleased();
			clickedOnMe = false;
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (clickedOnMe)
		{
			_InvokeOnReleased();
		}
		IsHovered = false;
		clickedOnMe = false;
	}

	public void OnMouseEnter()
	{
		_InvokeOnMouseEnter();
	}

	public void OnMouseExit()
	{
		_InvokeOnMouseExit();
	}

	public override void OnClicked()
	{
		_InvokeOnDown();
		IsHovered = base.gameObject.activeInHierarchy;
		clickedOnMe = true;
	}

	protected virtual bool _InvokeOnMouseEnter()
	{
		IsHovered = true;
		MouseEnter mouseEnter = this.MouseEnter;
		if (mouseEnter != null)
		{
			mouseEnter();
		}
		if (HoverChanged != null)
		{
			HoverChanged();
		}
		return true;
	}

	protected virtual bool _InvokeOnMouseExit()
	{
		IsHovered = false;
		MouseExit mouseExit = this.MouseExit;
		if (mouseExit != null)
		{
			mouseExit();
		}
		if (HoverChanged != null)
		{
			HoverChanged();
		}
		return true;
	}

	protected virtual bool _InvokeOnClick()
	{
		Click click = this.Click;
		if (click != null)
		{
			click();
		}
		return true;
	}

	protected virtual bool _InvokeOnDown()
	{
		heldTime = 0f;
		Down down = this.Down;
		if (down != null)
		{
			down();
		}
		return true;
	}

	protected virtual bool _InvokeOnHeld()
	{
		heldTime += Time.unscaledDeltaTime;
		Held held = this.Held;
		if (held != null)
		{
			held();
		}
		return true;
	}

	protected virtual bool _InvokeOnReleased()
	{
		heldTime = 0f;
		Released released = this.Released;
		if (released != null)
		{
			released();
		}
		return true;
	}

	public virtual void ResetDelegates()
	{
		this.Click = null;
		this.Held = null;
		this.Down = null;
		this.Released = null;
		this.MouseEnter = null;
		this.MouseExit = null;
	}
}
