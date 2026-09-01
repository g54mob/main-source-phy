using UnityEngine;

[AddComponentMenu("UI/UI Button")]
public class UIButton : SimpleUIButton
{
	public int mask = -1;

	protected override bool _InvokeOnClick()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			return false;
		}
		return base._InvokeOnClick();
	}

	protected override bool _InvokeOnDown()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			return false;
		}
		return base._InvokeOnDown();
	}

	protected override bool _InvokeOnHeld()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			_InvokeOnReleased();
			return false;
		}
		return base._InvokeOnHeld();
	}

	protected override bool _InvokeOnMouseEnter()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			IsHovered = false;
			return false;
		}
		return base._InvokeOnMouseEnter();
	}

	protected override bool _InvokeOnMouseExit()
	{
		IsHovered = false;
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			return false;
		}
		return base._InvokeOnMouseExit();
	}

	protected override bool _InvokeOnReleased()
	{
		return base._InvokeOnReleased();
	}
}
