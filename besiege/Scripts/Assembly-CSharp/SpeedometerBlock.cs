using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Logic/SpeedometerBlock")]
public class SpeedometerBlock : BlockBehaviour
{
	public enum Direction
	{
		All = 0,
		ForwardAndBackward = 1,
		Forward = 2,
		Backward = 3
	}

	public Transform hand;

	public MeshRenderer dial;

	public Color ledColor;

	public LineRenderer line;

	public Transform bar;

	protected MSlider speedSlider;

	protected MToggle nonAuto;

	protected MToggle holdToDetect;

	protected MToggle inverted;

	protected MToggle angular;

	protected MMenu direction;

	protected MKey activateKey;

	protected MKey emulateKey;

	private Color red;

	private Color grey;

	private bool toggle;

	private bool emulating;

	private bool isDetecting;

	private bool ledActive;

	private Vector3 lastPosition;

	private Quaternion lastRotation;

	private Vector3 inferredVelocity;

	private Vector3 smoothVel1;

	private Vector3 smoothVel2;

	protected MKey[] activationKeys;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool activateHeld;

	private bool emuActivateHeld;

	public MSlider HeightSlider
	{
		get
		{
			return speedSlider;
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
		speedSlider = AddSliderUnclamped(2350, "speed-threshold", 5f, 0f, 250f, string.Empty, string.Empty);
		angular = AddToggle(4971, "angular", false);
		direction = AddMenu("directional", 0, GetDirectionLabels());
		angular.Toggled += AngularToggle;
		AngularToggle(false);
		activateKey.DisplayInMapper = nonAuto.IsActive;
		holdToDetect.DisplayInMapper = nonAuto.IsActive;
		nonAuto.Toggled += AutoToggle;
		red = dial.material.GetColor("_TintColor");
		float num = (red.r + red.g + red.b) / 3f;
		grey = Color.white * num * 0.9f;
		activationKeys = new MKey[1] { activateKey };
		if (SimPhysics)
		{
			Rigidbody.centerOfMass = new Vector3(0f, 0f, Rigidbody.centerOfMass.z);
		}
	}

	protected void AutoToggle(bool isactive)
	{
		activateKey.DisplayInMapper = isactive;
		holdToDetect.DisplayInMapper = isactive;
	}

	protected void AngularToggle(bool e)
	{
		if ((!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim) && isSimulating)
		{
			Rigidbody.angularDrag = 0f;
			Rigidbody.maxAngularVelocity = 100f;
		}
		if ((bool)BlockMapper.CurrentInstance)
		{
			direction.Items = GetDirectionLabels();
			BlockMapper.CurrentInstance.Refresh();
		}
	}

	private List<string> GetDirectionLabels()
	{
		List<string> list = new List<string>();
		list.Add(LocalisationManager.GetTranslation(5010));
		list.Add(LocalisationManager.GetTranslation((!angular.IsActive) ? 5016 : 5017));
		list.Add(LocalisationManager.GetTranslation((!angular.IsActive) ? 5011 : 5014));
		list.Add(LocalisationManager.GetTranslation((!angular.IsActive) ? 5012 : 5015));
		return list;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
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
			isDetecting = false;
		}
		else
		{
			if (Time.timeScale == 0f || !base.ParentMachine.isReady)
			{
				return;
			}
			activatePressed = activateKey.IsPressed;
			activateHeld = activateKey.IsHeld;
			UpdateIsDetectingState(activatePressed, activateHeld || emuActivateHeld);
			if (noRigidbody)
			{
				if (angular.IsActive)
				{
					Quaternion rotation = VisualController.MeshFilter.transform.rotation;
					float angle;
					(rotation * Quaternion.Inverse(lastRotation)).ToAngleAxis(out angle, out inferredVelocity);
					inferredVelocity = inferredVelocity * angle * ((float)Math.PI / 180f) / Time.deltaTime;
					lastRotation = rotation;
				}
				else
				{
					Vector3 position = VisualController.MeshFilter.transform.position;
					inferredVelocity = (position - lastPosition) / Time.deltaTime;
					lastPosition = position;
				}
				inferredVelocity = (inferredVelocity + smoothVel1 + smoothVel2) * 0.3334f;
				smoothVel2 = smoothVel1;
				smoothVel1 = inferredVelocity;
			}
			float value = speedSlider.Value;
			AnimateHand(GetSqrSpeed(), value * value, isDetecting);
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
		if (!isDetecting)
		{
			StopEmulation();
			return;
		}
		float num = speedSlider.Value * speedSlider.Value;
		float sqrSpeed = GetSqrSpeed();
		if ((!inverted.IsActive) ? (sqrSpeed > num) : (sqrSpeed < num))
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

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!StatMaster.isPaste)
		{
			RecoverOldSpeed(data);
		}
	}

	private float GetSqrSpeed()
	{
		Vector3 vector = (noRigidbody ? inferredVelocity : ((!angular.IsActive) ? Rigidbody.velocity : Rigidbody.angularVelocity));
		Direction value = (Direction)direction.Value;
		if (value == Direction.All)
		{
			return vector.sqrMagnitude;
		}
		Vector3 vector2 = base.transform.InverseTransformDirection(vector);
		float num = ((!angular.IsActive) ? vector2.y : vector2.z);
		float b;
		switch (value)
		{
		case Direction.ForwardAndBackward:
			b = num * num;
			break;
		case Direction.Forward:
			b = Mathf.Sign(num) * num * num;
			break;
		case Direction.Backward:
			b = (0f - Mathf.Sign(num)) * num * num;
			break;
		default:
			throw new InvalidOperationException(value.ToString());
		}
		return Mathf.Max(0f, b);
	}

	private void RecoverOldSpeed(XDataHolder data)
	{
		string key = "bmt-speed";
		if (data.HasKey(key))
		{
			speedSlider.DeSerialize(data.Read(key));
		}
	}

	protected void AnimateHand(float currentSpeed, float targetSpeed, bool active)
	{
		float value = ((!(currentSpeed < targetSpeed)) ? (0.5f + Mathf.InverseLerp(targetSpeed, targetSpeed * 2f + 0.25f, currentSpeed) * 0.5f) : (Mathf.InverseLerp(0f, targetSpeed, currentSpeed) * 0.5f));
		value = ((!inverted.IsActive) ? Mathf.Clamp(value, (!ledActive) ? 0f : 0.5f, (!ledActive) ? 0.5f : 1f) : Mathf.Clamp(value, (!ledActive) ? 0.5f : 0f, (!ledActive) ? 1f : 0.5f));
		Vector3 localEulerAngles = hand.localEulerAngles;
		hand.localEulerAngles = new Vector3(localEulerAngles.x, -32f + value * 64f, localEulerAngles.z);
		dial.gameObject.SetActive(true);
		float num = (value - 0.9f) * 10f;
		if (num < 0f)
		{
			num = 0f;
		}
		dial.material.SetFloat("_Progress", 0.125f + value * 0.75f + num * 0.125f);
		dial.material.SetColor("_TintColor", (!active) ? grey : red);
	}
}
