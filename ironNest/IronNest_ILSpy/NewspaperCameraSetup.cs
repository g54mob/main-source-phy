using UnityEngine;

public class NewspaperCameraSetup : MonoBehaviour
{
	private RTSMapCameraController cameraController;

	private Vector3 cameraLocalPosition;

	private unsafe void OnEnable()
	{
		//IL_0032: Expected O, but got Ref
		if (!(cameraController == null))
		{
			object obj = default(object);
			cameraController.CenterOnFocusPointLocal((Vector3)(&obj));
			return;
		}
		GameObject gameObject = base.gameObject;
		string text = gameObject.name;
		string message = "[NewspaperCameraSetup] No RTSMapCameraController assigned on '" + text + "'.";
		Debug.LogWarning(message, this);
	}
}
