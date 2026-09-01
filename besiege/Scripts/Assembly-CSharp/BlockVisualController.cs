using System;
using System.Collections.Generic;
using System.Linq;
using BesiegeDlc;
using InternalModding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using cakeslice;

[AddComponentMenu("Blocks/BlockVisualController")]
public class BlockVisualController : MonoBehaviour, IFireEffect
{
	[Serializable]
	public class Heating
	{
		public float lerpSpeed;

		public Color glowCol;

		public string colToSet;
	}

	[Serializable]
	public class Burning
	{
		public Color Color;
	}

	public static Material[] fallbackGhost;

	public static Material[] bannedMat;

	public BlockBehaviour Block;

	public MeshRenderer[] renderers;

	private Vector4 tiling;

	public MeshRenderer[] shadows;

	public GameObject[] miscRenderers;

	public Outline[] outlines;

	public MeshRenderer arrow;

	public Renderer[] followVisibility;

	[NonSerialized]
	public BlockSkinLoader.SkinPack.Skin selectedSkin;

	[NonSerialized]
	public bool lockVisibility;

	[NonSerialized]
	public bool isVisible = true;

	protected bool lockSelectHighlight;

	protected bool skinsWereEnabled;

	protected bool hasBeenAssigned;

	protected float _prevIcePct;

	protected float _prevBurnPct;

	protected BlockSkinLoader.SkinPack.Skin prevSelectedSkin;

	protected bool skinWasEnabled = true;

	protected List<bool> renVisible;

	protected bool isDestroyed;

	protected bool quitting;

	private bool hasMaterialProperties;

	private Dictionary<string, object> materialProperties;

	private Material clusterMaterial;

	protected static Material intensityMaterial;

	protected int lastClusterIndex = -3;

	private float glowTimer;

	private float glowAmount;

	private bool outlineSetUp;

	protected MaterialPropertyBlock props;

	public float lastIgniteTime;

	public bool canBeHeated;

	public Heating heating;

	public Burning burning;

	[HideInInspector]
	[Obsolete("isSimulating property is obsolete, use Block.isSimulating instead.", false)]
	public bool isSimulating;

	[Obsolete("meshFiltery is obsolete, use MeshFilter instead.", false)]
	[HideInInspector]
	public MeshFilter meshFiltery;

	[Obsolete("shortVisRen is obsolete, use GetShortRenderer(out Renderer) instead.", false)]
	[HideInInspector]
	public Renderer shortVisRen;

	private bool changedColor;

	private float lastDrag;

	private static Color black = new Color(0.2f, 0.3f, 0.4f, 0f);

	private static Color yellow = new Color(1f, 1f, 0f, 0.5f);

	private static Color orange = (Color.red + Color.yellow) * 0.5f;

	private static Color red = Color.red;

	protected Color intensityColor;

	public bool freezeOutline;

	public float GlowAmount
	{
		get
		{
			return glowAmount;
		}
	}

	public float BurnPct
	{
		get
		{
			return _prevBurnPct;
		}
	}

	public int ID
	{
		get
		{
			return Prefab.ID;
		}
	}

	public BlockPrefab Prefab
	{
		get
		{
			return Block.Prefab;
		}
	}

	public BlockBehaviour SourceBlock
	{
		get
		{
			return (!Block.isSimulating) ? Block : Block.BuildingBlock;
		}
	}

	public virtual MeshFilter MeshFilter
	{
		get
		{
			return (!Prefab.hasMeshFilter) ? null : Block.MeshRenderer.GetComponent<MeshFilter>();
		}
	}

	public List<BlockSkinLoader.SkinPack.Skin> Options
	{
		get
		{
			return _Options();
		}
	}

	public bool IsPrefab
	{
		get
		{
			return Prefab.VisualController == this;
		}
	}

	public bool Selected { get; set; }

	public bool Highlighted { get; set; }

	public bool CanChangeMesh
	{
		get
		{
			return Prefab.CanChangeMesh;
		}
	}

	public virtual bool CanChangeTexture
	{
		get
		{
			return HasRenderer();
		}
	}

	protected BlockBehaviour BaseBlock
	{
		get
		{
			return (!Block.isParented) ? Block : Block.parentBlock;
		}
	}

	public event SetToNormal SetToNormal;

	public event SetToCluster SetToCluster;

	protected virtual List<BlockSkinLoader.SkinPack.Skin> _Options()
	{
		return Prefab.AvailableSkins;
	}

	public virtual List<BlockSkinLoader.SkinPack.Skin> CustomOptions()
	{
		return Prefab.downloadedSkins;
	}

	internal void Awake()
	{
		BlockPrefab prefab = Prefab;
		Machine componentInParent = base.transform.parent.GetComponentInParent<Machine>();
		if (Prefab == null)
		{
			BlockBehaviour block;
			if (!Block.HasParentMachine)
			{
				if (componentInParent == null || Block.BuildIndex == -1)
				{
					UnityEngine.Object.DestroyImmediate(base.gameObject);
				}
				componentInParent.GetBlockFromIndex(Block.BuildIndex, out block);
			}
			else
			{
				Block._parentMachine.GetBlockFromIndex(Block.BuildIndex, out block);
			}
			prefab = block.Prefab;
		}
		if (Machine.Active() != null && componentInParent.PlayerID == Machine.Active().PlayerID && !DlcManager.Instance.GetBlockDLCStatus(prefab.Type))
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		if (!Block.CanGlow || !Block.isSimulating)
		{
			return false;
		}
		float num = Time.deltaTime * 10f;
		if (!Block.SimPhysics)
		{
			num = Mathf.Max(num, NetworkScene.ServerSettings.sendRate);
			lastIgniteTime = Time.time;
		}
		glowTimer += num;
		return true;
	}

