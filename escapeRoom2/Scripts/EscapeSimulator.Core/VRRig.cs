using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class VRRig : MonoBehaviour
{
	[Serializable]
	public class Controller
	{
		public TrackedPoseDriver trackedPoseDriver;

		public TrackedPoseDriverFollower trackedPoseDriverFollower;

		public GameObject visuals;

		public Transform transform;

		public Transform attachPoint;

		public Transform attachRotator;

		public Transform raycastSource;

		public Transform touchSource;

		public Transform grabCenter;

		public Transform grabDistanceCheckCenter;

		public Transform inventorySource;

		public LineRenderer raycastLineRenderer;

		public GameObject raycastDestinationIndicator;

		public SpriteRenderer raycastDestinationIndicatorRenderer;

		public SpriteRenderer raycastPressIndicatorRenderer;

		public Image raycastDestinationIndicatorProgress;

		public Animator handAnimator;

		public List<VRFinger> fingers;

		public List<GameObject> shortSleeves;

		public List<GameObject> longSleeves;

		public List<VRTutorialController> tutorialControllers;

		public SkinnedMeshRenderer handMesh;

		public MaterialPropertyBlock handsPropertyBlock;

		public LineRenderer teleportArcRenderer;

		public GameObject teleportMarker;

		public VRWatch watch;

		[NonSerialized]
		public GripState gripState = new GripState();

		[NonSerialized]
		public GameObject uiHitLastFrame;

		[NonSerialized]
		public GameObject uiVibratorOnLastHitChange;

		[NonSerialized]
		public VRInventoryItem hoveredPocketLastFrame;

		[NonSerialized]
		public VRInventoryItem hoveredTrashcanLastFrame;

		[NonSerialized]
		public Game.VRGrabData grabData;

		[NonSerialized]
		public Game.VRHoveredSlot hoveredSlot;

		[NonSerialized]
		public Game.VRHoveredSurfaceSlot hoveredSurfaceSlot;

		[NonSerialized]
		public Switch3D touchedSwitchLastFrame;

		[NonSerialized]
		public Vector2 lastFrameTeleportInput;

		[NonSerialized]
		public Game.PrimaryButtonState primaryButtonState;

		[NonSerialized]
		public Game.MovementThumbstickState movementThumbstickState;

		[NonSerialized]
		public Game.RotateThumbstickState rotateThumbstickState;

		[NonSerialized]
		public float holdingDownDuration;

		[NonSerialized]
		public float teleportLineAlphaCurrent = -1f;

		[NonSerialized]
		public Gradient teleportLineGradient = new Gradient();

		[NonSerialized]
		public GradientColorKey[] teleportLineGradientColor = new GradientColorKey[1];

		[NonSerialized]
		public GradientAlphaKey[] teleportLineGradientAlpha = new GradientAlphaKey[3];

		[NonSerialized]
		public Vector3[] teleportArcPoints = new Vector3[30];

		[NonSerialized]
		public Vector3 hitPoint;

		[NonSerialized]
		public bool isInShoulderStashVolume;
	}

	[Serializable]
	public class DeviceSpecificSettings
	{
		public VR.DeviceType deviceType;

		public Vector3 leftControllerLocalPosition;

		public Quaternion leftControllerLocalRotation;

		public Vector3 rightControllerLocalPosition;

		public Quaternion rightControllerLocalRotation;
	}

	public class GripState
	{
		public bool isPressed;

		public bool wasPressedThisFrame;

		public bool wasReleasedThisFrame;
	}

	public TrackedPoseDriver headTrackedPoseDriver;

	public Transform headCameraOffset;

	public VRFade fadeEffect;

	public VRFlash flashEffect;

	public VROverlay overlay;

	public GameObject wallSphere;

	public Renderer wallSphereRenderer;

	public Camera headCamera;

	public Camera smoothedCamera;

	public Transform inventorySource;

	public TunnelingVignette vignette;

	public Renderer vignetteRenderer;

	public Canvas pauseCanvas;

	public Canvas mainMenuCanvas;

	public Canvas popupCanvas;

	public float grabRadius = 0.1f;

	public ParticleSystem skyConfettiEffect;

	public List<ParticleSystem> confettiEffects;

	public List<DeviceSpecificSettings> deviceSpecificSettings;

	public Controller leftController;

	public Controller rightController;

	public Transform shoulderStashVolumeCenter;

	public Vector3 shoulderStashVolumeExtents = new Vector3(0.5f, 0.5f, 0.5f);

	[Header("Design")]
	public Gradient defaultRayGradient;

	public Gradient hoveringRayGradient;

	public Color defaultHitIndicatorColor = Color.white;

	public Color hoveringHitIndicatorColor = Color.yellow;

	[Range(0f, 1f)]
	public float hitIndicatorAlpha = 1f;

	public Material defaultHands;

	public Material transparentHands;

	public Dictionary<GameObject, List<Material>> defaultSleeves;

	private void OnDrawGizmos()
	{
		drawGizmosForController(leftController);
		drawGizmosForController(rightController);
		drawShoulderStashAreaVolume();
	}

	private void drawGizmosForController(Controller controller)
	{
		Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
		Gizmos.DrawSphere(controller.grabCenter.position, grabRadius);
		foreach (VRFinger finger in controller.fingers)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(finger.source.position, finger.radius);
		}
	}

	private void drawShoulderStashAreaVolume()
	{
		Vector3 vector = shoulderStashVolumeExtents * -1f;
		Vector3 vector2 = shoulderStashVolumeExtents;
		Vector3 item = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector.x, vector.y, vector.z));
		Vector3 vector3 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector.x, vector.y, vector2.z));
		Vector3 vector4 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector.x, vector2.y, vector.z));
		Vector3 vector5 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector.x, vector2.y, vector2.z));
		Vector3 vector6 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector2.x, vector.y, vector.z));
		Vector3 vector7 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector2.x, vector.y, vector2.z));
		Vector3 vector8 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector2.x, vector2.y, vector.z));
		Vector3 item2 = shoulderStashVolumeCenter.TransformPoint(new Vector3(vector2.x, vector2.y, vector2.z));
		Span<(Vector3, Vector3)> span = stackalloc(Vector3, Vector3)[12]
		{
			(item, vector3),
			(item, vector4),
			(item, vector6),
			(vector3, vector5),
			(vector3, vector7),
			(vector4, vector5),
			(vector4, vector8),
			(vector5, item2),
			(vector6, vector7),
			(vector6, vector8),
			(vector7, item2),
			(vector8, item2)
		};
		for (int i = 0; i < span.Length; i++)
		{
			(Vector3, Vector3) tuple = span[i];
			Gizmos.color = Color.green;
			Gizmos.DrawLine(tuple.Item1, tuple.Item2);
		}
	}
}
