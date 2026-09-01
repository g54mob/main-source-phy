using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/VacuumBlock")]
public class VacuumBlock : BlockBehaviour
{
	[HideInInspector]
	public Vector3 vacuumPower;

	[HideInInspector]
	public MSlider powerSlider;

	[HideInInspector]
	public MToggle holdToVacuum;

	public ParticleSystem[] particle;

	public float accuracy = 0.5f;

	public float vacuumForce = 10f;

	public VacuumController vacuumeController;

	public TriggerEnterHook holeTrigger;

	private float[] particleLength;

	private MKey On;

	private bool keyPressed;

	private bool emuPressed;

	private bool keyHeld;

	private bool emuHeld;

	private bool keyReleased;

	private bool emuReleased;

	public MSlider PowerSlider
	{
		get
		{
			return powerSlider;
		}
	}

	public MToggle HoldToVaccumToggle
	{
		get
		{
			return holdToVacuum;
		}
	}

	public MKey VacuumKey
	{
		get
		{
			return On;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		On = AddKey(2448, "shoot", ControlScheme.BlockControls.Vacuum, 0, KeyCode.Y);
		holdToVacuum = AddToggle(2478, "hold to vacuum", false);
		holeTrigger.TriggerEntered += vacuumeController.UpdateJoinTarget;
		particleLength = new float[particle.Length];
		for (int i = 0; i < particleLength.Length; i++)
		{
			particleLength[i] = particle[i].shape.length;
		}
		if (isSimulating)
		{
			for (int j = 0; j < particle.Length; j++)
			{
				if (!particle[j].isPlaying)
				{
					particle[j].randomSeed = (uint)Random.Range(0f, 9999999f);
				}
			}
			if (!SimPhysics)
			{
				return;
			}
		}
		powerSlider = AddSlider(2427, "power", 1f, 0.5f, 2f, string.Empty);
		powerSlider.ValueChanged += UpdateRange;
		if (particle.Length != 0 && particle[0] != null)
		{
			WaterFogController.AddEffectMat(particle[0].GetComponent<ParticleSystemRenderer>().sharedMaterial);
		}
	}

	private void UpdateRange(float value)
	{
		vacuumeController.UpdateRange(value);
		for (int i = 0; i < particleLength.Length; i++)
		{
			ParticleSystem.ShapeModule shape = particle[i].shape;
			shape.length = particleLength[i] * (value * 0.5f + 0.5f);
		}
		ParticleSystem.ColorOverLifetimeModule colorOverLifetime = particle[0].colorOverLifetime;
		Gradient gradient = new Gradient();
		gradient.SetKeys(new GradientColorKey[1]
		{
			new GradientColorKey(Color.white, 0f)
		}, new GradientAlphaKey[4]
		{
			new GradientAlphaKey(0f, 0f),
			new GradientAlphaKey(0.22f, 0.019f * Mathf.Pow(value, 2f) - 0.19f * value + 0.345f),
			new GradientAlphaKey(0.97f, 0.8f),
			new GradientAlphaKey(0f, 1f)
		});
		colorOverLifetime.color = gradient;
	}

	public void ToggleParticles(bool toggle)
	{
		for (int i = 0; i < particle.Length; i++)
		{
			if (toggle)
			{
				if (!particle[i].isPlaying)
				{
					particle[i].Play();
				}
			}
			else if (particle[i].isPlaying)
			{
				particle[i].Stop();
			}
		}
		if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && NetBlock != null)
		{
			NetBlock.Event(NetworkEntity.EntityEvent.ToggleVacuum, (byte)(toggle ? 1u : 0u));
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (isSimulating && SimPhysics)
		{
			keyPressed = On.IsPressed;
			keyHeld = On.IsHeld;
			keyReleased = On.IsReleased;
			EvaluateKey(keyPressed, keyReleased, emuHeld);
			CalculateForcePositions();
		}
	}

	public override void EmulationUpdateBlock()
	{
		emuPressed = On.EmulationPressed();
		emuHeld = On.EmulationHeld(true);
		emuReleased = On.EmulationReleased();
		EvaluateKey(emuPressed, emuReleased, keyHeld);
	}

	protected void EvaluateKey(bool keyPressed, bool keyReleased, bool altHeld)
	{
		if (holdToVacuum.IsActive)
		{
			if (keyPressed)
			{
				vacuumeController.isOff = false;
			}
			else if (keyReleased && !altHeld)
			{
				vacuumeController.isOff = true;
			}
		}
		else if (keyReleased)
		{
			vacuumeController.isOff = !vacuumeController.isOff;
		}
		if (vacuumeController.isOff)
		{
			if (!vacuumeController.wasOff)
			{
				Object.Destroy(vacuumeController.joint);
			}
		}
		else
		{
			vacuumeController.wasOff = vacuumeController.isOff;
		}
	}

	public void CalculateForcePositions()
	{
		if (SimPhysics)
		{
			if (vacuumeController.enabled)
			{
				vacuumeController.CalculateForcePositions();
			}
			ToggleParticles(!vacuumeController.isTouching && !vacuumeController.isOff);
		}
	}

	public void OnJointBreak(float force)
	{
		if (SimPhysics)
		{
			if ((bool)vacuumeController.joint && !vacuumeController.isOff && !particle[0].isPlaying)
			{
				ToggleParticles(true);
			}
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
		}
	}
}
