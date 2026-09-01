using UnityEngine;

public class FlamethrowerOnDistance : SimBehaviour
{
	public Transform CanonBody;

	public Transform Frame;

	public float heightOffset;

	public Transform targetBlock;

	public Transform FlamethrowerObj;

	public ParticleSystem particles;

	public int targetCounty;

	public float lerpSmooth = 16f;

	public float distanceCutoff = 6f;

	public float sphereCastRadius;

	public float flameRange = 10f;

	public LayerMask layerMasky;

	public Transform[] sphereCastPositions;

	public bool isBroken;

	private Vector3 targetPos;

	private Vector3 targetPos1;

	private Vector3 targetPos2;

	private Quaternion smoothRot1;

	private Quaternion smoothRot2;

	private AudioSource audioSource;

	private int skipCheck;

	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
	}

	protected void Update()
	{
		if (isBroken || !base.SimPhysics)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return;
		}
		if (StatMaster.levelSimulating)
		{
			if (skipCheck == 0)
			{
				Flame(machine);
			}
			skipCheck = ((skipCheck < 3) ? skipCheck++ : 0);
			ToggleEffects(targetCounty > 0);
		}
		targetPos = ((!StatMaster.levelSimulating || !(targetBlock != null)) ? machine.MiddlePosition : targetBlock.position);
		targetPos1 = targetPos - CanonBody.position;
		smoothRot1 = Quaternion.LookRotation(targetPos1, Vector3.up);
		Quaternion rotation = CanonBody.rotation;
		if (rotation != smoothRot1)
		{
			CanonBody.rotation = Quaternion.Lerp(rotation, smoothRot1, Time.deltaTime * lerpSmooth);
		}
	}

	public void Break()
	{
		isBroken = true;
		base.enabled = false;
		ToggleEffects(false);
	}

	private void Flame(Machine activeMachine)
	{
		targetCounty = 0;
		float sqrMagnitude = (CanonBody.position - activeMachine.MiddlePosition).sqrMagnitude;
		if (sqrMagnitude < distanceCutoff)
		{
			targetCounty = 1;
			ShootSphereCasts();
		}
	}

	private void ShootSphereCasts()
	{
		for (int i = 0; i < sphereCastPositions.Length; i++)
		{
			Transform transform = sphereCastPositions[i];
			RaycastHit hitInfo;
			if (!Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out hitInfo, flameRange, layerMasky))
			{
				continue;
			}
			Rigidbody attachedRigidbody = hitInfo.collider.attachedRigidbody;
			if (attachedRigidbody != null)
			{
				FireTag component = attachedRigidbody.GetComponent<FireTag>();
				if (component != null)
				{
					component.Ignite(1f);
					break;
				}
			}
		}
	}

	private void ToggleEffects(bool toggle)
	{
		if (toggle)
		{
			if (!particles.isPlaying)
			{
				particles.Play();
			}
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			if (particles.isPlaying)
			{
				particles.Stop();
			}
			if (audioSource.isPlaying)
			{
				audioSource.Stop();
			}
		}
	}
}
