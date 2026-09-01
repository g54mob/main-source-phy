using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Logic/AltimeterBlock")]
public class AltimeterBlock : BlockBehaviour
{
	public Transform hand;

	public MeshRenderer dial;

	public Color ledColor;

	public LineRenderer line;

	public Transform bar;

	protected MSlider heightSlider;

	protected MToggle nonAuto;

	protected MToggle holdToDetect;

	protected MToggle inverted;

	protected MKey activateKey;

	protected MKey emulateKey;

	private Color red;

	private Color grey;

	private bool toggle;

	private bool emulating;

	private bool ledActive;

	private bool isDetecting;

	protected MKey[] activationKeys;

	private bool visualisingHeight;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool activateHeld;

	private bool emuActivateHeld;

	public MSlider HeightSlider
	{
		get
		{
			return heightSlider;
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

	public float floorHeight
	{
		get
		{
			if (WaterController.Exist && (!StatMaster.isMP || LevelEditor.Instance.environmentManager.currentEnv == LevelSettings.LevelEnvironment.Water))
			{
				return WaterController.waterTransformHeight;
			}
			return SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
		}
	}

	public float Height
	{
		get
		{
			return base.transform.TransformPoint(Vector3.forward * 0.25f).y - floorHeight;
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
		heightSlider = AddSliderUnclamped(3783, "height", 5f, 0.5f, 250f, string.Empty, string.Empty);
		heightSlider.logScaling = true;
		activateKey.DisplayInMapper = nonAuto.IsActive;
		holdToDetect.DisplayInMapper = nonAuto.IsActive;
		nonAuto.Toggled += AutoToggle;
		red = dial.material.GetColor("_TintColor");
		float num = (red.r + red.g + red.b) / 3f;
		grey = Color.white * num * 0.9f;
		activationKeys = new MKey[1] { activateKey };
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

	protected void ShowVisualisation()
	{
		if (!visualisingHeight)
		{
			line.gameObject.SetActive(true);
			bar.gameObject.SetActive(true);
		}
		visualisingHeight = true;
		UpdateVisualisation();
	}

	protected void HideVisualisation()
	{
		if (visualisingHeight)
		{
			line.gameObject.SetActive(false);
			bar.gameObject.SetActive(false);
			visualisingHeight = false;
		}
	}

	protected void UpdateVisualisation()
	{
		float y = heightSlider.Value + floorHeight;
		Vector4 vector = new Vector4(base.transform.position.x, y, base.transform.position.z, 0f);
		bar.position = vector;
		SetDirectionalLine(line, base.transform.position, vector);
	}

	private void SetDirectionalLine(LineRenderer ren, Vector3 pos1, Vector3 pos2)
	{
		ren.SetPosition(0, pos1);
		ren.SetPosition(1, pos2);
		ren.material.mainTextureScale = new Vector2(Vector3.Distance(pos1, pos2), 1f);
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
				ShowVisualisation();
			}
			else
			{
				HideVisualisation();
			}
			isDetecting = false;
		}
		else if (base.ParentMachine.isReady && Time.timeScale != 0f)
		{
			activatePressed = activateKey.IsPressed;
			activateHeld = activateKey.IsHeld;
			UpdateIsDetectingState(activatePressed, activateHeld || emuActivateHeld);
			AnimateHand(Height, heightSlider.Value, isDetecting);
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
		if (SimPhysics && _parentMachine.isReady)
		{
			if (!isDetecting)
			{
				StopEmulation();
			}
			else if ((!inverted.IsActive) ? (Height > heightSlider.Value) : (Height < heightSlider.Value))
			{
				StartEmulation();
			}
			else
			{
				StopEmulation();
			}
		}
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

	public override void OnRemoteEmulate(MKey key, bool emulate)
	{
		ToggleLED(emulate);
	}

	public void EmulateKeys(bool emulate)
	{
		emulating = emulate;
		EmulateKeys(activationKeys, emulateKey, emulate);
		ToggleLED(emulate);
	}

	private void ToggleLED(bool active)
	{
		if (active != ledActive)
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

	protected void AnimateHand(float currentHeight, float targetHeight, bool active)
	{
		float num = ((!(currentHeight < targetHeight)) ? (0.5f + Mathf.InverseLerp(targetHeight, targetHeight * 2f + 0.25f, currentHeight) * 0.5f) : (Mathf.InverseLerp(0.25f, targetHeight, currentHeight) * 0.5f));
		Vector3 localEulerAngles = hand.localEulerAngles;
		hand.localEulerAngles = new Vector3(localEulerAngles.x, -32f + num * 64f, localEulerAngles.z);
		dial.gameObject.SetActive(true);
		float num2 = (num - 0.9f) * 10f;
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		dial.material.SetFloat("_Progress", 0.125f + num * 0.75f + num2 * 0.125f);
		dial.material.SetColor("_TintColor", (!active) ? grey : red);
	}
}
