using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/FlyingController")]
public class FlyingController : BlockBehaviour
{
	public ConstantForce ConstantForcey;

	public ConfigurableJoint myJoint;

	public RotateObj spinCode;

	public Transform spinObj;

	public Vector3 speed = new Vector3(10f, 15f, 19f);

	public float lerpSpeed = 6f;

	public bool isFrozen;

	public bool canFly = true;

	public float dragScaler = 1.5f;

	protected bool brokenOff;

	public Vector3 speedToGo;

	private Vector3 lerpedSpeed = default(Vector3);

	private Vector3 force = default(Vector3);

	private float lerpySpeed;

	public bool flying;

	private Transform myTransform;

	public AudioSource sfx;

	public float sfxPitch = 0.7f;

	public float sfxSpeedPitch = 0.5f;

	private MKey flyKey;

	private MToggle automaticToggle;

	private MSlider speedSlider;

	private MToggle toggleMode;

	private MToggle reverseToggle;

	private float prevDrag;

	private float reverseMultiplier = 1f;

	private float magnitude;

	private bool isReleased = true;

	private bool flyPressed;

	private bool emuPressed;

	private bool flyReleased;

	private bool emuReleased;

	private bool flyHeld;

	private bool emuHeld;

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public MToggle AutomaticToggle
	{
		get
		{
			return automaticToggle;
		}
	}

	public MToggle ToggleModeToggle
	{
		get
		{
			return toggleMode;
		}
	}

	public MToggle ReverseToggle
	{
		get
		{
			return reverseToggle;
		}
	}

	public MKey FlyKey
	{
		get
		{
			return flyKey;
		}
	}

	public override Vector3 GetCenter()
	{
		if (!hasOffset)
		{
			center = new Vector3(0f, 0f, 0.5f);
			hasOffset = true;
		}
		return base.transform.TransformPoint(center);
	}

	protected override void Awake()
	{
		base.Awake();
		flyKey = AddKey(2468, "spin", ControlScheme.BlockControls.FlyingBlock, 0, KeyCode.O);
		automaticToggle = AddToggle(2430, "automatic", false);
		toggleMode = AddToggle(2431, "toggle-mode", false);
		reverseToggle = AddToggle(2434, "reverse", false);
		automaticToggle.Toggled += delegate(bool isActive)
		{
			toggleMode.DisplayInMapper = !isActive;
		};
		speedSlider = AddSlider(2469, "speed", 1f, 0f, 1.25f, string.Empty);
	}

	protected override void Start()
	{
		base.Start();
		if (isSimulating && SimPhysics)
		{
			prevDrag = Rigidbody.drag;
			lerpySpeed = lerpSpeed + (float)Random.Range(-2, 3);
			myTransform = base.transform;
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, Prefab.RegisterEmulationUpdate);
	}

	private void SetFlying(bool toggle)
	{
		if (SimPhysics && toggle != flying)
		{
			if (toggle)
			{
				_parentMachine.RegisterFixedUpdate(this, false);
				if (!sfx.isPlaying)
				{
					sfx.volume = 0f;
					sfx.Play();
					sfx.timeSamples = Random.Range(0, sfx.clip.samples);
				}
			}
			else
			{
				_parentMachine.UnregisterFixedUpdate(this, false);
			}
		}
		flying = toggle;
	}

