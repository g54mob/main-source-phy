using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class HeightOffsetRenderer : SimulationOutput
	{
		public Shader depthShader;

		public LayerMask renderedLayers;

		public float cameraYOffset;

		private float m_latestFarClipPlane;

		private float m_latestNearClipPlane;

		private Camera depthCamera;

		private RenderTexture displacementDepth;

		public float FarClipPlane
		{
			get
			{
				return m_latestFarClipPlane;
			}
		}

		public float NearClipPlane
		{
			get
			{
				return m_latestNearClipPlane;
			}
		}

		protected void OnDestroy()
		{
			DestroyDepthCamera();
		}

		public override void LoadData()
		{
			FindTextureManager();
		}

		private Camera CreateDepthCamera()
		{
			GameObject gameObject = new GameObject("Wave Depth Camera", typeof(Camera));
			gameObject.transform.parent = base.transform;
			gameObject.transform.Rotate(Vector3.right, -90f);
			gameObject.hideFlags = HideFlags.DontSave;
			gameObject.SetActive(false);
			Camera component = gameObject.GetComponent<Camera>();
			component.orthographic = true;
			component.cullingMask = renderedLayers;
			component.backgroundColor = Color.white;
			component.clearFlags = CameraClearFlags.Color;
			return component;
		}

		private void DestroyDepthCamera()
		{
			if (depthCamera != null)
			{
				GameObject obj = depthCamera.gameObject;
				Object.DestroyImmediate(obj);
			}
		}

		public override void RunStep()
		{
			if (depthCamera == null)
			{
				depthCamera = CreateDepthCamera();
			}
			RenderTexture renderTexture = simTextureManager.CreateOutputTexture("Displacing Objects Depth");
			float num = simTextureManager.resolutionU;
			float num2 = simTextureManager.resolutionV;
			depthCamera.targetTexture = renderTexture;
			depthCamera.orthographicSize = num / 2f;
			depthCamera.aspect = num / num2;
			Vector3 position = new Vector3(0.5f * num, cameraYOffset, 0.5f * num2) + base.transform.position;
			depthCamera.transform.position = position;
			depthCamera.RenderWithShader(depthShader, "RenderType");
			UpdateOutput(renderTexture);
			m_latestFarClipPlane = depthCamera.farClipPlane;
			m_latestNearClipPlane = depthCamera.nearClipPlane;
		}
	}
}
