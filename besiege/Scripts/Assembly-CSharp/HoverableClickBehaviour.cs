using UnityEngine;

public abstract class HoverableClickBehaviour : ClickBehaviour
{
	private static readonly RaycastHit[] hitBuffer = new RaycastHit[10];

	private Collider myCollider;

	private bool isCursorHovering;

	private Camera hudCamera;

	protected virtual void Awake()
	{
		myCollider = GetComponent<Collider>();
		if (myCollider == null)
		{
			Debug.LogWarning("[HoverableClickBehaviour] missing collider on: " + base.name, this);
			base.enabled = false;
		}
		hudCamera = GameObject.Find("HUD Cam").GetComponent<Camera>();
	}

	private void OnMouseEnter()
	{
		EnterMouseHoverState();
	}

	private void OnMouseExit()
	{
		if (isCursorHovering && !IsCursorHoveringCollider())
		{
			ExitHoverState();
		}
	}

	private void EnterMouseHoverState()
	{
		isCursorHovering = true;
		OnCursorEnter();
	}

	private void ExitHoverState()
	{
		isCursorHovering = false;
		OnCursorExit();
	}

	protected abstract void OnCursorEnter();

	protected abstract void OnCursorExit();

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (isCursorHovering)
		{
			CheckForCursorExit();
		}
	}

	private void CheckForCursorExit()
	{
		if (!IsCursorHoveringCollider())
		{
			ExitHoverState();
		}
	}

	private bool IsCursorHoveringCollider()
	{
		Vector2 vector = InputManager.CursorPosition();
		Ray ray = hudCamera.ScreenPointToRay(vector);
		int num = Physics.RaycastNonAlloc(ray, hitBuffer, float.PositiveInfinity, ReferenceMaster.Instance.hudMask, QueryTriggerInteraction.Ignore);
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				if (hitBuffer[i].collider.transform == base.transform)
				{
					return true;
				}
			}
		}
		return false;
	}
}
