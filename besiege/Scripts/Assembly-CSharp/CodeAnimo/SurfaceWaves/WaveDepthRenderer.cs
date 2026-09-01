using System.Collections.Generic;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Graphics/Water Depth Renderer")]
	public class WaveDepthRenderer : SimulationOutput
	{
		private RenderTexture depthTexture;

		public bool renderDepthMap = true;

		private bool renderingDepth;

		public Shader depthShader;

		private string defaultShaderName = "Hidden/Camera-DepthTexture";

		public WaveMeshGroup selectedWater;

		public List<Camera> selectedCameras = new List<Camera>();

		private Camera lastRenderingCamera;

		[SerializeField]
		[HideInInspector]
		private Camera depthCamera;

		public override void LoadData()
		{
			FindTextureManager();
			if (depthShader == null)
			{
				depthShader = Shader.Find(defaultShaderName);
				if (depthShader == null)
				{
					throw new MissingReferenceException("No depth rendering shader detected, and default shader can't be found");
				}
			}
			if (depthCamera == null)
			{
				depthCamera = CreateDepthCamera();
			}
		}

		public override void RunStep()
		{
			lastRenderingCamera = null;
		}

		public void CalculateDepth(Camera currentCamera)
		{
			if (base.enabled && renderDepthMap && !renderingDepth && !object.ReferenceEquals(currentCamera, lastRenderingCamera) && CameraIsSelected(currentCamera))
			{
				lastRenderingCamera = currentCamera;
				GenerateDepthTexture(currentCamera);
			}
		}

		private void GenerateDepthTexture(Camera currentCamera)
		{
			if (!(depthCamera == null))
			{
				if (depthShader == null)
				{
					throw new MissingReferenceException("Depth Rendering Shader missing");
				}
				renderingDepth = true;
				depthCamera.CopyFrom(currentCamera);
				int pixelWidth = currentCamera.pixelWidth;
				int pixelHeight = currentCamera.pixelHeight;
				simTextureManager.resolutionU = pixelWidth;
				simTextureManager.resolutionV = pixelHeight;
				depthTexture = simTextureManager.CreateOutputTexture(currentCamera.name + " Water Depth", true);
				depthCamera.renderingPath = RenderingPath.Forward;
				depthCamera.targetTexture = depthTexture;
				depthCamera.clearFlags = CameraClearFlags.Color;
				depthCamera.backgroundColor = Color.white;
				depthCamera.RenderWithShader(depthShader, "RenderType");
				UpdateOutput(depthTexture);
				Material selectedMaterial = selectedWater.selectedMaterial;
				selectedMaterial.SetTexture("_DepthTex", base.outputData);
				renderingDepth = false;
			}
		}

		private bool CameraIsSelected(Camera currentCam)
		{
			return selectedCameras.Contains(currentCam);
		}

		private Camera CreateDepthCamera()
		{
			GameObject gameObject = new GameObject("Water Mesh Depth Camera", typeof(Camera));
			gameObject.transform.parent = base.transform;
			gameObject.hideFlags = HideFlags.DontSave;
			gameObject.SetActive(false);
			Camera component = gameObject.GetComponent<Camera>();
			component.SetReplacementShader(depthShader, "RenderType");
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
	}
}
