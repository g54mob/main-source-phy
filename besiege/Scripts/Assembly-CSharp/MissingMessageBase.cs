using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public abstract class MissingMessageBase : WarningPopupBase
{
	public GameObject BG;

	public GameObject listContainer;

	protected List<MeshRenderer> entryRens = new List<MeshRenderer>();

	protected List<MeshRenderer> entryTexts = new List<MeshRenderer>();

	protected bool paused;

	protected float extend;

	protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

	protected bool overlayOpen;

	protected Vector3 bgStartSize;

	protected void OnEnable()
	{
		if (SteamManager.Initialized)
		{
			m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
	{
		if (pCallback.m_bActive != 0)
		{
			overlayOpen = true;
		}
		else
		{
			overlayOpen = false;
		}
	}

	protected void OnApplicationFocus(bool focusStatus)
	{
		paused = !focusStatus;
	}

	protected virtual void Update()
	{
		if (paused || overlayOpen)
		{
			extend += Time.deltaTime;
		}
	}

	protected override void Awake()
	{
		extend = 0f;
		bgStartSize = BG.transform.localScale;
		base.Awake();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (SteamManager.Initialized)
		{
			m_GameOverlayActivated.Dispose();
		}
		m_GameOverlayActivated = null;
	}

	protected override void Start()
	{
		extend = 0f;
		base.Start();
	}

	protected override IEnumerator DoIt()
	{
		extend = 0f;
		yield return StartCoroutine(WarningOn());
		for (float i = 0f; i < duration + extend && !(i > duration * 3f); i += Time.deltaTime)
		{
			yield return null;
		}
		WarningOff();
	}

	protected override IEnumerator WarningOn()
	{
		extend = 0f;
		yield return StartCoroutine(base.WarningOn());
	}

	protected override void WarningOff()
	{
		extend = 0f;
		base.WarningOff();
	}

	protected override IEnumerator FadeRenTo(float a)
	{
		if (a != 0f)
		{
			parentObj.gameObject.SetActive(true);
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
			for (int k = 0; k < entryRens.Count; k++)
			{
				Color c = entryRens[k].material.GetColor("_TintColor");
				entryRens[k].material.SetColor("_TintColor", new Color(c.r, c.g, c.b, renAlpha));
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
		}
	}

	protected override IEnumerator FadeTextTo(float a)
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
			for (int k = 0; k < entryTexts.Count; k++)
			{
				Color c = entryTexts[k].material.color;
				entryTexts[k].material.color = new Color(c.r, c.g, c.b, textAlpha);
			}
			yield return null;
		}
	}

	protected override void SetAllRenderersOff()
	{
		base.SetAllRenderersOff();
		for (int i = 0; i < entryRens.Count; i++)
		{
			Color color = entryRens[i].material.GetColor("_TintColor");
			entryRens[i].material.SetColor("_TintColor", new Color(color.r, color.g, color.b, renAlpha));
		}
		for (int j = 0; j < entryTexts.Count; j++)
		{
			Color color = entryTexts[j].material.color;
			entryTexts[j].material.color = new Color(color.r, color.g, color.b, 0f);
		}
		parentObj.gameObject.SetActive(false);
	}

	public virtual bool Push(bool push)
	{
		return false;
	}
}
