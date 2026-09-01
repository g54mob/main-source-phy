using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("UI/Tooltip")]
public class Tooltip : ClickBehaviour
{
	public Transform tooltipParent;

	public List<Renderer> tooltipRenderers = new List<Renderer>();

	public Dictionary<Renderer, Color> renOrgColors = new Dictionary<Renderer, Color>();

	private List<MaterialPropertyBlock> renProps = new List<MaterialPropertyBlock>();

	public Vector3 lerpPosDirection = -Vector3.right;

	public bool listenToStatMaster = true;

	public Collider[] myColliders = new Collider[0];

	public bool useDeltaTime;

	public float timeToProc;

	public float fadeSpeed = 0.15f;

	public bool fadeBlur;

	public float blurSize = 2.1f;

	public bool useFadeOut = true;

	private float lastMeshAlpha = 1f;

	private float lastTextAlpha = 1f;

	public int mask = -1;

	[NonSerialized]
	public bool isInitialized;

	[HideInInspector]
	public List<TextMesh> textMeshes = new List<TextMesh>();

	public Dictionary<GameObject, Color> textOrgColors = new Dictionary<GameObject, Color>();

	public List<MeshRenderer> textMeshRenderers = new List<MeshRenderer>();

	private List<MaterialPropertyBlock> textProps = new List<MaterialPropertyBlock>();

	private List<bool> useTextMaterial = new List<bool>();

	[HideInInspector]
	public List<DynamicText> dynamicTexts = new List<DynamicText>();

	public List<float> dynamicTextAlphas = new List<float>();

	public List<MeshRenderer> dynamicTextRenderers = new List<MeshRenderer>();

	[HideInInspector]
	public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	public List<float> spriteAlphas = new List<float>();

	private float timer;

	[HideInInspector]
	public Vector3 tooltipParentStartPos;

	[HideInInspector]
	public List<Color> textMeshOffColours = new List<Color>();

	private Color textOnCol;

	private Color textOffCol;

	private float textAlpha;

	private float percent;

	private bool on;

	private bool hasBackground;

	private MeshRenderer backgroundMeshRenderer;

	private MeshRenderer arrowMeshRenderer;

	private float leftBorderOffset;

	private bool isDestroyed;

	private bool leftAligned;

	private bool arrowHorizontallyAligned;

	private bool initialised;

	public MeshRenderer Background
	{
		get
		{
			return (!hasBackground) ? FindBackground() : backgroundMeshRenderer;
		}
	}

	public void Start()
	{
		if (!isInitialized)
		{
			if (timeToProc == 0f)
			{
				timeToProc = 0.25f;
			}
			isInitialized = true;
			Init();
		}
	}

	public void Reset()
	{
		Init(true);
	}

	protected virtual void Init(bool reset = false)
	{
		if (myColliders.Length >= 1)
		{
			for (int i = 0; i < myColliders.Length; i++)
			{
				myColliders[i].enabled = false;
			}
		}
		StopAllCoroutines();
		lastMeshAlpha = 1f;
		lastTextAlpha = 1f;
		tooltipRenderers.Clear();
		renProps.Clear();
		textMeshes.Clear();
		textMeshRenderers.Clear();
		dynamicTexts.Clear();
		dynamicTextAlphas.Clear();
		dynamicTextRenderers.Clear();
		spriteRenderers.Clear();
		spriteAlphas.Clear();
		textProps.Clear();
		useTextMaterial.Clear();
		tooltipParentStartPos = tooltipParent.localPosition;
		FindRenderers(tooltipParent);
		initialised = true;
		if (backgroundMeshRenderer != null && arrowMeshRenderer != null)
		{
			DetermineAlignment();
		}
		SetAllRenderersOff();
	}