	public void SetTiling(Vector4 tile)
	{
		tiling = tile;
		if (!hasMaterialProperties)
		{
			props = new MaterialPropertyBlock();
			hasMaterialProperties = true;
		}
		SetMaterialProperties(props);
	}

	public void AssignMaterialProperty(string propertyName, object val, bool setMaretialProperties = true)
	{
		SetMaterialProperty(propertyName, val);
		if (setMaretialProperties)
		{
			SetMaterialProperties();
		}
	}

	public void AssignMaterialColor(string propertyName, Color val, bool setMaretialProperties = true)
	{
		if (!hasMaterialProperties)
		{
			props = new MaterialPropertyBlock();
			hasMaterialProperties = true;
		}
		props.SetColor(propertyName, val);
		if (setMaretialProperties)
		{
			SetMaterialProperties();
		}
	}

	public object ReadMaterialProperty<T>(string propertyName)
	{
		if (hasMaterialProperties)
		{
			if (typeof(T) == typeof(float))
			{
				return props.GetFloat(propertyName);
			}
			if (typeof(T) == typeof(Color))
			{
				return props.GetVector(propertyName);
			}
			if (typeof(T) == typeof(int))
			{
				return props.GetFloat(propertyName);
			}
		}
		return null;
	}

	protected void SetMaterialProperty(string propertyName, object val)
	{
		if (!hasMaterialProperties)
		{
			props = new MaterialPropertyBlock();
			hasMaterialProperties = true;
		}
		if (val is float)
		{
			props.SetFloat(propertyName, (float)val);
		}
		else if (val is Color)
		{
			props.SetColor(propertyName, (Color)val);
		}
		else if (val is int)
		{
			props.SetFloat(propertyName, (int)val);
		}
		else if (val is Texture)
		{
			props.SetTexture(propertyName, (Texture)val);
		}
		else if (val is Vector3)
		{
			props.SetVector(propertyName, (Vector3)val);
		}
		else if (val is Vector4)
		{
			props.SetVector(propertyName, (Vector4)val);
		}
	}

	protected void SetMaterialProperties()
	{
		if (CanChangeTexture && hasMaterialProperties)
		{
			if (!hasMaterialProperties)
			{
				props = new MaterialPropertyBlock();
				hasMaterialProperties = true;
			}
			SetMaterialProperties(props);
			Renderer shortVis;
			if (GetShortRenderer(out shortVis))
			{
				shortVis.SetPropertyBlock(props);
			}
			if (Block.isSimulating)
			{
				SetAttachedMaterial();
			}
		}
	}

	public virtual void SetAttachedMaterial()
	{
		for (int i = 0; i < Block.visAddedToMe.Count; i++)
		{
			Renderer renderer = Block.visAddedToMe[i];
			if (!(renderer != null))
			{
				continue;
			}
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			MaterialPropertyBlock materialPropertyBlock2 = new MaterialPropertyBlock();
			renderer.GetPropertyBlock(materialPropertyBlock);
			renderers[0].GetPropertyBlock(materialPropertyBlock2);
			if (renderer.gameObject.tag == "PreventSurfaceFracture")
			{
				float value = materialPropertyBlock.GetFloat("_HSVEnabled");
				Color value2 = materialPropertyBlock.GetVector("_TintColor");
				materialPropertyBlock2.SetFloat("_HSVEnabled", value);
				materialPropertyBlock2.SetColor("_TintColor", value2);
			}
			if (changedColor)
			{
				Color value3 = materialPropertyBlock.GetVector("_EmissCol");
				if (value3.r > props.GetVector("_EmissCol").x)
				{
					materialPropertyBlock2.SetColor("_EmissCol", value3);
				}
				Color value2 = materialPropertyBlock.GetVector("_Color");
				if (value2.r > 0f && value2.r < props.GetVector("_Color").x)
				{
					materialPropertyBlock2.SetColor("_Color", value2);
				}
			}
			renderer.SetPropertyBlock(materialPropertyBlock2);
		}
	}

	public virtual bool GetShortRenderer(out Renderer shortVis)
	{
		shortVis = null;
		return false;
	}

	public void SetDamageLevel(float pct)
	{
		SetMaterialProperty("_DamageAmount", pct);
		SetMaterialProperties();
	}

	public virtual void SetBurnedLevel(float pct)
	{
		if (pct != _prevBurnPct)
		{
			Color b = Prefab.burnColor;
			if (Block is ModBlockBehaviourHandler)
			{
				b = burning.Color;
			}
			Color a = ((selectedSkin == null) ? DefaultRenderer().sharedMaterial.color : selectedSkin.material.color);
			SetMaterialProperty("_Color", Color.Lerp(a, b, pct));
			SetMaterialProperty("_EmissCol", pct * Color.white);
			SetMaterialProperties();
			changedColor = true;
			_prevBurnPct = pct;
		}
	}

	public void SetFrozenLevel(float pct)
	{
		if (pct != _prevIcePct)
		{
			float num = 0.3f;
			Color b = Color.black;
			if (Prefab.canFreeze && Block.iceTag.takesDamage)
			{
				num = 1f;
				b = new Color32(84, 100, 131, byte.MaxValue);
			}
			SetMaterialProperty("_Color", Color.Lerp(selectedSkin.material.color, b, pct * num));
			SetMaterialProperty("_FreezeAmount", pct);
			SetMaterialProperty("_EmissCol", pct * Color.white);
			SetMaterialProperties();
			changedColor = true;
			_prevIcePct = pct;
		}
	}

	public void SetBloodyLevel(float pct, Color c)
	{
		SetMaterialProperty("_BloodColor", c);
		SetMaterialProperty("_BloodAmount", pct);
		SetMaterialProperties();
	}

	public void SetTransparency(float pct)
	{
		if (selectedSkin != null && (bool)selectedSkin.material)
		{
			Color color = selectedSkin.material.color;
			SetMaterialProperty("_Color", new Color(color.r, color.g, color.b, pct));
			SetMaterialProperties();
		}
	}

