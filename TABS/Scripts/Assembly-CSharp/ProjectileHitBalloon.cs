using System.Collections;
using UnityEngine;

public class ProjectileHitBalloon : ProjectileHitEffect
{
	public GameObject objectToSpawn;

	private GameObject spawnedObject;

	private DataHandler targetData;

	private ConfigurableJoint joint;

	private LineRenderer line;

	private bool hadRigidbodyTarget;

	private Level level;

	public string balloonOutSFX;

	private void Start()
	{
		level = GetComponent<Level>();
	}

	private void Update()
	{
		if (hadRigidbodyTarget && (bool)joint && !joint.connectedBody && (bool)spawnedObject)
		{
			Object.Destroy(spawnedObject);
			spawnedObject = null;
		}
	}

	private void OnDestroy()
	{
		if ((bool)spawnedObject)
		{
			Object.Destroy(spawnedObject);
			spawnedObject = null;
		}
	}

	public override bool DoEffect(HitData hit)
	{
		Rigidbody rigidbody = hit.rigidbody;
		SurfaceAddForceToTarget component = hit.transform.gameObject.GetComponent<SurfaceAddForceToTarget>();
		if ((bool)component)
		{
			rigidbody = component.dataFinder.data.mainRig;
			hit.point = component.dataFinder.data.mainRig.position;
			base.transform.position = component.dataFinder.data.mainRig.position;
		}
		spawnedObject = Object.Instantiate(objectToSpawn, base.transform.position, Quaternion.identity);
		TeamHolder teamHolder = spawnedObject.AddComponent<TeamHolder>();
		TeamHolder component2 = GetComponent<TeamHolder>();
		if ((bool)teamHolder && (bool)component2)
		{
			teamHolder.team = component2.team;
			teamHolder.spawner = component2.spawner;
			teamHolder.target = component2.target;
		}
		if ((bool)level)
		{
			float num = Mathf.Pow(level.levelMultiplier, 2f);
			spawnedObject.GetComponentInChildren<Rigidbody>().mass *= num;
			spawnedObject.GetComponentInChildren<ConstantForce>().force *= num;
		}
		joint = spawnedObject.GetComponentInChildren<ConfigurableJoint>();
		line = spawnedObject.GetComponentInChildren<LineRenderer>();
		if ((bool)rigidbody)
		{
			targetData = hit.transform.GetComponentInParent<DataHandler>();
			if ((bool)joint)
			{
				joint.connectedBody = rigidbody;
			}
		}
		StartCoroutine(BalloonEffect());
		if ((bool)joint && (bool)joint.connectedBody)
		{
			hadRigidbodyTarget = true;
		}
		return false;
	}

	private IEnumerator BalloonEffect()
	{
		float t = 0f;
		while (t < 5f)
		{
			if ((bool)targetData)
			{
				targetData.sinceGrounded = 0f;
			}
			if ((bool)joint && (bool)joint.connectedBody)
			{
				line.SetPosition(0, joint.gameObject.transform.position);
				line.SetPosition(1, joint.connectedBody.transform.position);
			}
			else if ((bool)spawnedObject)
			{
				line.SetPosition(0, spawnedObject.transform.position);
				line.SetPosition(1, spawnedObject.transform.GetChild(0).position);
			}
			t += Time.deltaTime;
			yield return null;
		}
		if (spawnedObject != null)
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(balloonOutSFX, 1f, base.transform.position, SoundEffectVariations.MaterialType.Default, spawnedObject.transform);
		}
		if ((bool)joint)
		{
			Object.Destroy(joint);
		}
		Object.Destroy(line);
	}
}
