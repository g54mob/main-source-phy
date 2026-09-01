using UnityEngine;

public class GlowLerpEmiss : MonoBehaviour
{
	public Renderer rendy;

	public float lerpSpeed = 1f;

	public Color glowCol;

	public float glowTimer;

	public float lerpedGlowAmount;

	public string colToSet = "_Emission";

	private Color startColEmiss;

	private Color startColMain;

	private Color colToBeEmiss;

	private Color colToBeMain;

	private Color myCol;

	private float lerpedGlowAmountToBe;

	private Material renderMaterial;

	private Color lastMaterialColor;

	private Color lastEmissColor;

	private VisibilityTracker visibilityTracker;

	private bool isVisible = true;

	private void Start()
	{
		SetupGlow();
	}

	private void SetupGlow()
	{
		renderMaterial = rendy.material;
		if (renderMaterial.HasProperty(colToSet))
		{
			startColEmiss = renderMaterial.GetColor(colToSet);
		}
		startColMain = renderMaterial.color;
		lastMaterialColor = startColMain;
		lastEmissColor = startColEmiss;
		visibilityTracker = rendy.gameObject.GetComponent<VisibilityTracker>();
		if (visibilityTracker == null)
		{
			visibilityTracker = rendy.gameObject.AddComponent<VisibilityTracker>();
		}
		visibilityTracker.onVisibilityChanged = OnToggleVisibility;
	}

	private void Glow()
	{
		glowTimer += Time.deltaTime * 10f;
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		glowTimer -= Time.deltaTime;
		glowTimer = Mathf.Clamp01(glowTimer);
		if (glowTimer > 0f)
		{
			colToBeEmiss = glowCol;
			colToBeMain = Color.black;
			lerpedGlowAmountToBe = 1f;
		}
		else
		{
			colToBeEmiss = startColEmiss;
			colToBeMain = startColMain;
			lerpedGlowAmountToBe = 0f;
		}
		float t = Time.deltaTime * lerpSpeed;
		if (lerpedGlowAmount != lerpedGlowAmountToBe)
		{
			lerpedGlowAmount = Mathf.Lerp(lerpedGlowAmount, lerpedGlowAmountToBe, t);
		}
		if (!isVisible)
		{
			return;
		}
		renderMaterial = rendy.material;
		if (lastMaterialColor != colToBeMain)
		{
			Color color = Color.Lerp(lastMaterialColor, colToBeMain, t);
			renderMaterial.color = color;
			lastMaterialColor = color;
		}
		if (lastEmissColor != colToBeEmiss)
		{
			Color color2 = Color.Lerp(lastEmissColor, colToBeEmiss, t);
			if (renderMaterial.HasProperty(colToSet))
			{
				renderMaterial.SetColor(colToSet, color2);
			}
			lastEmissColor = color2;
		}
	}

	private void OnToggleVisibility(bool toggle)
	{
		isVisible = toggle;
	}
}
