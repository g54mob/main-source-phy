using Localisation;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/SteeringWheel")]
public class SteeringWheel : BlockBehaviour, ILimitsDisplay
{
	protected const int MAX_SFX = 64;

	[Header("Steering")]
	public int version = 1;

	public Vector3 axis = Vector3.zero;

	public bool allowLimits;

	public bool defaultLimits = true;

	public float limit = 40f;

	public float maxLimit = 180f;

	public bool canBeAutomatic;

	public float maxAngularVel = 50f;

	public float speedLerpSmooth = 26f;

	public bool targetAngleMode;

	public float targetAngleSpeed = 0.1f;

	public AudioSource sfx;

	private bool hasSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	private float angleyToBe;

	private float lastAngle;

	private MKey leftKey;

	private MKey rightKey;

	private MToggle automaticToggle;

	private MToggle returnToCenterToggle;

	private MSlider speedSlider;

	private MSlider tensionSlider;

	private MLimits limitsSlider;

	private float input;

	private Vector3 jointEulerRotation = Vector3.zero;

	private ConfigurableJoint myJoint;

	private bool hasJoint;

	protected static int sfx_count;

	private float targetPitch;

	private float lerpDir;

	private float lerpSfx;

	private float leftValue;

	private float rightValue;

	private float emuLeftValue;

	private float emuRightValue;

	private int FlipInvert
	{
		get
		{
			return (!Flipped) ? 1 : (-1);
		}
	}

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public MLimits LimitsSlider
	{
		get
		{
			return limitsSlider;
		}
	}

	public MToggle AutomaticToggle
	{
		get
		{
			return automaticToggle;
		}
	}

	public MToggle ReturnToCenterToggle
	{
		get
		{
			return returnToCenterToggle;
		}
	}

	public float AngleToBe
	{
		get
		{
			return angleyToBe;
		}
		set
		{
			angleyToBe = value;
		}
	}

