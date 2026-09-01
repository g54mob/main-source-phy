using System.Collections;
using UnityEngine;

public class FadeInOnHover : MonoBehaviour
{
	[SerializeField]
	private float fadeToAlpha = 0.5f;

	[SerializeField]
	private float fadeTime = 0.15f;

	[SerializeField]
	private UIHoverArea[] uiHoverAreas;

	[SerializeField]
	private Renderer[] renderersToFade;

	private bool displayed;

	private void Awake()
	{
		SetRenderersAlpha(0f);
	}

	private void LateUpdate()
	{
		bool flag = false;
		UIHoverArea[] array = uiHoverAreas;
		foreach (UIHoverArea uIHoverArea in array)
		{
			if (uIHoverArea.isMouseOver)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			MouseEnter();
		}
		else
		{
			MouseExit();
		}
	}

	private void MouseEnter()
	{
		if (!displayed)
		{
			displayed = true;
			Fade(fadeToAlpha);
		}
	}

	private void MouseExit()
	{
		if (displayed)
		{
			displayed = false;
			Fade(0f);
		}
	}

	private void Fade(float alpha)
	{
		iTween.Stop(base.gameObject, true);
		Renderer[] array = renderersToFade;
		foreach (Renderer renderer in array)
		{
			Hashtable hashtable = iTween.Hash("alpha", alpha, "time", fadeTime, "ignoretimescale", true);
			if (renderer.material.HasProperty("_Color"))
			{
				hashtable.Add("namedcolorvalue", "_Color");
			}
			else
			{
				if (!renderer.material.HasProperty("_TintColor"))
				{
					if (BesiegeLogFilter.logDebug)
					{
						Debug.Log("Couldn't find suitable material property to fade.");
					}
					continue;
				}
				hashtable.Add("namedcolorvalue", "_TintColor");
			}
			iTween.FadeTo(renderer.gameObject, hashtable);
		}
	}

	private void SetRenderersAlpha(float alpha)
	{
		Renderer[] array = renderersToFade;
		foreach (Renderer renderer in array)
		{
			Renderer component = renderer.GetComponent<Renderer>();
			SetRendererAlpha(component, alpha);
		}
	}

	private void SetRendererAlpha(Renderer renderer, float alpha)
	{
		if (renderer.material.HasProperty("_TintColor"))
		{
			SetMaterialColor(renderer.material, "_TintColor", alpha);
		}
		else if (renderer.material.HasProperty("_Color"))
		{
			SetMaterialColor(renderer.material, "_Color", alpha);
		}
	}

	private void SetMaterialColor(Material material, string propertyName, float alpha)
	{
		Color color = material.GetColor(propertyName);
		material.SetColor(propertyName, new Color(color.r, color.g, color.b, alpha));
	}
}
