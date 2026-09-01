using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Rendering/Global Fog")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GlobalFog : PostEffectsBase
{
	public enum FogMode
	{
		AbsoluteYAndDistance = 0,
		AbsoluteY = 1,
		Distance = 2,
		RelativeYAndDistance = 3
	}

	public FogMode fogMode;

	private float CAMERA_NEAR = 0.5f;

	private float CAMERA_FAR = 50f;

	private float CAMERA_FOV = 60f;

	private float CAMERA_ASPECT_RATIO = 1.333333f;

	public float startDistance = 200f;

	public float globalDensity = 1f;

	public float heightScale = 100f;

	public float height;

	public Color globalFogColor = Color.grey;

	public Shader fogShader;

	private Material fogMaterial;

	protected override bool CheckResources()
	{
		CheckSupport(true);
		fogMaterial = CheckShaderAndCreateMaterial(fogShader, fogMaterial);
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}
		Camera component = GetComponent<Camera>();
		CAMERA_NEAR = component.nearClipPlane;
		CAMERA_FAR = component.farClipPlane;
		CAMERA_FOV = component.fieldOfView;
		CAMERA_ASPECT_RATIO = component.aspect;
		Matrix4x4 identity = Matrix4x4.identity;
		float num = CAMERA_FOV * 0.5f;
		Vector3 vector = component.transform.right * CAMERA_NEAR * Mathf.Tan(num * ((float)Math.PI / 180f)) * CAMERA_ASPECT_RATIO;
		Vector3 vector2 = component.transform.up * CAMERA_NEAR * Mathf.Tan(num * ((float)Math.PI / 180f));
		Vector3 vector3 = component.transform.forward * CAMERA_NEAR - vector + vector2;
		float num2 = vector3.magnitude * CAMERA_FAR / CAMERA_NEAR;
		vector3.Normalize();
		vector3 *= num2;
		Vector3 vector4 = component.transform.forward * CAMERA_NEAR + vector + vector2;
		vector4.Normalize();
		vector4 *= num2;
		Vector3 vector5 = component.transform.forward * CAMERA_NEAR + vector - vector2;
		vector5.Normalize();
		vector5 *= num2;
		Vector3 vector6 = component.transform.forward * CAMERA_NEAR - vector - vector2;
		vector6.Normalize();
		vector6 *= num2;
		identity.SetRow(0, vector3);
		identity.SetRow(1, vector4);
		identity.SetRow(2, vector5);
		identity.SetRow(3, vector6);
		fogMaterial.SetMatrix("_FrustumCornersWS", identity);
		fogMaterial.SetVector("_CameraWS", component.transform.position);
		fogMaterial.SetVector("_StartDistance", new Vector4(1f / startDistance, num2 - startDistance));
		fogMaterial.SetVector("_Y", new Vector4(height, 1f / heightScale));
		fogMaterial.SetFloat("_GlobalDensity", globalDensity * 0.01f);
		fogMaterial.SetColor("_FogColor", globalFogColor);
		CustomGraphicsBlit(source, destination, fogMaterial, (int)fogMode);
	}

	private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
	{
		RenderTexture.active = dest;
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
}
