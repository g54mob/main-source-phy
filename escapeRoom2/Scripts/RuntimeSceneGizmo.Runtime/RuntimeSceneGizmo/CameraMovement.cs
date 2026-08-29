using UnityEngine;

namespace RuntimeSceneGizmo
{
	public class CameraMovement : MonoBehaviour
	{
		[SerializeField]
		private float sensitivity = 0.5f;

		private Vector3 prevMousePos;

		private Transform mainCamParent;

		private void Awake()
		{
			mainCamParent = Camera.main.transform.parent;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				prevMousePos = Input.mousePosition;
			}
			else if (Input.GetMouseButton(0))
			{
				Vector3 mousePosition = Input.mousePosition;
				Vector2 vector = (mousePosition - prevMousePos) * sensitivity;
				Vector3 localEulerAngles = mainCamParent.localEulerAngles;
				while (localEulerAngles.x > 180f)
				{
					localEulerAngles.x -= 360f;
				}
				while (localEulerAngles.x < -180f)
				{
					localEulerAngles.x += 360f;
				}
				localEulerAngles.x = Mathf.Clamp(localEulerAngles.x - vector.y, -89.8f, 89.8f);
				localEulerAngles.y += vector.x;
				localEulerAngles.z = 0f;
				mainCamParent.localEulerAngles = localEulerAngles;
				prevMousePos = mousePosition;
			}
		}
	}
}
