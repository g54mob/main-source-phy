using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/FixedCameraBlock")]
public class FixedCameraBlock : BlockBehaviour, ILocalisationAware
{
	public enum Mode
	{
		Car = 0,
		Plane = 1,
		FirstPerson = 2,
		Custom = 3
	}

	[Serializable]
	public class Settings
	{
		public float PositionLerp = 0.2f;

		public float RotationLerp = 10f;

		public float LookAheadAmount = 0.1f;

		public float ZoomSpeed = 1.5f;

		public float UserRotationResetSpeed = 5f;

		public float ThirdPersonMaxZoomDistance = 20f;

		public float FirstPersonMinZoomFOV = 10f;

		public Vector2 ThirdPersonLookSpeed = new Vector2(1.5f, 1.5f);

		public Vector2 FirstPersonLookSpeed = new Vector2(1.5f, 1.5f);
	}

	private enum Tab
	{
		Position = 0,
		Rotation = 1,
		Settings = 2
	}

	private const float MIN_Y = -89.999f;

	private const float MAX_Y = 89.999f;

	[HideInInspector]
	public Vector3 targetPosition = Vector3.forward;

	[HideInInspector]
	public Quaternion targetRotation = Quaternion.identity;

	[HideInInspector]
	public Quaternion keepUpRotation = Quaternion.identity;

	[HideInInspector]
	public bool isActive;

	public TriggerSetParent1 CamTriggerSetParent;

	public Transform CompositeTracker;

	public Transform CompositeTracker2;

	public Transform CompositeTracker3;

	public Transform CompoundTracker;

	public Transform VisualTarget;

	public Transform PlacedTrans;

	public Transform UprightTrans;

	public Settings settings;

	public MSlider fovSlider;

	protected int prevMode;

	protected float fpsFov = 60f;

	protected float defFov = 41f;

	protected float fpsHeight = 90f;

	protected float defHeight = 18f;

	protected float fpsDistance = 1.5f;

	protected float defDistance = 32f;

	protected float fpsTilt = 80f;

	protected float defTilt;

	private Vector3 userRotation = Vector3.zero;

	private Vector3 dampendPosition;

	private Quaternion dampendRotation;

	private bool isApplicationQuitting;

	private Mode mode;

	private MouseOrbit cameraOrbit;

	private Transform cameraTransform;

	private MKey activateKey;

	private MSlider distanceSlider;

	private MSlider heightSlider;

	private MSlider rotationSlider;

	private MSlider tiltSlider;

	private MSlider rollSlider;

	private MSlider yawSlider;

	private MSlider smoothSlider;

	private MSlider predictSlider;

	private MMenu modeMenu;

	private MToggle advancedToggle;

	private MToggle limitToTerrain;

	private FixedCameraController camController;

	private bool stayUpRight = true;

	private Quaternion visTargRot = Quaternion.identity;

	private bool simulationClone;

	private Vector3 CamPos = Vector3.zero;

	private Vector3 lookAtPos = Vector3.zero;

	private Vector3 up = Vector3.up;

	private float sliderLerp;

	private float deltaTime = 0.02f;

	private Vector3 lastTransPos = Vector3.zero;

	public KeyCode KeyCode
	{
		get
		{
			return activateKey.GetKey(0);
		}
	}

	public Mode CamMode
	{
		get
		{
			return mode;
		}
	}

	public MSlider DistanceSlider
	{
		get
		{
			return distanceSlider;
		}
	}

	public MSlider HeightSlider
	{
		get
		{
			return heightSlider;
		}
	}

	public MSlider RotationSlider
	{
		get
		{
			return rotationSlider;
		}
	}

	public MSlider TiltSlider
	{
		get
		{
			return tiltSlider;
		}
	}

	public MSlider RollSlider
	{
		get
		{
			return rollSlider;
		}
	}

	public MSlider YawSlider
	{
		get
		{
			return yawSlider;
		}
	}

	public MSlider SmoothSlider
	{
		get
		{
			return smoothSlider;
		}
	}

	public MSlider PredictSlider
	{
		get
		{
			return predictSlider;
		}
	}

	public MMenu ModeMenu
	{
		get
		{
			return modeMenu;
		}
	}

	public MToggle AdvancedToggle
	{
		get
		{
			return advancedToggle;
		}
	}

