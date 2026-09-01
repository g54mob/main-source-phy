using System.Collections.Generic;
using Localisation;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Logic/AnglometerBlock")]
public class AnglometerBlock : BlockBehaviour
{
	public MeshFilter hand;

	public MeshRenderer dial;

	public Color ledColor;

	public Mesh compassMesh;

	public Mesh gravityMesh;

	protected MSlider startSlider;

	protected MSlider stopSlider;

	protected MToggle nonAuto;

	protected MToggle holdToDetect;

	protected MToggle inverted;

	protected MMenu alignmentMode;

	protected MKey activateKey;

	protected MKey emulateKey;

	private Color red;

	private Color grey;

	private bool toggle;

	private bool emulating;

	private bool isDetecting;

	private bool ledActive;

	protected MKey[] activationKeys;

	private Vector3 targetDir = Vector3.up;

	private float start;

	private float end;

	private bool startIsEnd;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool activateHeld;

	private bool emuActivateHeld;

	public MSlider StartSlider
	{
		get
		{
			return startSlider;
		}
	}

	public MSlider StopSlider
	{
		get
		{
			return stopSlider;
		}
	}

	public MToggle NonAuto
	{
		get
		{
			return nonAuto;
		}
	}

	public MToggle HoldToDetect
	{
		get
		{
			return holdToDetect;
		}
	}

	public MToggle Inverted
	{
		get
		{
			return inverted;
		}
	}

	public MKey ActivateKey
	{
		get
		{
			return activateKey;
		}
	}

