using System;
using System.Collections;
using UnityEngine;

public class GlobeFadeController : MonoBehaviour
{
	[Serializable]
	public class Island
	{
		public MeshRenderer mesh;

		public MeshRenderer solid;

		public MeshRenderer[] monuments = new MeshRenderer[0];

		public MeshRenderer[] foliage = new MeshRenderer[0];
	}

	[SerializeField]
	public Island ipsilon;

	[SerializeField]
	public Island tolbrynd;

	[SerializeField]
	public Island valfross;

	[SerializeField]
	public Island krolmar;

	[SerializeField]
	protected MeshRenderer water;

	protected float globalAlpha;

	protected Coroutine fader;

	public void Fade(float alpha, float duration)
	{
		base.gameObject.SetActive(true);
		if (fader != null)
		{
			StopCoroutine(fader);
		}
		fader = StartCoroutine(IEFade(alpha, duration));
	}

	public void Disable()
	{
		if (fader != null)
		{
			StopCoroutine(fader);
		}
		globalAlpha = 0f;
		SetIslandAlpha(ipsilon, globalAlpha);
		SetIslandAlpha(tolbrynd, globalAlpha);
		SetIslandAlpha(valfross, globalAlpha);
		SetIslandAlpha(krolmar, globalAlpha);
		ChangeAlphaForRenderer(water, globalAlpha);
		base.gameObject.SetActive(false);
	}

	protected IEnumerator IEFade(float alpha, float duration)
	{
		float start = globalAlpha;
		for (float t = 0f; t <= duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			globalAlpha = Mathf.Lerp(start, alpha, pct);
			SetIslandAlpha(ipsilon, globalAlpha);
			SetIslandAlpha(tolbrynd, globalAlpha);
			SetIslandAlpha(valfross, globalAlpha);
			SetIslandAlpha(krolmar, globalAlpha);
			ChangeAlphaForRenderer(water, globalAlpha);
			yield return null;
		}
		fader = null;
	}

	protected void SetIslandAlpha(Island island, float alpha)
	{
		ChangeAlphaForRenderer(island.mesh, alpha);
		for (int i = 0; i < island.monuments.Length; i++)
		{
			ChangeAlphaForRenderer(island.monuments[i], alpha);
		}
		for (int j = 0; j < island.foliage.Length; j++)
		{
			ChangeAlphaForRenderer(island.foliage[j], alpha);
		}
	}

	protected void ChangeAlphaForRenderer(MeshRenderer r, float alpha)
	{
		Color color = r.material.color;
		r.material.color = new Color(color.r, color.g, color.b, alpha);
	}
}
