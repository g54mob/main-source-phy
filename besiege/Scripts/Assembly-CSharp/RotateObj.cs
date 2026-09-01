using UnityEngine;

public class RotateObj : SimBehaviour
{
	public Vector3 speed = new Vector3(10f, 15f, 19f);

	public bool spinInEditMode = true;

	private ParticleSystem particleSys;

	private Transform myTransform;

	protected override void Start()
	{
		base.Start();
		particleSys = GetComponent<ParticleSystem>();
		myTransform = base.transform;
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && !object.ReferenceEquals(particleSys, null) && particleSys.isPlaying)
		{
			myTransform.Rotate(speed * Time.deltaTime);
		}
	}
}