	public void SetGlowLevel(Color target, float pct)
	{
		if (!CanChangeTexture || Highlighted || Selected)
		{
			return;
		}
		string propertyName = Prefab.heatColorName;
		if (Block is ModBlockBehaviourHandler)
		{
			propertyName = heating.colToSet;
		}
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		for (int i = 0; i < renderers.Length; i++)
		{
			MeshRenderer meshRenderer = renderers[i];
			if (!(meshRenderer == null) && meshRenderer.material.HasProperty(propertyName))
			{
				meshRenderer.GetPropertyBlock(materialPropertyBlock);
				Color value = Color.Lerp(materialPropertyBlock.GetVector(propertyName), target, pct);
				materialPropertyBlock.SetColor(propertyName, value);
				meshRenderer.SetPropertyBlock(materialPropertyBlock);
			}
		}
		Renderer shortVis;
		if (GetShortRenderer(out shortVis) && shortVis.material.HasProperty(propertyName))
		{
			shortVis.GetPropertyBlock(materialPropertyBlock);
			Color value2 = Color.Lerp(materialPropertyBlock.GetVector(propertyName), target, pct);
			materialPropertyBlock.SetColor(propertyName, value2);
			shortVis.SetPropertyBlock(materialPropertyBlock);
		}
		if (materialPropertyBlock.GetFloat("_FreezeAmount") > 1f - pct)
		{
			SetFrozenLevel(1f - pct);
		}
	}

	public void UpdateBlockVis(float delta)
	{
		LerpGlow(delta);
	}

	protected void LerpGlow(float delta)
	{
		float num = Prefab.heatLerpSpeed;
		Color color = Prefab.heatGlowColor;
		if (Block is ModBlockBehaviourHandler)
		{
			num = heating.lerpSpeed;
			color = heating.glowCol;
		}
		float num2 = delta * num;
		if (glowTimer > 0f || glowAmount > 0f)
		{
			glowTimer = Mathf.Clamp01(glowTimer - delta);
			glowAmount = Mathf.Lerp(glowAmount, (!(glowTimer > 0f)) ? 0f : 1f, num2);
			if (glowAmount <= 0.01f)
			{
				glowAmount = 0f;
			}
			SetGlowLevel((!(glowTimer > 0f)) ? Color.black : color, num2);
		}
	}

	public void PlaceFromPrefab()
	{
		Initialize(Prefab.VisualController.selectedSkin);
	}

	public void PlaceFromBlock(BlockBehaviour block)
	{
		Initialize(block.VisualController.selectedSkin);
	}

	public void ApplyInfoFromBlock(BlockBehaviour block)
	{
		BlockSkinLoader.SkinPack.Skin skin = block.VisualController.selectedSkin;
		if (skin != null)
		{
			selectedSkin = skin;
			SetNormal();
			if (Prefab.CanGetNewVisuals)
			{
				hasBeenAssigned = selectedSkin.doneLoading && (selectedSkin.shortSkin == null || selectedSkin.shortSkin.doneLoading);
				prevSelectedSkin = selectedSkin;
				skinWasEnabled = selectedSkin.enabled;
				skinsWereEnabled = OptionsMaster.skinsEnabled;
				Prefab.blockVisControllers.Add(this);
			}
		}
	}

	public virtual void ReplaceSkin(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (skin == null)
		{
			Debug.LogWarning("Skin is null when calling ReplaceSkin!");
		}
		else if (skin.pack != null)
		{
			BlockSkinLoader.SkinPack.Skin skin2 = FindVisualOptionFor(skin.pack);
			if (skin2 == null)
			{
				skin2 = ((skin.prefab != Prefab) ? Prefab.DefaultSkin : skin);
			}
			Initialize(skin2);
		}
		else
		{
			Initialize(Prefab.DefaultSkin);
		}
	}

	public virtual void PlaceFromBlockInfo(BlockInfo info)
	{
		if (info != null && info.Skin != null)
		{
			ReplaceSkin(info.Skin);
		}
		else
		{
			Initialize(Prefab.DefaultSkin);
		}
	}

	public void Initialize(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (!IsPrefab)
		{
			UpdateVis(skin);
			if (Prefab.CanGetNewVisuals)
			{
				Prefab.blockVisControllers.Add(this);
			}
		}
		else
		{
			Debug.LogError("A block of type " + ID + " is the prefab, but was attempted initialized as a placed block.");
		}
	}

	public void InitPrefab(int ID)
	{
		skinsWereEnabled = OptionsMaster.skinsEnabled;
		UpdateVis();
		UpdateShadowCastingMode();
		if (Prefab.CanGetNewVisuals)
		{
			Prefab.VisualController = this;
		}
	}

	public virtual BlockSkinLoader.SkinPack.Skin FindVisualOptionFor(BlockSkinLoader.SkinPack pack)
	{
		BlockSkinLoader.SkinPack.Skin result = null;
		if (pack != null)
		{
			List<BlockSkinLoader.SkinPack.Skin> options = Options;
			for (int i = 0; i < options.Count; i++)
			{
				if (options[i].pack == pack)
				{
					return options[i];
				}
				if (!string.IsNullOrEmpty(pack.id) && !char.IsLetter(pack.id[0]))
				{
					if (options[i].pack.id == pack.id)
					{
						return options[i];
					}
					if (options[i].pack.name == pack.name)
					{
						result = options[i];
					}
				}
				else if (options[i].pack.name == pack.name)
				{
					if (options[i].pack.id == pack.id)
					{
						return options[i];
					}
					result = options[i];
				}
			}
		}
		return result;
	}

	public virtual BlockSkinLoader.SkinPack.Skin SafeGetVisualOptionFor(BlockSkinLoader.SkinPack pack)
	{
		BlockSkinLoader.SkinPack.Skin skin = FindVisualOptionFor(pack);
		if (skin == null)
		{
			skin = Options[0];
		}
		return skin;
	}

