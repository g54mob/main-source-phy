using System;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Rope Measure")]
public class RopeMeasure : GenericDraggedBlock, ISnapable
{
	public Vector3 visRotationAngle;

	public MeshRenderer ropeVis;

	public MeshRenderer emisVis;

	public MeshRenderer dial;

	public Color ledColor;

	public float tiling = 1f;

	public float ropeThreshold;

	private GameObject cylinderGO;

	private float sqrLength = 1f;

	private bool snapped;

	private float sqrRopeThreshold;

	private float lastCylSqr = -1f;

	private Vector3 cylScale;

	private Vector2 cylTexScale;

	private Vector3 cylPos;

	private Vector3 cylRot;

	public ConfigurableJoint confJoint1;

	public ConfigurableJoint confJoint2;

	protected bool hasJoint1;

	protected bool hasJoint2;

	protected bool jointJustBroke;

	protected MSlider lengthSlider;

	protected MToggle nonAuto;

	protected MToggle holdToDetect;

	protected MToggle inverted;

	protected MToggle hideToggle;

	protected MKey activateKey;

	protected MKey emulateKey;

	private Color red;

	private Color grey;

	protected MKey[] activationKeys;

	private bool toggle;

	private bool emulating;

	private bool ledActive;

	private bool isDetecting;

	[HideInInspector]
	[SerializeField]
	protected bool hide;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool activateHeld;

	private bool emuActivateHeld;

