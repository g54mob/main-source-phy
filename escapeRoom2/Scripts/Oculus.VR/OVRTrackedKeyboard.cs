using System;
using System.Collections;
using Meta.XR.Util;
using UnityEngine;
using UnityEngine.Serialization;

[Feature(Feature.TrackedKeyboard)]
public class OVRTrackedKeyboard : MonoBehaviour
{
	public enum TrackedKeyboardState
	{
		Uninitialized = 0,
		NoTrackableKeyboard = 1,
		Offline = 2,
		StartedNotTracked = 3,
		Stale = 4,
		Valid = 5,
		Error = 6,
		ErrorExtensionFailed = 7
	}

	public enum KeyboardPresentation
	{
		PreferOpaque = 0,
		PreferMR = 1
	}

	public struct TrackedKeyboardVisibilityChangedEvent
	{
		public readonly string ActiveKeyboardName;

		public readonly TrackedKeyboardState State;

		public readonly bool TrackingTimeout;

		public TrackedKeyboardVisibilityChangedEvent(string keyboardModel, TrackedKeyboardState state, bool timeout)
		{
			ActiveKeyboardName = keyboardModel;
			State = state;
			TrackingTimeout = timeout;
		}
	}

	public struct TrackedKeyboardSetActiveEvent
	{
		public readonly bool IsEnabled;

		public TrackedKeyboardSetActiveEvent(bool isEnabled)
		{
			IsEnabled = isEnabled;
		}
	}

	private static readonly float underlayScaleMultX_ = 1.475f;

	private static readonly float underlayScaleConstY_ = 0.001f;

	private static readonly float underlayScaleMultZ_ = 2.138f;

	private static readonly Vector3 underlayOffset_ = new Vector3
	{
		x = 0f,
		y = 0f,
		z = -0.028f
	};

	private static readonly float boundingBoxAboveKeyboardY_ = 0.08f;

	private static readonly float initialHorizontalDistanceKeyboard_ = 0.3f;

	private static readonly float initialVerticalDistanceKeyboard_ = 0.45f;

	[Header("Settings")]
	[SerializeField]
	[Tooltip("If true, will continually try to track and show keyboard. If false, no keyboard will be shown.")]
	private bool trackingEnabled = true;

	[SerializeField]
	[Tooltip("If true, system keyboard must be paired and connected to track.")]
	private bool connectionRequired = true;

	[SerializeField]
	[Tooltip("If true, keyboard will be displayed even if it is not currently connected or visible.")]
	private bool showUntracked;

	[SerializeField]
	[Tooltip("Which type of keyboard you wish to use.")]
	private OVRPlugin.TrackedKeyboardQueryFlags keyboardQueryFlags = OVRPlugin.TrackedKeyboardQueryFlags.Local;

	[SerializeField]
	[Tooltip("Opaque will render a solid model of the keyboard with passthrough hands. MR will render the entire keyboard and hands in a passthrough window cutout. These are both suggestions and may not always be available.")]
	private KeyboardPresentation presentation;

	[SerializeField]
	[Tooltip("Changes the Texture Quality setting of the currently used texture. Affects visualization quality only A value of -1 means no filtering. Bilinear is 0 (Unity Default) up to Aniso 16x which is 9.")]
	public OVRTextureQualityFiltering textureFiltering = OVRTextureQualityFiltering.Aniso2x;

	[SerializeField]
	[Tooltip("Changes the MipMap Bias of the currently used texture. Affects visualization quality only.")]
	[Range(-1f, 1f)]
	public float mipmapBias = -0.3f;

	[Tooltip("How large of a passthrough area to show surrounding the keyboard when using MR presentation")]
	public float PassthroughBorderMultiplier = 0.1f;

	[Tooltip("The shader used for rendering the keyboard model")]
	public Shader keyboardModelShader;

	[Tooltip("The shader used for rendering transparent parts of the keyboard model")]
	public Shader keyboardModelAlphaBlendShader;

	private OVRPlugin.TrackedKeyboardPresentationStyles currentKeyboardPresentationStyles;

	private OVROverlay projectedPassthroughOpaque_;

	private MeshRenderer[] activeKeyboardRenderers_;

	private GameObject activeKeyboardMesh_;

	private MeshRenderer activeKeyboardMeshRenderer_;

	private GameObject passthroughQuad_;

