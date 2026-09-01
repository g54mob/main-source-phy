using UnityEngine;

public class EnemySiegeController : MonoBehaviour
{
	public bool isSimulating;

	public Transform enemyMachineParent;

	private GameObject simulateMachine;

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			if (!isSimulating)
			{
				Simulate();
			}
		}
		else if (isSimulating)
		{
			UnSimulate();
		}
	}

	private void Simulate()
	{
		isSimulating = true;
		simulateMachine = Object.Instantiate(enemyMachineParent.gameObject, enemyMachineParent.position, enemyMachineParent.rotation) as GameObject;
		enemyMachineParent.gameObject.SetActive(false);
		for (int i = 0; i < simulateMachine.transform.childCount; i++)
		{
			simulateMachine.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	private void UnSimulate()
	{
		isSimulating = false;
		Object.Destroy(simulateMachine.gameObject);
		enemyMachineParent.gameObject.SetActive(true);
	}
}