	public bool UpdateVisFromPack(BlockSkinLoader.SkinPack pack)
	{
		if (pack != null)
		{
			BlockSkinLoader.SkinPack.Skin skin = FindVisualOptionFor(pack);
			if (skin != null)
			{
				return UpdateVis(skin);
			}
		}
		return false;
	}

	public virtual void UpdateVisFromBlockInfo(BlockInfo info)
	{
		if (info != null && info.Skin != null && info.Skin.pack != null)
		{
			BlockSkinLoader.SkinPack.Skin skin = FindVisualOptionFor(info.Skin.pack);
			if (skin == null)
			{
				skin = ((info.Skin.prefab != Prefab) ? Prefab.DefaultSkin : info.Skin);
			}
			UpdateVis(skin);
		}
	}

	public virtual bool UpdateVis(BlockSkinLoader.SkinPack.Skin skin = null)
	{
		if (isDestroyed)
		{
			return false;
		}
		if (Block.isBuildBlock && Block.hasSimBlock)
		{
			Block.SimBlock.VisualController.UpdateVis(skin);
		}
		if (Prefab.CanGetNewVisuals)
		{
			if (OptionsMaster.skinsEnabled != skinsWereEnabled || !hasBeenAssigned || skin != null || selectedSkin == null || selectedSkin != prevSelectedSkin || selectedSkin.enabled != skinWasEnabled || selectedSkin.pack.deleted)
			{
				skinsWereEnabled = OptionsMaster.skinsEnabled;
				if (skin == null)
				{
					skin = selectedSkin;
				}
				if (skin == null || skin.pack.deleted)
				{
					skin = Prefab.DefaultSkin;
				}
				if (skin == null)
				{
					return false;
				}
				if (skin.ID == Prefab.ID)
				{
					if (skin != selectedSkin)
					{
						if (selectedSkin != null && selectedSkin.shortSkin != null)
						{
							selectedSkin.shortSkin.Unregister(this);
						}
						if (selectedSkin != null)
						{
							selectedSkin.Unregister(this);
						}
						selectedSkin = skin.Register(this);
						if (selectedSkin != null && selectedSkin.shortSkin != null)
						{
							selectedSkin.shortSkin.Register(this);
						}
					}
					skinWasEnabled = selectedSkin.enabled;
					hasBeenAssigned = selectedSkin.doneLoading && (selectedSkin.shortSkin == null || selectedSkin.shortSkin.doneLoading);
					AssignSkin(selectedSkin);
					prevSelectedSkin = selectedSkin;
					return true;
				}
			}
		}
		else if (CanChangeTexture && selectedSkin == null)
		{
			selectedSkin = Prefab.DefaultSkin;
		}
		return false;
	}

	protected void AssignSkin(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		if (isDestroyed || selectedSkin == null)
		{
			return;
		}
		this.selectedSkin = selectedSkin;
		if (CanChangeMesh)
		{
			SetMesh(selectedSkin.mesh);
			if (Prefab.hasShortVis)
			{
				SetShortMesh(selectedSkin);
			}
			if (prevSelectedSkin != null)
			{
				if (prevSelectedSkin.isDefault != selectedSkin.isDefault)
				{
					SetShadow(selectedSkin.isDefault);
				}
			}
			else
			{
				SetShadow(selectedSkin.isDefault);
			}
		}
		if (CanChangeTexture)
		{
			if (!StatMaster.outlineBlocks)
			{
				if (Selected)
				{
					UpdateMat(ReferenceMaster.Instance.SelectedMaterial);
				}
				else if (Highlighted)
				{
					UpdateMat(ReferenceMaster.Instance.HighlightMaterial);
				}
				else
				{
					SetMaterial(selectedSkin);
				}
			}
			else
			{
				SetMaterial(selectedSkin);
			}
		}
		if (IsPrefab)
		{
			SetPrefabIcons();
			SetGhost(selectedSkin);
		}
		Block.hasOffset = false;
	}

	protected virtual void SetMesh(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		MeshFilter.sharedMesh = selectedSkin.mesh;
	}

	protected virtual void SetMaterial(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		SetNormal();
	}

	protected virtual void SetShadow(bool isDefault)
	{
		if (shadows.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < shadows.Length; i++)
		{
			shadows[i].enabled = isDefault;
		}
		for (int j = 0; j < renderers.Length; j++)
		{
			if (renderers[j].gameObject.tag != "BlockShadowChangeIgnore")
			{
				renderers[j].shadowCastingMode = ((!isDefault) ? ((!OptionsMaster.BesiegeConfig.ShadowsDoubled) ? ShadowCastingMode.On : ShadowCastingMode.TwoSided) : ShadowCastingMode.Off);
			}
		}
	}

	protected void SetBrokenFragmentMaterial(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		SetBrokenFragmentMaterial(selectedSkin.material);
	}

	protected virtual void SetShortMesh(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
	}

	public virtual void SetBrokenFragmentMaterial(Material mat)
	{
	}

	public virtual void SetPrefabIcons()
	{
		if (!Prefab.HasButtonIcons() || Prefab.GetButtonIcon() == null || renderers[0] == null)
		{
			return;
		}
		for (int i = 0; i < Prefab.ButtonIconCount(); i++)
		{
			BlockButtonControl buttonIcon = Prefab.GetButtonIcon(i);
			if (buttonIcon == null)
			{
				Debug.LogError("ButtonIcon " + i + " on '" + base.name + "' is null!", base.gameObject);
				continue;
			}
			if (CanChangeMesh && !Prefab.blockBehaviour.SurfaceType)
			{
				buttonIcon.SetMesh(selectedSkin.mesh);
			}
			if (CanChangeTexture)
			{
				bool value = renderers.Length == selectedSkin.materials.Length && renderers.Length > 1;
				buttonIcon.SetMaterial(selectedSkin, value);
			}
		}
	}