	private void CheckKeys(bool pressed, bool released, bool held)
	{
		if (toggleMode.IsActive)
		{
			if (pressed && !isFrozen)
			{
				if (flying)
				{
					SetFlying(false, Vector3.zero, 0.5f);
				}
				else
				{
					lerpySpeed = lerpSpeed + Random.Range(-2f, 3f);
					SetFlying(true, speed, 1.5f);
				}
			}
			isReleased = true;
		}
		else if (!isFrozen)
		{
			if (pressed || (isReleased && held))
			{
				isReleased = false;
				lerpySpeed = lerpSpeed + Random.Range(-2f, 3f);
				SetFlying(true, speed, 1.5f);
			}
			if (released)
			{
				isReleased = true;
				SetFlying(false, Vector3.zero, 0.5f);
			}
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (canFly && _parentMachine.isReady && !automaticToggle.IsActive)
		{
			emuPressed = flyKey.EmulationPressed();
			emuReleased = flyKey.EmulationReleased();
			emuHeld = flyKey.EmulationHeld(true);
			CheckKeys(emuPressed, emuReleased && !flyHeld, emuHeld || flyHeld);
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!canFly || !_parentMachine.isReady)
		{
			return;
		}
		flyPressed = flyKey.IsPressed;
		flyReleased = flyKey.IsReleased;
		flyHeld = flyKey.IsHeld;
		if (automaticToggle.IsActive)
		{
			if (!flying)
			{
				lerpySpeed = lerpSpeed + Random.Range(-2f, 3f);
				SetFlying(true, speed, 1.5f);
			}
		}
		else
		{
			CheckKeys(flyPressed, flyReleased && !emuHeld, flyHeld || emuHeld);
		}
		if (speedToGo == Vector3.zero && lerpedSpeed == Vector3.zero)
		{
			return;
		}
		Lerp(ref lerpedSpeed.x, speedToGo.x);
		Lerp(ref lerpedSpeed.y, speedToGo.y);
		Lerp(ref lerpedSpeed.z, speedToGo.z);
		float getSubmergedPctMV = base.GetSubmergedPctMV;
		float num = Time.deltaTime * (0f - reverseMultiplier) * (1f - 0.5f * getSubmergedPctMV);
		spinObj.Rotate(lerpedSpeed * num);
		float num2 = Mathf.Clamp(lerpedSpeed.z * 0.001f, 0f, 2f);
		sfx.volume = (0.1f * Mathf.Clamp01(num2 * 10f) + Mathf.Clamp01(num2) * 0.2f) * base.transform.localScale.x;
		sfx.pitch = Mathf.Clamp(sfxPitch + num2 * sfxSpeedPitch + Random.Range(-0.07f, 0.07f), 0f, 2f);
		if (num2 <= 0.0001f)
		{
			if (sfx.isPlaying)
			{
				sfx.Stop();
			}
		}
		else if (!sfx.isPlaying)
		{
			sfx.Play();
		}
		if (isSimulating && SimPhysics)
		{
			magnitude = 100f * speedSlider.Value * reverseMultiplier * (1f - 0.5f * submergedPercent);
		}
	}

	private void Lerp(ref float lerpedSpeed, float targetSpeed)
	{
		lerpedSpeed += (targetSpeed * speedSlider.Value - lerpedSpeed) * Time.deltaTime * lerpySpeed;
	}

	public void SetFlying(bool b, Vector3 velocity, float drag)
	{
		speedToGo = velocity;
		if (!noRigidbody)
		{
			originalDrag = drag;
			Rigidbody.drag += drag - prevDrag;
			prevDrag = drag;
		}
		SetFlying(b);
	}

	public override void FixedUpdateBlock()
	{
		if (!canFly || !_parentMachine.isReady)
		{
			return;
		}
		if (noRigidbody || !SimPhysics)
		{
			_parentMachine.UnregisterFixedUpdate(this, false);
			return;
		}
		if (speedToGo != Vector3.zero || lerpedSpeed != Vector3.zero)
		{
			Vector3 vector = myTransform.forward * magnitude;
			force.Set(vector.x, vector.y, vector.z);
		}
		if (base.InWater && !StatMaster.GodTools.GravityDisabled)
		{
			force *= 0.1f;
		}
		Rigidbody.AddForce(force - Rigidbody.velocity * dragScaler);
	}

	private void OnJointBreak()
	{
		FragmentVisualController.EmitJointBreakMarker(base.transform.position);
		canFly = false;
		SetFlying(false);
		if (SimPhysics)
		{
			ConstantForcey.enabled = false;
		}
	}

	public override void FreezeMe()
	{
		base.FreezeMe();
		isFrozen = true;
		speedToGo.Set(0f, 0f, 0f);
		if (SimPhysics)
		{
			ConstantForcey.enabled = false;
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating)
		{
			reverseMultiplier = ((!reverseToggle.IsActive) ? 1f : (-1f));
			if (SimPhysics)
			{
				magnitude = 100f * speedSlider.Value * reverseMultiplier;
			}
		}
	}
}
