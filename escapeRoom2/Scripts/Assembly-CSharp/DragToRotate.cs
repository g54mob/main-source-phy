using UnityEngine;

public class DragToRotate : MonoBehaviour
{
	private const float Sensitivity = 0.4f;

	private Vector3 _recentMousePosition;

	private Vector3 _mouseOffset;

	private Vector3 _rotation = Vector3.zero;

	private bool _isRotating;

	private void Update()
	{
		if (_isRotating)
		{
			_mouseOffset = Input.mousePosition - _recentMousePosition;
			_rotation.y = (0f - (_mouseOffset.x + _mouseOffset.y)) * 0.4f;
			base.transform.Rotate(_rotation);
			_recentMousePosition = Input.mousePosition;
		}
	}

	private void OnMouseDown()
	{
		_isRotating = true;
		_recentMousePosition = Input.mousePosition;
	}

	private void OnMouseUp()
	{
		_isRotating = false;
	}
}
