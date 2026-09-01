using UnityEngine;

public class RotateObjNoParticles : SimBehaviour
{
	public Vector3 speed = new Vector3(10f, 15f, 19f);

	public bool spinInEditMode = true;

	private Transform myTransform;

	protected override void Start()
	{
		base.Start();
		myTransform = base.transform;
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			myTransform.Rotate(speed * Time.deltaTime);
		}
	}
}
