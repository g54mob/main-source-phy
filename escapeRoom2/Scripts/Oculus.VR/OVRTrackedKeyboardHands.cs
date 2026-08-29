using System;
using Meta.XR.Util;
using UnityEngine;

[Feature(Feature.TrackedKeyboard)]
[HelpURL("https://developer.oculus.com/documentation/unity/tk-overview/")]
public class OVRTrackedKeyboardHands : MonoBehaviour
{
	private struct HandBoneMapping
	{
		public Transform LeftHandTransform;

		public Transform LeftPresenceTransform;

		public Transform RightHandTransform;

		public Transform RightPresenceTransform;

		public OVRSkeleton.BoneId BoneName;

		public string HandPresenceLeftBoneName;

		public string HandPresenceRightBoneName;
	}

	public struct TrackedKeyboardHandsVisibilityChangedEvent
	{
		public bool leftVisible;

		public bool rightVisible;
	}

	public GameObject LeftHandPresence;

	public GameObject RightHandPresence;

	private bool handPresenceInitialized_;

	private Transform leftHandRoot_;

	private Transform rightHandRoot_;

	public OVRTrackedKeyboard KeyboardTracker;

	private OVRCameraRig cameraRig_;

	private OVRHand leftHand_;

	private OVRSkeleton leftHandSkeleton_;

	private OVRSkeletonRenderer leftHandSkeletonRenderer_;

	private GameObject leftHandSkeletonRendererGO_;

	private SkinnedMeshRenderer leftHandSkinnedMeshRenderer_;

	private OVRMeshRenderer leftHandMeshRenderer_;

	private OVRHand rightHand_;

	private OVRSkeleton rightHandSkeleton_;

	private OVRSkeletonRenderer rightHandSkeletonRenderer_;

	private GameObject rightHandSkeletonRendererGO_;

	private OVRMeshRenderer rightHandMeshRenderer_;

	private SkinnedMeshRenderer rightHandSkinnedMeshRenderer_;

	private static readonly float handInnerAlphaThreshold_ = 0.08f;

	private static readonly float handOuterAlphaThreshold_ = 0.2f;

	private static readonly float maximumPassthroughHandsDistance_ = 0.18f;

	private static readonly float minimumModelHandsDistance_ = 0.11f;

	private TrackedKeyboardHandsVisibilityChangedEvent? lastVisibilityEvent_;

