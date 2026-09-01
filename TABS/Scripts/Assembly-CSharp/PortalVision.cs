using System;
using UnityEngine;

public class PortalVision : MonoBehaviour
{
	private Transform m_portal;

	private Material m_shaderMaterial;

	private Camera m_cam;

	private AnimationCurve m_distanceAlpha;

	public void Init(Material material, Transform portal, AnimationCurve distanceCurve)
	{
		m_shaderMaterial = material;
		m_cam = GetComponent<Camera>();
		m_portal = portal;
		m_distanceAlpha = distanceCurve;
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		float time = Vector3.Distance(m_portal.transform.position, m_cam.transform.position);
		float num = m_distanceAlpha.Evaluate(time);
		if (num < 0.01f)
		{
			Graphics.Blit(src, dest);
			return;
		}
		SetProperties(src);
		m_shaderMaterial.SetFloat("_BlendAlpha", num);
		RenderTexture.active = dest;
		GL.PushMatrix();
		GL.LoadOrtho();
		m_shaderMaterial.SetPass(0);
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

	private void SetProperties(RenderTexture source)
	{
		m_shaderMaterial.SetTexture("_MainTex", source);
		m_shaderMaterial.SetMatrix("_CamFrustum", GetCamFrustum());
		m_shaderMaterial.SetMatrix("_CamToWorld", m_cam.cameraToWorldMatrix);
	}

	private Matrix4x4 GetCamFrustum()
	{
		Matrix4x4 identity = Matrix4x4.identity;
		float num = Mathf.Tan(m_cam.fieldOfView * 0.5f * ((float)Math.PI / 180f));
		Vector3 vector = Vector3.up * num;
		Vector3 vector2 = Vector3.right * num * m_cam.aspect;
		Vector3 vector3 = -Vector3.forward - vector2 + vector;
		Vector3 vector4 = -Vector3.forward + vector2 + vector;
		Vector3 vector5 = -Vector3.forward + vector2 - vector;
		Vector3 vector6 = -Vector3.forward - vector2 - vector;
		identity.SetRow(0, vector3);
		identity.SetRow(1, vector4);
		identity.SetRow(2, vector5);
		identity.SetRow(3, vector6);
		return identity;
	}

	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(m_shaderMaterial);
	}
}
