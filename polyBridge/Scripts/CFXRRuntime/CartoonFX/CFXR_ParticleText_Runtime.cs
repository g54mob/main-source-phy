using System;
using UnityEngine;

namespace CartoonFX
{
	[RequireComponent(typeof(ParticleSystem))]
	public class CFXR_ParticleText_Runtime : MonoBehaviour
	{
		[Header("Text")]
		public string text;

		public float size = 1f;

		public float letterSpacing = 0.44f;

		[Header("Colors")]
		public Color backgroundColor = new Color(0f, 0f, 0f, 1f);

		public Color color1 = new Color(1f, 1f, 1f, 1f);

		public Color color2 = new Color(0f, 0f, 1f, 1f);

		[Header("Delay")]
		public float delay = 0.05f;

		public bool cumulativeDelay;

		[Range(0f, 2f)]
		public float compensateLifetime;

		[Header("Misc")]
		public float lifetimeMultiplier = 1f;

		[Range(-90f, 90f)]
		public float rotation = -5f;

		public float sortingFudgeOffset = 0.1f;

		public CFXR_ParticleTextFontAsset font;

		private float baseLifetime;

		private float baseScaleX;

		private float baseScaleY;

		private float baseScaleZ;

		private Vector3 basePivot;

		private void Awake()
		{
			InitializeFirstParticle();
		}

		public void InitializeFirstParticle()
		{
			if (base.transform.childCount == 0)
			{
				Debug.LogError("CFXR_ParticleText_Runtime requires a child with a Particle System component to act as the model for other letters.");
				return;
			}
			ParticleSystem component = base.transform.GetChild(0).GetComponent<ParticleSystem>();
			baseLifetime = component.main.startLifetime.constant;
			baseScaleX = component.main.startSizeXMultiplier;
			baseScaleY = component.main.startSizeYMultiplier;
			baseScaleZ = component.main.startSizeZMultiplier;
			basePivot = component.GetComponent<ParticleSystemRenderer>().pivot;
		}

		public void GenerateText(string text)
		{
			if (text == null || font == null || !font.IsValid())
			{
				return;
			}
			if (base.transform.childCount == 0)
			{
				Debug.LogError("CFXR_ParticleText_Runtime requires a child with a Particle System component to act as the model for other letters.");
				return;
			}
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (char.IsWhiteSpace(text[i]))
				{
					if (i > 0)
					{
						num += letterSpacing * size;
					}
					continue;
				}
				num2++;
				if (i > 0)
				{
					int num3 = font.CharSequence.IndexOf(text[i]);
					float num4 = font.CharSprites[num3].rect.width + font.CharKerningOffsets[num3].post + font.CharKerningOffsets[num3].pre;
					num += (num4 * 0.01f + letterSpacing) * size;
				}
			}
			int num5 = base.transform.childCount - 1;
			if (num5 < num2)
			{
				GameObject original = base.transform.GetChild(0).gameObject;
				for (int j = num5; j < num2; j++)
				{
					GameObject obj = UnityEngine.Object.Instantiate(original);
					obj.transform.SetParent(base.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localRotation = Quaternion.identity;
				}
			}
			float num6 = num / 2f;
			num = 0f;
			int num7 = 0;
			for (int k = 0; k < text.Length; k++)
			{
				char c = text[k];
				if (char.IsWhiteSpace(c))
				{
					num += letterSpacing * size;
					continue;
				}
				num7++;
				int num8 = font.CharSequence.IndexOf(text[k]);
				Sprite sprite = font.CharSprites[num8];
				float num9 = size * sprite.rect.width / 50f;
				num += font.CharKerningOffsets[num8].pre * 0.01f * size;
				float num10 = (num - num6) / num9;
				float num11 = sprite.rect.width + font.CharKerningOffsets[num8].post;
				num += (num11 * 0.01f + letterSpacing) * size;
				GameObject obj2 = base.transform.GetChild(num7).gameObject;
				obj2.name = c.ToString();
				ParticleSystem component = obj2.GetComponent<ParticleSystem>();
				ParticleSystem.MainModule main = component.main;
				main.startSizeXMultiplier = baseScaleX * num9;
				main.startSizeYMultiplier = baseScaleY * num9;
				main.startSizeZMultiplier = baseScaleZ * num9;
				component.textureSheetAnimation.SetSprite(0, sprite);
				main.startRotation = MathF.PI / 180f * rotation;
				main.startColor = backgroundColor;
				ParticleSystem.CustomDataModule customData = component.customData;
				customData.enabled = true;
				customData.SetColor(ParticleSystemCustomData.Custom1, color1);
				customData.SetColor(ParticleSystemCustomData.Custom2, color2);
				if (cumulativeDelay)
				{
					main.startDelay = delay * (float)k;
					main.startLifetime = Mathf.LerpUnclamped(baseLifetime, baseLifetime + delay * (float)(text.Length - k), compensateLifetime);
				}
				else
				{
					main.startDelay = delay;
				}
				main.startLifetime = main.startLifetime.constant * lifetimeMultiplier;
				ParticleSystemRenderer component2 = component.GetComponent<ParticleSystemRenderer>();
				component2.enabled = true;
				component2.pivot = new Vector3(basePivot.x + num10, basePivot.y, basePivot.z);
				component2.sortingFudge += (float)k * sortingFudgeOffset;
			}
			int l = 1;
			for (int childCount = base.transform.childCount; l < childCount; l++)
			{
				base.transform.GetChild(l).gameObject.SetActive(l <= num2);
			}
			GetComponent<ParticleSystem>().Play(withChildren: true);
		}
	}
}
