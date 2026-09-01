using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Paddle")]
public class PaddleBlock : BlockBehaviour
{
	public Vector3 paddleAxis;

	public ParticleSystem[] bubbles;

	public AudioSource audio;

	public AudioClip[] clips;

	private Vector3 currentVelocity;

	private Vector3 xyz;

	private bool playing;

	private bool tipInWater;

	private Vector3 tipPos = new Vector3(0f, 0f, 1.4f);

	private float speed;

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (!noRigidbody)
		{
			if (blockJoint == null)
			{
				OnJointBreak();
			}
			uint randomSeed = (uint)Random.Range(0, 9999999);
			for (int i = 0; i < bubbles.Length; i++)
			{
				bubbles[i].randomSeed = randomSeed;
			}
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!base.ParentMachine.isReady)
		{
			return;
		}
		if (tipInWater)
		{
			if (!playing)
			{
				playing = true;
				for (int i = 0; i < bubbles.Length; i++)
				{
					bubbles[i].Play();
				}
				if (speed > 300f)
				{
					audio.volume = Mathf.InverseLerp(0f, 2000f, speed) * 0.11f;
					audio.pitch = Random.Range(0.8f, 1f);
					audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
				}
			}
		}
		else if (playing)
		{
			playing = false;
			for (int j = 0; j < bubbles.Length; j++)
			{
				bubbles[j].Stop();
			}
		}
	}

	public override void FixedUpdateBlock()
	{
		if (!base.ParentMachine.isReady)
		{
			return;
		}
		if (noRigidbody)
		{
			_parentMachine.UnregisterFixedUpdate(this, false);
			tipInWater = base.InWater;
			return;
		}
		if (!base.InWater)
		{
			Rigidbody.drag = 0.2f;
			tipInWater = false;
			return;
		}
		float magnitude = Rigidbody.velocity.magnitude;
		Rigidbody.drag = 0.2f + Mathf.Abs(Vector3.Dot(Rigidbody.velocity / magnitude, base.transform.up)) * submergedPercent * Mathf.Clamp01(magnitude);
		Vector3 vector = base.transform.TransformPoint(tipPos);
		tipInWater = WaterController.Exist && WaterController.IsUnderwater(vector);
		if (tipInWater)
		{
			currentVelocity = Rigidbody.GetPointVelocity(vector);
			speed = currentVelocity.sqrMagnitude;
			xyz = -base.transform.InverseTransformDirection(currentVelocity);
			xyz = Vector3.Scale(xyz, paddleAxis * (1f + speed * 0.0025f));
			xyz = base.transform.TransformDirection(xyz);
			xyz = Vector3.ClampMagnitude(xyz, 1000f);
			float num = Mathf.Clamp(xyz.sqrMagnitude / 1000f, 0.1f, 1f) * 0.5f;
			for (int i = 0; i < bubbles.Length; i++)
			{
				ParticleSystem.EmissionModule emission = bubbles[i].emission;
				emission.rate = num;
			}
			float num2 = submergedPercent;
			if (num2 < 0.5f)
			{
				num2 = 0.5f;
			}
			num2 *= BlockHealth.health / BlockHealth.maxHealth;
			if (num2 > 0f)
			{
				Rigidbody.AddForceAtPosition(xyz * num2 * 2f, vector, ForceMode.Acceleration);
			}
		}
	}

	public void OnJointBreak()
	{
		tipPos = new Vector3(0f, 0f, 0.875f);
		FragmentVisualController.EmitJointBreakMarker(base.transform.position);
	}
}
