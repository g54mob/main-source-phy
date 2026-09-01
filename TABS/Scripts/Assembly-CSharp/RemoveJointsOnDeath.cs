using UnityEngine;

public class RemoveJointsOnDeath : MonoBehaviour
{
	private Joint[] joints;

	private HealthHandler healthHandler;

	private void Start()
	{
		joints = GetComponentsInChildren<Joint>();
		healthHandler = base.transform.root.GetComponentInChildren<HealthHandler>();
		healthHandler.AddDieAction(Die);
	}

	public void Die()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			Object.Destroy(joints[i]);
		}
		Object.Destroy(this);
	}
}
