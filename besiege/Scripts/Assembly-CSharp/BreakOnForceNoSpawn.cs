using UnityEngine;

[AddComponentMenu("Destruction/Break On Force No Spawn")]
public class BreakOnForceNoSpawn : BreakOnForce
{
	public ParticleSystem[] particleSystems;

	public Transform[] sendBreakMessage;

	public bool physGoalParent;

	protected AudioSource audioSource;

	protected override void Start()
	{
		usePhysicsGoalAsParent = physGoalParent;
		base.Start();
		audioSource = GetComponent<AudioSource>();
	}

	public override void Break()
	{
	}

	public override void BreakExplosion(float powery, Vector3 positiony, float radiusy, float upAmount)
	{
		if (CanDie && base.enabled)
		{
			CanDie = false;
			if (myBody == null)
			{
				myBody = GetComponent<Rigidbody>();
			}
			if (myBody != null)
			{
				myBody.isKinematic = false;
				myBody.WakeUp();
				myBody.AddExplosionForce(powery, positiony, radiusy, 0f);
			}
			ExplodeSupports(powery, positiony, radiusy, 0f);
			if (physGoalParent)
			{
				base.transform.parent = ReferenceMaster.physicsGoalInstance;
			}
			SendBreakMessage();
			AddToPercentageBar();
			if (audioSource != null)
			{
				audioSource.Play();
			}
			SendBreakEvent();
			for (int i = 0; i < particleSystems.Length; i++)
			{
				particleSystems[i].Play();
			}
			if (OnBreakTrigger != null)
			{
				OnBreakTrigger(this);
			}
		}
	}

	protected override void SendBreakEvent()
	{
		base.SendBreakEvent();
		if (StatMaster.isHosting && base.SimPhysics)
		{
			LevelEntity levelEntity = base.NetBlock as LevelEntity;
			if (levelEntity != null)
			{
				levelEntity.StartSimTrack(true);
			}
		}
	}

	protected void ExplodeSupports(float powery, Vector3 positiony, float radiusy, float upAmount)
	{
		for (int i = 0; i < objsImSupporting.Length; i++)
		{
			Rigidbody rigidbody = objsImSupporting[i];
			if (rigidbody != null)
			{
				rigidbody.GetComponent<BreakOnForceNoSpawn>().BreakExplosion(powery, positiony, radiusy, 0f);
			}
		}
	}

	protected void SendBreakMessage()
	{
		for (int i = 0; i < sendBreakMessage.Length; i++)
		{
			Transform transform = sendBreakMessage[i];
			if (transform != null)
			{
				transform.SendMessage("Break");
			}
		}
	}
}
