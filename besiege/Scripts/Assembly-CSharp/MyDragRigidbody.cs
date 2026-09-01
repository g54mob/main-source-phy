using UnityEngine;

public class MyDragRigidbody : MonoBehaviour
{
	public float dragToBe;

	public float angularDragToBe;

	public SpringJoint mySpringJoint;

	public Ray ray;

	public RaycastHit hit;

	public bool joined;

	private float startDrag;

	private float startAngularDrag;

	private Camera mainCamera;

	private Transform myTransform;

	private void Start()
	{
		mainCamera = Camera.main;
		myTransform = base.transform;
	}

	private void Update()
	{
		if (InputManager.LeftMouseButton() && Physics.Raycast(mainCamera.ScreenPointToRay(InputManager.CursorPosition()), out hit, 500f))
		{
			myTransform.position = hit.point;
			mySpringJoint.connectedBody = hit.collider.attachedRigidbody;
		}
	}

	private void JoinToObject()
	{
		joined = true;
		mySpringJoint.connectedBody = hit.collider.GetComponent<Rigidbody>();
	}
}
