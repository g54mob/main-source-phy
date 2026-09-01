using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectSkinButton : ClickBehaviour
{
	[NonSerialized]
	public int ID;

	[NonSerialized]
	public BlockSkinLoader.SkinPack.Skin mySkin;

	protected MeshRenderer skinRenderer;

	protected AudioSource audioSource;

	protected MaterialPropertyBlock props;

	protected bool quitting;

	public void Setup(int ID, BlockSkinLoader.SkinPack.Skin skin)
	{
		props = new MaterialPropertyBlock();
		props.SetFloat("_GridSpacing", 0.75f);
		if (skinRenderer == null)
		{
			skinRenderer = base.transform.FindChild("IconPivot/Icon").GetComponent<MeshRenderer>();
		}
		skinRenderer.SetPropertyBlock(props);
		if (audioSource == null)
		{
			audioSource = base.transform.parent.GetComponent<AudioSource>();
		}
		base.gameObject.SetActive(true);
		HandleIcon(ID, skin);
	}

	public void HandleIcon(int ID, BlockSkinLoader.SkinPack.Skin skin)
	{
		if (ID != this.ID || skin != mySkin || skin == null)
		{
			this.ID = ID;
			if (mySkin != null)
			{
				mySkin.Unregister(this);
			}
			if (skin != null)
			{
				mySkin = skin.Register(this);
			}
			else
			{
				mySkin = null;
			}
		}
		SetIconToMatch(PrefabMaster.BlockPrefabs[ID].GetButtonIcon().Alignment);
		SetIconToVisual(PrefabMaster.BlockPrefabs[ID].VisualController, mySkin);
		base.gameObject.transform.FindChild("Tooltip").gameObject.SetActive(skin != null);
		if (mySkin != null)
		{
			CorrectScaleForOutlierSkinSizes();
			string text = mySkin.pack.name.ToUpper().TrimEnd();
			while (true)
			{
				if (text.EndsWith("SKIN"))
				{
					text = text.Replace("SKIN", string.Empty);
					text = text.TrimEnd();
					break;
				}
				if (text.EndsWith("PACK"))
				{
					text = text.Replace("PACK", string.Empty);
					text = text.TrimEnd();
					continue;
				}
				if (text.EndsWith("PACKAGE"))
				{
					text = text.Replace("PACKAGE", string.Empty);
					text = text.TrimEnd();
					continue;
				}
				if (text.EndsWith("包"))
				{
					text = ((!text.EndsWith("图像包")) ? text.Replace("包", string.Empty) : text.Replace("图像包", string.Empty));
				}
				break;
			}
			base.gameObject.transform.FindChild("Tooltip/TooltipText").GetComponent<TextMesh>().text = text;
		}
		base.gameObject.SendMessage("SetEnabledMsg", skin != null, SendMessageOptions.DontRequireReceiver);
		skinRenderer.shadowCastingMode = ShadowCastingMode.Off;
	}

	public void Disable(int ID)
	{
		if (mySkin != null)
		{
			mySkin.Unregister(this);
		}
		mySkin = null;
		if (skinRenderer == null)
		{
			skinRenderer = base.transform.FindChild("IconPivot/Icon").GetComponent<MeshRenderer>();
		}
		SetIconToMatch(PrefabMaster.BlockPrefabs[ID].GetButtonIcon().Alignment);
		SetIconToVisual(PrefabMaster.BlockPrefabs[ID].VisualController, null);
		base.gameObject.transform.FindChild("Tooltip").gameObject.SetActive(false);
		base.gameObject.SendMessage("SetEnabledMsg", false, SendMessageOptions.DontRequireReceiver);
		base.gameObject.SetActive(false);
	}

	protected void OnApplicationQuit()
	{
		quitting = true;
	}

	protected void OnDestroy()
	{
		if (!quitting && SingleInstance<BlockSkinLoader>.hasInstance() && mySkin != null)
		{
			mySkin.Unregister(this);
		}
	}

	public override void OnClicked()
	{
		if ((bool)MachineToolController.Instance)
		{
			MachineToolController.Instance.DisableAll();
		}
		audioSource.Play();
		if (mySkin != null)
		{
			PrefabMaster.BlockPrefabs[ID].VisualController.UpdateVis(mySkin);
			ReferenceMaster.ActiveSkin = mySkin;
			if (SingleInstanceFindOnly<PrefabVisualUI>.hasInstance())
			{
				SingleInstanceFindOnly<PrefabVisualUI>.Instance.SetRecent(mySkin);
			}
		}
	}

	protected void SetIconToMatch(FauxTransform trans)
	{
		Vector3 localPosition = trans.localPosition;
		localPosition.z = 0f;
		skinRenderer.transform.localPosition = localPosition;
		skinRenderer.transform.localRotation = trans.localRotation;
		skinRenderer.transform.localScale = trans.localScale;
	}

	protected void SetIconToVisual(BlockVisualController visControl, BlockSkinLoader.SkinPack.Skin vis)
	{
		Transform transform = skinRenderer.transform;
		foreach (Transform item in transform)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		Transform transform3 = visControl.Prefab.GetButtonIcon().transform.FindChild("IconPivot/Icon");
		List<MeshRenderer> list = new List<MeshRenderer>();
		if (transform3 != null && transform3.childCount > 0)
		{
			Quaternion rotation = transform.rotation;
			MeshRenderer component = transform3.GetComponent<MeshRenderer>();
			string value = component.material.shader.name;
			for (int i = 0; i < transform3.childCount; i++)
			{
				Transform child = transform3.GetChild(i);
				Transform transform4 = UnityEngine.Object.Instantiate(child, transform.TransformPoint(child.localPosition), rotation * child.localRotation, transform) as Transform;
				transform4.localScale = child.localScale;
				MeshRenderer component2 = transform4.GetComponent<MeshRenderer>();
				if (component2 != null && (component2.material.shader.name.Equals(value) || child.gameObject.tag == "BlockIconExtra"))
				{
					list.Add(component2);
				}
				else
				{
					UnityEngine.Object.Destroy(transform4.gameObject);
				}
			}
		}
		Mesh mesh = null;
		Material material = null;
		if (vis == null)
		{
			if (SingleInstanceFindOnly<PrefabVisualUI>.Instance != null)
			{
				material = SingleInstanceFindOnly<PrefabVisualUI>.Instance.iconInactiveMaterial;
			}
			if (visControl.CanChangeMesh)
			{
				vis = visControl.Prefab.DefaultSkin;
				mesh = visControl.Prefab.DefaultSkin.mesh;
			}
			SetMeshAndTex(visControl, list, mesh, material);
			SetSecondary(list, null, null);
			return;
		}
		if (visControl.CanChangeTexture && vis.material != null)
		{
			Color color = vis.material.color;
			material = new Material(vis.material);
			material.color = new Color(color.r, color.g, color.b, skinRenderer.material.color.a);
			material.mainTexture = vis.texture;
		}
		if (visControl.CanChangeMesh)
		{
			mesh = vis.mesh;
		}
		SetMeshAndTex(visControl, list, mesh, material);
		SetSecondary(list, mesh, material);
	}

	private void SetMeshAndTex(BlockVisualController visControl, List<MeshRenderer> children, Mesh mesh, Material mat)
	{
		if ((bool)mat)
		{
			Material[] array = new Material[1] { mat };
			BlockType iD = (BlockType)visControl.Prefab.ID;
			if (iD == BlockType.SqrBalloon)
			{
				array = new Material[2] { mat, mat };
				if (mat.HasProperty("_DepthOffset"))
				{
					array[0] = new Material(mat);
					array[0].SetFloat("_DepthOffset", -100f);
				}
			}
			skinRenderer.materials = array;
			if (ID != 78)
			{
				for (int i = 0; i < children.Count; i++)
				{
					children[i].materials = array;
				}
			}
		}
		if ((bool)mesh)
		{
			skinRenderer.GetComponent<MeshFilter>().sharedMesh = mesh;
		}
		else
		{
			skinRenderer.GetComponent<MeshFilter>().sharedMesh = visControl.Prefab.GetButtonIcon().myMeshFilter.sharedMesh;
		}
	}

	private void SetSecondary(List<MeshRenderer> l, Mesh mesh, Material mat)
	{
		if (ID == 78 && mat != null)
		{
			if ((bool)mat.mainTexture)
			{
				props.SetTexture("_SailTex", mat.mainTexture);
			}
			BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[ID];
			bool flag = mesh == blockPrefab.DefaultSkin.mesh;
			Material[] materials = new Material[2]
			{
				mat,
				(blockPrefab.blockBehaviour as SailBlock).sailMat
			};
			if (l.Count > 0)
			{
				MeshRenderer meshRenderer = l[0];
				meshRenderer.gameObject.SetActive(!flag);
				meshRenderer.materials = materials;
				meshRenderer.SetPropertyBlock(props);
			}
			if (flag)
			{
				skinRenderer.materials = materials;
			}
			skinRenderer.SetPropertyBlock(props);
		}
		else if (l.Count > 0)
		{
			l[0].gameObject.SetActive(false);
		}
	}

	protected void CorrectScaleForOutlierSkinSizes()
	{
		MeshRenderer meshRenderer = skinRenderer;
		Vector3 size = meshRenderer.bounds.size;
		float magnitude = new Vector3(size.x, size.y, 0f).magnitude;
		float targetMag = PrefabMaster.BlockPrefabs[ID].GetButtonIcon().targetMag;
		if (magnitude != 0f && Mathf.Abs(targetMag - magnitude) > 0.6f * targetMag)
		{
			float num = targetMag / magnitude;
			meshRenderer.transform.localScale *= num;
		}
	}
}
