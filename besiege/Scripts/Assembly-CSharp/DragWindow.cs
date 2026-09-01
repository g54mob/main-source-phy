using UnityEngine;

public class DragWindow : ClickBehaviour
{
	public Camera hudCam;

	public Transform myTransform;

	private float startPosZ;

	private Vector3 posToBe;

	private Vector3 difference;

	private void Start()
	{
		if (hudCam == null)
		{
			hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		startPosZ = myTransform.position.z;
	}

	public override void OnClicked()
	{
		if (UIMask.InsideMask(-1, base.transform.position))
		{
			Vector3 vector = hudCam.ScreenToWorldPoint(InputManager.CursorPosition());
			posToBe = new Vector3(vector.x, vector.y, startPosZ);
			difference = myTransform.position - posToBe;
		}
	}

	public override void OnClickDrag()
	{
		if (UIMask.InsideMask(-1, base.transform.position))
		{
			Vector3 vector = hudCam.ScreenToWorldPoint(InputManager.CursorPosition());
			posToBe = new Vector3(vector.x, vector.y, startPosZ);
			myTransform.position = posToBe + difference;
		}
	}
}
