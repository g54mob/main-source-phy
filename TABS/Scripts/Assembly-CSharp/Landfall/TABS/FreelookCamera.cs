using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	public class FreelookCamera : MonoBehaviour
	{
		public bool inFreelook;

		private Camera cam;

		private MouseLook mouseLook;

		private CameraMovement cameraMovement;

		private CursorVisibilityController cursorVisibility;

		private void Awake()
		{
			cam = GetComponentInChildren<Camera>();
			mouseLook = GetComponentInChildren<MouseLook>();
			cameraMovement = GetComponentInChildren<CameraMovement>();
			cursorVisibility = ServiceLocator.GetService<CursorVisibilityController>();
			if (inFreelook)
			{
				cursorVisibility.SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
				mouseLook.enabled = true;
			}
			else
			{
				cursorVisibility.SetLockStateAndVisibility(CursorLockMode.None, visible: true);
				mouseLook.enabled = false;
			}
		}

		public void StartFreelook()
		{
			inFreelook = true;
			cursorVisibility.SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
			mouseLook.enabled = true;
		}

		public void EndFreeLook(Vector3 pos, Quaternion rotation)
		{
			inFreelook = false;
			cursorVisibility.SetLockStateAndVisibility(CursorLockMode.None, visible: true);
			mouseLook.enabled = false;
		}
	}
}