	private Texture2D dynamicQualityTexture_;

	private Vector3 untrackedPosition_;

	[Header("Internal")]
	public Shader PassthroughShader;

	[SerializeField]
	private Transform projectedPassthroughRoot;

	[SerializeField]
	private MeshFilter projectedPassthroughMesh;

	[FormerlySerializedAs("ProjectedPassthroughKeyLabel")]
	public OVRPassthroughLayer ProjectedPassthroughMR;

	public Action<TrackedKeyboardSetActiveEvent> TrackedKeyboardActiveChanged = delegate
	{
	};

	public Action<TrackedKeyboardVisibilityChangedEvent> TrackedKeyboardVisibilityChanged = delegate
	{
	};

	public Transform ActiveKeyboardTransform;

	[HideInInspector]
	public bool HandsOverKeyboard;

	private OVRCameraRig cameraRig_;

	private Coroutine updateKeyboardRoutine_;

	private BoxCollider keyboardBoundingBox_;

	private float staleTimeoutCounter_;

	private const float STALE_TIMEOUT = 10f;

	private float reacquisitionTimer_;

	private float sendFilteredPoseEventTimer_;

	private int skippedPoseCount_;

	private const float FILTERED_POSE_TIMEOUT = 15f;

	private Vector3? EWAPosition;

	private Quaternion? EWARotation;

	private float HAND_HEIGHT_TUNING;

	[HideInInspector]
	public bool UseHeuristicRollback;

	public float CurrentKeyboardAngleFromUp { get; private set; }

	public TrackedKeyboardState TrackingState { get; private set; }

	public OVRKeyboard.TrackedKeyboardInfo ActiveKeyboardInfo { get; private set; }

	public OVRKeyboard.TrackedKeyboardInfo SystemKeyboardInfo { get; private set; }

	public KeyboardPresentation Presentation
	{
		get
		{
			return presentation;
		}
		set
		{
			presentation = value;
			UpdatePresentation(GetKeyboardVisibility());
		}
	}

	public bool TrackingEnabled
	{
		get
		{
			return trackingEnabled;
		}
		set
		{
			trackingEnabled = value;
		}
	}

	public bool ConnectionRequired
	{
		get
		{
			return connectionRequired;
		}
		set
		{
			connectionRequired = value;
		}
	}

	public bool ShowUntracked
	{
		get
		{
			return showUntracked;
		}
		set
		{
			showUntracked = value;
		}
	}

	public bool RemoteKeyboard
	{
		get
		{
			if (KeyboardQueryFlags == OVRPlugin.TrackedKeyboardQueryFlags.Local)
			{
				return false;
			}
			return true;
		}
		set
		{
			if (value)
			{
				KeyboardQueryFlags = OVRPlugin.TrackedKeyboardQueryFlags.Remote;
			}
			else
			{
				KeyboardQueryFlags = OVRPlugin.TrackedKeyboardQueryFlags.Local;
			}
		}
	}

	public OVRPlugin.TrackedKeyboardQueryFlags KeyboardQueryFlags
	{
		get
		{
			return keyboardQueryFlags;
		}
		set
		{
			keyboardQueryFlags = value;
		}
	}

	public OVROverlay PassthroughOverlay
	{
		get
		{
			return projectedPassthroughOpaque_;
		}
		private set
		{
		}
	}

	private IEnumerator Start()
	{
		cameraRig_ = UnityEngine.Object.FindAnyObjectByType<OVRCameraRig>();
		SystemKeyboardInfo = new OVRKeyboard.TrackedKeyboardInfo
		{
			Name = "None",
			Dimensions = new Vector3(0f, 0f, 0f),
			Identifier = 4294967295uL
		};
		yield return InitializeHandPresenceData();
		yield return UpdateTrackingStateCoroutine();
	}

	private IEnumerator InitializeHandPresenceData()
	{
		GameObject gameObject = GameObject.Find("OVRCameraRig");
		if (gameObject == null)
		{
			Debug.LogError("Scene does not contain an OVRCameraRig");
			yield break;
		}
		projectedPassthroughOpaque_ = gameObject.AddComponent<OVROverlay>();
		projectedPassthroughOpaque_.currentOverlayShape = OVROverlay.OverlayShape.KeyboardHandsPassthrough;
		projectedPassthroughOpaque_.hidden = true;
		projectedPassthroughOpaque_.gameObject.SetActive(value: true);
		ProjectedPassthroughMR.hidden = true;
		ProjectedPassthroughMR.gameObject.SetActive(value: true);
	}

