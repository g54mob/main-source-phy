using UnityEngine;

[AddComponentMenu("Destruction/Break On Force No Scaling")]
public class BreakOnForceNoScaling : BreakOnForce
{
	public bool sleepOnEnable = true;

	public bool rotateSpawnedObject = true;

	[HideInInspector]
	public bool usingBreakableSkin = true;

	protected override void Start()
	{
		Init();
		if (sleepOnEnable && (bool)myBody)
		{
			myBody.Sleep();
		}
	}

	protected override void OnEnable()
	{
		if (sleepOnEnable && (bool)myBody)
		{
			myBody.Sleep();
		}
		if ((bool)colHook)
		{
			colHook.CollisionHappend += OnCollisionEnter;
			colHook.ExplosionHappend += base.OnExplode;
		}
	}

	protected override void SetParent(Transform breakObj)
	{
		breakObj.SetParent(ReferenceMaster.physicsGoalInstance, true);
	}

	protected override Quaternion GetBreakRotation()
	{
		return rotateSpawnedObject ? base.transform.rotation : Quaternion.LookRotation(Vector3.forward, Vector3.up);
	}

	protected override void DestroyObjects()
	{
	}

	private void SkinIsDefault(bool b)
	{
		usingBreakableSkin = b;
	}
}
