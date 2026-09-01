using System.Collections.Generic;
using Localisation;
using Modding;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/CogMotorControllerHinge")]
public class CogMotorControllerHinge : BlockBehaviour, ILimitsDisplay
{
	[HideInInspector]
	public float Velocity;

	public static float minForce;

	public static float accInfinity = 1000000f;

	public int version = 1;

	public Transform limitsVisual;

	public bool startOn;

	public bool allowControl = true;

	public bool hasSpeed = true;

	public bool enableLimits;

	public bool canBrake = true;

	public float degreesPerSecond = 1f;

	public float maxAngularVel = 50f;

	public Transform rotationArrow;

	public float minAcc = 0.1f;

	public float maxAcc = 50f;

	public float defaultAcc = float.PositiveInfinity;

	public float defaultSpeed = 1f;

	public float minSpeed;

	public float maxSpeed = 2f;

	public float speedLerpSmooth = 26f;

	public JointMotor motor;

	public MeshCollider altCollider;

	public Vector3 inertia = Vector3.zero;

	public bool hasAltCollider;

	public AudioSource sfx;

	private bool hasSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected MLimits limitsSlider;

	protected HingeJoint myJoint;

	protected float lastInput = -2f;

	protected float input;

	protected float arrowStartScaleX;

	protected MToggle automaticToggle;

	protected MToggle toggleMode;

	protected MToggle autoBrakeMode;

	protected MToggle optimiseCollider;

	protected MKey forwardKey;

	protected MKey backwardKey;

	public MSlider speedSlider;

	private MSlider contactSlider;

	protected MSlider accSlider;

	protected Renderer arrowRenderer;

	protected float deltaMultiplier;

	protected float lastVelocity;

	protected JointSpring mySpring;

	protected bool hasStarted;

	protected bool hasJoint;

	public static bool ShowContactToggle;

	protected bool isWheel;

	protected bool isSpin;

	protected float waterLimitForce;

	private float sfxSpeed = 1f;

	protected bool forwardPressed;

	protected bool backwardPressed;

	protected bool forwardHeld;

	protected bool backwardHeld;

	protected bool emuForwardPressed;

	protected bool emuBackwardPressed;

	protected bool emuForwardHeld;

	protected bool emuBackwardHeld;

	private float accelSfx;

	private float lerpSfx;

	private float lastAngle;

	protected bool forceReset = true;

	public float Input
	{
		get
		{
			return input;
		}
		set
		{
			input = value;
		}
	}

	protected virtual int FlipInvert
	{
		get
		{
			return Flipped ? 1 : (-1);
		}
	}

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public MSlider AccelerationSlider
	{
		get
		{
			return accSlider;
		}
	}

	public MToggle AutomaticToggle
	{
		get
		{
			return automaticToggle;
		}
	}

	public MToggle AutoBreakToggle
	{
		get
		{
			return autoBrakeMode;
		}
	}

	public MToggle ToggleModeToggle
	{
		get
		{
			return toggleMode;
		}
	}

	public MLimits LimitsSlider
	{
		get
		{
			return limitsSlider;
		}
	}

	public MKey ForwardKey
	{
		get
		{
			return forwardKey;
		}
	}

