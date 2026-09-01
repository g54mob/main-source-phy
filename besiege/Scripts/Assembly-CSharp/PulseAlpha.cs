using System;
using System.Collections;
using UnityEngine;

public class PulseAlpha : MonoBehaviour
{
	public Renderer[] rendys;

	public float duration = 1f;

	public float minAlpha;

	public float multiplier = 1f;

	public PulseAlpha follow;

	private Color currentColor;

	private float offset = -1f;

	private bool started;

	private bool fading;

	private bool visible = true;

	public bool combineMaterial;

	private Material cachedMat;

	private bool removing;

	public bool Transitioning
	{
		get
		{
			return fading;
		}
	}

	public float Offset
	{
		get
		{
			if (!started)
			{
				Awake();
			}
			return offset;
		}
	}

	private void Awake()
	{
		if (offset != -1f)
		{
			return;
		}
		started = true;
		if (follow != null)
		{
			offset = follow.Offset + 0.06f;
		}
		else
		{
			offset = UnityEngine.Random.value * 10f;
		}
		if (!combineMaterial)
		{
			return;
		}
		cachedMat = rendys[0].material;
		if (rendys.Length > 1)
		{
			for (int i = 1; i < rendys.Length; i++)
			{
				rendys[i].sharedMaterial = cachedMat;
			}
		}
	}

	private void Update()
	{
		if (!fading && visible)
		{
			Pulse();
		}
	}

	private void OnDestroy()
	{
		removing = true;
		float a = minAlpha + 0.5f * multiplier;
		if (combineMaterial)
		{
			Color color = cachedMat.GetColor("_TintColor");
			cachedMat.SetColor("_TintColor", new Color(color.r, color.g, color.b, a));
			return;
		}
		for (int i = 0; i < rendys.Length; i++)
		{
			Renderer renderer = rendys[i];
			if (!(renderer == null) && !(renderer.material == null))
			{
				Color color2 = renderer.material.GetColor("_TintColor");
				renderer.material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, a));
			}
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		fading = false;
		visible = false;
		if (removing)
		{
			return;
		}
		if (combineMaterial)
		{
			Color color = cachedMat.GetColor("_TintColor");
			cachedMat.SetColor("_TintColor", new Color(color.r, color.g, color.b, 0f));
			return;
		}
		for (int i = 0; i < rendys.Length; i++)
		{
			Material material = rendys[i].material;
			if (material.HasProperty("_TintColor"))
			{
				Color color2 = material.GetColor("_TintColor");
				material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, 0f));
			}
		}
	}

	private void OnEnable()
	{
		if (!fading)
		{
			StartCoroutine(FadeIn());
		}
	}

	public IEnumerator FadeOut()
	{
		fading = true;
		float cTime = 0f;
		Color[] currentCol = new Color[combineMaterial ? 1 : rendys.Length];
		if (combineMaterial)
		{
			currentCol[0] = cachedMat.GetColor("_TintColor");
			if (currentCol[0].a == 0f)
			{
				visible = false;
				fading = false;
				yield return null;
				yield break;
			}
		}
		else
		{
			for (int i = 0; i < rendys.Length; i++)
			{
				currentCol[i] = rendys[i].material.GetColor("_TintColor");
				if (currentCol[i].a == 0f)
				{
					visible = false;
					fading = false;
					yield return null;
					yield break;
				}
			}
		}
		yield return new WaitForSeconds(0.5f);
		while (cTime < 1f)
		{
			if (combineMaterial)
			{
				cachedMat.SetColor("_TintColor", new Color(currentCol[0].r, currentCol[0].g, currentCol[0].b, Mathf.Lerp(currentCol[0].a, 0f, cTime)));
			}
			else
			{
				for (int j = 0; j < rendys.Length; j++)
				{
					rendys[j].material.SetColor("_TintColor", new Color(currentCol[j].r, currentCol[j].g, currentCol[j].b, Mathf.Lerp(currentCol[j].a, 0f, cTime)));
				}
			}
			cTime += TimeSlider.Instance.DeltaTime() * 2f;
			yield return null;
		}
		visible = false;
		fading = false;
	}

	public IEnumerator FadeIn()
	{
		fading = true;
		float cTime = 0f;
		Color[] currentCol = new Color[combineMaterial ? 1 : rendys.Length];
		float phi = (Time.time + 0.256f + offset) / duration * 2f * (float)Math.PI;
		float amplitude = Mathf.Cos(phi) * 0.5f + 0.5f;
		float alpha = minAlpha + amplitude * multiplier;
		if (combineMaterial)
		{
			currentCol[0] = cachedMat.GetColor("_TintColor");
			if (currentCol[0].a != 0f)
			{
				fading = false;
				visible = true;
				yield return null;
				yield break;
			}
		}
		else
		{
			for (int i = 0; i < rendys.Length; i++)
			{
				currentCol[i] = rendys[i].material.GetColor("_TintColor");
				if (currentCol[i].a != 0f)
				{
					fading = false;
					visible = true;
					yield return null;
					yield break;
				}
			}
		}
		while (cTime < 1f)
		{
			if (combineMaterial)
			{
				cachedMat.SetColor("_TintColor", new Color(currentCol[0].r, currentCol[0].g, currentCol[0].b, Mathf.Lerp(currentCol[0].a, alpha, cTime)));
			}
			else
			{
				for (int j = 0; j < rendys.Length; j++)
				{
					rendys[j].material.SetColor("_TintColor", new Color(currentCol[j].r, currentCol[j].g, currentCol[j].b, Mathf.Lerp(currentCol[j].a, alpha, cTime)));
				}
			}
			visible = true;
			cTime += TimeSlider.Instance.DeltaTime() * 4f;
			yield return null;
		}
		fading = false;
	}

	private void Pulse()
	{
		if (rendys.Length == 0 || ((!combineMaterial) ? (!rendys[0].material.HasProperty("_TintColor")) : (!cachedMat.HasProperty("_TintColor"))))
		{
			return;
		}
		float f = (Time.time + offset) / duration * 2f * (float)Math.PI;
		float num = Mathf.Cos(f) * 0.5f + 0.5f;
		float a = minAlpha + num * multiplier;
		if (combineMaterial)
		{
			Color color = cachedMat.GetColor("_TintColor");
			cachedMat.SetColor("_TintColor", new Color(color.r, color.g, color.b, a));
			return;
		}
		for (int i = 0; i < rendys.Length; i++)
		{
			Renderer renderer = rendys[i];
			Color color2 = renderer.material.GetColor("_TintColor");
			renderer.material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, a));
		}
	}
}
