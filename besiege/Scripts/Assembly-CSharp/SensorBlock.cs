using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Logic/SensorBlock")]
public class SensorBlock : BlockBehaviour
{
	public LayerMask sensorMask;

	public LayerMask floorMask;

	private LayerMask fullMask;

	public Transform sensorPos;

	public Color ledColor;

	public Transform sphereTop;

	public Transform sphereBottom;

	public Transform cylinder;

	protected MSlider distanceSlider;

	protected MSlider proximitySlider;

	protected MSlider radiusSlider;

	protected MToggle nonAuto;

	protected MToggle holdToDetect;

	protected MToggle inverted;

	protected MToggle ignoreStatic;

	protected MToggle detectWater;

	protected MKey activateKey;

	protected MKey emulateKey;

	private int overlapCount;

	private bool seenWater;

	private bool toggle;

	private bool isDetecting;

	private bool capsuleShown;

	private bool flashingLED;

	protected MKey[] activationKeys;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool activateHeld;

	private bool emuActivateHeld;

	private bool emulating;

	public MSlider DistanceSlider
	{
		get
		{
			return distanceSlider;
		}
	}

	public MSlider ProximitySlider
	{
		get
		{
			return proximitySlider;
		}
	}

	public MSlider RadiusSllider
	{
		get
		{
			return radiusSlider;
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

	public MToggle IgnoreStatic
	{
		get
		{
			return ignoreStatic;
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

	protected Vector3 forward
	{
		get
		{
			return -base.transform.up;
		}
	}

	public bool IsDetecting
	{
		get
		{
			return isDetecting;
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
		ignoreStatic = AddToggle(3858, "ignore-static", false);
		detectWater = AddToggle("detect water", "detect-water", false);
		detectWater.DisplayInMapper = WaterController.Exist;
		distanceSlider = AddSlider(3773, "distance", 5f, 0.5f, 50f, string.Empty);
		proximitySlider = AddSlider(4968, "proximity", 0f, 0f, 25f, string.Empty);
		proximitySlider.logScaling = true;
		radiusSlider = AddSlider(3774, "radius", 0.5f, 0.25f, 2f, string.Empty);
		activateKey.DisplayInMapper = nonAuto.IsActive;
		holdToDetect.DisplayInMapper = nonAuto.IsActive;
		nonAuto.Toggled += AutoToggle;
		activationKeys = new MKey[1] { activateKey };
		fullMask = (int)sensorMask | (int)floorMask;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	protected void AutoToggle(bool isactive)
	{
		activateKey.DisplayInMapper = isactive;
		holdToDetect.DisplayInMapper = isactive;
	}

	protected void EvaluateSensor()
	{
		float r;
		float trueDist;
		Vector3 start;
		Vector3 end;
		GetSensorArea(out start, out end, out r, out trueDist);
		LayerMask layerMask = fullMask;
		if (ignoreStatic.IsActive)
		{
			layerMask = sensorMask;
		}
		Collider[] array = ((!(trueDist < float.Epsilon)) ? Physics.OverlapCapsule(start, end, r, sensorMask) : Physics.OverlapSphere(start, r, layerMask));
		int num = 0;
		foreach (Collider collider in array)
		{
			if (collider.isTrigger)
			{
				if (collider.transform.root != SingleInstanceFindOnly<AddPiece>.Instance.PhysicsGoalObject.root && collider.transform.root != ReferenceMaster.physicsGoalInstance)
				{
					continue;
				}
				if (StatMaster.isMP)
				{
					if (collider.gameObject.CompareTag("Insignia") || collider.gameObject.CompareTag("SpawnZone"))
					{
						continue;
					}
				}
				else if ((bool)collider.gameObject.GetComponentInParent<FinishLine>())
				{
					continue;
				}
			}
			if (!(collider.transform != base.transform))
			{
				continue;
			}
			if (ignoreStatic.IsActive)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (!attachedRigidbody || attachedRigidbody.isKinematic)
				{
					continue;
				}
			}
			num++;
		}
		overlapCount = num;
		if (overlapCount == 0 && trueDist > float.Epsilon && !ignoreStatic.IsActive)
		{
			if (Physics.CheckSphere(end, r, floorMask, QueryTriggerInteraction.Ignore) || Physics.CheckSphere(start, r, floorMask, QueryTriggerInteraction.Ignore))
			{
				overlapCount = 1;
			}
			if (overlapCount == 0)
			{
				Vector3 vector = Vector3.ProjectOnPlane(Vector3.down, base.transform.up).normalized * r;
				Vector3 vector2 = start + vector;
				Vector3 vector3 = end + vector;
				Vector3 vector4 = vector3 - vector2;
				overlapCount = (Physics.Raycast(vector2, vector4.normalized, vector4.magnitude, floorMask, QueryTriggerInteraction.Ignore) ? 1 : 0);
			}
		}
		if (overlapCount == 0 && trueDist > float.Epsilon && !ignoreStatic.IsActive)
		{
			overlapCount = (Physics.CheckCapsule(start, end, r, floorMask) ? 1 : 0);
		}
		if (overlapCount == 0 && detectWater.IsActive && WaterController.Exist)
		{
			trueDist %= 180f;
			start = sensorPos.position;
			end = start + forward * (trueDist + r);
			base.InWater = WaterController.IsUnderwater(start);
			Vector3 vector5 = ((!base.InWater) ? Vector3.down : Vector3.up);
			if (trueDist > r)
			{
				bool exitedEarly = false;
				bool flag = WaterController.IsUnderwater(start + vector5 * r, ref exitedEarly);
				bool flag2 = WaterController.IsUnderwater(end + vector5 * r, ref exitedEarly);
				if (flag == base.InWater && flag2 == base.InWater && exitedEarly)
				{
					float num2 = Mathf.Abs(Vector3.Dot(Vector3.up, forward));
					if (num2 < 0.5f)
					{
						end = start + forward * trueDist * 0.5f;
						flag2 = WaterController.IsUnderwater(end + vector5 * r);
					}
				}
				seenWater = flag != base.InWater || flag2 != base.InWater;
			}
			else
			{
				bool flag3 = WaterController.IsUnderwater(end + vector5 * r);
				seenWater = base.InWater != flag3;
			}
		}
		else
		{
			seenWater = false;
		}
	}

	protected void ShowSensorArea()
	{
		if (!capsuleShown)
		{
			sphereTop.gameObject.SetActive(true);
			sphereBottom.gameObject.SetActive(true);
			cylinder.gameObject.SetActive(true);
		}
		capsuleShown = true;
		UpdateSensorAreaDisplay(0f);
	}

	protected void HideSensorArea()
	{
		if (capsuleShown)
		{
			capsuleShown = false;
			sphereTop.gameObject.SetActive(false);
			sphereBottom.gameObject.SetActive(false);
			cylinder.gameObject.SetActive(false);
		}
	}

	protected void UpdateSensorAreaDisplay(float x)
	{
		Vector3 start;
		Vector3 end;
		float r;
		float trueDist;
		GetSensorArea(out start, out end, out r, out trueDist);
		ShowOverlapCapsule(start, end, r, trueDist);
	}

	protected void GetSensorArea(out Vector3 start, out Vector3 end, out float r, out float trueDist)
	{
		r = radiusSlider.Value;
		float num = distanceSlider.Value;
		float num2 = proximitySlider.Value;
		if (float.IsNaN(num))
		{
			num = 0f;
		}
		if (float.IsNaN(num2))
		{
			num2 = 0f;
		}
		trueDist = num - r * 2f;
		if (trueDist < 0f)
		{
			trueDist = 0f;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		start = sensorPos.position + forward * (num2 + r);
		end = sensorPos.position + forward * (num2 + trueDist + r);
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
				ShowSensorArea();
			}
			else
			{
				HideSensorArea();
			}
			isDetecting = false;
		}
		else if (SimPhysics && Time.timeScale != 0f && base.ParentMachine.isReady)
		{
			activatePressed = activateKey.IsPressed;
			activateHeld = activateKey.IsHeld;
			UpdateIsDetectingState(activatePressed, activateHeld || emuActivateHeld);
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
		if (!_parentMachine.isReady)
		{
			return;
		}
		if (!isDetecting)
		{
			StopEmulation();
			return;
		}
		EvaluateSensor();
		bool flag = overlapCount > 0 || seenWater;
		if ((!inverted.IsActive) ? flag : (!flag))
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

	protected override void OnDisable()
	{
		base.OnDisable();
		if (emulating)
		{
			EmulateKeys(false);
		}
	}

	protected void EmulateKeys(bool emulate)
	{
		emulating = emulate;
		EmulateKeys(activationKeys, emulateKey, emulate);
		ToggleLED(emulate);
	}

	public override void OnRemoteEmulate(MKey key, bool emulate)
	{
		ToggleLED(emulate);
	}

	private void ToggleLED(bool toggle)
	{
		if (flashingLED != toggle)
		{
			VisualController.AssignMaterialColor("_EmissCol", (!toggle) ? Color.black : ledColor);
			flashingLED = toggle;
		}
	}

	public void ShowOverlapCapsule(Vector3 start, Vector3 end, float radius, float height)
	{
		Vector3 vector = new Vector3(1f / base.transform.localScale.x, 1f / base.transform.localScale.y, 1f / base.transform.localScale.z);
		Vector3 position = (end + start) / 2f;
		Vector3 vector2 = forward;
		sphereTop.position = start;
		sphereTop.rotation = Quaternion.LookRotation(vector2) * Quaternion.Euler(270f, 0f, 0f);
		sphereTop.localScale = vector * radius * 2f;
		sphereBottom.position = end;
		sphereBottom.rotation = Quaternion.LookRotation(vector2) * Quaternion.Euler(90f, 0f, 0f);
		sphereBottom.localScale = vector * radius * 2f;
		cylinder.position = position;
		cylinder.rotation = Quaternion.LookRotation(vector2) * Quaternion.Euler(90f, 0f, 0f);
		cylinder.localScale = Vector3.Scale(vector, new Vector3(radius * 2f, height / 2f, radius * 2f));
	}
}
