using System.Collections;
using Localisation;
using UnityEngine;

public class SettingsBarTextSizer : MonoBehaviour, ILocalisationAware
{
	public float minTextSize = 0.09f;

	public float maxTextSize = 0.11f;

	public TextMesh[] texts;

	public float[] sizes;

	public MeshRenderer exampleBG;

	private void Awake()
	{
		sizes = new float[texts.Length];
		for (int i = 0; i < texts.Length; i++)
		{
			sizes[i] = texts[i].characterSize;
		}
	}

	private void OnEnable()
	{
		StopAllCoroutines();
		StartCoroutine(UpdateSize());
	}

	public IEnumerator UpdateSize()
	{
		ResetSize();
		yield return new WaitForEndOfFrame();
		SetSize();
	}

	public void ResetSize()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			if (!(texts[i] == null))
			{
				texts[i].characterSize = sizes[i];
			}
		}
	}

	public void SetSize()
	{
		Vector2 vector = new Vector2(exampleBG.bounds.size.x * 0.85f, exampleBG.bounds.size.y * 0.5f);
		for (int i = 0; i < texts.Length; i++)
		{
			if (!(texts[i] == null))
			{
				MeshRenderer component = texts[i].GetComponent<MeshRenderer>();
				float num = vector.x - component.bounds.size.x;
				texts[i].characterSize = Mathf.Clamp((1f + num) * maxTextSize * 0.7f + minTextSize * 0.3f, minTextSize, maxTextSize);
				num = vector.y - component.bounds.size.y;
				texts[i].characterSize = Mathf.Clamp((1f + num * 1.1f) * maxTextSize, minTextSize, texts[i].characterSize);
			}
		}
	}

	public void OnLocalisationChange()
	{
		if (base.gameObject.activeInHierarchy)
		{
			StopAllCoroutines();
			StartCoroutine(UpdateSize());
		}
	}
}
