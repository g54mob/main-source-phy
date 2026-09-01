using UnityEngine;
using UnityEngine.EventSystems;

namespace Poly.Demo.UI
{
	[RequireComponent(typeof(Camera))]
	public class CameraPanAndZoom : MonoBehaviour
	{
		public float scrollWheelSpeedScale = -1f;

		public float minCameraDistance = 0.5f;

		private Plane touchPlane = new Plane(Vector3.back, 0f);

		private bool isMouseDown;

		private bool canDrag;

		private Vector3 initialTouchPoint = invalidVec;

		private Camera camera;

		private const string MouseScrollWheelAxis = "Mouse ScrollWheel";

		private static Vector3 invalidVec = new Vector3(float.MinValue, 0f, 0f);

		private void Awake()
		{
			camera = GetComponent<Camera>();
			minCameraDistance = Mathf.Max(minCameraDistance, 1.1f * camera.nearClipPlane);
		}

		private void Update()
		{
			float num = Input.GetAxis("Mouse ScrollWheel") * scrollWheelSpeedScale;
			Vector3 vector = CalcMouseHitPoint();
			if (num != 0f && vector != invalidVec)
			{
				Vector3 vector2 = camera.transform.position - vector;
				float magnitude = vector2.magnitude;
				float num2 = Mathf.Max(minCameraDistance, magnitude * (1f + num));
				camera.transform.position = vector + vector2 * num2 / magnitude;
				if (camera.orthographic)
				{
					camera.orthographicSize *= 1f + num;
				}
			}
			bool flag = Input.GetMouseButton(0) || Input.GetMouseButton(1);
			if (flag ^ isMouseDown)
			{
				if (flag)
				{
					if (!EventSystem.current.IsPointerOverGameObject())
					{
						OnMouseDown_Manual(vector);
						canDrag = true;
					}
				}
				else
				{
					OnMouseUp_Manual();
					canDrag = false;
				}
				isMouseDown = flag;
			}
			if (isMouseDown && canDrag)
			{
				OnMouseDrag_Manual(vector);
			}
		}

		private void OnMouseDown_Manual(Vector3 touchPoint)
		{
			initialTouchPoint = touchPoint;
		}

		private void OnMouseUp_Manual()
		{
			initialTouchPoint = invalidVec;
		}

		private void OnMouseDrag_Manual(Vector3 touchPoint)
		{
			if (initialTouchPoint != invalidVec && touchPoint != invalidVec)
			{
				camera.transform.position -= touchPoint - initialTouchPoint;
			}
		}

		private Vector3 CalcMouseHitPoint()
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			if (!touchPlane.Raycast(ray, out var enter))
			{
				return invalidVec;
			}
			return ray.GetPoint(enter);
		}
	}
}
