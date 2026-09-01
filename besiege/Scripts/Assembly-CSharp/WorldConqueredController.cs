using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("UI/World Conquered Controller")]
public class WorldConqueredController : MonoBehaviour
{
	[SerializeField]
	protected GameObject parent;

	[SerializeField]
	protected AudioSource impactAudio;

	[SerializeField]
	protected AudioSource igniteAudio;

	public GlobeFadeController planet;

	[SerializeField]
	protected Transform solidMask;

	[SerializeField]
	protected ParticleSystem impact;

	[SerializeField]
	protected ConquerTextController text;

	[SerializeField]
	protected MeshRenderer flavorText;

	[SerializeField]
	protected MeshRenderer background;

	[SerializeField]
	protected MeshRenderer[] backgroundsUI = new MeshRenderer[0];

	[SerializeField]
	protected MeshRenderer[] extraFade = new MeshRenderer[0];

	[SerializeField]
	protected TriumphFlagAnim[] flagsAndTrumpets = new TriumphFlagAnim[0];

	[SerializeField]
	protected TriumphBarsLerpIn winUI;

	[SerializeField]
	protected ParticleSystem[] embers = new ParticleSystem[0];

	[SerializeField]
	protected ParticleSystem[] fires = new ParticleSystem[0];

	[SerializeField]
	protected ParticleSystem[] smoke = new ParticleSystem[0];

	[SerializeField]
	protected ParticleSystem[] particles = new ParticleSystem[0];

	[SerializeField]
	protected AnimationCurve curve;

	protected Coroutine fade;

	protected Coroutine solidify;

	protected Coroutine transformation;

	protected Coroutine bgFade;

	protected Coroutine flavorFade;

	protected Coroutine desaturation;

	public void Start()
	{
		Disable();
	}

	public void Display()
	{
		Disable();
		StartRoutine(this, fade, IEFadeIn());
	}

	public void AddRender(MeshRenderer ren)
	{
		List<MeshRenderer> list = extraFade.ToList();
		list.Add(ren);
		extraFade = list.ToArray();
	}

	public void Disable()
	{
		StopAllCoroutines();
		planet.Disable();
		SetIslandSaturation(1f);
		text.Disable();
		ChangeAlphaForRenderer(background, "_TintColor", 0f);
		for (int i = 0; i < backgroundsUI.Length; i++)
		{
			if (backgroundsUI[i] != null)
			{
				ChangeAlphaForRenderer(backgroundsUI[i], "_TintColor", 0f);
			}
			else
			{
				Debug.LogWarning("[WorldConqueredController] BackgroundsUI " + i + " is null!");
			}
		}
		ChangeAlphaForRenderer(flavorText, 0f);
		StopParticles();
		impact.Stop();
		DisplayFlags(false);
		solidMask.localScale = new Vector3(2.2f, 2.2f, 2.2f);
		winUI.HideBars();
		parent.SetActive(false);
	}

	protected IEnumerator IEFadeIn()
	{
		parent.SetActive(true);
		float planetFadeDuration = 0.2f;
		float transformDuration = planetFadeDuration;
		float textFadeDuration = 1f;
		float backgroundFadeDuration = 3f;
		float solidifyWait = 0.1f;
		float planetParticlesWait = 0.3f;
		DisplayFlags(true);
		yield return new WaitForSecondsRealtime(0.1f);
		winUI.ShowBars();
		yield return new WaitForSecondsRealtime(0.6f);
		FadeBackground(backgroundFadeDuration);
		text.StartAnimation(0f, textFadeDuration);
		yield return new WaitForSecondsRealtime(0.2f);
		FadeFlavorText(textFadeDuration);
		yield return new WaitForSecondsRealtime(0.7f);
		impactAudio.Play();
		planet.Fade(1f, planetFadeDuration);
		TransformPlanet(transformDuration);
		yield return new WaitForSecondsRealtime(solidifyWait);
		SolidifyPlanet(planetFadeDuration);
		yield return new WaitForSecondsRealtime(transformDuration - solidifyWait);
		PlayImpact();
		yield return new WaitForSecondsRealtime(0.3f);
		DesaturateIslands(backgroundFadeDuration * 1.5f);
		igniteAudio.Play();
		PlayParticles(embers);
		yield return new WaitForSecondsRealtime(planetParticlesWait);
		PlayParticles(smoke);
		yield return new WaitForSecondsRealtime(planetParticlesWait);
		PlayParticles(fires);
		yield return new WaitForSecondsRealtime(planetParticlesWait);
		PlayParticles(particles);
	}

	protected void TransformPlanet(float duration)
	{
		StartRoutine(this, transformation, IETransformPlanet(duration));
	}