	public void SetGhost(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		if (!(Prefab.ghostController != null))
		{
			return;
		}
		if (CanChangeTexture)
		{
			if (CanChangeMesh)
			{
				Prefab.ghostController.SetGhostVis(selectedSkin.mesh, selectedSkin.ghostMaterials);
			}
			else
			{
				Prefab.ghostController.SetGhostVis(selectedSkin.ghostMaterials);
			}
		}
		else if (CanChangeMesh)
		{
			Prefab.ghostController.SetGhostVis(selectedSkin.mesh);
		}
	}

	public void ToggleArrow()
	{
		arrow.enabled = !StatMaster.hudHidden;
	}

	public void FlipArrow(bool flipped, Axes axis = Axes.x)
	{
		if (!Block.isSimulating && Prefab.hasArrow)
		{
			Transform transform = arrow.transform;
			Vector3 localScale = transform.localScale;
			switch (axis)
			{
			case Axes.x:
				transform.localScale = new Vector3(FlipAxis(localScale.x, flipped), localScale.y, localScale.z);
				break;
			case Axes.y:
				transform.localScale = new Vector3(localScale.x, FlipAxis(localScale.y, flipped), localScale.z);
				break;
			case Axes.z:
				transform.localScale = new Vector3(localScale.x, localScale.y, FlipAxis(localScale.z, flipped));
				break;
			}
		}
	}

	protected float FlipAxis(float val, bool flipped)
	{
		return Mathf.Abs(val) * (float)((!flipped) ? 1 : (-1));
	}

	public void ResetIsVisible()
	{
		isVisible = true;
	}

	public void SetInvisible()
	{
		if (StatMaster.isHeadless || lockVisibility || !isVisible)
		{
			return;
		}
		if (renVisible == null)
		{
			renVisible = new List<bool>();
		}
		else
		{
			renVisible.Clear();
		}
		for (int i = 0; i < miscRenderers.Length; i++)
		{
			if ((bool)miscRenderers[i])
			{
				miscRenderers[i].SetActive(false);
			}
		}
		EnableRenderer(false);
		Renderer shortVis;
		if (GetShortRenderer(out shortVis))
		{
			renVisible.Add(shortVis.enabled);
			shortVis.enabled = false;
		}
		if (!Block.isSimulating && Prefab.hasArrow)
		{
			arrow.gameObject.SetActive(false);
		}
		for (int j = 0; j < followVisibility.Length; j++)
		{
			if (followVisibility[j] != null)
			{
				followVisibility[j].enabled = false;
			}
		}
		for (int k = 0; k < shadows.Length; k++)
		{
			shadows[k].enabled = false;
		}
		isVisible = false;
	}

	public void SetVisible()
	{
		if (StatMaster.isHeadless || isVisible || lockVisibility)
		{
			return;
		}
		if (renVisible == null)
		{
			Debug.Log("renVisible isn't setup correctly!", base.gameObject);
			return;
		}
		for (int i = 0; i < miscRenderers.Length; i++)
		{
			if ((bool)miscRenderers[i])
			{
				miscRenderers[i].SetActive(true);
			}
		}
		EnableRenderer(true);
		Renderer shortVis;
		if (GetShortRenderer(out shortVis))
		{
			shortVis.enabled = renVisible.Last();
		}
		if (arrow != null)
		{
			arrow.gameObject.SetActive(true);
		}
		for (int j = 0; j < followVisibility.Length; j++)
		{
			if (followVisibility[j] != null)
			{
				followVisibility[j].enabled = true;
			}
		}
		for (int k = 0; k < shadows.Length; k++)
		{
			shadows[k].enabled = true;
		}
		isVisible = true;
		renVisible.Clear();
	}

	public Material GetClusterMaterial()
	{
		if (clusterMaterial == null && StatMaster.clusterCoded)
		{
			ColourCodeFromCluster();
		}
		return clusterMaterial;
	}

	private Material CreateClusterMaterial()
	{
		if (ReferenceMaster.Instance.clusterShader == null)
		{
			Debug.LogWarning("ReferenceMaster clusterShader is null");
			return new Material(Shader.Find("Custom/ClusterShader"));
		}
		return new Material(ReferenceMaster.Instance.clusterShader);
	}

	public void ColourCodeFromCluster(bool ignoreSelected = false)
	{
		if (!CanChangeTexture || ((bool)Block && lastClusterIndex == Block.ClusterIndex))
		{
			SetIntensityTexture(true);
		}
		else
		{
			if (!StatMaster.outlineBlocks && !ignoreSelected && Selected)
			{
				return;
			}
			lastClusterIndex = Block.ClusterIndex;
			if (!ReferenceMaster.clusterMaterials.TryGetValue(Block.ClusterIndex, out clusterMaterial))
			{
				clusterMaterial = CreateClusterMaterial();
				if (Block.ClusterIndex == -1)
				{
					clusterMaterial.SetColor("_ClusterColor", new Color(0.065f, 0.065f, 0.065f, 0.2f));
				}
				else if (Block.ClusterIndex == -2)
				{
					clusterMaterial.SetColor("_ClusterColor", new Color(0.2f, 0.045f, 0.05f, 0.2f));
				}
				else
				{
					System.Random random = new System.Random(Block.ClusterIndex + 1);
					float num = 0.3f * Mathf.Cos((float)Block.ClusterIndex * 1.7f) + 0.7f;
					clusterMaterial.SetColor("_ClusterColor", new Color((float)random.NextDouble() * num, (float)new System.Random(Block.ClusterIndex + random.Next()).NextDouble() * num, (float)new System.Random(Block.ClusterIndex * random.Next()).NextDouble() * num, 0.65f));
				}
				ReferenceMaster.clusterMaterials.Add(Block.ClusterIndex, clusterMaterial);
			}
			if (Block.isSimulating)
			{
				foreach (Renderer item in Block.visAddedToMe)
				{
					if ((bool)item)
					{
						item.sharedMaterial = clusterMaterial;
					}
				}
			}
			SetMaterial(clusterMaterial);
			Renderer shortVis;
			if (GetShortRenderer(out shortVis))
			{
				shortVis.sharedMaterial = clusterMaterial;
			}
			SetBrokenFragmentMaterial((Material)null);
			if (this.SetToCluster != null)
			{
				this.SetToCluster();
			}
			SetIntensityTexture(true);
		}
	}