	public MKey ActivateKey
	{
		get
		{
			return activateKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		isApplicationQuitting = false;
		activateKey = AddKey(2448, "activate", ControlScheme.BlockControls.Camera, 0, KeyCode.F);
		modeMenu = AddMenu("mode", 0, new List<string>
		{
			LocalisationManager.GetTranslation(2450),
			LocalisationManager.GetTranslation(2451),
			LocalisationManager.GetTranslation(2452),
			LocalisationManager.GetTranslation(2453)
		});
		modeMenu.ValueChanged += UpdateMode;
		advancedToggle = AddToggle(2454, "advanced-options", false);
		advancedToggle.Toggled += OnAdvancedModeToggled;
		limitToTerrain = AddToggle(4906, "limit-to-terrain", true);
		limitToTerrain.DisplayInMapper = !advancedToggle.DisplayInMapper || advancedToggle.IsActive;
		distanceSlider = AddSlider(2455, "distance", 32f, 1f, 80f, string.Empty);
		heightSlider = AddSlider(2456, "height", 18f, -90f, 90f, string.Empty);
		rotationSlider = AddSlider(2457, "rotation", 0f, -180f, 180f, string.Empty);
		tiltSlider = AddSlider(2458, "pitch", 0f, -180f, 180f, string.Empty);
		rollSlider = AddSlider(2459, "roll", 0f, -180f, 180f, string.Empty);
		yawSlider = AddSlider(2460, "yaw", 0f, -180f, 180f, string.Empty);
		smoothSlider = AddSlider(2461, "smooth", 0.75f, 0f, 1f, string.Empty);
		fovSlider = AddSlider(2462, "fov", 41f, 30f, 70f, string.Empty);
		predictSlider = AddSlider(3255, "predict", 2f, 0f, 10f, string.Empty);
		camController = SingleInstance<FixedCameraController>.Instance;
		MSlider mSlider = distanceSlider;
		bool flag = true;
		rotationSlider.DisplayInMapper = flag;
		flag = flag;
		heightSlider.DisplayInMapper = flag;
		mSlider.DisplayInMapper = flag;
		UpdateMode(0);
	}

	public override void StartPhysics(bool isKinematic)
	{
	}

	protected override void Start()
	{
		base.Start();
		if (_hasParentMachine)
		{
			up = _parentMachine.BuildingMachine.up;
		}
		if (SimPhysics && !noRigidbody)
		{
			Rigidbody.isKinematic = (isKinematic = true);
		}
		simulationClone = isSimulating;
		if (!isSimulating)
		{
			visTargRot = VisualTarget.rotation;
			if (!stripped)
			{
				UprightTrans.rotation = Quaternion.identity;
				PlacedTrans.rotation = Quaternion.LookRotation(-base.transform.forward, up);
				if (Vector3.Angle(PlacedTrans.forward, up) < 45f || Vector3.Angle(PlacedTrans.forward, -up) < 45f)
				{
					PlacedTrans.forward = -base.transform.up;
				}
				PlacedTrans.parent = null;
				PlacedTrans.gameObject.SetActive(false);
			}
			if (!_parentMachine.isLocalMachine)
			{
				VisualController.SetInvisible();
				VisualController.lockVisibility = true;
			}
			VisualTarget.rotation = visTargRot;
		}
		else if (_hasParentMachine && _parentMachine.isLocalMachine)
		{
			cameraOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
			cameraTransform = cameraOrbit.transform;
			CompositeTracker.SetParent(_parentMachine.SimulationMachine, true);
			CamPos = (lookAtPos = base.transform.position);
			advancedToggle.IsActive = false;
			DestroyRigidbody();
			stayUpRight = mode == Mode.Car;
			SetSmoothing();
			VisualController.SetInvisible();
			VisualController.lockVisibility = true;
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	protected void SetSmoothing()
	{
		float num = 1f - smoothSlider.Value;
		if (mode == Mode.FirstPerson)
		{
			sliderLerp = 60f;
		}
		else
		{
			sliderLerp = 16.126f * num * num - 1.286f * num + 0.287f;
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (isSimulating && _hasParentMachine && _parentMachine.isLocalMachine && activateKey.IsPressed)
		{
			camController.OnKeyPressed(KeyCode);
		}
		if (!isActive)
		{
			userRotation = Vector3.zero;
			return;
		}
		if (mode != Mode.FirstPerson)
		{
			userRotation.z = Mathf.Clamp(userRotation.z + InputManager.ZoomValue() * settings.ZoomSpeed, -1f, 1f);
		}
		if (InputManager.FocusCameraKey())
		{
			userRotation = Vector3.zero;
		}
		if (InputManager.RotateCameraKeyHeld())
		{
			if (mode != Mode.FirstPerson)
			{
				userRotation.x += InputManager.MouseX();
				userRotation.y += InputManager.MouseY();
			}
			else
			{
				userRotation.x = InputManager.MouseX();
				userRotation.y = InputManager.MouseY();
			}
		}
		else if (mode != Mode.FirstPerson)
		{
			Vector2 vector = Vector2.Lerp(userRotation, Vector2.zero, Time.unscaledDeltaTime * settings.UserRotationResetSpeed);
			userRotation.x = vector.x;
			userRotation.y = vector.y;
		}
		else
		{
			userRotation.x = 0f;
			userRotation.y = 0f;
		}
	}

	public override void EmulationUpdateBlock()
	{
		base.EmulationUpdateBlock();
		if (isSimulating && _hasParentMachine && _parentMachine.isLocalMachine && activateKey.EmulationPressed())
		{
			camController.OnKeyPressed(KeyCode);
		}
	}

	public override void LateUpdateBlock()
	{
		CamUpdate();
	}

	private void CamUpdate()
	{
		if (stripped || !PlacedTrans || !UprightTrans || !CompositeTracker || !CompoundTracker)
		{
			return;
		}
		targetPosition = Vector3.forward;
		keepUpRotation = Quaternion.Euler(0f, UprightTrans.eulerAngles.y, 0f);
		targetPosition = SetTargetFromDHR();
		if (!isSimulating)
		{
			if (_parentMachine.isLocalMachine)
			{
				Building();
			}
		}
		else if (_hasParentMachine && _parentMachine.isLocalMachine && _parentMachine.isReady && _parentMachine.finishedPhysics)
		{
			Simulation();
		}
		else
		{
			CompositeTracker.position = (CamPos = (lookAtPos = targetPosition));
		}
	}

	private void Simulation()
	{
		deltaTime = Time.unscaledDeltaTime;
		Vector3 position = UprightTrans.position;
		if (lastTransPos == Vector3.zero)
		{
			lastTransPos = position;
		}
		float num = (stayUpRight ? 1f : 4f);
		CamPos = Vector3.Lerp(CamPos, targetPosition, deltaTime * 4f * num * sliderLerp);
		Vector3 vector = position - lastTransPos;
		lookAtPos = Vector3.Lerp(lookAtPos, position + ((mode != Mode.Custom) ? Vector3.zero : (vector * predictSlider.Value)), deltaTime * 4f * num * sliderLerp);
		lastTransPos = position;
		if (!stayUpRight)
		{
			float num2 = ((mode != Mode.Custom) ? 6f : 1f);
			up = Vector3.Lerp(up, UprightTrans.up, deltaTime * num2 * sliderLerp);
			CompositeTracker.position = Vector3.Lerp(CompositeTracker.position, CamPos, deltaTime * 6f * sliderLerp);
			CompoundTracker.localPosition = Vector3.zero;
			num = 10f;
		}
		else
		{
			up = Vector3.up;
			Vector3 vector2 = Vector3.Lerp(CompositeTracker.position, CamPos, deltaTime * 6f * sliderLerp);
			CompositeTracker.position = new Vector3(vector2.x, Mathf.Clamp(vector2.y, SingleInstanceFindOnly<AddPiece>.Instance.floorHeight + settings.PositionLerp, 100000f), vector2.z);
			CompoundTracker.localPosition = Vector3.zero;
			num = 1f;
		}
		Quaternion b = Quaternion.LookRotation((lookAtPos - CompositeTracker.position).normalized, up);
		if (mode == Mode.Custom)
		{
			b.eulerAngles = new Vector3(b.eulerAngles.x, b.eulerAngles.y, 0f);
		}
		CompositeTracker.rotation = Quaternion.Slerp(CompositeTracker.rotation, b, deltaTime * settings.RotationLerp * num * sliderLerp);
		if (!isActive)
		{
			return;
		}
		if (mode == Mode.FirstPerson)
		{
			float num3 = CompoundTracker.localEulerAngles.x - userRotation.y * settings.FirstPersonLookSpeed.y * 1.5f;
			if (num3 > 180f)
			{
				num3 -= 360f;
			}
			num3 = Mathf.Clamp(num3, -89.999f, 89.999f);
			CompoundTracker.localRotation = Quaternion.Euler(num3, CompoundTracker.localEulerAngles.y + userRotation.x * settings.FirstPersonLookSpeed.x * 1.5f, CompoundTracker.localEulerAngles.z);
			if (!InputManager.RotateCameraKeyHeld())
			{
				CompoundTracker.localRotation = Quaternion.Slerp(CompoundTracker.localRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f - rollSlider.Value)), deltaTime * 2f);
			}
			CompoundTracker.localRotation = Quaternion.Euler(CompoundTracker.localEulerAngles.x, CompoundTracker.localEulerAngles.y, 0f - rollSlider.Value);
		}
		if (!StatMaster.isHeadless && (bool)cameraTransform && (bool)cameraOrbit)
		{
			cameraTransform.position = (cameraOrbit.camPos = CompoundTracker.position);
			cameraTransform.rotation = (cameraOrbit.camRot = CompoundTracker.rotation);
			cameraOrbit.camForward = cameraTransform.forward;
			cameraOrbit.camUp = cameraTransform.up;
			cameraOrbit.cam.nearClipPlane = 0.3f;
			if (MouseOrbit.CameraMoved != null)
			{
				MouseOrbit.CameraMoved(cameraOrbit.camPos);
			}
		}
	}

	private void Building()
	{
		CamPos = targetPosition;
		lookAtPos = UprightTrans.position;
		CompositeTracker.position = CamPos;
		CompoundTracker.localPosition = Vector3.zero;
		Quaternion rotation = Quaternion.LookRotation((lookAtPos - CompositeTracker.position).normalized, up);
		CompositeTracker.rotation = rotation;
		CompositeTracker2.localRotation = Quaternion.Euler(new Vector3(0f - tiltSlider.Value, 0f, 0f));
		CompositeTracker3.localRotation = Quaternion.Euler(new Vector3(0f, 0f - yawSlider.Value, 0f));
		CompoundTracker.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - rollSlider.Value));
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		angle %= 360f;
		return Mathf.Clamp(angle, min, max);
	}

