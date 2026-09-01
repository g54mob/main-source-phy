using System.Collections;
using UnityEngine;

public class CameraMBlurScript : MonoBehaviour
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/CameraMBlur")]
	private Shader compositeShader;

	private float Strength = 13f;

	private Material m_CompositeMaterial;

	private Material GetCompositeMaterial()
	{
		if (m_CompositeMaterial == null)
		{
			m_CompositeMaterial = new Material(compositeShader);
			m_CompositeMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
		return m_CompositeMaterial;
	}

	private void OnDisable()
	{
		Object.DestroyImmediate(m_CompositeMaterial);
	}

	private void OnPreCull()
	{
		Shader.SetGlobalMatrix("_Myview", (GetComponent<Camera>().worldToCameraMatrix.inverse * GetComponent<Camera>().projectionMatrix).inverse);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material compositeMaterial = GetCompositeMaterial();
		compositeMaterial.SetFloat("_Strength", Strength);
		Graphics.Blit(source, destination, compositeMaterial);
	}

	private void OnPostRender()
	{
		StartCoroutine(renderlate());
	}

	private IEnumerator renderlate()
	{
		yield return new WaitForEndOfFrame();
		Matrix4x4 Iviewprev = GetComponent<Camera>().worldToCameraMatrix.inverse * GetComponent<Camera>().projectionMatrix;
		Shader.SetGlobalMatrix("_Myviewprev", Iviewprev);
	}
}
