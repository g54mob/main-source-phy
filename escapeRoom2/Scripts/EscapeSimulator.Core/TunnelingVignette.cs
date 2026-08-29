using System.Collections.Generic;
using UnityEngine;

public class TunnelingVignette : MonoBehaviour
{
	public Dictionary<VignetteSource, float> amounts = new Dictionary<VignetteSource, float>();

	public float featheringEffect = 0.5f;

	public Color color = Color.black;

	public Color colorBlend = Color.black;

	private Renderer vignetteRenderer;

	private MaterialPropertyBlock propBlock;

	public static readonly int apertureSizeProp = Shader.PropertyToID("_ApertureSize");

	public static readonly int featheringEffectProp = Shader.PropertyToID("_FeatheringEffect");

	public static readonly int vignetteColorProp = Shader.PropertyToID("_VignetteColor");

	public static readonly int vignetteColorBlendProp = Shader.PropertyToID("_VignetteColorBlend");

	private void Awake()
	{
		vignetteRenderer = GetComponent<Renderer>();
		propBlock = new MaterialPropertyBlock();
		updateVignette(tween: false);
	}

	private void Update()
	{
		updateVignette(tween: true);
	}

	private void updateVignette(bool tween)
	{
		vignetteRenderer.GetPropertyBlock(propBlock);
		float num = 1f;
		if (amounts.Count > 0)
		{
			num = 1f - maxAmount();
		}
		if (tween)
		{
			setWithTween(apertureSizeProp, num);
			setWithTween(featheringEffectProp, featheringEffect);
			setWithTween(vignetteColorProp, color);
			setWithTween(vignetteColorBlendProp, colorBlend);
		}
		else
		{
			propBlock.SetFloat(apertureSizeProp, num);
			propBlock.SetFloat(featheringEffectProp, featheringEffect);
			propBlock.SetColor(vignetteColorProp, color);
			propBlock.SetColor(vignetteColorBlendProp, colorBlend);
		}
		vignetteRenderer.SetPropertyBlock(propBlock);
		float maxAmount()
		{
			float num2 = float.MinValue;
			foreach (KeyValuePair<VignetteSource, float> amount in amounts)
			{
				if (amount.Value > num2)
				{
					num2 = amount.Value;
				}
			}
			return num2;
		}
	}

	private void setWithTween(int propId, float target)
	{
		float current = propBlock.GetFloat(propId);
		propBlock.SetFloat(propId, Mathf.MoveTowards(current, target, Time.deltaTime * 10f));
	}

	private void setWithTween(int propId, Color target)
	{
		Color color = propBlock.GetColor(propId);
		propBlock.SetColor(propId, Vector4.MoveTowards(color, target, Time.deltaTime * 10f));
	}

	public void updateSource(VignetteSource source, float amount)
	{
		amounts[source] = amount;
	}

	public float getCurrentValue()
	{
		vignetteRenderer.GetPropertyBlock(propBlock);
		return propBlock.GetFloat(apertureSizeProp);
	}
}
