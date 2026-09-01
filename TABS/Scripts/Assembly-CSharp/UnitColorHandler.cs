using System.Collections;
using System.Collections.Generic;
using TFBGames;
using UnityEngine;

public class UnitColorHandler : MonoBehaviour
{
	public bool isDone;

	public List<UnitColorInstance> colors = new List<UnitColorInstance>();

	public Color effectColor = Color.black;

	public float totalValue;

	private bool initiated;

	private Renderer[] rends;

	private MaterialPropertyBlock[] propertyBlocks;

	private Color[][] rendColors;

	private WaitForSeconds waitOneSecond = new WaitForSeconds(1f);

	private List<Material> mats = new List<Material>();

	private void Init()
	{
		initiated = true;
		rends = base.transform.root.GetComponentsInChildren<Renderer>();
		List<Renderer> list = new List<Renderer>();
		for (int i = 0; i < rends.Length; i++)
		{
			if (!rends[i].GetComponent<ParticleSystem>())
			{
				list.Add(rends[i]);
			}
		}
		rends = list.ToArray();
		propertyBlocks = new MaterialPropertyBlock[rends.Length];
		rendColors = new Color[rends.Length][];
		for (int j = 0; j < rendColors.Length; j++)
		{
			Material[] sharedMaterialsNonAlloc = rends[j].GetSharedMaterialsNonAlloc();
			bool flag = true;
			rendColors[j] = new Color[sharedMaterialsNonAlloc.Length];
			for (int k = 0; k < rendColors[j].Length; k++)
			{
				Material material = sharedMaterialsNonAlloc[k];
				if (!(material == null))
				{
					if (material.shader.name != "TFBG/SimpleVertexColorUnit" && material.shader.name != "TFBG/SimpleTintDiffuseUnit" && material.shader.name != "TFBG/VertexColorNormalUnit")
					{
						flag = false;
					}
					if (material.HasProperty("_Color"))
					{
						rendColors[j][k] = material.color;
					}
				}
			}
			if (flag)
			{
				propertyBlocks[j] = new MaterialPropertyBlock();
			}
		}
	}

	public void SetMaterial(Material material)
	{
		if (!initiated)
		{
			Init();
		}
		isDone = true;
		for (int i = 0; i < rends.Length; i++)
		{
			Material[] materialsNonAlloc = rends[i].GetMaterialsNonAlloc();
			for (int j = 0; j < materialsNonAlloc.Length; j++)
			{
				Material material2 = materialsNonAlloc[j];
				if (material2.HasProperty("_BumpMap"))
				{
					Texture texture = material2.GetTexture("_BumpMap");
					if ((bool)texture)
					{
						Material material3 = new Material(material);
						material3.SetTexture("_BumpMap", texture);
						material3.EnableKeyword("_NORMALMAP");
						materialsNonAlloc[j] = material3;
						continue;
					}
				}
				materialsNonAlloc[j] = material;
			}
			rends[i].materials = materialsNonAlloc;
		}
	}

	public void ForceReInitialize()
	{
		initiated = false;
	}

	public void SetColor(UnitColorInstance color, float currentValue)
	{
		if (isDone)
		{
			return;
		}
		if (!initiated)
		{
			Init();
		}
		bool flag = false;
		for (int i = 0; i < colors.Count; i++)
		{
			if (colors[i].colorName == color.colorName)
			{
				colors[i].currentValue = currentValue;
				colors[i].color = color.color;
				flag = true;
			}
		}
		if (!flag)
		{
			UnitColorInstance unitColorInstance = new UnitColorInstance();
			unitColorInstance.currentValue = currentValue;
			unitColorInstance.colorName = color.colorName;
			unitColorInstance.color = color.color;
			colors.Add(unitColorInstance);
		}
		UpdateColors();
	}

	public void UpdateColors()
	{
		if (!initiated)
		{
			return;
		}
		UpdateEffectColor();
		for (int i = 0; i < rends.Length; i++)
		{
			Renderer renderer = rends[i];
			if (!renderer)
			{
				continue;
			}
			MaterialPropertyBlock materialPropertyBlock = propertyBlocks[i];
			if (materialPropertyBlock == null)
			{
				Material[] materialsNonAlloc = rends[i].GetMaterialsNonAlloc();
				for (int j = 0; j < materialsNonAlloc.Length; j++)
				{
					if (materialsNonAlloc[j].HasProperty("_Color"))
					{
						materialsNonAlloc[j].color = Color.Lerp(rendColors[i][j], effectColor, Mathf.Clamp(totalValue, 0f, 0.9f));
					}
				}
			}
			else
			{
				renderer.GetPropertyBlock(materialPropertyBlock);
				materialPropertyBlock.SetColor("_TintColor", effectColor);
				materialPropertyBlock.SetFloat("_TintLerp", Mathf.Clamp(totalValue, 0f, 0.9f));
				renderer.SetPropertyBlock(materialPropertyBlock);
				materialPropertyBlock.Clear();
			}
		}
	}

	private void UpdateEffectColor()
	{
		effectColor = Color.black;
		totalValue = 0f;
		for (int i = 0; i < colors.Count; i++)
		{
			if (effectColor == Color.black)
			{
				totalValue += colors[i].currentValue;
				effectColor = colors[i].color;
			}
			else
			{
				totalValue += colors[i].currentValue;
				effectColor = Color.Lerp(effectColor, colors[i].color, colors[i].currentValue / totalValue);
			}
		}
	}

	public IEnumerator GoGray(Renderer rend)
	{
		yield return waitOneSecond;
		if (isDone)
		{
			yield break;
		}
		Material[] mats = rend.GetMaterialsNonAlloc();
		for (int i2 = 0; i2 < mats.Length; i2++)
		{
			if (mats[i2].HasProperty("_Color"))
			{
				Color color = mats[i2].color;
				Color.RGBToHSV(color, out var h, out var s, out var v);
				while (s > 0.35f)
				{
					v -= Time.deltaTime * 0.005f;
					s -= Time.deltaTime * 0.05f;
					s = Mathf.Clamp(s, 0.35f, 1f);
					color = Color.HSVToRGB(h, s, v);
					mats[i2].color = color;
					yield return null;
				}
			}
		}
		rend.materials = mats;
	}
}
