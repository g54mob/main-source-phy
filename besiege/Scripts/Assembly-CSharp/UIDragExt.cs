using UnityEngine;

[AddComponentMenu("UI/UI Drag Ext")]
public class UIDragExt : UIDrag
{
	public Transform boundsTransform;

	public override void OnClickDrag()
	{
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			if (isDragging)
			{
				OnClickReleased();
			}
			return;
		}
		isDragging = true;
		Vector3 position = InputManager.CursorPosition();
		position = hudCam.ScreenToWorldPoint(position);
		position.z = startPosZ;
		posToBe = position - difference;
		if ((bool)upperLeft && (bool)lowerRight)
		{
			posToBe = new Vector3(Mathf.Clamp(posToBe.x, upperLeft.position.x, lowerRight.position.x), Mathf.Clamp(posToBe.y, lowerRight.position.y + boundsTransform.lossyScale.y, upperLeft.position.y), posToBe.z);
		}
		myTransform.position = posToBe;
	}
}
