using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Fly Wheel Block")]
public class FlyWheelBlock : CogMotorControllerHinge
{
	private const float infiniteForce = 1000f;

	public AnimationCurve massCurve;

	public bool clutchKey;

	private MSlider inertiaSlider;

	private MSlider clutchSlider;

	private MToggle useMotor;

	protected Vector3 startInertia = new Vector3(0.166f, 0.166f, 0.317f);

	private bool locked;

	private float lastForce;

	private float lerp;

	private bool physicsStarted;

	public AudioClip lockSfx;

	public AudioClip unlockSfx;

	public AnimationCurve ringScale = AnimationCurve.Linear(0f, 0f, 10f, 1f);

	public MToggle UseMotor
	{
		get
		{
			return useMotor;
		}
	}

	public MSlider InertiaSlider
	{
		get
		{
			return inertiaSlider;
		}
	}

	public MSlider ClutchSlider
	{
		get
		{
			return clutchSlider;
		}
	}

	protected override void Awake()
	{
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		if (allowControl)
		{
			useMotor = AddToggle("USE MOTOR", "use-motor", false);
		}
		base.Awake();
		inertiaSlider = AddSlider(4582, "inertia", 2f, 1f, 10f, string.Empty);
		inertiaSlider.logScaling = true;
		inertiaSlider.ValueChanged += SetInertia;
		if (allowControl)
		{
			if (!isSimulating)
			{
				useMotor.Toggled += ToggleMotor;
				ToggleMotor(false);
			}
		}
		else if (clutchKey)
		{
			forwardKey = AddKey(4591, "lock", KeyCode.J);
			toggleMode = AddToggle(2431, "toggle-mode", true);
			automaticToggle = AddToggle(3772, "inverted", false);
			if (!hasSpeed)
			{
				speedSlider = AddSliderUnclamped(2427, "lockforce", 50f, 1f, 100f, string.Empty, string.Empty, true);
				speedSlider.logScaling = true;
				speedSlider.maxInfinity = true;
				clutchSlider = AddSliderUnclamped(4890, "clutchTime", 0.25f, 0f, 2f, string.Empty, "s", true);
			}
		}
	}

	protected void SetInertia(float i)
	{
		if (float.IsNaN(i))
		{
			if (!noRigidbody)
			{
				Rigidbody.mass = 1f;
			}
			i = -0.135f;
		}
		else
		{
			if (!noRigidbody)
			{
				Rigidbody.mass = massCurve.Evaluate(i);
			}
			i = ringScale.Evaluate(i);
		}
		VisualController.AssignMaterialProperty("_Deform", new Vector4(0f, 0f, i, 0f));
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		return false;
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (noRigidbody || !SimPhysics)
		{
			return;
		}
		if (hasJoint)
		{
			if (allowControl)
			{
				myJoint.useMotor = useMotor.IsActive;
			}
			else if (clutchKey)
			{
				if (automaticToggle.IsActive)
				{
					motor.targetVelocity = 0f;
					motor.force = Mathf.Pow(speedSlider.Value, 2f);
					myJoint.motor = motor;
					myJoint.useMotor = true;
					locked = true;
					lastForce = motor.force;
				}
				else
				{
					myJoint.useMotor = false;
				}
			}
			else
			{
				myJoint.useMotor = false;
			}
		}
		if (float.IsNaN(speedSlider.Value) || float.IsNaN(clutchSlider.Value))
		{
			base.ParentMachine.UnregisterFixedUpdate(this, false);
		}
		else
		{
			physicsStarted = true;
		}
		float num = Mathf.Clamp(inertiaSlider.Value, 0.1f, 100f);
		if (float.IsNaN(num))
		{
			Debug.Log("inertia is NaN");
			return;
		}
		Vector3 localScale = base.transform.localScale;
		localScale.z = 0f;
		float sqrMagnitude = localScale.sqrMagnitude;
		Vector3 vector = new Vector3(1f, 1f, num) * num * num;
		Vector3 inertiaTensor = Vector3.Scale(startInertia, vector * sqrMagnitude);
		Rigidbody.inertiaTensor = inertiaTensor;
		Rigidbody.mass = massCurve.Evaluate(num);
	}