	public MKey EmulateKey
	{
		get
		{
			return emulateKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		activateKey = AddKey(3768, "activate", ControlScheme.BlockControls.Activate, 0, KeyCode.B);
		emulateKey = AddEmulatorKey(3769, "emulate", ControlScheme.BlockControls.Automate, 0, KeyCode.C);
		nonAuto = AddToggle(3770, "non-automatic", false);
		holdToDetect = AddToggle(3771, "hold-to-activate", true);
		inverted = AddToggle(3772, "reverse", false);
		alignmentMode = AddMenu("alignment", 0, new List<string>
		{
			LocalisationManager.GetTranslation(5088),
			LocalisationManager.GetTranslation(5089),
			LocalisationManager.GetTranslation(5090)
		});
		startSlider = AddSliderLooped(2399, "start-a", 45f, -180f, 180f, string.Empty, "°");
		stopSlider = AddSliderLooped(2400, "end-a", -45f, -180f, 180f, string.Empty, "°");
		alignmentMode.ValueChanged += ChangeHand;
		activateKey.DisplayInMapper = nonAuto.IsActive;
		holdToDetect.DisplayInMapper = nonAuto.IsActive;
		nonAuto.Toggled += AutoToggle;
		red = dial.material.GetColor("_TintColor");
		float num = (red.r + red.g + red.b) / 3f;
		grey = Color.white * num * 0.9f;
		activationKeys = new MKey[1] { activateKey };
		if (isSimulating)
		{
			targetDir = base.ParentMachine.MachineSpawnRotation * base.transform.up;
			if (SimPhysics)
			{
				Rigidbody.maxAngularVelocity = 50f;
			}
		}
	}

	public void ChangeHand(int mode)
	{
		hand.sharedMesh = ((mode != 1) ? compassMesh : gravityMesh);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		UpdateVisualisation();
		if (isSimulating)
		{
			SetupValuesForSim();
		}
	}

	protected void AutoToggle(bool isactive)
	{
		activateKey.DisplayInMapper = isactive;
		holdToDetect.DisplayInMapper = isactive;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	protected void UpdateVisualisation()
	{
		dial.gameObject.SetActive(true);
		float value = startSlider.Value;
		value = ((!(value < 0f)) ? value : (360f + value));
		float value2 = stopSlider.Value;
		value2 = ((!(value2 < 0f)) ? value2 : (360f + value2));
		if (Mathf.Approximately(value, value2))
		{
			value -= 2f;
			value2 += 2f;
		}
		value /= 360f;
		value2 /= 360f;
		dial.material.SetFloat("_Start", value);
		dial.material.SetFloat("_Stop", value2);
	}

	private void SetupValuesForSim()
	{
		start = ClampAngle(startSlider.Value);
		end = ClampAngle(stopSlider.Value);
		startIsEnd = Mathf.Approximately(start, end);
		if (!startIsEnd)
		{
			end -= start;
			end = ClampAngle(end);
		}
	}

	private bool IsBetweenLimits(float mid)
	{
		mid = ClampAngle(mid);
		if (startIsEnd)
		{
			return mid > end - 0.5f && mid < end + 0.5f;
		}
		mid -= start;
		mid = ClampAngle(mid);
		return mid < end;
	}

	private float ClampAngle(float a)
	{
		return (!(a < 0f)) ? a : (a + 360f);
	}

	public override void EmulationUpdateBlock()
	{
		emuActivatePressed = activateKey.EmulationPressed();
		emuActivateHeld = activateKey.EmulationHeld(true);
		UpdateIsDetectingState(emuActivatePressed, emuActivateHeld || activateHeld);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!isSimulating)
		{
			if (BlockMapper.IsOpen && BlockMapper.CurrentInstance.Current == this)
			{
				UpdateVisualisation();
			}
			isDetecting = false;
		}
		else
		{
			if (!base.ParentMachine.isReady || Time.timeScale == 0f)
			{
				return;
			}
			activatePressed = activateKey.IsPressed;
			activateHeld = activateKey.IsHeld;
			UpdateIsDetectingState(activatePressed, activateHeld || emuActivateHeld);
			if (_parentMachine.isReady)
			{
				if (StatMaster.isClient)
				{
					targetDir = CalculateTargetDir(targetDir);
				}
				AnimateHand(targetDir, isDetecting);
			}
		}
	}

	private void UpdateIsDetectingState(bool pressed, bool held)
	{
		if (!nonAuto.IsActive)
		{
			isDetecting = true;
			return;
		}
		if (holdToDetect.IsActive)
		{
			isDetecting = held;
			return;
		}
		if (pressed)
		{
			toggle = !toggle;
		}
		isDetecting = toggle;
	}

	public override void SendEmulationUpdateBlock()
	{
		if (!SimPhysics || !_parentMachine.isReady)
		{
			return;
		}
		targetDir = CalculateTargetDir(targetDir);
		if (!isDetecting)
		{
			StopEmulation();
			return;
		}
		float mid = CorrectedAngle(targetDir);
		bool flag = IsBetweenLimits(mid);
		if (inverted.IsActive)
		{
			flag = !flag;
		}
		if (flag)
		{
			StartEmulation();
		}
		else
		{
			StopEmulation();
		}
	}

	private Vector3 CalculateTargetDir(Vector3 currentTargetDir)
	{
		switch (alignmentMode.Value)
		{
		case 0:
			return currentTargetDir;
		case 1:
			return Vector3.up;
		case 2:
		{
			Vector3 vector = (SimPhysics ? Rigidbody.velocity : NetBlock.Velocity);
			float magnitude = vector.magnitude;
			return (!(magnitude < 0.1f)) ? LerpAroundAxis(currentTargetDir, vector / magnitude, base.transform.forward, magnitude * Time.fixedDeltaTime) : currentTargetDir;
		}
		default:
			Debug.LogError("Velocity mode " + alignmentMode.Value + " not implemented");
			return currentTargetDir;
		}
	}

	private Vector3 LerpAroundAxis(Vector3 from, Vector3 to, Vector3 axis, float t)
	{
		from -= axis * Vector3.Dot(from, axis);
		to -= axis * Vector3.Dot(to, axis);
		from.Normalize();
		to.Normalize();
		float b = SignedAngle(from, to, axis);
		float angle = Mathf.Lerp(0f, b, t);
		return Quaternion.AngleAxis(angle, axis) * from;
	}

	private float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
	{
		float num = Vector3.Angle(from, to);
		float num2 = Mathf.Sign(Vector3.Dot(Vector3.Cross(from, to), axis));
		return num * num2;
	}

	public float CurrentAngle(Vector3 dir)
	{
		Vector3 a = base.transform.InverseTransformDirection(dir);
		a = Vector3.Scale(a, new Vector3(1f, 1f, 0f));
		float num = ((a.x != 0f) ? (a.x / Mathf.Abs(a.x)) : 1f);
		return (0f - Vector3.Angle(a, Vector3.up)) * num;
	}

	private float StopFalsePositivesWhenDialAxisAlignsWithTargetDir()
	{
		Vector3 forward = base.transform.forward;
		Vector3 rhs = targetDir;
		float num = Vector3.Dot(forward, rhs);
		num = ((!(num < 0f)) ? num : (0f - num));
		return Mathf.InverseLerp(0.9f, 1f, num);
	}

	private float CorrectedAngle(Vector3 dir)
	{
		float num = ClampAngle(startSlider.Value);
		float num2 = ClampAngle(stopSlider.Value);
		num2 -= num;
		num2 = ClampAngle(num2);
		float num3 = num2 / 2f;
		num3 = ClampAngle(num3 - 180f + num);
		return Mathf.Lerp(ClampAngle(CurrentAngle(dir)), num3, StopFalsePositivesWhenDialAxisAlignsWithTargetDir());
	}

	public void StartEmulation()
	{
		if (!emulating)
		{
			EmulateKeys(true);
		}
	}

	public void StopEmulation()
	{
		if (emulating)
		{
			EmulateKeys(false);
		}
	}

	public void EmulateKeys(bool emulate)
	{
		emulating = emulate;
		EmulateKeys(activationKeys, emulateKey, emulate);
		ToggleLED(emulate);
	}

	public override void OnRemoteEmulate(MKey key, bool emulate)
	{
		ToggleLED(emulate);
	}

	private void ToggleLED(bool active)
	{
		if (ledActive != active)
		{
			VisualController.AssignMaterialColor("_EmissCol", (!active) ? Color.black : ledColor);
			ledActive = active;
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (emulating)
		{
			EmulateKeys(false);
		}
	}

	protected void AnimateHand(Vector3 dir, bool active)
	{
		float y = CorrectedAngle(dir);
		hand.transform.localEulerAngles = new Vector3(0f, y, 0f);
		dial.material.SetColor("_TintColor", (!active) ? grey : red);
	}
}
