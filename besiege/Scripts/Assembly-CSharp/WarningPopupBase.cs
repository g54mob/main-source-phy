using System.Collections;
using UnityEngine;

public class WarningPopupBase : ClickBehaviour
{
	public Transform parentObj;

	public Renderer[] rendys;

	public TextMesh textMeshy;

	public float duration = 1.5f;

	public float fadeSpeed = 0.15f;

	public bool playAudio;

	public Vector3 lerpPosDirection = -Vector3.right;

	protected BoxCollider boxCollider;

	protected bool hasCollider;

	protected float textAlpha;

	protected float renAlpha;

	protected bool on;

	protected Color[] onCols;

	protected Vector3 parentObjStartPos;

	protected Color textOnCol;

	public Vector3 startPosy;

	protected virtual void Awake()
	{
		onCols = new Color[rendys.Length];
		boxCollider = GetComponent<BoxCollider>();
		hasCollider = boxCollider != null;
		for (int i = 0; i < rendys.Length; i++)
		{
			onCols[i] = rendys[i].material.GetColor("_TintColor");
		}
		MeshRenderer component = textMeshy.GetComponent<MeshRenderer>();
		if (!component.material.name.StartsWith("Font Material") && !component.material.name.EndsWith("Font Material"))
		{
			textOnCol = component.material.color;
		}
		else
		{
			textOnCol = textMeshy.color;
		}
	}

	protected virtual void Start()
	{
		parentObjStartPos = parentObj.localPosition;
		SetAllRenderersOff();
	}

	protected void ShowWarning()
	{
		StopAllCoroutines();
		StartCoroutine(DoIt());
		if (playAudio)
		{
			GetComponent<AudioSource>().Play();
		}
	}

	protected virtual IEnumerator DoIt()
	{
		yield return StartCoroutine(WarningOn());
		yield return new WaitForSeconds(duration);
		WarningOff();
	}

	protected virtual void WarningOff()
	{
		if (on)
		{
			on = false;
			if (base.gameObject.activeSelf && base.enabled)
			{
				StartCoroutine(FadeTextTo(0f));
				StartCoroutine(FadeRenTo(0f));
			}
		}
	}

	protected virtual IEnumerator WarningOn()
	{
		if (!on)
		{
			on = true;
			StartCoroutine(FadeTextTo(1f));
			StartCoroutine(FadeRenTo(1f));
			yield return StartCoroutine(LerpPosIn());
		}
	}

	protected IEnumerator LerpPosIn()
	{
		float cTime = renAlpha;
		float rate = 1f / fadeSpeed;
		startPosy = parentObj.localPosition;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			parentObj.localPosition = Vector3.Lerp(startPosy - lerpPosDirection, parentObjStartPos, cTime);
			yield return null;
		}
	}

	protected IEnumerator LerpPosOut()
	{
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		startPosy = parentObj.localPosition;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			parentObj.localPosition = Vector3.Lerp(startPosy, parentObjStartPos - lerpPosDirection, cTime);
			yield return null;
		}
	}

	protected virtual IEnumerator FadeRenTo(float a)
	{
		if (a != 0f)
		{
			parentObj.gameObject.SetActive(true);
			if (hasCollider)
			{
				boxCollider.enabled = true;
			}
		}
		for (int i = 0; i < rendys.Length; i++)
		{
			rendys[i].enabled = true;
		}
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		float startA = renAlpha;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			renAlpha = Mathf.Lerp(startA, a, cTime);
			for (int j = 0; j < rendys.Length; j++)
			{
				rendys[j].material.SetColor("_TintColor", new Color(onCols[j].r, onCols[j].g, onCols[j].b, renAlpha * onCols[j].a));
			}
			yield return null;
		}
		if (a == 0f)
		{
			Renderer[] array = rendys;
			foreach (Renderer ren in array)
			{
				ren.enabled = false;
			}
			parentObj.gameObject.SetActive(false);
			if (hasCollider)
			{
				boxCollider.enabled = false;
			}
		}
	}

	protected virtual IEnumerator FadeTextTo(float a)
	{
		Renderer ren = textMeshy.GetComponent<Renderer>();
		ren.enabled = true;
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		float startA = textAlpha;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			textAlpha = Mathf.Lerp(startA, a, cTime);
			if (!ren.material.name.StartsWith("Font Material") && !ren.material.name.EndsWith("Font Material"))
			{
				ren.material.color = new Color(textOnCol.r, textOnCol.g, textOnCol.b, textAlpha);
			}
			else
			{
				textMeshy.color = new Color(textOnCol.r, textOnCol.g, textOnCol.b, textAlpha);
			}
			yield return null;
		}
	}

	protected virtual void SetAllRenderersOff()
	{
		for (int i = 0; i < rendys.Length; i++)
		{
			rendys[i].enabled = false;
			Color color = onCols[i];
			rendys[i].material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 0f));
		}
		Renderer component = textMeshy.GetComponent<Renderer>();
		if (!component.material.name.StartsWith("Font Material") && !component.material.name.EndsWith("Font Material"))
		{
			component.material.color = new Color(textOnCol.r, textOnCol.g, textOnCol.b, 0f);
		}
		else
		{
			textMeshy.color = new Color(textOnCol.r, textOnCol.g, textOnCol.b, 0f);
		}
		component.enabled = false;
	}
}
