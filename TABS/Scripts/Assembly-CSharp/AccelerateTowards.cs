using UnityEngine;

public class AccelerateTowards : MonoBehaviour
{
	public Transform target;

	public float amount;

	private MoveTransform move;

	private void Start()
	{
		move = GetComponent<MoveTransform>();
	}

	private void Update()
	{
		move.velocity += (target.position - base.transform.position) * amount * Time.deltaTime;
	}
}
