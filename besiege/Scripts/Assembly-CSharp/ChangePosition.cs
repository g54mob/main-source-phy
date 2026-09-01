using UnityEngine;

public class ChangePosition : MonoBehaviour
{
	public Transform targetPosition;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position = targetPosition.position;
	}
}
