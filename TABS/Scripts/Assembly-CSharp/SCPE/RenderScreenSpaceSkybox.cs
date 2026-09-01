using UnityEngine;
using UnityEngine.Rendering;

namespace SCPE
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class RenderScreenSpaceSkybox : MonoBehaviour
	{
		private Camera thisCam;

		private Camera skyboxCam;

		private RenderTexture skyboxRT;

		private const string RENDER_TAG = "[SCPE] Render skybox to texture";

		private CommandBuffer cmd;

		private int skyboxTexID;

		private const string texName = "_SkyboxTex";

		private const int downsamples = 2;

		public bool manuallyAdded = true;

		private void OnEnable()
		{
			cmd = new CommandBuffer();
			cmd.name = "[SCPE] Render skybox to texture";
			if (!thisCam)
			{
				thisCam = GetComponent<Camera>();
			}
		}

		private void Update()
		{
			if ((bool)thisCam)
			{
				CopyCameraSettings(thisCam, skyboxCam);
			}
		}

		public void Destroy()
		{
			if (cmd != null)
			{
				thisCam.RemoveCommandBuffer(CameraEvent.AfterSkybox, cmd);
			}
		}

		private void CreateSkyboxCamera()
		{
			GameObject gameObject = new GameObject("Skybox renderer for " + thisCam.name);
			skyboxCam = gameObject.AddComponent<Camera>();
			gameObject.hideFlags = HideFlags.HideInHierarchy;
			skyboxCam.hideFlags = HideFlags.NotEditable;
			skyboxCam.useOcclusionCulling = false;
			skyboxCam.depth = -100f;
			skyboxCam.allowMSAA = false;
			skyboxCam.cullingMask = 0;
			skyboxCam.clearFlags = CameraClearFlags.Skybox;
			skyboxCam.nearClipPlane = 0.01f;
			skyboxCam.farClipPlane = 1f;
			CreateSkyboxRT();
			skyboxCam.AddCommandBuffer(CameraEvent.AfterSkybox, cmd);
			skyboxCam.targetTexture = skyboxRT;
			cmd.SetGlobalTexture("_SkyboxTex", skyboxRT);
		}

		private void CreateSkyboxRT()
		{
			skyboxRT = new RenderTexture(thisCam.pixelWidth / 2, thisCam.pixelHeight / 2, 0, RenderTextureFormat.ARGB32);
			skyboxRT.filterMode = FilterMode.Trilinear;
			skyboxRT.useMipMap = true;
			skyboxRT.autoGenerateMips = true;
			skyboxRT.Create();
		}

		public void OnDestroy()
		{
			if ((bool)skyboxCam)
			{
				skyboxCam.RemoveCommandBuffer(CameraEvent.AfterSkybox, cmd);
			}
		}

		private static void CopyCameraSettings(Camera src, Camera dest)
		{
			if (!(dest == null))
			{
				dest.transform.position = src.transform.position;
				dest.transform.rotation = src.transform.rotation;
				dest.fieldOfView = src.fieldOfView;
				dest.aspect = src.aspect;
				dest.orthographic = src.orthographic;
				dest.orthographicSize = src.orthographicSize;
				dest.renderingPath = src.renderingPath;
				dest.targetDisplay = src.targetDisplay;
			}
		}
	}
}
