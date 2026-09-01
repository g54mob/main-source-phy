using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Cameras : MonoBehaviour
{
	[Header("Main Camera")]
	public Camera m_Main;

	[Header("Child Cameras")]
	public Camera m_Foreground;

	public Camera m_RenderLast;

	public Camera m_Outlines;

	public Camera m_BuildZones;

	public Camera m_Decor;

	[Header("Specialized Cameras")]
	public Camera m_Replay;

	public Camera m_BridgePreview;

	public Camera m_RenderOverAll;

	public Camera m_SplashCamera;

	[Header("Sky")]
	public MeshRenderer m_SandboxSky;

	public MeshRenderer m_BuildModeSky;

	[Header("Misc")]
	public RawImage m_RenderOverAllImage;

	public CameraRotate m_CameraRotate;

	[NonSerialized]
	public GradientSky m_GradientSky;

	public static Cameras m_Instance;

	public static AsyncCapture m_AsyncCapture;

	private bool m_StressViewOnBeforeReplay;

	private void Awake()
	{
		if (!m_Instance)
		{
			m_Instance = this;
			m_AsyncCapture = m_Replay.GetComponent<AsyncCapture>();
			RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
			m_SandboxSky.gameObject.SetActive(value: false);
			m_BuildModeSky.gameObject.SetActive(value: false);
			m_Decor.gameObject.SetActive(value: false);
			m_SplashCamera.gameObject.SetActive(value: false);
			m_RenderOverAll.enabled = false;
		}
	}

	private void OnDestroy()
	{
		RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
		RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
	}

	private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
	{
		if (camera == m_Replay)
		{
			m_StressViewOnBeforeReplay = Profiles.m_ActiveProfile.m_StressViewEnabled;
			Profiles.m_ActiveProfile.m_StressViewEnabled = false;
			BridgeEdges.UpdateStressColor();
		}
	}

	private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
	{
		if (camera == m_Replay)
		{
			m_AsyncCapture.DoPostRender();
			Profiles.m_ActiveProfile.m_StressViewEnabled = m_StressViewOnBeforeReplay;
			BridgeEdges.UpdateStressColor();
		}
	}

	private void Update()
	{
		CameraInterpolate.UpdateManual();
		if (GameStateManager.GetState() == GameState.SIM)
		{
			Quaternion identity = Quaternion.identity;
			identity.eulerAngles = new Vector3(Mathf.Max(0.01f, MainCamera().transform.rotation.eulerAngles.x), MainCamera().transform.rotation.eulerAngles.y, MainCamera().transform.rotation.eulerAngles.z);
			MainCamera().transform.localRotation = identity;
		}
	}

	public static void Init()
	{
		float farClipPlane = 1000f;
		m_Instance.m_Main.farClipPlane = farClipPlane;
		m_Instance.m_Foreground.farClipPlane = farClipPlane;
		m_Instance.m_Outlines.farClipPlane = farClipPlane;
		m_Instance.m_BuildZones.farClipPlane = farClipPlane;
		m_Instance.m_RenderLast.farClipPlane = farClipPlane;
		m_Instance.m_Replay.farClipPlane = farClipPlane;
		m_Instance.m_Decor.farClipPlane = farClipPlane;
		m_Instance.m_RenderOverAll.farClipPlane = farClipPlane;
		m_Instance.m_SplashCamera.farClipPlane = farClipPlane;
		m_Instance.m_Foreground.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_Outlines.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_BuildZones.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_RenderLast.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_Decor.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_SplashCamera.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_Replay.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_BridgePreview.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		m_Instance.m_RenderOverAll.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(URP.FORWARD_RENDERER);
		DisableReplayCamera();
		m_Instance.m_Decor.gameObject.SetActive(value: false);
	}

	public static void SetForwardRenderer(int index)
	{
		m_Instance.m_Main.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(index);
		m_Instance.m_Replay.transform.GetComponent<UniversalAdditionalCameraData>().SetRenderer(index);
	}

	public static Camera MainCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_Main;
	}

	public static Camera ForegroundCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_Foreground;
	}

	public static Camera OutlinesCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_Outlines;
	}

	public static Camera BuildZoneCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_BuildZones;
	}

	public static Camera DecorCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_Decor;
	}

	public static Camera RenderLastCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_RenderLast;
	}

	public static Camera ReplayCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_Replay;
	}

	public static Camera BridgePreviewCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_BridgePreview;
	}

	public static Camera SplashCamera()
	{
		if (!m_Instance)
		{
			return null;
		}
		return m_Instance.m_SplashCamera;
	}

	public static void SetOrthographicSize(float orthographicSize)
	{
		m_Instance.m_Main.orthographicSize = orthographicSize;
		m_Instance.m_Foreground.orthographicSize = orthographicSize;
		m_Instance.m_Outlines.orthographicSize = orthographicSize;
		m_Instance.m_BuildZones.orthographicSize = orthographicSize;
		m_Instance.m_RenderLast.orthographicSize = orthographicSize;
		m_Instance.m_Replay.orthographicSize = orthographicSize;
		m_Instance.m_Decor.orthographicSize = orthographicSize;
		m_Instance.m_RenderOverAll.orthographicSize = orthographicSize;
	}

	public static void SetFOV(float fov)
	{
		m_Instance.m_Main.fieldOfView = fov;
		m_Instance.m_Foreground.fieldOfView = fov;
		m_Instance.m_Outlines.fieldOfView = fov;
		m_Instance.m_BuildZones.fieldOfView = fov;
		m_Instance.m_RenderLast.fieldOfView = fov;
		m_Instance.m_Replay.fieldOfView = fov;
		m_Instance.m_Decor.fieldOfView = fov;
		m_Instance.m_RenderOverAll.fieldOfView = fov;
	}

	public static float GetOrthographicSize()
	{
		return m_Instance.m_Main.orthographicSize;
	}

	public static float GetMinPitch()
	{
		return m_Instance.m_CameraRotate.m_MinPitch;
	}

	public static float GetMaxPitch()
	{
		if (Game.InDecorModeTopView())
		{
			return 89.99f;
		}
		return m_Instance.m_CameraRotate.m_MaxPitch;
	}

	public static float GetPitch()
	{
		float num = MainCamera().transform.eulerAngles.x;
		if (num > 180f)
		{
			num = 360f - num;
		}
		return num;
	}

	public static void StartRecording()
	{
		if (m_AsyncCapture.m_Initialized)
		{
			m_AsyncCapture.Reset();
			m_AsyncCapture.m_IsRecording = true;
		}
	}

	public static void AbortRecording()
	{
		m_AsyncCapture.Reset();
		m_AsyncCapture.m_IsRecording = false;
	}

	public static void PauseRecording()
	{
		m_AsyncCapture.m_IsRecording = false;
	}

	public static void ResumeRecording()
	{
		if (m_AsyncCapture.m_Initialized)
		{
			m_AsyncCapture.m_IsRecording = true;
		}
	}

	public static bool IsRecordingReplay()
	{
		return m_AsyncCapture.m_IsRecording;
	}

	public static void EnableReplayCamera()
	{
		m_Instance.m_Replay.gameObject.SetActive(value: true);
		m_Instance.m_Replay.transform.localPosition = Vector3.zero;
		m_Instance.m_Replay.transform.localRotation = Quaternion.identity;
	}

	public static void DisableReplayCamera()
	{
		m_Instance.m_Replay.gameObject.SetActive(value: false);
	}

	public static void EnableBuildModeSky()
	{
		if (m_Instance != null)
		{
			m_Instance.m_BuildModeSky.gameObject.SetActive(value: true);
		}
	}

	public static void DisableBuildModeSky()
	{
		if (m_Instance != null)
		{
			m_Instance.m_BuildModeSky.gameObject.SetActive(value: false);
		}
	}

	public static void EnableSky()
	{
		if (m_Instance != null && Theme.m_Instance != null)
		{
			m_Instance.m_GradientSky.gameObject.SetActive(value: true);
		}
	}

	public static void DisableSky()
	{
		if (m_Instance != null)
		{
			m_Instance.m_GradientSky.gameObject.SetActive(value: false);
		}
	}

	public static void AdjustOrthographicSizeToFrameBounds(List<Bounds> boundsList, float expandBounds)
	{
		float orthographicSize = GetOrthographicSize();
		SetOrthographicSize(Game.MinOrthographicSize());
		float num2;
		do
		{
			for (int num = boundsList.Count - 1; num >= 0; num--)
			{
				if (BoundsInView(boundsList[num], expandBounds))
				{
					boundsList.RemoveAt(num);
				}
			}
			if (boundsList.Count == 0)
			{
				break;
			}
			num2 = Mathf.Clamp(GetOrthographicSize() + 0.2f, Game.MinOrthographicSize(), Game.MaxOrthographicSize());
			SetOrthographicSize(num2);
		}
		while (!Mathf.Approximately(num2, Game.MaxOrthographicSize()));
		if (!Mathf.Approximately(orthographicSize, GetOrthographicSize()))
		{
			SetOrthographicSize(Mathf.Clamp(GetOrthographicSize(), Game.MinOrthographicSize(), Game.MaxOrthographicSize()));
		}
	}

	public static void EnableRenderOverAllWithTexture(Texture2D texture)
	{
		if (m_Instance != null)
		{
			m_Instance.m_RenderOverAllImage.GetComponent<RectTransform>().sizeDelta = new Vector2(texture.width, texture.height);
			m_Instance.m_RenderOverAllImage.texture = texture;
			m_Instance.m_RenderOverAll.enabled = true;
		}
	}

	public static void DisableRenderOverAll()
	{
		if (m_Instance != null)
		{
			m_Instance.m_RenderOverAll.enabled = false;
		}
	}

	public static void EnterDecorMode()
	{
		if (m_Instance != null)
		{
			m_Instance.m_Decor.gameObject.SetActive(value: true);
			m_Instance.m_Decor.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
			m_Instance.m_RenderLast.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
			m_Instance.m_Main.cullingMask &= ~Utils.DECOR_LAYER_MASK;
		}
	}

	public static void ExitDecorMode()
	{
		if (m_Instance != null)
		{
			m_Instance.m_Decor.gameObject.SetActive(value: false);
			m_Instance.m_Decor.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
			m_Instance.m_RenderLast.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
			m_Instance.m_Main.cullingMask |= Utils.DECOR_LAYER_MASK;
		}
	}

	public static bool InLocked2DMode()
	{
		if (!PointsOfView.m_Locked2D)
		{
			return false;
		}
		return In2DMode();
	}

	public static bool In2DMode()
	{
		if (MainCamera().transform.rotation.eulerAngles.x <= 0.01001f)
		{
			return Mathf.Abs(MainCamera().transform.rotation.eulerAngles.y) < 0.01f;
		}
		return false;
	}

	private static CameraDummy CreateCameraDummy(string name)
	{
		return new GameObject(name).AddComponent<CameraDummy>();
	}

	private static bool BoundsInView(Bounds bounds, float buffer)
	{
		if (!WorldPosVisible(bounds.center + new Vector3(0f - (bounds.extents.x + buffer), bounds.extents.y + buffer, 0f)))
		{
			return false;
		}
		if (!WorldPosVisible(bounds.center + new Vector3(bounds.extents.x + buffer, bounds.extents.y + buffer, 0f)))
		{
			return false;
		}
		if (!WorldPosVisible(bounds.center + new Vector3(bounds.extents.x + buffer, 0f - (bounds.extents.y + buffer), 0f)))
		{
			return false;
		}
		if (!WorldPosVisible(bounds.center + new Vector3(0f - (bounds.extents.x + buffer), 0f - (bounds.extents.y + buffer), 0f)))
		{
			return false;
		}
		return true;
	}

	private static bool WorldPosVisible(Vector3 pos)
	{
		return ScreenPosVisible(MainCamera().WorldToScreenPoint(pos));
	}

	private static bool ScreenPosVisible(Vector2 screenPos)
	{
		if (screenPos.x >= 0f && screenPos.x < (float)Screen.width && screenPos.y >= 0f)
		{
			return screenPos.y < (float)Screen.height;
		}
		return false;
	}
}
