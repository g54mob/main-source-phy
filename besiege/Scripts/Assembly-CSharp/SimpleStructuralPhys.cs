using UnityEngine;

public class SimpleStructuralPhys : MonoBehaviour
{
	public int solverCount = 2;

	public Transform blocks;

	public Transform joints;

	public float breakForce = 50f;

	public float breakTorque = 50f;

	private void Start()
	{
		SetChildrenSolver();
	}

	private void SetChildrenSolver()
	{
		foreach (Object item in blocks.transform)
		{
			Transform transform = item as Transform;
			transform.GetComponent<Rigidbody>().solverIterations = solverCount;
		}
	}
}
