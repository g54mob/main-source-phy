using UnityEngine;

public class CameraSmoothing : MonoBehaviour
{
	private Vector3 previousState;

	private Vector3 currentState;

	private void Start()
	{
		currentState = base.transform.root.position;
	}

	private void LateUpdate()
	{
		float t = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
		Vector3 position = Vector3.Lerp(previousState, currentState, t);
		base.transform.position = position;
	}

	private void FixedUpdate()
	{
		previousState = currentState;
		currentState = base.transform.root.position;
	}

	public void JumpToCurrent()
	{
		base.transform.position = currentState;
	}
}