	private bool canAutoReturn
	{
		get
		{
			return true;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		hasSfx = sfx != null;
		leftKey = AddKey(2437, "left", ControlScheme.BlockControls.Steering, 0, KeyCode.LeftArrow);
		rightKey = AddKey(2438, "right", ControlScheme.BlockControls.Steering, 1, KeyCode.RightArrow);
		speedSlider = AddSlider(2439, "rotation-speed", 1f, 0f, 2f, string.Empty);
		tensionSlider = AddSlider(4590, "tension", 1f, 0.5f, 2f, string.Empty);
		tensionSlider.logScaling = true;
		if (canBeAutomatic)
		{
			automaticToggle = AddToggle(2430, "automatic", false);
			automaticToggle.Toggled += AutoToggled;
		}
		if (canAutoReturn)
		{
			returnToCenterToggle = AddToggle(3655, "autoReturn", base.name.Contains("Hinge"));
			if (canBeAutomatic)
			{
				returnToCenterToggle.DisplayInMapper = !automaticToggle.IsActive;
			}
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
		if (allowLimits)
		{
			BlockType type = Prefab.Type;
			limitsSlider = AddLimits(iconInfo: (type == BlockType.SteeringBlock || type != BlockType.SteeringHinge) ? new FauxTransform(new Vector3(0f, 0.1f, 0f), Quaternion.Euler(0f, 0f, 0f), Vector3.one * 0.33f) : new FauxTransform(new Vector3(0f, -0.342f, 0f), Quaternion.Euler(90f, 0f, 0f), Vector3.one * 0.5f), displayName: LocalisationManager.GetTranslation(2435), key: "limits", defaultMin: limit, defaultMax: limit, highestAngle: maxLimit, limitsDisplay: this, enabled: defaultLimits);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (hasSfx && speedSlider.Value > 0f)
		{
			sfx_count--;
		}
	}

	private void AutoToggled(bool b)
	{
		if (canAutoReturn)
		{
			returnToCenterToggle.DisplayInMapper = !b;
		}
	}

	protected override void FlipArrow(bool flipped)
	{
		VisualController.FlipArrow(flipped, (Prefab.Type == BlockType.SteeringHinge) ? Axes.y : Axes.x);
	}

	private void SetupSFX()
	{
		if (hasSfx && speedSlider.Value > 0f)
		{
			targetPitch = Mathf.Clamp(Mathf.Pow(speedSlider.Value, 0.25f) * Random.Range(0.9f, 1.1f), 0.25f, 4f);
			if (sfx_count < 64)
			{
				sfx.volume = 0f;
				sfx.pitch = targetPitch;
				sfx.Play();
				sfx.timeSamples = Random.Range(0, sfx.clip.samples);
				sfx_count++;
			}
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		SetupSFX();
		if (!noRigidbody)
		{
			Rigidbody.WakeUp();
		}
		hasJoint = true;
		if (!SimPhysics || myJoint == null)
		{
			hasJoint = false;
		}
		else if (myJoint.connectedBody == null)
		{
			Object.Destroy(myJoint);
			hasJoint = false;
		}
	}

	protected bool InvalidJoint()
	{
		return !hasJoint || noRigidbody || (isKinematic && myJoint.connectedBody.gameObject.CompareTag("StayKinematic"));
	}

	private void OnJointBreak()
	{
		if (SimPhysics)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			hasJoint = false;
			myJoint = null;
			if (hasSfx)
			{
				sfx.volume = 0f;
			}
		}
	}

	protected override void Start()
	{
		base.Start();
		myJoint = blockJoint as ConfigurableJoint;
		if (isSimulating && SimPhysics)
		{
			if (!noRigidbody)
			{
				Rigidbody.maxAngularVelocity = maxAngularVel;
			}
			float value = tensionSlider.Value;
			value = value * value * value * value * value * value;
			JointDrive angularYZDrive = myJoint.angularYZDrive;
			angularYZDrive.positionDamper = 50f * value;
			angularYZDrive.positionSpring = 100000f * value;
			myJoint.angularYZDrive = angularYZDrive;
			JointDrive angularXDrive = myJoint.angularXDrive;
			angularXDrive.positionDamper = 50f * value;
			angularXDrive.positionSpring = 100000f * value;
			myJoint.angularXDrive = angularXDrive;
		}
		else if (isSimulating && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			SetupSFX();
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		return true;
	}

	public bool CanMove()
	{
		bool result = true;
		if (!hasJoint)
		{
			input = float.NaN;
			return false;
		}
		if (InvalidJoint())
		{
			input = float.NaN;
			result = false;
		}
		if (!targetAngleMode)
		{
			Debug.LogWarning("targetAngleMode??");
			input = float.NaN;
			result = false;
		}
		return result;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!SimPhysics)
		{
			if (hasSfx)
			{
				HandleSFX();
			}
		}
		else if (!CanMove())
		{
			return;
		}
		if (canBeAutomatic && automaticToggle.IsActive)
		{
			input = 1f;
		}
		else
		{
			leftValue = leftKey.Value;
			rightValue = rightKey.Value;
			GetInput(leftValue, rightValue, emuLeftValue, emuRightValue);
		}
		if (hasSfx)
		{
			if (base.GetSubmergedPctMV < 0.5f)
			{
				sfx.outputAudioMixerGroup = mixer;
			}
			else
			{
				sfx.outputAudioMixerGroup = underwaterMixer;
			}
			if (Time.timeScale == 0f)
			{
				sfx.volume = 0f;
			}
		}
	}

	public override void FixedUpdateBlock()
	{
		if (float.IsNaN(input) || !CanMove())
		{
			return;
		}
		float value = speedSlider.Value;
		bool flag = canAutoReturn && returnToCenterToggle.IsActive && (!canBeAutomatic || !automaticToggle.IsActive);
		if ((input != 0f || flag) && value != 0f)
		{
			if (!noRigidbody && Rigidbody.IsSleeping())
			{
				Rigidbody.WakeUp();
			}
			if (hasJoint)
			{
				Rigidbody connectedBody = myJoint.connectedBody;
				if (connectedBody.IsSleeping())
				{
					connectedBody.WakeUp();
				}
			}
			if (flag && input == 0f && angleyToBe != 0f)
			{
				float num = Mathf.Clamp(value, 0f, 2f);
				float num2 = (0f - Mathf.Clamp(angleyToBe, -1f, 1f)) * Time.fixedDeltaTime * 100f * targetAngleSpeed * num * 0.6f;
				bool flag2 = angleyToBe > 0f;
				angleyToBe += num2;
				bool flag3 = angleyToBe > 0f;
				if (flag2 != flag3)
				{
					angleyToBe = 0f;
				}
			}
			else
			{
				float num3 = input * Time.fixedDeltaTime * 100f * targetAngleSpeed * (float)FlipInvert * value;
				angleyToBe += num3;
			}
			if (allowLimits && limitsSlider.IsActive)
			{
				float num4;
				float num5;
				if (Flipped)
				{
					num4 = 0f - limitsSlider.Min;
					num5 = limitsSlider.Max;
				}
				else
				{
					num4 = 0f - limitsSlider.Max;
					num5 = limitsSlider.Min;
				}
				angleyToBe = ((angleyToBe < num4) ? num4 : ((!(angleyToBe > num5)) ? angleyToBe : num5));
			}
			else if (flag)
			{
				if (angleyToBe > 360f)
				{
					angleyToBe -= 360f;
				}
				else if (angleyToBe < -360f)
				{
					angleyToBe += 360f;
				}
			}
			else if (angleyToBe > 180f)
			{
				angleyToBe -= 360f;
			}
			else if (angleyToBe < -180f)
			{
				angleyToBe += 360f;
			}
		}
		if (Mathf.Abs(angleyToBe) < 0.0001f)
		{
			myJoint.targetAngularVelocity = Vector3.zero;
		}
		else
		{
			myJoint.targetAngularVelocity = axis * 10f;
		}
		jointEulerRotation.x = axis.x * angleyToBe;
		jointEulerRotation.y = axis.y * angleyToBe;
		jointEulerRotation.z = axis.z * angleyToBe;
		myJoint.targetRotation = Quaternion.Euler(jointEulerRotation);
		if (hasSfx)
		{
			HandleSFX();
		}
		lastAngle = angleyToBe;
	}

	protected void HandleSFX()
	{
		float num = 0f;
		if (SimPhysics)
		{
			num = angleyToBe - lastAngle;
		}
		else if (NetBlock.AngularVelocityMag != 0f)
		{
			num = input;
		}
		lerpDir = Mathf.Clamp(lerpDir + num * 0.1f, -1f, 1f);
		num = Mathf.Abs(num);
		num = ((!(num > 0.0001f)) ? 0f : 1f);
		if (!canAutoReturn || !returnToCenterToggle.IsActive || (canBeAutomatic && automaticToggle.IsActive))
		{
			num *= Mathf.Abs(input);
		}
		lerpSfx += ((!(num > float.Epsilon)) ? (-1f) : 1f) * Time.fixedDeltaTime * 5f * Time.timeScale;
		lerpSfx = Mathf.Clamp01(lerpSfx);
		if (lerpSfx < 0f)
		{
			lerpSfx = 0f;
		}
		num = lerpSfx * 0.75f * Mathf.Abs(lerpDir);
		if (SimPhysics && !hasJoint)
		{
			num = 0f;
		}
		else if (base.ClusterIndex < 0)
		{
			num = 0f;
		}
		if (sfx.volume != num)
		{
			sfx.volume = num;
		}
	}

	public override void EmulationUpdateBlock()
	{
		if ((!SimPhysics || CanMove()) && (!canBeAutomatic || !automaticToggle.IsActive))
		{
			emuLeftValue = leftKey.EmulationValue();
			emuRightValue = rightKey.EmulationValue();
			GetInput(emuLeftValue, emuRightValue, leftValue, rightValue);
		}
	}

	private void GetInput(float leftValue, float rightValue, float altLeftValue, float altRightValue)
	{
		float num = Mathf.Max(leftValue, altLeftValue);
		float num2 = Mathf.Max(rightValue, altRightValue);
		input = num - num2;
	}

	public Transform GetLimitsDisplay()
	{
		return VisualController.Block.MeshRenderer.transform;
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		data.Write("bmt-version", version);
		data.Write("flipped", Flipped);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating && !SimPhysics)
		{
			return;
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
		if (version == 0 && Prefab.Type == BlockType.SteeringBlock)
		{
			targetAngleSpeed = 0.8f;
		}
		if (data.HasKey("flipped"))
		{
			Flipped = data.ReadBool("flipped");
			PostFlip(false, false);
		}
		if (data.HasData && canAutoReturn && !data.HasKey("bmt-autoReturn"))
		{
			returnToCenterToggle.SetValue(false);
		}
	}
}
