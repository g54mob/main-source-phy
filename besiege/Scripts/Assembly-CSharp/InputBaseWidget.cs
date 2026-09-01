using System.Collections.Generic;
using BlockMapperInternal;
using Selectors;
using UnityEngine;

public class InputBaseWidget : ParameterWidget
{
	[HideInInspector]
	public Renderer backgroundRenderer;

	public FilterRendererPair[] blockPairs;

	public KeyChangeSelector keySelector;

	public InputGroup group;

	[SerializeField]
	protected ContainerDetails container;

	protected int index;

	public int Index
	{
		get
		{
			return index;
		}
	}

	public override void Init(int index, object parameter)
	{
		base.Init(index, parameter);
		backgroundRenderer = container.Background.GetComponentInChildren<Renderer>();
	}

	protected virtual void UpdateBlockVis(List<BlockType> type)
	{
		for (int i = 0; i < blockPairs.Length; i++)
		{
			GameObject gameObject = blockPairs[i].filter.transform.parent.gameObject;
			if (i >= type.Count)
			{
				gameObject.SetActive(false);
				break;
			}
			gameObject.SetActive(true);
			SetIconTo(type[i], blockPairs[i]);
		}
	}

	protected virtual void SetIconTo(BlockType type, FilterRendererPair block)
	{
		BlockPrefab value;
		if (PrefabMaster.BlockPrefabs.TryGetValue((int)type, out value))
		{
			SetIconToMatch(block.renderer.transform, value.GetButtonIcon().Alignment);
			SetIconToVisual(value, block);
			CorrectScaleForOutlierSkinSizes(value, block.renderer);
		}
	}

	private void SetIconToMatch(Transform ico, FauxTransform trans)
	{
		Vector3 localPosition = trans.localPosition;
		localPosition.z = -1f;
		ico.localPosition = localPosition;
		ico.localRotation = trans.localRotation;
		ico.localScale = trans.localScale;
	}

	private void SetIconToVisual(BlockPrefab prefab, FilterRendererPair ico)
	{
		BlockSkinLoader.SkinPack.Skin defaultSkin = prefab.DefaultSkin;
		if (prefab.VisualController.CanChangeTexture)
		{
			Color color = defaultSkin.material.color;
			Material material = ico.renderer.material;
			material.color = new Color(color.r, color.g, color.b, material.color.a);
			material.mainTexture = defaultSkin.texture;
			if (defaultSkin.material.shader == PrefabMaster.BlockPrefabs[57].DefaultSkin.material.shader)
			{
				Color color2 = defaultSkin.material.GetColor("_Emission");
				material.color = (material.color + color2) / 2f;
				material.color += color2;
			}
			if (defaultSkin.material.HasProperty("_RimColor"))
			{
				material.SetColor("_RimColor", defaultSkin.material.GetColor("_RimColor"));
			}
			if (defaultSkin.material.HasProperty("_RimPower"))
			{
				material.SetFloat("_RimPower", defaultSkin.material.GetFloat("_RimPower"));
			}
		}
		if (prefab.VisualController.CanChangeMesh)
		{
			ico.filter.sharedMesh = defaultSkin.mesh;
			CorrectScaleForOutlierSkinSizes(prefab, ico.renderer);
		}
		else
		{
			ico.filter.sharedMesh = prefab.GetButtonIcon().myMeshFilter.sharedMesh;
		}
	}

	private void CorrectScaleForOutlierSkinSizes(BlockPrefab prefab, Renderer target)
	{
		Vector3 size = target.bounds.size;
		float magnitude = new Vector3(size.x, size.y, 0f).magnitude;
		float targetMag = prefab.GetButtonIcon().targetMag;
		if (magnitude != 0f && Mathf.Abs(targetMag - magnitude) > 0.6f * targetMag)
		{
			float num = targetMag / magnitude;
			target.transform.localScale *= num;
		}
	}
}
