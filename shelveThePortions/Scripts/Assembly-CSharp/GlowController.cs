using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowController : MonoBehaviour
{
	[SerializeField]
	private float duration;

	[SerializeField]
	private List<MeshRenderer> meshRenderersList;

	public void SetGlow(Color color)
	{
		if (meshRenderersList.Count == 0)
		{
			meshRenderersList.AddRange(GetComponentsInChildren<MeshRenderer>());
		}
		SetColor(color);
		StopAllCoroutines();
		StartCoroutine(Pulse());
	}

	private void SetColor(Color color)
	{
		foreach (MeshRenderer meshRenderers in meshRenderersList)
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			meshRenderers.GetPropertyBlock(materialPropertyBlock);
			materialPropertyBlock.SetColor("_GlowColor", color);
			meshRenderers.SetPropertyBlock(materialPropertyBlock);
		}
	}

	private IEnumerator Pulse()
	{
		float t = 0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			float value = Mathf.Lerp(0.45f, 0.55f, t / duration);
			foreach (MeshRenderer meshRenderers in meshRenderersList)
			{
				meshRenderers.material.SetFloat("_Pulse", value);
			}
			yield return null;
		}
		foreach (MeshRenderer meshRenderers2 in meshRenderersList)
		{
			meshRenderers2.material.SetFloat("_Pulse", 0.45f);
		}
	}
}
