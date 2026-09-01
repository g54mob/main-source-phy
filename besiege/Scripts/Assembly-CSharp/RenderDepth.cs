using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RenderDepth : MonoBehaviour
{
	public Shader depthShader;

	private RenderTexture renderTexture;

	private GameObject shaderCamera;

	private void Start()
	{
		if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			base.enabled = false;
		}
		else if (!depthShader || !depthShader.isSupported)
		{
			base.enabled = false;
		}
	}

	private void OnDisable()
	{
		Object.DestroyImmediate(shaderCamera);
	}

	private void OnPreCull()
	{
		if (base.enabled && base.gameObject.activeSelf)
		{
			renderTexture = RenderTexture.GetTemporary(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight, 24, RenderTextureFormat.Depth);
			if (shaderCamera == null)
			{
				shaderCamera = new GameObject("ShaderCamera", typeof(Camera));
				shaderCamera.GetComponent<Camera>().enabled = false;
				shaderCamera.hideFlags = HideFlags.HideAndDontSave;
			}
			Camera component = shaderCamera.GetComponent<Camera>();
			component.CopyFrom(GetComponent<Camera>());
			component.backgroundColor = new Color(1f, 1f, 1f, 1f);
			component.clearFlags = CameraClearFlags.Color;
			component.targetTexture = renderTexture;
			component.RenderWithShader(depthShader, "RenderType");
			Shader.SetGlobalTexture("_GlobalDepthTexture", renderTexture);
			Shader.SetGlobalVector("_GlobalDepthTextureSize", new Vector4(renderTexture.width, renderTexture.height, 0f, 0f));
		}
	}

	private void OnPostRender()
	{
		if (base.enabled && base.gameObject.activeSelf)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}
}
