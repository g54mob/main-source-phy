using UnityEngine;

public class GhostPinTrigger : GhostTrigger
{
	private void Update()
	{
		if (!GhostTrigger.isTouching && touchingCount > 0)
		{
			Check();
		}
		if (!hasMaterialCode)
		{
			return;
		}
		if (!StatMaster.Mode.allowIntersection && (touchingCount > 0 || SingleInstanceFindOnly<AddPiece>.Instance.OutOfBounds))
		{
			materialCode.SetRed();
			if (InputManager.LeftMouseButton())
			{
				IntersectWarning.WarningFromWorldPos(base.transform.position);
			}
		}
		else
		{
			materialCode.SetNormal();
		}
	}

	private void OnDisable()
	{
		touchingCount = 0;
		Check();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == overlayBlockLayer)
		{
			touchingCount++;
		}
		Check();
	}

	private void OnTriggerStay(Collider other)
	{
		if (touchingCount <= 0 && other.gameObject.layer == overlayBlockLayer)
		{
			touchingCount++;
			Check();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == overlayBlockLayer)
		{
			touchingCount--;
		}
		Check();
	}
}
