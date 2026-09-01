using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public bool hasControl;

	public Vector3 direction;

	public Vector3 normalizedDirection;

	public bool spaceWasPressed;

	public bool spaceWasReleased;

	public bool spaceIsDown;

	public bool mouse0WasPressed;

	public bool mouse0WasReleased;

	public bool mouse0IsPressed;

	public bool mouse1WasPressed;

	public bool mouse1WasReleased;

	public bool mouse1IsPressed;

	public bool shiftIsDown;

	public bool altIsDown;

	public bool ctrlIsDown;

	public bool ctrlWasPressed;

	public bool ctrlWasReleased;

	public bool SpecialIsDown;

	public bool SpecialWasPressed;

	public bool SpecialWasReleased;

	private void Update()
	{
		if (hasControl)
		{
			direction = Vector3.zero;
			mouse0WasPressed = Input.GetKeyDown(KeyCode.Mouse0);
			mouse0IsPressed = Input.GetKey(KeyCode.Mouse0);
			mouse0WasReleased = Input.GetKeyUp(KeyCode.Mouse0);
			mouse1WasPressed = Input.GetKeyDown(KeyCode.Mouse1);
			mouse1IsPressed = Input.GetKey(KeyCode.Mouse1);
			mouse1WasReleased = Input.GetKeyUp(KeyCode.Mouse1);
			shiftIsDown = Input.GetKey(KeyCode.LeftShift);
			ctrlIsDown = Input.GetKey(KeyCode.LeftControl);
			ctrlWasPressed = Input.GetKeyDown(KeyCode.LeftControl);
			ctrlWasReleased = Input.GetKeyUp(KeyCode.LeftControl);
			altIsDown = Input.GetKey(KeyCode.LeftAlt);
			spaceWasPressed = Input.GetKeyDown(KeyCode.Space);
			spaceWasReleased = Input.GetKeyUp(KeyCode.Space);
			spaceIsDown = Input.GetKey(KeyCode.Space);
			SpecialIsDown = Input.GetKey(KeyCode.E);
			SpecialWasPressed = Input.GetKeyDown(KeyCode.E);
			SpecialWasReleased = Input.GetKeyUp(KeyCode.E);
			if (Input.GetKey(KeyCode.A))
			{
				direction += Vector3.left;
			}
			if (Input.GetKey(KeyCode.D))
			{
				direction += Vector3.right;
			}
			if (Input.GetKey(KeyCode.W))
			{
				direction += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.S))
			{
				direction += Vector3.back;
			}
			normalizedDirection = direction.normalized;
		}
	}
}
