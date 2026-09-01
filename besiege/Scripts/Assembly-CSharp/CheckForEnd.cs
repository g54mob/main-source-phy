using System.Collections;
using UnityEngine;

public class CheckForEnd : MonoBehaviour
{
	public Transform endPiece;

	public float rayLength = 1f;

	public LayerMask layerMasky;

	public bool hasChecked;

	public Renderer fullVis;

	public Renderer halfVis;

	public Transform instantiatey;

	public Transform jointPos;

	public Transform endTrigger;

	private BoxCollider boxCol;

	private Machine machine;

	private bool foundMachine;

	private IEnumerator Start()
	{
		machine = GetComponentInParent<Machine>();
		foundMachine = machine != null;
		yield return new WaitForFixedUpdate();
		if (foundMachine && !machine.isSimulating && !hasChecked)
		{
			hasChecked = true;
			RayCheck();
		}
	}

	private void RayCheck()
	{
		RaycastHit[] array = Physics.RaycastAll(base.transform.position, base.transform.forward, rayLength);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit raycastHit = array[i];
			if ((layerMasky.value & (1 << raycastHit.collider.gameObject.layer)) != 0 && raycastHit.collider != null && raycastHit.collider.transform.parent != base.transform.parent)
			{
				endTrigger.localPosition = new Vector3(endTrigger.localPosition.x, endTrigger.localPosition.y, 1f);
				fullVis.enabled = false;
				halfVis.enabled = true;
				break;
			}
		}
	}
}