	private void RegisterPassthroughMeshToSDK()
	{
		if (ProjectedPassthroughMR.IsSurfaceGeometry(projectedPassthroughMesh.gameObject))
		{
			ProjectedPassthroughMR.RemoveSurfaceGeometry(projectedPassthroughMesh.gameObject);
		}
		ProjectedPassthroughMR.AddSurfaceGeometry(projectedPassthroughMesh.gameObject, updateTransform: true);
	}

	public float GetDistanceToKeyboard(Vector3 point)
	{
		if (keyboardBoundingBox_ == null)
		{
			return float.PositiveInfinity;
		}
		if (keyboardBoundingBox_.bounds.Contains(point))
		{
			return 0f;
		}
		Vector3 direction = keyboardBoundingBox_.ClosestPointOnBounds(point) - point;
		if (!keyboardBoundingBox_.Raycast(new Ray(point, direction), out var hitInfo, float.PositiveInfinity))
		{
			return float.PositiveInfinity;
		}
		return hitInfo.distance;
	}

	public void LaunchLocalKeyboardSelectionDialog()
	{
		LaunchOverlayIntent("systemux://dialog/set-local-physical-tracked-keyboard");
	}

	public void LaunchRemoteKeyboardSelectionDialog()
	{
		LaunchOverlayIntent("systemux://dialog/set-remote-physical-tracked-keyboard");
	}

	private bool KeyboardTrackerIsRunning()
	{
		if (TrackingState != TrackedKeyboardState.NoTrackableKeyboard)
		{
			return TrackingState != TrackedKeyboardState.Offline;
		}
		return false;
	}

