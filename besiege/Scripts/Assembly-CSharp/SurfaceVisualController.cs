using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceVisualController : FragmentVisualController
{
	public bool hasBroken;

	private BuildSurface surface;

	private BuildSurface visSource;

	private SurfaceFragmentController fragmentController;

	public bool isGlass;

	public bool isPainted;

	public Color paintColor = Color.red;

	public Color glassColor = Color.white;

	public float tint = 1f;

	private bool started;

	private static Material intensityMaterialSurface;

	public override void StartController()
	{
		if (!started)
		{
			started = true;
			if (selectedSkin == null)
			{
				selectedSkin = base.Prefab.DefaultSkin;
			}
			if (Block.visAddedToMe == null)
			{
				Block.visAddedToMe = new List<Renderer>();
			}
			surface = (BuildSurface)Block;
			visSource = ((!Block.isSimulating) ? surface : ((BuildSurface)Block.BuildingBlock));
			mainRen = renderers[0];
			fragmentController = Block.GetComponent<SurfaceFragmentController>();
			if (!Block.isSimulating)
			{
				visSource.SetupVisualDependencies();
			}
		}
	}

	public override void OnJointBreak(float breakForce)
	{
		if (!surface.isValid || !breakIntoPieces || hasBroken)
		{
			return;
		}
		BreakSurface();
		if (fragmentController != null)
		{
			fragmentController.OnSurfaceBreak();
		}
		if (StatMaster.isMP && surface.SimPhysics)
		{
			NetworkBlock netBlock = surface.NetBlock;
			if (netBlock != null)
			{
				netBlock.pollTransform = false;
				netBlock.Event(NetworkEntity.EntityEvent.Break);
			}
		}
		ConfigurableJoint[] components = GetComponents<ConfigurableJoint>();
		StartCoroutine(FindBrokenJointAndNotifyOther(components));
		surface.RestoreStoredVelocities();
		CopyMaterialProperties();
	}

	private void BreakSurface()
	{
		if (!hasBroken)
		{
			MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
			if (instance.target == base.transform)
			{
				instance.SoftResetCamTarget();
			}
			hasBroken = true;
			InvokeOnVisualBreak();
			for (int i = 0; i < disableOnBreak.Length; i++)
			{
				disableOnBreak[i].gameObject.SetActive(false);
			}
			if (surface.SimPhysics)
			{
				surface.meshCollider.enabled = false;
			}
			surface.PlayBreakParticles();
		}
	}

	public void OnRemoteBreak()
	{
		BreakSurface();
		CopyMaterialProperties();
	}

	private IEnumerator FindBrokenJointAndNotifyOther(ConfigurableJoint[] joints)
	{
		GameObject[] connectedObjects = new GameObject[joints.Length];
		for (int i = 0; i < joints.Length; i++)
		{
			connectedObjects[i] = surface.GetConnectedBlock(joints[i]);
		}
		yield return new WaitForFixedUpdate();
		surface.PreBreakJoints();
		for (int j = 0; j < joints.Length; j++)
		{
			if (joints[j] == null)
			{
				GameObject other = connectedObjects[j];
				if (!(other == null))
				{
					SurfaceVisualController otherVis = other.GetComponent<SurfaceVisualController>();
					if (!(otherVis == null))
					{
						otherVis.OnJointBreak(0f);
					}
				}
			}
			else
			{
				UnityEngine.Object.Destroy(joints[j]);
			}
		}
		yield return new WaitForFixedUpdate();
		surface.OnBreakComplete();
	}

	public override void CopyMaterialProperties()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		if (mainRen != null)
		{
			mainRen.GetPropertyBlock(materialPropertyBlock);
		}
		for (int i = 0; i < brokenVis.Length; i++)
		{
			if (brokenVis[i].renderer != null)
			{
				brokenVis[i].renderer.SetPropertyBlock(materialPropertyBlock);
			}
		}
	}

	public void UpdateSurfaceMaterial()
	{
		SetNormal();
	}

	public void UpdateIsGlass(bool b)
	{
		StartController();
		isGlass = b;
		if (StatMaster.stressCoded || StatMaster.clusterCoded || StatMaster.aeroCoded)
		{
			SetIntensityTexture(true);
		}
	}

	public void UpdatePaintedMaterial(bool b)
	{
		StartController();
		isPainted = b;
		SetNormal();
	}

	public void AssignColor(Color c)
	{
		StartController();
		paintColor = c;
		SetMaterialProperty("_TintColor", paintColor);
		if (isPainted)
		{
			SetMaterialProperties();
		}
	}

	public void AssignTiling(Vector2 tiling)
	{
		StartController();
		SetMaterialProperty("_MainTex_ST", new Vector4(tiling.x, tiling.y, 0f, 0f));
		SetMaterialProperties();
	}

	public void AssignTint(float v)
	{
		StartController();
		tint = v;
		if (isGlass)
		{
			SetMaterialProperty("_Color", new Color(glassColor.r * v, glassColor.g * v, glassColor.b * v, glassColor.a));
		}
		else
		{
			SetMaterialProperty("_Color", Color.white);
		}
		SetMaterialProperties();
	}

	private void UpdateMat()
	{
		StartController();
		if (selectedSkin == null || selectedSkin.isDefault || !OptionsMaster.skinsEnabled)
		{
			renderers[0].sharedMaterial = ((!isPainted) ? visSource.CurrentMaterial : visSource.CurrentPaintedMaterial);
		}
		else
		{
			UpdateMats(selectedSkin.materials, selectedSkin.materials);
		}
		if (Block.isSimulating)
		{
			SetMaterialProperty("_TintColor", paintColor);
		}
		SetMaterialProperty("_HSVEnabled", (!isPainted) ? 0f : 1f);
		SetMaterialProperties();
		lastClusterIndex = -3;
	}

	public override void SetNormal()
	{
		if (!NormalBypassCases() && !StatMaster.isHeadless && CanChangeTexture && (!lockSelectHighlight || !base.Selected))
		{
			UpdateMat();
			SetBrokenFragmentMaterial(selectedSkin.material);
			InvokeSetToNormalAction();
		}
	}

	public override bool UpdateMat(Material mat)
	{
		if (mat == null)
		{
			return false;
		}
		Material[] array = new Material[1] { mat };
		return UpdateMats(array, array);
	}

	public override bool UpdateMats(Material[] mats, Material[] shortMats)
	{
		if (mats == null || shortMats == null || !CanChangeTexture)
		{
			return false;
		}
		lastClusterIndex = -3;
		for (int i = 0; i < renderers.Length; i++)
		{
			MeshRenderer meshRenderer = renderers[i];
			if (!(meshRenderer == null))
			{
				meshRenderer.sharedMaterials = mats;
			}
		}
		return true;
	}

	public override void SetBrokenFragmentMaterial(Material mat)
	{
		if (selectedSkin == null)
		{
			return;
		}
		Material sharedMaterial = ((OptionsMaster.skinsEnabled && !selectedSkin.isDefault) ? mat : mainRen.sharedMaterial);
		for (int i = 0; i < brokenVis.Length; i++)
		{
			MeshRenderer renderer = brokenVis[i].renderer;
			if (renderer != null)
			{
				if (StatMaster.clusterCoded)
				{
					renderer.material = GetClusterMaterial();
				}
				else
				{
					brokenVis[i].renderer.sharedMaterial = sharedMaterial;
				}
			}
		}
	}

	public override void ColourCodeFromIntensity(Action update)
	{
		if (!base.Prefab.CanGetNewVisuals || !CanChangeTexture)
		{
			return;
		}
		if (StatMaster.stressCoded)
		{
			if (intensityMaterialSurface == null)
			{
				intensityMaterialSurface = ReferenceMaster.Instance.aerodynamicMaterialSurface;
			}
			SetIntensityTexture(false);
			ColourCodeFromIntensity(update, intensityMaterialSurface);
		}
		else
		{
			GetIntensityMaterial();
			ColourCodeFromIntensity(update, BlockVisualController.intensityMaterial);
		}
		surface = (BuildSurface)Block;
		surface.AssignStressCorners();
	}

	public override void SetIntensityTexture(bool update)
	{
		if (isGlass)
		{
			visSource = ((!Block.isSimulating) ? surface : ((BuildSurface)Block.BuildingBlock));
			AssignMaterialProperty("_TexProp", visSource.CurrentMaterial.mainTexture, update);
		}
		else
		{
			base.SetIntensityTexture(update);
		}
	}

	public override void UpdateStressDisplay()
	{
		if (StatMaster.stressCoded)
		{
			float[] array = new float[4];
			if (Block.isSimulating && (bool)surface)
			{
				array = surface.GetStresses();
			}
			UpdateIntensityMaterial("_AeroColor", array[0], false);
			UpdateIntensityMaterial("_AeroColor2", array[1], false);
			UpdateIntensityMaterial("_AeroColor3", array[2], false);
			UpdateIntensityMaterial("_AeroColor4", array[3]);
		}
	}

	public override void UpdateShadowCastingMode()
	{
	}
}
