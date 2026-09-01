using Localisation;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/RudderController")]
public class RudderController : BlockBehaviour, ILimitsDisplay
{
	public Vector3 AxisDrag;

	public Transform upTransform;

	public Vector3 xyz;

	public Vector3 currentVelocity;

	private float currentSpeed;

	public float dragForceMagnitude;

	public Vector3 dragForceVector;

	public float velocityCap = 100f;

	private float sqrCap;

	[HideInInspector]
	public float oldMass;

	protected BlockBehaviour parent;

	[Header("Debug - Water Options")]
	public bool debugInfo;

	[Header("Debug - Axial Drag Special for water")]
	public bool specialWaterDrag = true;

	[Header("Steering")]
	public Vector3 axis = Vector3.zero;

	public bool allowLimits;

	public bool canBeAutomatic;

	public float degreesPerSecond = 10f;

	public float maxAngularVel = 50f;

	public float speedLerpSmooth = 26f;

	public bool targetAngleMode;

	public float targetAngleSpeed = 0.1f;

	public bool limitRotation;

	public Vector2 limits = new Vector2(-45f, 45f);

	public AudioSource audioSource;

	private float angleyToBe;

	private MKey leftKey;

	private MKey rightKey;

	private MToggle automaticToggle;

	private MToggle returnToCenterToggle;

	private MSlider speedSlider;

	private MLimits limitsSlider;

	private float input;

	private bool hasStarted;

	private int startFrames;

	private float leftValue;

	private float rightValue;

	private float emuLeftValue;

	private float emuRightValue;

	protected bool noJoint;

	private ConfigurableJoint myJoint;

	private bool lastInWater;

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

	public override bool OnFlip(bool sound, bool isUndo)
	{
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		return true;
	}

