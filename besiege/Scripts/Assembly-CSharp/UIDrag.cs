using UnityEngine;

[AddComponentMenu("UI/UI Drag")]
public class UIDrag : ClickBehaviour
{
	public Camera hudCam;

	public Transform myTransform;

	public int mask = -1;

	public Transform upperLeft;

	public Transform lowerRight;

	protected float startPosZ;

	protected Vector3 posToBe;

	protected Vector3 difference;

	[HideInInspector]
	public bool isDragging;

	public event DragEnded DragEnded;

	protected virtual void Start()
	{
		if (hudCam == null)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("hudCamera");
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] == null))
				{
					if (array[i].name.Contains("(Clone)"))
					{
						Object.Destroy(array[i]);
					}
					else if (hudCam == null)
					{
						hudCam = array[i].GetComponent<Camera>();
					}
				}
			}
		}
		if ((bool)myTransform)
		{
			startPosZ = myTransform.position.z;
		}
		if (!upperLeft)
		{
			upperLeft = GameObject.FindWithTag("upperLeft").transform;
		}
		if (!lowerRight)
		{
			lowerRight = GameObject.FindWithTag("lowerRight").transform;
		}
	}

	public override void OnClicked()
	{
		if (UIMask.InsideMask(mask, base.transform.position))
		{
			Vector3 position = InputManager.CursorPosition();
			position = hudCam.ScreenToWorldPoint(position);
			position.z = startPosZ;
			difference = position - myTransform.position;
		}
	}

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
			posToBe = new Vector3(Mathf.Clamp(posToBe.x, upperLeft.position.x, lowerRight.position.x), Mathf.Clamp(posToBe.y, lowerRight.position.y + myTransform.lossyScale.y / 2f, upperLeft.position.y), posToBe.z);
		}
		myTransform.position = posToBe;
	}

	public override void OnClickReleased()
	{
		if (isDragging)
		{
			_InvokeOnDragEnded();
		}
		isDragging = false;
	}

	protected virtual void _InvokeOnDragEnded()
	{
		DragEnded dragEnded = this.DragEnded;
		if (dragEnded != null)
		{
			dragEnded();
		}
	}
}