	public virtual void ColourCodeFromIntensity(Action update)
	{
		if (Prefab.CanGetNewVisuals && CanChangeTexture)
		{
			GetIntensityMaterial();
			ColourCodeFromIntensity(update, intensityMaterial);
		}
	}

	public void ColourCodeFromIntensity(Action update, Material mat)
	{
		update();
		if (Block.isSimulating)
		{
			foreach (Renderer item in Block.visAddedToMe)
			{
				if ((bool)item)
				{
					item.sharedMaterial = mat;
				}
			}
		}
		SetMaterial(mat);
		Renderer shortVis;
		if (GetShortRenderer(out shortVis))
		{
			shortVis.sharedMaterial = mat;
		}
		SetBrokenFragmentMaterial(mat);
	}

	public void ColourCodeFromAreodynamics()
	{
		ColourCodeFromIntensity(delegate
		{
			UpdateAeroDragDisplay();
		});
	}

	public void ColourCodeFromStress()
	{
		ColourCodeFromIntensity(delegate
		{
			UpdateStressDisplay();
		});
	}

	public virtual void UpdateAeroDragDisplay()
	{
		if (!StatMaster.aeroCoded)
		{
			return;
		}
		float num = 0f;
		if (!Block.isSimulating)
		{
			num = ((!StatMaster.isMP) ? (Block.Rigidbody.drag * 0.5f) : ((!StatMaster.isHosting && (!StatMaster.isClient || !StatMaster.isLocalSim)) ? (Block.originalDrag * 0.5f) : (Block.Rigidbody.drag * 0.5f)));
		}
		else
		{
			BlockBehaviour baseBlock = BaseBlock;
			float num2 = baseBlock.waterDragMulti;
			if (num2 == 0f)
			{
				num2 = 1f;
			}
			num2 = 0.5f + num2 * 0.5f;
			float num3 = baseBlock.submergedPercent;
			if (StatMaster.isClient && !StatMaster.isLocalSim && baseBlock.InWater)
			{
				num3 = baseBlock.CalculateClientSubmerge();
			}
			num = ((!baseBlock.InWater) ? 0f : (num3 * Mathf.Clamp01(baseBlock.dragScale) * num2));
			if (baseBlock.InWind && !baseBlock.InWater)
			{
				num += baseBlock.dragScale;
			}
			num = (lastDrag = Mathf.Lerp(lastDrag, num, Time.deltaTime * 3f)) + baseBlock.originalDrag * 0.5f;
		}
		UpdateIntensityMaterial("_AeroColor", num);
	}

	public virtual void UpdateStressDisplay()
	{
		if (StatMaster.stressCoded)
		{
			float drag = 0f;
			if (Block.isSimulating)
			{
				drag = Block.GetStress();
			}
			UpdateIntensityMaterial("_AeroColor", drag);
		}
	}

	public virtual Material GetIntensityMaterial()
	{
		if (intensityMaterial == null)
		{
			intensityMaterial = ReferenceMaster.Instance.aerodynamicMaterial;
		}
		SetIntensityTexture(false);
		return intensityMaterial;
	}

	public virtual void SetIntensityTexture(bool update)
	{
		BlockVisualController visualController = SourceBlock.VisualController;
		Texture val = ((!visualController.hasBeenAssigned) ? Prefab.DefaultSkin.texture : visualController.selectedSkin.texture);
		AssignMaterialProperty("_TexProp", val, update);
	}

	protected void UpdateIntensityMaterial(string s, float drag, bool update = true)
	{
		intensityColor = Color.Lerp(black, yellow, drag * 3f);
		intensityColor = Color.Lerp(intensityColor, orange, drag * 3f - 1f);
		intensityColor = Color.Lerp(intensityColor, red, drag * 3f - 2f);
		AssignMaterialProperty(s, intensityColor, update);
	}

	public void SetGhostly()
	{
		if (CanChangeTexture && selectedSkin != null && (!lockSelectHighlight || !Selected))
		{
			Material[] array = new Material[1]
			{
				new Material((!ReferenceMaster.Instance.ghostShader) ? Shader.Find("Custom/MultiplayerGhostShader") : ReferenceMaster.Instance.ghostShader)
			};
			Color mpGhostColor = ReferenceMaster.Instance.mpGhostColor;
			float num = (mpGhostColor.r + mpGhostColor.g + mpGhostColor.b) / 3f;
			Color b = new Color(num, num, num, 1f);
			array[0].SetColor("_GhostEmiss", Color.Lerp(mpGhostColor, b, 0.5f) * 0.75f);
			array[0].SetColor("_GhostColor", mpGhostColor);
			if (Prefab != null)
			{
				array[0].SetTexture("_MainTex", Prefab.DefaultSkin.texture);
			}
			Selected = false;
			Highlighted = false;
			UpdateMats(array, array);
			SetBrokenFragmentMaterial(array[0]);
		}
	}

