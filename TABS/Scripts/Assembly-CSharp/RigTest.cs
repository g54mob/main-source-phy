using Landfall.TABS;
using Landfall.TABS.AI;
using UnityEngine;

public class RigTest : MonoBehaviour
{
	public Unit target;

	private void Start()
	{
		target.GetComponent<UnitAPI>().SetActive(active: false);
	}

	private void Update()
	{
		target.data.input.inputDirection = Vector3.forward;
		target.data.GetComponent<MovementHandler>().multiplier = 6f;
	}
}
