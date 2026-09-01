using UnityEngine;

public class LocalisationUploadButton : MonoBehaviour
{
	public Camera uiCam;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = uiCam.ScreenPointToRay(InputManager.CursorPosition());
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 100f) && hitInfo.collider.gameObject.name == "UploadLanguageButton")
			{
				UploadCurrentSkin();
			}
		}
	}

	public void UploadCurrentSkin()
	{
	}
}
