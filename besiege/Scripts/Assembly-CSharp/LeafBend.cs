using UnityEngine;

public class LeafBend : MonoBehaviour
{
	public Transform[] leaves;

	public Vector3[] leafStartPos;

	public Transform myTransform;

	public float updateSpeed = 0.05f;

	public float pushPower = 10f;

	public float pushSize = 2f;

	private Rigidbody[] leavesRigidbody;

	private Vector3 distance;

	private Vector3 distanceToStart;

	private void Start()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("LeafBend");
		leaves = new Transform[array.Length];
		leavesRigidbody = new Rigidbody[array.Length];
		leafStartPos = new Vector3[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			leaves[i] = array[i].transform;
			leavesRigidbody[i] = array[i].GetComponent<Rigidbody>();
			leafStartPos[i] = array[i].transform.position;
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < leaves.Length; i++)
		{
			distance = leaves[i].position - myTransform.position;
			distanceToStart = leafStartPos[i] - leaves[i].position;
			if (distance.sqrMagnitude < pushSize)
			{
				leavesRigidbody[i].AddForce(distance * pushPower * (pushSize - distance.sqrMagnitude));
			}
			else
			{
				leavesRigidbody[i].AddForce(distanceToStart * pushPower * Random.Range(1f, 1.5f));
			}
		}
	}
}