	public void MimicGhost()
	{
		if (!CanChangeTexture || selectedSkin == null || (lockSelectHighlight && Selected))
		{
			return;
		}
		Shader shader = Shader.Find("Custom/Transparent/Ghosts with Rim");
		if (shader == null)
		{
			Debug.LogError("[BlockVisualController]: Can't find Ghost with Rim shader");
			return;
		}
		Material[] array = new Material[1]
		{
			new Material(shader)
		};
		if (Prefab == null || Prefab.DefaultSkin == null || Prefab.DefaultSkin.material == null)
		{
			Debug.LogError("[BlockVisualController]: Prefab, default skin or default material is null: " + Prefab);
			return;
		}
		Material material = Prefab.DefaultSkin.material;
		Color color = material.GetColor("_Color");
		color.a = 0.65f;
		Material material2 = array[0];
		material2.SetColor("_Color", color);
		material2.SetColor("_RimColor", material.GetColor("_RimColor"));
		material2.SetFloat("_RimPower", (!material.HasProperty("_RimPower")) ? 4.6f : material.GetFloat("_RimPower"));
		material2.SetTexture("_MainTex", Prefab.DefaultSkin.texture);
		material2.SetFloat("_FrontOpacity", 0.63f);
		if (Machine.IsDraggedBlock(Prefab.Type))
		{
			array = new Material[3]
			{
				material2,
				material2,
				new Material(material2)
			};
			array[2].SetFloat("_FrontOpacity", 0.1f);
		}
		UpdateMats(array, array);
		SetBrokenFragmentMaterial(array[0]);
	}

	public void SetHighlighted(bool force = false)
	{
		if ((!Selected || force) && ((!StatMaster.outlineBlocks) ? UpdateMat(ReferenceMaster.Instance.HighlightMaterial) : UpdateOutline(2)))
		{
			Highlighted = true;
		}
	}

	public void SetSelected()
	{
		if ((!StatMaster.outlineBlocks) ? UpdateMat(ReferenceMaster.Instance.SelectedMaterial) : UpdateOutline((!Block.IsSelectedExtra) ? 1 : 2))
		{
			Selected = true;
			Highlighted = false;
		}
	}

	public void SetNoOutline()
	{
		if (StatMaster.outlineBlocks)
		{
			UpdateOutline(0);
		}
		else
		{
			SetNormal();
		}
		Selected = false;
		Highlighted = false;
	}

	public void SetBanned()
	{
		if (CanChangeTexture && (!lockSelectHighlight || !Selected))
		{
			if (bannedMat == null)
			{
				bannedMat = new Material[1]
				{
					new Material((!ReferenceMaster.Instance.ghostShader) ? Shader.Find("Custom/MultiplayerGhostShader") : ReferenceMaster.Instance.ghostShader)
				};
				Color color = new Color(0.65f, 0.004f, 0.06f, 0.2f);
				float num = (color.r + color.g + color.b) / 3f;
				Color b = new Color(num, num, num, 1f);
				bannedMat[0].SetColor("_GhostEmiss", Color.Lerp(color, b, 0.5f) * 0.75f);
				bannedMat[0].SetColor("_GhostColor", color);
			}
			Selected = false;
			Highlighted = false;
			UpdateMats(bannedMat, bannedMat);
		}
	}

	protected bool NormalBypassCases()
	{
		if (!Block.HasParentMachine)
		{
			return false;
		}
		if (StatMaster.clusterCoded)
		{
			ColourCodeFromCluster();
			return true;
		}
		if (StatMaster.aeroCoded)
		{
			ColourCodeFromAreodynamics();
			return true;
		}
		if (StatMaster.stressCoded)
		{
			ColourCodeFromStress();
			return true;
		}
		Machine parentMachine = Block.ParentMachine;
		if (!Block.isSimulating)
		{
			if (parentMachine.isLocalMachine)
			{
				if (Block.wasNotAllowed)
				{
					SetBanned();
					return true;
				}
			}
			else
			{
				if (parentMachine.curtainMode)
				{
					SetInvisible();
					return true;
				}
				SetVisible();
			}
		}
		else if (parentMachine.ghostMode)
		{
			SetGhostly();
			return true;
		}
		return false;
	}

	public virtual void SetNormal()
	{
		if (!NormalBypassCases() && !StatMaster.isHeadless && CanChangeTexture && selectedSkin != null && (!lockSelectHighlight || !Selected))
		{
			if (selectedSkin.shortSkin == null || selectedSkin.shortSkin.texture == null)
			{
				UpdateMats(selectedSkin.materials, selectedSkin.materials);
			}
			else
			{
				UpdateMats(selectedSkin.materials, selectedSkin.shortSkin.materials);
			}
			SetBrokenFragmentMaterial(selectedSkin.material);
			InvokeSetToNormalAction();
		}
	}

	public void InvokeSetToNormalAction()
	{
		if (this.SetToNormal != null)
		{
			this.SetToNormal();
		}
	}

	public virtual bool UpdateMat(Material mat)
	{
		if (mat == null)
		{
			return false;
		}
		bool flag = selectedSkin != null;
		Material[] array = new Material[1];
		if (flag && !SplitMats(selectedSkin.materials))
		{
			array = selectedSkin.materials.ToArray();
		}
		array[0] = mat;
		Material[] array2 = array.ToArray();
		if (flag && selectedSkin.shortSkin != null && selectedSkin.shortSkin.texture != null)
		{
			array2 = selectedSkin.shortSkin.materials.ToArray();
		}
		array2[0] = mat;
		return UpdateMats(array, array2);
	}

	public virtual bool UpdateMats(Material[] mats, Material[] shortMats)
	{
		if (mats == null || shortMats == null || !CanChangeTexture)
		{
			return false;
		}
		lastClusterIndex = -3;
		bool flag = SplitMats(mats);
		for (int i = 0; i < renderers.Length; i++)
		{
			MeshRenderer meshRenderer = renderers[i];
			if (!(meshRenderer == null))
			{
				bool flag2 = Prefab.Type == BlockType.RopeWinch || Prefab.Type == BlockType.RopeMeasure;
				if (flag2)
				{
					tiling = meshRenderer.material.mainTextureScale;
				}
				if (flag)
				{
					meshRenderer.sharedMaterial = mats[i];
				}
				else
				{
					meshRenderer.sharedMaterials = mats;
				}
				if (flag2)
				{
					meshRenderer.sharedMaterial.mainTextureScale = tiling;
				}
			}
		}
		Renderer shortVis;
		if (GetShortRenderer(out shortVis))
		{
			shortVis.sharedMaterials = shortMats;
		}
		return true;
	}