	private Vector3 SetTargetFromDHR()
	{
		Quaternion quaternion = keepUpRotation;
		if (isSimulating)
		{
			if (stayUpRight)
			{
				float f = Vector3.Dot(up, UprightTrans.forward);
				f = (Mathf.Abs(f) - 0.75f) * 4f;
				quaternion = Quaternion.Slerp(keepUpRotation, UprightTrans.rotation, f);
			}
			else
			{
				quaternion = UprightTrans.rotation;
			}
		}
		Vector3 vector = quaternion * PlacedTrans.forward;
		float angle;
		float angle2;
		if (isActive && mode != Mode.FirstPerson)
		{
			angle = Mathf.Clamp(heightSlider.Value - userRotation.y * settings.ThirdPersonLookSpeed.y, -89.999f, 89.999f);
			angle2 = rotationSlider.Value + userRotation.x * settings.ThirdPersonLookSpeed.x;
		}
		else
		{
			angle = Mathf.Clamp(heightSlider.Value, -89.999f, 89.999f);
			angle2 = rotationSlider.Value;
		}
		vector = Quaternion.AngleAxis(angle, quaternion * PlacedTrans.right) * vector;
		vector = (Quaternion.AngleAxis(angle2, quaternion * PlacedTrans.up) * vector).normalized;
		float num = 0f - distanceSlider.Value + userRotation.z * settings.ThirdPersonMaxZoomDistance;
		num = ((!(num > -0.1f)) ? num : (-0.1f));
		if (isActive && limitToTerrain.IsActive && _parentMachine.isReady && _parentMachine.finishedPhysics)
		{
			num = 0f - MouseOrbit.LimitDistance(UprightTrans.position, Quaternion.LookRotation(vector), 0f - num);
		}
		vector *= num;
		return vector + UprightTrans.position;
	}

