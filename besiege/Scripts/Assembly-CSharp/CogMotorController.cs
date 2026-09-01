using System.Collections;
using Localisation;
using UnityEngine;

public class CogMotorController : BlockBehaviour, ILimitsDisplay
{
	public Transform limitsVisual;

	public bool startOn;

	public bool allowControl = true;

	public bool enableLimits;

	public float degreesPerSecond = 1f;

	public ConfigurableJoint myJoint;

	public float maxAngularVel = 50f;

	public float defaultSpeed = 1f;

	public float minSpeed;

	public float maxSpeed = 2f;

	public float speedLerpSmooth = 26f;

	public AudioSource audioSource;

	protected MLimits limitsSlider;

	private float input;

	private MToggle automaticToggle;

	private MToggle toggleMode;

	private MToggle autoBreakMode;

	private MKey forwardKey;

	private MKey backwardKey;

	private MSlider speedSlider;

	private float lastVelocity;

	public float Input
	{
		get
		{
			return input;
		}
	}

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
			return autoBreakMode;
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
		Debug.LogError("using CogMotorController instead of CogMotorControllerHinge, the former is out of date");
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		if (allowControl)
		{
			automaticToggle = AddToggle(2430, "automatic", startOn);
			toggleMode = AddToggle(2431, "toggle-mode", false);
			autoBreakMode = AddToggle(2432, "auto-brake", true);
			automaticToggle.Toggled += delegate(bool isActive)
			{
				toggleMode.DisplayInMapper = !isActive;
			};
			toggleMode.DisplayInMapper = !startOn;
			forwardKey = AddKey(2433, "forward", KeyCode.UpArrow);
			backwardKey = AddKey(2434, "backward", KeyCode.DownArrow);
		}
		if (enableLimits)
		{
			limitsSlider = AddLimits(LocalisationManager.GetTranslation(2435), "limits", 90f, 90f, 180f, this);
		}
		speedSlider = AddSlider(2428, "speed", defaultSpeed, minSpeed, maxSpeed, string.Empty);
	}

	protected override void Start()
	{
		base.Start();
		StartCoroutine(StartController());
	}

	private IEnumerator StartController()
	{
		myJoint = GetComponent<ConfigurableJoint>();
		myJoint.targetAngularVelocity = new Vector3(0f, myJoint.targetAngularVelocity.y, myJoint.targetAngularVelocity.z);
		JointDrive xDrive = myJoint.angularXDrive;
		xDrive.positionDamper = 3.402823E+30f;
		myJoint.angularXDrive = xDrive;
		yield return new WaitForFixedUpdate();
		if (isSimulating && !noRigidbody)
		{
			Rigidbody.maxAngularVelocity = maxAngularVel;
		}
	}

	private void Update()
	{
		if (!isSimulating || myJoint == null)
		{
			return;
		}
		if (!allowControl || automaticToggle.IsActive)
		{
			input = -1f;
		}
		else if (toggleMode.IsActive)
		{
			if (forwardKey.IsPressed)
			{
				input = ((!(input > 0.9f)) ? (-1f) : 0f);
			}
			if (backwardKey.IsPressed)
			{
				input = ((!(input < -0.9f)) ? 1f : 0f);
			}
		}
		else if (forwardKey.IsHeld)
		{
			input = 0f - forwardKey.Value;
		}
		else if (backwardKey.IsHeld)
		{
			input = backwardKey.Value;
		}
		else
		{
			input = 0f;
		}
	}

	protected void FixedUpdate()
	{
		if (!isSimulating || noRigidbody || Rigidbody.isKinematic || myJoint == null)
		{
			return;
		}
		if (Mathf.Abs(input) < 0.05f)
		{
			input = 0f;
		}
		float b = input * speedSlider.Value * degreesPerSecond * (float)(-FlipInvert);
		float num = Mathf.Lerp(lastVelocity, b, Time.deltaTime * speedLerpSmooth);
		if (autoBreakMode != null && !autoBreakMode.IsActive && Mathf.Abs(input) < 0.05f)
		{
			if (lastVelocity > 0f)
			{
				num = Mathf.Min(num, -0.01f);
			}
			else if (lastVelocity < 0f)
			{
				num = Mathf.Max(num, 0.01f);
			}
		}
		myJoint.targetAngularVelocity = new Vector3(num, myJoint.targetAngularVelocity.y, myJoint.targetAngularVelocity.z);
		lastVelocity = num;
	}

	public Transform GetLimitsDisplay()
	{
		return limitsVisual;
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		return true;
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		data.Write("flipped", Flipped);
	}

	public override void OnLoad(XDataHolder data)
	{
		if (!data.HasKey("bmt-uselimits"))
		{
			data.Write("bmt-uselimits", false);
		}
		base.OnLoad(data);
		if (data.HasKey("flipped"))
		{
			Flipped = data.ReadBool("flipped");
			PostFlip(false, false);
		}
	}
}
