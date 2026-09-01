using System.Collections.Generic;
using UnityEngine;

namespace CodeAnimo
{
	public class CameraDepthChecker : MonoBehaviour
	{
		public List<Camera> depthlessCameras;

		public bool automaticallyEnableDepthOnStart = true;

		public DepthTextureMode automaticDepthTextureMode = DepthTextureMode.Depth;

		protected void Start()
		{
			FindDepthlessCameras();
			if (automaticallyEnableDepthOnStart)
			{
				EnableAllCameraDepthRendering();
			}
		}

		public void FindDepthlessCameras()
		{
			depthlessCameras = ListDepthlessCameras();
			if (!automaticallyEnableDepthOnStart && depthlessCameras.Count >= 1)
			{
				if (depthlessCameras.Count == 1)
				{
					Debug.LogWarning("One of your cameras is not set up for use with depth-based effects. Click this error message once to see the component with more information.", this);
				}
				else
				{
					Debug.LogWarning("Several of your cameras are not set up for use with depth-based effects. Click this error message once to see the component with more information.", this);
				}
			}
		}

		protected void ApplyDefaultCameraDepthTexture(Texture someDefaultTexture)
		{
			if (someDefaultTexture != null)
			{
				Shader.SetGlobalTexture("_CameraDepthTexture", someDefaultTexture);
			}
		}

		protected void EnableAllCameraDepthRendering()
		{
			for (int i = 0; i < depthlessCameras.Count; i++)
			{
				Camera camera = depthlessCameras[i];
				Debug.LogWarning("Depth rendering automatically enabled for camera '" + camera.name + "', by CameraDepthChecker to allow for depth-based effects.", this);
				depthlessCameras[i].depthTextureMode = automaticDepthTextureMode;
			}
		}

		protected List<Camera> ListDepthlessCameras()
		{
			List<Camera> list = new List<Camera>();
			Camera[] allCameras = Camera.allCameras;
			for (int i = 0; i < allCameras.Length; i++)
			{
				Camera camera = allCameras[i];
				if (camera.actualRenderingPath != RenderingPath.DeferredLighting)
				{
					DepthTextureMode depthTextureMode = camera.depthTextureMode;
					if (depthTextureMode != DepthTextureMode.Depth && depthTextureMode != DepthTextureMode.DepthNormals)
					{
						list.Add(allCameras[i]);
					}
				}
			}
			return list;
		}
	}
}