	protected override void Awake()
	{
		base.Awake();
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		sqrCap = velocityCap * velocityCap;
		myJoint = blockJoint as ConfigurableJoint;
		leftKey = AddKey(2437, "left", ControlScheme.BlockControls.Steering, 0, KeyCode.LeftArrow);
		rightKey = AddKey(2438, "right", ControlScheme.BlockControls.Steering, 1, KeyCode.RightArrow);
		if (canAutoReturn)
		{
			returnToCenterToggle = AddToggle(3655, "autoReturn", true);
			if (canBeAutomatic)
			{
				returnToCenterToggle.DisplayInMapper = !automaticToggle.IsActive;
			}
		}
		speedSlider = AddSlider(2439, "water-rotation-speed", 1f, 0f, 1.5f, string.Empty);
		if (allowLimits)
		{
			FauxTransform iconInfo = new FauxTransform(new Vector3(-0.221f, 0.011f, -0.44f), Quaternion.Euler(6.432f, 34.369f, -39.2f), Vector3.one * 0.75f);
			limitsSlider = AddLimits(LocalisationManager.GetTranslation(2435), "limits", 40f, 40f, 180f, iconInfo, this);
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
		VisualController.FlipArrow(flipped, Axes.y);
	}

	protected override void Start()
	{
		base.Start();
		ConfigurableJoint configurableJoint = blockJoint as ConfigurableJoint;
		if (isSimulating && SimPhysics)
		{
			if (!noRigidbody)
			{
				Rigidbody.maxAngularVelocity = maxAngularVel;
			}
			JointDrive angularYZDrive = configurableJoint.angularYZDrive;
			angularYZDrive.positionDamper = 50f;
			angularYZDrive.positionSpring = 100000f;
			configurableJoint.angularYZDrive = angularYZDrive;
			JointDrive angularXDrive = configurableJoint.angularXDrive;
			angularXDrive.positionDamper = 50f;
			angularXDrive.positionSpring = 100000f;
			configurableJoint.angularXDrive = angularXDrive;
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void FixedUpdateBlock()
	{
		if (noRigidbody)
		{
			_parentMachine.UnregisterFixedUpdate(this, false);
			return;
		}
		if (!float.IsNaN(input) && CanMove())
		{
			float value = speedSlider.Value;
			bool flag = canAutoReturn && returnToCenterToggle.IsActive && (!canBeAutomatic || !automaticToggle.IsActive);
			if ((input != 0f || flag) && value != 0f)
			{
				if (!noRigidbody && Rigidbody.IsSleeping())
				{
					Rigidbody.WakeUp();
				}
				Rigidbody rigidbody = ((!noJoint) ? blockJoint.connectedBody : null);
				if (rigidbody != null && rigidbody.IsSleeping())
				{
					rigidbody.WakeUp();
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
			myJoint.targetRotation = Quaternion.Euler(axis * angleyToBe);
		}
		currentVelocity = Rigidbody.GetPointVelocity(base.transform.position);
		if (!base.InWater || StatMaster.GodTools.GravityDisabled)
		{
			AirForce();
			lastInWater = false;
		}
		else
		{
			WaterForce();
			lastInWater = true;
		}
	}

	private void AirForce()
	{
		Vector3 vector = upTransform.InverseTransformDirection(currentVelocity);
		xyz = Vector3.Scale(-vector, AxisDrag * 0.334f);
		xyz = upTransform.TransformDirection(xyz);
		float num = Mathf.Min(currentVelocity.sqrMagnitude, 600f);
		Rigidbody.AddForce(xyz * num);
	}

	private void WaterForce()
	{
		float magnitude = currentVelocity.magnitude;
		Vector3 vector = upTransform.InverseTransformDirection(currentVelocity);
		xyz = Vector3.Scale(-vector, AxisDrag);
		xyz.x = ClampVel(xyz.x);
		xyz.y = ClampVel(xyz.y);
		xyz.z = ClampVel(xyz.z);
		xyz = upTransform.TransformDirection(xyz);
		Vector3 onNormal = Vector3.Cross(currentVelocity.normalized, base.transform.forward);
		xyz = Vector3.Project(xyz, onNormal) * 0.375f;
		xyz += -base.transform.up * 0.005f * Mathf.Clamp01(Mathf.Abs(vector.x));
		if (!lastInWater)
		{
			xyz *= 0.1f;
		}
		xyz *= 1f - Mathf.Clamp01(Rigidbody.angularVelocity.sqrMagnitude * 0.05f - 5f);
		currentSpeed = Mathf.Min(magnitude * 50f, sqrCap);
		Rigidbody.AddForce(xyz * currentSpeed * submergedPercent * 10f);
	}

	private float ClampVel(float x)
	{
		return (x < 0f - velocityCap) ? (0f - velocityCap) : ((!(x > velocityCap)) ? x : velocityCap);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		if (!hasStarted)
		{
			if (startFrames == 3)
			{
				if (!noRigidbody)
				{
					Rigidbody.WakeUp();
				}
				hasStarted = true;
			}
			else
			{
				startFrames++;
			}
		}
		if (CanMove())
		{
			if (canBeAutomatic && automaticToggle.IsActive)
			{
				input = 1f;
				return;
			}
			leftValue = leftKey.Value;
			rightValue = rightKey.Value;
			GetInput(leftValue, rightValue, emuLeftValue, emuRightValue);
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (CanMove() && (!canBeAutomatic || !automaticToggle.IsActive))
		{
			emuLeftValue = leftKey.EmulationValue();
			emuRightValue = rightKey.EmulationValue();
			GetInput(emuLeftValue, emuRightValue, leftValue, rightValue);
		}
	}

	public Transform GetLimitsDisplay()
	{
		return VisualController.Block.MeshRenderer.transform;
	}

	private void GetInput(float leftValue, float rightValue, float altLeftValue, float altRightValue)
	{
		float num = Mathf.Max(leftValue, altLeftValue);
		float num2 = Mathf.Max(rightValue, altRightValue);
		input = num2 - num;
	}

	public bool CanMove()
	{
		if ((isSimulating && !SimPhysics) || !_parentMachine.finishedPhysics)
		{
			input = float.NaN;
			return false;
		}
		bool result = true;
		if (!SimPhysics || noJoint)
		{
			input = float.NaN;
			return false;
		}
		Rigidbody connectedBody = blockJoint.connectedBody;
		if (connectedBody != null && connectedBody.isKinematic && !noRigidbody && Rigidbody.isKinematic)
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

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		data.Write("flipped", Flipped);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!isSimulating || SimPhysics)
		{
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

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (isSimulating && SimPhysics && (blockJoint == null || blockJoint.connectedBody == null))
		{
			Debug.Log(blockJoint);
			OnJointBreak();
		}
	}

	private void OnJointBreak()
	{
		noJoint = true;
		FragmentVisualController.EmitJointBreakMarker(base.transform.position);
	}
}
