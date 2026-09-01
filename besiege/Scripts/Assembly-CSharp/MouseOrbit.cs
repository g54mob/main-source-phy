using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using SRF;
using UnityEngine;

public class MouseOrbit : SingleInstanceFindOnly<MouseOrbit>
{
	public enum SimOrientation
	{
		Manual = 0,
		Machine = 1
	}

	public enum TargetType
	{
		Machine = 0,
		Block = 1,
		Entity = 2,
		Misc = 3
	}

	private class RaycastEntry
	{
		public RaycastHit hit;

		public float camDistance;
	}

	private const float simStartDuration = 1.5f;

	public static bool AllowWASDCamControl = true;

	public static Action<Vector3> CameraMoved;

	private static Vector3 ZERO = Vector3.zero;

	[HideInInspector]
	public bool cinematic;

	[HideInInspector]
	public Vector3 PosComposite = Vector3.zero;

	[HideInInspector]
	public Quaternion rotation;

	[HideInInspector]
	public Vector3 wasdPosOffset = Vector3.zero;

	[HideInInspector]
	public Camera cam;

	[HideInInspector]
	public Camera farCam;

	public bool isActive = true;

	private static bool useNewCalculation = false;

	private static bool snapToCardinalAxes = true;

	public Transform target;

	public TargetType targetType;

	public BasicInfo targetInfo;

	private Transform prevTarget;

	private BasicInfo prevTargetInfo;

	public BasicInfo buildTarget;

	private Vector3 buildWasdPos;

	public float distance = 10f;

	public float lerpedLimitDistance = float.MaxValue;

	public float xSpeed = 250f;

	public float ySpeed = 120f;

	public float yMinLimit = -20f;

	public float yMaxLimit = 80f;

	public float x;

	public float y;

	public Vector3 up = Vector3.up;

	public float smooth = 8f;

	public float wasdSpeed = 0.5f;

	public float wasdSmooth = 6f;

	public float zoomSmooth = 10f;

	public float maxFocusTime = 0.3f;

	private float maxFocusCursorMovement = 200f;

	public AudioSource introSound;

	public float filmCamSmooth = 1f;

	public float panSpeed = 0.6f;

	public Transform machineTarget;

	public Transform focusObject;

	public float focusLerpSmooth = 12f;

	public Transform dofTarget;

	public float minZoom = 1.25f;

	public float minNearclip = 0.3f;

	public float defaultFarclip = 2600f;

	public static float maxZoom = 335f;

	public float mouseSensitivityScaler = 1f;

	public float scrollSensitivityScaler = 1f;

	public float yPosClamp = 0.2f;

	public Camera hud3Dcam;

	[HideInInspector]
	public Vector3 camForward;

	[HideInInspector]
	public Vector3 camUp;

	[HideInInspector]
	public Vector3 camPos;

	[HideInInspector]
	public Quaternion camRot;

	public ParticleSystem focusRipple;

	private float smoothX;

	private float smoothY;

	private float refX = float.MaxValue;

	private float refY = float.MaxValue;

	private Vector3 wasdPOS;

	private Vector3 position;

	private bool wasActive = true;

	private float startX;

	private float startY;

	private float startDistance;

	public AnimationCurve introCurve = new AnimationCurve(new Keyframe(0f, 0f, 3f, 3f), new Keyframe(1f, 1f, 0f, 0f));

	public float introSmooth = 0.5f;

	private float animX;

	private float animY;

	private float animDistance;

	private bool animating = true;

	private float actualSmooth;

	private float actualZoomSmooth;

	private float actualWasdSmooth;

	private float zoomSmoothDelegate;

	private Vector3 posToBe;

	private Quaternion rotToBe = Quaternion.identity;

	private float focusHeldTime;

	private float focusCursorMovement;

	private bool isFocusing;

	private Vector3 focusMousePos;

	private Vector3 targetPos;

	private Quaternion targetRot;

	private Transform myTransform;

	public Transform animStart;

	private LayerMask layerMasky;

	private float uDelta = 0.02f;

	private float scrl;

	private Transform dummyTarget;

	private bool freeze;

	private float xSpeedMultiplier;

	private float ySpeedMultiplier;

	private Transform uprightBZtransform;

	private IEnumerator focusCoroutine;

	private bool wasdTutorialDisplayed;

	private float scheduleTargetConversion = float.MaxValue;

	private bool targetConversionEnteringSim;

	private bool machineSimulating;

	private Rigidbody sourceCube;

	private Vector3 sourceCubeStartForward;

	private Quaternion followRot = Quaternion.identity;

	public Plane cameraPlane;

	private float simTimer = 1.5f;

	private Vector3 buildPos;

	private bool usingInputs;

	private float lastDist;

	private bool isOrthographic;

	private CameraClearFlags currentClearFlags;

	private float currentFarClip = 1000f;

	public GameObject orthoFloor;

	public ColorfulFog barrenExpanceFog;

	private bool _introPlayed;

	private float machineThickness = float.MaxValue;

	private int lastUpdateFrame;

	private static LayerMask environmentMask = 822083841;

	private static float cameraCollisionErrorMargin = 0.1f;

	private float lerpTimer;

	private Vector3 prevPos = Vector3.zero;

	private bool fadeOffset;

	private bool resetting;

	public LayerMask LayerMask
	{
		get
		{
			return layerMasky;
		}
		set
		{
			layerMasky = value;
		}
	}

	public bool IsOrthographic
	{
		get
		{
			return isOrthographic;
		}
	}

	public bool introPlayed
	{
		get
		{
			return _introPlayed;
		}
		set
		{
			_introPlayed = value;
		}
	}

	public override string Name
	{
		get
		{
			return "MouseOrbit";
		}
	}

	public static bool HasFocus()
	{
		return SingleInstanceFindOnly<MouseOrbit>.Instance.target != null && SingleInstanceFindOnly<MouseOrbit>.Instance.target != SingleInstanceFindOnly<MouseOrbit>.Instance.machineTarget;
	}

	private bool PlayerRotatingCam()
	{
		return InputManager.RotateCameraKeyHeld();
	}

	private bool PlayerMovingCam()
	{
		return usingInputs || InputManager.PanCameraKeyHeld() || (AllowWASDCamControl && (InputManager.Camera.RightKeyHeld() || InputManager.Camera.LeftKeyHeld() || InputManager.Camera.ForwardKeyHeld() || InputManager.Camera.BackwardKeyHeld()));
	}