	public override void UpdateBlock()
	{
		if (allowControl)
		{
			base.UpdateBlock();
		}
		else if (clutchKey)
		{
			forwardPressed = forwardKey.IsPressed;
			forwardHeld = forwardKey.IsHeld;
			CheckKeys(forwardPressed, forwardHeld, emuForwardHeld);
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (allowControl)
		{
			base.EmulationUpdateBlock();
		}
		else if (clutchKey)
		{
			emuForwardPressed = forwardKey.EmulationPressed();
			emuForwardHeld = forwardKey.EmulationHeld(true);
			CheckKeys(emuForwardPressed, emuForwardHeld, forwardHeld);
		}
	}

	protected void CheckKeys(bool forwardPress, bool forwardHeld, bool altForwardHeld)
	{
		if (InvalidJoint() && !base.IsMPClientNotLocalSim)
		{
			locked = false;
			return;
		}
		if (toggleMode.IsActive)
		{
			if (forwardPress)
			{
				locked = !locked;
				PlaySound(locked);
			}
			return;
		}
		bool flag = locked;
		locked = (automaticToggle.IsActive ? (!forwardHeld && !altForwardHeld) : (forwardHeld || altForwardHeld));
		if (locked != flag)
		{
			PlaySound(locked);
		}
	}

	private void PlaySound(bool b)
	{
		sfx.pitch = ((!b) ? 0.7f : 1f);
		sfx.PlayOneShot((!b) ? unlockSfx : lockSfx);
	}

	public override void FixedUpdateBlock()
	{
		if (!physicsStarted)
		{
			return;
		}
		if (allowControl)
		{
			base.FixedUpdateBlock();
		}
		else
		{
			if (!clutchKey || InvalidJoint())
			{
				return;
			}
			if (locked)
			{
				if (!myJoint.useMotor)
				{
					motor.targetVelocity = 0f;
					myJoint.motor = motor;
					myJoint.useMotor = true;
				}
				if (lerp < 1f)
				{
					float value = speedSlider.Value;
					float value2 = clutchSlider.Value;
					bool flag = value == float.PositiveInfinity;
					value = Mathf.Min(value, 1000f);
					lerp += ((!(value2 > 0f)) ? 1f : (Time.fixedDeltaTime / value2));
					lastForce = Mathf.Lerp(0f, value * value, Mathf.Pow(lerp, 1f + Mathf.InverseLerp(1f, 1000f, value) * 3f));
					if (lerp >= 1f)
					{
						lastForce = ((!flag) ? (value * value) : float.PositiveInfinity);
					}
					motor.force = lastForce;
					myJoint.motor = motor;
				}
			}
			else if (myJoint.useMotor)
			{
				lerp = 0f;
				lastForce = 0f;
				myJoint.useMotor = false;
			}
		}
	}

	protected void ToggleMotor(bool val)
	{
		MSlider mSlider = speedSlider;
		bool displayInMapper = val;
		accSlider.DisplayInMapper = displayInMapper;
		mSlider.DisplayInMapper = displayInMapper;
		MKey mKey = forwardKey;
		displayInMapper = val;
		automaticToggle.DisplayInMapper = displayInMapper;
		displayInMapper = displayInMapper;
		backwardKey.DisplayInMapper = displayInMapper;
		mKey.DisplayInMapper = displayInMapper;
		toggleMode.DisplayInMapper = val && !automaticToggle.IsActive;
	}

	protected override void AutoToggled(bool val)
	{
		toggleMode.DisplayInMapper = !val && useMotor.IsActive;
	}

	protected override float HandleFreeSpin(float newValue)
	{
		return newValue;
	}
}