	public bool SplitMats(Material[] mats)
	{
		return renderers.Length == mats.Length && renderers.Length > 1;
	}

	public bool UpdateOutline(int state)
	{
		bool flag = state != 0;
		bool flag2 = false;
		if (outlines != null)
		{
			if (flag)
			{
				OutlineEffect.Instance.ChangeTargetType(1);
			}
			for (int i = 0; i < outlines.Length; i++)
			{
				Outline outline = outlines[i];
				if (outline == null)
				{
					continue;
				}
				if (!outlineSetUp)
				{
					outline.SetFromBlock(Block);
				}
				if (flag)
				{
					outline.color = state - 1;
				}
				if (outline.enabled != flag)
				{
					outline.enabled = flag;
					if (flag)
					{
						OutlineEffect.ToggleOutline(flag);
					}
				}
				flag2 = true;
			}
		}
		if (flag2)
		{
			outlineSetUp = true;
		}
		return flag2;
	}

	public void RemoveOutline()
	{
		for (int i = 0; i < outlines.Length; i++)
		{
			UnityEngine.Object.Destroy(outlines[i]);
		}
		outlines = null;
	}

	public virtual void UpdateShadowCastingMode()
	{
		if (shadows.Length > 0 && selectedSkin.isDefault)
		{
			return;
		}
		for (int i = 0; i < renderers.Length; i++)
		{
			ShadowCastingMode shadowCastingMode = renderers[i].shadowCastingMode;
			if (shadowCastingMode == ShadowCastingMode.On || shadowCastingMode == ShadowCastingMode.TwoSided)
			{
				renderers[i].shadowCastingMode = ((!OptionsMaster.BesiegeConfig.ShadowsDoubled) ? ShadowCastingMode.On : ShadowCastingMode.TwoSided);
			}
		}
	}

	protected virtual void SetMesh(Mesh m)
	{
		MeshFilter.sharedMesh = m;
	}

	protected virtual Renderer DefaultRenderer()
	{
		return renderers[0];
	}

	protected virtual bool HasRenderer()
	{
		return renderers.Length > 0 && renderers[0] != null;
	}

	protected virtual void EnableRenderer(bool e)
	{
		if (!e)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				if (renderers[i] != null)
				{
					renVisible.Add(renderers[i].enabled);
					renderers[i].enabled = false;
				}
			}
			return;
		}
		for (int i = 0; i < renderers.Length; i++)
		{
			if (renderers[i] != null)
			{
				renderers[i].enabled = i >= renVisible.Count || renVisible[i];
			}
		}
	}

	protected virtual void SetMaterial(Material mat)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			MeshRenderer meshRenderer = renderers[i];
			if (meshRenderer == null)
			{
				continue;
			}
			Material[] sharedMaterials = meshRenderer.sharedMaterials;
			if (sharedMaterials.Length > 1)
			{
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					sharedMaterials[j] = mat;
				}
				meshRenderer.sharedMaterials = sharedMaterials;
			}
			else
			{
				meshRenderer.sharedMaterial = mat;
			}
		}
	}

	protected virtual void SetMaterial(Material[] mats, bool splitMats)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			MeshRenderer meshRenderer = renderers[i];
			if (!(meshRenderer == null))
			{
				if (splitMats)
				{
					meshRenderer.sharedMaterial = mats[i];
				}
				else
				{
					meshRenderer.sharedMaterials = mats;
				}
			}
		}
	}

	protected virtual void SetMaterialProperties(MaterialPropertyBlock prop)
	{
		Vector4 value = new Vector4(1f, 1f, 0f, 0f);
		for (int i = 0; i < renderers.Length; i++)
		{
			if (renderers[i] == null)
			{
				continue;
			}
			if (tiling != Vector4.zero)
			{
				if (i == renderers.Length - 1)
				{
					props.SetVector("_MainTex_ST", tiling);
				}
				else
				{
					props.SetVector("_MainTex_ST", value);
				}
			}
			renderers[i].SetPropertyBlock(prop);
		}
	}

	public void Start()
	{
		BlockType type = Prefab.Type;
		if (type != BlockType.Pin && type != BlockType.CameraBlock && type != BlockType.BuildNode && type != BlockType.BuildEdge)
		{
			if (!Block.isSimulating && Prefab.hasArrow)
			{
				StatMaster.hudHiddenChanged += ToggleArrow;
			}
			ReferenceMaster.onBlockShadowsChanged = (Action)Delegate.Combine(ReferenceMaster.onBlockShadowsChanged, new Action(UpdateShadowCastingMode));
		}
	}

	protected void OnApplicationQuit()
	{
		quitting = true;
	}

	protected virtual void OnDestroy()
	{
		if (quitting)
		{
			return;
		}
		isDestroyed = true;
		ReferenceMaster.onBlockShadowsChanged = (Action)Delegate.Remove(ReferenceMaster.onBlockShadowsChanged, new Action(UpdateShadowCastingMode));
		StatMaster.hudHiddenChanged -= ToggleArrow;
		if (SingleInstance<BlockSkinLoader>.hasInstance() && selectedSkin != null)
		{
			selectedSkin.Unregister(this);
		}
		if (SingleInstance<PrefabMaster>.hasInstance() && PrefabMaster.BlockPrefabs.ContainsKey(ID))
		{
			BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[ID];
			if (blockPrefab != null && blockPrefab.blockVisControllers.Contains(this))
			{
				blockPrefab.blockVisControllers.Remove(this);
			}
		}
	}
}