	protected IEnumerator IETransformPlanet(float duration)
	{
		Vector3 farSize = Vector3.one * 1.4f;
		Vector3 targetScale = Vector3.one;
		for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			planet.transform.localScale = Vector3.Lerp(farSize, targetScale, pct);
			yield return null;
		}
		planet.transform.localScale = targetScale;
	}

	protected void SolidifyPlanet(float duration)
	{
		StartRoutine(this, solidify, IESolidifyPlanet(duration));
	}

	protected IEnumerator IESolidifyPlanet(float duration)
	{
		for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			solidMask.localScale = Vector3.Lerp(Vector3.one * 2.2f, Vector3.zero, curve.Evaluate(pct));
			yield return null;
		}
	}

	protected void FadeBackground(float duration)
	{
		StartRoutine(this, bgFade, IEFadeBackground(duration));
	}

	protected IEnumerator IEFadeBackground(float duration)
	{
		for (int i = 0; i < backgroundsUI.Length; i++)
		{
			backgroundsUI[i].gameObject.SetActive(true);
		}
		for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			ChangeAlphaForRenderer(background, "_TintColor", Mathf.Lerp(0f, 0.45f, curve.Evaluate(pct)));
			for (int j = 0; j < backgroundsUI.Length; j++)
			{
				ChangeAlphaForRenderer(backgroundsUI[j], "_TintColor", Mathf.Lerp(0f, 0.27f, curve.Evaluate(pct)));
			}
			for (int k = 0; k < extraFade.Length; k++)
			{
				ChangeAlphaForRenderer(extraFade[k], "_TintColor", Mathf.Lerp(0f, 0.5f, curve.Evaluate(pct)));
			}
			yield return null;
		}
	}

	protected void DesaturateIslands(float duration)
	{
		StartRoutine(this, desaturation, IEDesaturateIslands(duration));
	}

	protected IEnumerator IEDesaturateIslands(float duration)
	{
		for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			float a = Mathf.Lerp(1f, 0.3f, pct);
			SetIslandSaturation(a);
			yield return null;
		}
	}

	protected void SetIslandSaturation(float sat)
	{
		planet.ipsilon.mesh.material.SetFloat("_Saturation", sat);
		planet.tolbrynd.mesh.material.SetFloat("_Saturation", sat);
		planet.valfross.mesh.material.SetFloat("_Saturation", sat);
		planet.krolmar.mesh.material.SetFloat("_Saturation", sat);
		planet.ipsilon.solid.material.SetFloat("_Saturation", sat);
		planet.tolbrynd.solid.material.SetFloat("_Saturation", sat);
		planet.valfross.solid.material.SetFloat("_Saturation", sat);
		planet.krolmar.solid.material.SetFloat("_Saturation", sat);
	}

	protected void PlayImpact()
	{
		impact.Play();
	}

	protected void DisplayFlags(bool display)
	{
		for (int i = 0; i < flagsAndTrumpets.Length; i++)
		{
			if (display)
			{
				flagsAndTrumpets[i].Display();
			}
			else
			{
				flagsAndTrumpets[i].Disable();
			}
		}
	}

	protected void FadeFlavorText(float duration)
	{
		StartRoutine(this, flavorFade, IEFadeFlavorText(duration));
	}

	protected IEnumerator IEFadeFlavorText(float duration)
	{
		for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
		{
			ChangeAlphaForRenderer(alpha: t / duration, r: flavorText);
			yield return null;
		}
	}

	public void StopParticles()
	{
		StopParticles(fires);
		StopParticles(embers);
		StopParticles(smoke);
		StopParticles(particles);
	}

	public static void PlayParticles(ParticleSystem[] p)
	{
		for (int i = 0; i < p.Length; i++)
		{
			p[i].Play();
		}
	}

	public static void StopParticles(ParticleSystem[] p)
	{
		for (int i = 0; i < p.Length; i++)
		{
			p[i].Stop();
		}
	}

	public static void StartRoutine(MonoBehaviour instance, Coroutine routine, IEnumerator func)
	{
		if (routine != null)
		{
			instance.StopCoroutine(routine);
		}
		routine = instance.StartCoroutine(func);
	}

	public static void ChangeAlphaForRenderer(MeshRenderer r, float alpha)
	{
		Color color = r.material.color;
		r.material.color = new Color(color.r, color.g, color.b, alpha);
	}

	public static void ChangeAlphaForRenderer(MeshRenderer r, string s, float alpha)
	{
		Color color = r.material.GetColor(s);
		r.material.SetColor(s, new Color(color.r, color.g, color.b, alpha));
	}
}
