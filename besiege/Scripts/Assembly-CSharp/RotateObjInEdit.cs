using UnityEngine;

public class RotateObjInEdit : SimBehaviour
{
	public Vector3 speed = new Vector3(10f, 15f, 19f);

	private Transform myTransform;

	protected override void Start()
	{
		base.Start();
		myTransform = base.transform;
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			myTransform.Rotate(speed * Time.deltaTime);
		}
	}
}