	private IEnumerator UpdateTrackingStateCoroutine()
	{
		while (true)
		{
			if (OVRPlugin.initialized)
			{
				if (OVRKeyboard.GetSystemKeyboardInfo(KeyboardQueryFlags, out var keyboardInfo))
				{
					bool flag = false;
					if (SystemKeyboardInfo.Identifier != keyboardInfo.Identifier || SystemKeyboardInfo.KeyboardFlags != keyboardInfo.KeyboardFlags)
					{
						Debug.Log(string.Format("New System keyboard info: [{0}] {1} (Flags {2}) ({3} {4})", keyboardInfo.Identifier, keyboardInfo.Name, keyboardInfo.KeyboardFlags, ((keyboardInfo.SupportedPresentationStyles & OVRPlugin.TrackedKeyboardPresentationStyles.Opaque) != OVRPlugin.TrackedKeyboardPresentationStyles.Unknown) ? "Supports Opaque" : "", ((keyboardInfo.SupportedPresentationStyles & OVRPlugin.TrackedKeyboardPresentationStyles.MR) != OVRPlugin.TrackedKeyboardPresentationStyles.Unknown) ? "Supports MR" : ""));
						if (TrackingState == TrackedKeyboardState.NoTrackableKeyboard)
						{
							SetKeyboardState(TrackedKeyboardState.Offline);
						}
						SystemKeyboardInfo = keyboardInfo;
						flag = true;
					}
					bool flag2 = (keyboardInfo.KeyboardFlags & OVRPlugin.TrackedKeyboardFlags.Exists) != 0;
					if ((flag2 && trackingEnabled) || showUntracked)
					{
						bool flag3 = (keyboardInfo.KeyboardFlags & OVRPlugin.TrackedKeyboardFlags.Local) != 0;
						bool num = (keyboardInfo.KeyboardFlags & OVRPlugin.TrackedKeyboardFlags.Remote) != 0;
						bool flag4 = (keyboardInfo.KeyboardFlags & OVRPlugin.TrackedKeyboardFlags.Connected) != 0;
						bool flag5 = num || (flag3 && (!connectionRequired || flag4)) || showUntracked;
						if (KeyboardTrackerIsRunning() && (flag || !flag5))
						{
							StopKeyboardTrackingInternal();
						}
						if (!KeyboardTrackerIsRunning() && flag5)
						{
							yield return StartKeyboardTrackingCoroutine();
						}
					}
					else
					{
						if (KeyboardTrackerIsRunning())
						{
							StopKeyboardTrackingInternal();
						}
						if (!flag2)
						{
							SetKeyboardState(TrackedKeyboardState.NoTrackableKeyboard);
						}
					}
				}
				else
				{
					if (KeyboardTrackerIsRunning())
					{
						StopKeyboardTrackingInternal();
					}
					SetKeyboardState(TrackedKeyboardState.ErrorExtensionFailed);
				}
				SystemKeyboardInfo = keyboardInfo;
				keyboardInfo = default(OVRKeyboard.TrackedKeyboardInfo);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator StartKeyboardTrackingCoroutine()
	{
		if (KeyboardTrackerIsRunning())
		{
			Debug.Log("StartKeyboardTracking(): Keyboard already being tracked");
			yield break;
		}
		InitializeKeyboardInfo();
		RegisterPassthroughMeshToSDK();
		Debug.Log("Calling StartKeyboardTracking with id " + SystemKeyboardInfo.Identifier);
		if (!OVRPlugin.StartKeyboardTracking(SystemKeyboardInfo.Identifier) && !showUntracked)
		{
			Debug.LogWarning("OVRKeyboard.StartKeyboardTracking Failed");
			SetKeyboardState(TrackedKeyboardState.Error);
			yield break;
		}
		projectedPassthroughRoot.localScale = new Vector3
		{
			x = SystemKeyboardInfo.Dimensions.x * underlayScaleMultX_,
			y = underlayScaleConstY_,
			z = SystemKeyboardInfo.Dimensions.z * underlayScaleMultZ_
		};
		currentKeyboardPresentationStyles = SystemKeyboardInfo.SupportedPresentationStyles;
		ActiveKeyboardInfo = SystemKeyboardInfo;
		LoadKeyboardMesh();
		updateKeyboardRoutine_ = StartCoroutine(UpdateKeyboardPose());
		EWAPosition = null;
		EWARotation = null;
		TrackedKeyboardActiveChanged?.Invoke(new TrackedKeyboardSetActiveEvent(isEnabled: true));
		SetKeyboardState(TrackedKeyboardState.StartedNotTracked);
	}

	private void StopKeyboardTrackingInternal()
	{
		if (!KeyboardTrackerIsRunning() || updateKeyboardRoutine_ == null)
		{
			SetKeyboardState(TrackedKeyboardState.Offline);
			return;
		}
		projectedPassthroughOpaque_.hidden = true;
		ProjectedPassthroughMR.hidden = true;
		TrackedKeyboardActiveChanged?.Invoke(new TrackedKeyboardSetActiveEvent(isEnabled: false));
		Debug.Log("StopKeyboardTracking " + ActiveKeyboardInfo.Name);
		StopCoroutine(updateKeyboardRoutine_);
		updateKeyboardRoutine_ = null;
		OVRKeyboard.StopKeyboardTracking(ActiveKeyboardInfo);
		InitializeKeyboardInfo();
		if (activeKeyboardMesh_ != null)
		{
			UnityEngine.Object.Destroy(activeKeyboardMesh_.gameObject);
			activeKeyboardMesh_ = null;
			activeKeyboardRenderers_ = null;
			keyboardBoundingBox_ = null;
		}
		untrackedPosition_ = Vector3.zero;
		SetKeyboardState(TrackedKeyboardState.Offline);
	}

	private IEnumerator UpdateKeyboardPose()
	{
		while (true)
		{
			base.transform.position = cameraRig_.trackingSpace.transform.position;
			base.transform.rotation = cameraRig_.trackingSpace.transform.rotation;
			OVRKeyboard.TrackedKeyboardState keyboardState = OVRKeyboard.GetKeyboardState();
			if ((!keyboardState.isPositionValid || !keyboardState.isPositionTracked) && showUntracked)
			{
				keyboardState.isPositionValid = true;
				keyboardState.isPositionTracked = true;
				if (untrackedPosition_ == Vector3.zero && Camera.main != null)
				{
					Transform transform = Camera.main.transform;
					Vector3 normalized = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
					untrackedPosition_ = transform.position + normalized * initialHorizontalDistanceKeyboard_ + new Vector3(0f, 0f - initialVerticalDistanceKeyboard_, 0f);
				}
				keyboardState.position = untrackedPosition_;
			}
			TrackedKeyboardState keyboardState2 = TrackedKeyboardState.StartedNotTracked;
			if (keyboardState.isPositionValid)
			{
				if (keyboardState.isPositionTracked && activeKeyboardMesh_ != null)
				{
					float num = (UseHeuristicRollback ? 360f : 20f);
					float num2 = (UseHeuristicRollback ? 0f : 0.65f);
					Vector3 vector = base.transform.rotation * keyboardState.rotation * Vector3.up;
					CurrentKeyboardAngleFromUp = Vector3.Angle(vector, Vector3.up);
					if (CurrentKeyboardAngleFromUp < num)
					{
						if (!EWAPosition.HasValue)
						{
							EWAPosition = keyboardState.position;
						}
						else
						{
							OVRTrackedKeyboard oVRTrackedKeyboard = this;
							float value = num2;
							Vector3? eWAPosition = EWAPosition;
							oVRTrackedKeyboard.EWAPosition = value * eWAPosition + (1f - num2) * keyboardState.position;
						}
						if (!EWARotation.HasValue)
						{
							EWARotation = keyboardState.rotation;
						}
						else
						{
							EWARotation = Quaternion.Slerp(EWARotation.Value, keyboardState.rotation, 1f - num2);
						}
						ActiveKeyboardTransform.localPosition = EWAPosition.Value;
						ActiveKeyboardTransform.localRotation = EWARotation.Value;
						projectedPassthroughRoot.localPosition = EWAPosition.Value + underlayOffset_ + new Vector3(0f, HAND_HEIGHT_TUNING, 0f);
						projectedPassthroughRoot.localRotation = EWARotation.Value;
					}
					else
					{
						skippedPoseCount_++;
					}
				}
				keyboardState2 = (keyboardState.isPositionTracked ? TrackedKeyboardState.Valid : TrackedKeyboardState.Stale);
			}
			SetKeyboardState(keyboardState2);
			UpdateSkippedPoseTimer();
			yield return null;
		}
	}

	private void UpdateSkippedPoseTimer()
	{
		sendFilteredPoseEventTimer_ += Time.deltaTime;
		if (sendFilteredPoseEventTimer_ > 15f && skippedPoseCount_ > 0)
		{
			skippedPoseCount_ = 0;
			sendFilteredPoseEventTimer_ = 0f;
		}
	}

	private void LoadKeyboardMesh()
	{
		Debug.Log("LoadKeyboardMesh");
		activeKeyboardMesh_ = LoadRuntimeKeyboardMesh();
		if (activeKeyboardMesh_ == null)
		{
			Debug.LogError("Failed to load keyboard mesh.");
			SetKeyboardState(TrackedKeyboardState.Error);
			return;
		}
		keyboardBoundingBox_ = activeKeyboardMesh_.AddComponent<BoxCollider>();
		keyboardBoundingBox_.center = new Vector3(0f, ActiveKeyboardInfo.Dimensions.y / 2f, 0f);
		keyboardBoundingBox_.size = new Vector3(ActiveKeyboardInfo.Dimensions.x, ActiveKeyboardInfo.Dimensions.y + boundingBoxAboveKeyboardY_, ActiveKeyboardInfo.Dimensions.z);
		activeKeyboardMeshRenderer_ = activeKeyboardMesh_.GetComponentInChildren<MeshRenderer>();
		if (activeKeyboardMeshRenderer_ == null)
		{
			Debug.LogError("Failed to load activeKeyboardMeshRenderer_.");
			SetKeyboardState(TrackedKeyboardState.Error);
			return;
		}
		passthroughQuad_ = GameObject.CreatePrimitive(PrimitiveType.Quad);
		passthroughQuad_.transform.localPosition = new Vector3(0f, -0.01f, 0f);
		passthroughQuad_.transform.parent = ActiveKeyboardTransform;
		passthroughQuad_.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		float num = ActiveKeyboardInfo.Dimensions.x * PassthroughBorderMultiplier;
		passthroughQuad_.transform.localScale = new Vector3(ActiveKeyboardInfo.Dimensions.x + num, ActiveKeyboardInfo.Dimensions.z + num, ActiveKeyboardInfo.Dimensions.y);
		passthroughQuad_.GetComponent<MeshRenderer>().material.shader = PassthroughShader;
		GameObject gameObject = new GameObject();
		activeKeyboardMesh_.transform.parent = gameObject.transform;
		activeKeyboardMesh_ = gameObject;
		activeKeyboardRenderers_ = activeKeyboardMesh_.GetComponentsInChildren<MeshRenderer>();
		activeKeyboardMesh_.transform.SetParent(ActiveKeyboardTransform, worldPositionStays: false);
		ActiveKeyboardTransform.localRotation = Quaternion.identity;
		Texture mainTexture = activeKeyboardMeshRenderer_.material.mainTexture;
		if (mainTexture != null)
		{
			dynamicQualityTexture_ = Texture2D.CreateExternalTexture(mainTexture.width, mainTexture.height, TextureFormat.BC7, mipChain: true, linear: true, mainTexture.GetNativeTexturePtr());
		}
		UpdateTextureQuality();
		UpdateKeyboardVisibility();
	}

	private void UpdateTextureQuality()
	{
		if (!(dynamicQualityTexture_ == null))
		{
			OVRGLTFLoader.ApplyTextureQuality(textureFiltering, ref dynamicQualityTexture_);
			Material material = activeKeyboardMeshRenderer_.material;
			material.mainTexture = dynamicQualityTexture_;
			if (material.HasProperty("_MainTexMMBias"))
			{
				material.SetFloat("_MainTexMMBias", mipmapBias);
			}
			activeKeyboardMeshRenderer_.material = material;
		}
	}

	private void UpdatePresentation(bool isVisible)
	{
		KeyboardPresentation keyboardPresentation = Presentation;
		if (currentKeyboardPresentationStyles != OVRPlugin.TrackedKeyboardPresentationStyles.Unknown)
		{
			if (Presentation == KeyboardPresentation.PreferOpaque && (currentKeyboardPresentationStyles & OVRPlugin.TrackedKeyboardPresentationStyles.Opaque) == 0)
			{
				if ((currentKeyboardPresentationStyles & OVRPlugin.TrackedKeyboardPresentationStyles.MR) != OVRPlugin.TrackedKeyboardPresentationStyles.Unknown)
				{
					keyboardPresentation = KeyboardPresentation.PreferMR;
				}
			}
			else if (Presentation == KeyboardPresentation.PreferMR && (currentKeyboardPresentationStyles & OVRPlugin.TrackedKeyboardPresentationStyles.MR) == 0 && (currentKeyboardPresentationStyles & OVRPlugin.TrackedKeyboardPresentationStyles.Opaque) != OVRPlugin.TrackedKeyboardPresentationStyles.Unknown)
			{
				keyboardPresentation = KeyboardPresentation.PreferOpaque;
			}
		}
		if (!isVisible)
		{
			projectedPassthroughOpaque_.hidden = true;
			ProjectedPassthroughMR.hidden = true;
		}
		else if (keyboardPresentation == KeyboardPresentation.PreferOpaque)
		{
			passthroughQuad_.SetActive(value: false);
			projectedPassthroughOpaque_.hidden = !GetKeyboardVisibility() || !HandsOverKeyboard;
			ProjectedPassthroughMR.hidden = true;
			activeKeyboardMesh_.SetActive(value: true);
		}
		else
		{
			passthroughQuad_.SetActive(value: true);
			projectedPassthroughOpaque_.hidden = true;
			ProjectedPassthroughMR.hidden = false;
			activeKeyboardMesh_.SetActive(value: false);
		}
	}

	private GameObject LoadRuntimeKeyboardMesh()
	{
		Debug.Log("LoadRuntimekeyboardMesh");
		string[] renderModelPaths = OVRPlugin.GetRenderModelPaths();
		if (renderModelPaths != null)
		{
			for (int i = 0; i < renderModelPaths.Length; i++)
			{
				if ((!RemoteKeyboard || !renderModelPaths[i].Equals("/model_fb/keyboard/remote")) && (RemoteKeyboard || !renderModelPaths[i].Equals("/model_fb/keyboard/local")))
				{
					continue;
				}
				OVRPlugin.RenderModelProperties modelProperties = default(OVRPlugin.RenderModelProperties);
				if (OVRPlugin.GetRenderModelProperties(renderModelPaths[i], ref modelProperties) && modelProperties.ModelKey != 0L)
				{
					byte[] array = OVRPlugin.LoadRenderModel(modelProperties.ModelKey);
					if (array != null)
					{
						OVRGLTFLoader oVRGLTFLoader = new OVRGLTFLoader(array);
						oVRGLTFLoader.SetModelShader(keyboardModelShader);
						oVRGLTFLoader.SetModelAlphaBlendShader(keyboardModelAlphaBlendShader);
						return oVRGLTFLoader.LoadGLB(supportAnimation: false).root;
					}
				}
				Debug.LogError("Failed to load model. Ensure that the correct keyboard is connected.");
				break;
			}
		}
		Debug.LogError("Failed to find keyboard model.");
		return null;
	}

	public void UpdateKeyboardVisibility()
	{
		if (activeKeyboardMesh_ == null)
		{
			return;
		}
		bool keyboardVisibility = GetKeyboardVisibility();
		UpdatePresentation(keyboardVisibility);
		if (activeKeyboardRenderers_ != null)
		{
			MeshRenderer[] array = activeKeyboardRenderers_;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = keyboardVisibility;
			}
		}
	}

	private void SetKeyboardState(TrackedKeyboardState state)
	{
		TrackedKeyboardState trackingState = TrackingState;
		TrackingState = state;
		bool flag = false;
		switch (state)
		{
		case TrackedKeyboardState.Stale:
			if (!HandsOverKeyboard)
			{
				staleTimeoutCounter_ += Time.deltaTime;
				flag = staleTimeoutCounter_ - 10f > 0f;
				if (flag)
				{
					reacquisitionTimer_ += Time.deltaTime;
					EWAPosition = null;
					EWARotation = null;
				}
			}
			else
			{
				reacquisitionTimer_ = 0f;
				staleTimeoutCounter_ = 0f;
			}
			break;
		case TrackedKeyboardState.Valid:
			staleTimeoutCounter_ = 0f;
			if (trackingState == TrackedKeyboardState.Stale && !(reacquisitionTimer_ > 0f))
			{
			}
			break;
		case TrackedKeyboardState.NoTrackableKeyboard:
		case TrackedKeyboardState.Offline:
		case TrackedKeyboardState.StartedNotTracked:
			reacquisitionTimer_ = 0f;
			staleTimeoutCounter_ = 0f;
			break;
		}
		if (trackingState != state || flag)
		{
			DispatchVisibilityEvent(flag);
		}
		UpdateKeyboardVisibility();
	}

	private bool GetKeyboardVisibility()
	{
		switch (TrackingState)
		{
		case TrackedKeyboardState.Stale:
			if (!HandsOverKeyboard)
			{
				return !(staleTimeoutCounter_ - 10f > 0f);
			}
			return true;
		case TrackedKeyboardState.Valid:
			return true;
		default:
			if (showUntracked)
			{
				return true;
			}
			return false;
		}
	}

	private void InitializeKeyboardInfo()
	{
		ActiveKeyboardInfo = new OVRKeyboard.TrackedKeyboardInfo
		{
			Name = "None",
			Dimensions = new Vector3(0f, 0f, 0f),
			Identifier = 4294967295uL
		};
	}

	private void LaunchOverlayIntent(string dataUri)
	{
		AndroidJavaObject androidJavaObject = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("android.content.Intent");
		androidJavaObject2.Call<AndroidJavaObject>("setPackage", new object[1] { "com.oculus.vrshell" });
		androidJavaObject2.Call<AndroidJavaObject>("setAction", new object[1] { "com.oculus.vrshell.intent.action.LAUNCH" });
		androidJavaObject2.Call<AndroidJavaObject>("putExtra", new object[2] { "intent_data", dataUri });
		androidJavaObject.Call("sendBroadcast", androidJavaObject2);
	}

	public void Dispose()
	{
		if (KeyboardTrackerIsRunning())
		{
			StopKeyboardTrackingInternal();
		}
		if (ProjectedPassthroughMR.IsSurfaceGeometry(projectedPassthroughMesh.gameObject))
		{
			ProjectedPassthroughMR.RemoveSurfaceGeometry(projectedPassthroughMesh.gameObject);
		}
		if (activeKeyboardMesh_ != null)
		{
			UnityEngine.Object.Destroy(activeKeyboardMesh_.gameObject);
		}
	}

	private void DispatchVisibilityEvent(bool timeOut)
	{
		TrackedKeyboardVisibilityChanged?.Invoke(new TrackedKeyboardVisibilityChangedEvent(ActiveKeyboardInfo.Name, TrackingState, timeOut));
	}
}
