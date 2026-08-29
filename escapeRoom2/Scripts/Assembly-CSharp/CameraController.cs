using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform[] view;

	public float transitionSpeed;

	private Transform currentView;

	private void Start()
	{
		currentView = view[0];
	}

	private void LateUpdate()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, currentView.position, Time.deltaTime * transitionSpeed);
	}

	public void ZoomIn()
	{
		currentView = view[1];
	}

	public void ZoomOut()
	{
		currentView = view[0];
	}
}
