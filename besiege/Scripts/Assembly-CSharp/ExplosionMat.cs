using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExplosionMat : MonoBehaviour
{
	public enum Noise
	{
		Octaves_1 = 1,
		Octaves_2 = 2,
		Octaves_3 = 3,
		Octaves_4 = 4,
		Octaves_5 = 5
	}

	public enum Quality
	{
		Low = 0,
		Medium = 1,
		High = 2
	}

	public bool invokeUpdate = true;

	private string[] qualitySel = new string[3] { "QUALITY_LOW", "QUALITY_MED", "QUALITY_HIGH" };

	public Noise octaves = Noise.Octaves_4;

	public Quality quality;

	public bool scattering = true;

	private Noise lastoctaves = Noise.Octaves_4;

	private Quality lastquality;

	private bool lastscatter = true;

	[Range(0f, 1f)]
	public float alpha = 1f;

	public float heat = 1f;

	public float scrollSpeed = 1f;

	public float frequency = 1f;

	private float radius;

	private float lastheat = 1f;

	private float lastalpha = 1f;

	private float lastscroll = 1f;

	private float lastfreq = 1f;

	[Header("Texture Settings")]
	public Texture2D ramp;

	public Texture2D noise;

	public Renderer ren;

	public Renderer ripple;

	protected MaterialPropertyBlock prop;

	protected MaterialPropertyBlock rippleProp;

	private bool alwaysUseSharedMat = true;

	private bool handleWater;

	private Material underMat;

	private Material overMat;

	private static bool hasReferenceMaster;

	private bool hasAssigned;

	public void Start()
	{
		prop = new MaterialPropertyBlock();
		rippleProp = new MaterialPropertyBlock();
		Setup();
	}

	public void AssignMaterials()
	{
		if (!hasReferenceMaster)
		{
			hasReferenceMaster = ReferenceMaster.Instance != null;
			if (!hasReferenceMaster)
			{
				return;
			}
		}
		hasAssigned = true;
		if (ren.sharedMaterial.name.ToLower().Contains("(water)"))
		{
			overMat = ren.sharedMaterial;
			underMat = ReferenceMaster.Instance.explosionOverlay;
		}
		else
		{
			overMat = ren.sharedMaterial;
			underMat = ReferenceMaster.Instance.explosionOpaque;
		}
		if (WaterController.Exist)
		{
			ChangeMat(!WaterFogController.overWater);
		}
	}

	private void OnDestroy()
	{
		if (handleWater)
		{
			handleWater = false;
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(ChangeMat));
		}
	}

	private void ChangeMat(bool under)
	{
		ren.sharedMaterial = ((!under) ? overMat : underMat);
	}

	private void Setup()
	{
		ren = GetComponent<Renderer>();
		SetupMaterialProperties();
		SetShaderKeywords();
	}

	public void Update()
	{
		if (invokeUpdate)
		{
			UpdateMaterial();
		}
	}

	private void UpdateMaterial()
	{
		float num = Mathf.Min(base.transform.lossyScale.x, Mathf.Min(base.transform.lossyScale.y, base.transform.lossyScale.z));
		bool flag = false;
		if (num != radius)
		{
			radius = num;
			prop.SetFloat("_Radius", radius / 2.03f - 2f);
			flag = true;
		}
		if (lastheat != heat)
		{
			lastheat = heat;
			prop.SetFloat("_Heat", heat);
		}
		if (lastalpha != alpha)
		{
			lastalpha = alpha;
			prop.SetFloat("_Alpha", alpha);
		}
		if (lastscroll != scrollSpeed)
		{
			lastscroll = scrollSpeed;
			prop.SetFloat("_ScrollSpeed", scrollSpeed);
		}
		if (lastfreq != frequency)
		{
			lastfreq = frequency;
			prop.SetFloat("_Frequency", frequency);
		}
		if (WaterController.Exist)
		{
			float value = WaterController.CheckHeightMap(base.transform.position.x, base.transform.position.z);
			prop.SetFloat("_WaterHeight", value);
			if (ripple != null)
			{
				rippleProp.SetFloat("_WaterHeight", value);
				ripple.SetPropertyBlock(rippleProp);
			}
		}
		if (flag)
		{
			ren.SetPropertyBlock(prop);
		}
	}

	private void SetupMaterialProperties()
	{
		if (ramp != null)
		{
			prop.SetTexture("_RampTex", ramp);
		}
		if (noise != null)
		{
			prop.SetTexture("_MainTex", noise);
		}
		prop.SetFloat("_Heat", heat);
		prop.SetFloat("_Alpha", alpha);
		prop.SetFloat("_ScrollSpeed", scrollSpeed);
		prop.SetFloat("_Frequency", frequency);
		if (WaterController.Exist)
		{
			float value = WaterController.CheckHeightMap(base.transform.position.x, base.transform.position.z);
			prop.SetFloat("_WaterHeight", value);
			if (ripple != null)
			{
				rippleProp.SetFloat("_WaterHeight", value);
				ripple.SetPropertyBlock(rippleProp);
			}
		}
		else
		{
			prop.SetFloat("_WaterHeight", -99999f);
			if (ripple != null)
			{
				rippleProp.SetFloat("_WaterHeight", -99999f);
				ripple.SetPropertyBlock(rippleProp);
			}
		}
		ren.SetPropertyBlock(prop);
	}

	private void SetShaderKeywords()
	{
		if (lastscatter != scattering || lastoctaves != octaves || lastquality != quality)
		{
			lastscatter = scattering;
			lastoctaves = octaves;
			lastquality = quality;
			List<string> list = new List<string>();
			list.Add((!scattering) ? "SCATTERING_OFF" : "SCATTERING_ON");
			list.Add("OCTAVES_" + (int)octaves);
			list.Add(qualitySel[(int)quality]);
			List<string> list2 = list;
			if (alwaysUseSharedMat)
			{
				ren.sharedMaterial.shaderKeywords = list2.ToArray();
			}
			else
			{
				ren.material.shaderKeywords = list2.ToArray();
			}
		}
	}
}
