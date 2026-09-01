using UnityEngine;

public class NailRayGhost : MonoBehaviour
{
	public RaycastHit hit;

	public float rayLength = 1f;

	public LayerMask layerMasky;

	public Transform nailTop;

	public Transform nailRight;

	public Transform nailBottom;

	public Transform nailLeft;

	public Transform myParent;

	public ConfigurableJoint myJoint;

	public Transform jointTopTarget;

	public Transform jointRightTarget;

	public Transform jointBottomTarget;

	public Transform jointLeftTarget;

	public bool hasJoined;

	private void Update()
	{
		RayCheck();
	}

	private void RayCheck()
	{
		if (Physics.Raycast(nailTop.position, -nailTop.forward, out hit, rayLength, layerMasky))
		{
			jointTopTarget = hit.collider.transform;
			nailTop.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			jointTopTarget = null;
			nailTop.GetComponent<Renderer>().enabled = false;
		}
		if (Physics.Raycast(nailRight.position, -nailRight.forward, out hit, rayLength, layerMasky))
		{
			jointRightTarget = hit.collider.transform;
			nailRight.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			jointRightTarget = null;
			nailRight.GetComponent<Renderer>().enabled = false;
		}
		if (Physics.Raycast(nailBottom.position, -nailBottom.forward, out hit, rayLength, layerMasky))
		{
			jointBottomTarget = hit.collider.transform;
			nailBottom.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			jointBottomTarget = null;
			nailBottom.GetComponent<Renderer>().enabled = false;
		}
		if (Physics.Raycast(nailLeft.position, -nailLeft.forward, out hit, rayLength, layerMasky))
		{
			jointLeftTarget = hit.collider.transform;
			nailLeft.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			jointLeftTarget = null;
			nailLeft.GetComponent<Renderer>().enabled = false;
		}
	}
}