	protected void FindRenderers(Transform parent, bool rec = false)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			DeactivateOnBase deactivateOnBase = child.GetComponentInParent<DeactivateOnBase>() ?? child.GetComponent<DeactivateOnBase>();
			if ((bool)deactivateOnBase && deactivateOnBase.Deactivated)
			{
				continue;
			}
			DynamicText component = child.GetComponent<DynamicText>();
			SpriteRenderer component2 = child.GetComponent<SpriteRenderer>();
			TextMesh component3 = child.GetComponent<TextMesh>();
			MeshRenderer component4 = child.GetComponent<MeshRenderer>();
			if ((bool)component)
			{
				dynamicTexts.Add(component);
				dynamicTextAlphas.Add(component.color.a);
				dynamicTextRenderers.Add(component4);
			}
			else if ((bool)component2)
			{
				spriteRenderers.Add(component2);
				spriteAlphas.Add(component2.color.a);
			}
			else if (component3 != null)
			{
				textMeshes.Add(component3);
				textMeshRenderers.Add(component4);
				textProps.Add(new MaterialPropertyBlock());
				bool flag = !component4.sharedMaterial.name.Contains("Font Material");
				useTextMaterial.Add(flag);
				if (flag)
				{
					if (!textOrgColors.ContainsKey(component4.gameObject))
					{
						textOrgColors.Add(component4.gameObject, component4.sharedMaterial.color);
					}
				}
				else if (!textOrgColors.ContainsKey(component3.gameObject))
				{
					textOrgColors.Add(component3.gameObject, component3.color);
				}
			}
			else if ((bool)component4)
			{
				MeshFilter component5 = component4.GetComponent<MeshFilter>();
				if (component5 != null)
				{
					if (!component5.gameObject.CompareTag("IgnoreTooltipScaling") && component5.mesh.name.Equals("Cube Instance"))
					{
						backgroundMeshRenderer = component4;
						hasBackground = true;
					}
					else if (!component5.gameObject.CompareTag("IgnoreTooltipScaling") && component5.mesh.name.Equals("default Instance"))
					{
						arrowMeshRenderer = component4;
					}
				}
				if (!tooltipRenderers.Contains(component4))
				{
					tooltipRenderers.Add(component4);
					renProps.Add(new MaterialPropertyBlock());
					Color color = component4.sharedMaterial.GetColor("_TintColor");
					if (!renOrgColors.ContainsKey(tooltipRenderers[tooltipRenderers.Count - 1]))
					{
						renOrgColors.Add(tooltipRenderers[tooltipRenderers.Count - 1], color);
					}
				}
			}
			FindRenderers(child, true);
		}
	}

	protected MeshRenderer FindBackground()
	{
		MeshFilter[] componentsInChildren = tooltipParent.GetComponentsInChildren<MeshFilter>();
		foreach (MeshFilter meshFilter in componentsInChildren)
		{
			if (meshFilter != null && !meshFilter.gameObject.CompareTag("IgnoreTooltipScaling") && meshFilter.mesh.name.Equals("Cube Instance"))
			{
				backgroundMeshRenderer = meshFilter.GetComponent<MeshRenderer>();
				hasBackground = true;
			}
		}
		return backgroundMeshRenderer;
	}

	private void DetermineAlignment()
	{
		leftAligned = arrowMeshRenderer.bounds.center.x > backgroundMeshRenderer.bounds.center.x;
		float num = arrowMeshRenderer.bounds.center.y - backgroundMeshRenderer.bounds.center.y;
		if (num < -0.1f || num > 0.1f)
		{
			arrowHorizontallyAligned = false;
		}
		else
		{
			arrowHorizontallyAligned = true;
		}
	}

	private void OnDestroy()
	{
		isDestroyed = true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SetAllRenderersOff();
	}

	private Bounds GetContentBounds()
	{
		Bounds result = new Bounds(backgroundMeshRenderer.bounds.center, Vector3.zero);
		List<Renderer> list = new List<Renderer>(tooltipRenderers);
		list.AddRange(((IEnumerable<MeshRenderer>)textMeshRenderers).Select((Func<MeshRenderer, Renderer>)((MeshRenderer x) => x)));
		list.AddRange(spriteRenderers.Cast<Renderer>());
		list.AddRange(dynamicTextRenderers.Cast<Renderer>());
		foreach (Renderer item in list)
		{
			if (item.gameObject.active && !item.Equals(backgroundMeshRenderer) && !item.Equals(arrowMeshRenderer) && !item.bounds.size.Equals(Vector3.zero) && !item.gameObject.CompareTag("IgnoreTooltipScaling"))
			{
				result.Encapsulate(item.bounds);
			}
		}
		return result;
	}

	private void ResizeBackground()
	{
		if (!(backgroundMeshRenderer == null) && !(arrowMeshRenderer == null) && (textMeshes.Count != 0 || dynamicTextRenderers.Count != 0) && tooltipRenderers.Count >= 2)
		{
			Bounds bounds = backgroundMeshRenderer.bounds;
			Bounds contentBounds = GetContentBounds();
			Bounds bounds2 = arrowMeshRenderer.bounds;
			leftBorderOffset = bounds2.center.x - ((!(bounds.center.x > bounds2.center.x)) ? bounds.max.x : bounds.min.x);
			float num = (contentBounds.size.x + 0.3f) / bounds.size.x;
			Vector3 localScale = backgroundMeshRenderer.transform.localScale;
			if (num > 1f)
			{
				localScale.x *= num;
			}
			backgroundMeshRenderer.transform.localScale = localScale;
			RepositionTooltip();
		}
	}

	private void RepositionTooltip()
	{
		float x = backgroundMeshRenderer.transform.localPosition.x;
		if (arrowHorizontallyAligned)
		{
			float x2 = arrowMeshRenderer.bounds.min.x - backgroundMeshRenderer.bounds.extents.x;
			if (!leftAligned)
			{
				x2 = arrowMeshRenderer.bounds.max.x + backgroundMeshRenderer.bounds.extents.x;
			}
			backgroundMeshRenderer.transform.position = arrowMeshRenderer.bounds.center.WithX(x2);
		}
		else
		{
			float num = arrowMeshRenderer.bounds.center.x - ((!(backgroundMeshRenderer.bounds.center.x > arrowMeshRenderer.bounds.center.x)) ? backgroundMeshRenderer.bounds.max.x : backgroundMeshRenderer.bounds.min.x);
			float x3 = num - leftBorderOffset;
			backgroundMeshRenderer.transform.localPosition += Vector3.zero.WithX(x3);
		}
		float x4 = backgroundMeshRenderer.transform.localPosition.x;
		float x5 = x4 - x;
		for (int i = 0; i < tooltipParent.childCount; i++)
		{
			Transform child = tooltipParent.GetChild(i);
			if (!(child == arrowMeshRenderer.transform) && !(child == backgroundMeshRenderer.transform) && !child.CompareTag("IgnoreTooltipScaling"))
			{
				child.transform.localPosition += Vector3.zero.WithX(x5);
			}
		}
	}

	public void OnMouseEnter()
	{
		if (!base.enabled || !UIMask.InsideMask(mask, base.transform.position))
		{
			TooltipOff();
		}
		else
		{
			timer += Time.unscaledDeltaTime;
		}
	}

	public void OnMouseExit()
	{
		TooltipOff();
	}

	public override void OnCursorOver()
	{
		if (!base.enabled || !UIMask.InsideMask(mask, base.transform.position))
		{
			TooltipOff();
			return;
		}
		if (!InputManager.LeftMouseButtonHeld() && timer >= timeToProc && (!listenToStatMaster || OptionsMaster.BesiegeConfig.Tooltips))
		{
			TooltipOn();
		}
		timer += Time.unscaledDeltaTime;
		base.OnCursorOver();
	}

	public override void OnClicked()
	{
		TooltipOff();
	}

	private void TooltipOff()
	{
		timer = 0f;
		if (!on)
		{
			return;
		}
		on = false;
		StopAllCoroutines();
		if (myColliders.Length >= 1)
		{
			for (int i = 0; i < myColliders.Length; i++)
			{
				myColliders[i].enabled = false;
			}
		}
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (useFadeOut)
		{
			if (base.enabled)
			{
				StartCoroutine(FadeOthers(false));
				StartCoroutine(FadeTextTo(0f));
				StartCoroutine(FadeRenTo(0f));
			}
		}
		else
		{
			SetAllRenderersOff();
		}
	}

	private void TooltipOn()
	{
		if (on)
		{
			return;
		}
		on = true;
		StopAllCoroutines();
		if (myColliders.Length >= 1)
		{
			for (int i = 0; i < myColliders.Length; i++)
			{
				myColliders[i].enabled = true;
			}
		}
		ResizeBackground();
		StartCoroutine(LerpPosIn());
		StartCoroutine(FadeOthers(true));
		StartCoroutine(FadeTextTo(1f));
		StartCoroutine(FadeRenTo(1f));
	}

	private IEnumerator FadeRenTo(float a)
	{
		for (int i = 0; i < tooltipRenderers.Count; i++)
		{
			tooltipRenderers[i].enabled = true;
		}
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		float startA = percent;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime && !StatMaster.isMainMenu) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.unscaledDeltaTime * rate));
			percent = Mathf.Lerp(startA, a, cTime);
			for (int j = 0; j < tooltipRenderers.Count; j++)
			{
				Renderer currentRenderer = tooltipRenderers[j];
				if (!currentRenderer)
				{
					Debug.Log("renderer null");
					continue;
				}
				Color c = currentRenderer.sharedMaterial.GetColor("_TintColor");
				Color defaultColor;
				renOrgColors.TryGetValue(currentRenderer, out defaultColor);
				renProps[j].SetColor("_TintColor", new Color(c.r, c.g, c.b, percent * defaultColor.a));
				if (fadeBlur && currentRenderer.sharedMaterial.HasProperty("_Size"))
				{
					renProps[j].SetFloat("_Size", Mathf.Lerp((!(a < percent)) ? 0.5f : currentRenderer.sharedMaterial.GetFloat("_Size"), (!(a < percent)) ? blurSize : 0.5f, percent));
				}
				currentRenderer.SetPropertyBlock(renProps[j]);
			}
			lastMeshAlpha = percent;
			yield return null;
		}
		if (a != 0f)
		{
			yield break;
		}
		foreach (Renderer ren in tooltipRenderers)
		{
			ren.enabled = false;
		}
	}

	private IEnumerator LerpPosIn()
	{
		float cTime = percent;
		float rate = 1f / fadeSpeed;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime && !StatMaster.isMainMenu) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.unscaledDeltaTime * rate));
			tooltipParent.localPosition = Vector3.Lerp(tooltipParentStartPos - lerpPosDirection, tooltipParentStartPos, cTime);
			yield return null;
		}
	}

	private IEnumerator FadeTextTo(float a)
	{
		foreach (MeshRenderer ren in textMeshRenderers)
		{
			ren.enabled = true;
		}
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		float startA = textAlpha;
		while (cTime < 1f)
		{
			cTime = ((!useDeltaTime && !StatMaster.isMainMenu) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.unscaledDeltaTime * rate));
			textAlpha = Mathf.Lerp(startA, a, cTime);
			for (int i = 0; i < textMeshRenderers.Count; i++)
			{
				Color org = textOrgColors[textMeshRenderers[i].gameObject];
				if (useTextMaterial[i])
				{
					textProps[i].SetColor("_Color", new Color(org.r, org.g, org.b, textAlpha * org.a));
					textMeshRenderers[i].SetPropertyBlock(textProps[i]);
				}
				else
				{
					textMeshes[i].color = new Color(org.r, org.g, org.b, textAlpha * org.a);
				}
			}
			lastTextAlpha = textAlpha;
			yield return null;
		}
		if (a != 0f)
		{
			yield break;
		}
		foreach (MeshRenderer ren2 in textMeshRenderers)
		{
			ren2.enabled = false;
		}
	}

	private IEnumerator FadeOthers(bool toActive)
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.unscaledDeltaTime / fadeSpeed;
			float alpha = Mathf.Lerp(0f, 1f, (!toActive) ? (1f - time) : time);
			SetDynamicTextsAlpha(alpha);
			SetSpriteRenderersAlpha(alpha);
			yield return null;
		}
	}

	public void SetAllRenderersOn()
	{
		if (initialised)
		{
			on = true;
			SetAllRenderersToAlpha(1f);
		}
	}

	public void SetAllRenderersOff()
	{
		if (initialised)
		{
			on = false;
			SetAllRenderersToAlpha(0f);
		}
	}

	private void SetAllRenderersToAlpha(float alpha)
	{
		if (isDestroyed)
		{
			return;
		}
		percent = alpha;
		timer = 0f;
		if (alpha != lastMeshAlpha)
		{
			for (int i = 0; i < tooltipRenderers.Count; i++)
			{
				if (!(tooltipRenderers[i] == null))
				{
					Renderer renderer = tooltipRenderers[i];
					renderer.enabled = false;
					Color color = renderer.sharedMaterial.GetColor("_TintColor");
					renProps[i].SetColor("_TintColor", new Color(color.r, color.g, color.b, percent * renOrgColors[tooltipRenderers[i]].a));
					renderer.SetPropertyBlock(renProps[i]);
				}
			}
			lastMeshAlpha = alpha;
		}
		if (alpha != lastTextAlpha)
		{
			for (int j = 0; j < textMeshes.Count; j++)
			{
				if (!(textMeshes[j] == null))
				{
					Renderer renderer2 = textMeshRenderers[j];
					renderer2.enabled = ((alpha > 0f) ? true : false);
					if (useTextMaterial[j])
					{
						Color color = textOrgColors[renderer2.gameObject];
						textProps[j].SetColor("_Color", new Color(color.r, color.g, color.b, alpha * textOrgColors[renderer2.gameObject].a));
						textMeshRenderers[j].SetPropertyBlock(textProps[j]);
					}
					else
					{
						TextMesh textMesh = textMeshes[j];
						Color color = textMesh.color;
						textMesh.color = new Color(color.r, color.g, color.b, textOrgColors[textMesh.gameObject].a * alpha);
					}
				}
			}
			lastTextAlpha = alpha;
		}
		SetDynamicTextsAlpha(alpha);
		SetSpriteRenderersAlpha(alpha);
		if (tooltipParent != null)
		{
			if (!tooltipParent.gameObject.activeSelf)
			{
				tooltipParent.gameObject.SetActive(true);
			}
			if (isInitialized)
			{
				tooltipParent.localPosition = tooltipParentStartPos;
			}
		}
	}

	public void SetSpecificTextMeshColor(Color c, int index, bool keepAlpha)
	{
		if (keepAlpha)
		{
			c.a = textOrgColors[textMeshRenderers[index].gameObject].a;
		}
		textOrgColors[textMeshRenderers[index].gameObject] = c;
		if (useTextMaterial[index])
		{
			textProps[index].SetColor("_Color", c);
			textMeshRenderers[index].SetPropertyBlock(textProps[index]);
		}
		else
		{
			textMeshes[index].color = c;
		}
	}

	public void ResetSpecificTextMeshColor(int index)
	{
		Color color = textMeshRenderers[index].sharedMaterial.color;
		textOrgColors[textMeshRenderers[index].gameObject] = color;
		if (useTextMaterial[index])
		{
			textProps[index].SetColor("_Color", color);
			textMeshRenderers[index].SetPropertyBlock(textProps[index]);
		}
		else
		{
			textMeshes[index].color = color;
		}
	}

	private void SetDynamicTextsAlpha(float alpha)
	{
		for (int i = 0; i < dynamicTexts.Count; i++)
		{
			DynamicText dynamicText = dynamicTexts[i];
			MeshRenderer meshRenderer = dynamicTextRenderers[i];
			Color color = dynamicText.color;
			dynamicText.color = new Color(color.r, color.g, color.b, dynamicTextAlphas[i] * alpha);
			bool flag = (dynamicText.enabled = (double)alpha > 1E-06);
			meshRenderer.enabled = flag;
		}
	}

	private void SetSpriteRenderersAlpha(float alpha)
	{
		for (int i = 0; i < spriteRenderers.Count; i++)
		{
			SpriteRenderer spriteRenderer = spriteRenderers[i];
			Color color = spriteRenderer.color;
			spriteRenderer.color = new Color(color.r, color.g, color.b, spriteAlphas[i] * alpha);
			spriteRenderer.enabled = (double)alpha > 1E-06;
		}
	}
}