	public MKey BackwardKey
	{
		get
		{
			return backwardKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		hasSfx = sfx != null;
		if (hasSpeed)
		{
			speedSlider = AddSlider(2428, "speed", defaultSpeed, minSpeed, (!StatMaster.UnlockSpeedSliders) ? maxSpeed : 1000f, string.Empty);
		}
		if (hasSpeed)
		{
			accSlider = AddSliderUnclamped(2436, "acceleration", defaultAcc, minAcc, maxAcc, string.Empty);
			accSlider.logScaling = true;
			accSlider.maxInfinity = true;
		}
		myJoint = blockJoint as HingeJoint;
		if (allowControl)
		{
			automaticToggle = AddToggle(2430, "automatic", startOn);
			toggleMode = AddToggle(2431, "toggle-mode", false);
			if (canBrake)
			{
				autoBrakeMode = AddToggle(2432, "auto-brake", true);
			}
			automaticToggle.Toggled += AutoToggled;
			toggleMode.DisplayInMapper = !startOn;
			forwardKey = AddKey(2433, "forward", ControlScheme.BlockControls.Wheels, 0, KeyCode.UpArrow);
			backwardKey = AddKey(2434, "backward", ControlScheme.BlockControls.Wheels, 1, KeyCode.DownArrow);
		}
		if (enableLimits)
		{
			limitsSlider = AddLimits(LocalisationManager.GetTranslation(2435), "limits", 90f, 90f, 180f, this);
		}
		if (hasAltCollider)
		{
			optimiseCollider = AddToggle("OPTIMISE COLLIDER", "opt-collider", true);
			optimiseCollider.Toggled += ToggleColliders;
			ToggleColliders(true);
			optimiseCollider.DisplayInMapper = false;
		}
		switch (Prefab.Type)
		{
		case BlockType.Wheel:
		case BlockType.LargeWheel:
			isWheel = true;
			contactSlider = AddSlider(4430, "contact", 0.1f, 0.1f, 0.5f, string.Empty);
			contactSlider.DisplayInMapper = ShowContactToggle;
			break;
		case BlockType.SpinningBlock:
			isSpin = true;
			if (canBrake)
			{
				autoBrakeMode = AddToggle(4592, "auto-brake", true);
			}
			break;
		}
		if (isSimulating)
		{
			if (hasSfx)
			{
				mixer = sfx.outputAudioMixerGroup;
				underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			}
			if (!SimPhysics)
			{
				return;
			}
		}
		if (allowControl)
		{
			AutoToggled(startOn);
		}
	}

	protected virtual void AutoToggled(bool val)
	{
		toggleMode.DisplayInMapper = !val;
		MKey mKey = forwardKey;
		bool displayInMapper = !val;
		backwardKey.DisplayInMapper = displayInMapper;
		mKey.DisplayInMapper = displayInMapper;
		if (isSpin)
		{
			myJoint.useSpring = val;
		}
	}

	private static Vector3 ScaleInertia(Vector3 scale, Vector3 inertia)
	{
		Vector3 vector = scale;
		Vector3 vector2 = new Vector3(Mathf.Max(vector.y, vector.z), Mathf.Max(vector.x, vector.z), Mathf.Max(vector.x, vector.y));
		Vector3 vector3 = inertia;
		return new Vector3(vector3.x * vector2.x * vector2.x, vector3.y * vector2.y * vector2.y, vector3.z * vector2.z * vector2.z);
	}

	protected override void Start()
	{
		base.Start();
		if (isSimulating && SimPhysics)
		{
			if (hasAltCollider)
			{
				if (altCollider.enabled)
				{
					for (int i = 0; i < myBounds.childColliders.Count - 1; i++)
					{
						Object.Destroy(myBounds.childColliders[i].gameObject);
					}
					myBounds.childColliders = new List<Collider> { altCollider };
					if (inertia != Vector3.zero)
					{
						Rigidbody.inertiaTensor = ScaleInertia(base.transform.localScale, inertia);
					}
				}
				else
				{
					myBounds.childColliders.Remove(altCollider);
					Object.Destroy(altCollider);
				}
			}
			if (version > 0)
			{
				Rigidbody.angularDrag = 0.05f;
				Rigidbody.drag = 0.1f;
				originalDrag = 0.1f;
				originalADrag = 0.05f;
			}
			if (isWheel && !stripped)
			{
				float num = Mathf.Clamp(contactSlider.Value * 0.1f, 0.01f, 0.05f);
				if (float.IsNaN(num) && (version > 0 || optimiseCollider.IsActive))
				{
					num = 0.01f;
				}
				foreach (Collider childCollider in myBounds.childColliders)
				{
					childCollider.contactOffset = num;
				}
			}
			motor = myJoint.motor;
			bool flag = (allowControl || isSpin) && canBrake;
			if (version > 0)
			{
				motor.force = ((!flag || !autoBrakeMode.IsActive) ? minForce : float.PositiveInfinity);
			}
			else
			{
				motor.force = accSlider.Value;
			}
			motor.freeSpin = !canBrake || (flag && !autoBrakeMode.IsActive);
			motor.targetVelocity = 0f;
			myJoint.motor = motor;
			myJoint.useMotor = false;
			waterLimitForce = WaterController.WheelMotorForceValue * speedSlider.Value;
		}
		else if (hasSpeed && isSimulating && base.IsMPClientNotLocalSim)
		{
			SetupSFX();
		}
	}

	private void SetupSFX()
	{
		sfxSpeed = Mathf.Pow(speedSlider.Value, 0.25f);
		if (!isWheel)
		{
			sfxSpeed *= 2f;
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		hasJoint = true;
		if (!noRigidbody)
		{
			Rigidbody.maxAngularVelocity = maxAngularVel;
		}
		deltaMultiplier = degreesPerSecond * 80f * (float)(-FlipInvert);
		if (hasSpeed && SimPhysics)
		{
			SetupSFX();
		}
		if (!SimPhysics || myJoint == null)
		{
			hasJoint = false;
		}
		else if (myJoint.connectedBody == null)
		{
			Object.Destroy(myJoint);
			hasJoint = false;
		}
		else
		{
			myJoint.useMotor = true;
		}
	}

	protected virtual void OnJointBreak()
	{
		if (SimPhysics)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			if (hasSfx)
			{
				sfx.Stop();
			}
			hasJoint = false;
			myJoint = null;
		}
	}

	protected bool InvalidJoint()
	{
		return !hasJoint || noRigidbody || (isKinematic && myJoint.connectedBody.gameObject.CompareTag("StayKinematic"));
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		return true;
	}

	private void ClampAcceleration(float newValue)
	{
		if (newValue < minAcc)
		{
			accSlider.Value = minAcc;
		}
	}

	protected virtual void ToggleColliders(bool val)
	{
		if (!isSimulating && !stripped)
		{
			for (int i = 0; i < myBounds.childColliders.Count; i++)
			{
				myBounds.childColliders[i].enabled = !val;
			}
			if ((bool)altCollider)
			{
				altCollider.enabled = val;
			}
			SingleInstance<Events>.Instance.CollidersChanged(this);
		}
	}

	private void ClientAndHostUpdateLoop()
	{
		if (allowControl)
		{
			forwardPressed = forwardKey.IsPressed;
			backwardPressed = backwardKey.IsPressed;
			forwardHeld = forwardKey.IsHeld;
			backwardHeld = backwardKey.IsHeld;
			CheckKeys(forwardPressed, backwardPressed, forwardHeld, backwardHeld, forwardKey.Value, 0f - backwardKey.Value, emuForwardHeld, emuBackwardHeld);
		}
		else if (InvalidJoint() && !base.IsMPClientNotLocalSim)
		{
			input = 0f;
		}
		else
		{
			input = 1f;
		}
		if (hasSfx)
		{
			if (!(base.GetSubmergedPctMV > 0.5f))
			{
				sfx.outputAudioMixerGroup = mixer;
			}
			else
			{
				sfx.outputAudioMixerGroup = underwaterMixer;
			}
			if (Time.timeScale == 0f || (SimPhysics && !hasJoint))
			{
				sfx.volume = 0f;
			}
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		ClientAndHostUpdateLoop();
	}

	public override void EmulationUpdateBlock()
	{
		if (allowControl)
		{
			emuForwardPressed = forwardKey.EmulationPressed();
			emuBackwardPressed = backwardKey.EmulationPressed();
			emuForwardHeld = forwardKey.EmulationHeld(true);
			emuBackwardHeld = backwardKey.EmulationHeld(true);
			CheckKeys(emuForwardPressed, emuBackwardPressed, emuForwardHeld, emuBackwardHeld, 1f, -1f, forwardHeld, backwardHeld);
		}
	}

	protected void CheckKeys(bool forwardPress, bool backwardPress, bool forwardHeld, bool backwardHeld, float forwardVal, float backwardVal, bool altForwardHeld, bool altBackwardHeld)
	{
		if (InvalidJoint() && !base.IsMPClientNotLocalSim)
		{
			input = 0f;
		}
		else if (automaticToggle.IsActive)
		{
			input = 1f;
		}
		else if (toggleMode.IsActive)
		{
			if (forwardPress)
			{
				if (input > 0.9f)
				{
					input = 0f;
				}
				else
				{
					input = 1f;
				}
			}
			if (backwardPress)
			{
				if (input < -0.9f)
				{
					input = 0f;
				}
				else
				{
					input = -1f;
				}
			}
		}
		else if (forwardHeld)
		{
			input = forwardVal;
		}
		else if (!altForwardHeld)
		{
			if (backwardHeld)
			{
				input = backwardVal;
			}
			else if (!altBackwardHeld)
			{
				input = 0f;
			}
		}
	}

	public override void FixedUpdateBlock()
	{
		if (!hasJoint || noRigidbody)
		{
			if (!SimPhysics && hasSfx)
			{
				ClientAndHostUpdateLoop();
				HandleSFX();
			}
			return;
		}
		if (isSpin && speedSlider.Value == 0f)
		{
			input = 0f;
		}
		Velocity = input * speedSlider.Value;
		float num = ((version <= 0) ? OldAcceleration() : CombinedAcceleration());
		if (version == 0)
		{
			num = HandleFreeSpin(num);
		}
		if (hasSfx)
		{
			HandleSFX();
		}
		float f = num - lastVelocity;
		if (Mathf.Abs(f) > float.Epsilon)
		{
			if (Rigidbody.IsSleeping())
			{
				Rigidbody.WakeUp();
			}
			if (myJoint == null || myJoint.connectedBody == null)
			{
				hasJoint = false;
				return;
			}
			if (myJoint.connectedBody.IsSleeping())
			{
				myJoint.connectedBody.WakeUp();
			}
			motor.targetVelocity = num;
			myJoint.motor = motor;
			lastVelocity = num;
		}
		lastInput = input;
	}

	protected void HandleSFX()
	{
		float num = Mathf.Abs(Input);
		float value;
		if (SimPhysics && !noRigidbody)
		{
			value = Mathf.Abs(myJoint.velocity) * Mathf.Clamp01(Mathf.Abs(myJoint.angle - lastAngle));
			lastAngle = myJoint.angle;
		}
		else
		{
			value = NetBlock.AngularVelocityMag * num * 3f;
		}
		float num2 = Mathf.InverseLerp(5f, 50f, value);
		float fixedDeltaTime = Time.fixedDeltaTime;
		float num3 = ((num != 0f) ? num : (-1f));
		accelSfx += num3 * (accSlider.Value / 200f) * fixedDeltaTime * 10f;
		accelSfx = Mathf.Clamp(accelSfx, 0.5f, 1f);
		if (num != 0f && !sfx.isPlaying)
		{
			sfx.Play();
			sfx.timeSamples = Random.Range(0, sfx.clip.samples);
		}
		lerpSfx += num3 * fixedDeltaTime * 10f * Time.timeScale;
		lerpSfx = Mathf.Clamp01(lerpSfx) * num2;
		float num4 = lerpSfx * 0.2f * sfxSpeed;
		if (num4 < 0.01f)
		{
			num4 = 0f;
		}
		else if (SimPhysics && !hasJoint)
		{
			num4 = 0f;
		}
		else if (base.ClusterIndex < 0)
		{
			num4 = 0f;
		}
		sfx.volume = num4;
		sfx.pitch = Mathf.Clamp(sfxSpeed * Mathf.Lerp(0.25f, 1f, accelSfx), 0f, 3f);
	}

	protected virtual float HandleFreeSpin(float newValue)
	{
		if (allowControl && canBrake && !autoBrakeMode.IsActive && Mathf.Abs(input) < 0.05f)
		{
			float num = 0.01f;
			if (lastVelocity > 0f)
			{
				newValue = Mathf.Min(num, newValue);
			}
			else if (lastVelocity < 0f)
			{
				newValue = Mathf.Max(0f - num, newValue);
			}
		}
		return newValue;
	}

	public Transform GetLimitsDisplay()
	{
		return limitsVisual;
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
		if (hasSpeed)
		{
			data.Write("flipped", Flipped);
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		if (enableLimits && !data.HasKey("bmt-uselimits"))
		{
			data.Write("bmt-uselimits", false);
		}
		base.OnLoad(data);
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		if (data.HasKey("flipped"))
		{
			Flipped = data.ReadBool("flipped");
			PostFlip(false, false);
		}
		if (hasAltCollider && !isSimulating && data.WasLoadedFromFile && !data.HasKey("bmt-opt-collider"))
		{
			data.Write("bmt-opt-collider", false);
			optimiseCollider.SetValue(false);
			optimiseCollider.ApplyValue();
		}
		if (!data.HasKey("bmt-version"))
		{
			if (data.WasLoadedFromFile)
			{
				version = 0;
				data.Write("bmt-version", version);
			}
		}
		else
		{
			version = data.ReadInt("bmt-version");
		}
	}

	public float NewAcceleration()
	{
		float num = ((!base.InWater) ? float.PositiveInfinity : 1000000f);
		if (input != lastInput)
		{
			if (allowControl && input == 0f)
			{
				bool flag = canBrake && autoBrakeMode.IsActive;
				motor.force = ((!flag) ? minForce : num);
			}
			else
			{
				motor.force = num;
			}
			myJoint.motor = motor;
		}
		float num2 = Velocity * deltaMultiplier;
		return lastVelocity + (num2 - lastVelocity) * Time.fixedDeltaTime * speedLerpSmooth * Mathf.Clamp01(accSlider.Value / 200f);
	}

	public float CombinedAcceleration()
	{
		float num = ((!base.InWater) ? float.PositiveInfinity : 1000000f);
		bool flag = (allowControl || isSpin) && canBrake && autoBrakeMode.IsActive;
		float num2 = accSlider.Value * 10f;
		if (num2 < 0f)
		{
			num2 = num;
		}
		if (input != lastInput)
		{
			if (input == 0f)
			{
				motor.force = ((!flag) ? minForce : num);
			}
			else
			{
				motor.force = Mathf.Max(num2, 0.001f);
			}
			myJoint.motor = motor;
		}
		else if ((input != 0f || (flag && num2 < num)) && motor.force != num)
		{
			motor.force += motor.force * Time.fixedDeltaTime * 100f;
			if (motor.force > 1000000f)
			{
				motor.force = num;
			}
			myJoint.motor = motor;
		}
		float num3 = Velocity * deltaMultiplier;
		return lastVelocity + (num3 - lastVelocity) * Time.fixedDeltaTime * speedLerpSmooth * Mathf.Clamp01(accSlider.Value / 200f);
	}

	public float OldAcceleration()
	{
		float num = ((!base.InWater) ? float.PositiveInfinity : 1000000f);
		if (input == 0f)
		{
			motor.force = num;
			forceReset = true;
		}
		else
		{
			if (motor.force == num && forceReset)
			{
				motor.force = accSlider.Value;
				forceReset = false;
			}
			if (motor.force != num && !forceReset)
			{
				motor.force += accSlider.Value * Time.fixedDeltaTime * 10f;
				if (motor.force > accInfinity * 10f)
				{
					motor.force = num;
				}
				myJoint.motor = motor;
			}
		}
		float num2 = Velocity * deltaMultiplier;
		return lastVelocity + (num2 - lastVelocity) * Time.fixedDeltaTime * speedLerpSmooth;
	}
}