	protected override void OnEnable()
	{
		if (needsRigidUpdate)
		{
			StartCoroutine(UpdateRigidbodies(base.transform));
			needsRigidUpdate = false;
		}
		if (!_hasParentMachine || isBuildBlock)
		{
			UpdateSimState();
		}
		if (!isSimulating && _hasParentMachine && !stripped && CamTriggerSetParent != null)
		{
			CamTriggerSetParent.machine = _parentMachine;
			CamTriggerSetParent.hasMachine = true;
		}
		if (!StatMaster.isHeadless)
		{
			if (camController != null)
			{
				camController.Register(this);
			}
			else
			{
				Debug.LogError("Couldn't register FixedCameraBlock with CamController!");
			}
		}
	}

	protected override void OnDisable()
	{
		if (!isApplicationQuitting)
		{
			if (!StatMaster.isHeadless)
			{
				camController.Unregister(this);
			}
			if (cameraOrbit != null)
			{
				cameraOrbit.isActive = true;
			}
		}
	}

	protected override void OnDestroy()
	{
		if (!isApplicationQuitting && !simulationClone && PlacedTrans != null)
		{
			UnityEngine.Object.DestroyImmediate(PlacedTrans.gameObject);
		}
	}

	private void OnApplicationQuit()
	{
		isApplicationQuitting = true;
	}

