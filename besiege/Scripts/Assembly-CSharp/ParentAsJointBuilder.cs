using UnityEngine;

public class ParentAsJointBuilder : MonoBehaviour
{
	public static bool isSimulating;

	public static Vector3 mouseHitPos;

	public static Vector3 mouseHitNormal;

	public Camera mainCam;

	public Transform machineParent;

	public int blockCount;

	public Transform block;

	private RaycastHit hit;

	private Ray ray;

	private bool canAdd;

	private Transform activeInstance;

	private void Update()
	{
		ray = mainCam.ScreenPointToRay(new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 0f));
		canAdd = Physics.Raycast(ray, out hit, 100f);
		if (InputManager.LeftMouseButton() && canAdd)
		{
			AddBlock();
		}
		if (InputManager.ToggleSimulationKey())
		{
			Simulate();
		}
	}

	private void AddBlock()
	{
		mouseHitPos = hit.collider.transform.position + hit.normal;
		mouseHitNormal = hit.normal;
		Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, mouseHitNormal) * Quaternion.Euler(new Vector3(0f, 0f, 90f));
		Transform transform = (Object.Instantiate(block, mouseHitPos, rotation) as GameObject).transform;
		transform.parent = machineParent;
		machineParent.GetComponent<Rigidbody>().mass += 1f;
		transform.name = string.Empty + machineParent.GetComponent<Rigidbody>().mass;
	}

	private void Simulate()
	{
		if (!isSimulating)
		{
			isSimulating = true;
			Transform transform = (Object.Instantiate(machineParent.gameObject, machineParent.position, machineParent.rotation) as GameObject).transform;
			machineParent.gameObject.SetActive(false);
			activeInstance = transform;
			activeInstance.GetComponent<Rigidbody>().isKinematic = false;
		}
		else
		{
			isSimulating = false;
			Object.Destroy(activeInstance.gameObject);
			machineParent.gameObject.SetActive(true);
		}
	}
}
