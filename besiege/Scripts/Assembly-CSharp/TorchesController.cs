using UnityEngine;

public class TorchesController : MonoBehaviour
{
	public FireController fireController;

	public float timer;

	public float timeLimit = 5f;

	private int pointCount;

	private void Start()
	{
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && (double)fireController.fireProgress > 0.0005)
		{
			CheckIfOnFire(fireController);
		}
	}

	private void Doused()
	{
		WinCondition.currentObjsCompleted++;
		timer = 0f;
		fireController.onFire = false;
	}

	private void TorchRelight(FireTag fireTag)
	{
		fireTag.Ignite(1f);
	}

	private void CheckIfOnFire(FireController controller)
	{
		if (!controller.onFire)
		{
			timer += Time.deltaTime;
			if (timer >= timeLimit)
			{
				TorchRelight(controller.gameObject.transform.parent.GetComponent<FireTag>());
				WinCondition.currentObjsCompleted--;
				pointCount--;
				timer = 0f;
			}
		}
	}
}