	private readonly HandBoneMapping[] boneMappings_ = new HandBoneMapping[18]
	{
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Start,
			HandPresenceLeftBoneName = "b_l_wrist",
			HandPresenceRightBoneName = "b_r_wrist"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Thumb0,
			HandPresenceLeftBoneName = "b_l_thumb0",
			HandPresenceRightBoneName = "b_r_thumb0"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Thumb1,
			HandPresenceLeftBoneName = "b_l_thumb1",
			HandPresenceRightBoneName = "b_r_thumb1"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Thumb2,
			HandPresenceLeftBoneName = "b_l_thumb2",
			HandPresenceRightBoneName = "b_r_thumb2"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Thumb3,
			HandPresenceLeftBoneName = "b_l_thumb3",
			HandPresenceRightBoneName = "b_r_thumb3"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Index1,
			HandPresenceLeftBoneName = "b_l_index1",
			HandPresenceRightBoneName = "b_r_index1"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Index2,
			HandPresenceLeftBoneName = "b_l_index2",
			HandPresenceRightBoneName = "b_r_index2"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Index3,
			HandPresenceLeftBoneName = "b_l_index3",
			HandPresenceRightBoneName = "b_r_index3"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Middle1,
			HandPresenceLeftBoneName = "b_l_middle1",
			HandPresenceRightBoneName = "b_r_middle1"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Middle2,
			HandPresenceLeftBoneName = "b_l_middle2",
			HandPresenceRightBoneName = "b_r_middle2"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Middle3,
			HandPresenceLeftBoneName = "b_l_middle3",
			HandPresenceRightBoneName = "b_r_middle3"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Ring1,
			HandPresenceLeftBoneName = "b_l_ring1",
			HandPresenceRightBoneName = "b_r_ring1"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Ring2,
			HandPresenceLeftBoneName = "b_l_ring2",
			HandPresenceRightBoneName = "b_r_ring2"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Ring3,
			HandPresenceLeftBoneName = "b_l_ring3",
			HandPresenceRightBoneName = "b_r_ring3"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Pinky0,
			HandPresenceLeftBoneName = "b_l_pinky0",
			HandPresenceRightBoneName = "b_r_pinky0"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Pinky1,
			HandPresenceLeftBoneName = "b_l_pinky1",
			HandPresenceRightBoneName = "b_r_pinky1"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Pinky2,
			HandPresenceLeftBoneName = "b_l_pinky2",
			HandPresenceRightBoneName = "b_r_pinky2"
		},
		new HandBoneMapping
		{
			BoneName = OVRSkeleton.BoneId.Hand_Pinky3,
			HandPresenceLeftBoneName = "b_l_pinky3",
			HandPresenceRightBoneName = "b_r_pinky3"
		}
	};

	public Material HandsMaterial;

	private const float XSCALE = 0.73f;

	private const float YSCALE = 0.8f;

	private const float FORWARD_OFFSET = -0.02f;

	private int keyboardPositionID_;

	private int keyboardRotationID_;

	private int keyboardScaleID_;

	public bool RightHandOverKeyboard { get; private set; }

	public bool LeftHandOverKeyboard { get; private set; }

	private bool AreControllersActive
	{
		get
		{
			if (!leftHand_.IsTracked)
			{
				return !rightHand_.IsTracked;
			}
			return false;
		}
	}

	private void Awake()
	{
		OVRTrackedKeyboard keyboardTracker = KeyboardTracker;
		keyboardTracker.TrackedKeyboardActiveChanged = (Action<OVRTrackedKeyboard.TrackedKeyboardSetActiveEvent>)Delegate.Combine(keyboardTracker.TrackedKeyboardActiveChanged, new Action<OVRTrackedKeyboard.TrackedKeyboardSetActiveEvent>(TrackedKeyboardActiveUpdated));
		OVRTrackedKeyboard keyboardTracker2 = KeyboardTracker;
		keyboardTracker2.TrackedKeyboardVisibilityChanged = (Action<OVRTrackedKeyboard.TrackedKeyboardVisibilityChangedEvent>)Delegate.Combine(keyboardTracker2.TrackedKeyboardVisibilityChanged, new Action<OVRTrackedKeyboard.TrackedKeyboardVisibilityChangedEvent>(TrackedKeyboardVisibilityChanged));
		keyboardPositionID_ = Shader.PropertyToID("_KeyboardPosition");
		keyboardRotationID_ = Shader.PropertyToID("_KeyboardRotation");
		keyboardScaleID_ = Shader.PropertyToID("_KeyboardScale");
	}

	private void Start()
	{
		cameraRig_ = UnityEngine.Object.FindAnyObjectByType<OVRCameraRig>();
		leftHand_ = cameraRig_.leftHandAnchor.GetComponentInChildren<OVRHand>();
		if (leftHand_ == null)
		{
			leftHand_ = cameraRig_.leftControllerAnchor.GetComponentInChildren<OVRHand>();
		}
		rightHand_ = cameraRig_.rightHandAnchor.GetComponentInChildren<OVRHand>();
		if (rightHand_ == null)
		{
			rightHand_ = cameraRig_.rightControllerAnchor.GetComponentInChildren<OVRHand>();
		}
		leftHandSkeleton_ = leftHand_.GetComponent<OVRSkeleton>();
		rightHandSkeleton_ = rightHand_.GetComponent<OVRSkeleton>();
		leftHandMeshRenderer_ = leftHand_.GetComponent<OVRMeshRenderer>();
		rightHandMeshRenderer_ = rightHand_.GetComponent<OVRMeshRenderer>();
		leftHandSkeletonRenderer_ = leftHand_.GetComponent<OVRSkeletonRenderer>();
		rightHandSkeletonRenderer_ = rightHand_.GetComponent<OVRSkeletonRenderer>();
		if (!leftHandSkeletonRenderer_.enabled)
		{
			leftHandSkeletonRenderer_ = null;
			rightHandSkeletonRenderer_ = null;
		}
		leftHandSkinnedMeshRenderer_ = leftHand_.GetComponent<SkinnedMeshRenderer>();
		rightHandSkinnedMeshRenderer_ = rightHand_.GetComponent<SkinnedMeshRenderer>();
		GameObject gameObject = UnityEngine.Object.Instantiate(LeftHandPresence);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(RightHandPresence);
		leftHandRoot_ = gameObject.transform;
		rightHandRoot_ = gameObject2.transform;
		gameObject.SetActive(value: false);
		gameObject2.SetActive(value: false);
		RetargetHandTrackingToHandPresence();
		base.enabled = false;
	}

	private void LateUpdate()
	{
		if (AreControllersActive)
		{
			DisableHandObjects();
			return;
		}
		HandBoneMapping[] array = boneMappings_;
		for (int i = 0; i < array.Length; i++)
		{
			HandBoneMapping handBoneMapping = array[i];
			handBoneMapping.LeftPresenceTransform.localRotation = handBoneMapping.LeftHandTransform.localRotation;
			handBoneMapping.RightPresenceTransform.localRotation = handBoneMapping.RightHandTransform.localRotation;
			if (handBoneMapping.BoneName == OVRSkeleton.BoneId.Hand_Start)
			{
				handBoneMapping.LeftPresenceTransform.rotation = handBoneMapping.LeftHandTransform.rotation;
				handBoneMapping.RightPresenceTransform.rotation = handBoneMapping.RightHandTransform.rotation;
				float handScale = leftHand_.HandScale;
				float handScale2 = rightHand_.HandScale;
				handBoneMapping.RightPresenceTransform.localScale = new Vector3(handScale2, handScale2, handScale2);
				handBoneMapping.LeftPresenceTransform.localScale = new Vector3(handScale, handScale, handScale);
			}
		}
		rightHandRoot_.position = rightHand_.transform.position;
		rightHandRoot_.rotation = rightHand_.transform.rotation;
		leftHandRoot_.position = leftHand_.transform.position;
		leftHandRoot_.rotation = leftHand_.transform.rotation;
		float handDistanceToKeyboard = GetHandDistanceToKeyboard(leftHandSkeleton_);
		float handDistanceToKeyboard2 = GetHandDistanceToKeyboard(rightHandSkeleton_);
		LeftHandOverKeyboard = ShouldEnablePassthrough(handDistanceToKeyboard);
		RightHandOverKeyboard = ShouldEnablePassthrough(handDistanceToKeyboard2);
		KeyboardTracker.HandsOverKeyboard = RightHandOverKeyboard || LeftHandOverKeyboard;
		bool enableLeftModel = ShouldEnableModel(handDistanceToKeyboard);
		bool enableRightModel = ShouldEnableModel(handDistanceToKeyboard2);
		SetHandModelsEnabled(enableLeftModel, enableRightModel);
		if (KeyboardTracker.Presentation == OVRTrackedKeyboard.KeyboardPresentation.PreferOpaque)
		{
			leftHandRoot_.gameObject.SetActive(value: false);
			rightHandRoot_.gameObject.SetActive(value: false);
		}
		else
		{
			leftHandRoot_.gameObject.SetActive(LeftHandOverKeyboard);
			rightHandRoot_.gameObject.SetActive(RightHandOverKeyboard);
		}
		Vector3? vector = KeyboardTracker.ActiveKeyboardTransform?.position;
		Quaternion? quaternion = KeyboardTracker.ActiveKeyboardTransform?.rotation;
		Vector3 vector2 = ((KeyboardTracker.ActiveKeyboardTransform == null) ? Vector3.zero : (KeyboardTracker.ActiveKeyboardTransform.forward * -0.02f));
		HandsMaterial.SetVector(keyboardPositionID_, vector.HasValue ? (vector.Value + vector2) : Vector3.zero);
		HandsMaterial.SetVector(keyboardRotationID_, quaternion.HasValue ? quaternion.Value.eulerAngles : Vector3.zero);
		HandsMaterial.SetVector(keyboardScaleID_, new Vector4(KeyboardTracker.ActiveKeyboardInfo.Dimensions.x * 0.73f, 0.1f, KeyboardTracker.ActiveKeyboardInfo.Dimensions.z * 0.8f, 1f));
		if (!lastVisibilityEvent_.HasValue || LeftHandOverKeyboard != lastVisibilityEvent_.Value.leftVisible || RightHandOverKeyboard != lastVisibilityEvent_.Value.rightVisible)
		{
			lastVisibilityEvent_ = new TrackedKeyboardHandsVisibilityChangedEvent
			{
				leftVisible = LeftHandOverKeyboard,
				rightVisible = RightHandOverKeyboard
			};
			KeyboardTracker.UpdateKeyboardVisibility();
		}
		if (LeftHandOverKeyboard || RightHandOverKeyboard)
		{
			OVRPlugin.InsightPassthroughKeyboardHandsIntensity intensity = new OVRPlugin.InsightPassthroughKeyboardHandsIntensity
			{
				LeftHandIntensity = ComputeOpacity(handDistanceToKeyboard, handInnerAlphaThreshold_, handOuterAlphaThreshold_),
				RightHandIntensity = ComputeOpacity(handDistanceToKeyboard2, handInnerAlphaThreshold_, handOuterAlphaThreshold_)
			};
			OVRPlugin.SetInsightPassthroughKeyboardHandsIntensity(KeyboardTracker.PassthroughOverlay.layerId, intensity);
		}
	}

	private bool ShouldEnablePassthrough(float distance)
	{
		return distance <= maximumPassthroughHandsDistance_;
	}

	private bool ShouldEnableModel(float distance)
	{
		return distance >= minimumModelHandsDistance_;
	}

	private float GetHandDistanceToKeyboard(OVRSkeleton handSkeleton)
	{
		Vector3 position = handSkeleton.Bones[8].Transform.position;
		Vector3 position2 = handSkeleton.Bones[9].Transform.position;
		Vector3 position3 = handSkeleton.Bones[18].Transform.position;
		return Mathf.Min(KeyboardTracker.GetDistanceToKeyboard(position), KeyboardTracker.GetDistanceToKeyboard(position2), KeyboardTracker.GetDistanceToKeyboard(position3));
	}

	private float ComputeOpacity(float distance, float innerThreshold, float outerThreshold)
	{
		return Mathf.Clamp((outerThreshold - distance) / (outerThreshold - innerThreshold), 0f, 1f);
	}

	private void SetHandModelsEnabled(bool enableLeftModel, bool enableRightModel)
	{
		leftHandMeshRenderer_.enabled = enableLeftModel;
		rightHandMeshRenderer_.enabled = enableRightModel;
		leftHandSkinnedMeshRenderer_.enabled = enableLeftModel;
		rightHandSkinnedMeshRenderer_.enabled = enableRightModel;
		if (leftHandSkeletonRenderer_ != null)
		{
			if (leftHandSkeletonRendererGO_ == null)
			{
				leftHandSkeletonRendererGO_ = leftHandSkeletonRenderer_.gameObject.transform.Find("SkeletonRenderer")?.gameObject;
				rightHandSkeletonRendererGO_ = rightHandSkeletonRenderer_.gameObject.transform.Find("SkeletonRenderer")?.gameObject;
			}
			if (leftHandSkeletonRendererGO_ != null)
			{
				leftHandSkeletonRendererGO_.SetActive(enableLeftModel);
			}
			if (rightHandSkeletonRendererGO_ != null)
			{
				rightHandSkeletonRendererGO_.SetActive(enableRightModel);
			}
		}
	}

	private void RetargetHandTrackingToHandPresence()
	{
		for (int i = 0; i < boneMappings_.Length; i++)
		{
			HandBoneMapping handBoneMapping = boneMappings_[i];
			string text = OVRSkeleton.BoneLabelFromBoneId(leftHandSkeleton_.GetSkeletonType(), handBoneMapping.BoneName);
			string text2 = OVRSkeleton.BoneLabelFromBoneId(rightHandSkeleton_.GetSkeletonType(), handBoneMapping.BoneName);
			boneMappings_[i].LeftHandTransform = leftHand_.transform.FindChildRecursive(text);
			boneMappings_[i].LeftPresenceTransform = leftHandRoot_.FindChildRecursive(handBoneMapping.HandPresenceLeftBoneName);
			boneMappings_[i].RightHandTransform = rightHand_.transform.FindChildRecursive(text2);
			boneMappings_[i].RightPresenceTransform = rightHandRoot_.FindChildRecursive(handBoneMapping.HandPresenceRightBoneName);
		}
		handPresenceInitialized_ = true;
	}

	private void StopHandPresence()
	{
		base.enabled = false;
		SetHandModelsEnabled(enableLeftModel: true, enableRightModel: true);
		DisableHandObjects();
	}

	private void DisableHandObjects()
	{
		KeyboardTracker.HandsOverKeyboard = false;
		RightHandOverKeyboard = false;
		LeftHandOverKeyboard = false;
		if (leftHandRoot_ != null)
		{
			leftHandRoot_.gameObject.SetActive(value: false);
		}
		if (rightHandRoot_ != null)
		{
			rightHandRoot_.gameObject.SetActive(value: false);
		}
	}

	public void TrackedKeyboardActiveUpdated(OVRTrackedKeyboard.TrackedKeyboardSetActiveEvent e)
	{
		if (!e.IsEnabled)
		{
			StopHandPresence();
		}
	}

	public void TrackedKeyboardVisibilityChanged(OVRTrackedKeyboard.TrackedKeyboardVisibilityChangedEvent e)
	{
		switch (e.State)
		{
		case OVRTrackedKeyboard.TrackedKeyboardState.NoTrackableKeyboard:
		case OVRTrackedKeyboard.TrackedKeyboardState.Offline:
		case OVRTrackedKeyboard.TrackedKeyboardState.StartedNotTracked:
			StopHandPresence();
			break;
		case OVRTrackedKeyboard.TrackedKeyboardState.Valid:
			base.enabled = handPresenceInitialized_;
			break;
		case OVRTrackedKeyboard.TrackedKeyboardState.Stale:
			if (e.TrackingTimeout)
			{
				StopHandPresence();
			}
			break;
		case OVRTrackedKeyboard.TrackedKeyboardState.Uninitialized:
		case OVRTrackedKeyboard.TrackedKeyboardState.Error:
		case OVRTrackedKeyboard.TrackedKeyboardState.ErrorExtensionFailed:
			StopHandPresence();
			Debug.LogWarning("Invalid state passed into TrackedKeyboardVisibilityChanged " + e.State);
			break;
		default:
			throw new Exception($"[tracked_keyboard] - unhandled state: TrackedKeyboardVisibilityChanged {e.State}");
		}
	}
}
