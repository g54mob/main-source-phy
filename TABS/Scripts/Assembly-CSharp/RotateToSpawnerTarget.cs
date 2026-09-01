using UnityEngine;

public class RotateToSpawnerTarget : MonoBehaviour
{
	private Vector3 target;

	private TeamHolder teamHolder;

	private DataHandler data;

	private void Start()
	{
		teamHolder = GetComponent<TeamHolder>();
		if ((bool)teamHolder)
		{
			data = teamHolder.spawner.GetComponentInChildren<DataHandler>();
		}
		if (!data)
		{
			data = base.transform.root.GetComponentInChildren<DataHandler>();
		}
	}

	private void Update()
	{
		if ((bool)data && (bool)data.targetData)
		{
			base.transform.LookAt(data.targetData.mainRig.transform.position);
		}
		base.transform.rotation = Quaternion.LookRotation(new Vector3(base.transform.forward.x, 0f, base.transform.forward.z));
	}
}