	protected override void Awake()
	{
		base.Awake();
		SetDefaults();
		SetFarClip(defaultFarclip);
		OnSensitivityChanged();
		ReferenceMaster.onCameraSensitivityChanged = (Action)Delegate.Combine(ReferenceMaster.onCameraSensitivityChanged, new Action(OnSensitivityChanged));
		ReferenceMaster.onFOVChanged = (Action)Delegate.Combine(ReferenceMaster.onFOVChanged, new Action(OnFOVChanged));
		ReferenceMaster.onDestroyPhysicsGoal = (Action)Delegate.Combine(ReferenceMaster.onDestroyPhysicsGoal, new Action(OnDestroyPhysics));
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulation));
		ReferenceMaster.onMachineSimulation = (Action<Machine, bool>)Delegate.Combine(ReferenceMaster.onMachineSimulation, new Action<Machine, bool>(OnMachineSimulation));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnMachineSimulation));
		ReferenceMaster.onAudioReverbToggled = (Action)Delegate.Combine(ReferenceMaster.onAudioReverbToggled, new Action(ReverbToggle));
		ReverbToggle();
	}

	public void SetFov(float fov)
	{
		cam.fieldOfView = fov;
		hud3Dcam.fieldOfView = fov;
		if (useNewCalculation)
		{
			farCam.fieldOfView = fov;
		}
	}

	public void SetFarClip(float far)
	{
		if (useNewCalculation)
		{
			cam.farClipPlane = far;
			hud3Dcam.farClipPlane = far;
			farCam.farClipPlane = far * 10f;
		}
	}

	public void OnSensitivityChanged()
	{
		xSpeedMultiplier = xSpeed * 0.02f * mouseSensitivityScaler * OptionsMaster.BesiegeConfig.CameraSensitivity / 100f;
		ySpeedMultiplier = ySpeed * 0.02f * mouseSensitivityScaler * OptionsMaster.BesiegeConfig.CameraSensitivity / 100f;
	}

	private void OnMachineSimulation(bool sim)
	{
		if (!sim)
		{
			sourceCube = null;
			followRot = Quaternion.identity;
			machineThickness = float.MaxValue;
		}
		else
		{
			buildPos = posToBe;
			machineThickness = Machine.Active().GetBounds(false).extents.y - cameraCollisionErrorMargin;
		}
		simTimer = 1.5f;
		ResetFocusRipple();
		scheduleTargetConversion = Time.unscaledTime;
		targetConversionEnteringSim = sim;
		machineSimulating = sim;
	}

	private void OnMachineSimulation(Machine m, bool sim)
	{
		scheduleTargetConversion = Time.unscaledTime;
		targetConversionEnteringSim = sim;
	}

	private void OnLevelSimulation(bool sim)
	{
		scheduleTargetConversion = Time.unscaledTime;
		targetConversionEnteringSim = sim;
	}

	private void OnDestroyPhysics()
	{
		targetConversionEnteringSim = false;
		ConvertBuildSimTarget(targetType);
		scheduleTargetConversion = float.MaxValue;
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onDestroyPhysicsGoal = (Action)Delegate.Remove(ReferenceMaster.onDestroyPhysicsGoal, new Action(OnDestroyPhysics));
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulation));
		ReferenceMaster.onMachineSimulation = (Action<Machine, bool>)Delegate.Remove(ReferenceMaster.onMachineSimulation, new Action<Machine, bool>(OnMachineSimulation));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnMachineSimulation));
		ReferenceMaster.onFOVChanged = (Action)Delegate.Remove(ReferenceMaster.onFOVChanged, new Action(OnFOVChanged));
		ReferenceMaster.onAudioReverbToggled = (Action)Delegate.Remove(ReferenceMaster.onAudioReverbToggled, new Action(ReverbToggle));
	}

	protected void ReverbToggle()
	{
		base.gameObject.GetComponent<AudioReverbZone>().enabled = OptionsMaster.BesiegeConfig.SfxDistanceFX;
	}

	private void OnFOVChanged()
	{
		if (isActive)
		{
			SetFov(OptionsMaster.GetVerticalFOV());
		}
	}

	protected void Start()
	{
		layerMasky = AddPiece.CreateLayerMask(SingleInstanceFindOnly<AddPiece>.Instance.layerMasky, 10, 27);
		OnFOVChanged();
		PlayAnimation();
	}

	private void SetDefaults()
	{
		GameObject gameObject = new GameObject("TRANSFORM HELPERS");
		uprightBZtransform = new GameObject("buildzone upright").transform;
		uprightBZtransform.parent = gameObject.transform;
		myTransform = base.transform;
		targetPos = target.position;
		targetType = TargetType.Machine;
		Vector3 eulerAngles = myTransform.eulerAngles;
		x = eulerAngles.y;
		y = eulerAngles.x;
		startX = x;
		startY = y;
		startDistance = distance;
		if ((bool)animStart)
		{
			animDistance = Vector3.Distance(animStart.position, targetPos);
			eulerAngles = animStart.eulerAngles;
			UnityEngine.Object.Destroy(animStart.gameObject);
		}
		else
		{
			animDistance = 45f;
			eulerAngles = new Vector3(3f, 7.7f, 0f);
		}
		animX = eulerAngles.y;
		animY = eulerAngles.x;
		cam = base.gameObject.GetComponent<Camera>();
		SetCamera(animX, animY, animDistance, 0f);
	}

	private void PlayAnimation()
	{
		StopAllCoroutines();
		ResetCamTarget();
		StartCoroutine(IntroAnimation(4f));
	}

	private IEnumerator IntroAnimation(float duration)
	{
		animating = true;
		SetCamera(animX, animY, animDistance, 0f);
		while (StatMaster.isMP && !PlayerData.hasLocalPlayer)
		{
			yield return null;
		}
		if ((bool)introSound)
		{
			introSound.Stop();
			introSound.Play();
		}
		for (int f = 0; f < 2; f++)
		{
			yield return null;
		}
		if (!StatMaster.isMP)
		{
			yield return new WaitForSeconds(0.05f);
		}
		if (!animating)
		{
			yield break;
		}
		float x = animX;
		float y = animY;
		float d = animDistance;
		for (float t = 0f; t < duration; t += ((Time.timeScale != 1f) ? Time.unscaledDeltaTime : Time.deltaTime))
		{
			if (PlayerRotatingCam() || PlayerMovingCam() || HasFocus())
			{
				animating = false;
				if (StatMaster.isMP)
				{
					HardSet(startX, startY, startDistance);
				}
				else
				{
					HardSet(x, y, d);
				}
				IntroAnimationEnded();
				yield break;
			}
			float pct = t / duration;
			float ramp = Mathf.Pow(introCurve.Evaluate(pct), introSmooth);
			x = Mathf.Lerp(animX, startX, ramp);
			y = Mathf.Lerp(animY, startY, ramp);
			d = Mathf.Lerp(animDistance, startDistance, ramp);
			SetCamera(x, y, d, pct);
			yield return null;
		}
		SetCamera(startX, startY, startDistance);
		HardReset();
		IntroAnimationEnded();
	}

	private void IntroAnimationEnded()
	{
		introPlayed = true;
		SingleInstance<Events>.Instance.CameraInitFinished();
	}

	private void Update()
	{
		usingInputs = false;
		if (!isActive || freeze || cinematic || StatMaster.isHeadless || animating)
		{
			return;
		}
		uDelta = ((Time.timeScale != 0f) ? (Time.deltaTime / Time.timeScale) : Time.unscaledDeltaTime);
		if (OptionsMaster.BesiegeConfig.SmoothCamera)
		{
			actualSmooth = filmCamSmooth;
			actualZoomSmooth = filmCamSmooth;
			actualWasdSmooth = filmCamSmooth;
		}
		else
		{
			actualSmooth = smooth;
			actualZoomSmooth = zoomSmooth;
			actualWasdSmooth = wasdSmooth;
		}
		if (!(target != null))
		{
			return;
		}
		bool inMenu = StatMaster.inMenu;
		if (!inMenu && InputManager.RotateCameraKeyHeld())
		{
			x += InputManager.MouseX() * xSpeedMultiplier;
			y = Mathf.Clamp(y - InputManager.MouseY() * ySpeedMultiplier, yMinLimit + 0.01f, yMaxLimit - 0.01f);
		}
		if (!StatMaster.Mode.isRotating && !StatMaster.Mode.isScaling)
		{
			smoothX = Mathf.Lerp(smoothX, x, uDelta * actualSmooth);
			smoothY = ClampAngle(Mathf.Lerp(smoothY, y, uDelta * actualSmooth), yMinLimit, yMaxLimit);
		}
		zoomSmoothDelegate = Mathf.Lerp(zoomSmoothDelegate, scrl * distance * scrollSensitivityScaler, uDelta * actualZoomSmooth);
		if (inMenu)
		{
			return;
		}
		if (InputManager.PanCameraKeyHeld())
		{
			usingInputs = true;
			fadeOffset = false;
			float num = ((!InputManager.AdvancedBuilding.LeftShiftKey()) ? 1f : 2f);
			num *= 0.02f + Mathf.Pow(1.5f * (distance - minZoom) / (maxZoom - minZoom), 1f) * 4.3f;
			float num2 = panSpeed * num * mouseSensitivityScaler;
			if (useNewCalculation)
			{
				Transform parent = base.transform.parent.parent.parent.parent;
				Transform parent2 = base.transform.parent;
				Vector3 vector = parent.TransformPoint(wasdPosOffset);
				float num3 = InputManager.MouseX();
				float num4 = InputManager.MouseY();
				vector -= parent2.right * num3 * num2;
				vector -= parent2.up * num4 * num2;
				wasdPosOffset = parent.InverseTransformPoint(vector);
			}
			else
			{
				wasdPosOffset -= myTransform.right * InputManager.MouseX() * num2;
				wasdPosOffset -= myTransform.up * InputManager.MouseY() * num2;
			}
		}
		if (AllowWASDCamControl)
		{
			WASD();
		}
		scrl = ((!StatMaster.disableCameraZoom) ? InputManager.ZoomValue() : 0f);
		if (scrl != 0f)
		{
			resetting = false;
		}
		if (StatMaster.stopCamZoom)
		{
			scrl = 0f;
		}
	}

	public bool MouseControlUsed()
	{
		return usingInputs || scrl != 0f;
	}

	private void LateUpdate()
	{
		UpdateCam();
		if (InputManager.FocusCameraKey())
		{
			focusMousePos = InputManager.CursorPosition();
			isFocusing = true;
			focusHeldTime = (focusCursorMovement = 0f);
		}
		if (isFocusing)
		{
			ProcessFocus();
			focusHeldTime += Time.deltaTime;
		}
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating)
		{
			UpdateCamPlane();
		}
	}

	protected void ProcessFocus()
	{
		Vector3 vector = InputManager.CursorPosition();
		focusCursorMovement += (vector - focusMousePos).sqrMagnitude;
		if (focusCursorMovement > maxFocusCursorMovement || focusHeldTime > maxFocusTime)
		{
			isFocusing = false;
		}
		else if (InputManager.FocusCameraKeyReleased())
		{
			GetTarget();
			isFocusing = false;
		}
	}

	private Quaternion GetCamRotationFromZoneRotation(Quaternion buildRot)
	{
		return Quaternion.AngleAxis(buildRot.eulerAngles.y, Vector3.up);
	}

	public void SetCameraPositionAndRotation(Vector3 pos, Quaternion rot)
	{
		myTransform.rotation = (rotation = (camRot = rot));
		x = (smoothX = myTransform.eulerAngles.y);
		y = (smoothY = myTransform.eulerAngles.x);
		myTransform.position = (camPos = pos);
		distance = (camPos - posToBe).magnitude;
		position = new Vector3(0f, 0f, 0f - distance);
		wasdPosOffset = (wasdPOS = camPos - posToBe - rotation * position);
		camForward = myTransform.forward;
		camUp = myTransform.up;
		UpdateCam();
		if (CameraMoved != null)
		{
			CameraMoved(camPos);
		}
	}

	public void FetchMiddle()
	{
		if (lastUpdateFrame < Time.frameCount)
		{
			SingleInstanceFindOnly<AddPiece>.Instance.SetMiddle(Machine.Active().CalculateMiddle());
		}
		lastUpdateFrame = Time.frameCount;
	}

	private void SetCamera(float hor, float ver, float d, float wasdMagnitude = 1f)
	{
		WASD();
		posToBe = (targetPos = target.position);
		rotToBe = Quaternion.identity;
		SetCamera(targetPos, rotToBe, hor, ver, d, wasdMagnitude);
	}

	private void SetCamera(Vector3 targetPos, Quaternion rotToBe, float hor, float ver, float d, float wasdMagnitude = 1f, bool limitToTerrain = false, Machine m = null)
	{
		distance = d;
		float z = position.z;
		if (useNewCalculation)
		{
			Transform transform = myTransform;
			Transform parent = transform.parent;
			Transform parent2 = parent.parent;
			Transform parent3 = parent2.parent;
			Transform parent4 = parent3.parent;
			parent4.position = targetPos;
			parent3.localPosition = wasdPOS * wasdMagnitude;
			AlignTransformWithGravity(parent4);
			AlignTransformWithGravity(parent3);
			parent2.localRotation = rotToBe * Quaternion.Euler(0f, SnapAngleTo90(hor, ref refX), 0f);
			parent.localRotation = Quaternion.Euler(SnapAngleTo90(ver, ref refY), 0f, 0f);
			rotation = parent.rotation;
			position = new Vector3(0f, 0f, 0f - distance);
			PosComposite = parent3.position;
			LimitCameraToTerrain(m, limitToTerrain, z);
			transform.localPosition = position;
			camRot = transform.rotation;
			camPos = transform.position;
			camUp = parent3.up;
		}
		else
		{
			rotation = rotToBe * Quaternion.Euler(SnapAngleTo90(ver, ref refY), SnapAngleTo90(hor, ref refX), 0f);
			position = new Vector3(0f, 0f, 0f - distance);
			PosComposite = targetPos + wasdPOS * wasdMagnitude;
			LimitCameraToTerrain(m, limitToTerrain, z);
			myTransform.position = (camPos = rotation * position + PosComposite);
			myTransform.rotation = (camRot = rotation);
			camUp = myTransform.up;
		}
		camForward = myTransform.forward;
		if (CameraMoved != null)
		{
			CameraMoved(camPos);
		}
	}

	public Vector3 GetCameraUp(Vector3 pos, bool followMachine = false)
	{
		return up;
	}

	public void AlignTransformWithGravity(Transform t)
	{
		Vector3 cameraUp = GetCameraUp(t.position);
		t.localRotation = Quaternion.identity;
		Quaternion quaternion = Quaternion.LookRotation(Vector3.Cross(t.right, cameraUp), cameraUp);
		t.rotation = quaternion;
	}

	private void UpdateClientMiddle(Machine m, bool hasMachine)
	{
		if (StatMaster.isMP && StatMaster.isClient && hasMachine && m.isSimulating && targetType == TargetType.Machine)
		{
			FetchMiddle();
		}
	}

	private void PrepareOnReactivation(Vector3 tPos)
	{
		position = new Vector3(0f, 0f, 0f - distance);
		posToBe = tPos;
		if (StatMaster.isMP && (targetType == TargetType.Block || targetType == TargetType.Machine))
		{
			PlayerBuildZone playerBuildZone = null;
			if (targetType == TargetType.Block)
			{
				playerBuildZone = ((targetInfo as BlockBehaviour).ParentMachine as ServerMachine).player.buildZone;
				rotToBe = playerBuildZone.transform.rotation;
			}
			else if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
			{
				playerBuildZone = PlayerData.localPlayer.buildZone;
				rotToBe = GetCamRotationFromZoneRotation(playerBuildZone.transform.rotation);
			}
			else
			{
				rotToBe = Quaternion.identity;
			}
			targetRot = rotToBe;
		}
		else
		{
			rotToBe = Quaternion.identity;
		}
		rotation = rotToBe * Quaternion.Euler(smoothY, smoothX, 0f);
		myTransform.position = (camPos = rotation * position + posToBe + wasdPOS);
		myTransform.rotation = (camRot = rotation);
		camForward = myTransform.forward;
		camUp = myTransform.up;
	}

	private Quaternion GetCameraTargetRotation(Machine m, bool hasMachine)
	{
		Vector3 forward = Vector3.forward;
		Vector3 originalUp = Vector3.up;
		if (StatMaster.isMP && (targetType == TargetType.Block || targetType == TargetType.Machine))
		{
			if (!StatMaster.Mode.isRotating && !StatMaster.Mode.isScaling)
			{
				if (targetType == TargetType.Block)
				{
					PlayerBuildZone buildZone = ((targetInfo as BlockBehaviour).ParentMachine as ServerMachine).player.buildZone;
					targetRot = buildZone.transform.rotation;
					forward = buildZone.transform.forward;
				}
				else if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
				{
					PlayerBuildZone buildZone = PlayerData.localPlayer.buildZone;
					targetRot = GetCamRotationFromZoneRotation(buildZone.transform.rotation);
					forward = buildZone.transform.forward;
				}
				else
				{
					targetRot = Quaternion.identity;
				}
			}
		}
		else
		{
			targetRot = Quaternion.identity;
		}
		if (hasMachine && machineSimulating && m.isReady)
		{
			switch (targetType)
			{
			case TargetType.Block:
				followRot = GetTargetRelativeRotationChange(targetInfo.Rigidbody, forward, originalUp);
				break;
			case TargetType.Machine:
				followRot = GetTargetRelativeRotationChange(m.RefBlock.Rigidbody, forward, originalUp);
				break;
			default:
				followRot = Quaternion.Slerp(followRot, Quaternion.identity, 0.1f);
				sourceCube = null;
				break;
			}
			targetRot *= followRot;
		}
		return targetRot;
	}

	private Quaternion GetTargetRelativeRotationChange(Rigidbody b, Vector3 originalFwd, Vector3 originalUp)
	{
		if (sourceCube != b)
		{
			sourceCube = b;
			if ((bool)sourceCube)
			{
				sourceCubeStartForward = sourceCube.transform.InverseTransformDirection(originalFwd);
			}
		}
		if ((bool)sourceCube)
		{
			SimOrientation simCamFollow = OptionsMaster.BesiegeConfig.SimCamFollow;
			if (simCamFollow == SimOrientation.Machine)
			{
				Vector3 cameraUp = GetCameraUp(targetPos);
				Quaternion horizontalAlignedRotation = GetHorizontalAlignedRotation(originalFwd, originalUp);
				Quaternion horizontalAlignedRotation2 = GetHorizontalAlignedRotation(sourceCube.transform.TransformDirection(sourceCubeStartForward), cameraUp);
				Vector3 velocity = sourceCube.velocity;
				Vector3 angularVelocity = sourceCube.angularVelocity;
				angularVelocity = Vector3.ProjectOnPlane(angularVelocity, cameraUp);
				return Quaternion.Slerp(followRot, horizontalAlignedRotation2 * Quaternion.Inverse(horizontalAlignedRotation), 0.05f + velocity.sqrMagnitude * 0.005f - angularVelocity.sqrMagnitude * 0.5f);
			}
		}
		return Quaternion.identity;
	}

	private Quaternion GetHorizontalAlignedRotation(Vector3 forward, Vector3 up)
	{
		forward = Vector3.ProjectOnPlane(forward, up);
		return Quaternion.LookRotation(forward, up);
	}

	private void LerpTransformations(Machine m, bool hasMachine, bool lerpPos)
	{
		float t = uDelta * focusLerpSmooth;
		if (lerpPos)
		{
			if (hasMachine && m.isSimulating && m.isReady)
			{
				if (lerpTimer > 0f)
				{
					prevPos = PreviousTargetPos();
					posToBe = Vector3.Lerp(targetPos, prevPos, Mathf.Pow(lerpTimer, 4f));
					lerpTimer -= Time.unscaledDeltaTime;
					simTimer = 0f;
				}
				else if (simTimer > 0f)
				{
					posToBe = Vector3.Lerp(targetPos, buildPos, Mathf.Pow(simTimer / 1.5f, 4f));
					simTimer -= Time.unscaledDeltaTime;
				}
				else
				{
					posToBe = targetPos;
				}
			}
			else
			{
				posToBe = Vector3.Lerp(posToBe, targetPos, t);
				lerpTimer = 0f;
			}
			if (fadeOffset)
			{
				wasdPosOffset = (wasdPOS = Vector3.Lerp(wasdPosOffset, ZERO, t));
				if (wasdPosOffset.sqrMagnitude < 0.001f)
				{
					wasdPosOffset = ZERO;
					fadeOffset = false;
				}
			}
		}
		rotToBe = Quaternion.Lerp(rotToBe, targetRot, t);
	}

	private void SetNearClip()
	{
		float num = Mathf.Max(0.1f, minNearclip, 0.26f + 2.7f * Mathf.Clamp01(distance * 0.01f));
		if (!Mathf.Approximately(cam.nearClipPlane, num))
		{
			cam.nearClipPlane = num;
		}
	}

	private void LimitCameraToTerrain(Machine m, bool useLimit, float lz)
	{
		float num = Time.unscaledDeltaTime;
		if (usingInputs || (!resetting && Mathf.Abs(distance - lastDist) > 0.1f))
		{
			lerpedLimitDistance = distance;
			lz = position.z;
			num = 1f;
		}
		if (useLimit && OptionsMaster.BesiegeConfig.LimitCamera && !usingInputs && m.isSimulating && m.isReady && targetType == TargetType.Machine && wasdPOS.sqrMagnitude < m.GetBounds(false).extents.sqrMagnitude)
		{
			float num2 = LimitDistance(PosComposite, rotation, distance);
			if (num2 < lerpedLimitDistance)
			{
				lerpedLimitDistance = num2;
			}
			if (lerpedLimitDistance < distance)
			{
				position.z = Mathf.Lerp(lz, 0f - lerpedLimitDistance, num * 20f);
			}
		}
		lastDist = distance;
	}

	public void UpdateCam()
	{
		if (!isActive || cinematic || StatMaster.isHeadless || animating)
		{
			wasActive = isActive;
			return;
		}
		if (target == null)
		{
			ResetCamTarget();
		}
		if (scheduleTargetConversion < Time.unscaledTime)
		{
			ConvertBuildSimTarget(targetType);
		}
		Machine machine = Machine.Active();
		bool flag = machine != null;
		UpdateClientMiddle(machine, flag);
		Vector3 tPos = CurrentTargetPos();
		if (!wasActive)
		{
			PrepareOnReactivation(tPos);
		}
		dofTarget.position = tPos;
		if ((bool)target)
		{
			bool flag2 = StatMaster.Mode.isTranslating || StatMaster.Mode.isRotating || StatMaster.Mode.isScaling;
			if (!flag2)
			{
				targetPos = tPos;
			}
			targetRot = GetCameraTargetRotation(machine, flag);
			LerpTransformations(machine, flag, !flag2);
			SetCamera(posToBe, rotToBe, smoothX, smoothY, distance, 1f, flag, machine);
			SetNearClip();
			if (resetting)
			{
				distance = Mathf.Clamp(Mathf.Lerp(distance, startDistance, uDelta * (2f + 0.8f * actualZoomSmooth)), minZoom, maxZoom);
			}
			else
			{
				distance = Mathf.Clamp(distance - zoomSmoothDelegate, minZoom, maxZoom);
			}
			lerpedLimitDistance = Mathf.Lerp(lerpedLimitDistance, distance, Time.fixedDeltaTime * 2f);
		}
		SetOrthographic();
		wasActive = isActive;
	}

	public float SnapAngleTo90(float angle, ref float stored, float variance = 2f)
	{
		if (!StatMaster.advancedBuilding || machineSimulating || !snapToCardinalAxes || animating)
		{
			return angle;
		}
		float num = Mathf.Round(angle / 90f) * 90f;
		if (!InputManager.SnapCameraKeyHeld())
		{
			if (Mathf.Abs(stored - num) <= 0.1f && Mathf.Abs(angle - num) <= variance)
			{
				return stored;
			}
			stored = float.MaxValue;
			return angle;
		}
		if (Mathf.Abs(angle - num) <= variance)
		{
			return stored = num;
		}
		return stored = angle;
	}

	private void SetOrthographic()
	{
		if (machineSimulating || !StatMaster.advancedBuilding || !snapToCardinalAxes)
		{
			SetOrthographic(false);
		}
		else if (refX != float.MaxValue && refY != float.MaxValue && Mathf.Approximately(refX % 90f, 0f) && Mathf.Approximately(refY % 90f, 0f))
		{
			SetOrthographic(true);
		}
		else
		{
			SetOrthographic(false);
		}
	}

	private void SetOrthographic(bool enabled)
	{
		if (isOrthographic != enabled && CameraMoved != null)
		{
			CameraMoved(camPos);
		}
		if (enabled)
		{
			Shader.EnableKeyword("_IsOrthographic");
		}
		else
		{
			Shader.DisableKeyword("_IsOrthographic");
		}
		if (enabled)
		{
			if (!isOrthographic)
			{
				currentFarClip = cam.farClipPlane;
			}
			ApplyApproxOrthographic(cam, distance, currentFarClip);
			ApplyApproxOrthographic(hud3Dcam, distance, currentFarClip);
			orthoFloor.transform.position = PosComposite.WithY((!StatMaster.isMP) ? LevelAttributes.FloorHeight : LevelEditor.Instance.environmentManager.floorRenderer.transform.localPosition.y);
			orthoFloor.transform.rotation = Quaternion.Euler(0f, base.transform.eulerAngles.y, 0f);
			orthoFloor.SetActive(true);
			if (barrenExpanceFog != null)
			{
				barrenExpanceFog.height = orthoFloor.transform.position.y - 38f;
			}
			isOrthographic = true;
		}
		else if (isOrthographic)
		{
			ClearApproxOrthographic(cam);
			ClearApproxOrthographic(hud3Dcam);
			barrenExpanceFog.height = 5.75f;
			orthoFloor.SetActive(false);
			isOrthographic = false;
		}
	}

	public static void ApplyApproxOrthographic(Camera cam, float focalDistance, float farClip)
	{
		float num = CalculateOrthographicSize(cam, focalDistance);
		float num2 = num * cam.aspect;
		cam.projectionMatrix = Matrix4x4.Ortho(0f - num2, num2, 0f - num, num, 0.3f, farClip);
	}

	public static void ClearApproxOrthographic(Camera cam)
	{
		cam.ResetProjectionMatrix();
	}

	public static float CalculateOrthographicSize(Camera perspectiveCamera, float focalDistance)
	{
		return focalDistance * Mathf.Tan(perspectiveCamera.fieldOfView * ((float)Math.PI / 180f) * 0.5f);
	}

	public static float LimitDistance(Vector3 pos, Quaternion rotation, float distance)
	{
		Vector3 direction = rotation * Vector3.back;
		float num = distance;
		float num2 = Vector3.Dot(Vector3.down, rotation * Vector3.forward);
		float num3 = pos.y - SingleInstanceFindOnly<AddPiece>.Instance.floorHeight - cameraCollisionErrorMargin;
		float radius = Mathf.Min(num3, SingleInstanceFindOnly<MouseOrbit>.Instance.machineThickness, Mathf.Pow(Mathf.Max(0.0001f, num2), 0.25f), 1f);
		RaycastHit[] array = Physics.SphereCastAll(pos, radius, direction, distance, environmentMask, QueryTriggerInteraction.Ignore);
		for (int i = 0; i < array.Length; i++)
		{
			float num4 = array[i].distance;
			if (num4 < num && num4 > 0f)
			{
				Collider collider = array[i].collider;
				if (!IsBlock(collider.transform) && (!(num2 < -0.35f) || (collider.gameObject.layer != 29 && collider.gameObject.layer != 8)) && (!collider.attachedRigidbody || collider.attachedRigidbody.isKinematic || !(collider.bounds.size.sqrMagnitude < 100f)) && !collider.name.Contains("Floor") && !collider.CompareTag("Cannonball") && !collider.CompareTag("Projectile") && !collider.CompareTag("CamIgnored"))
				{
					num = num4;
					Debug.DrawRay(array[i].point, array[i].normal, Color.yellow);
				}
			}
		}
		return num;
	}

	public static void FixSphereCast(ref Vector3 origin, float radius, Vector3 direction, ref float distance)
	{
		float num = 0.25f;
		int num2 = (int)(distance / num);
		for (int i = 0; i < num2; i++)
		{
			float num3 = num * (float)i;
			Vector3 vector = origin + direction * num3;
			if (!Physics.CheckSphere(vector, radius, environmentMask, QueryTriggerInteraction.Ignore))
			{
				origin = vector;
				distance -= num3;
				if (distance < 0.1f)
				{
					distance = 0.1f;
				}
				break;
			}
		}
	}

	private static bool IsBlock(Transform t)
	{
		if (StatMaster.isMP)
		{
			foreach (KeyValuePair<uint, List<BlockBehaviour>> simulationBlock in ReferenceMaster.SimulationBlocks)
			{
				if (simulationBlock.Value.Count > 0 && t.root == simulationBlock.Value[0].transform.root)
				{
					return true;
				}
			}
		}
		else if (t.root == Machine.Active().transform)
		{
			return true;
		}
		return false;
	}

	public bool ConvertBuildSimTarget(TargetType targetType)
	{
		scheduleTargetConversion = float.MaxValue;
		if ((bool)buildTarget)
		{
			targetInfo = buildTarget;
			this.targetType = GetTargetType(targetInfo);
			wasdPosOffset = buildWasdPos;
			buildTarget = null;
		}
		if (targetInfo == null)
		{
			return false;
		}
		switch (targetType)
		{
		case TargetType.Misc:
			switch (targetInfo.infoType)
			{
			case BasicInfo.BasicInfoType.Block:
				return ConvertBuildSimTarget(TargetType.Block);
			case BasicInfo.BasicInfoType.Entity:
				return ConvertBuildSimTarget(TargetType.Entity);
			default:
				if (targetConversionEnteringSim)
				{
					return ConvertBuildSimMiscPhysics(SingleInstanceFindOnly<AddPiece>.Instance.physicsGoalObject, ReferenceMaster.physicsGoalInstance);
				}
				return ConvertBuildSimMiscPhysics(ReferenceMaster.physicsGoalInstance, SingleInstanceFindOnly<AddPiece>.Instance.physicsGoalObject);
			}
		case TargetType.Entity:
		{
			GenericEntity genericEntity = targetInfo as GenericEntity;
			if (genericEntity.entity.isStatic)
			{
				break;
			}
			if (!genericEntity.entity.isSimulating)
			{
				LevelEntity simEntity = genericEntity.entity.simEntity;
				if ((bool)simEntity && simEntity.hasBehaviour)
				{
					targetInfo = simEntity.behaviour;
					target = simEntity.transform;
					return true;
				}
			}
			else
			{
				LevelEntity buildEntity = genericEntity.entity.buildEntity;
				if ((bool)buildEntity && buildEntity.hasBehaviour)
				{
					targetInfo = buildEntity.behaviour;
					target = buildEntity.transform;
					return true;
				}
			}
			break;
		}
		case TargetType.Block:
		{
			BlockBehaviour blockBehaviour = targetInfo as BlockBehaviour;
			if (blockBehaviour.ParentMachine.isSimulating)
			{
				if (blockBehaviour.hasSimBlock)
				{
					targetInfo = blockBehaviour.SimBlock;
					target = targetInfo.transform;
					return true;
				}
				break;
			}
			BlockBehaviour buildingBlock = blockBehaviour.BuildingBlock;
			if (!(buildingBlock != null))
			{
				break;
			}
			targetInfo = blockBehaviour.BuildingBlock;
			target = targetInfo.transform;
			return true;
		}
		}
		return false;
	}

	private bool ConvertBuildSimMiscPhysics(Transform fromRoot, Transform toRoot)
	{
		if (!fromRoot || !toRoot)
		{
			return false;
		}
		List<string> path;
		List<int> siblingIndices;
		if (!targetInfo.transform.GetPath(out path, out siblingIndices, fromRoot))
		{
			return false;
		}
		Transform transform = null;
		EntityAI component = targetInfo.GetComponent<EntityAI>();
		if ((bool)component && component.parentToPhysicsGoal)
		{
			EntityAI[] componentsInChildren = toRoot.GetComponentsInChildren<EntityAI>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Vector3 vector = ((!component.my.basicInfo.isSimulating) ? targetInfo.transform.position : component.simStartPosition);
				if (vector == componentsInChildren[i].transform.position)
				{
					transform = componentsInChildren[i].transform;
					break;
				}
			}
		}
		else
		{
			transform = toRoot.Find(siblingIndices);
		}
		if (!transform)
		{
			return false;
		}
		BasicInfo component2 = transform.GetComponent<BasicInfo>();
		if (!component2)
		{
			return false;
		}
		targetInfo = component2;
		target = transform;
		return true;
	}

	public void ResetLerp()
	{
		lerpTimer = 1f;
	}

	public Vector3 CurrentTargetPos()
	{
		if ((bool)targetInfo)
		{
			return targetInfo.GetCenter();
		}
		return target.position;
	}

	public Vector3 PreviousTargetPos()
	{
		if ((bool)prevTargetInfo)
		{
			return prevTargetInfo.GetCenter();
		}
		if ((bool)prevTarget)
		{
			return prevTarget.position;
		}
		return prevPos;
	}

	public Vector3 GetTargetCenter(BasicInfo info)
	{
		return info.GetCenter();
	}

	public void SetTarget(GenericEntity entity)
	{
		SetPrevTarget();
		target = entity.transform;
		targetInfo = entity;
		targetType = TargetType.Entity;
		wasdPosOffset = ZERO;
	}

	public void SetTarget(BlockBehaviour block)
	{
		SetPrevTarget();
		target = block.transform;
		targetInfo = block;
		targetType = TargetType.Block;
		wasdPosOffset = ZERO;
	}

	public Ray GetFixedUpdateRelativeRay()
	{
		Vector3 vector = InputManager.CursorPosition();
		Ray result = Camera.main.ScreenPointToRay(new Vector3(vector.x, vector.y, 0f));
		BasicInfo refBlock = targetInfo;
		Machine machine = Machine.Active();
		if ((refBlock != null && refBlock.isSimulating) || (machine != null && machine.isSimulating))
		{
			Vector3 vector2 = CurrentTargetPos();
			if (targetType == TargetType.Machine)
			{
				refBlock = machine.RefBlock;
				if (refBlock == null)
				{
					Debug.LogError("Missing RefBlock on Machine");
				}
				else
				{
					vector2 = refBlock.GetCenter();
				}
			}
			Vector3 vector3 = result.origin - vector2;
			Vector3 vector4;
			if ((bool)refBlock && !refBlock.isDestroyed && refBlock.gameObject.activeInHierarchy)
			{
				if (refBlock.noRigidbody || !refBlock.Rigidbody || refBlock.Rigidbody.isKinematic)
				{
					vector4 = refBlock.GetCenter();
				}
				else
				{
					Vector3 vector5 = refBlock.GetCenter() - refBlock.transform.position;
					vector4 = refBlock.Rigidbody.position + vector5;
				}
			}
			else
			{
				vector4 = vector2;
			}
			Vector3 origin = vector4 + vector3;
			result.origin = origin;
		}
		return result;
	}

	public Vector3 FixedPointToCameraPoint(Vector3 pos)
	{
		BasicInfo refBlock = targetInfo;
		Machine machine = Machine.Active();
		if ((refBlock != null && refBlock.isSimulating) || (machine != null && machine.isSimulating))
		{
			Vector3 vector = CurrentTargetPos();
			if (targetType == TargetType.Machine)
			{
				refBlock = machine.RefBlock;
				if (refBlock == null)
				{
					Debug.LogError("Missing RefBlock on Machine");
				}
				else
				{
					vector = refBlock.GetCenter();
				}
			}
			Vector3 vector2;
			if ((bool)refBlock && !refBlock.isDestroyed && refBlock.gameObject.activeInHierarchy)
			{
				if (refBlock.noRigidbody || refBlock.Rigidbody.isKinematic)
				{
					vector2 = refBlock.GetCenter();
				}
				else
				{
					Vector3 vector3 = refBlock.GetCenter() - refBlock.transform.position;
					vector2 = refBlock.Rigidbody.position + vector3;
				}
			}
			else
			{
				vector2 = vector;
			}
			Vector3 vector4 = pos - vector2;
			Vector3 vector5 = vector + vector4;
			pos = vector5;
		}
		return pos;
	}

	private void GetTarget()
	{
		Ray fixedUpdateRelativeRay = GetFixedUpdateRelativeRay();
		List<Collider> list = new List<Collider>();
		if (StatMaster.isMP)
		{
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				if (!playerData.isSpectator && !playerData.machine.SimPhysics && (!playerData.isLocalPlayer || playerData.machine.isSimulating))
				{
					Transform transform = playerData.machine.SimulationMachine;
					if (!playerData.isLocalPlayer && !playerData.machine.isSimulating)
					{
						transform = playerData.machine.BuildingMachine;
					}
					if (transform != null)
					{
						list.AddRange(transform.GetComponentsInChildren<Collider>(true));
					}
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].enabled = true;
			}
			NetworkAuxAddPiece.Instance.TurnOffZoneColliders();
		}
		RaycastHit[] array = Physics.RaycastAll(fixedUpdateRelativeRay, float.PositiveInfinity, layerMasky);
		Vector3 vector = base.transform.position;
		RaycastEntry raycastEntry = null;
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit hit = array[i];
			float sqrMagnitude = (hit.point - vector).sqrMagnitude;
			if (StatMaster.isMP)
			{
				LevelEntity levelEntity = hit.collider.GetComponentInParent<LevelEntity>();
				if (levelEntity != null)
				{
					if (levelEntity.hasBase)
					{
						levelEntity = levelEntity.baseEntity as LevelEntity;
					}
					GenericEntity behaviour = levelEntity.behaviour;
					if (levelEntity.hasBehaviour && behaviour.prefab.ignoreInPlaceMode)
					{
						continue;
					}
				}
			}
			if (!flag || sqrMagnitude < raycastEntry.camDistance)
			{
				RaycastEntry raycastEntry2 = new RaycastEntry();
				raycastEntry2.hit = hit;
				raycastEntry2.camDistance = sqrMagnitude;
				raycastEntry = raycastEntry2;
				flag = true;
			}
		}
		if (flag)
		{
			RaycastHit hit2 = raycastEntry.hit;
			Rigidbody attachedRigidbody = hit2.collider.attachedRigidbody;
			if ((bool)attachedRigidbody && !hit2.collider.CompareTag("FocusColNotRigidbody"))
			{
				if (!attachedRigidbody.GetComponent<IceController>())
				{
					SetPrevTarget();
					target = attachedRigidbody.transform;
					targetInfo = target.GetComponentInParent<BasicInfo>();
					targetType = GetTargetType(targetInfo);
					wasdPOS = wasdPosOffset;
					fadeOffset = true;
					MakeRipple(hit2);
				}
			}
			else
			{
				SetPrevTarget();
				target = hit2.collider.transform;
				targetInfo = target.GetComponentInParent<BasicInfo>();
				targetType = GetTargetType(targetInfo);
				wasdPOS = wasdPosOffset;
				fadeOffset = true;
				MakeRipple(hit2);
			}
		}
		if (StatMaster.isMP)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i].enabled = false;
			}
		}
	}

	protected TargetType GetTargetType(BasicInfo targetInfo)
	{
		if (targetInfo != null)
		{
			switch (targetInfo.infoType)
			{
			case BasicInfo.BasicInfoType.Block:
				return TargetType.Block;
			case BasicInfo.BasicInfoType.Entity:
				return TargetType.Entity;
			default:
				return TargetType.Misc;
			}
		}
		return TargetType.Misc;
	}

	public void FocusBlock(BlockBehaviour block)
	{
		SetPrevTarget();
		targetType = TargetType.Block;
		targetInfo = block;
	}

	private void ResetFocusRipple()
	{
		if (focusCoroutine != null)
		{
			StopCoroutine(focusCoroutine);
			focusCoroutine = null;
		}
		focusRipple.Clear();
		focusRipple.Stop();
	}

	private void MakeRipple(RaycastHit hit)
	{
		if (OptionsMaster.BesiegeConfig.MiddleClickVFX && !StatMaster.hudHidden && !(focusRipple == null))
		{
			AudioSource component = focusRipple.GetComponent<AudioSource>();
			component.Play();
			ResetFocusRipple();
			focusCoroutine = IEFocus(target, hit.point);
			StartCoroutine(focusCoroutine);
		}
	}

	private IEnumerator IEFocus(Transform target, Vector3 globalPoint)
	{
		Vector3 hitOffset = globalPoint - target.position;
		Transform focusTransform = focusRipple.transform;
		focusRipple.Play();
		float c = 0f;
		float simThreshold = 0.01f;
		float d = Time.unscaledDeltaTime;
		float ts = Time.timeScale;
		if (ts > simThreshold)
		{
			focusRipple.playbackSpeed = d / Time.deltaTime;
		}
		while (c < 1.5f && target != null)
		{
			focusTransform.position = hitOffset + target.position;
			if (ts < simThreshold)
			{
				focusRipple.Simulate(d, true, false);
			}
			c += d;
			yield return null;
			d = Time.unscaledDeltaTime;
			ts = Time.timeScale;
		}
		ResetFocusRipple();
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		while (angle < -360f)
		{
			angle += 360f;
		}
		while (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	private void WASD()
	{
		if (!StatMaster.stopHotkeys && !StatMaster.stopWASDcamMovement)
		{
			bool flag = InputManager.Camera.RightKeyHeld();
			bool flag2 = InputManager.Camera.LeftKeyHeld();
			bool flag3 = InputManager.Camera.ForwardKeyHeld();
			bool flag4 = InputManager.Camera.BackwardKeyHeld();
			if (flag || flag2 || flag3 || flag4)
			{
				usingInputs = true;
			}
			Vector3 right = myTransform.right;
			float num = ((!InputManager.AdvancedBuilding.LeftShiftKey()) ? 1f : 3f);
			num *= 0.1f + 5f * Mathf.Pow((distance - minZoom) / (maxZoom - minZoom), 0.7f);
			num *= 0.72f;
			float num2 = wasdSpeed * num * Time.unscaledDeltaTime * 100f;
			if (useNewCalculation)
			{
				Transform parent = myTransform.parent.parent.parent.parent;
				Transform parent2 = myTransform.parent.parent;
				Vector3 vector = parent.TransformPoint(wasdPosOffset);
				Vector3 zERO = ZERO;
				zERO += ((!flag3) ? ZERO : parent2.forward);
				zERO -= ((!flag4) ? ZERO : parent2.forward);
				zERO += ((!flag) ? ZERO : parent2.right);
				zERO -= ((!flag2) ? ZERO : parent2.right);
				if (zERO != ZERO)
				{
					Vector3 vector2 = vector + zERO * num2;
					Debug.DrawRay(vector, zERO * num2, Color.red);
					Debug.DrawLine(vector, vector2, Color.yellow);
					vector = vector2;
				}
				wasdPosOffset = parent.InverseTransformPoint(vector);
			}
			else
			{
				if (flag || flag2)
				{
					fadeOffset = false;
					wasdPosOffset += right * num2 * ((flag && flag2) ? 0f : ((!flag) ? (-1f) : 1f));
				}
				if (flag3 || flag4)
				{
					fadeOffset = false;
					wasdPosOffset += Vector3.Cross(right, Vector3.up) * num2 * ((flag3 && flag4) ? 0f : ((!flag3) ? (-1f) : 1f));
				}
			}
			if (!wasdTutorialDisplayed && (wasdPosOffset.x != 0f || wasdPosOffset.y != 0f || wasdPosOffset.z != 0f))
			{
				TutorialSystem.StartCustomTutorial("TutorialContainerWASD");
				wasdTutorialDisplayed = true;
			}
		}
		wasdPOS = Vector3.Lerp(wasdPOS, wasdPosOffset, uDelta * actualWasdSmooth);
	}

	public void FocusOnTarget(Vector3 targetPosition)
	{
		SetPrevTarget();
		if (dummyTarget == null)
		{
			dummyTarget = new GameObject("CameraDummyTarget").transform;
		}
		dummyTarget.position = targetPosition;
		target = dummyTarget;
		targetType = TargetType.Machine;
		wasdPosOffset = ZERO;
		targetInfo = null;
	}

	public void ResetCam()
	{
		StopAllCoroutines();
		bool flag = animating;
		animating = false;
		resetting = true;
		smoothX %= 360f;
		Set(startX, startY, startDistance);
		wasdPosOffset = ZERO;
		up = Vector3.up;
		buildTarget = null;
		buildWasdPos = ZERO;
		ResetCamTarget();
		if (flag)
		{
			IntroAnimationEnded();
		}
	}

	public void ResetCamTarget()
	{
		SetPrevTarget();
		targetType = TargetType.Machine;
		target = machineTarget;
		targetInfo = null;
	}

	public void Set(float x, float y, float distance)
	{
		this.x = x % 360f;
		this.y = y;
		smoothX %= 360f;
		scrl = (this.distance - distance) / (this.distance * scrollSensitivityScaler);
	}

	public void HardSet(float x, float y, float distance)
	{
		smoothX = x;
		smoothY = y;
		Set(x, y, distance);
		this.distance = distance;
		posToBe = (targetPos = target.position);
	}

	public void HardReset()
	{
		animating = false;
		resetting = true;
		wasdPosOffset = ZERO;
		HardSet(startX, startY, startDistance);
		ResetCamTarget();
	}

	public void SoftResetCamTarget()
	{
		if (targetInfo is BlockBehaviour && (targetInfo as BlockBehaviour).isSimulating)
		{
			buildTarget = (targetInfo as BlockBehaviour).BuildingBlock;
			buildWasdPos = wasdPosOffset;
		}
		else
		{
			buildTarget = null;
			buildWasdPos = ZERO;
		}
		ResetCamTarget();
		posToBe = CurrentTargetPos();
		Vector3 vector = prevPos - posToBe;
		wasdPOS += vector;
		wasdPosOffset += vector;
		fadeOffset = false;
	}

	public void UpdateCamPlane()
	{
		cameraPlane.SetNormalAndPosition(camForward, myTransform.position);
	}

	public void SetPrevTarget()
	{
		prevPos = posToBe;
		prevTarget = target;
		prevTargetInfo = targetInfo;
		ResetLerp();
	}
}
