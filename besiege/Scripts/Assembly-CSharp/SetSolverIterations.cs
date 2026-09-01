using UnityEngine;

public class SetSolverIterations : MonoBehaviour
{
	public SolveBlockType blockType;

	private Rigidbody rigidbodyReference;

	private void Start()
	{
		Machine componentInParent = GetComponentInParent<Machine>();
		if (!componentInParent.SimPhysics)
		{
			return;
		}
		rigidbodyReference = GetComponent<Rigidbody>();
		if (rigidbodyReference == null)
		{
			Debug.LogWarning("No rigidbody attached!");
			return;
		}
		switch (blockType)
		{
		case SolveBlockType.Wood:
			rigidbodyReference.solverIterations = 30;
			break;
		case SolveBlockType.Panel:
			rigidbodyReference.solverIterations = 10;
			break;
		}
	}
}
