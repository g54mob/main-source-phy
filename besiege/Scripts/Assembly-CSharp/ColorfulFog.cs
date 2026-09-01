using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Rendering/Colorful Fog")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ColorfulFog : MonoBehaviour
{
	public enum ColoringMode
	{
		Solid = 0,
		Cube = 1,
		SimpleGradient = 2,
		Gradient = 3,
		GradientTexture = 4
	}

	public bool useCustomDepthTexture = true;

	public bool distanceFog = true;

	public bool useRadialDistance;

	public bool heightFog;

	public float height;

	[Range(0.001f, 10f)]
	public float heightDensity = 2f;

	public float startDistance;

	public FogMode fogMode = FogMode.Exponential;

	public float fogDensity = 0.01f;

	public float fogStart = 50f;

	public float fogEnd = 100f;

	public ColoringMode coloringMode;

	public Cubemap fogCube;

	public Color solidColor = Color.magenta;

	public Color skyColor = Color.red;

	public Color equatorColor = Color.green;

	public Color groundColor = Color.blue;

	public Gradient gradient;

	public int gradientResolution = 100;

	public Texture2D gradientTexture;

	public Shader fogShader;

	public Shader customDepthShader;

	protected Texture2D tmpGradientTexture;

	private Material fogMaterial;

	private Camera cam;

	private Camera depthCamera;

	private RenderTexture depthTexture;

	private Vector2 cachedResolution = new Vector2(Screen.width, Screen.height);

	private Material GetFogMaterial()
	{
		if (fogMaterial == null)
		{
			if (fogShader == null)
			{
				Debug.LogError("fogShader is not assigned");
				base.enabled = false;
				return null;
			}
			fogMaterial = new Material(fogShader);
		}
		fogMaterial.hideFlags = HideFlags.HideAndDontSave;
		return fogMaterial;
	}

	private bool CheckResources()
	{
		if (GetFogMaterial() == null)
		{
			return false;
		}
		return true;
	}

	private void OnDisable()
	{
		if (depthCamera != null)
		{
			UnityEngine.Object.DestroyImmediate(depthCamera.gameObject);
		}
		if (depthTexture != null)
		{
			RenderTexture.ReleaseTemporary(depthTexture);
			depthTexture = null;
		}
		if (tmpGradientTexture != null)
		{
			UnityEngine.Object.DestroyImmediate(tmpGradientTexture);
			tmpGradientTexture = null;
		}
	}

	private bool ScreenResolutionChanged()
	{
		Vector2 vector = new Vector2(Screen.width, Screen.height);
		if (vector == cachedResolution)
		{
			cachedResolution = vector;
			return false;
		}
		cachedResolution = vector;
		return true;
	}

	private void OnPreRender()
	{
		if (!useCustomDepthTexture)
		{
			if (depthCamera != null)
			{
				UnityEngine.Object.DestroyImmediate(depthCamera.gameObject);
			}
		}
		else if (base.enabled)
		{
			if (cam == null)
			{
				cam = GetComponent<Camera>();
			}
			if (ScreenResolutionChanged() && depthTexture != null)
			{
				RenderTexture.ReleaseTemporary(depthTexture);
				depthTexture = null;
				depthTexture = RenderTexture.GetTemporary(cam.pixelWidth, cam.pixelHeight, 8, RenderTextureFormat.Default);
			}
			if (depthTexture == null)
			{
				depthTexture = RenderTexture.GetTemporary(cam.pixelWidth, cam.pixelHeight, 8, RenderTextureFormat.Default);
			}
			if (!depthCamera)
			{
				GameObject gameObject = new GameObject("numYbmEEUj1dGQT6ybw9EPd2qr7ISj");
				depthCamera = gameObject.AddComponent<Camera>();
				depthCamera.enabled = false;
				depthCamera.gameObject.hideFlags = HideFlags.HideAndDontSave;
			}
			depthCamera.CopyFrom(cam);
			depthCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			depthCamera.clearFlags = CameraClearFlags.Color;
			depthCamera.targetTexture = depthTexture;
			depthCamera.hideFlags = HideFlags.DontSave;
			depthCamera.RenderWithShader(customDepthShader, string.Empty);
			Shader.SetGlobalTexture("_CustomDepthTexture", depthTexture);
			Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(cam.projectionMatrix, false);
			fogMaterial.SetMatrix("_CameraInverseProjection", gPUProjectionMatrix.inverse);
		}
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources() || (!distanceFog && !heightFog))
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (cam == null)
		{
			cam = GetComponent<Camera>();
		}
		if (!useCustomDepthTexture)
		{
			cam.depthTextureMode |= DepthTextureMode.Depth;
		}
		Transform transform = cam.transform;
		float nearClipPlane = cam.nearClipPlane;
		float farClipPlane = cam.farClipPlane;
		float fieldOfView = cam.fieldOfView;
		float aspect = cam.aspect;
		Matrix4x4 identity = Matrix4x4.identity;
		float num = fieldOfView * 0.5f;
		float num2 = Mathf.Tan(num * ((float)Math.PI / 180f));
		Vector3 vector = transform.right * nearClipPlane * num2 * aspect;
		Vector3 vector2 = transform.up * nearClipPlane * num2;
		Vector3 v = transform.forward * nearClipPlane - vector + vector2;
		float magnitude = v.magnitude;
		float num3 = magnitude * farClipPlane / nearClipPlane;
		v = ((magnitude == 0f) ? Vector3.zero : MathUtil.NormalizeVector(magnitude, v));
		v *= num3;
		Vector3 vector3 = transform.forward * nearClipPlane + vector + vector2;
		vector3.Normalize();
		vector3 *= num3;
		Vector3 vector4 = transform.forward * nearClipPlane + vector - vector2;
		vector4.Normalize();
		vector4 *= num3;
		Vector3 vector5 = transform.forward * nearClipPlane - vector - vector2;
		vector5.Normalize();
		vector5 *= num3;
		identity.SetRow(0, v);
		identity.SetRow(1, vector3);
		identity.SetRow(2, vector4);
		identity.SetRow(3, vector5);
		Vector3 position = transform.position;
		float num4 = position.y - height;
		float z = ((!(num4 <= 0f)) ? 0f : 1f);
		fogMaterial.SetMatrix("_FrustumCornersWS", identity);
		fogMaterial.SetVector("_CameraWS", position);
		fogMaterial.SetVector("_HeightParams", new Vector4(height, num4, z, heightDensity * 0.5f));
		fogMaterial.SetVector("_DistanceParams", new Vector4(0f - Mathf.Max(startDistance, 0f), 0f, 0f, 0f));
		bool flag = fogMode == FogMode.Linear;
		float num5 = ((!flag) ? 0f : (fogEnd - fogStart));
		float num6 = ((!(Mathf.Abs(num5) > 0.0001f)) ? 0f : (1f / num5));
		Vector4 vector6 = default(Vector4);
		vector6.x = fogDensity * 1.2011224f;
		vector6.y = fogDensity * 1.442695f;
		vector6.z = ((!flag) ? 0f : (0f - num6));
		vector6.w = ((!flag) ? 0f : (fogEnd * num6));
		fogMaterial.SetVector("_SceneFogParams", vector6);
		fogMaterial.SetVector("_SceneFogMode", new Vector4((float)fogMode, useRadialDistance ? 1 : 0, 0f, 0f));
		int num7 = 0;
		num7 = ((!distanceFog || !heightFog) ? ((!distanceFog) ? 8 : 4) : 0);
		num7 += Mathf.Clamp((int)coloringMode, 0, 3);
		fogMaterial.SetTexture("_Cube", fogCube);
		Matrix4x4 identity2 = Matrix4x4.identity;
		if (coloringMode == ColoringMode.Solid)
		{
			identity2.SetRow(0, solidColor);
		}
		else
		{
			identity2.SetRow(0, skyColor);
		}
		identity2.SetRow(1, equatorColor);
		identity2.SetRow(2, groundColor);
		fogMaterial.SetMatrix("_Colors", identity2);
		fogMaterial.SetInt("_UseCustomDepth", Convert.ToInt32(useCustomDepthTexture));
		if (coloringMode == ColoringMode.GradientTexture)
		{
			fogMaterial.SetTexture("_Gradient", gradientTexture);
		}
		else if (coloringMode == ColoringMode.Gradient)
		{
			if (tmpGradientTexture == null)
			{
				ApplyGradientChanges();
			}
			fogMaterial.SetTexture("_Gradient", tmpGradientTexture);
		}
		CustomGraphicsBlit(source, destination, fogMaterial, num7);
	}

	private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
	{
		Graphics.SetRenderTarget(dest);
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		GL.MultiTexCoord2(0, 0f, 0f);
		GL.Vertex3(0f, 0f, 3f);
		GL.MultiTexCoord2(0, 1f, 0f);
		GL.Vertex3(1f, 0f, 2f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 1f);
		GL.MultiTexCoord2(0, 0f, 1f);
		GL.Vertex3(0f, 1f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	public void ApplyGradientChanges()
	{
		UnityEngine.Object.DestroyImmediate(tmpGradientTexture);
		tmpGradientTexture = null;
		tmpGradientTexture = GetGradientTexture(gradient, gradientResolution);
		tmpGradientTexture.hideFlags = HideFlags.HideAndDontSave;
	}

	public void NullTmpGradTex()
	{
		UnityEngine.Object.DestroyImmediate(tmpGradientTexture);
		tmpGradientTexture = null;
	}

	private Texture2D GetGradientTexture(Gradient gradient, int resolution = 10)
	{
		Texture2D texture2D = new Texture2D(resolution, 1, TextureFormat.RGB24, false);
		Color[] array = new Color[resolution];
		for (int i = 0; i < array.Length; i++)
		{
			float time = Mathf.Clamp((float)i / (float)resolution, 0f, 1f);
			array[i] = gradient.Evaluate(time);
		}
		texture2D.SetPixels(array);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.Apply(false);
		return texture2D;
	}
}