	public MSlider LengthSlider
	{
		get
		{
			return lengthSlider;
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

	public MToggle Hide
	{
		get
		{
			return hideToggle;
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

	public override Vector3 GetCenter()
	{
		if (isSimulating)
		{
			return cylinder.position;
		}
		return base.GetCenter();
	}

	protected override void Awake()
	{
		cylinderGO = cylinder.gameObject;
		cylScale = new Vector3(radius, radius, radius);
		cylTexScale = Vector2.one;
		cylPos = Vector3.zero;
		cylRot = Vector3.zero;
		base.Awake();
		activateKey = AddKey(3768, "activate", ControlScheme.BlockControls.Activate, 0, KeyCode.B);
		emulateKey = AddEmulatorKey(3769, "emulate", ControlScheme.BlockControls.Automate, 0, KeyCode.C);
		nonAuto = AddToggle(3770, "non-automatic", false);
		holdToDetect = AddToggle(3771, "hold-to-activate", true);
		inverted = AddToggle(3772, "reverse", false);
		hideToggle = AddToggle(2499, "hide", false);
		lengthSlider = AddSliderUnclamped(4424, "length", 2f, 0.1f, 10f, string.Empty, string.Empty);
		lengthSlider.logScaling = true;
		lengthSlider.ValueChanged += SetLength;
		SetLength(lengthSlider.Value);
		inverted.Toggled += SetInverted;
		hideToggle.Toggled += delegate(bool hide)
		{
			this.hide = hide;
		};
		activateKey.DisplayInMapper = nonAuto.IsActive;
		holdToDetect.DisplayInMapper = nonAuto.IsActive;
		nonAuto.Toggled += AutoToggle;
		red = dial.material.GetColor("_TintColor");
		float num = (red.r + red.g + red.b) / 3f;
		grey = Color.white * num * 0.8f;
		grey.b *= 1.1f;
		activationKeys = new MKey[1] { activateKey };
		if (isSimulating)
		{
			if (hide)
			{
				cylinderGO.SetActive(false);
			}
			if (SimPhysics)
			{
				cylinder.SetParent(base.transform.parent, true);
				startPoint.SetParent(base.transform.parent, true);
				endPoint.SetParent(base.transform.parent, true);
				if (!stripped)
				{
					Rigidbody component = startInterpolater.GetComponent<Rigidbody>();
					RigidbodyInterpolation interpolation = RigidbodyInterpolation.Interpolate;
					endInterpolater.GetComponent<Rigidbody>().interpolation = interpolation;
					component.interpolation = interpolation;
				}
				cylScale *= Mathf.Min(base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
				return;
			}
		}
		sqrRopeThreshold = ropeThreshold * ropeThreshold;
	}

	protected void SetLength(float l)
	{
		sqrLength = l * l;
		float currentLength = cylinder.localScale.z * 2f;
		UpdateVisualisation(l, currentLength);
	}

	protected void AutoToggle(bool isactive)
	{
		activateKey.DisplayInMapper = isactive;
		holdToDetect.DisplayInMapper = isactive;
	}

	protected void UpdateVisualisation(float length, float currentLength)
	{
		float num = 0.1f;
		length -= num * 2f;
		float value = length / currentLength;
		if (inverted.IsActive)
		{
			dial.material.SetFloat("_Start", 0f);
			dial.material.SetFloat("_Progress", value);
		}
		else
		{
			dial.material.SetFloat("_Start", value);
			dial.material.SetFloat("_Progress", 1f);
		}
	}

	protected void SetInverted(bool isactive)
	{
		float currentLength = cylinder.localScale.z * 2f;
		UpdateVisualisation(lengthSlider.Value, currentLength);
	}

	public override void EmulationUpdateBlock()
	{
		emuActivatePressed = activateKey.EmulationPressed();
		emuActivateHeld = activateKey.EmulationHeld(true);
		UpdateIsDetectingState(emuActivatePressed, emuActivateHeld || activateHeld);
	}

	private void UpdateIsDetectingState(bool pressed, bool held)
	{
		bool flag = isDetecting;
		if (!nonAuto.IsActive)
		{
			isDetecting = true;
		}
		else if (holdToDetect.IsActive)
		{
			isDetecting = held;
		}
		else
		{
			if (pressed)
			{
				toggle = !toggle;
			}
			isDetecting = toggle;
		}
		if (isDetecting != flag)
		{
			dial.material.SetColor("_TintColor", (!isDetecting) ? grey : red);
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!isSimulating)
		{
			isDetecting = false;
		}
		else if (Time.timeScale != 0f)
		{
			activatePressed = activateKey.IsPressed;
			activateHeld = activateKey.IsHeld;
			UpdateIsDetectingState(activatePressed, activateHeld || emuActivateHeld);
		}
	}

	public override void SendEmulationUpdateBlock()
	{
		if (!SimPhysics || !_parentMachine.isReady)
		{
			return;
		}
		if (!isDetecting)
		{
			StopEmulation();
			return;
		}
		float sqrMagnitude = (endBody.position - startBody.position).sqrMagnitude;
		if ((!inverted.IsActive) ? (sqrMagnitude > sqrLength) : (sqrMagnitude < sqrLength))
		{
			StartEmulation();
		}
		else
		{
			StopEmulation();
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
			emisVis.material.SetColor("_EmissCol", (!active) ? Color.black : ledColor);
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

	public override void StartPhysics(bool isKinematic)
	{
		SetCenterOfMass();
		hasJoint1 = true;
		hasJoint2 = true;
		if (!SimPhysics)
		{
			hasJoint1 = false;
			hasJoint2 = false;
			return;
		}
		if (confJoint1 == null)
		{
			hasJoint1 = false;
		}
		else if (confJoint1.connectedBody == null)
		{
			UnityEngine.Object.Destroy(confJoint1);
			hasJoint1 = false;
		}
		if (confJoint2 == null)
		{
			hasJoint2 = false;
		}
		else if (confJoint2.connectedBody == null)
		{
			UnityEngine.Object.Destroy(confJoint2);
			hasJoint2 = false;
		}
		if (hasJoint1)
		{
			startVis.transform.parent = confJoint1.connectedBody.transform;
			AddVisToBlock(startVis);
		}
		else
		{
			UnityEngine.Object.Destroy(startVis.gameObject);
		}
		if (hasJoint2)
		{
			endVis.transform.parent = confJoint2.connectedBody.transform;
			AddVisToBlock(endVis);
		}
		else
		{
			UnityEngine.Object.Destroy(endVis.gameObject);
		}
		if (!hasJoint1 || !hasJoint2)
		{
			Snap();
		}
	}

	public void BreakJoint(Joint j)
	{
		if (SimPhysics)
		{
			if (hasJoint1 && j == confJoint1)
			{
				hasJoint1 = false;
				Snap();
			}
			else if (hasJoint2 && j == confJoint2)
			{
				hasJoint2 = false;
				Snap();
			}
		}
	}

	protected override void Start()
	{
		base.Start();
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	protected override void CreateCylinderBetweenPoints(Vector3 start, Vector3 end)
	{
		float num = end.x - start.x;
		float num2 = end.y - start.y;
		float num3 = end.z - start.z;
		float num4 = num * num + num2 * num2 + num3 * num3;
		if (num4 < float.Epsilon)
		{
			cylinder.localScale = Vector3.zero;
			return;
		}
		bool flag = cylinderGO.activeSelf;
		if (!isSimulating || num4 > sqrRopeThreshold)
		{
			if (!flag)
			{
				cylinderGO.SetActive(true);
				flag = true;
			}
			float num5 = 0.02f;
			float num6 = num4 - lastCylSqr;
			if (((!(num6 < 0f)) ? num6 : (0f - num6)) > ((!isSimulating) ? num5 : (-1f)))
			{
				float num7 = ((!(num4 < num5)) ? Mathf.Sqrt(num4) : 0.2f);
				cylScale.z = num7 * 0.5f;
				cylinder.localScale = cylScale;
				UpdateVisualisation(lengthSlider.Value, cylScale.z * 2f);
				if (ropeVis != null)
				{
					cylTexScale.x = tiling * num7;
					VisualController.SetTiling(new Vector4(cylTexScale.x, 1f, 0f, 0f));
				}
				lastCylSqr = num4;
			}
		}
		else if (flag)
		{
			cylinderGO.SetActive(false);
			flag = false;
		}
		if (flag)
		{
			cylPos.Set(start.x + num * 0.5f, start.y + num2 * 0.5f, start.z + num3 * 0.5f);
			cylRot.Set(num, num2, num3);
			cylinder.position = cylPos;
			if (!Mathf.Approximately(abs(num) + abs(num2) + abs(num3), 0f))
			{
				cylinder.rotation = Quaternion.LookRotation(-cylRot, Vector3.up);
			}
		}
	}

	public override void UpdateDragged()
	{
		SetDragged(startPoint.rotation);
	}

	public override void LateUpdateBlock()
	{
		if (!hide || !isSimulating)
		{
			CreateCylinderBetweenPoints(startInterpolater.position, endInterpolater.position);
		}
	}

	public void Snap()
	{
		if (!isSimulating || snapped)
		{
			return;
		}
		snapped = true;
		if (_parentMachine != null)
		{
			if (StatMaster.isMP && SimPhysics)
			{
				NetworkBlock netBlock = NetBlock;
				if (netBlock != null)
				{
					netBlock.Event(NetworkEntity.EntityEvent.Break);
					netBlock.pollTransform = false;
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
			_parentMachine.UnregisterUpdate(this, false);
			_parentMachine.UnregisterLateUpdate(this, false);
			_parentMachine.UnregisterEmulationUpdate(this);
		}
		if (emulating)
		{
			EmulateKeys(false);
		}
		IsDestroyed = true;
		base.gameObject.SetActive(false);
		cylinderGO.SetActive(false);
	}

	private void AddVisToBlock(Renderer r)
	{
		BlockBehaviour component = r.transform.parent.GetComponent<BlockBehaviour>();
		if (!object.ReferenceEquals(component, null))
		{
			component.visAddedToMe.Add(r.GetComponent<Renderer>());
		}
	}

	public override bool Set(bool forceFail = false)
	{
		return base.Set(forceFail);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
	}
}