	private void OnAdvancedModeToggled(bool isActive)
	{
		mode = (Mode)modeMenu.Value;
		UpdateVisibility();
		UpdateMapper();
	}

	private void UpdateVisibility()
	{
		MSlider mSlider = distanceSlider;
		bool flag = true;
		rotationSlider.DisplayInMapper = flag;
		flag = flag;
		heightSlider.DisplayInMapper = flag;
		mSlider.DisplayInMapper = flag;
		advancedToggle.DisplayInMapper = mode != Mode.Custom;
		fovSlider.DisplayInMapper = mode == Mode.FirstPerson || mode == Mode.Custom;
		MSlider mSlider2 = rollSlider;
		flag = advancedToggle.IsActive || mode == Mode.Custom;
		yawSlider.DisplayInMapper = flag;
		mSlider2.DisplayInMapper = flag;
		smoothSlider.DisplayInMapper = (advancedToggle.IsActive && mode != Mode.FirstPerson) || mode == Mode.Custom;
		predictSlider.DisplayInMapper = mode == Mode.Custom;
		tiltSlider.DisplayInMapper = true;
		limitToTerrain.DisplayInMapper = !advancedToggle.DisplayInMapper || advancedToggle.IsActive;
	}

	public void UpdateMode(int newMode)
	{
		SaveModeSpecificValues();
		prevMode = (int)mode;
		mode = (Mode)modeMenu.Value;
		UpdateVisibility();
		UpdateModeSpecificValues();
		UpdateMapper();
	}

	public void UpdateMapper()
	{
		if ((bool)BlockMapper.CurrentInstance)
		{
			BlockMapper.CurrentInstance.Refresh();
		}
	}

	protected void SaveModeSpecificValues()
	{
		if (mode == Mode.FirstPerson)
		{
			fpsFov = fovSlider.Value;
			fpsHeight = heightSlider.Value;
			fpsDistance = distanceSlider.Value;
			fpsTilt = tiltSlider.Value;
		}
		else if (mode == Mode.Custom)
		{
			defFov = fovSlider.Value;
			defHeight = heightSlider.Value;
			defDistance = distanceSlider.Value;
			defTilt = tiltSlider.Value;
		}
	}

	protected void UpdateModeSpecificValues()
	{
		stayUpRight = mode == Mode.Car;
		fovSlider.Value = ((mode != Mode.FirstPerson) ? defFov : fpsFov);
		heightSlider.Value = ((mode != Mode.FirstPerson) ? defHeight : fpsHeight);
		distanceSlider.Value = ((mode != Mode.FirstPerson) ? defDistance : fpsDistance);
		tiltSlider.Value = ((mode != Mode.FirstPerson) ? defTilt : fpsTilt);
	}

	public override void OnReset()
	{
		fpsFov = 60f;
		defFov = 41f;
		fpsHeight = 90f;
		defHeight = 18f;
		fpsDistance = 1.5f;
		defDistance = 32f;
		fpsTilt = 80f;
		defTilt = 0f;
		if (!StatMaster.isMP)
		{
			modeMenu.Value = prevMode;
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		if (!data.HasKey("bmt-limit-to-terrain") && data.WasLoadedFromFile)
		{
			data.Write("bmt-limit-to-terrain", false);
		}
		base.OnLoad(data);
		if (!isSimulating || !_hasParentMachine || _parentMachine.isLocalMachine)
		{
			mode = (Mode)modeMenu.Value;
			UpdateVisibility();
		}
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		if (modeMenu != null)
		{
			modeMenu.Items = new List<string>
			{
				LocalisationManager.GetTranslation(2450),
				LocalisationManager.GetTranslation(2451),
				LocalisationManager.GetTranslation(2452),
				LocalisationManager.GetTranslation(2453)
			};
		}
	}
}
