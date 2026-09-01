using System.Collections;
using UnityEngine;

public class OverwriteButton : SimpleUIButton
{
	[SerializeField]
	private TextMesh[] textMeshes;

	[SerializeField]
	private float fadeTime = 0.15f;

	private bool _isShown;

	private BoxCollider collider;

	public bool IsShown
	{
		get
		{
			return _isShown;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		collider = GetComponent<BoxCollider>();
		Hide(true);
	}

	public void Show()
	{
		_isShown = true;
		collider.enabled = true;
		Fade(1f);
	}

	public void Hide()
	{
		Hide(true);
	}

	private void Hide(bool instant)
	{
		_isShown = false;
		collider.enabled = false;
		if (instant)
		{
			iTween.Stop(base.gameObject, true);
			SetTextMeshesAlpha(0f);
		}
		else
		{
			Fade(0f);
		}
	}

	private void Fade(float alpha)
	{
		iTween.Stop(base.gameObject, true);
		Hashtable args = iTween.Hash("alpha", alpha, "time", fadeTime, "ignoretimescale", true);
		TextMesh[] array = textMeshes;
		foreach (TextMesh textMesh in array)
		{
			iTween.FadeTo(textMesh.gameObject, args);
		}
	}

	private void SetTextMeshesAlpha(float alpha)
	{
		TextMesh[] array = textMeshes;
		foreach (TextMesh textMesh in array)
		{
			Renderer component = textMesh.GetComponent<Renderer>();
			component.material.color = new Color(component.material.color.r, component.material.color.g, component.material.color.b, alpha);
		}
	}
}
